using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace PclUnlock
{
    /// <summary>
    /// PCL 联网下载解锁：在 PCL 主程序上做定点 IL 补丁，
    /// 解除作者在程序里硬编码的功能限制（强制隐藏的联机页、更新/下载的赞助门槛、拦截用的公告）。
    /// 兼容 2.13.x 正式版 / 2.14.x 快照版，缺失的特征会自动跳过。
    /// </summary>
    internal static class Program
    {
        private const string HookTypeName = "PCLFix.Hooks";

        private static readonly List<string> Report = new List<string>();
        private static int _ok, _skip, _fail;

        private static int Main(string[] args)
        {
            try { Console.OutputEncoding = Encoding.UTF8; } catch { }
            Console.Title = "PCL 联网下载解锁";

            Console.WriteLine("==============================================");
            Console.WriteLine(" PCL 联网下载解锁  v1.0");
            Console.WriteLine(" 解除作者硬编码的联网/下载限制（快照版 + 正式版通用）");
            Console.WriteLine("==============================================");
            Console.WriteLine();

            string target = args.Length > 0 ? args[0] : null;
            if (target == null)
            {
                target = FindPclExe();
                if (target == null)
                {
                    Console.WriteLine("[失败] 当前目录下没找到 PCL 主程序（Plain Craft Launcher 2.exe）。");
                    Console.WriteLine("       请把本程序放到 PCL 所在文件夹后运行，或把 exe 路径拖到本程序上。");
                    Pause();
                    return 2;
                }
            }
            target = Path.GetFullPath(target);
            if (!File.Exists(target))
            {
                Console.WriteLine("[失败] 文件不存在：" + target);
                Pause();
                return 2;
            }

            Console.WriteLine("目标程序：" + target);
            Console.WriteLine("文件大小：" + new FileInfo(target).Length.ToString("N0") + " 字节");
            Console.WriteLine();

            string tempDir = null;
            try
            {
                var resolver = new DefaultAssemblyResolver();
                resolver.AddSearchDirectory(Path.GetDirectoryName(target));

                var asm = AssemblyDefinition.ReadAssembly(target, new ReaderParameters
                {
                    ReadWrite = false,
                    ReadingMode = ReadingMode.Deferred,
                    AssemblyResolver = resolver
                });
                var mod = asm.MainModule;

                Console.WriteLine("程序集  ：" + asm.Name.Name + "  " + asm.Name.Version);
                Console.WriteLine("模块    ：" + mod.Name);
                Console.WriteLine();

                if (!asm.Name.Name.StartsWith("Plain Craft Launcher", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("[失败] 这不是 PCL 主程序（程序集名：" + asm.Name.Name + "）。");
                    Pause();
                    return 3;
                }

                // 依赖 DLL 内嵌在 PCL.Resources.resources 里，Cecil 写回时需要解析，先解出来
                tempDir = ExtractEmbeddedDependencies(mod, resolver);

                Console.WriteLine("--- 开始打补丁 ---");
                bool changed = false;
                changed |= PatchForcedHiddenPages(mod);
                changed |= PatchHasUpdatePermission(mod);
                changed |= PatchUpdateNotice(mod);
                changed |= PatchLinkPage(mod);
                changed |= PatchLinkEnabled(mod);

                if (!changed)
                {
                    Console.WriteLine();
                    Console.WriteLine("没有需要修改的内容（可能已经解锁过了）。");
                    asm.Dispose();
                    Pause();
                    return 0;
                }

                // 备份
                string backup = target + ".pclunlock.bak";
                if (!File.Exists(backup))
                {
                    File.Copy(target, backup, true);
                    Console.WriteLine();
                    Console.WriteLine("[备份] " + Path.GetFileName(backup));
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("[备份] 已存在，保留最早的备份：" + Path.GetFileName(backup));
                }

                // 写回（先写临时文件，成功后再替换）
                string tmp = target + ".new";
                if (File.Exists(tmp)) File.Delete(tmp);
                asm.Write(tmp);
                asm.Dispose();

                // 校验写出的程序集能重新读取
                using (var check = AssemblyDefinition.ReadAssembly(tmp, new ReaderParameters { ReadWrite = false }))
                {
                    if (check.MainModule.Types.Count < 50)
                        throw new Exception("写出的程序集类型数量异常");
                }

                File.Copy(tmp, target, true);
                File.Delete(tmp);

                Console.WriteLine();
                Console.WriteLine("--- 完成 ---");
                Console.WriteLine("已写入：" + target);
                Console.WriteLine("原文件备份：" + backup);
                Console.WriteLine();
                Console.WriteLine("请重新启动 PCL 生效。");
                Pause();
                return 0;
            }
            catch (IOException ex)
            {
                Console.WriteLine();
                Console.WriteLine("[失败] 文件被占用，请先完全退出 PCL 再运行本程序。");
                Console.WriteLine("       " + ex.Message);
                Pause();
                return 4;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("[失败] " + ex.GetType().Name + "：" + ex.Message);
                Pause();
                return 1;
            }
            finally
            {
                if (tempDir != null)
                {
                    try { Directory.Delete(tempDir, true); } catch { }
                }
            }
        }

        // ------------------------------------------------------------ 定位

        private static string FindPclExe()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            var candidates = new List<string>();
            try
            {
                candidates.AddRange(Directory.GetFiles(dir, "*.exe", SearchOption.TopDirectoryOnly));
            }
            catch { }

            // 优先匹配已知文件名
            string[] prefer =
            {
                "Plain Craft Launcher 2.exe",
                "PCL Fix.exe",
                "PCL2.exe",
                "Plain Craft Launcher.exe"
            };
            foreach (var p in prefer)
            {
                var f = Path.Combine(dir, p);
                if (File.Exists(f)) return f;
            }

            foreach (var f in candidates)
            {
                if (string.Equals(Path.GetFileName(f), Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                        StringComparison.OrdinalIgnoreCase)) continue;
                try
                {
                    var an = System.Reflection.AssemblyName.GetAssemblyName(f);
                    if (an.Name.StartsWith("Plain Craft Launcher", StringComparison.OrdinalIgnoreCase))
                        return f;
                }
                catch { }
            }
            return null;
        }

        // ------------------------------------------- 内嵌依赖解压（供 Cecil 解析）

        private static string ExtractEmbeddedDependencies(ModuleDefinition mod, IAssemblyResolver resolver)
        {
            var resources = mod.Resources.OfType<EmbeddedResource>()
                .Where(r => r.Name.EndsWith(".resources", StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (resources.Count == 0) return null;

            string dir = Path.Combine(Path.GetTempPath(), "pclunlock_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(dir);

            int n = 0;
            try
            {
                foreach (var res in resources)
                {
                    try
                    {
                        using (var stream = res.GetResourceStream())
                        using (var reader = new ResourceReader(stream))
                        {
                            var e = reader.GetEnumerator();
                            while (e.MoveNext())
                            {
                                if (!(e.Value is byte[] data) || data.Length < 1024) continue;
                                if (data[0] != (byte)'M' || data[1] != (byte)'Z') continue;   // 只取 PE
                                string name = e.Key as string;
                                if (string.IsNullOrEmpty(name)) continue;
                                File.WriteAllBytes(Path.Combine(dir, name + ".dll"), data);
                                n++;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }

            if (n > 0 && resolver is DefaultAssemblyResolver dar)
            {
                dar.AddSearchDirectory(dir);
                Console.WriteLine("内嵌依赖：已解出 " + n + " 个 DLL 供解析使用");
                return dir;
            }

            try { Directory.Delete(dir, true); } catch { }
            return null;
        }

        // ------------------------------------------- 补丁 4：恢复被硬编码隐藏的联机页

        private static bool PatchLinkPage(ModuleDefinition mod)
        {
            var ui = FindType(mod, "PCL.PageSetupUI");
            var refresh = FindMethod(ui, "HiddenRefresh", 0);
            if (refresh == null)
            {
                Line("SKIP", "未找到 PageSetupUI.HiddenRefresh，跳过联机页解锁");
                return false;
            }

            var ins = refresh.Body.Instructions;
            for (int i = 0; i + 1 < ins.Count; i++)
            {
                if (ins[i].OpCode != OpCodes.Call && ins[i].OpCode != OpCodes.Callvirt) continue;
                if (!(ins[i].Operand is MethodReference mr) || mr.Name != "get_BtnTitleSelect2") continue;

                var next = ins[i + 1];
                if (next.OpCode == OpCodes.Ldc_I4_0)          // 已经是 Visible
                {
                    Line("SKIP", "联机页本来就是显示的");
                    return false;
                }
                if (next.OpCode != OpCodes.Ldc_I4_2) continue; // 期望是 Collapsed

                next.OpCode = OpCodes.Ldc_I4_0;                // Collapsed -> Visible
                next.Operand = null;
                refresh.Body.OptimizeMacros();
                Line("OK", "恢复被硬编码隐藏的联机页（导航栏「联机」重新出现）");
                return true;
            }

            Line("SKIP", "没找到 BtnTitleSelect2 的可见性赋值，跳过联机页解锁");
            return false;
        }
        // ------------------------------------------- 补丁 5：解除联机页的服务端开关拦截

        private static bool PatchLinkEnabled(ModuleDefinition mod)
        {
            var page = FindType(mod, "PCL.PageLinkMain");
            var init = FindMethod(page, "InitConfig", 1);
            if (init == null)
            {
                Line("SKIP", "未找到 PageLinkMain.InitConfig，跳过联机开关解锁");
                return false;
            }

            int hit = 0;
            foreach (var ins in init.Body.Instructions)
            {
                if (ins.OpCode != OpCodes.Ldstr) continue;
                if ((string)ins.Operand != "DisableReason2") continue;
                ins.Operand = "DisableReason2#unlocked";   // 读不到这个键 -> 不再抛“联机已暂时关闭”
                hit++;
            }

            if (hit == 0)
            {
                Line("SKIP", "该版本没有联机开关拦截（或已解锁）");
                return false;
            }

            Line("OK", "解除「联机功能已暂时关闭」拦截（联机页可以真正用了）");
            return true;
        }
        // ------------------------------------------------------------ 工具

        private static void Line(string level, string text)
        {
            switch (level)
            {
                case "OK": _ok++; break;
                case "SKIP": _skip++; break;
                case "FAIL": _fail++; break;
            }
            Console.WriteLine("[" + level + "] " + text);
            Report.Add(level + " " + text);
        }

        private static TypeDefinition FindType(ModuleDefinition mod, string fullName)
        {
            return mod.GetType(fullName);
        }

        private static MethodDefinition FindMethod(TypeDefinition type, string name, int paramCount)
        {
            if (type == null) return null;
            foreach (var m in type.Methods)
            {
                if (m.Name != name) continue;
                if (paramCount >= 0 && m.Parameters.Count != paramCount) continue;
                if (m.Body == null) continue;
                return m;
            }
            return null;
        }

        private static void AppendBeforeLastRet(MethodDefinition m, Action<ILProcessor> emit)
        {
            var il = m.Body.GetILProcessor();
            Instruction lastRet = null;
            for (int i = m.Body.Instructions.Count - 1; i >= 0; i--)
                if (m.Body.Instructions[i].OpCode == OpCodes.Ret) { lastRet = m.Body.Instructions[i]; break; }
            if (lastRet == null) throw new Exception("方法里没有 ret：" + m.FullName);

            int pre = m.Body.Instructions.Count;
            emit(il);
            var added = new List<Instruction>();
            for (int i = pre; i < m.Body.Instructions.Count; i++) added.Add(m.Body.Instructions[i]);
            foreach (var ins in added) m.Body.Instructions.Remove(ins);
            foreach (var ins in added) il.InsertBefore(lastRet, ins);
            m.Body.OptimizeMacros();
        }

        private static void Pause()
        {
            Console.WriteLine();
            if (Console.IsInputRedirected) return;   // 由脚本/管道调用时不阻塞
            Console.WriteLine("按任意键退出…");
            try { Console.ReadKey(true); } catch { }
        }

        // ------------------------------------------- 补丁 1：解除强制隐藏的页面

        private static bool PatchForcedHiddenPages(ModuleDefinition mod)
        {
            var settings = FindType(mod, "PCL.Settings");
            var get = FindMethod(settings, "Get", 2);
            if (get == null)
            {
                Line("SKIP", "未找到 Settings.Get，跳过「强制隐藏页面」解锁");
                return false;
            }

            string[] keys = { "UiHiddenPageLink", "UiHiddenSetupLink" };
            int hit = 0;
            foreach (var ins in get.Body.Instructions)
            {
                if (ins.OpCode != OpCodes.Ldstr) continue;
                var s = ins.Operand as string;
                if (s == null) continue;
                if (!keys.Contains(s)) continue;
                ins.Operand = s + "#unlocked";   // 让 ForcedSettings 里查不到这个键
                hit++;
            }

            if (hit == 0)
            {
                Line("SKIP", "该版本没有强制隐藏的页面（或已解锁）");
                return false;
            }

            Line("OK", "解除强制隐藏：" + string.Join("、", keys) + "（共 " + hit + " 处）");
            return true;
        }

        // ------------------------------------------- 补丁 2：解除更新/下载的赞助门槛

        private static bool PatchHasUpdatePermission(ModuleDefinition mod)
        {
            var secret = FindType(mod, "PCL.ModSecret");
            var prop = FindMethod(secret, "get_HasUpdatePermission", 0);
            if (prop == null)
            {
                Line("SKIP", "该版本没有 HasUpdatePermission（可能本来就不需要赞助码）");
                return false;
            }

            var body = prop.Body;
            if (body.Instructions.Count == 2 &&
                body.Instructions[0].OpCode == OpCodes.Ldc_I4_1 &&
                body.Instructions[1].OpCode == OpCodes.Ret)
            {
                Line("SKIP", "更新/下载权限本来就是放开的");
                return false;
            }

            body.Instructions.Clear();
            body.Variables.Clear();
            body.ExceptionHandlers.Clear();
            body.InitLocals = false;
            var il = body.GetILProcessor();
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Ret);
            body.OptimizeMacros();

            Line("OK", "解除更新/下载的赞助门槛（HasUpdatePermission 恒为 true）");
            return true;
        }

        // ------------------------------------------- 补丁 3：跳过拦截用的更新类公告

        private static bool PatchUpdateNotice(ModuleDefinition mod)
        {
            var secret = FindType(mod, "PCL.ModSecret");
            var sub = FindMethod(secret, "ServerSub", 1);
            if (sub == null)
            {
                Line("SKIP", "未找到 ServerSub，跳过公告解锁");
                return false;
            }

            // 已经打过补丁？
            foreach (var ins in sub.Body.Instructions)
            {
                if (ins.OpCode != OpCodes.Call && ins.OpCode != OpCodes.Callvirt) continue;
                if (ins.Operand is MethodReference mr && mr.DeclaringType.FullName == HookTypeName)
                {
                    Line("SKIP", "公告解锁已经打过补丁");
                    return false;
                }
            }

            var hook = EnsureHookType(mod);

            var instructions = sub.Body.Instructions;
            for (int i = 1; i + 1 < instructions.Count; i++)
            {
                if (instructions[i].OpCode != OpCodes.Ldstr || (string)instructions[i].Operand != "id") continue;
                if (instructions[i + 1].OpCode != OpCodes.Callvirt) continue;
                if (!(instructions[i + 1].Operand is MethodReference mr2) || mr2.Name != "get_Item") continue;
                var prev = instructions[i - 1];
                if (prev.OpCode != OpCodes.Ldloc && prev.OpCode != OpCodes.Ldloc_S) continue;

                var local = (VariableDefinition)prev.Operand;
                var il = sub.Body.GetILProcessor();
                var call = Instruction.Create(OpCodes.Call, hook);
                var ldloc2 = Instruction.Create(OpCodes.Ldloc, local);
                il.InsertAfter(prev, call);
                il.InsertAfter(call, ldloc2);
                sub.Body.OptimizeMacros();

                Line("OK", "跳过更新类公告（“悲报 / 不再提供下载”这类拦截弹窗不再出现）");
                return true;
            }

            Line("SKIP", "ServerSub 里没找到公告循环，跳过公告解锁");
            return false;
        }

        private static MethodReference EnsureHookType(ModuleDefinition mod)
        {
            var existing = FindType(mod, HookTypeName);
            if (existing != null)
            {
                var m = FindMethod(existing, "NeutralizeIfUpdate", 1) ?? FindMethod(existing, "SkipUpdateNotice", 1);
                if (m != null) return m;
            }

            var jGetItem = FindExistingCall(mod, "JObject", "get_Item");
            if (jGetItem == null) throw new Exception("找不到 Newtonsoft.Json 引用，无法注入公告钩子");
            var jObject = jGetItem.DeclaringType;
            var jToken = jGetItem.ReturnType;

            var jRemove = MakeMethod(jObject, "Remove", mod.TypeSystem.Boolean, mod.TypeSystem.String);
            var jToString = MakeMethod(jToken, "ToString", mod.TypeSystem.String);
            var strEquals = MakeMethod(mod.TypeSystem.String, "Equals", mod.TypeSystem.Boolean, mod.TypeSystem.String);

            TypeDefinition hooks = existing;
            if (hooks == null)
            {
                hooks = new TypeDefinition("PCLFix", "Hooks",
                    TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.Class,
                    mod.TypeSystem.Object);
                mod.Types.Add(hooks);
            }

            var method = new MethodDefinition("NeutralizeIfUpdate",
                MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig,
                mod.TypeSystem.Void);
            method.Parameters.Add(new ParameterDefinition("notice", ParameterAttributes.None, mod.TypeSystem.Object));
            hooks.Methods.Add(method);

            var body = method.Body;
            body.InitLocals = true;
            var jo = new VariableDefinition(jObject);
            var tk = new VariableDefinition(jToken);
            body.Variables.Add(jo);
            body.Variables.Add(tk);

            var il = body.GetILProcessor();
            var l1 = Instruction.Create(OpCodes.Nop);
            var l2 = Instruction.Create(OpCodes.Nop);
            var l3 = Instruction.Create(OpCodes.Nop);

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Isinst, jObject);
            il.Emit(OpCodes.Stloc, jo);
            il.Emit(OpCodes.Ldloc, jo);
            il.Emit(OpCodes.Brtrue, l1);
            il.Emit(OpCodes.Ret);

            il.Append(l1);
            il.Emit(OpCodes.Ldloc, jo);
            il.Emit(OpCodes.Ldstr, "isUpdate");
            il.Emit(OpCodes.Callvirt, jGetItem);
            il.Emit(OpCodes.Stloc, tk);
            il.Emit(OpCodes.Ldloc, tk);
            il.Emit(OpCodes.Brtrue, l2);
            il.Emit(OpCodes.Ret);

            il.Append(l2);
            il.Emit(OpCodes.Ldloc, tk);
            il.Emit(OpCodes.Callvirt, jToString);
            il.Emit(OpCodes.Ldstr, "True");
            il.Emit(OpCodes.Callvirt, strEquals);
            il.Emit(OpCodes.Brtrue, l3);
            il.Emit(OpCodes.Ret);

            il.Append(l3);
            il.Emit(OpCodes.Ldloc, jo);
            il.Emit(OpCodes.Ldstr, "id");
            il.Emit(OpCodes.Callvirt, jRemove);
            il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ret);

            body.OptimizeMacros();
            return method;
        }

        private static MethodReference FindExistingCall(ModuleDefinition mod, string typeName, string methodName)
        {
            foreach (var t in mod.GetTypes())
            foreach (var m in t.Methods)
            {
                if (m.Body == null) continue;
                foreach (var i in m.Body.Instructions)
                {
                    if (i.OpCode != OpCodes.Call && i.OpCode != OpCodes.Callvirt) continue;
                    if (i.Operand is MethodReference mr && mr.Name == methodName &&
                        mr.DeclaringType.Name == typeName)
                        return mr;
                }
            }
            return null;
        }

        private static MethodReference MakeMethod(TypeReference declaring, string name,
            TypeReference returnType, params TypeReference[] ps)
        {
            var mr = new MethodReference(name, returnType, declaring) { HasThis = true };
            foreach (var p in ps) mr.Parameters.Add(new ParameterDefinition(p));
            return mr;
        }
    }
}





