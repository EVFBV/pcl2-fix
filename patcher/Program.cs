using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace PclFixPatcher;

/// <summary>
/// PCL Fix 补丁器：在官方 PCL 2.14.0.0 快照版上做 4 组定点修改
///   1. 服务端公告里 isUpdate=true 的弹窗（“悲报”）不再显示
///   2. 启动页“快照版 PCL 包含尚未正式发布的测试功能…”提示条隐藏
///   3. 设置页“复制识别码 / 输入土豆码”整行（PanDonate）隐藏
///   4. 品牌名改为 “PCL Fix”（窗口标题 / 标题栏文字 / 关于页 / 程序集信息 / 单例窗口名）
/// </summary>
internal static class Program
{
    private static ModuleDefinition _mod;
    private static int _patchCount;

    private static int Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("usage: PclFixPatcher <input.exe> <output.exe> [--test-mutex]");
            return 2;
        }

        bool testMutex = args.Contains("--test-mutex");

        var resolver = new DefaultAssemblyResolver();
        resolver.AddSearchDirectory(Path.GetDirectoryName(Path.GetFullPath(args[0]))!);
        foreach (var extra in args.Skip(2))
            if (Directory.Exists(extra)) resolver.AddSearchDirectory(extra);
        var asm = AssemblyDefinition.ReadAssembly(args[0],
            new ReaderParameters { ReadWrite = false, AssemblyResolver = resolver });
        _mod = asm.MainModule;
        Console.WriteLine($"module   : {_mod.Name}");
        Console.WriteLine($"assembly : {asm.Name.Name} {asm.Name.Version}");
        Console.WriteLine($"types    : {_mod.Types.Count}");

        var hooks = CreateHookType();

        PatchNoticePopup(hooks);
        PatchSnapshotBanner();
        PatchDonateRow();
        PatchWindowTitle();
        PatchTitleLogo();
        PatchAboutPage();
        PatchMutexWindowName(testMutex);
        PatchTelemetry();
        PatchTelemetryCheckbox();
        PatchUpdateMessage();
        PatchAssemblyAttributes(asm);

        asm.Write(args[1]);
        asm.Dispose();

        PatchVersionResource(args[1]);

        Console.WriteLine($"patched  : {_patchCount} site(s) -> {args[1]}");
        return 0;
    }

    // ---------------------------------------------------------------- helpers

    private static void Ok(string msg)
    {
        _patchCount++;
        Console.WriteLine("  [ok] " + msg);
    }

    private static TypeDefinition Type(string fullName)
    {
        var t = _mod.GetType(fullName);
        if (t == null) throw new Exception("type not found: " + fullName);
        return t;
    }

    private static MethodDefinition Method(TypeDefinition t, string name, int paramCount = -1)
    {
        foreach (var m in t.Methods)
        {
            if (m.Name != name) continue;
            if (paramCount >= 0 && m.Parameters.Count != paramCount) continue;
            if (m.Body == null) continue;
            return m;
        }
        throw new Exception($"method not found: {t.FullName}::{name}/{paramCount}");
    }

    private static MethodReference PropGetter(TypeDefinition t, string prop)
    {
        foreach (var p in t.Properties)
            if (p.Name == prop && p.GetMethod != null) return p.GetMethod;
        throw new Exception($"getter not found: {t.FullName}.{prop}");
    }

    private static MethodReference PropSetter(TypeDefinition t, string prop)
    {
        foreach (var p in t.Properties)
            if (p.Name == prop && p.SetMethod != null) return p.SetMethod;
        throw new Exception($"setter not found: {t.FullName}.{prop}");
    }

    /// <summary>在整个模块里找一条已有的 call/callvirt，用来复用它的方法签名（避免手写外部类型引用）。</summary>
    private static MethodReference FindCall(string name)
    {
        foreach (var t in _mod.GetTypes())
        foreach (var m in t.Methods)
        {
            if (m.Body == null) continue;
            foreach (var i in m.Body.Instructions)
            {
                if (i.OpCode != OpCodes.Call && i.OpCode != OpCodes.Callvirt) continue;
                if (i.Operand is MethodReference mr && mr.Name == name) return mr;
            }
        }
        throw new Exception("existing call not found: " + name);
    }

    private static MethodReference FindCallOn(string declaringTypeName, string name)
    {
        foreach (var t in _mod.GetTypes())
        foreach (var m in t.Methods)
        {
            if (m.Body == null) continue;
            foreach (var i in m.Body.Instructions)
            {
                if (i.OpCode != OpCodes.Call && i.OpCode != OpCodes.Callvirt) continue;
                if (i.Operand is MethodReference mr && mr.Name == name &&
                    mr.DeclaringType.Name == declaringTypeName)
                    return mr;
            }
        }
        throw new Exception($"existing call not found: {declaringTypeName}::{name}");
    }

    /// <summary>在模块已有的 TypeReference 里找外部类型引用（不加载 PresentationFramework）。</summary>
    private static TypeReference TypeRef(string ns, string name)
    {
        foreach (var tr in _mod.GetTypeReferences())
            if (tr.Name == name && tr.Namespace == ns) return tr;
        // 也扫一遍签名里出现的类型引用
        foreach (var t in _mod.GetTypes())
        {
            if (t.BaseType != null && t.BaseType.Name == name && t.BaseType.Namespace == ns) return t.BaseType;
            foreach (var m in t.Methods)
                foreach (var p in m.Parameters)
                    if (p.ParameterType.Name == name && p.ParameterType.Namespace == ns) return p.ParameterType;
        }
        throw new Exception($"type reference not found: {ns}.{name}");
    }
    private static MethodReference MakeMethod(TypeReference declaring, string name,
        TypeReference returnType, params TypeReference[] ps)
    {
        var mr = new MethodReference(name, returnType, declaring) { HasThis = true };
        foreach (var p in ps) mr.Parameters.Add(new ParameterDefinition(p));
        return mr;
    }

    private static void AppendBeforeLastRet(MethodDefinition m, Action<ILProcessor> emit)
    {
        var il = m.Body.GetILProcessor();
        Instruction lastRet = null;
        for (int i = m.Body.Instructions.Count - 1; i >= 0; i--)
            if (m.Body.Instructions[i].OpCode == OpCodes.Ret) { lastRet = m.Body.Instructions[i]; break; }
        if (lastRet == null) throw new Exception("no ret in " + m.FullName);

        var pre = m.Body.Instructions.Count;
        emit(il);
        var added = new List<Instruction>();
        for (int i = pre; i < m.Body.Instructions.Count; i++) added.Add(m.Body.Instructions[i]);
        foreach (var ins in added) m.Body.Instructions.Remove(ins);
        foreach (var ins in added) il.InsertBefore(lastRet, ins);
        m.Body.OptimizeMacros();
    }

    private static void EmitSetVisibility(ILProcessor il, MethodReference getter, int vis, MethodReference setter)
    {
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Call, getter);
        il.Emit(OpCodes.Ldc_I4, vis); // 0=Visible 1=Hidden 2=Collapsed
        il.Emit(OpCodes.Callvirt, setter);
    }

    private static MethodReference SetVisibility()
        => MakeMethod(TypeRef("System.Windows", "UIElement"), "set_Visibility",
            _mod.TypeSystem.Void, TypeRef("System.Windows", "Visibility"));
    // ------------------------------------------------------- 1. Hooks type

    private static TypeDefinition CreateHookType()
    {
        var jGetItem = FindCallOn("JObject", "get_Item");          // JToken JObject::get_Item(object)
        var jObject = jGetItem.DeclaringType;                       // Newtonsoft.Json.Linq.JObject
        var jToken = jGetItem.ReturnType;                           // Newtonsoft.Json.Linq.JToken

        var jRemove = MakeMethod(jObject, "Remove", _mod.TypeSystem.Boolean, _mod.TypeSystem.String);
        var jToString = MakeMethod(jToken, "ToString", _mod.TypeSystem.String);
        var strEquals = MakeMethod(_mod.TypeSystem.String, "Equals", _mod.TypeSystem.Boolean, _mod.TypeSystem.String);

        var hooks = new TypeDefinition("PCLFix", "Hooks",
            TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.Class,
            _mod.TypeSystem.Object);
        _mod.Types.Add(hooks);

        var m = new MethodDefinition("NeutralizeIfUpdate",
            MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig,
            _mod.TypeSystem.Void);
        m.Parameters.Add(new ParameterDefinition("notice", ParameterAttributes.None, _mod.TypeSystem.Object));
        hooks.Methods.Add(m);

        var body = m.Body;
        body.InitLocals = true;
        var jo = new VariableDefinition(jObject);
        var tk = new VariableDefinition(jToken);
        body.Variables.Add(jo);
        body.Variables.Add(tk);

        var il = body.GetILProcessor();
        var l1 = Instruction.Create(OpCodes.Nop);
        var l2 = Instruction.Create(OpCodes.Nop);
        var l3 = Instruction.Create(OpCodes.Nop);

        // jo = notice as JObject; if (jo == null) return;
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Isinst, jObject);
        il.Emit(OpCodes.Stloc, jo);
        il.Emit(OpCodes.Ldloc, jo);
        il.Emit(OpCodes.Brtrue, l1);
        il.Emit(OpCodes.Ret);

        // tk = jo["isUpdate"]; if (tk == null) return;
        il.Append(l1);
        il.Emit(OpCodes.Ldloc, jo);
        il.Emit(OpCodes.Ldstr, "isUpdate");
        il.Emit(OpCodes.Callvirt, jGetItem);
        il.Emit(OpCodes.Stloc, tk);
        il.Emit(OpCodes.Ldloc, tk);
        il.Emit(OpCodes.Brtrue, l2);
        il.Emit(OpCodes.Ret);

        // if (!tk.ToString().Equals("True")) return;
        il.Append(l2);
        il.Emit(OpCodes.Ldloc, tk);
        il.Emit(OpCodes.Callvirt, jToString);
        il.Emit(OpCodes.Ldstr, "True");
        il.Emit(OpCodes.Callvirt, strEquals);
        il.Emit(OpCodes.Brtrue, l3);
        il.Emit(OpCodes.Ret);

        // jo.Remove("id");   // 让公告的 id 变成 0，被 “id <= 已读编号” 直接跳过
        il.Append(l3);
        il.Emit(OpCodes.Ldloc, jo);
        il.Emit(OpCodes.Ldstr, "id");
        il.Emit(OpCodes.Callvirt, jRemove);
        il.Emit(OpCodes.Pop);
        il.Emit(OpCodes.Ret);

        body.OptimizeMacros();
        Ok("inject PCLFix.Hooks::NeutralizeIfUpdate");
        return hooks;
    }

    // --------------------------------------------------- 2. notice popup

    private static void PatchNoticePopup(TypeDefinition hooks)
    {
        var secret = Type("PCL.ModSecret");
        var sub = Method(secret, "ServerSub");
        var hook = hooks.Methods.First(x => x.Name == "NeutralizeIfUpdate");

        var ins = sub.Body.Instructions;
        for (int i = 1; i + 1 < ins.Count; i++)
        {
            if (ins[i].OpCode != OpCodes.Ldstr || (string)ins[i].Operand! != "id") continue;
            if (ins[i + 1].OpCode != OpCodes.Callvirt) continue;
            if (!(ins[i + 1].Operand is MethodReference mr) || mr.Name != "get_Item") continue;
            if (ins[i - 1].OpCode != OpCodes.Ldloc && ins[i - 1].OpCode != OpCodes.Ldloc_S) continue;

            // 原序列：ldloc val ; ldstr "id" ; callvirt JObject::get_Item
            // 目标  ：ldloc val ; call Hooks ; ldloc val ; ldstr "id" ; callvirt get_Item
            var il = sub.Body.GetILProcessor();
            var local = (VariableDefinition)ins[i - 1].Operand!;
            var call = Instruction.Create(OpCodes.Call, hook);
            var ldloc2 = Instruction.Create(OpCodes.Ldloc, local);
            il.InsertAfter(ins[i - 1], call);
            il.InsertAfter(call, ldloc2);
            sub.Body.OptimizeMacros();
            Ok("ModSecret.ServerSub: skip isUpdate notices");
            return;
        }
        throw new Exception("notice loop pattern not found in ServerSub");
    }

    // ------------------------------------------------- 3. snapshot banner

    private static void PatchSnapshotBanner()
    {
        var page = Type("PCL.PageLaunchRight");
        var init = Method(page, "Init");
        var setVis = SetVisibility();

        AppendBeforeLastRet(init, il => EmitSetVisibility(il, PropGetter(page, "PanHint"), 2, setVis));
        Ok("PageLaunchRight.Init: hide snapshot banner (PanHint)");
    }

    // ---------------------------------------------------- 4. donate buttons

    private static void PatchDonateRow()
    {
        var page = Type("PCL.PageSetupSystem");
        var loaded = Method(page, "PageSetupSystem_Loaded");
        var getter = PropGetter(page, "PanDonate");

        var ins = loaded.Body.Instructions;
        for (int i = 0; i + 1 < ins.Count; i++)
        {
            if (ins[i].OpCode != OpCodes.Call && ins[i].OpCode != OpCodes.Callvirt) continue;
            if (!(ins[i].Operand is MethodReference mr) || mr.Name != "get_PanDonate") continue;

            var next = ins[i + 1];
            if (next.OpCode == OpCodes.Ldc_I4_0)      // Visible -> Collapsed
            {
                next.OpCode = OpCodes.Ldc_I4_2;
                next.Operand = null;
                Ok("PageSetupSystem_Loaded: PanDonate -> Collapsed (识别码/土豆码整行隐藏)");
                return;
            }
        }
        throw new Exception("PanDonate visibility assignment not found");
    }

    // -------------------------------------------------------- 5. window title

    private static void PatchWindowTitle()
    {
        var form = Type("PCL.FormMain");
        var ctor = Method(form, ".ctor");

        var window = form.BaseType ?? throw new Exception("FormMain base type missing");
        var setTitle = MakeMethod(window, "set_Title", _mod.TypeSystem.Void, _mod.TypeSystem.String);

        AppendBeforeLastRet(ctor, il =>
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldstr, "PCL Fix");
            il.Emit(OpCodes.Call, setTitle);
        });
        Ok("FormMain..ctor: window title = \"PCL Fix\"");
    }

    // --------------------------------------------------------- 6. title logo

    private static void PatchTitleLogo()
    {
        var form = Type("PCL.FormMain");
        var upd = Method(form, "UpdateBackgroundAndTitleBar", 0);
        var setVis = SetVisibility();
        var setText = MakeMethod(TypeRef("System.Windows.Controls", "TextBlock"), "set_Text", _mod.TypeSystem.Void, _mod.TypeSystem.String);

        var shape = PropGetter(form, "ShapeTitleLogo");
        var image = PropGetter(form, "ImageTitleLogo");
        var label = PropGetter(form, "LabTitleLogo");

        AppendBeforeLastRet(upd, il =>
        {
            EmitSetVisibility(il, shape, 2, setVis);
            EmitSetVisibility(il, image, 2, setVis);
            EmitSetVisibility(il, label, 0, setVis);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, label);
            il.Emit(OpCodes.Ldstr, "PCL Fix");
            il.Emit(OpCodes.Callvirt, setText);
        });
        Ok("FormMain.UpdateBackgroundAndTitleBar: logo -> text \"PCL Fix\"");
    }

    // -------------------------------------------------------- 7. about page

    private static void PatchAboutPage()
    {
        var page = Type("PCL.PageOtherAbout");
        var loaded = Method(page, "PageOtherAbout_Loaded");
        var item = Type("PCL.MyListItem");
        var setTitle = PropSetter(item, "Title");

        AppendBeforeLastRet(loaded, il =>
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, PropGetter(page, "ItemAboutPcl"));
            il.Emit(OpCodes.Ldstr, "PCL Fix");
            il.Emit(OpCodes.Callvirt, setTitle);
        });
        Ok("PageOtherAbout_Loaded: about page title = \"PCL Fix\"");
    }

    // --------------------------------------------------- 8. singleton window

    private static void PatchMutexWindowName(bool testMutex)
    {
        var app = Type("PCL.Application");
        var wait = Method(app, "WaitingMutex");
        string newName = testMutex ? "PCL Fix Test" : "PCL Fix";

        int hits = 0;
        foreach (var i in wait.Body.Instructions)
        {
            if (i.OpCode == OpCodes.Ldstr && (string)i.Operand! == "Plain Craft Launcher\u3000")
            {
                i.Operand = newName;
                hits++;
            }
        }
        if (hits == 0) throw new Exception("singleton window name literal not found");
        Ok($"Application.WaitingMutex: FindWindow title -> \"{newName}\"");

        if (testMutex)
        {
            foreach (var i in wait.Body.Instructions)
                if (i.OpCode == OpCodes.Ldstr && (string)i.Operand! == "PCL_SingletonMutex")
                    i.Operand = "PCLFix_TestMutex";
            Ok("Application.WaitingMutex: mutex name -> PCLFix_TestMutex (test build)");
        }
    }

    // ------------------------------------------------------ 9. assembly info

    private static void PatchAssemblyAttributes(AssemblyDefinition asm)
    {
        string[] targets = { "AssemblyTitleAttribute", "AssemblyProductAttribute", "AssemblyDescriptionAttribute" };
        int n = 0;
        foreach (var ca in asm.CustomAttributes)
        {
            if (!targets.Contains(ca.AttributeType.Name)) continue;
            if (ca.ConstructorArguments.Count != 1) continue;
            ca.ConstructorArguments[0] = new CustomAttributeArgument(_mod.TypeSystem.String, "PCL Fix");
            n++;
        }
        Ok($"assembly attributes renamed ({n}): Title/Product/Description = \"PCL Fix\"");
    }

    // ------------------------------------------------- 11. telemetry removal

    private static void PatchTelemetry()
    {
        var main = Type("PCL.ModMain");
        var tel = Method(main, "Telemetry");

        var body = tel.Body;
        body.Instructions.Clear();
        body.Variables.Clear();
        body.ExceptionHandlers.Clear();
        body.InitLocals = false;
        body.GetILProcessor().Emit(OpCodes.Ret);
        body.OptimizeMacros();
        Ok("ModMain.Telemetry: emptied (匿名数据上报功能已移除)");
    }

    private static void PatchTelemetryCheckbox()
    {
        var page = Type("PCL.PageSetupSystem");
        var loaded = Method(page, "PageSetupSystem_Loaded");
        var setVis = SetVisibility();
        AppendBeforeLastRet(loaded, il => EmitSetVisibility(il, PropGetter(page, "CheckSystemTelemetry"), 2, setVis));
        Ok("PageSetupSystem_Loaded: 匿名数据收集 checkbox hidden");
    }

    // --------------------------------------------- 12. update check message

    private static void PatchUpdateMessage()
    {
        const string NewMessage = "由于此版本的特殊性,暂时不提供在线更新功能";
        var secret = Type("PCL.ModSecret");
        var m = Method(secret, "UpdateCheckByButton");

        int hits = 0;
        foreach (var i in m.Body.Instructions)
        {
            if (i.OpCode != OpCodes.Ldstr) continue;
            var s = (string)i.Operand;
            if (s != null && s.StartsWith("在2026年10月20日", StringComparison.Ordinal))
            {
                i.Operand = NewMessage;
                hits++;
            }
        }
        if (hits == 0) throw new Exception("original update message not found");
        Ok("ModSecret.UpdateCheckByButton: message replaced");
    }
    // ------------------------------------------- 10. win32 version resource

    /// <summary>在 .rsrc 里把 UTF-16 的产品名就地改成等长（右侧补空格）的 “PCL Fix”。</summary>
    private static void PatchVersionResource(string path)
    {
        try
        {
            var bytes = File.ReadAllBytes(path);
            int peOff = BitConverter.ToInt32(bytes, 0x3C);
            int numSections = BitConverter.ToUInt16(bytes, peOff + 6);
            int optSize = BitConverter.ToUInt16(bytes, peOff + 20);
            int secTable = peOff + 24 + optSize;

            (int start, int len) rsrc = (-1, 0);
            for (int s = 0; s < numSections; s++)
            {
                int o = secTable + s * 40;
                string name = System.Text.Encoding.ASCII.GetString(bytes, o, 8).TrimEnd('\0');
                if (name != ".rsrc") continue;
                int rawSize = BitConverter.ToInt32(bytes, o + 16);
                int rawPtr = BitConverter.ToInt32(bytes, o + 20);
                rsrc = (rawPtr, rawSize);
            }
            if (rsrc.start < 0) { Console.WriteLine("  [--] .rsrc not found, skip version info"); return; }

            // 从长到短替换，避免短串先吃掉长串
            string[] olds = {
                "Plain Craft Launcher 2.exe",
                "Plain Craft Launcher \u542F\u52A8\u5668",
                "Plain Craft Launcher 2",
                "Plain Craft Launcher"
            };
            int replaced = 0;
            foreach (var oldText in olds)
            {
                var oldBytes = System.Text.Encoding.Unicode.GetBytes(oldText);
                var newBytes = System.Text.Encoding.Unicode.GetBytes("PCL Fix".PadRight(oldText.Length));
                for (int p = rsrc.start; p <= rsrc.start + rsrc.len - oldBytes.Length; p++)
                {
                    bool match = true;
                    for (int k = 0; k < oldBytes.Length; k++)
                        if (bytes[p + k] != oldBytes[k]) { match = false; break; }
                    if (!match) continue;
                    Array.Copy(newBytes, 0, bytes, p, newBytes.Length);
                    replaced++;
                    p += oldBytes.Length - 1;
                }
            }
            if (replaced > 0)
            {
                File.WriteAllBytes(path, bytes);
                Ok($"win32 version strings renamed ({replaced})");
            }
            else Console.WriteLine("  [--] no version string match in .rsrc");
        }
        catch (Exception ex)
        {
            Console.WriteLine("  [--] version resource patch skipped: " + ex.Message);
        }
    }
}






