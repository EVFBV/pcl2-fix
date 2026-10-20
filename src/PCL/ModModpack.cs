using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using MeloongCore;
using MeloongCore.Extensions;
using MeloongCore.Wpf;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCLCS;

namespace PCL;

[StandardModule]
public sealed class ModModpack
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__10_002D0
	{
		public string _0024VB_0024Local_TargetFolder;

		public string _0024VB_0024Local_FileAddress;

		public string _0024VB_0024Local_ArchiveBaseFolder;

		public _Closure_0024__10_002D0(_Closure_0024__10_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_TargetFolder = arg0._0024VB_0024Local_TargetFolder;
				_0024VB_0024Local_FileAddress = arg0._0024VB_0024Local_FileAddress;
				_0024VB_0024Local_ArchiveBaseFolder = arg0._0024VB_0024Local_ArchiveBaseFolder;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(ModLoader.LoaderTask<string, int> Task)
		{
			ExtractModpackFiles(_0024VB_0024Local_TargetFolder, _0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.9));
			ModBase.OpenExplorer(_0024VB_0024Local_TargetFolder);
			Thread.Sleep(400);
			string text = null;
			foreach (string item in DirectoryUtils.EnumerateFiles(_0024VB_0024Local_TargetFolder, false, "*.exe"))
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(item);
				Logger.Info($"文件 {item} 的产品名标识为 {versionInfo.ProductName}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				if (Operators.CompareString(versionInfo.ProductName, "Plain Craft Launcher", TextCompare: false) == 0)
				{
					text = item;
					Logger.Info($"发现整合包附带的 PCL 启动器：{item}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
				else if ((StringExtensions.ContainsIgnoreCase(versionInfo.ProductName, "Launcher") || versionInfo.ProductName.Contains("启动")) && Operators.CompareString(versionInfo.ProductName, "Plain Craft Launcher Admin Manager", TextCompare: false) != 0 && text == null)
				{
					text = item;
					Logger.Info($"发现整合包附带的疑似第三方启动器：{item}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
			}
			Task.Progress = 0.95;
			if (text != null)
			{
				Logger.Info($"找到压缩包中附带的启动器：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				if (ModMain.MyMsgBox(string.Format("整合包里似乎自带了启动器，是否换用它继续安装？{0}即将打开：{1}", "\r\n", text), "换用整合包启动器？", "换用", "不换用") == 1)
				{
					ModBase.StartProcess(text, "--wait");
					Logger.Info("为换用整合包中的启动器启动，强制结束程序", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					ModMain.FrmMain.EndProgram(SendWarning: false);
					return;
				}
			}
			else
			{
				Logger.Info("未找到压缩包中附带的启动器", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			}
			string lastPart = PathUtils.GetLastPart(_0024VB_0024Local_TargetFolder);
			DirectoryUtils.Create(_0024VB_0024Local_TargetFolder + ".minecraft\\");
			PageSelectLeft.AddFolder(_0024VB_0024Local_TargetFolder + ".minecraft\\" + _0024VB_0024Local_ArchiveBaseFolder.Replace("/", "\\").TrimStart('\\'), lastPart, ShowHint: false);
			string text2 = DirectoryUtils.EnumerateFiles(_0024VB_0024Local_TargetFolder, true, "modpack.*").First();
			Logger.Info($"调用 modpack 文件继续安装：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			ModpackInstall(text2);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__11_002D0
	{
		public string _0024VB_0024Local_TargetFolder;

		public string _0024VB_0024Local_FileAddress;

		public string _0024VB_0024Local_ArchiveBaseFolder;

		public _Closure_0024__11_002D0(_Closure_0024__11_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_TargetFolder = arg0._0024VB_0024Local_TargetFolder;
				_0024VB_0024Local_FileAddress = arg0._0024VB_0024Local_FileAddress;
				_0024VB_0024Local_ArchiveBaseFolder = arg0._0024VB_0024Local_ArchiveBaseFolder;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(ModLoader.LoaderTask<string, int> Task)
		{
			ExtractModpackFiles(_0024VB_0024Local_TargetFolder, _0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.95));
			PageSelectLeft.AddFolder(_0024VB_0024Local_TargetFolder + _0024VB_0024Local_ArchiveBaseFolder, PathUtils.GetLastPart(_0024VB_0024Local_TargetFolder), ShowHint: false);
			Thread.Sleep(400);
			ModBase.RunInUi([SpecialName] () =>
			{
				ModMain.FrmMain.PageChange(FormMain.PageType.InstanceSelect);
			});
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__5_002D0
	{
		public string _0024VB_0024Local_InstallTemp;

		public string _0024VB_0024Local_FileAddress;

		public string _0024VB_0024Local_VersionFolder;

		public List<int> _0024VB_0024Local_ModList;

		public JObject _0024VB_0024Local_Json;

		public List<int> _0024VB_0024Local_ModOptionalList;

		public McVersion _0024VB_0024Local_TargetVersion;

		public string _0024VB_0024Local_Logo;

		public string _0024VB_0024Local_LoaderName;

		public Func<int, (int ProjectId, int FileId)> _0024I3;

		public _Closure_0024__5_002D0(_Closure_0024__5_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_InstallTemp = arg0._0024VB_0024Local_InstallTemp;
				_0024VB_0024Local_FileAddress = arg0._0024VB_0024Local_FileAddress;
				_0024VB_0024Local_VersionFolder = arg0._0024VB_0024Local_VersionFolder;
				_0024VB_0024Local_ModList = arg0._0024VB_0024Local_ModList;
				_0024VB_0024Local_Json = arg0._0024VB_0024Local_Json;
				_0024VB_0024Local_ModOptionalList = arg0._0024VB_0024Local_ModOptionalList;
				_0024VB_0024Local_TargetVersion = arg0._0024VB_0024Local_TargetVersion;
				_0024VB_0024Local_Logo = arg0._0024VB_0024Local_Logo;
				_0024VB_0024Local_LoaderName = arg0._0024VB_0024Local_LoaderName;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__1(ModLoader.LoaderTask<int, JArray> Task)
		{
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			Task.Output = (JArray)ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/files", HttpMethod.Post, "{\"fileIds\": [" + EnumerableExtensions.Join((IEnumerable)_0024VB_0024Local_ModList, ",") + "]}", "application/json")[(object)"data"];
			if (_0024VB_0024Local_ModList.Count == ((JContainer)Task.Output).Count)
			{
				return;
			}
			List<int> list = _0024VB_0024Local_ModList.Except(((IEnumerable<JToken>)Task.Output).Select([SpecialName] (JToken j) => j[(object)"id"].ToObject<int>())).ToList();
			Logger.Info(string.Format("缺失的 fileIds：{0}", EnumerableExtensions.Join((IEnumerable)list, "、")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			List<(int, int)> list2 = list.Select([SpecialName] (int id) =>
			{
				//IL_0034: Unknown result type (might be due to invalid IL or missing references)
				_Closure_0024__5_002D2 arg = default(_Closure_0024__5_002D2);
				_Closure_0024__5_002D2 CS_0024_003C_003E8__locals0 = new _Closure_0024__5_002D2(arg)
				{
					_0024VB_0024Local_id = id
				};
				return (((JObject)((IEnumerable<JToken>)_0024VB_0024Local_Json["files"]).AsEnumerable().First([SpecialName] (JToken f) => f[(object)"fileID"].ToObject<int>() == CS_0024_003C_003E8__locals0._0024VB_0024Local_id))["projectID"].ToObject<int>(), CS_0024_003C_003E8__locals0._0024VB_0024Local_id);
			}).ToList();
			Logger.Info(string.Format("缺失项：{0}", EnumerableExtensions.Join((IEnumerable)list2, "、")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			List<ResourceProject> source = new List<ResourceProject>();
			try
			{
				source = ResourceProject.FromProjectIds(list2.Select<(int, int), string>([SpecialName] ((int ProjectId, int FileId) e) => e.ProjectId.ToString()), (ResourcePlatforms)1).ToList();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "获取缺失的工程信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				ProjectData.ClearProjectError();
			}
			List<string> list3 = new List<string>();
			using (List<(int, int)>.Enumerator enumerator = list2.GetEnumerator())
			{
				_Closure_0024__5_002D3 closure_0024__5_002D = default(_Closure_0024__5_002D3);
				while (enumerator.MoveNext())
				{
					closure_0024__5_002D = new _Closure_0024__5_002D3(closure_0024__5_002D)
					{
						_0024VB_0024Local_Missing = enumerator.Current
					};
					ResourceProject resourceProject = source.FirstOrDefault(closure_0024__5_002D._Lambda_0024__6);
					if ((object)resourceProject == null)
					{
						list3.Add($"未知 CurseForge 项目 (项目 ID {closure_0024__5_002D._0024VB_0024Local_Missing.ProjectId}，文件 ID {closure_0024__5_002D._0024VB_0024Local_Missing.FileId})");
					}
					else
					{
						list3.Add($"{resourceProject.TranslatedName} (文件 ID {closure_0024__5_002D._0024VB_0024Local_Missing.FileId})");
					}
				}
			}
			if (ModMain.MyMsgBox(string.Format("未找到该整合包所需的以下内容，它们可能已被原作者删除：{0}- {1}{2}{3}是否继续安装？缺失这些内容可能导致整合包出现异常！", "\r\n", EnumerableExtensions.Join((IEnumerable)list3, "\r\n- "), "\r\n", "\r\n"), "整合包内容缺失", "继续", "取消", "", IsWarn: true) == 2)
			{
				Task.Cancel();
			}
		}

		[SpecialName]
		internal (int, int) _Lambda_0024__3(int id)
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			_Closure_0024__5_002D2 arg = default(_Closure_0024__5_002D2);
			_Closure_0024__5_002D2 CS_0024_003C_003E8__locals0 = new _Closure_0024__5_002D2(arg)
			{
				_0024VB_0024Local_id = id
			};
			return (((JObject)((IEnumerable<JToken>)_0024VB_0024Local_Json["files"]).AsEnumerable().First([SpecialName] (JToken f) => f[(object)"fileID"].ToObject<int>() == CS_0024_003C_003E8__locals0._0024VB_0024Local_id))["projectID"].ToObject<int>(), CS_0024_003C_003E8__locals0._0024VB_0024Local_id);
		}

		[SpecialName]
		internal void _Lambda_0024__7(ModLoader.LoaderTask<JArray, List<ModNet.NetFile>> Task)
		{
			//IL_018b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_018e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_019a: Expected O, but got Unknown
			//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00db: Expected O, but got Unknown
			//IL_013e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_017f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			Dictionary<int, ModNet.NetFile> dictionary = new Dictionary<int, ModNet.NetFile>();
			foreach (JToken item in Task.Input)
			{
				int num = item[(object)"id"].ToObject<int>();
				if (dictionary.ContainsKey(num) || (_0024VB_0024Local_ModOptionalList.Contains(num) && ModMain.MyMsgBox("是否要下载整合包中的可选文件 " + item[(object)"displayName"].ToString() + "？", "下载可选文件", "是", "否") == 2))
				{
					continue;
				}
				string path;
				ResourceTypes expectedResourceType;
				if (((IEnumerable<JToken>)item[(object)"modules"]).Any())
				{
					List<string> list = ((IEnumerable<JToken>)(JArray)item[(object)"modules"]).Select([SpecialName] (JToken l) => l[(object)"name"].ToString()).ToList();
					if (list.Contains("META-INF") || list.Contains("mcmod.info") || list.Contains("fabric.mod.json"))
					{
						goto IL_0136;
					}
					if (item != null)
					{
						JToken obj = item[(object)"FileName"];
						if (obj != null && StringExtensions.EndsWithF(obj.ToString(), ".jar", true))
						{
							goto IL_0136;
						}
					}
					if (list.Contains("pack.mcmeta"))
					{
						path = "resourcepacks";
						expectedResourceType = (ResourceTypes)4;
					}
					else if (list.Contains("shaders"))
					{
						path = "shaderpacks";
						expectedResourceType = (ResourceTypes)8;
					}
					else
					{
						path = "saves";
						expectedResourceType = (ResourceTypes)64;
					}
				}
				else
				{
					path = "mods";
					expectedResourceType = (ResourceTypes)1;
				}
				goto IL_018d;
				IL_0136:
				path = "mods";
				expectedResourceType = (ResourceTypes)1;
				goto IL_018d;
				IL_018d:
				ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)item, expectedResourceType);
				if (resourceVersion.DownloadAvailable)
				{
					dictionary.Add(num, resourceVersion.ToNetFile(PathUtils.AddSlashSuffix(Path.Combine(_0024VB_0024Local_VersionFolder, path)), ResourceVersion.DownloadReason.ModPack, _0024VB_0024Local_TargetVersion.VanillaName, _0024VB_0024Local_TargetVersion.ModLoaders));
					Task.Progress += 1.0 / (double)checked(1 + _0024VB_0024Local_ModList.Count);
				}
			}
			Task.Output = dictionary.Values.ToList();
		}

		[SpecialName]
		internal void _Lambda_0024__12(ModLoader.LoaderTask<string, string> Task)
		{
			if (_0024VB_0024Local_Logo != null && FileUtils.Exists(_0024VB_0024Local_Logo))
			{
				FileUtils.Copy(_0024VB_0024Local_Logo, _0024VB_0024Local_VersionFolder + "PCL\\Logo.png");
				ModBase.WriteIni(_0024VB_0024Local_VersionFolder + "PCL\\Setup.ini", "Logo", "PCL\\Logo.png");
				ModBase.WriteIni(_0024VB_0024Local_VersionFolder + "PCL\\Setup.ini", "LogoCustom", "True");
				Logger.Info($"已设置整合包 Logo：{_0024VB_0024Local_Logo}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			}
			string text = $"{_0024VB_0024Local_VersionFolder}saves\\";
			foreach (string item in DirectoryUtils.EnumerateFiles(text, false, "*.zip"))
			{
				FileUtils.ExtractToDirectory(item, text, Task.CreateCancellationToken(), (ProgressProvider)null);
				FileUtils.Delete(item, false);
			}
			string[] array = new string[2]
			{
				_0024VB_0024Local_VersionFolder + "原始整合包.zip",
				_0024VB_0024Local_VersionFolder + "原始整合包.mrpack"
			};
			foreach (string text2 in array)
			{
				if (FileUtils.Exists(text2))
				{
					Logger.Info($"删除原始整合包文件：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					FileUtils.Delete(text2, false);
				}
			}
			if (FileUtils.Exists(_0024VB_0024Local_FileAddress) && Operators.CompareString(PathUtils.GetFileNameWithoutExtension(_0024VB_0024Local_FileAddress), "modpack", TextCompare: false) == 0)
			{
				Logger.Info($"删除安装整合包文件：{_0024VB_0024Local_FileAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				FileUtils.Delete(_0024VB_0024Local_FileAddress, false);
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__13(ModLoader.LoaderBase l)
		{
			return Operators.CompareString(l.Name, _0024VB_0024Local_LoaderName, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__5_002D1
	{
		public string _0024VB_0024Local_OverridesFolder;

		public _Closure_0024__5_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__5_002D1(_Closure_0024__5_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_OverridesFolder = arg0._0024VB_0024Local_OverridesFolder;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(ModLoader.LoaderTask<string, int> Task)
		{
			ExtractModpackFiles(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_InstallTemp, _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.6));
			CopyOverrideDirectory(_0024VB_0024Local_OverridesFolder, _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_VersionFolder, Task);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__5_002D2
	{
		public int _0024VB_0024Local_id;

		public _Closure_0024__5_002D2(_Closure_0024__5_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_id = arg0._0024VB_0024Local_id;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__4(JToken f)
		{
			return f[(object)"fileID"].ToObject<int>() == _0024VB_0024Local_id;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__5_002D3
	{
		public (int ProjectId, int FileId) _0024VB_0024Local_Missing;

		public _Closure_0024__5_002D3(_Closure_0024__5_002D3 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Missing = arg0._0024VB_0024Local_Missing;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__6(ResourceProject p)
		{
			return Conversions.ToDouble(p.Id) == (double)_0024VB_0024Local_Missing.ProjectId;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D0
	{
		public string _0024VB_0024Local_InstallTemp;

		public string _0024VB_0024Local_FileAddress;

		public string _0024VB_0024Local_ArchiveBaseFolder;

		public string _0024VB_0024Local_VersionFolder;

		public string _0024VB_0024Local_Logo;

		public string _0024VB_0024Local_LoaderName;

		public _Closure_0024__6_002D0(_Closure_0024__6_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_InstallTemp = arg0._0024VB_0024Local_InstallTemp;
				_0024VB_0024Local_FileAddress = arg0._0024VB_0024Local_FileAddress;
				_0024VB_0024Local_ArchiveBaseFolder = arg0._0024VB_0024Local_ArchiveBaseFolder;
				_0024VB_0024Local_VersionFolder = arg0._0024VB_0024Local_VersionFolder;
				_0024VB_0024Local_Logo = arg0._0024VB_0024Local_Logo;
				_0024VB_0024Local_LoaderName = arg0._0024VB_0024Local_LoaderName;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(ModLoader.LoaderTask<string, int> Task)
		{
			ExtractModpackFiles(_0024VB_0024Local_InstallTemp, _0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.5));
			CopyOverrideDirectory(_0024VB_0024Local_InstallTemp + _0024VB_0024Local_ArchiveBaseFolder + "overrides", _0024VB_0024Local_VersionFolder, Task);
			CopyOverrideDirectory(_0024VB_0024Local_InstallTemp + _0024VB_0024Local_ArchiveBaseFolder + "client-overrides", _0024VB_0024Local_VersionFolder, Task);
		}

		[SpecialName]
		internal void _Lambda_0024__5(ModLoader.LoaderTask<string, string> Task)
		{
			if (_0024VB_0024Local_Logo != null && FileUtils.Exists(_0024VB_0024Local_Logo))
			{
				FileUtils.Copy(_0024VB_0024Local_Logo, _0024VB_0024Local_VersionFolder + "PCL\\Logo.png");
				ModBase.WriteIni(_0024VB_0024Local_VersionFolder + "PCL\\Setup.ini", "Logo", "PCL\\Logo.png");
				ModBase.WriteIni(_0024VB_0024Local_VersionFolder + "PCL\\Setup.ini", "LogoCustom", "True");
				Logger.Info($"已设置整合包 Logo：{_0024VB_0024Local_Logo}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			}
			string[] array = new string[2]
			{
				_0024VB_0024Local_VersionFolder + "原始整合包.zip",
				_0024VB_0024Local_VersionFolder + "原始整合包.mrpack"
			};
			foreach (string text in array)
			{
				if (FileUtils.Exists(text))
				{
					Logger.Info($"删除原始整合包文件：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					FileUtils.Delete(text, false);
				}
			}
			if (FileUtils.Exists(_0024VB_0024Local_FileAddress) && Operators.CompareString(PathUtils.GetFileNameWithoutExtension(_0024VB_0024Local_FileAddress), "modpack", TextCompare: false) == 0)
			{
				Logger.Info($"删除安装整合包文件：{_0024VB_0024Local_FileAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				FileUtils.Delete(_0024VB_0024Local_FileAddress, false);
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__6(ModLoader.LoaderBase l)
		{
			return Operators.CompareString(l.Name, _0024VB_0024Local_LoaderName, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__8_002D0
	{
		public string _0024VB_0024Local_InstallTemp;

		public string _0024VB_0024Local_FileAddress;

		public string _0024VB_0024Local_ArchiveBaseFolder;

		public string _0024VB_0024Local_VersionFolder;

		public string _0024VB_0024Local_SetupFile;

		public string _0024VB_0024Local_LoaderName;

		public _Closure_0024__8_002D0(_Closure_0024__8_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_InstallTemp = arg0._0024VB_0024Local_InstallTemp;
				_0024VB_0024Local_FileAddress = arg0._0024VB_0024Local_FileAddress;
				_0024VB_0024Local_ArchiveBaseFolder = arg0._0024VB_0024Local_ArchiveBaseFolder;
				_0024VB_0024Local_VersionFolder = arg0._0024VB_0024Local_VersionFolder;
				_0024VB_0024Local_SetupFile = arg0._0024VB_0024Local_SetupFile;
				_0024VB_0024Local_LoaderName = arg0._0024VB_0024Local_LoaderName;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(ModLoader.LoaderTask<string, int> Task)
		{
			ExtractModpackFiles(_0024VB_0024Local_InstallTemp, _0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.55));
			string overridesFolder = ((!DirectoryUtils.Exists(_0024VB_0024Local_InstallTemp + _0024VB_0024Local_ArchiveBaseFolder + ".minecraft") || DirectoryUtils.Exists(_0024VB_0024Local_InstallTemp + _0024VB_0024Local_ArchiveBaseFolder + "minecraft")) ? (_0024VB_0024Local_InstallTemp + _0024VB_0024Local_ArchiveBaseFolder + "minecraft") : (_0024VB_0024Local_InstallTemp + _0024VB_0024Local_ArchiveBaseFolder + ".minecraft"));
			CopyOverrideDirectory(overridesFolder, _0024VB_0024Local_VersionFolder, Task);
			try
			{
				string text = _0024VB_0024Local_InstallTemp + _0024VB_0024Local_ArchiveBaseFolder + "instance.cfg";
				if (!FileUtils.Exists(text))
				{
					return;
				}
				List<string> list = new List<string>();
				string[] array = FileUtils.ReadAsLines(text, true, (Encoding)null, (Type)null);
				foreach (string text2 in array)
				{
					if (text2.Contains("="))
					{
						list.Add(StringExtensions.BeforeFirst(text2, "=", false) + ":" + StringExtensions.AfterFirst(text2, "=", false));
					}
				}
				FileUtils.Write(text, EnumerableExtensions.Join((IEnumerable)list, "\r\n"), (Encoding)null);
				if (Conversions.ToBoolean(ModBase.ReadIni(text, "OverrideCommands", Conversions.ToString(Value: false))))
				{
					string text3 = ModBase.ReadIni(text, "PreLaunchCommand");
					if (Operators.CompareString(text3, "", TextCompare: false) != 0)
					{
						text3 = text3.Replace("\\\"", "\"").Replace("$INST_JAVA", "{java}java.exe").Replace("$INST_MC_DIR\\", "{minecraft}")
							.Replace("$INST_MC_DIR", "{minecraft}")
							.Replace("$INST_DIR\\", "{verpath}")
							.Replace("$INST_DIR", "{verpath}")
							.Replace("$INST_ID", "{name}")
							.Replace("$INST_NAME", "{name}");
						ModBase.WriteIni(_0024VB_0024Local_SetupFile, "VersionAdvanceRun", text3);
						Logger.Info($"迁移 MultiMC 版本独立设置：启动前执行命令：{text3}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
				}
				if (Conversions.ToBoolean(ModBase.ReadIni(text, "JoinServerOnLaunch", Conversions.ToString(Value: false))))
				{
					string text4 = ModBase.ReadIni(text, "JoinServerOnLaunchAddress").Replace("\\\"", "\"");
					ModBase.WriteIni(_0024VB_0024Local_SetupFile, "VersionServerEnter", text4);
					Logger.Info($"迁移 MultiMC 版本独立设置：自动进入服务器：{text4}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
				string text5 = ModBase.ReadIni(text, "iconKey");
				if (Operators.CompareString(text5, "", TextCompare: false) != 0 && FileUtils.Exists($"{_0024VB_0024Local_InstallTemp}{_0024VB_0024Local_ArchiveBaseFolder}{text5}.png"))
				{
					ModBase.WriteIni(_0024VB_0024Local_SetupFile, "LogoCustom", Conversions.ToString(Value: true));
					ModBase.WriteIni(_0024VB_0024Local_SetupFile, "Logo", "PCL\\Logo.png");
					FileUtils.Copy($"{_0024VB_0024Local_InstallTemp}{_0024VB_0024Local_ArchiveBaseFolder}{text5}.png", _0024VB_0024Local_VersionFolder + "PCL\\Logo.png");
					Logger.Info($"迁移 MultiMC 版本独立设置：版本图标（{text5}.png）", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
				string text6 = ModBase.ReadIni(text, "JvmArgs");
				if (Operators.CompareString(text6, "", TextCompare: false) != 0)
				{
					if (Conversions.ToBoolean(ModBase.ReadIni(text, "OverrideJavaArgs", Conversions.ToString(Value: false))))
					{
						ModBase.WriteIni(_0024VB_0024Local_SetupFile, "VersionAdvanceJvm", text6);
						Logger.Info($"迁移 MultiMC 版本独立设置：JVM 参数（覆盖）：{text6}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
					else
					{
						text6 = text6 + " " + Settings.Get<string>("LaunchAdvanceJvm");
						ModBase.WriteIni(_0024VB_0024Local_SetupFile, "VersionAdvanceJvm", text6);
						Logger.Info($"迁移 MultiMC 版本独立设置：JVM 参数（追加）：{text6}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"读取 MMC 配置文件失败（{_0024VB_0024Local_InstallTemp}{_0024VB_0024Local_ArchiveBaseFolder}instance.cfg）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				ProjectData.ClearProjectError();
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__3(ModLoader.LoaderBase l)
		{
			return Operators.CompareString(l.Name, _0024VB_0024Local_LoaderName, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__9_002D0
	{
		public string _0024VB_0024Local_InstallTemp;

		public string _0024VB_0024Local_FileAddress;

		public string _0024VB_0024Local_ArchiveBaseFolder;

		public string _0024VB_0024Local_VersionFolder;

		public JObject _0024VB_0024Local_Json;

		public string _0024VB_0024Local_SetupFile;

		public string _0024VB_0024Local_LoaderName;

		public _Closure_0024__9_002D0(_Closure_0024__9_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_InstallTemp = arg0._0024VB_0024Local_InstallTemp;
				_0024VB_0024Local_FileAddress = arg0._0024VB_0024Local_FileAddress;
				_0024VB_0024Local_ArchiveBaseFolder = arg0._0024VB_0024Local_ArchiveBaseFolder;
				_0024VB_0024Local_VersionFolder = arg0._0024VB_0024Local_VersionFolder;
				_0024VB_0024Local_Json = arg0._0024VB_0024Local_Json;
				_0024VB_0024Local_SetupFile = arg0._0024VB_0024Local_SetupFile;
				_0024VB_0024Local_LoaderName = arg0._0024VB_0024Local_LoaderName;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(ModLoader.LoaderTask<string, int> Task)
		{
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			ExtractModpackFiles(_0024VB_0024Local_InstallTemp, _0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.6));
			CopyOverrideDirectory(_0024VB_0024Local_InstallTemp + _0024VB_0024Local_ArchiveBaseFolder + "overrides", _0024VB_0024Local_VersionFolder, Task);
			if (_0024VB_0024Local_Json["launchInfo"] != null)
			{
				JObject val = (JObject)_0024VB_0024Local_Json["launchInfo"];
				if (val.ContainsKey("javaArgument"))
				{
					ModBase.WriteIni(_0024VB_0024Local_SetupFile, "VersionAdvanceJvm", string.Join(" ", (IEnumerable<JToken>)val["javaArgument"]));
				}
				if (val.ContainsKey("launchArgument"))
				{
					ModBase.WriteIni(_0024VB_0024Local_SetupFile, "VersionAdvanceGame", string.Join(" ", (IEnumerable<JToken>)val["launchArgument"]));
				}
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__4(ModLoader.LoaderBase l)
		{
			return Operators.CompareString(l.Name, _0024VB_0024Local_LoaderName, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__9_002D1
	{
		public string _0024VB_0024Local_MinecraftVersion;

		public string _0024VB_0024Local_OptiFineFileSuffix;

		public _Closure_0024__9_002D1(_Closure_0024__9_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_MinecraftVersion = arg0._0024VB_0024Local_MinecraftVersion;
				_0024VB_0024Local_OptiFineFileSuffix = arg0._0024VB_0024Local_OptiFineFileSuffix;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__1(ModDownload.DlOptiFineListEntry v)
		{
			if (Operators.CompareString(v.Inherit, _0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0)
			{
				return StringExtensions.EndsWithF(v.FileName, _0024VB_0024Local_OptiFineFileSuffix, false);
			}
			return false;
		}
	}

	public static void ModpackInstall()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		string text = Dialogs.SelectFile("选择整合包压缩文件", false, (string)null, (OneOf<IEnumerable<ValueTuple<string[], string>>, string>?)OneOf<IEnumerable<(string[], string)>, string>.op_Implicit((IEnumerable<(string[], string)>)new(string[], string)[1] { (new string[3] { "rar", "zip", "mrpack" }, "整合包文件") })).FirstOrDefault();
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		ModBase.RunInThread([SpecialName] () =>
		{
			try
			{
				ModpackInstall(text);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ExceptionExtensions.ThrowIfCanceled(ex2);
				Logger.Error(ex2, "手动安装整合包失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				ProjectData.ClearProjectError();
			}
		});
	}

	public static ModLoader.LoaderCombo<string> ModpackInstall(string File, string InstanceName = null, string Logo = null)
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		Logger.Info(string.Format("整合包安装请求：{0}", File ?? "null"), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
		ZipArchive val = null;
		string archiveBaseFolder = "";
		try
		{
			string text = $"{ModMinecraft.McFolderSelected}versions\\{InstanceName}\\";
			if (text.Contains("!") || text.Contains(";"))
			{
				ModMain.Hint("游戏路径中不能含有感叹号或分号：" + text, ModMain.HintType.Red);
				throw new OperationCanceledException();
			}
			int num = -1;
			try
			{
				val = FileUtils.OpenZip(File, default(CancellationToken));
				if (val.GetEntry("mcbbs.packmeta") != null)
				{
					num = 3;
				}
				else if (val.GetEntry("mmc-pack.json") != null)
				{
					num = 2;
				}
				else if (val.GetEntry("modrinth.index.json") != null)
				{
					num = 4;
				}
				else if (val.GetEntry("manifest.json") != null)
				{
					num = ((((JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(val.GetEntry("manifest.json").Open(), Encoding.UTF8)))["addons"] != null) ? 3 : 0);
				}
				else if (val.GetEntry("modpack.json") != null)
				{
					num = 1;
				}
				else if (val.GetEntry("modpack.zip") != null || val.GetEntry("modpack.mrpack") != null)
				{
					num = 9;
				}
				else
				{
					foreach (ZipArchiveEntry entry in val.Entries)
					{
						string[] array = StringExtensions.Split(entry.FullName, "/", false);
						archiveBaseFolder = array[0] + "/";
						if (array.Count() != 2)
						{
							continue;
						}
						if (Operators.CompareString(array[1], "mcbbs.packmeta", TextCompare: false) == 0)
						{
							num = 3;
							break;
						}
						if (Operators.CompareString(array[1], "mmc-pack.json", TextCompare: false) == 0)
						{
							num = 2;
							break;
						}
						if (Operators.CompareString(array[1], "modrinth.index.json", TextCompare: false) == 0)
						{
							num = 4;
							break;
						}
						if (Operators.CompareString(array[1], "manifest.json", TextCompare: false) == 0)
						{
							if (((JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(entry.Open(), Encoding.UTF8)))["addons"] == null)
							{
								num = 0;
								break;
							}
							num = 3;
							archiveBaseFolder = "overrides/";
							break;
						}
						if (Operators.CompareString(array[1], "modpack.json", TextCompare: false) == 0)
						{
							num = 1;
							break;
						}
						if (Operators.CompareString(array[1], "modpack.zip", TextCompare: false) == 0 || Operators.CompareString(array[1], "modpack.mrpack", TextCompare: false) == 0)
						{
							num = 9;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				if (ExceptionExtensions.GetDisplay(ex2, true).Contains("Error.WinIOError"))
				{
					throw new Exception("打开整合包文件失败", ex2);
				}
				if (Operators.CompareString(PathUtils.GetExtension(File), "rar", TextCompare: false) == 0)
				{
					throw new Exception("PCL 无法处理 rar 格式的压缩包，请在解压后重新压缩为 zip 格式再试", ex2);
				}
				throw new Exception("打开整合包文件失败，文件可能损坏或为不支持的压缩包格式", ex2);
			}
			switch (num)
			{
			case 0:
				Logger.Info("整合包种类：CurseForge", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				return InstallPackCurseForge(File, val, archiveBaseFolder, InstanceName, Logo);
			case 1:
				Logger.Info("整合包种类：HMCL", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				return InstallPackHMCL(File, val, archiveBaseFolder);
			case 2:
				Logger.Info("整合包种类：MMC", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				return InstallPackMMC(File, val, archiveBaseFolder);
			case 3:
				Logger.Info("整合包种类：MCBBS", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				return InstallPackMCBBS(File, val, archiveBaseFolder, InstanceName);
			case 4:
				Logger.Info("整合包种类：Modrinth", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				return InstallPackModrinth(File, val, archiveBaseFolder, InstanceName, Logo);
			case 9:
				Logger.Info("整合包种类：带启动器的压缩包", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				return InstallPackLauncherPack(File, val, archiveBaseFolder);
			default:
				Logger.Info("整合包种类：未能识别，假定为压缩包", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				return InstallPackCompress(File, val);
			}
		}
		finally
		{
			if (val != null)
			{
				val.Dispose();
			}
		}
	}

	private static void ExtractModpackFiles(string InstallTemp, string FileAddress, CancellationToken c = default(CancellationToken), ProgressProvider p = null)
	{
		DirectoryUtils.Delete(InstallTemp, false);
		c.ThrowIfCancellationRequested();
		FileUtils.ExtractToDirectory(FileAddress, InstallTemp, c, p);
	}

	private static void CopyOverrideDirectory(string OverridesFolder, string VersionFolder, ModLoader.LoaderBase Loader)
	{
		OverridesFolder = PathUtils.AddSlashSuffix(OverridesFolder);
		VersionFolder = PathUtils.AddSlashSuffix(VersionFolder);
		if (DirectoryUtils.Exists(OverridesFolder))
		{
			Logger.Info($"处理整合包覆写文件夹：{OverridesFolder} → {VersionFolder}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			DirectoryUtils.Copy(OverridesFolder, VersionFolder);
		}
		else
		{
			Logger.Info($"整合包中没有覆写文件夹：{OverridesFolder}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
		}
		string text = $"{OverridesFolder}PCL\\Setup.ini";
		string text2 = $"{VersionFolder}PCL\\Setup.ini";
		if (FileUtils.Exists(text))
		{
			ModBase.WriteIni(text, "VersionArgumentIndie", Conversions.ToString(1));
			ModBase.WriteIni(text, "VersionArgumentIndieV2", Conversions.ToString(Value: true));
			ModBase.WriteIni(text, "IsStar", Conversions.ToString(Value: false));
			FileUtils.Copy(text, text2);
		}
		else
		{
			ModBase.WriteIni(text2, "VersionArgumentIndie", Conversions.ToString(1));
			ModBase.WriteIni(text2, "VersionArgumentIndieV2", Conversions.ToString(Value: true));
		}
		ModBase.IniClearCache(text2);
	}

	private static void NotifyIncompatibleLoader(string LoaderName)
	{
		if (ModMain.MyMsgBox(string.Format("整合包中存在不兼容的加载器：{0}{1}如果你知道如何手动安装它，可以先选择跳过，然后在整合包下载结束后手动安装。", LoaderName, "\r\n"), "不兼容的加载器", "取消", $"不安装 {LoaderName} 并继续") == 1)
		{
			throw new OperationCanceledException();
		}
	}

	private static ModLoader.LoaderCombo<string> InstallPackCurseForge(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder, string InstanceName = null, string Logo = null)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Expected O, but got Unknown
		_Closure_0024__5_002D0 arg = default(_Closure_0024__5_002D0);
		_Closure_0024__5_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__5_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress = FileAddress;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Logo = Logo;
		try
		{
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Json = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(Archive.GetEntry(ArchiveBaseFolder + "manifest.json").Open(), (Encoding)null));
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("CurseForge 整合包安装信息存在问题", innerException);
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["minecraft"] == null || CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["minecraft"][(object)"version"] == null)
		{
			throw new Exception("CurseForge 整合包未提供 Minecraft 版本信息");
		}
		if (InstanceName == null)
		{
			InstanceName = (string)(CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["name"] ?? JToken.op_Implicit(""));
			ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
			if (Operators.CompareString(validateFolderName.Validate(InstanceName), "", TextCompare: false) != 0)
			{
				InstanceName = "";
			}
			if (Operators.CompareString(InstanceName, "", TextCompare: false) == 0)
			{
				InstanceName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<Validate> { validateFolderName });
			}
			if (string.IsNullOrEmpty(InstanceName))
			{
				throw new OperationCanceledException();
			}
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = PathUtils.AddSlashSuffix(Path.Combine(ModMinecraft.McFolderSelected, "versions", InstanceName) + "\\");
		CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetVersion = new McVersion(CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["minecraft"][(object)"version"].ToString(), true);
		foreach (JToken item in (IEnumerable<JToken>)(CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["minecraft"][(object)"modLoaders"] ?? JToken.op_Implicit(new byte[0])))
		{
			string text = StringExtensions.Lower((item[(object)"id"] ?? JToken.op_Implicit("")).ToString());
			if (StringExtensions.StartsWithF(text, "forge-", false))
			{
				if (text.Contains("recommended"))
				{
					throw new Exception("该整合包版本过老，已不支持进行安装！");
				}
				Logger.Info($"整合包 Forge 版本：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetVersion.ModLoaderVersions[(ModLoaders)1] = text.Replace("forge-", "");
			}
			else if (StringExtensions.StartsWithF(text, "neoforge-", false))
			{
				Logger.Info($"整合包 NeoForge 版本：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetVersion.ModLoaderVersions[(ModLoaders)16] = text.Replace("neoforge-", "");
			}
			else if (StringExtensions.StartsWithF(text, "fabric-", false))
			{
				Logger.Info($"整合包 Fabric 版本：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetVersion.ModLoaderVersions[(ModLoaders)4] = text.Replace("fabric-", "");
			}
			else
			{
				NotifyIncompatibleLoader(text);
			}
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp = ModMain.RequestTaskTempFolder();
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		string text2 = (string)(CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["overrides"] ?? JToken.op_Implicit(""));
		if (Operators.CompareString(text2, "", TextCompare: false) != 0)
		{
			_Closure_0024__5_002D1 arg2 = default(_Closure_0024__5_002D1);
			_Closure_0024__5_002D1 CS_0024_003C_003E8__locals1 = new _Closure_0024__5_002D1(arg2);
			CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2 = CS_0024_003C_003E8__locals0;
			CS_0024_003C_003E8__locals1._0024VB_0024Local_OverridesFolder = CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_InstallTemp + ArchiveBaseFolder + ((Operators.CompareString(text2, ".", TextCompare: false) == 0 || Operators.CompareString(text2, "./", TextCompare: false) == 0) ? "" : text2);
			if (!PathUtils.IsParentOf(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_InstallTemp, CS_0024_003C_003E8__locals1._0024VB_0024Local_OverridesFolder))
			{
				throw new Exception("整合包的 overrides 路径超出了解压目录，请向整合包作者反馈此问题！");
			}
			list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
			{
				ExtractModpackFiles(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_InstallTemp, CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.6));
				CopyOverrideDirectory(CS_0024_003C_003E8__locals1._0024VB_0024Local_OverridesFolder, CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_VersionFolder, Task);
			})
			{
				ProgressWeight = (double)FileUtils.GetInfo(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
				Block = false
			});
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ModList = new List<int>();
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ModOptionalList = new List<int>();
		foreach (JToken item2 in (IEnumerable<JToken>)(CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["files"] ?? JToken.op_Implicit(new byte[0])))
		{
			if (item2[(object)"projectID"] == null || item2[(object)"fileID"] == null)
			{
				ModMain.Hint("某项 Mod 缺少必要信息，已跳过：" + item2.ToString());
				continue;
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_ModList.Add((int)item2[(object)"fileID"]);
			if (item2[(object)"required"] != null && !item2[(object)"required"].ToObject<bool>())
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_ModOptionalList.Add((int)item2[(object)"fileID"]);
			}
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_ModList.Any())
		{
			List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
			list2.Add(new ModLoader.LoaderTask<int, JArray>("获取 Mod 下载信息", [SpecialName] (ModLoader.LoaderTask<int, JArray> Task) =>
			{
				//IL_003e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0048: Expected O, but got Unknown
				Task.Output = (JArray)ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/files", HttpMethod.Post, "{\"fileIds\": [" + EnumerableExtensions.Join((IEnumerable)CS_0024_003C_003E8__locals0._0024VB_0024Local_ModList, ",") + "]}", "application/json")[(object)"data"];
				if (CS_0024_003C_003E8__locals0._0024VB_0024Local_ModList.Count != ((JContainer)Task.Output).Count)
				{
					List<int> list6 = CS_0024_003C_003E8__locals0._0024VB_0024Local_ModList.Except(((IEnumerable<JToken>)Task.Output).Select([SpecialName] (JToken j) => j[(object)"id"].ToObject<int>())).ToList();
					Logger.Info(string.Format("缺失的 fileIds：{0}", EnumerableExtensions.Join((IEnumerable)list6, "、")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					List<(int, int)> list7 = list6.Select([SpecialName] (int id) =>
					{
						//IL_0034: Unknown result type (might be due to invalid IL or missing references)
						_Closure_0024__5_002D2 arg3 = default(_Closure_0024__5_002D2);
						_Closure_0024__5_002D2 CS_0024_003C_003E8__locals2 = new _Closure_0024__5_002D2(arg3);
						CS_0024_003C_003E8__locals2._0024VB_0024Local_id = id;
						return (((JObject)((IEnumerable<JToken>)CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["files"]).AsEnumerable().First([SpecialName] (JToken f) => f[(object)"fileID"].ToObject<int>() == CS_0024_003C_003E8__locals2._0024VB_0024Local_id))["projectID"].ToObject<int>(), CS_0024_003C_003E8__locals2._0024VB_0024Local_id);
					}).ToList();
					Logger.Info(string.Format("缺失项：{0}", EnumerableExtensions.Join((IEnumerable)list7, "、")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					List<ResourceProject> source = new List<ResourceProject>();
					try
					{
						source = ResourceProject.FromProjectIds(list7.Select<(int, int), string>([SpecialName] ((int ProjectId, int FileId) e) => e.ProjectId.ToString()), (ResourcePlatforms)1).ToList();
					}
					catch (Exception ex2)
					{
						ProjectData.SetProjectError(ex2);
						Exception ex3 = ex2;
						Logger.Warn(ex3, "获取缺失的工程信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						ProjectData.ClearProjectError();
					}
					List<string> list8 = new List<string>();
					using (List<(int, int)>.Enumerator enumerator5 = list7.GetEnumerator())
					{
						_Closure_0024__5_002D3 closure_0024__5_002D = default(_Closure_0024__5_002D3);
						while (enumerator5.MoveNext())
						{
							closure_0024__5_002D = new _Closure_0024__5_002D3(closure_0024__5_002D);
							closure_0024__5_002D._0024VB_0024Local_Missing = enumerator5.Current;
							ResourceProject resourceProject = source.FirstOrDefault(closure_0024__5_002D._Lambda_0024__6);
							if ((object)resourceProject == null)
							{
								list8.Add($"未知 CurseForge 项目 (项目 ID {closure_0024__5_002D._0024VB_0024Local_Missing.ProjectId}，文件 ID {closure_0024__5_002D._0024VB_0024Local_Missing.FileId})");
							}
							else
							{
								list8.Add($"{resourceProject.TranslatedName} (文件 ID {closure_0024__5_002D._0024VB_0024Local_Missing.FileId})");
							}
						}
					}
					if (ModMain.MyMsgBox(string.Format("未找到该整合包所需的以下内容，它们可能已被原作者删除：{0}- {1}{2}{3}是否继续安装？缺失这些内容可能导致整合包出现异常！", "\r\n", EnumerableExtensions.Join((IEnumerable)list8, "\r\n- "), "\r\n", "\r\n"), "整合包内容缺失", "继续", "取消", "", IsWarn: true) == 2)
					{
						Task.Cancel();
					}
				}
			})
			{
				ProgressWeight = (double)CS_0024_003C_003E8__locals0._0024VB_0024Local_ModList.Count / 10.0
			});
			list2.Add(new ModLoader.LoaderTask<JArray, List<ModNet.NetFile>>("构造 Mod 下载信息", [SpecialName] (ModLoader.LoaderTask<JArray, List<ModNet.NetFile>> Task) =>
			{
				//IL_018b: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
				//IL_018e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0193: Unknown result type (might be due to invalid IL or missing references)
				//IL_019a: Expected O, but got Unknown
				//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
				//IL_00db: Expected O, but got Unknown
				//IL_013e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0158: Unknown result type (might be due to invalid IL or missing references)
				//IL_017f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0172: Unknown result type (might be due to invalid IL or missing references)
				Dictionary<int, ModNet.NetFile> dictionary = new Dictionary<int, ModNet.NetFile>();
				foreach (JToken item3 in Task.Input)
				{
					int num = item3[(object)"id"].ToObject<int>();
					if (dictionary.ContainsKey(num) || (CS_0024_003C_003E8__locals0._0024VB_0024Local_ModOptionalList.Contains(num) && ModMain.MyMsgBox("是否要下载整合包中的可选文件 " + item3[(object)"displayName"].ToString() + "？", "下载可选文件", "是", "否") == 2))
					{
						continue;
					}
					string path;
					ResourceTypes expectedResourceType;
					if (((IEnumerable<JToken>)item3[(object)"modules"]).Any())
					{
						List<string> list5 = ((IEnumerable<JToken>)(JArray)item3[(object)"modules"]).Select([SpecialName] (JToken l) => l[(object)"name"].ToString()).ToList();
						if (list5.Contains("META-INF") || list5.Contains("mcmod.info") || list5.Contains("fabric.mod.json"))
						{
							goto IL_0136;
						}
						if (item3 != null)
						{
							JToken obj = item3[(object)"FileName"];
							if (obj != null && StringExtensions.EndsWithF(obj.ToString(), ".jar", true))
							{
								goto IL_0136;
							}
						}
						if (list5.Contains("pack.mcmeta"))
						{
							path = "resourcepacks";
							expectedResourceType = (ResourceTypes)4;
						}
						else if (list5.Contains("shaders"))
						{
							path = "shaderpacks";
							expectedResourceType = (ResourceTypes)8;
						}
						else
						{
							path = "saves";
							expectedResourceType = (ResourceTypes)64;
						}
					}
					else
					{
						path = "mods";
						expectedResourceType = (ResourceTypes)1;
					}
					goto IL_018d;
					IL_0136:
					path = "mods";
					expectedResourceType = (ResourceTypes)1;
					goto IL_018d;
					IL_018d:
					ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)item3, expectedResourceType);
					if (resourceVersion.DownloadAvailable)
					{
						dictionary.Add(num, resourceVersion.ToNetFile(PathUtils.AddSlashSuffix(Path.Combine(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder, path)), ResourceVersion.DownloadReason.ModPack, CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetVersion.VanillaName, CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetVersion.ModLoaders));
						Task.Progress += 1.0 / (double)checked(1 + CS_0024_003C_003E8__locals0._0024VB_0024Local_ModList.Count);
					}
				}
				Task.Output = dictionary.Values.ToList();
			})
			{
				ProgressWeight = (double)CS_0024_003C_003E8__locals0._0024VB_0024Local_ModList.Count / 200.0,
				Show = false
			});
			list2.Add(new ModNet.LoaderDownload("下载 Mod", new List<ModNet.NetFile>())
			{
				ProgressWeight = (double)CS_0024_003C_003E8__locals0._0024VB_0024Local_ModList.Count * 1.5
			});
			list.Add(new ModLoader.LoaderCombo<int>("下载 Mod（主加载器）", list2)
			{
				Show = false,
				ProgressWeight = list2.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			});
		}
		McComponents components = new McComponents(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetVersion.VanillaName, CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetVersion.ModLoaderVersions, (JObject)null, (string)null, (string)null);
		List<ModLoader.LoaderBase> list3 = ModDownloadLib.McInstallLoadersGet(new ModDownloadLib.McInstallRequest
		{
			NewInstanceName = InstanceName,
			InstanceFolder = CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder,
			Components = components
		});
		List<ModLoader.LoaderBase> list4 = new List<ModLoader.LoaderBase>();
		list4.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
		{
			Show = false,
			Block = false,
			ProgressWeight = list.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
		});
		list4.Add(new ModLoader.LoaderCombo<string>("游戏安装", list3)
		{
			Show = false,
			ProgressWeight = list3.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
		});
		list4.Add(new ModLoader.LoaderTask<string, string>("最终整理文件", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
		{
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Logo != null && FileUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_Logo))
			{
				FileUtils.Copy(CS_0024_003C_003E8__locals0._0024VB_0024Local_Logo, CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "PCL\\Logo.png");
				ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "PCL\\Setup.ini", "Logo", "PCL\\Logo.png");
				ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "PCL\\Setup.ini", "LogoCustom", "True");
				Logger.Info($"已设置整合包 Logo：{CS_0024_003C_003E8__locals0._0024VB_0024Local_Logo}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			}
			string text3 = $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder}saves\\";
			foreach (string item4 in DirectoryUtils.EnumerateFiles(text3, false, "*.zip"))
			{
				FileUtils.ExtractToDirectory(item4, text3, Task.CreateCancellationToken(), (ProgressProvider)null);
				FileUtils.Delete(item4, false);
			}
			string[] array = new string[2]
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "原始整合包.zip",
				CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "原始整合包.mrpack"
			};
			foreach (string text4 in array)
			{
				if (FileUtils.Exists(text4))
				{
					Logger.Info($"删除原始整合包文件：{text4}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					FileUtils.Delete(text4, false);
				}
			}
			if (FileUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress) && Operators.CompareString(PathUtils.GetFileNameWithoutExtension(CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress), "modpack", TextCompare: false) == 0)
			{
				Logger.Info($"删除安装整合包文件：{CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				FileUtils.Delete(CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress, false);
			}
		})
		{
			ProgressWeight = 0.1,
			Show = false
		});
		CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName = "CurseForge 整合包安装：" + InstanceName + " ";
		if (((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).Any([SpecialName] (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName, TextCompare: false) == 0))
		{
			ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Red);
			throw new OperationCanceledException();
		}
		ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName, list4);
		loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
		loaderCombo.Start(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder);
		ModLoader.LoaderTaskbarAdd(loaderCombo);
		ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
		return loaderCombo;
	}

	private static ModLoader.LoaderCombo<string> InstallPackModrinth(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder, string InstanceName = null, string Logo = null)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Expected O, but got Unknown
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Expected O, but got Unknown
		_Closure_0024__6_002D0 arg = default(_Closure_0024__6_002D0);
		_Closure_0024__6_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__6_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress = FileAddress;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder = ArchiveBaseFolder;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Logo = Logo;
		JObject val;
		try
		{
			val = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(Archive.GetEntry(CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "modrinth.index.json").Open(), (Encoding)null));
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("Modrinth 整合包安装信息存在问题", innerException);
		}
		if (val["dependencies"] == null || val["dependencies"][(object)"minecraft"] == null)
		{
			throw new Exception("Modrinth 整合包未提供 Minecraft 版本信息");
		}
		McVersion val2 = new McVersion("Unknown", true);
		foreach (JProperty item in (IEnumerable<JToken>)(val["dependencies"] ?? JToken.op_Implicit(new byte[0])))
		{
			JProperty val3 = item;
			switch (StringExtensions.Lower(val3.Name))
			{
			case "minecraft":
				val2.VanillaName = val3.Value.ToString();
				break;
			case "forge":
				val2.ModLoaderVersions[(ModLoaders)1] = val3.Value.ToString();
				Logger.Info($"整合包 Forge 版本：{val2.ModLoaderVersions[(ModLoaders)1]}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				break;
			case "neoforge":
			case "neo-forge":
				val2.ModLoaderVersions[(ModLoaders)16] = val3.Value.ToString();
				Logger.Info($"整合包 NeoForge 版本：{val2.ModLoaderVersions[(ModLoaders)16]}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				break;
			case "fabric-loader":
				val2.ModLoaderVersions[(ModLoaders)4] = val3.Value.ToString();
				Logger.Info($"整合包 Fabric 版本：{val2.ModLoaderVersions[(ModLoaders)4]}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				break;
			default:
				NotifyIncompatibleLoader(val3.Name);
				break;
			}
		}
		if (InstanceName == null)
		{
			InstanceName = (string)(val["name"] ?? JToken.op_Implicit(""));
			ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
			if (Operators.CompareString(validateFolderName.Validate(InstanceName), "", TextCompare: false) != 0)
			{
				InstanceName = "";
			}
			if (Operators.CompareString(InstanceName, "", TextCompare: false) == 0)
			{
				InstanceName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<Validate> { validateFolderName });
			}
			if (string.IsNullOrEmpty(InstanceName))
			{
				throw new OperationCanceledException();
			}
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = Path.Combine(ModMinecraft.McFolderSelected, "versions", InstanceName) + "\\";
		CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp = ModMain.RequestTaskTempFolder();
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
		{
			ExtractModpackFiles(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp, CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.5));
			CopyOverrideDirectory(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp + CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "overrides", CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder, Task);
			CopyOverrideDirectory(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp + CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "client-overrides", CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder, Task);
		})
		{
			ProgressWeight = (double)FileUtils.GetInfo(CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
			Block = false
		});
		List<ModNet.NetFile> list2 = new List<ModNet.NetFile>();
		foreach (JToken item2 in (IEnumerable<JToken>)(val["files"] ?? JToken.op_Implicit(new byte[0])))
		{
			if (item2[(object)"env"] != null)
			{
				string left = item2[(object)"env"][(object)"client"].ToString();
				if (Operators.CompareString(left, "optional", TextCompare: false) != 0)
				{
					if (Operators.CompareString(left, "unsupported", TextCompare: false) == 0)
					{
						continue;
					}
				}
				else if (ModMain.MyMsgBox("是否要下载可选文件 " + PathUtils.GetLastPart(item2[(object)"path"].ToString()) + "？", "下载可选文件", "是", "否") == 2)
				{
					continue;
				}
			}
			List<string> list3 = ((IEnumerable<JToken>)item2[(object)"downloads"]).SelectMany([SpecialName] (JToken t) => ResourceVersion.ParseCurseForgeDownloadUrls(t.ToString())).ToList();
			list3.AddRange(list3.Select([SpecialName] (string u) => ModDownload.DlSourceModGet(u)).ToList());
			list3 = ResourceVersion.ParseModrinthTrackArguments(list3, ResourceVersion.DownloadReason.ModPack, val2.VanillaName, val2.ModLoaders).ToList();
			list3 = list3.Distinct().ToList();
			string text = Path.Combine(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder, item2[(object)"path"].ToString().TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
			if (!PathUtils.IsParentOf(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder, text))
			{
				ModMain.MyMsgBox(string.Format("整合包的文件路径超出了版本文件夹，请向整合包作者反馈此问题！{0}目标：{1}{2}错误的文件：{3}", "\r\n", Path.GetFullPath(text), "\r\n", text), "文件路径校验失败", "确定", "", "", IsWarn: true);
				throw new OperationCanceledException();
			}
			list2.Add(new ModNet.NetFile(list3, text, new FileChecker
			{
				Hash = item2[(object)"hashes"][(object)"sha1"].ToString()
			}, SimulateBrowserHeaders: true));
		}
		if (list2.Any())
		{
			list.Add(new ModNet.LoaderDownload("下载额外文件", list2)
			{
				ProgressWeight = (double)list2.Count * 1.5
			});
		}
		McComponents components = new McComponents(val2.VanillaName, val2.ModLoaderVersions, (JObject)null, (string)null, (string)null);
		List<ModLoader.LoaderBase> list4 = ModDownloadLib.McInstallLoadersGet(new ModDownloadLib.McInstallRequest
		{
			NewInstanceName = InstanceName,
			InstanceFolder = CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder,
			Components = components
		});
		List<ModLoader.LoaderBase> list5 = new List<ModLoader.LoaderBase>();
		list5.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
		{
			Show = false,
			Block = false,
			ProgressWeight = list.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
		});
		list5.Add(new ModLoader.LoaderCombo<string>("游戏安装", list4)
		{
			Show = false,
			ProgressWeight = list4.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
		});
		list5.Add(new ModLoader.LoaderTask<string, string>("最终整理文件", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
		{
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Logo != null && FileUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_Logo))
			{
				FileUtils.Copy(CS_0024_003C_003E8__locals0._0024VB_0024Local_Logo, CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "PCL\\Logo.png");
				ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "PCL\\Setup.ini", "Logo", "PCL\\Logo.png");
				ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "PCL\\Setup.ini", "LogoCustom", "True");
				Logger.Info($"已设置整合包 Logo：{CS_0024_003C_003E8__locals0._0024VB_0024Local_Logo}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			}
			string[] array = new string[2]
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "原始整合包.zip",
				CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "原始整合包.mrpack"
			};
			foreach (string text2 in array)
			{
				if (FileUtils.Exists(text2))
				{
					Logger.Info($"删除原始整合包文件：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					FileUtils.Delete(text2, false);
				}
			}
			if (FileUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress) && Operators.CompareString(PathUtils.GetFileNameWithoutExtension(CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress), "modpack", TextCompare: false) == 0)
			{
				Logger.Info($"删除安装整合包文件：{CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				FileUtils.Delete(CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress, false);
			}
		})
		{
			ProgressWeight = 0.1,
			Show = false
		});
		CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName = $"Modrinth 整合包安装：{InstanceName} ";
		if (((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).Any([SpecialName] (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName, TextCompare: false) == 0))
		{
			ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Red);
			throw new OperationCanceledException();
		}
		ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName, list5);
		loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
		loaderCombo.Start(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder);
		ModLoader.LoaderTaskbarAdd(loaderCombo);
		ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
		return loaderCombo;
	}

	private static ModLoader.LoaderCombo<string> InstallPackHMCL(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		JObject val;
		try
		{
			val = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(Archive.GetEntry(ArchiveBaseFolder + "modpack.json").Open(), Encoding.UTF8));
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("HMCL 整合包安装信息存在问题", innerException);
		}
		string text = (string)(val["name"] ?? JToken.op_Implicit(""));
		ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
		if (Operators.CompareString(validateFolderName.Validate(text), "", TextCompare: false) != 0)
		{
			text = "";
		}
		if (Operators.CompareString(text, "", TextCompare: false) == 0)
		{
			text = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<Validate> { validateFolderName });
		}
		if (string.IsNullOrEmpty(text))
		{
			throw new OperationCanceledException();
		}
		string text2 = Path.Combine(ModMinecraft.McFolderSelected, "versions", text) + "\\";
		string text3 = ModMain.RequestTaskTempFolder();
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
		{
			ExtractModpackFiles(text3, FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.6));
			CopyOverrideDirectory(text3 + ArchiveBaseFolder + "minecraft", text2, Task);
		})
		{
			ProgressWeight = (double)FileUtils.GetInfo(FileAddress).Length / 1024.0 / 1024.0 / 6.0,
			Block = false
		});
		if (val["gameVersion"] == null)
		{
			throw new Exception("该 HMCL 整合包未提供游戏版本信息，无法安装！");
		}
		McComponents components = new McComponents(val["gameVersion"].ToString(), new Dictionary<ModLoaders, string>(), (JObject)null, (string)null, (string)null);
		List<ModLoader.LoaderBase> list2 = ModDownloadLib.McInstallLoadersGet(new ModDownloadLib.McInstallRequest
		{
			NewInstanceName = text,
			InstanceFolder = text2,
			Components = components
		});
		List<ModLoader.LoaderBase> loaders = new List<ModLoader.LoaderBase>
		{
			new ModLoader.LoaderCombo<string>("整合包安装", list)
			{
				Show = false,
				Block = false,
				ProgressWeight = list.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			},
			new ModLoader.LoaderCombo<string>("游戏安装", list2)
			{
				Show = false,
				ProgressWeight = list2.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
			}
		};
		string text4 = "HMCL 整合包安装：" + text + " ";
		if (((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).Any([SpecialName] (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, text4, TextCompare: false) == 0))
		{
			ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Red);
			throw new OperationCanceledException();
		}
		ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(text4, loaders);
		loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
		loaderCombo.Start(text2);
		ModLoader.LoaderTaskbarAdd(loaderCombo);
		ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
		return loaderCombo;
	}

	private static ModLoader.LoaderCombo<string> InstallPackMMC(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Expected O, but got Unknown
		_Closure_0024__8_002D0 arg = default(_Closure_0024__8_002D0);
		_Closure_0024__8_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__8_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress = FileAddress;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder = ArchiveBaseFolder;
		JObject val;
		string text;
		try
		{
			val = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(Archive.GetEntry(CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "mmc-pack.json").Open(), Encoding.UTF8));
			text = StringExtensions.ReadString(Archive.GetEntry(CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "instance.cfg").Open(), Encoding.UTF8);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("MMC 整合包安装信息存在问题", innerException);
		}
		string text2 = StringExtensions.RegexSeek(text, "(?<=\\nname\\=)[^\\n]+", RegexOptions.None) ?? "";
		ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
		if (Operators.CompareString(validateFolderName.Validate(text2), "", TextCompare: false) != 0)
		{
			text2 = "";
		}
		if (Operators.CompareString(text2, "", TextCompare: false) == 0)
		{
			text2 = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<Validate> { validateFolderName });
		}
		if (string.IsNullOrEmpty(text2))
		{
			throw new OperationCanceledException();
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = Path.Combine(ModMinecraft.McFolderSelected, "versions", text2) + "\\";
		CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp = ModMain.RequestTaskTempFolder();
		CS_0024_003C_003E8__locals0._0024VB_0024Local_SetupFile = CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "PCL\\Setup.ini";
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
		{
			ExtractModpackFiles(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp, CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.55));
			string overridesFolder = ((!DirectoryUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp + CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + ".minecraft") || DirectoryUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp + CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "minecraft")) ? (CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp + CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "minecraft") : (CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp + CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + ".minecraft"));
			CopyOverrideDirectory(overridesFolder, CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder, Task);
			try
			{
				string text5 = CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp + CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "instance.cfg";
				if (FileUtils.Exists(text5))
				{
					List<string> list4 = new List<string>();
					string[] array = FileUtils.ReadAsLines(text5, true, (Encoding)null, (Type)null);
					foreach (string text6 in array)
					{
						if (text6.Contains("="))
						{
							list4.Add(StringExtensions.BeforeFirst(text6, "=", false) + ":" + StringExtensions.AfterFirst(text6, "=", false));
						}
					}
					FileUtils.Write(text5, EnumerableExtensions.Join((IEnumerable)list4, "\r\n"), (Encoding)null);
					if (Conversions.ToBoolean(ModBase.ReadIni(text5, "OverrideCommands", Conversions.ToString(Value: false))))
					{
						string text7 = ModBase.ReadIni(text5, "PreLaunchCommand");
						if (Operators.CompareString(text7, "", TextCompare: false) != 0)
						{
							text7 = text7.Replace("\\\"", "\"").Replace("$INST_JAVA", "{java}java.exe").Replace("$INST_MC_DIR\\", "{minecraft}")
								.Replace("$INST_MC_DIR", "{minecraft}")
								.Replace("$INST_DIR\\", "{verpath}")
								.Replace("$INST_DIR", "{verpath}")
								.Replace("$INST_ID", "{name}")
								.Replace("$INST_NAME", "{name}");
							ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_SetupFile, "VersionAdvanceRun", text7);
							Logger.Info($"迁移 MultiMC 版本独立设置：启动前执行命令：{text7}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						}
					}
					if (Conversions.ToBoolean(ModBase.ReadIni(text5, "JoinServerOnLaunch", Conversions.ToString(Value: false))))
					{
						string text8 = ModBase.ReadIni(text5, "JoinServerOnLaunchAddress").Replace("\\\"", "\"");
						ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_SetupFile, "VersionServerEnter", text8);
						Logger.Info($"迁移 MultiMC 版本独立设置：自动进入服务器：{text8}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
					string text9 = ModBase.ReadIni(text5, "iconKey");
					if (Operators.CompareString(text9, "", TextCompare: false) != 0 && FileUtils.Exists($"{CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp}{CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder}{text9}.png"))
					{
						ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_SetupFile, "LogoCustom", Conversions.ToString(Value: true));
						ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_SetupFile, "Logo", "PCL\\Logo.png");
						FileUtils.Copy($"{CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp}{CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder}{text9}.png", CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "PCL\\Logo.png");
						Logger.Info($"迁移 MultiMC 版本独立设置：版本图标（{text9}.png）", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
					string text10 = ModBase.ReadIni(text5, "JvmArgs");
					if (Operators.CompareString(text10, "", TextCompare: false) != 0)
					{
						if (Conversions.ToBoolean(ModBase.ReadIni(text5, "OverrideJavaArgs", Conversions.ToString(Value: false))))
						{
							ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_SetupFile, "VersionAdvanceJvm", text10);
							Logger.Info($"迁移 MultiMC 版本独立设置：JVM 参数（覆盖）：{text10}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						}
						else
						{
							text10 = text10 + " " + Settings.Get<string>("LaunchAdvanceJvm");
							ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_SetupFile, "VersionAdvanceJvm", text10);
							Logger.Info($"迁移 MultiMC 版本独立设置：JVM 参数（追加）：{text10}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						}
					}
				}
			}
			catch (Exception ex2)
			{
				ProjectData.SetProjectError(ex2);
				Exception ex3 = ex2;
				Logger.Warn(ex3, $"读取 MMC 配置文件失败（{CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp}{CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder}instance.cfg）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				ProjectData.ClearProjectError();
			}
		})
		{
			ProgressWeight = (double)FileUtils.GetInfo(CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
			Block = false
		});
		if (val["components"] == null)
		{
			throw new Exception("该 MMC 整合包未提供游戏版本信息，无法安装！");
		}
		string text3 = null;
		Dictionary<ModLoaders, string> dictionary = new Dictionary<ModLoaders, string>();
		foreach (JObject item in (IEnumerable<JToken>)val["components"])
		{
			JObject val2 = item;
			if (!val2.ContainsKey("uid"))
			{
				continue;
			}
			string text4 = val2["uid"].ToString();
			switch (text4)
			{
			case "net.minecraft":
				text3 = (string)val2["version"];
				continue;
			case "net.minecraftforge":
				dictionary[(ModLoaders)1] = (string)val2["version"];
				continue;
			case "net.neoforged":
				dictionary[(ModLoaders)16] = (string)val2["version"];
				continue;
			case "net.fabricmc.fabric-loader":
				dictionary[(ModLoaders)4] = (string)val2["version"];
				continue;
			}
			if (StringExtensions.StartsWithF(text4, "org.lwjgl", false))
			{
				Logger.Info($"已跳过 LWJGL 项：{text4}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
			}
			else
			{
				NotifyIncompatibleLoader(text4);
			}
		}
		McComponents components = new McComponents(text3, dictionary, (JObject)null, (string)null, (string)null);
		List<ModLoader.LoaderBase> list2 = ModDownloadLib.McInstallLoadersGet(new ModDownloadLib.McInstallRequest
		{
			NewInstanceName = text2,
			InstanceFolder = CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder,
			Components = components
		});
		List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
		list3.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
		{
			Show = false,
			Block = false,
			ProgressWeight = list.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
		});
		list3.Add(new ModLoader.LoaderCombo<string>("游戏安装", list2)
		{
			Show = false,
			ProgressWeight = list2.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
		});
		CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName = "MMC 整合包安装：" + text2 + " ";
		if (((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).Any([SpecialName] (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName, TextCompare: false) == 0))
		{
			ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Red);
			throw new OperationCanceledException();
		}
		ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName, list3);
		loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
		loaderCombo.Start(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder);
		ModLoader.LoaderTaskbarAdd(loaderCombo);
		ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
		return loaderCombo;
	}

	private static ModLoader.LoaderCombo<string> InstallPackMCBBS(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder, string InstanceName = null)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Expected O, but got Unknown
		_Closure_0024__9_002D0 arg = default(_Closure_0024__9_002D0);
		_Closure_0024__9_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__9_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress = FileAddress;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder = ArchiveBaseFolder;
		try
		{
			ZipArchiveEntry val = Archive.GetEntry(CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "mcbbs.packmeta") ?? Archive.GetEntry(CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "manifest.json");
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Json = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(val.Open(), Encoding.UTF8));
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("MCBBS 整合包安装信息存在问题", innerException);
		}
		if (InstanceName == null)
		{
			InstanceName = (string)(CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["name"] ?? JToken.op_Implicit(""));
			ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
			if (Operators.CompareString(validateFolderName.Validate(InstanceName), "", TextCompare: false) != 0)
			{
				InstanceName = "";
			}
			if (Operators.CompareString(InstanceName, "", TextCompare: false) == 0)
			{
				InstanceName = ModMain.MyMsgBoxInput("输入版本名称", "", "", new Collection<Validate> { validateFolderName });
			}
			if (string.IsNullOrEmpty(InstanceName))
			{
				throw new OperationCanceledException();
			}
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = Path.Combine(ModMinecraft.McFolderSelected, "versions", InstanceName) + "\\";
		CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp = ModMain.RequestTaskTempFolder();
		CS_0024_003C_003E8__locals0._0024VB_0024Local_SetupFile = CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "PCL\\Setup.ini";
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		list.Add(new ModLoader.LoaderTask<string, int>("解压整合包文件", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
		{
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			ExtractModpackFiles(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp, CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.6));
			CopyOverrideDirectory(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallTemp + CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder + "overrides", CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder, Task);
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["launchInfo"] != null)
			{
				JObject val3 = (JObject)CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["launchInfo"];
				if (val3.ContainsKey("javaArgument"))
				{
					ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_SetupFile, "VersionAdvanceJvm", string.Join(" ", (IEnumerable<JToken>)val3["javaArgument"]));
				}
				if (val3.ContainsKey("launchArgument"))
				{
					ModBase.WriteIni(CS_0024_003C_003E8__locals0._0024VB_0024Local_SetupFile, "VersionAdvanceGame", string.Join(" ", (IEnumerable<JToken>)val3["launchArgument"]));
				}
			}
		})
		{
			ProgressWeight = (double)FileUtils.GetInfo(CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress).Length / 1024.0 / 1024.0 / 6.0,
			Block = false
		});
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["addons"] == null)
		{
			throw new Exception("该 MCBBS 整合包未提供游戏版本附加信息，无法安装！");
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (JToken item in (IEnumerable<JToken>)CS_0024_003C_003E8__locals0._0024VB_0024Local_Json["addons"])
		{
			dictionary.Add((string)item[(object)"id"], (string)item[(object)"version"]);
		}
		if (!dictionary.ContainsKey("game"))
		{
			throw new Exception("该 MCBBS 整合包未提供游戏版本信息，无法安装！");
		}
		if (dictionary.ContainsKey("quilt"))
		{
			ModMain.Hint("PCL 不支持安装 Quilt 整合包！", ModMain.HintType.Red);
			throw new OperationCanceledException();
		}
		Dictionary<ModLoaders, string> dictionary2 = new Dictionary<ModLoaders, string>();
		if (dictionary.ContainsKey("forge"))
		{
			dictionary2[(ModLoaders)1] = dictionary["forge"];
		}
		if (dictionary.ContainsKey("neoforge"))
		{
			dictionary2[(ModLoaders)16] = dictionary["neoforge"];
		}
		if (dictionary.ContainsKey("fabric"))
		{
			dictionary2[(ModLoaders)4] = dictionary["fabric"];
		}
		JObject val2 = null;
		if (dictionary.ContainsKey("optifine"))
		{
			_Closure_0024__9_002D1 arg2 = default(_Closure_0024__9_002D1);
			_Closure_0024__9_002D1 CS_0024_003C_003E8__locals1 = new _Closure_0024__9_002D1(arg2);
			CS_0024_003C_003E8__locals1._0024VB_0024Local_MinecraftVersion = dictionary["game"];
			if (StringExtensions.EndsWithF(CS_0024_003C_003E8__locals1._0024VB_0024Local_MinecraftVersion, ".0", false))
			{
				CS_0024_003C_003E8__locals1._0024VB_0024Local_MinecraftVersion = CS_0024_003C_003E8__locals1._0024VB_0024Local_MinecraftVersion.Substring(0, checked(CS_0024_003C_003E8__locals1._0024VB_0024Local_MinecraftVersion.Length - 2));
			}
			CS_0024_003C_003E8__locals1._0024VB_0024Local_OptiFineFileSuffix = "_HD_" + StringExtensions.AfterLast(dictionary["optifine"], "HD_", false) + ".jar";
			if (ModDownload.DlOptiFineListBmclapiLoader.State != ModBase.LoadState.Finished)
			{
				ModDownload.DlOptiFineListBmclapiLoader.WaitForExit();
			}
			val2 = JObject.FromObject((object)(ModDownload.DlOptiFineListBmclapiLoader.Output.Value.FirstOrDefault([SpecialName] (ModDownload.DlOptiFineListEntry v) => Operators.CompareString(v.Inherit, CS_0024_003C_003E8__locals1._0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0 && StringExtensions.EndsWithF(v.FileName, CS_0024_003C_003E8__locals1._0024VB_0024Local_OptiFineFileSuffix, false)) ?? throw new Exception(string.Format("OptiFine 列表中没有整合包所需的 OptiFine {0}（Minecraft {1}）", dictionary["optifine"], dictionary["game"]))));
		}
		McComponents components = new McComponents(dictionary["game"], dictionary2, val2, (string)null, (string)null);
		List<ModLoader.LoaderBase> list2 = ModDownloadLib.McInstallLoadersGet(new ModDownloadLib.McInstallRequest
		{
			NewInstanceName = InstanceName,
			InstanceFolder = CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder,
			Components = components
		});
		List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
		list3.Add(new ModLoader.LoaderCombo<string>("整合包安装", list)
		{
			Show = false,
			Block = false,
			ProgressWeight = list.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
		});
		list3.Add(new ModLoader.LoaderCombo<string>("游戏安装", list2)
		{
			Show = false,
			ProgressWeight = list2.Sum([SpecialName] (ModLoader.LoaderBase l) => l.ProgressWeight)
		});
		CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName = "MCBBS 整合包安装：" + InstanceName + " ";
		if (((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).Any([SpecialName] (ModLoader.LoaderBase l) => Operators.CompareString(l.Name, CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName, TextCompare: false) == 0))
		{
			ModMain.Hint("该整合包正在安装中！", ModMain.HintType.Red);
			throw new OperationCanceledException();
		}
		ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName, list3);
		loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
		loaderCombo.Start(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder);
		ModLoader.LoaderTaskbarAdd(loaderCombo);
		ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
		return loaderCombo;
	}

	private static ModLoader.LoaderCombo<string> InstallPackLauncherPack(string FileAddress, ZipArchive Archive, string ArchiveBaseFolder)
	{
		_Closure_0024__10_002D0 arg = default(_Closure_0024__10_002D0);
		_Closure_0024__10_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__10_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress = FileAddress;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder = ArchiveBaseFolder;
		ModMain.MyMsgBox("接下来请选择一个空文件夹，它会被安装到这个文件夹里。", "安装", "继续", "", "", IsWarn: false, HighLight: true, ForceWait: true);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder = Dialogs.SelectFolder("选择安装目标（必须是一个空文件夹）", false, (string)null).FirstOrDefault();
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder == null)
		{
			throw new OperationCanceledException();
		}
		if (!DirectoryUtils.IsEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder))
		{
			ModMain.Hint("请选择一个空文件夹作为安装目标！", ModMain.HintType.Red);
			throw new OperationCanceledException();
		}
		ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("解压压缩包", new ModLoader.LoaderBase[1]
		{
			new ModLoader.LoaderTask<string, int>("解压压缩包", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
			{
				ExtractModpackFiles(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder, CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.9));
				ModBase.OpenExplorer(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder);
				Thread.Sleep(400);
				string text = null;
				foreach (string item in DirectoryUtils.EnumerateFiles(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder, false, "*.exe"))
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(item);
					Logger.Info($"文件 {item} 的产品名标识为 {versionInfo.ProductName}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					if (Operators.CompareString(versionInfo.ProductName, "Plain Craft Launcher", TextCompare: false) == 0)
					{
						text = item;
						Logger.Info($"发现整合包附带的 PCL 启动器：{item}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
					else if ((StringExtensions.ContainsIgnoreCase(versionInfo.ProductName, "Launcher") || versionInfo.ProductName.Contains("启动")) && Operators.CompareString(versionInfo.ProductName, "Plain Craft Launcher Admin Manager", TextCompare: false) != 0 && text == null)
					{
						text = item;
						Logger.Info($"发现整合包附带的疑似第三方启动器：{item}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					}
				}
				Task.Progress = 0.95;
				if (text != null)
				{
					Logger.Info($"找到压缩包中附带的启动器：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
					if (ModMain.MyMsgBox(string.Format("整合包里似乎自带了启动器，是否换用它继续安装？{0}即将打开：{1}", "\r\n", text), "换用整合包启动器？", "换用", "不换用") == 1)
					{
						ModBase.StartProcess(text, "--wait");
						Logger.Info("为换用整合包中的启动器启动，强制结束程序", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
						ModMain.FrmMain.EndProgram(SendWarning: false);
						return;
					}
				}
				else
				{
					Logger.Info("未找到压缩包中附带的启动器", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				}
				string lastPart = PathUtils.GetLastPart(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder);
				DirectoryUtils.Create(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder + ".minecraft\\");
				PageSelectLeft.AddFolder(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder + ".minecraft\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder.Replace("/", "\\").TrimStart('\\'), lastPart, ShowHint: false);
				string text2 = DirectoryUtils.EnumerateFiles(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder, true, "modpack.*").First();
				Logger.Info($"调用 modpack 文件继续安装：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
				ModpackInstall(text2);
			})
		});
		loaderCombo.Start(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder);
		ModLoader.LoaderTaskbarAdd(loaderCombo);
		ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
		ModMain.FrmMain.BtnExtraDownload.Ribble();
		return loaderCombo;
	}

	private static ModLoader.LoaderCombo<string> InstallPackCompress(string FileAddress, ZipArchive Archive)
	{
		_Closure_0024__11_002D0 arg = default(_Closure_0024__11_002D0);
		_Closure_0024__11_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__11_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress = FileAddress;
		Match match = null;
		Regex regex = new Regex("^.*\\/(?=versions\\/(?<ver>[^\\/]+)\\/(\\k<ver>)\\.json$)", RegexOptions.IgnoreCase);
		foreach (ZipArchiveEntry entry in Archive.Entries)
		{
			Match match2 = regex.Match("/" + entry.FullName);
			if (match2.Success)
			{
				match = match2;
				break;
			}
		}
		if (match == null)
		{
			throw new Exception("文件结构不匹配，这可能不是 Minecraft 客户端压缩包？");
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder = match.Value.Replace("/", "\\").TrimStart('\\');
		Logger.Info($"检测到压缩包的 .minecraft 根目录：{CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder}，命中的版本名：{match.Groups[1].Value}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModModpack.vb");
		ModMain.MyMsgBox("接下来请选择一个空文件夹，它会被安装到这个文件夹里。", "安装", "继续", "", "", IsWarn: false, HighLight: true, ForceWait: true);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder = Dialogs.SelectFolder("选择安装目标（必须是一个空文件夹）", false, (string)null).FirstOrDefault();
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder == null)
		{
			throw new OperationCanceledException();
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder.Contains("!") || CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder.Contains(";"))
		{
			ModMain.Hint("Minecraft 文件夹路径中不能含有感叹号或分号！", ModMain.HintType.Red);
			throw new OperationCanceledException();
		}
		if (!DirectoryUtils.IsEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder))
		{
			ModMain.Hint("请选择一个空文件夹作为安装目标！", ModMain.HintType.Red);
			throw new OperationCanceledException();
		}
		ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("解压压缩包", new ModLoader.LoaderBase[1]
		{
			new ModLoader.LoaderTask<string, int>("解压压缩包", [SpecialName] (ModLoader.LoaderTask<string, int> Task) =>
			{
				ExtractModpackFiles(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder, CS_0024_003C_003E8__locals0._0024VB_0024Local_FileAddress, Task.CreateCancellationToken(), Task.CreateSyncProgressProvider(0.0, 0.95));
				PageSelectLeft.AddFolder(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder + CS_0024_003C_003E8__locals0._0024VB_0024Local_ArchiveBaseFolder, PathUtils.GetLastPart(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder), ShowHint: false);
				Thread.Sleep(400);
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMain.FrmMain.PageChange(FormMain.PageType.InstanceSelect);
				});
			})
		});
		loaderCombo.OnStateChanged = ModDownloadLib.McInstallState;
		loaderCombo.Start(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder);
		ModLoader.LoaderTaskbarAdd(loaderCombo);
		ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
		ModMain.FrmMain.BtnExtraDownload.Ribble();
		return loaderCombo;
	}
}
