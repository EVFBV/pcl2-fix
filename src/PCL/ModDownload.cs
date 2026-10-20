using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCLCS;

namespace PCL;

[StandardModule]
public sealed class ModDownload
{
	public struct DlClientListResult
	{
		public string SourceName;

		public bool IsOfficial;

		public JObject Value;
	}

	public struct DlOptiFineListResult
	{
		public string SourceName;

		public bool IsOfficial;

		public List<DlOptiFineListEntry> Value;
	}

	public class DlOptiFineListEntry
	{
		public string DisplayName;

		public string FileName;

		public string InstanceName;

		public bool IsPreview;

		private string _inherit;

		public string ReleaseTime;

		public string RequiredForgeVersion;

		public string Inherit
		{
			get
			{
				return _inherit;
			}
			set
			{
				if (StringExtensions.EndsWithF(value, ".0", false))
				{
					value = Strings.Left(value, checked(value.Length - 2));
				}
				_inherit = value;
			}
		}
	}

	public struct DlForgeListResult
	{
		public string SourceName;

		public bool IsOfficial;

		public List<string> Value;
	}

	public abstract class DlForgelikeEntry : IComparable<DlForgelikeEntry>
	{
		public bool IsNeoForge;

		public Version Version;

		public string VersionName;

		public string Inherit;

		public abstract string LoaderName { get; }

		public abstract string FileExtension { get; }

		public int CompareTo(DlForgelikeEntry other)
		{
			if (Version != other.Version)
			{
				return Version.CompareTo(other.Version);
			}
			return McUtils.CompareVersion(VersionName, other.VersionName);
		}

		int IComparable<DlForgelikeEntry>.CompareTo(DlForgelikeEntry other)
		{
			//ILSpy generated this explicit interface implementation from .override directive in CompareTo
			return this.CompareTo(other);
		}
	}

	public class DlForgeVersionEntry : DlForgelikeEntry
	{
		public string ReleaseTime;

		public string Hash;

		public bool IsRecommended;

		public string Category;

		public string FileVersion;

		public override string LoaderName => "Forge";

		public override string FileExtension
		{
			get
			{
				if (Operators.CompareString(Category, "installer", TextCompare: false) != 0)
				{
					return "zip";
				}
				return "jar";
			}
		}

		public DlForgeVersionEntry(string Version, string Branch, string Inherit)
		{
			Hash = null;
			switch (Version)
			{
			case "11.15.1.2318":
			case "11.15.1.1902":
			case "11.15.1.1890":
				Branch = "1.8.9";
				break;
			}
			if (Branch == null && Operators.CompareString(Inherit, "1.7.10", TextCompare: false) == 0 && Conversions.ToDouble(StringExtensions.Split(Version, ".", false)[3]) >= 1300.0)
			{
				Branch = "1.7.10";
			}
			IsNeoForge = false;
			VersionName = Version;
			base.Version = new Version(Version);
			base.Inherit = Inherit;
			FileVersion = Version + ((Branch == null) ? "" : ("-" + Branch));
		}
	}

	public struct DlNeoForgeListResult
	{
		public string SourceName;

		public bool IsOfficial;

		public List<DlNeoForgeListEntry> Value;
	}

	public class DlNeoForgeListEntry : DlForgelikeEntry
	{
		public bool IsBeta;

		public string ApiName;

		public string UrlBase
		{
			get
			{
				string text = ((Operators.CompareString(Inherit, "1.20.1", TextCompare: false) == 0) ? "forge" : "neoforge");
				return $"https://maven.neoforged.net/releases/net/neoforged/{text}/{ApiName}/{text}-{ApiName}";
			}
		}

		public override string LoaderName => "NeoForge";

		public override string FileExtension => "jar";

		public DlNeoForgeListEntry(string ApiName)
		{
			IsNeoForge = true;
			this.ApiName = ApiName;
			IsBeta = StringExtensions.ContainsIgnoreCase(ApiName, "beta") || StringExtensions.ContainsIgnoreCase(ApiName, "alpha");
			if (ApiName.Contains("1.20.1"))
			{
				VersionName = ApiName.Replace("1.20.1-", "");
				Version = new Version("19." + VersionName);
				Inherit = "1.20.1";
				return;
			}
			if (StringExtensions.StartsWithF(ApiName, "0.", false))
			{
				VersionName = ApiName;
				string[] array = StringExtensions.BeforeFirst(ApiName, "-", false).Split('.');
				Version = new Version(0, 0, Conversions.ToInteger(array.Last()));
				Inherit = array[1];
				return;
			}
			VersionName = ApiName;
			Version = new Version(StringExtensions.BeforeFirst(ApiName, "-", false));
			if (Version.Major >= 24)
			{
				Inherit = $"{Version.Major}.{Version.Minor}.{Version.Build}";
			}
			else
			{
				Inherit = $"1.{Version.Major}.{Version.Minor}";
			}
			if (StringExtensions.EndsWithF(Inherit, ".0", false))
			{
				Inherit = Strings.Left(Inherit, checked(Inherit.Length - 2));
			}
			if (VersionName.Contains("+"))
			{
				ref string inherit = ref Inherit;
				inherit = inherit + "-" + StringExtensions.AfterFirst(VersionName, "+", false);
			}
		}
	}

	public struct DlLiteLoaderListResult
	{
		public string SourceName;

		public bool IsOfficial;

		public List<DlLiteLoaderListEntry> Value;

		public Exception OfficialError;
	}

	public class DlLiteLoaderListEntry
	{
		public string FileName;

		public bool IsPreview;

		public string Inherit;

		public bool IsLegacy;

		public string ReleaseTime;

		public string MD5;

		public JToken JsonToken;
	}

	public struct DlFabricListResult
	{
		public string SourceName;

		public bool IsOfficial;

		public JObject Value;
	}

	private static List<int> _AllDrops = null;

	public static ModLoader.LoaderTask<string, DlClientListResult> DlClientListLoader = new ModLoader.LoaderTask<string, DlClientListResult>("DlClientList Main", DlClientListMain);

	public static ModLoader.LoaderTask<string, DlClientListResult> DlClientListMojangLoader = new ModLoader.LoaderTask<string, DlClientListResult>("DlClientList Mojang", DlClientListMojangMain);

	public static ModLoader.LoaderTask<string, DlClientListResult> DlClientListBmclapiLoader = new ModLoader.LoaderTask<string, DlClientListResult>("DlClientList Bmclapi", DlClientListBmclapiMain);

	public static ModLoader.LoaderTask<int, DlOptiFineListResult> DlOptiFineListLoader = new ModLoader.LoaderTask<int, DlOptiFineListResult>("DlOptiFineList Main", DlOptiFineListMain);

	public static ModLoader.LoaderTask<int, DlOptiFineListResult> DlOptiFineListOfficialLoader = new ModLoader.LoaderTask<int, DlOptiFineListResult>("DlOptiFineList Official", DlOptiFineListOfficialMain);

	public static ModLoader.LoaderTask<int, DlOptiFineListResult> DlOptiFineListBmclapiLoader = new ModLoader.LoaderTask<int, DlOptiFineListResult>("DlOptiFineList Bmclapi", DlOptiFineListBmclapiMain);

	public static ModLoader.LoaderTask<int, DlForgeListResult> DlForgeListLoader = new ModLoader.LoaderTask<int, DlForgeListResult>("DlForgeList Main", DlForgeListMain);

	public static ModLoader.LoaderTask<int, DlForgeListResult> DlForgeListOfficialLoader = new ModLoader.LoaderTask<int, DlForgeListResult>("DlForgeList Official", DlForgeListOfficialMain);

	public static ModLoader.LoaderTask<int, DlForgeListResult> DlForgeListBmclapiLoader = new ModLoader.LoaderTask<int, DlForgeListResult>("DlForgeList Bmclapi", DlForgeListBmclapiMain);

	public static ModLoader.LoaderTask<int, DlNeoForgeListResult> DlNeoForgeListLoader = new ModLoader.LoaderTask<int, DlNeoForgeListResult>("DlNeoForgeList Main", DlNeoForgeListMain);

	public static ModLoader.LoaderTask<int, DlNeoForgeListResult> DlNeoForgeListOfficialLoader = new ModLoader.LoaderTask<int, DlNeoForgeListResult>("DlNeoForgeList Official", DlNeoForgeListOfficialMain);

	public static ModLoader.LoaderTask<int, DlNeoForgeListResult> DlNeoForgeListBmclapiLoader = new ModLoader.LoaderTask<int, DlNeoForgeListResult>("DlNeoForgeList Bmclapi", DlNeoForgeListBmclapiMain);

	public static ModLoader.LoaderTask<int, DlLiteLoaderListResult> DlLiteLoaderListLoader = new ModLoader.LoaderTask<int, DlLiteLoaderListResult>("DlLiteLoaderList Main", DlLiteLoaderListMain);

	public static ModLoader.LoaderTask<int, DlLiteLoaderListResult> DlLiteLoaderListOfficialLoader = new ModLoader.LoaderTask<int, DlLiteLoaderListResult>("DlLiteLoaderList Official", DlLiteLoaderListOfficialMain);

	public static ModLoader.LoaderTask<int, DlLiteLoaderListResult> DlLiteLoaderListBmclapiLoader = new ModLoader.LoaderTask<int, DlLiteLoaderListResult>("DlLiteLoaderList Bmclapi", DlLiteLoaderListBmclapiMain);

	public static ModLoader.LoaderTask<int, DlFabricListResult> DlFabricListLoader = new ModLoader.LoaderTask<int, DlFabricListResult>("DlFabricList Main", DlFabricListMain);

	public static ModLoader.LoaderTask<int, DlFabricListResult> DlFabricListOfficialLoader = new ModLoader.LoaderTask<int, DlFabricListResult>("DlFabricList Official", DlFabricListOfficialMain);

	public static ModLoader.LoaderTask<int, DlFabricListResult> DlFabricListBmclapiLoader = new ModLoader.LoaderTask<int, DlFabricListResult>("DlFabricList Bmclapi", DlFabricListBmclapiMain);

	public static ModLoader.LoaderTask<int, List<ResourceVersion>> DlFabricApiLoader = new ModLoader.LoaderTask<int, List<ResourceVersion>>("Fabric API List Loader", [SpecialName] (ModLoader.LoaderTask<int, List<ResourceVersion>> Task) =>
	{
		Task.Output = ResourceVersion.FromProjectId("fabric-api", (ResourcePlatforms)2, LoadDependencies: false);
	});

	public static ModLoader.LoaderTask<int, List<ResourceVersion>> DlOptiFabricLoader = new ModLoader.LoaderTask<int, List<ResourceVersion>>("OptiFabric List Loader", [SpecialName] (ModLoader.LoaderTask<int, List<ResourceVersion>> Task) =>
	{
		Task.Output = ResourceVersion.FromProjectId("322385", (ResourcePlatforms)1, LoadDependencies: false);
	});

	private static bool DlPreferMojang = false;

	[SpecialName]
	private static bool _0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted_0024Init;

	public static List<int> AllDrops
	{
		get
		{
			if (_AllDrops == null)
			{
				_AllDrops = (from d in StringExtensions.Split(Settings.Get<string>("CacheDrops").ToString(), ",", true)
					select checked((int)Math.Round(ModBase.Val(d)))).ToList();
			}
			if (!_AllDrops.Any())
			{
				return null;
			}
			return _AllDrops;
		}
		set
		{
			_AllDrops = value;
			Settings.Set("CacheDrops", EnumerableExtensions.Join((IEnumerable)value, ','));
		}
	}

	public static bool DlSourcePreferMojang
	{
		get
		{
			if (Settings.Get<int>("ToolDownloadSource") != 2)
			{
				if (Settings.Get<int>("ToolDownloadSource") == 1)
				{
					return DlPreferMojang;
				}
				return false;
			}
			return true;
		}
	}

	public static bool DlVersionListPreferMojang
	{
		get
		{
			if (Settings.Get<int>("ToolDownloadVersion") != 2)
			{
				if (Settings.Get<int>("ToolDownloadVersion") == 1)
				{
					return DlPreferMojang;
				}
				return false;
			}
			return true;
		}
	}

	public static ModNet.NetFile DlClientJarGet(ModMinecraft.McInstance Instance, bool ReturnNothingOnFileUseable)
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		try
		{
			while (!string.IsNullOrEmpty(Instance.InheritName))
			{
				Instance = new ModMinecraft.McInstance(Instance.InheritName);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "获取底层继承版本失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
			ProjectData.ClearProjectError();
		}
		if (Instance.JsonObject["downloads"] == null || Instance.JsonObject["downloads"][(object)"client"] == null || Instance.JsonObject["downloads"][(object)"client"][(object)"url"] == null)
		{
			throw new Exception("底层版本 " + Instance.Name + " 中无 jar 文件下载信息");
		}
		FileChecker val = new FileChecker
		{
			MinSize = 1024L,
			ActualSize = (long)(Instance.JsonObject["downloads"][(object)"client"][(object)"size"] ?? JToken.op_Implicit(-1)),
			Hash = (string)Instance.JsonObject["downloads"][(object)"client"][(object)"sha1"]
		};
		if (ReturnNothingOnFileUseable && val.Check(Instance.PathVersion + Instance.Name + ".jar") == null)
		{
			return null;
		}
		string original = (string)Instance.JsonObject["downloads"][(object)"client"][(object)"url"];
		return new ModNet.NetFile(DlSourceLauncherOrMetaGet(original), Instance.PathVersion + Instance.Name + ".jar", val);
	}

	public static ModNet.NetFile DlClientAssetIndexGet(ModMinecraft.McInstance Instance)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		while (!string.IsNullOrEmpty(Instance.InheritName))
		{
			Instance = new ModMinecraft.McInstance(Instance.InheritName);
		}
		JToken val = ModMinecraft.McAssetsGetIndex(Instance, ReturnLegacyOnError: true, CheckURLEmpty: true);
		string localPath = ModMinecraft.McFolderSelected + "assets\\indexes\\" + val[(object)"id"].ToString() + ".json";
		Logger.Info(string.Format("版本 {0} 对应的资源文件索引为 {1}", Instance.Name, val[(object)"id"]), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
		string text = (string)(val[(object)"url"] ?? JToken.op_Implicit(""));
		if (Operators.CompareString(text, "", TextCompare: false) == 0)
		{
			return null;
		}
		return new ModNet.NetFile(DlSourceLauncherOrMetaGet(text), localPath, new FileChecker
		{
			IsJson = true
		});
	}

	public static List<ModLoader.LoaderBase> DlClientFix(ModMinecraft.McInstance Instance, bool CheckAssetsHash, bool DownloadAssetIndexInBackground)
	{
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		List<ModLoader.LoaderBase> loaders = new List<ModLoader.LoaderBase>
		{
			new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析缺失支持库文件", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				Task.Output = ModMinecraft.McLibNetFilesFromInstance(Instance);
			})
			{
				ProgressWeight = 1.0
			},
			new ModNet.LoaderDownload("下载支持库文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 15.0
			}
		};
		list.Add(new ModLoader.LoaderCombo<string>("下载支持库文件（主加载器）", loaders)
		{
			Block = false,
			Show = false,
			ProgressWeight = 16.0
		});
		if (Conversions.ToBoolean(ModMinecraft.ShouldIgnoreFileCheck(Instance)))
		{
			Logger.Warn("设置要求跳过所有 Assets 检查，文件可能不是最新的", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
		}
		else
		{
			List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
			string text = null;
			string text2 = null;
			list2.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析资源文件索引地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				try
				{
					ModNet.NetFile netFile2 = DlClientAssetIndexGet(Instance);
					if (netFile2 == null)
					{
						Task.Output = new List<ModNet.NetFile>();
						Logger.Warn($"未找到版本 {Instance.Name} 的合适的资源索引下载地址，游戏 assets 可能缺失", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
					}
					else
					{
						string text4 = ModBase.PathTemp + "Cache\\" + netFile2.LocalName + "_background";
						if (FileUtils.Exists(text4) && DateTime.Compare(FileUtils.GetInfo(text4).LastWriteTime.Date, FileUtils.GetInfo(netFile2.LocalPath).LastWriteTime.Date) > 0)
						{
							Logger.Info($"发现后台更新的资源索引缓存文件：{text4}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
							FileUtils.Move(text4, netFile2.LocalPath, false);
						}
						if (DownloadAssetIndexInBackground)
						{
							if (netFile2.Check.Check(netFile2.LocalPath) == null)
							{
								Task.Output = new List<ModNet.NetFile>();
							}
							else
							{
								Task.Output = new List<ModNet.NetFile> { netFile2 };
							}
						}
						else
						{
							text2 = netFile2.LocalPath;
							text = ModBase.PathTemp + "Cache\\" + netFile2.LocalName;
							netFile2.LocalPath = text;
							Task.Output = new List<ModNet.NetFile> { netFile2 };
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("分析资源文件索引地址失败", innerException);
				}
			})
			{
				ProgressWeight = 0.5,
				Show = false
			});
			list2.Add(new ModNet.LoaderDownload("下载资源文件索引", new List<ModNet.NetFile>())
			{
				ProgressWeight = 2.0
			});
			list2.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取资源文件列表", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				if (text != null)
				{
					FileUtils.Move(text, text2, false);
				}
				ModLoader.LoaderTask<string, List<ModNet.NetFile>> loaderTask = Task;
				ModMinecraft.McInstance instance = Instance;
				bool checkHash = CheckAssetsHash;
				ModLoader.LoaderBase ProgressFeed = Task;
				List<ModNet.NetFile> output = ModMinecraft.McAssetsFixList(instance, checkHash, ref ProgressFeed);
				Task = (ModLoader.LoaderTask<string, List<ModNet.NetFile>>)ProgressFeed;
				loaderTask.Output = output;
			})
			{
				ProgressWeight = 0.01
			});
			list2.Add(new ModNet.LoaderDownload("下载资源文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 25.0
			});
			list.Add(new ModLoader.LoaderCombo<string>("下载资源文件（主加载器）", list2)
			{
				Block = false,
				Show = false,
				ProgressWeight = 27.0
			});
			if (DownloadAssetIndexInBackground)
			{
				string text3 = null;
				string localPath = null;
				List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
				list3.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("后台分析资源文件索引地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
				{
					ModNet.NetFile netFile = DlClientAssetIndexGet(Instance);
					if (netFile == null)
					{
						Logger.Warn($"未找到版本 {Instance.Name} 的合适的资源索引下载地址，游戏 assets 可能缺失", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
						Task.Cancel();
						throw new OperationCanceledException();
					}
					text3 = netFile.LocalPath;
					localPath = ModBase.PathTemp + "Cache\\" + netFile.LocalName + "_background";
					netFile.LocalPath = localPath;
					Task.Output = new List<ModNet.NetFile> { netFile };
					DateTime date = FileUtils.GetInfo(text3).LastWriteTime.Date;
					if (FileUtils.Exists(text3) && Math.Abs((date - DateAndTime.Now.Date).TotalDays) < 1.0)
					{
						Logger.Info($"无需更新资源文件索引，上次更新于 {date}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
						Task.Cancel();
					}
				}));
				list3.Add(new ModNet.LoaderDownload("后台下载资源文件索引", new List<ModNet.NetFile>()));
				ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("后台更新资源文件索引", list3);
				Logger.Info("开始后台检查资源文件索引", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
				loaderCombo.Start();
			}
		}
		return list;
	}

	private static void DlClientListMain(ModLoader.LoaderTask<string, DlClientListResult> Loader)
	{
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		switch (Settings.Get<int>("ToolDownloadVersion"))
		{
		case 0:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListBmclapiLoader, 30),
				new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListMojangLoader, 90)
			}, Loader.IsForceRestarting);
			break;
		case 1:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListMojangLoader, 5),
				new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListBmclapiLoader, 35)
			}, Loader.IsForceRestarting);
			break;
		default:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListMojangLoader, 60),
				new KeyValuePair<ModLoader.LoaderTask<string, DlClientListResult>, int>(DlClientListBmclapiLoader, 120)
			}, Loader.IsForceRestarting);
			break;
		}
		List<int> list = new List<int>();
		foreach (JObject item in (IEnumerable<JToken>)Loader.Output.Value["versions"])
		{
			string text = item["id"].ToString();
			if (!text.Contains("-"))
			{
				int num = McVersion.VersionToDrop(text);
				if (num != 209)
				{
					list.Add(num);
				}
			}
		}
		AllDrops = (from d in list.Distinct()
			orderby d descending
			select d).ToList();
	}

	private static void DlClientListMojangMain(ModLoader.LoaderTask<string, DlClientListResult> Loader)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		long timeMs = ModBase.GetTimeMs();
		JObject val = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://launchermeta.mojang.com/mc/game/version_manifest.json", null, null, null, null, "*/*", null, RequireJson: true));
		try
		{
			if (((JContainer)(JArray)val["versions"]).Count < 200)
			{
				throw new Exception("获取到的版本列表长度不足（" + ((JToken)val).ToString() + "）");
			}
			if (!DlPreferMojang)
			{
				long num = checked(ModBase.GetTimeMs() - timeMs);
				DlPreferMojang = num < 4000;
				Logger.Info(string.Format("Mojang 官方源加载耗时：{0}ms，{1}", num, DlPreferMojang ? "可优先使用官方源" : "不优先使用官方源"), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
			}
			Loader.Output = new DlClientListResult
			{
				IsOfficial = true,
				SourceName = "Mojang 官方源",
				Value = val
			};
			if (_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted_0024Init == null)
			{
				Interlocked.CompareExchange(ref _0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted_0024Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted_0024Init, ref lockTaken);
				if (_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted_0024Init.State == 0)
				{
					_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted_0024Init.State = 2;
					_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted = false;
				}
				else if (_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted_0024Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted_0024Init);
				}
			}
			string text = (string)val["latest"][(object)"snapshot"];
			if (Settings.Get<bool>("ToolUpdateSnapshot") && Operators.CompareString(Settings.Get<string>("ToolUpdateSnapshotLast"), "", TextCompare: false) != 0 && Operators.CompareString(Settings.Get<string>("ToolUpdateSnapshotLast"), text, TextCompare: false) != 0 && !_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted)
			{
				_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted = true;
				ModMinecraft.McDownloadClientUpdateHint(text, val);
			}
			Settings.Set("ToolUpdateSnapshotLast", text ?? "Nothing");
			text = (string)val["latest"][(object)"release"];
			if (Settings.Get<bool>("ToolUpdateRelease") && Operators.CompareString(Settings.Get<string>("ToolUpdateReleaseLast"), "", TextCompare: false) != 0 && Operators.CompareString(Settings.Get<string>("ToolUpdateReleaseLast"), text, TextCompare: false) != 0 && !_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted)
			{
				_0024STATIC_0024DlClientListMojangMain_0024011151286FC2E118758_0024IsHinted = true;
				ModMinecraft.McDownloadClientUpdateHint(text, val);
			}
			Settings.Set("ToolUpdateReleaseLast", text);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("Minecraft 官方源版本列表解析失败", innerException);
		}
	}

	private static void DlClientListBmclapiMain(ModLoader.LoaderTask<string, DlClientListResult> Loader)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		JObject val = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/mc/game/version_manifest.json", null, null, null, null, "*/*", null, RequireJson: true));
		try
		{
			JArray val2 = (JArray)val["versions"];
			if (((JContainer)val2).Count < 200)
			{
				throw new Exception("获取到的版本列表长度不足（" + ((JToken)val).ToString() + "）");
			}
			if (FileUtils.Exists(ModBase.PathTemp + "Cache\\download.json"))
			{
				((JContainer)val2).Merge((object)FileUtils.ReadAsJson(ModBase.PathTemp + "Cache\\download.json", (Encoding)null, (Type)null));
			}
			if (!string.IsNullOrEmpty(Loader.Input))
			{
				string input = Loader.Input;
				if (DlClientListLoader.Output.Value != null && !((IEnumerable<JToken>)DlClientListLoader.Output.Value["versions"]).Any([SpecialName] (JToken v) => (string)v[(object)"id"] == input))
				{
					throw new Exception("BMCLAPI 源未包含目标版本 " + input);
				}
			}
			Loader.Output = new DlClientListResult
			{
				IsOfficial = false,
				SourceName = "BMCLAPI",
				Value = val
			};
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("Minecraft BMCLAPI 版本列表解析失败（" + ((JToken)val).ToString() + "）", innerException);
		}
	}

	public static object DlClientListGet(string Id)
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		object result;
		try
		{
			Id = Id.Replace("_", "-");
			if (Operators.CompareString(Id, "1.0", TextCompare: false) != 0 && StringExtensions.EndsWithF(Id, ".0", false))
			{
				Id = Strings.Left(Id, checked(Id.Length - 2));
			}
			switch (DlClientListLoader.State)
			{
			case ModBase.LoadState.Finished:
				foreach (JObject item in (IEnumerable<JToken>)DlClientListLoader.Output.Value["versions"])
				{
					JObject val = item;
					if (!((string)val["id"] == Id))
					{
						continue;
					}
					result = val["url"].ToString();
					goto end_IL_0000;
				}
				DlClientListLoader.WaitForExit(Id, null, IsForceRestart: true);
				break;
			case ModBase.LoadState.Loading:
				DlClientListLoader.WaitForExit(Id);
				break;
			case ModBase.LoadState.Waiting:
			case ModBase.LoadState.Failed:
			case ModBase.LoadState.Canceled:
				DlClientListLoader.WaitForExit(Id, null, IsForceRestart: true);
				break;
			}
			foreach (JObject item2 in (IEnumerable<JToken>)DlClientListLoader.Output.Value["versions"])
			{
				JObject val2 = item2;
				if (!((string)val2["id"] == Id))
				{
					continue;
				}
				result = val2["url"].ToString();
				goto end_IL_0000;
			}
			Logger.Warn(string.Format("未发现版本 {0} 的 json 下载地址，版本列表返回为：{1}{2}", Id, "\r\n", DlClientListLoader.Output.Value), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
			result = null;
			end_IL_0000:;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, $"获取版本 {Id} 的 json 下载地址失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModDownload.vb");
			result = null;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	private static void DlOptiFineListMain(ModLoader.LoaderTask<int, DlOptiFineListResult> Loader)
	{
		switch (Settings.Get<int>("ToolDownloadVersion"))
		{
		case 0:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListBmclapiLoader, 30),
				new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListOfficialLoader, 90)
			}, Loader.IsForceRestarting);
			break;
		case 1:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListOfficialLoader, 5),
				new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListBmclapiLoader, 35)
			}, Loader.IsForceRestarting);
			break;
		default:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListOfficialLoader, 60),
				new KeyValuePair<ModLoader.LoaderTask<int, DlOptiFineListResult>, int>(DlOptiFineListBmclapiLoader, 120)
			}, Loader.IsForceRestarting);
			break;
		}
	}

	private static void DlOptiFineListOfficialMain(ModLoader.LoaderTask<int, DlOptiFineListResult> Loader)
	{
		string text = ModNet.NetRequestByClientRetry("https://optifine.net/downloads", null, null, null, null, "*/*", null, RequireJson: false, Encoding.Default);
		if (text.Length < 200)
		{
			throw new Exception("获取到的版本列表长度不足（" + text + "）");
		}
		checked
		{
			try
			{
				List<string> list = StringExtensions.RegexSearch(text, "(?<=colForge'>)[^<]*", RegexOptions.None).ToList();
				List<string> list2 = StringExtensions.RegexSearch(text, "(?<=colDate'>)[^<]+", RegexOptions.None).ToList();
				List<string> list3 = StringExtensions.RegexSearch(text, "(?<=OptiFine_)[0-9A-Za-z_.]+(?=.jar\")", RegexOptions.None).ToList();
				if (list2.Count != list3.Count)
				{
					throw new Exception("版本与发布时间数据无法对应");
				}
				if (list.Count != list3.Count)
				{
					throw new Exception("版本与 Forge 兼容数据无法对应");
				}
				if (list2.Count < 10)
				{
					throw new Exception("获取到的版本数量不足（" + text + "）");
				}
				List<DlOptiFineListEntry> list4 = new List<DlOptiFineListEntry>();
				int num = list2.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					list3[i] = list3[i].Replace("_", " ");
					DlOptiFineListEntry dlOptiFineListEntry = new DlOptiFineListEntry();
					dlOptiFineListEntry.DisplayName = list3[i].Replace("HD U ", "").Replace(".0 ", " ");
					dlOptiFineListEntry.ReleaseTime = EnumerableExtensions.Join((IEnumerable)new string[3]
					{
						StringExtensions.Split(list2[i], ".", false)[2],
						StringExtensions.Split(list2[i], ".", false)[1],
						StringExtensions.Split(list2[i], ".", false)[0]
					}, '/');
					dlOptiFineListEntry.IsPreview = StringExtensions.ContainsIgnoreCase(list3[i], "pre");
					dlOptiFineListEntry.Inherit = StringExtensions.Split(list3[i].ToString(), " ", false)[0];
					dlOptiFineListEntry.FileName = (StringExtensions.ContainsIgnoreCase(list3[i], "pre") ? "preview_" : "") + "OptiFine_" + list3[i].Replace(" ", "_") + ".jar";
					dlOptiFineListEntry.RequiredForgeVersion = list[i].Replace("Forge ", "").Replace("#", "");
					DlOptiFineListEntry dlOptiFineListEntry2 = dlOptiFineListEntry;
					if (dlOptiFineListEntry2.RequiredForgeVersion.Contains("N/A"))
					{
						dlOptiFineListEntry2.RequiredForgeVersion = null;
					}
					dlOptiFineListEntry2.InstanceName = dlOptiFineListEntry2.Inherit + "-OptiFine_" + StringExtensions.AfterFirst(list3[i], " ", false).Replace(" ", "_");
					list4.Add(dlOptiFineListEntry2);
				}
				Loader.Output = new DlOptiFineListResult
				{
					IsOfficial = true,
					SourceName = "OptiFine 官方源",
					Value = list4
				};
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("OptiFine 官方源版本列表解析失败（" + text + "）", innerException);
			}
		}
	}

	private static void DlOptiFineListBmclapiMain(ModLoader.LoaderTask<int, DlOptiFineListResult> Loader)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		JArray val = (JArray)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/optifine/versionList", null, null, null, null, "*/*", null, RequireJson: true));
		try
		{
			List<DlOptiFineListEntry> list = new List<DlOptiFineListEntry>();
			foreach (JObject item in val)
			{
				JObject val2 = item;
				DlOptiFineListEntry dlOptiFineListEntry = new DlOptiFineListEntry
				{
					DisplayName = (val2["mcversion"].ToString() + val2["type"].ToString().Replace("HD_U", "").Replace("_", " ") + " " + val2["patch"].ToString()).Replace(".0 ", " "),
					ReleaseTime = "",
					IsPreview = StringExtensions.ContainsIgnoreCase(val2["patch"].ToString(), "pre"),
					Inherit = val2["mcversion"].ToString(),
					FileName = val2["filename"].ToString(),
					RequiredForgeVersion = (val2["forge"] ?? JToken.op_Implicit("")).ToString().Replace("Forge ", "").Replace("#", "")
				};
				if (dlOptiFineListEntry.RequiredForgeVersion.Contains("N/A"))
				{
					dlOptiFineListEntry.RequiredForgeVersion = null;
				}
				dlOptiFineListEntry.InstanceName = dlOptiFineListEntry.Inherit + "-OptiFine_" + (val2["type"].ToString() + " " + val2["patch"].ToString()).Replace(".0 ", " ").Replace(" ", "_").Replace(dlOptiFineListEntry.Inherit + "_", "");
				list.Add(dlOptiFineListEntry);
			}
			Loader.Output = new DlOptiFineListResult
			{
				IsOfficial = false,
				SourceName = "BMCLAPI",
				Value = list
			};
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("OptiFine BMCLAPI 版本列表解析失败（" + ((JToken)val).ToString() + "）", innerException);
		}
	}

	private static void DlForgeListMain(ModLoader.LoaderTask<int, DlForgeListResult> Loader)
	{
		switch (Settings.Get<int>("ToolDownloadVersion"))
		{
		case 0:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListBmclapiLoader, 30),
				new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListOfficialLoader, 90)
			}, Loader.IsForceRestarting);
			break;
		case 1:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListOfficialLoader, 5),
				new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListBmclapiLoader, 35)
			}, Loader.IsForceRestarting);
			break;
		default:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListOfficialLoader, 60),
				new KeyValuePair<ModLoader.LoaderTask<int, DlForgeListResult>, int>(DlForgeListBmclapiLoader, 120)
			}, Loader.IsForceRestarting);
			break;
		}
	}

	private static void DlForgeListOfficialMain(ModLoader.LoaderTask<int, DlForgeListResult> Loader)
	{
		string text = ModNet.NetRequestByClientRetry("https://files.minecraftforge.net/maven/net/minecraftforge/forge/index_1.2.4.html", null, null, null, null, "text/html", null, RequireJson: false, Encoding.Default, SimulateBrowserHeaders: true);
		if (text.Length < 200)
		{
			throw new Exception("获取到的版本列表长度不足（" + text + "）");
		}
		List<string> list = StringExtensions.RegexSearch(text, "(?<=a href=\"index_)[0-9.]+((_pre|[_-]snapshot[_-]?)[0-9]?)?(?=.html)", RegexOptions.None).ToList();
		list.Add("1.2.4");
		if (list.Count < 10)
		{
			throw new Exception("获取到的版本数量不足（" + text + "）");
		}
		Loader.Output = new DlForgeListResult
		{
			IsOfficial = true,
			SourceName = "Forge 官方源",
			Value = list
		};
	}

	private static void DlForgeListBmclapiMain(ModLoader.LoaderTask<int, DlForgeListResult> Loader)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		List<string> list = ((IEnumerable<JToken>)(JArray)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/forge/minecraft", null, null, null, null, "*/*", null, RequireJson: true, Encoding.Default))).Select([SpecialName] (JToken v) => v.ToString()).ToList();
		if (list.Count < 10)
		{
			throw new Exception("获取到的版本数量不足");
		}
		Loader.Output = new DlForgeListResult
		{
			IsOfficial = false,
			SourceName = "BMCLAPI",
			Value = list
		};
	}

	public static void DlForgeVersionMain(ModLoader.LoaderTask<string, List<DlForgeVersionEntry>> Loader)
	{
		ModLoader.LoaderTask<string, List<DlForgeVersionEntry>> key = new ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>("DlForgeVersion Official", DlForgeVersionOfficialMain);
		ModLoader.LoaderTask<string, List<DlForgeVersionEntry>> key2 = new ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>("DlForgeVersion Bmclapi", DlForgeVersionBmclapiMain);
		switch (Settings.Get<int>("ToolDownloadVersion"))
		{
		case 0:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key2, 30),
				new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key, 90)
			}, Loader.IsForceRestarting);
			break;
		case 1:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key, 5),
				new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key2, 35)
			}, Loader.IsForceRestarting);
			break;
		default:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key, 60),
				new KeyValuePair<ModLoader.LoaderTask<string, List<DlForgeVersionEntry>>, int>(key2, 120)
			}, Loader.IsForceRestarting);
			break;
		}
	}

	public static void DlForgeVersionOfficialMain(ModLoader.LoaderTask<string, List<DlForgeVersionEntry>> Loader)
	{
		string text;
		try
		{
			text = ModNet.NetRequestByLoader("https://files.minecraftforge.net/maven/net/minecraftforge/forge/index_" + Loader.Input.Replace("-", "_") + ".html", 45000, IsJson: false, SimulateBrowserHeaders: true);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			if (ExceptionExtensions.GetDisplay(ex2, false).Contains("(404)"))
			{
				throw new Exception("无");
			}
			throw;
		}
		if (text.Length < 1000)
		{
			throw new Exception("获取到的版本列表长度不足（" + text + "）");
		}
		List<DlForgeVersionEntry> list = new List<DlForgeVersionEntry>();
		checked
		{
			try
			{
				string[] array = StringExtensions.Split(text.Substring(0, StringExtensions.LastIndexOfF(text, "</table>", false)), "<td class=\"download-version", false);
				int num = array.Count() - 1;
				for (int i = 1; i <= num; i++)
				{
					string text2 = array[i];
					try
					{
						string text3 = StringExtensions.RegexSeek(text2, "(?<=[^(0-9)]+)[0-9\\.]+", RegexOptions.None);
						bool isRecommended = text2.Contains("fa promo-recommended");
						string input = Loader.Input;
						string text4 = StringExtensions.RegexSeek(text2, $"(?<=-{text3}-)[^-\"]+(?=-[a-z]+.[a-z]{{3}})", RegexOptions.None);
						if (string.IsNullOrWhiteSpace(text4))
						{
							text4 = null;
						}
						string[] array2 = StringExtensions.RegexSeek(text2, "(?<=\"download-time\" title=\")[^\"]+", RegexOptions.None).Split(" -:".ToCharArray());
						string releaseTime = new DateTime(Conversions.ToInteger(array2[0]), Conversions.ToInteger(array2[1]), Conversions.ToInteger(array2[2]), Conversions.ToInteger(array2[3]), Conversions.ToInteger(array2[4]), Conversions.ToInteger(array2[5]), 0, DateTimeKind.Utc).ToLocalTime().ToString("yyyy'/'MM'/'dd HH':'mm");
						string text5;
						string category;
						if (text2.Contains("classifier-installer\""))
						{
							text2 = text2.Substring(StringExtensions.IndexOfF(text2, "installer.jar", false));
							text5 = StringExtensions.RegexSeek(text2, "(?<=MD5:</strong> )[^<]+", RegexOptions.None);
							category = "installer";
							goto IL_022b;
						}
						if (text2.Contains("classifier-universal\""))
						{
							text2 = text2.Substring(StringExtensions.IndexOfF(text2, "universal.zip", false));
							text5 = StringExtensions.RegexSeek(text2, "(?<=MD5:</strong> )[^<]+", RegexOptions.None);
							category = "universal";
							goto IL_022b;
						}
						if (text2.Contains("client.zip"))
						{
							text2 = text2.Substring(StringExtensions.IndexOfF(text2, "client.zip", false));
							text5 = StringExtensions.RegexSeek(text2, "(?<=MD5:</strong> )[^<]+", RegexOptions.None);
							category = "client";
							goto IL_022b;
						}
						goto end_IL_00ba;
						IL_022b:
						list.Add(new DlForgeVersionEntry(text3, text4, input)
						{
							Category = category,
							IsRecommended = isRecommended,
							Hash = StringExtensions.ReplaceLineEndings(text5, "", false),
							ReleaseTime = releaseTime
						});
						end_IL_00ba:;
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception innerException = ex3;
						throw new Exception("Forge 官方源版本信息提取失败（" + text2 + "）", innerException);
					}
				}
			}
			catch (Exception ex4)
			{
				ProjectData.SetProjectError(ex4);
				Exception innerException2 = ex4;
				throw new Exception("Forge 官方源版本列表解析失败（" + text + "）", innerException2);
			}
			if (!list.Any())
			{
				throw new Exception("无");
			}
			Loader.Output = list;
		}
	}

	public static void DlForgeVersionBmclapiMain(ModLoader.LoaderTask<string, List<DlForgeVersionEntry>> Loader)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		JArray val = (JArray)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/forge/minecraft/" + Loader.Input.Replace("-", "_"), null, null, null, null, "*/*", null, RequireJson: true));
		List<DlForgeVersionEntry> list = new List<DlForgeVersionEntry>();
		try
		{
			string left = ModDownloadLib.McDownloadForgeRecommendedGet(Loader.Input);
			foreach (JObject item in val)
			{
				JObject val2 = item;
				string hash = null;
				string category = "unknown";
				int num = -1;
				foreach (JObject item2 in (IEnumerable<JToken>)val2["files"])
				{
					JObject val3 = item2;
					switch (val3["category"].ToString())
					{
					case "installer":
						if (Operators.CompareString(val3["format"].ToString(), "jar", TextCompare: false) == 0)
						{
							hash = (string)val3["hash"];
							category = "installer";
							num = 2;
						}
						break;
					case "universal":
						if (num <= 1 && Operators.CompareString(val3["format"].ToString(), "zip", TextCompare: false) == 0)
						{
							hash = (string)val3["hash"];
							category = "universal";
							num = 1;
						}
						break;
					case "client":
						if (num <= 0 && Operators.CompareString(val3["format"].ToString(), "zip", TextCompare: false) == 0)
						{
							hash = (string)val3["hash"];
							category = "client";
							num = 0;
						}
						break;
					}
				}
				string branch = (string)val2["branch"];
				string text = (string)val2["version"];
				DlForgeVersionEntry dlForgeVersionEntry = new DlForgeVersionEntry(text, branch, Loader.Input)
				{
					Hash = hash,
					Category = category,
					IsRecommended = (Operators.CompareString(left, text, TextCompare: false) == 0)
				};
				val2["modified"].ToString().Split('-', 'T', ':', '.', ' ', '/');
				dlForgeVersionEntry.ReleaseTime = val2["modified"].ToObject<DateTime>().ToLocalTime().ToString("yyyy'/'MM'/'dd HH':'mm");
				list.Add(dlForgeVersionEntry);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("Forge BMCLAPI 版本列表解析失败（" + ((JToken)val).ToString() + "）", innerException);
		}
		if (!list.Any())
		{
			throw new Exception("无");
		}
		Loader.Output = list;
	}

	private static void DlNeoForgeListMain(ModLoader.LoaderTask<int, DlNeoForgeListResult> Loader)
	{
		switch (Settings.Get<int>("ToolDownloadVersion"))
		{
		case 0:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListBmclapiLoader, 30),
				new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListOfficialLoader, 90)
			}, Loader.IsForceRestarting);
			break;
		case 1:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListOfficialLoader, 5),
				new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListBmclapiLoader, 35)
			}, Loader.IsForceRestarting);
			break;
		default:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListOfficialLoader, 60),
				new KeyValuePair<ModLoader.LoaderTask<int, DlNeoForgeListResult>, int>(DlNeoForgeListBmclapiLoader, 120)
			}, Loader.IsForceRestarting);
			break;
		}
	}

	private static void DlNeoForgeListOfficialMain(ModLoader.LoaderTask<int, DlNeoForgeListResult> Loader)
	{
		string text = ModNet.NetRequestByClientRetry("https://maven.neoforged.net/api/maven/versions/releases/net/neoforged/neoforge", null, null, null, null, "*/*", null, RequireJson: true);
		string text2 = ModNet.NetRequestByClientRetry("https://maven.neoforged.net/api/maven/versions/releases/net/neoforged/forge", null, null, null, null, "*/*", null, RequireJson: true);
		if (text.Length < 100 || text2.Length < 100)
		{
			throw new Exception("获取到的版本列表长度不足（" + text + "）");
		}
		try
		{
			Loader.Output = new DlNeoForgeListResult
			{
				IsOfficial = true,
				SourceName = "NeoForge 官方源",
				Value = GetNeoForgeEntries(text, text2)
			};
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("NeoForge 官方源版本列表解析失败（" + text + "\r\n\r\n" + text2 + "）", innerException);
		}
	}

	public static void DlNeoForgeListBmclapiMain(ModLoader.LoaderTask<int, DlNeoForgeListResult> Loader)
	{
		string text = ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/neoforge/meta/api/maven/details/releases/net/neoforged/neoforge", null, null, null, null, "*/*", null, RequireJson: true);
		string text2 = ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/neoforge/meta/api/maven/details/releases/net/neoforged/forge", null, null, null, null, "*/*", null, RequireJson: true);
		if (text.Length < 100 || text2.Length < 100)
		{
			throw new Exception("获取到的版本列表长度不足（" + text + "）");
		}
		try
		{
			Loader.Output = new DlNeoForgeListResult
			{
				IsOfficial = true,
				SourceName = "BMCLAPI",
				Value = GetNeoForgeEntries(text, text2)
			};
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("NeoForge BMCLAPI 版本列表解析失败（" + text + "\r\n\r\n" + text2 + "）", innerException);
		}
	}

	private static List<DlNeoForgeListEntry> GetNeoForgeEntries(string LatestJson, string LatestLegacyJson)
	{
		List<DlNeoForgeListEntry> list = (from name in StringExtensions.RegexSearch(LatestLegacyJson + LatestJson, "(?<=\")(1\\.20\\.1-)?\\d+\\.[^\\.]+\\.\\d+(\\.\\d+)?(-(beta|alpha)(\\.\\d+)?)?(\\+snapshot-\\d+)?(?=\")", RegexOptions.None)
			where Operators.CompareString(name, "47.1.82", TextCompare: false) != 0
			select new DlNeoForgeListEntry(name) into a
			orderby a descending
			select a).ToList();
		if (!list.Any())
		{
			throw new Exception("无");
		}
		return list;
	}

	private static void DlLiteLoaderListMain(ModLoader.LoaderTask<int, DlLiteLoaderListResult> Loader)
	{
		switch (Settings.Get<int>("ToolDownloadVersion"))
		{
		case 0:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListBmclapiLoader, 30),
				new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListOfficialLoader, 90)
			}, Loader.IsForceRestarting);
			break;
		case 1:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListOfficialLoader, 5),
				new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListBmclapiLoader, 35)
			}, Loader.IsForceRestarting);
			break;
		default:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListOfficialLoader, 60),
				new KeyValuePair<ModLoader.LoaderTask<int, DlLiteLoaderListResult>, int>(DlLiteLoaderListBmclapiLoader, 120)
			}, Loader.IsForceRestarting);
			break;
		}
	}

	private static void DlLiteLoaderListOfficialMain(ModLoader.LoaderTask<int, DlLiteLoaderListResult> Loader)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		JObject val = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://dl.liteloader.com/versions/versions.json", null, null, null, null, "*/*", null, RequireJson: true));
		try
		{
			JObject val2 = (JObject)val["versions"];
			List<DlLiteLoaderListEntry> list = new List<DlLiteLoaderListEntry>();
			foreach (KeyValuePair<string, JToken> item in val2)
			{
				if (!StringExtensions.StartsWithF(item.Key, "1.6", false) && !StringExtensions.StartsWithF(item.Key, "1.5", false))
				{
					JToken val3 = (item.Value[(object)"artefacts"] ?? item.Value[(object)"snapshots"])[(object)"com.mumfrey:liteloader"][(object)"latest"];
					list.Add(new DlLiteLoaderListEntry
					{
						Inherit = item.Key,
						IsLegacy = (Conversions.ToDouble(StringExtensions.Split(item.Key, ".", false)[1]) < 8.0),
						IsPreview = (Operators.CompareString(StringExtensions.Lower(val3[(object)"stream"].ToString()), "snapshot", TextCompare: false) == 0),
						FileName = "liteloader-installer-" + item.Key + ((Operators.CompareString(item.Key, "1.8", TextCompare: false) == 0 || Operators.CompareString(item.Key, "1.9", TextCompare: false) == 0) ? ".0" : "") + "-00-SNAPSHOT.jar",
						MD5 = (string)val3[(object)"md5"],
						ReleaseTime = ModBase.GetLocalTime(ModBase.GetDate((int)val3[(object)"timestamp"])).ToString("yyyy'/'MM'/'dd HH':'mm"),
						JsonToken = val3
					});
				}
			}
			Loader.Output = new DlLiteLoaderListResult
			{
				IsOfficial = true,
				SourceName = "LiteLoader 官方源",
				Value = list
			};
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("LiteLoader 官方源版本列表解析失败（" + ((JToken)val).ToString() + "）", innerException);
		}
	}

	private static void DlLiteLoaderListBmclapiMain(ModLoader.LoaderTask<int, DlLiteLoaderListResult> Loader)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		JObject val = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/maven/com/mumfrey/liteloader/versions.json", null, null, null, null, "*/*", null, RequireJson: true));
		try
		{
			JObject val2 = (JObject)val["versions"];
			List<DlLiteLoaderListEntry> list = new List<DlLiteLoaderListEntry>();
			foreach (KeyValuePair<string, JToken> item in val2)
			{
				if (!StringExtensions.StartsWithF(item.Key, "1.6", false) && !StringExtensions.StartsWithF(item.Key, "1.5", false))
				{
					JToken val3 = (item.Value[(object)"artefacts"] ?? item.Value[(object)"snapshots"])[(object)"com.mumfrey:liteloader"][(object)"latest"];
					list.Add(new DlLiteLoaderListEntry
					{
						Inherit = item.Key,
						IsLegacy = (Conversions.ToDouble(StringExtensions.Split(item.Key, ".", false)[1]) < 8.0),
						IsPreview = (Operators.CompareString(StringExtensions.Lower(val3[(object)"stream"].ToString()), "snapshot", TextCompare: false) == 0),
						FileName = "liteloader-installer-" + item.Key + ((Operators.CompareString(item.Key, "1.8", TextCompare: false) == 0 || Operators.CompareString(item.Key, "1.9", TextCompare: false) == 0) ? ".0" : "") + "-00-SNAPSHOT.jar",
						MD5 = (string)val3[(object)"md5"],
						ReleaseTime = ModBase.GetLocalTime(ModBase.GetDate((int)val3[(object)"timestamp"])).ToString("yyyy'/'MM'/'dd HH':'mm"),
						JsonToken = val3
					});
				}
			}
			Loader.Output = new DlLiteLoaderListResult
			{
				IsOfficial = false,
				SourceName = "BMCLAPI",
				Value = list
			};
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("LiteLoader BMCLAPI 版本列表解析失败（" + ((JToken)val).ToString() + "）", innerException);
		}
	}

	private static void DlFabricListMain(ModLoader.LoaderTask<int, DlFabricListResult> Loader)
	{
		switch (Settings.Get<int>("ToolDownloadVersion"))
		{
		case 0:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListBmclapiLoader, 30),
				new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListOfficialLoader, 90)
			}, Loader.IsForceRestarting);
			break;
		case 1:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListOfficialLoader, 5),
				new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListBmclapiLoader, 35)
			}, Loader.IsForceRestarting);
			break;
		default:
			DlSourceLoader(Loader, new List<KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>>
			{
				new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListOfficialLoader, 60),
				new KeyValuePair<ModLoader.LoaderTask<int, DlFabricListResult>, int>(DlFabricListBmclapiLoader, 120)
			}, Loader.IsForceRestarting);
			break;
		}
	}

	private static void DlFabricListOfficialMain(ModLoader.LoaderTask<int, DlFabricListResult> Loader)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		JObject val = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://meta.fabricmc.net/v2/versions", null, null, null, null, "*/*", null, RequireJson: true));
		try
		{
			DlFabricListResult dlFabricListResult = default(DlFabricListResult);
			dlFabricListResult.IsOfficial = true;
			dlFabricListResult.SourceName = "Fabric 官方源";
			dlFabricListResult.Value = val;
			DlFabricListResult output = dlFabricListResult;
			if (output.Value["game"] == null || output.Value["loader"] == null || output.Value["installer"] == null)
			{
				throw new Exception("获取到的列表缺乏必要项");
			}
			Loader.Output = output;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("Fabric 官方源版本列表解析失败（" + ((JToken)val).ToString() + "）", innerException);
		}
	}

	private static void DlFabricListBmclapiMain(ModLoader.LoaderTask<int, DlFabricListResult> Loader)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		JObject val = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/fabric-meta/v2/versions", null, null, null, null, "*/*", null, RequireJson: true));
		try
		{
			DlFabricListResult dlFabricListResult = default(DlFabricListResult);
			dlFabricListResult.IsOfficial = false;
			dlFabricListResult.SourceName = "BMCLAPI";
			dlFabricListResult.Value = val;
			DlFabricListResult output = dlFabricListResult;
			if (output.Value["game"] == null || output.Value["loader"] == null || output.Value["installer"] == null)
			{
				throw new Exception("获取到的列表缺乏必要项");
			}
			Loader.Output = output;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new Exception("Fabric BMCLAPI 版本列表解析失败（" + ((JToken)val).ToString() + "）", innerException);
		}
	}

	public static JToken DlModRequest(string Url, HttpMethod Method = null, string Content = null, string ContentType = null)
	{
		List<(string, int)> list = new List<(string, int)>();
		string text = DlSourceModGet(Url);
		if (Operators.CompareString(text, Url, TextCompare: false) != 0)
		{
			switch (Settings.Get<int>("ToolDownloadMod"))
			{
			case 0:
				list.Add((text, 10));
				list.Add((text, 10));
				list.Add((Url, 30));
				break;
			case 1:
				list.Add((Url, Url.Contains("modrinth") ? 20 : 10));
				list.Add((text, 10));
				list.Add((Url, 30));
				list.Add((text, 30));
				break;
			default:
				list.Add((Url, Url.Contains("modrinth") ? 20 : 10));
				list.Add((Url, 30));
				list.Add((text, 30));
				break;
			}
		}
		else
		{
			list.Add((Url, Url.Contains("modrinth") ? 20 : 10));
			list.Add((Url, 30));
		}
		List<Exception> list2 = new List<Exception>();
		foreach (var item2 in list)
		{
			try
			{
				return StringExtensions.DeserializeJson(ModNet.NetRequestByClient(item2.Item1, Method, Content, ContentType, "*/*", checked(item2.Item2 * 1000), null, RequireJson: true, Encoding.UTF8));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception item = ex;
				list2.Add(item);
				ProjectData.ClearProjectError();
			}
		}
		throw new AggregateException(list2);
	}

	public static IEnumerable<string> DlSourceOrder(IEnumerable<string> OfficialUrls, IEnumerable<string> MirrorUrls)
	{
		if (!DlSourcePreferMojang)
		{
			return MirrorUrls.Union(OfficialUrls);
		}
		return OfficialUrls.Union(MirrorUrls);
	}

	public static IEnumerable<string> DlVersionListOrder(IEnumerable<string> OfficialUrls, IEnumerable<string> MirrorUrls)
	{
		if (!DlVersionListPreferMojang)
		{
			return MirrorUrls.Union(OfficialUrls);
		}
		return OfficialUrls.Union(MirrorUrls);
	}

	public static IEnumerable<string> DlSourceAssetsGet(string Original)
	{
		Original = Original.Replace("http://resources.download.minecraft.net", "https://resources.download.minecraft.net");
		return DlSourceOrder(new string[1] { Original }, new string[1] { Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/assets").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/assets").Replace("https://resources.download.minecraft.net", "https://bmclapi2.bangbang93.com/assets") });
	}

	public static IEnumerable<string> DlSourceLibraryGet(string Original)
	{
		if (new string[3] { "minecraftforge", "fabricmc", "neoforged" }.Any([SpecialName] (string k) => Original.Contains(k)))
		{
			return new string[2]
			{
				Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/maven").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/maven").Replace("https://libraries.minecraft.net", "https://bmclapi2.bangbang93.com/maven"),
				Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/libraries").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/libraries").Replace("https://libraries.minecraft.net", "https://bmclapi2.bangbang93.com/libraries")
			};
		}
		return DlSourceOrder(new string[1] { Original }, new string[2]
		{
			Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/maven").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/maven").Replace("https://libraries.minecraft.net", "https://bmclapi2.bangbang93.com/maven"),
			Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com/libraries").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com/libraries").Replace("https://libraries.minecraft.net", "https://bmclapi2.bangbang93.com/libraries")
		});
	}

	public static IEnumerable<string> DlSourceLauncherOrMetaGet(string Original)
	{
		if (Original == null)
		{
			throw new Exception("无对应的 json 下载地址");
		}
		return DlSourceOrder(new string[1] { Original }, new string[1] { Original.Replace("https://piston-data.mojang.com", "https://bmclapi2.bangbang93.com").Replace("https://piston-meta.mojang.com", "https://bmclapi2.bangbang93.com").Replace("https://launcher.mojang.com", "https://bmclapi2.bangbang93.com")
			.Replace("https://launchermeta.mojang.com", "https://bmclapi2.bangbang93.com") });
	}

	public static string DlSourceModGet(string Original)
	{
		return Original.Replace("api.modrinth.com", "mod.mcimirror.top/modrinth").Replace("staging-api.modrinth.com", "mod.mcimirror.top/modrinth").Replace("cdn.modrinth.com", "mod.mcimirror.top")
			.Replace("api.curseforge.com", "mod.mcimirror.top/curseforge")
			.Replace("edge.forgecdn.net", "mod.mcimirror.top")
			.Replace("mediafilez.forgecdn.net", "mod.mcimirror.top")
			.Replace("media.forgecdn.net", "mod.mcimirror.top");
	}

	private static void DlSourceLoader<InputType, OutputType>(ModLoader.LoaderTask<InputType, OutputType> MainLoader, List<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>> LoaderList, bool IsForceRestart = false)
	{
		int num = 0;
		checked
		{
			do
			{
				bool flag = true;
				foreach (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> Loader in LoaderList)
				{
					if (num == 0)
					{
						if (IsForceRestart || ((Loader.Key.Input == null) ^ (MainLoader.Input == null)))
						{
							continue;
						}
						if (Loader.Key.Input != null)
						{
							ref InputType reference = ref Loader.Key.Input;
							InputType val = default(InputType);
							if (val == null)
							{
								val = reference;
								reference = ref val;
							}
							if (!reference.Equals(MainLoader.Input))
							{
								continue;
							}
						}
					}
					if (Loader.Key.State != ModBase.LoadState.Failed)
					{
						flag = false;
					}
					if (Loader.Key.State == ModBase.LoadState.Finished)
					{
						MainLoader.Output = Loader.Key.Output;
						DlSourceLoaderCancel(LoaderList);
						return;
					}
					if (flag && num < Loader.Value * 100)
					{
						num = Loader.Value * 100;
					}
				}
				if (num == 0)
				{
					LoaderList.First().Key.Start(MainLoader.Input, IsForceRestart);
					foreach (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> item in LoaderList.Skip(1))
					{
						item.Key.State = ModBase.LoadState.Waiting;
					}
				}
				int num2 = LoaderList.Count - 1;
				for (int i = 0; i <= num2; i++)
				{
					if (num != LoaderList[i].Value * 100)
					{
						continue;
					}
					if (i < LoaderList.Count - 1 && !LoaderList.All([SpecialName] (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> l) => l.Key.State == ModBase.LoadState.Failed))
					{
						LoaderList[i + 1].Key.Start(MainLoader.Input, IsForceRestart);
						break;
					}
					Exception ex = null;
					int num3 = LoaderList.Count - 1;
					for (int j = 0; j <= num3; j++)
					{
						LoaderList[j].Key.Input = default(InputType);
						if (LoaderList[j].Key.Error != null && (ex == null || Operators.CompareString(LoaderList[j].Key.Error.Message, "无", TextCompare: false) == 0))
						{
							ex = LoaderList[j].Key.Error;
						}
					}
					if (ex == null)
					{
						ex = new TimeoutException("下载源连接超时");
					}
					DlSourceLoaderCancel(LoaderList);
					throw ex;
				}
				Thread.Sleep(10);
				num++;
			}
			while (!MainLoader.IsCanceled);
			DlSourceLoaderCancel(LoaderList);
		}
	}

	private static void DlSourceLoaderCancel<InputType, OutputType>(List<KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int>> LoaderList)
	{
		foreach (KeyValuePair<ModLoader.LoaderTask<InputType, OutputType>, int> Loader in LoaderList)
		{
			if (Loader.Key.State == ModBase.LoadState.Loading)
			{
				Loader.Key.Cancel();
			}
		}
	}
}
