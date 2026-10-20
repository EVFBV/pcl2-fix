using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCLCS;

namespace PCL;

public class ResourceProject
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__29_002D0
	{
		public bool _0024VB_0024Local_ShowMcVersionDesc;

		public bool _0024VB_0024Local_ShowLoaderDesc;

		public string _0024VB_0024Local_ModLoaderDescriptionPart;

		public string _0024VB_0024Local_GameVersionDescription;

		public string _0024VB_0024Local_ModLoaderDescriptionFull;

		public ResourceProject _0024VB_0024Me;

		public _Closure_0024__29_002D0(_Closure_0024__29_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_ShowMcVersionDesc = arg0._0024VB_0024Local_ShowMcVersionDesc;
				_0024VB_0024Local_ShowLoaderDesc = arg0._0024VB_0024Local_ShowLoaderDesc;
				_0024VB_0024Local_ModLoaderDescriptionPart = arg0._0024VB_0024Local_ModLoaderDescriptionPart;
				_0024VB_0024Local_GameVersionDescription = arg0._0024VB_0024Local_GameVersionDescription;
				_0024VB_0024Local_ModLoaderDescriptionFull = arg0._0024VB_0024Local_ModLoaderDescriptionFull;
			}
		}

		[SpecialName]
		internal MyResourceItem _Lambda_0024__0()
		{
			//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
			MyResourceItem myResourceItem = new MyResourceItem
			{
				Tag = _0024VB_0024Me
			};
			_0024VB_0024Me.ApplyLogoToMyImage(myResourceItem.PathLogo);
			(string, string) controlTitle = _0024VB_0024Me.GetControlTitle(HasModLoaderDescription: true);
			(myResourceItem.Title, _) = controlTitle;
			if (Operators.CompareString(controlTitle.Item2, "", TextCompare: false) == 0)
			{
				((StackPanel)myResourceItem.LabTitleRaw.Parent).Children.Remove(myResourceItem.LabTitleRaw);
			}
			else
			{
				myResourceItem.SubTitle = controlTitle.Item2;
			}
			myResourceItem.Tags = _0024VB_0024Me.Tags;
			myResourceItem.Description = StringExtensions.ReplaceLineEndings(_0024VB_0024Me.Description, "", false);
			if (!_0024VB_0024Local_ShowMcVersionDesc && !_0024VB_0024Local_ShowLoaderDesc)
			{
				((Grid)myResourceItem.PathVersion.Parent).Children.Remove(myResourceItem.PathVersion);
				((Grid)myResourceItem.LabVersion.Parent).Children.Remove(myResourceItem.LabVersion);
				myResourceItem.ColumnVersion1.Width = new GridLength(0.0);
				myResourceItem.ColumnVersion2.MaxWidth = 0.0;
				myResourceItem.ColumnVersion3.Width = new GridLength(0.0);
			}
			else if (_0024VB_0024Local_ShowMcVersionDesc && _0024VB_0024Local_ShowLoaderDesc)
			{
				myResourceItem.LabVersion.Text = ((Operators.CompareString(_0024VB_0024Local_ModLoaderDescriptionPart, "", TextCompare: false) == 0) ? "" : (_0024VB_0024Local_ModLoaderDescriptionPart + " ")) + _0024VB_0024Local_GameVersionDescription;
			}
			else if (_0024VB_0024Local_ShowMcVersionDesc)
			{
				myResourceItem.LabVersion.Text = _0024VB_0024Local_GameVersionDescription;
			}
			else
			{
				myResourceItem.LabVersion.Text = _0024VB_0024Local_ModLoaderDescriptionFull;
			}
			TextBlock labSource = myResourceItem.LabSource;
			ResourcePlatforms platform = _0024VB_0024Me.Platform;
			labSource.Text = ((Enum)(ResourcePlatforms)(ref platform)).ToString();
			myResourceItem.LabTime.Text = StringUtils.FormatTimeSpan(_0024VB_0024Me.LastUpdate - DateTime.Now, true);
			myResourceItem.LabDownload.Text = Conversions.ToString((_0024VB_0024Me.DownloadCount > 100000000) ? (Conversions.ToString(Math.Round((double)_0024VB_0024Me.DownloadCount / 100000000.0, 2)) + " 亿") : ((_0024VB_0024Me.DownloadCount > 100000) ? (Conversions.ToString(Math.Floor((double)_0024VB_0024Me.DownloadCount / 10000.0)) + " 万") : ((object)_0024VB_0024Me.DownloadCount)));
			return myResourceItem;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__31_002D0
	{
		public string _0024VB_0024Local_LowerName;

		public _Closure_0024__31_002D0(_Closure_0024__31_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_LowerName = arg0._0024VB_0024Local_LowerName;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__2(string l)
		{
			return _0024VB_0024Local_LowerName.Contains(l);
		}
	}

	public readonly ResourcePlatforms Platform;

	public readonly ResourceTypes Types;

	public readonly string Slug;

	public readonly string Id;

	public readonly List<int> CurseForgeFileIds;

	public readonly string RawName;

	public readonly string Description;

	public readonly string Website;

	public readonly DateTime LastUpdate;

	public readonly int DownloadCount;

	public readonly ModLoaders ModLoaders;

	public readonly List<string> Tags;

	public string LogoUrl;

	public readonly List<int> Drops;

	public readonly List<string> UnsafeGameVersions;

	private bool LoadedDatabase;

	private WikiEntry _WikiEntry;

	protected internal static Dictionary<string, ResourceProject> Cache = new Dictionary<string, ResourceProject>();

	private WikiEntry WikiEntry
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			if (!LoadedDatabase)
			{
				LoadedDatabase = true;
				if (EnumUtils.HasFlagF<ResourceTypes>(Types, (ResourceTypes)1) || EnumUtils.HasFlagF<ResourceTypes>(Types, (ResourceTypes)16))
				{
					_WikiEntry = WikiEntry.All.Value.FirstOrDefault([SpecialName] (WikiEntry c) => Operators.CompareString(EnumerableExtensions.GetOrDefault<ResourcePlatforms, string>((IDictionary<ResourcePlatforms, string>)c.Slugs, Platform, (string)null), Slug, TextCompare: false) == 0);
				}
			}
			return _WikiEntry;
		}
		set
		{
			LoadedDatabase = true;
			_WikiEntry = value;
		}
	}

	public int WikiId
	{
		get
		{
			if (WikiEntry != null)
			{
				return WikiEntry.Id;
			}
			return 0;
		}
	}

	public string TranslatedName => WikiEntry?.ChineseName ?? RawName;

	public ResourceProject(JObject Data)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Invalid comparison between Unknown and I4
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_01a6: Expected O, but got Unknown
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fda: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Expected O, but got Unknown
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_1008: Expected O, but got Unknown
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Expected O, but got Unknown
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_10eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1118: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1106: Unknown result type (might be due to invalid IL or missing references)
		//IL_11bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec7: Invalid comparison between Unknown and I4
		//IL_1eca: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc6: Invalid comparison between Unknown and I4
		//IL_1b9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba5: Invalid comparison between Unknown and I4
		//IL_1a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b84: Invalid comparison between Unknown and I4
		ModLoaders = (ModLoaders)0;
		LogoUrl = null;
		LoadedDatabase = false;
		_WikiEntry = null;
		if (Data.ContainsKey("Tags"))
		{
			Platform = (ResourcePlatforms)Data["Platform"].ToObject<int>();
			Types = (ResourceTypes)Data["Types"].ToObject<int>();
			Slug = (string)Data["Slug"];
			Id = (string)Data["Id"];
			if (Data.ContainsKey("CurseForgeFileIds"))
			{
				CurseForgeFileIds = ((IEnumerable<JToken>)(JArray)Data["CurseForgeFileIds"]).Select([SpecialName] (JToken t) => t.ToObject<int>()).ToList();
			}
			RawName = (string)Data["RawName"];
			Description = (string)Data["Description"];
			Website = (string)Data["Website"];
			LastUpdate = (DateTime)Data["LastUpdate"];
			DownloadCount = (int)Data["DownloadCount"];
			if (Data.ContainsKey("ModLoaders"))
			{
				ModLoaders = (ModLoaders)Data["ModLoaders"].ToObject<int>();
			}
			Tags = ((IEnumerable<JToken>)(JArray)Data["Tags"]).Select([SpecialName] (JToken t) => t.ToString()).ToList();
			if (Data.ContainsKey("LogoUrl"))
			{
				LogoUrl = (string)Data["LogoUrl"];
			}
			if (Data.ContainsKey("Drops"))
			{
				Drops = ((IEnumerable<JToken>)(JArray)Data["Drops"]).Select([SpecialName] (JToken t) => t.ToObject<int>()).ToList();
			}
			else
			{
				Drops = new List<int>();
			}
		}
		else
		{
			Platform = (ResourcePlatforms)(Data.ContainsKey("summary") ? 1 : 2);
			if ((int)Platform == 1)
			{
				Id = (string)Data["id"];
				Slug = (string)Data["slug"];
				RawName = (string)Data["name"];
				Description = (string)Data["summary"];
				Website = Data["links"][(object)"websiteUrl"].ToString().TrimEnd('/');
				LastUpdate = (DateTime)Data["dateReleased"];
				DownloadCount = (int)Data["downloadCount"];
				if (((IEnumerable<JToken>)Data["logo"]).Count() > 0)
				{
					if (Data["logo"][(object)"thumbnailUrl"] == null || (string)Data["logo"][(object)"thumbnailUrl"] == "")
					{
						LogoUrl = (string)Data["logo"][(object)"url"];
					}
					else
					{
						LogoUrl = (string)Data["logo"][(object)"thumbnailUrl"];
					}
				}
				if (Operators.CompareString(LogoUrl, "", TextCompare: false) == 0)
				{
					LogoUrl = null;
				}
				if (Website.Contains("/mc-mods/") || Website.Contains("/mod/"))
				{
					Types = (ResourceTypes)1;
				}
				else if (Website.Contains("/modpacks/"))
				{
					Types = (ResourceTypes)2;
				}
				else if (Website.Contains("/resourcepacks/"))
				{
					Types = (ResourceTypes)4;
				}
				else if (Website.Contains("/texture-packs/"))
				{
					Types = (ResourceTypes)4;
				}
				else if (Website.Contains("/shaders/"))
				{
					Types = (ResourceTypes)8;
				}
				else
				{
					Types = (ResourceTypes)16;
				}
				ModLoaders = (ModLoaders)0;
				List<KeyValuePair<int, List<string>>> list = new List<KeyValuePair<int, List<string>>>();
				foreach (JToken item in (IEnumerable<JToken>)(((object)Data["latestFiles"]) ?? ((object)new JArray())))
				{
					ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)item, Types);
					if (resourceVersion.DownloadAvailable)
					{
						ModLoaders |= resourceVersion.ModLoaders;
						List<string> list2 = item[(object)"gameVersions"].ToObject<List<string>>();
						if (list2.Any([SpecialName] (string v) => McVersion.IsStandardFormat(v)))
						{
							list.Add(new KeyValuePair<int, List<string>>((int)item[(object)"id"], list2));
						}
					}
				}
				foreach (JToken item2 in (IEnumerable<JToken>)(((object)Data["latestFilesIndexes"]) ?? ((object)new JArray())))
				{
					if (McVersion.IsStandardFormat((string)item2[(object)"gameVersion"]))
					{
						list.Add(new KeyValuePair<int, List<string>>((int)item2[(object)"fileId"], new string[1] { item2[(object)"gameVersion"].ToString() }.ToList()));
					}
				}
				CurseForgeFileIds = list.Select([SpecialName] (KeyValuePair<int, List<string>> f) => f.Key).Distinct().ToList();
				Drops = (from v in (from v in list.SelectMany([SpecialName] (KeyValuePair<int, List<string>> f) => f.Value)
						select McVersion.VersionToDrop(v) into v
						where v != 209
						select v).Distinct()
					orderby v descending
					select v).ToList();
				Tags = new List<string>();
				using IEnumerator<int> enumerator3 = (from c in ((IEnumerable<JToken>)(((object)Data["categories"]) ?? ((object)new JArray()))).Select([SpecialName] (JToken t) => (int)t[(object)"id"]).Distinct()
					orderby c descending
					select c).GetEnumerator();
				while (enumerator3.MoveNext())
				{
					switch (enumerator3.Current)
					{
					case 406:
						Tags.Add("世界元素");
						break;
					case 407:
						Tags.Add("生物群系");
						break;
					case 410:
						Tags.Add("维度");
						break;
					case 408:
						Tags.Add("矿物/资源");
						break;
					case 409:
						Tags.Add("天然结构");
						break;
					case 412:
						Tags.Add("科技");
						break;
					case 415:
						Tags.Add("管道/物流");
						break;
					case 4843:
						Tags.Add("自动化");
						break;
					case 417:
						Tags.Add("能源");
						break;
					case 4558:
						Tags.Add("红石");
						break;
					case 436:
						Tags.Add("食物/烹饪");
						break;
					case 416:
						Tags.Add("农业");
						break;
					case 414:
						Tags.Add("运输");
						break;
					case 420:
						Tags.Add("仓储");
						break;
					case 419:
						Tags.Add("魔法");
						break;
					case 422:
						Tags.Add("冒险");
						break;
					case 424:
						Tags.Add("装饰");
						break;
					case 411:
						Tags.Add("生物");
						break;
					case 434:
						Tags.Add("装备");
						break;
					case 6814:
						Tags.Add("性能优化");
						break;
					case 9026:
						Tags.Add("创造模式");
						break;
					case 423:
						Tags.Add("信息显示");
						break;
					case 435:
						Tags.Add("服务器");
						break;
					case 5191:
						Tags.Add("改良");
						break;
					case 421:
						Tags.Add("支持库");
						break;
					case 4484:
						Tags.Add("多人");
						break;
					case 4479:
						Tags.Add("硬核");
						break;
					case 4483:
						Tags.Add("战斗");
						break;
					case 4478:
						Tags.Add("任务");
						break;
					case 4472:
						Tags.Add("科技");
						break;
					case 4473:
						Tags.Add("魔法");
						break;
					case 4475:
						Tags.Add("冒险");
						break;
					case 4476:
						Tags.Add("探索");
						break;
					case 4477:
						Tags.Add("小游戏");
						break;
					case 4474:
						Tags.Add("科幻");
						break;
					case 4736:
						Tags.Add("空岛");
						break;
					case 5128:
						Tags.Add("原版改良");
						break;
					case 4487:
						Tags.Add("FTB");
						break;
					case 4480:
						Tags.Add("基于地图");
						break;
					case 4481:
						Tags.Add("轻量");
						break;
					case 4482:
						Tags.Add("大型");
						break;
					case 403:
						Tags.Add("原版风");
						break;
					case 400:
						Tags.Add("写实风");
						break;
					case 401:
						Tags.Add("现代风");
						break;
					case 402:
						Tags.Add("中世纪");
						break;
					case 399:
						Tags.Add("蒸汽朋克");
						break;
					case 5244:
						Tags.Add("含字体");
						break;
					case 404:
						Tags.Add("动态效果");
						break;
					case 4465:
						Tags.Add("兼容 Mod");
						break;
					case 393:
						Tags.Add("16x");
						break;
					case 394:
						Tags.Add("32x");
						break;
					case 395:
						Tags.Add("64x");
						break;
					case 396:
						Tags.Add("128x");
						break;
					case 397:
						Tags.Add("256x");
						break;
					case 398:
						Tags.Add("超高清");
						break;
					case 5193:
						Tags.Add("数据包");
						break;
					case 6553:
						Tags.Add("写实风");
						break;
					case 6554:
						Tags.Add("幻想风");
						break;
					case 6555:
						Tags.Add("原版风");
						break;
					case 6948:
						Tags.Add("冒险");
						break;
					case 6949:
						Tags.Add("幻想");
						break;
					case 6950:
						Tags.Add("支持库");
						break;
					case 6952:
						Tags.Add("魔法");
						break;
					case 6946:
						Tags.Add("Mod 相关");
						break;
					case 6951:
						Tags.Add("科技");
						break;
					case 6953:
						Tags.Add("实用");
						break;
					}
				}
			}
			else
			{
				Id = (string)(Data["project_id"] ?? Data["id"]);
				Slug = (string)Data["slug"];
				RawName = (string)Data["title"];
				Description = (string)Data["description"];
				LastUpdate = (DateTime)(Data["date_modified"] ?? Data["updated"]);
				DownloadCount = (int)Data["downloads"];
				LogoUrl = (string)Data["icon_url"];
				if (Operators.CompareString(LogoUrl, "", TextCompare: false) == 0)
				{
					LogoUrl = null;
				}
				Website = string.Format("https://modrinth.com/{0}/{1}", Data["project_type"], Slug);
				_003F val = (JArray)(Data["game_versions"] ?? Data["versions"]);
				if ((int)val == 0)
				{
					val = new JArray();
				}
				UnsafeGameVersions = ((IEnumerable<JToken>)val).Select([SpecialName] (JToken v) => v.ToString()).Distinct().ToList();
				Drops = (from v in (from v in UnsafeGameVersions
						select McVersion.VersionToDrop(v) into v
						where v != 209
						select v).Distinct()
					orderby v descending
					select v).ToList();
				switch (Data["project_type"].ToString())
				{
				case "modpack":
					Types = (ResourceTypes)2;
					break;
				case "resourcepack":
					Types = (ResourceTypes)4;
					break;
				case "shader":
					Types = (ResourceTypes)8;
					break;
				default:
					Types = (ResourceTypes)1;
					break;
				}
				Tags = new List<string>();
				ModLoaders = (ModLoaders)0;
				foreach (string item3 in (((IEnumerable<JToken>)Data["loaders"])?.Select([SpecialName] (JToken t) => t.ToString()) ?? new string[0]).Concat(((IEnumerable<JToken>)Data["categories"]).Select([SpecialName] (JToken t) => t.ToString())))
				{
					foreach (ModLoaders allFlag in EnumUtils.GetAllFlags<ModLoaders>())
					{
						ModLoaders current4 = allFlag;
						if (Operators.CompareString(item3, StringExtensions.Lower(((Enum)(ModLoaders)(ref current4)).ToString()), TextCompare: false) == 0)
						{
							ModLoaders |= current4;
							break;
						}
					}
				}
				using (IEnumerator<string> enumerator6 = ((IEnumerable<JToken>)Data["categories"]).Select([SpecialName] (JToken t) => t.ToString()).GetEnumerator())
				{
					while (enumerator6.MoveNext())
					{
						switch (enumerator6.Current)
						{
						case "datapack":
							Types = (ResourceTypes)16;
							break;
						case "technology":
							Tags.Add("科技");
							break;
						case "magic":
							Tags.Add("魔法");
							break;
						case "adventure":
							Tags.Add("冒险");
							break;
						case "utility":
							Tags.Add("实用");
							break;
						case "optimization":
							Tags.Add("性能优化");
							break;
						case "vanilla-like":
							Tags.Add("原版风");
							break;
						case "realistic":
							Tags.Add("写实风");
							break;
						case "worldgen":
							Tags.Add("世界元素");
							break;
						case "food":
							Tags.Add("食物/烹饪");
							break;
						case "game-mechanics":
							Tags.Add("游戏机制");
							break;
						case "transportation":
							Tags.Add("运输");
							break;
						case "storage":
							Tags.Add("仓储");
							break;
						case "decoration":
							if ((int)Types != 4)
							{
								Tags.Add("装饰");
							}
							break;
						case "mobs":
							if ((int)Types != 4)
							{
								Tags.Add("生物");
							}
							break;
						case "equipment":
							if ((int)Types != 4)
							{
								Tags.Add("装备");
							}
							break;
						case "social":
							Tags.Add("服务器");
							break;
						case "library":
							Tags.Add("支持库");
							break;
						case "multiplayer":
							Tags.Add("多人");
							break;
						case "challenging":
							Tags.Add("硬核");
							break;
						case "combat":
							Tags.Add("战斗");
							break;
						case "quests":
							Tags.Add("任务");
							break;
						case "kitchen-sink":
							Tags.Add("水槽包");
							break;
						case "lightweight":
							Tags.Add("轻量");
							break;
						case "simplistic":
							Tags.Add("简洁");
							break;
						case "tweaks":
							Tags.Add("改良");
							break;
						case "8x-":
							Tags.Add("极简");
							break;
						case "16x":
							Tags.Add("16x");
							break;
						case "32x":
							Tags.Add("32x");
							break;
						case "48x":
							Tags.Add("48x");
							break;
						case "64x":
							Tags.Add("64x");
							break;
						case "128x":
							Tags.Add("128x");
							break;
						case "256x":
							Tags.Add("256x");
							break;
						case "512x+":
							Tags.Add("超高清");
							break;
						case "audio":
							Tags.Add("含声音");
							break;
						case "fonts":
							Tags.Add("含字体");
							break;
						case "models":
							Tags.Add("含模型");
							break;
						case "gui":
							Tags.Add("含 UI");
							break;
						case "locale":
							Tags.Add("含语言");
							break;
						case "core-shaders":
							Tags.Add("核心着色器");
							break;
						case "modded":
							Tags.Add("兼容 Mod");
							break;
						case "fantasy":
							Tags.Add("幻想风");
							break;
						case "semi-realistic":
							Tags.Add("半写实风");
							break;
						case "cartoon":
							Tags.Add("卡通风");
							break;
						case "colored-lighting":
							Tags.Add("彩色光照");
							break;
						case "path-tracing":
							Tags.Add("路径追踪");
							break;
						case "pbr":
							Tags.Add("PBR");
							break;
						case "reflections":
							Tags.Add("反射");
							break;
						case "iris":
							Tags.Add("Iris");
							break;
						case "optifine":
							Tags.Add("OptiFine");
							break;
						case "vanilla":
							Tags.Add("原版可用");
							break;
						}
					}
				}
				if ((int)Types == 16 && (int)ModLoaders != 0)
				{
					Types = (ResourceTypes)17;
				}
			}
			if (!Tags.Any())
			{
				Tags.Add("其他");
			}
			Tags.Sort();
		}
		Cache[Id] = this;
	}

	public static ResourceProject FromProjectId(string ProjectId, ResourcePlatforms Platform)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Invalid comparison between Unknown and I4
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		if (!EnumerableExtensions.IsSingle<ResourcePlatforms>(EnumerableExtensions.Flags<ResourcePlatforms>(Platform)))
		{
			throw new ArgumentException($"必须指定单一平台，当前值为 {Platform}");
		}
		if (Cache.ContainsKey(ProjectId))
		{
			return Cache[ProjectId];
		}
		if ((int)Platform == 1)
		{
			return new ResourceProject((JObject)ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/" + ProjectId)[(object)"data"]);
		}
		return new ResourceProject((JObject)ModDownload.DlModRequest("https://api.modrinth.com/v2/project/" + ProjectId));
	}

	public static IEnumerable<ResourceProject> FromProjectIds(IEnumerable<string> ProjectIds, ResourcePlatforms Platform)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		if (!EnumerableExtensions.IsSingle<ResourcePlatforms>(EnumerableExtensions.Flags<ResourcePlatforms>(Platform)))
		{
			throw new ArgumentException($"必须指定单一平台，当前值为 {Platform}");
		}
		List<string> list = new List<string>();
		foreach (string ProjectId in ProjectIds)
		{
			if (Cache.ContainsKey(ProjectId))
			{
				yield return Cache[ProjectId];
			}
			else
			{
				list.Add(ProjectId);
			}
		}
		if (!list.Any())
		{
			yield break;
		}
		JArray val = ((!EnumUtils.HasFlagF<ResourcePlatforms>(Platform, (ResourcePlatforms)1)) ? ((JArray)ModDownload.DlModRequest(string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", EnumerableExtensions.Join((IEnumerable)list, "\",\"")))) : ((JArray)ModDownload.DlModRequest("https://api.curseforge.com/v1/mods", HttpMethod.Post, "{\"modIds\": [" + EnumerableExtensions.Join((IEnumerable)list, ',') + "]}", "application/json")[(object)"data"]));
		foreach (JToken item in val)
		{
			yield return new ResourceProject((JObject)item);
		}
	}

	public JObject ToJson()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected I4, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected I4, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected I4, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		JObject val = new JObject();
		val["Platform"] = JToken.op_Implicit((int)Platform);
		val["Types"] = JToken.op_Implicit((int)Types);
		val["Slug"] = JToken.op_Implicit(Slug);
		val["Id"] = JToken.op_Implicit(Id);
		if (CurseForgeFileIds != null)
		{
			val["CurseForgeFileIds"] = (JToken)new JArray((object)CurseForgeFileIds);
		}
		val["RawName"] = JToken.op_Implicit(RawName);
		val["Description"] = JToken.op_Implicit(Description);
		val["Website"] = JToken.op_Implicit(Website);
		val["LastUpdate"] = JToken.op_Implicit(LastUpdate);
		val["DownloadCount"] = JToken.op_Implicit(DownloadCount);
		if ((int)ModLoaders != 0)
		{
			val["ModLoaders"] = JToken.op_Implicit((int)ModLoaders);
		}
		val["Tags"] = (JToken)new JArray((object)Tags);
		if (LogoUrl != null)
		{
			val["LogoUrl"] = JToken.op_Implicit(LogoUrl);
		}
		if (Drops.Any())
		{
			val["Drops"] = (JToken)new JArray((object)Drops);
		}
		val["CacheTime"] = JToken.op_Implicit(DateTime.Now);
		return val;
	}

	public MyVirtualizingElement<MyResourceItem> ToResourceItem(bool ShowMcVersionDesc, bool ShowLoaderDesc)
	{
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		_Closure_0024__29_002D0 arg = default(_Closure_0024__29_002D0);
		_Closure_0024__29_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__29_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Me = this;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ShowMcVersionDesc = ShowMcVersionDesc;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ShowLoaderDesc = ShowLoaderDesc;
		checked
		{
			if (Drops == null || !Drops.Any())
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_GameVersionDescription = "仅快照版本";
			}
			else
			{
				List<string> list = new List<string>();
				bool flag = false;
				int num = Drops.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					int num2 = Drops[i];
					int num3 = Drops[i];
					if (num2 < 120)
					{
						if (list.Any() && !flag)
						{
							break;
						}
						flag = true;
					}
					int num4 = i + 1;
					int num5 = Drops.Count - 1;
					for (int j = num4; j <= num5; j++)
					{
						if (ModDownload.AllDrops == null)
						{
							break;
						}
						if (ModDownload.AllDrops.IndexOf(Drops[j]) != ModDownload.AllDrops.IndexOf(num3) + 1)
						{
							break;
						}
						num3 = Drops[j];
						i = j;
					}
					string text = McVersion.DropToVersion(num2);
					string text2 = McVersion.DropToVersion(num3);
					if (num2 == num3)
					{
						list.Add(text);
						continue;
					}
					if (ModDownload.AllDrops != null && num2 >= ModDownload.AllDrops.First())
					{
						if (num3 < 120)
						{
							list.Clear();
							list.Add("全版本");
							break;
						}
						list.Add(text2 + "+");
						continue;
					}
					if (num3 < 120)
					{
						list.Add(text + "-");
						break;
					}
					if (ModDownload.AllDrops == null || ModDownload.AllDrops.IndexOf(num3) - ModDownload.AllDrops.IndexOf(num2) == 1)
					{
						list.Add(text + ", " + text2);
					}
					else
					{
						list.Add(text + "~" + text2);
					}
				}
				CS_0024_003C_003E8__locals0._0024VB_0024Local_GameVersionDescription = EnumerableExtensions.Join((IEnumerable)list, ", ");
			}
			List<ModLoaders> list2 = EnumerableExtensions.Flags<ModLoaders>(ModLoaders).ToList();
			ModLoaders val;
			switch (list2.Count)
			{
			case 0:
				if (EnumerableExtensions.IsSingle<ModLoaders>((IEnumerable<ModLoaders>)list2))
				{
					_Closure_0024__29_002D0 closure_0024__29_002D = CS_0024_003C_003E8__locals0;
					val = list2.Single();
					closure_0024__29_002D._0024VB_0024Local_ModLoaderDescriptionFull = "仅 " + ((Enum)(ModLoaders)(ref val)).ToString();
					_Closure_0024__29_002D0 closure_0024__29_002D2 = CS_0024_003C_003E8__locals0;
					val = list2.Single();
					closure_0024__29_002D2._0024VB_0024Local_ModLoaderDescriptionPart = ((Enum)(ModLoaders)(ref val)).ToString();
				}
				else
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ModLoaderDescriptionFull = "未知";
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ModLoaderDescriptionPart = "";
				}
				break;
			case 1:
			{
				_Closure_0024__29_002D0 closure_0024__29_002D3 = CS_0024_003C_003E8__locals0;
				val = list2.Single();
				closure_0024__29_002D3._0024VB_0024Local_ModLoaderDescriptionFull = "仅 " + ((Enum)(ModLoaders)(ref val)).ToString();
				_Closure_0024__29_002D0 closure_0024__29_002D4 = CS_0024_003C_003E8__locals0;
				val = list2.Single();
				closure_0024__29_002D4._0024VB_0024Local_ModLoaderDescriptionPart = ((Enum)(ModLoaders)(ref val)).ToString();
				break;
			}
			default:
			{
				int num6 = (Drops.Any() ? Drops.First() : 9999);
				if (list2.Contains((ModLoaders)1) && (num6 < 140 || list2.Contains((ModLoaders)4)) && (num6 < 200 || list2.Contains((ModLoaders)16)))
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ModLoaderDescriptionFull = "任意";
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ModLoaderDescriptionPart = "";
				}
				else
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ModLoaderDescriptionFull = EnumerableExtensions.Join((IEnumerable)list2, " / ");
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ModLoaderDescriptionPart = EnumerableExtensions.Join((IEnumerable)list2, " / ");
				}
				break;
			}
			}
			return new MyVirtualizingElement<MyResourceItem>([SpecialName] () =>
			{
				//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
				//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
				MyResourceItem myResourceItem = new MyResourceItem
				{
					Tag = CS_0024_003C_003E8__locals0._0024VB_0024Me
				};
				CS_0024_003C_003E8__locals0._0024VB_0024Me.ApplyLogoToMyImage(myResourceItem.PathLogo);
				(string, string) controlTitle = CS_0024_003C_003E8__locals0._0024VB_0024Me.GetControlTitle(HasModLoaderDescription: true);
				(myResourceItem.Title, _) = controlTitle;
				if (Operators.CompareString(controlTitle.Item2, "", TextCompare: false) == 0)
				{
					((StackPanel)myResourceItem.LabTitleRaw.Parent).Children.Remove(myResourceItem.LabTitleRaw);
				}
				else
				{
					myResourceItem.SubTitle = controlTitle.Item2;
				}
				myResourceItem.Tags = CS_0024_003C_003E8__locals0._0024VB_0024Me.Tags;
				myResourceItem.Description = StringExtensions.ReplaceLineEndings(CS_0024_003C_003E8__locals0._0024VB_0024Me.Description, "", false);
				if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_ShowMcVersionDesc && !CS_0024_003C_003E8__locals0._0024VB_0024Local_ShowLoaderDesc)
				{
					((Grid)myResourceItem.PathVersion.Parent).Children.Remove(myResourceItem.PathVersion);
					((Grid)myResourceItem.LabVersion.Parent).Children.Remove(myResourceItem.LabVersion);
					myResourceItem.ColumnVersion1.Width = new GridLength(0.0);
					myResourceItem.ColumnVersion2.MaxWidth = 0.0;
					myResourceItem.ColumnVersion3.Width = new GridLength(0.0);
				}
				else if (CS_0024_003C_003E8__locals0._0024VB_0024Local_ShowMcVersionDesc && CS_0024_003C_003E8__locals0._0024VB_0024Local_ShowLoaderDesc)
				{
					myResourceItem.LabVersion.Text = ((Operators.CompareString(CS_0024_003C_003E8__locals0._0024VB_0024Local_ModLoaderDescriptionPart, "", TextCompare: false) == 0) ? "" : (CS_0024_003C_003E8__locals0._0024VB_0024Local_ModLoaderDescriptionPart + " ")) + CS_0024_003C_003E8__locals0._0024VB_0024Local_GameVersionDescription;
				}
				else if (CS_0024_003C_003E8__locals0._0024VB_0024Local_ShowMcVersionDesc)
				{
					myResourceItem.LabVersion.Text = CS_0024_003C_003E8__locals0._0024VB_0024Local_GameVersionDescription;
				}
				else
				{
					myResourceItem.LabVersion.Text = CS_0024_003C_003E8__locals0._0024VB_0024Local_ModLoaderDescriptionFull;
				}
				TextBlock labSource = myResourceItem.LabSource;
				ResourcePlatforms platform = CS_0024_003C_003E8__locals0._0024VB_0024Me.Platform;
				labSource.Text = ((Enum)(ResourcePlatforms)(ref platform)).ToString();
				myResourceItem.LabTime.Text = StringUtils.FormatTimeSpan(CS_0024_003C_003E8__locals0._0024VB_0024Me.LastUpdate - DateTime.Now, true);
				myResourceItem.LabDownload.Text = Conversions.ToString((CS_0024_003C_003E8__locals0._0024VB_0024Me.DownloadCount > 100000000) ? (Conversions.ToString(Math.Round((double)CS_0024_003C_003E8__locals0._0024VB_0024Me.DownloadCount / 100000000.0, 2)) + " 亿") : ((CS_0024_003C_003E8__locals0._0024VB_0024Me.DownloadCount > 100000) ? (Conversions.ToString(Math.Floor((double)CS_0024_003C_003E8__locals0._0024VB_0024Me.DownloadCount / 10000.0)) + " 万") : ((object)CS_0024_003C_003E8__locals0._0024VB_0024Me.DownloadCount)));
				return myResourceItem;
			})
			{
				Height = 64.0
			};
		}
	}

	public void ApplyLogoToMyImage(MyImage Img)
	{
		if (string.IsNullOrEmpty(LogoUrl))
		{
			Img.Source = ModBase.PathImage + "Icons/NoIcon.png";
			return;
		}
		Img.Source = LogoUrl;
		Img.FallbackSource = ModDownload.DlSourceModGet(LogoUrl);
	}

	public (string Title, string SubTitle) GetControlTitle(bool HasModLoaderDescription)
	{
		string text = RawName;
		List<string> list;
		if (Operators.CompareString(TranslatedName, RawName, TextCompare: false) == 0)
		{
			IEnumerable<string> enumerable = from s in StringExtensions.Split(TranslatedName, new string[8] { " | ", " - ", "(", ")", "[", "]", "{", "}" }, true)
				select s.Trim(" /\\".ToCharArray()) into w
				where !string.IsNullOrEmpty(w)
				select w;
			if (!EnumerableExtensions.IsSingle<string>(enumerable))
			{
				list = new List<string>();
				List<string> list2 = new List<string>();
				_Closure_0024__31_002D0 closure_0024__31_002D = default(_Closure_0024__31_002D0);
				foreach (string item in enumerable)
				{
					closure_0024__31_002D = new _Closure_0024__31_002D0(closure_0024__31_002D);
					closure_0024__31_002D._0024VB_0024Local_LowerName = StringExtensions.Lower(item);
					if (Operators.CompareString(item, StringExtensions.Upper(item), TextCompare: false) == 0 && Operators.CompareString(item, "FPS", TextCompare: false) != 0 && Operators.CompareString(item, "HUD", TextCompare: false) != 0)
					{
						list.Add(item);
					}
					else if (new string[4] { "neoforge", "forge", "fabric", "quilt" }.Any(closure_0024__31_002D._Lambda_0024__2) && !StringExtensions.RegexCheck(closure_0024__31_002D._0024VB_0024Local_LowerName.Replace("neoforge", "").Replace("forge", "").Replace("fabric", "")
						.Replace("quilt", ""), "[a-z]+", RegexOptions.None))
					{
						list.Add(item);
					}
					else
					{
						list2.Add(item);
					}
				}
				if (list2.Any() && list.Any())
				{
					text = EnumerableExtensions.Join((IEnumerable)list2, " - ");
					goto IL_0415;
				}
			}
			goto IL_05ec;
		}
		text = (TranslatedName.Contains(" (") ? StringExtensions.BeforeFirst(TranslatedName, " (", false) : StringExtensions.BeforeLast(TranslatedName, " - ", false));
		string text2 = "";
		if (StringExtensions.AfterLast(TranslatedName, ")", false).Contains(" - "))
		{
			text2 = StringExtensions.AfterLast(StringExtensions.AfterLast(TranslatedName, ")", false), " - ", false);
		}
		string text3 = TranslatedName;
		if (Operators.CompareString(text2, "", TextCompare: false) != 0)
		{
			text3 = text3.Replace(" - " + text2, "");
		}
		text3 = text3.Replace(text, "").Trim('(', ')', ' ');
		list = (from s in StringExtensions.Split(text3, new string[8] { " | ", " - ", "(", ")", "[", "]", "{", "}" }, true)
			select s.Trim(" /".ToCharArray()) into w
			where !string.IsNullOrEmpty(w)
			select w).ToList();
		if (list.Count > 1 && !list.Any([SpecialName] (string s) => StringExtensions.Lower(s).Contains("forge") || StringExtensions.Lower(s).Contains("fabric") || StringExtensions.Lower(s).Contains("quilt")) && (list.Count != 2 || Operators.CompareString(StringExtensions.Upper(list.Last()), list.Last(), TextCompare: false) != 0))
		{
			list = new List<string> { text3 };
		}
		if (Operators.CompareString(text2, "", TextCompare: false) != 0)
		{
			list.Add(text2);
		}
		goto IL_0415;
		IL_05ec:
		string text4 = "";
		goto IL_05f2;
		IL_05f2:
		return (Title: text, SubTitle: text4);
		IL_0415:
		list = list.Distinct().ToList();
		text4 = "";
		if (!list.Any())
		{
			goto IL_05ec;
		}
		foreach (string item2 in list)
		{
			string text5 = item2;
			bool flag = StringExtensions.Lower(text5).Contains("neoforge") || StringExtensions.Lower(text5).Contains("forge") || StringExtensions.Lower(text5).Contains("fabric") || StringExtensions.Lower(text5).Contains("quilt");
			if ((HasModLoaderDescription || !flag) && (text5.Length >= 16 || !StringExtensions.Lower(text5).Contains("fabric") || !StringExtensions.Lower(text5).Contains("forge")))
			{
				if (flag && !text5.Contains("版") && StringExtensions.Lower(text5).Replace("neoforge", "").Replace("forge", "")
					.Replace("fabric", "")
					.Replace("quilt", "")
					.Length <= 3)
				{
					text5 = StringExtensions.Capitalize(text5.Replace("Edition", "").Replace("edition", "").Trim()) + " 版";
				}
				text5 = text5.Replace("neoforge", "NeoForge").Replace("forge", "Forge").Replace("neo", "Neo")
					.Replace("fabric", "Fabric")
					.Replace("quilt", "Quilt");
				text4 = text4 + "  |  " + text5.Trim();
			}
		}
		goto IL_05f2;
	}

	public bool IsLike(ResourceProject Project)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Invalid comparison between Unknown and I4
		if (Operators.CompareString(Id, Project.Id, TextCompare: false) == 0)
		{
			return true;
		}
		VB_0024AnonymousDelegate_1<string, string> vB_0024AnonymousDelegate_ = [SpecialName] (string Data) =>
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char item in Data.Where([SpecialName] (char c) => char.IsLetterOrDigit(c)))
			{
				stringBuilder.Append(item);
			}
			return StringExtensions.Lower(stringBuilder.ToString());
		};
		if (Platform == Project.Platform)
		{
			return false;
		}
		if (ModLoaders != Project.ModLoaders)
		{
			return false;
		}
		if ((int)Types != 8 && EnumerableExtensions.Ordered<int>((IEnumerable<int>)Drops).SequenceEqual(EnumerableExtensions.Ordered<int>((IEnumerable<int>)Project.Drops)))
		{
			return false;
		}
		if (Math.Abs((LastUpdate - Project.LastUpdate).TotalDays) > 7.0)
		{
			return false;
		}
		if (Operators.CompareString(TranslatedName, Project.TranslatedName, TextCompare: false) == 0 || Operators.CompareString(RawName, Project.RawName, TextCompare: false) == 0 || Operators.CompareString(Description, Project.Description, TextCompare: false) == 0 || Operators.CompareString(vB_0024AnonymousDelegate_(Slug), vB_0024AnonymousDelegate_(Project.Slug), TextCompare: false) == 0)
		{
			Logger.Info($"将 {RawName} ({Slug}) 与 {Project.RawName} ({Project.Slug}) 认定为相似工程", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceProject.vb");
			if (WikiEntry == null && Project.WikiEntry != null)
			{
				WikiEntry = Project.WikiEntry;
			}
			if (WikiEntry != null && Project.WikiEntry == null)
			{
				Project.WikiEntry = WikiEntry;
			}
			return true;
		}
		return false;
	}

	public override string ToString()
	{
		return $"{Id} ({Slug}): {RawName}";
	}

	public override bool Equals(object obj)
	{
		if (obj is ResourceProject resourceProject)
		{
			return Operators.CompareString(Id, resourceProject.Id, TextCompare: false) == 0;
		}
		return false;
	}

	public static bool operator ==(ResourceProject left, ResourceProject right)
	{
		return EqualityComparer<ResourceProject>.Default.Equals(left, right);
	}

	public static bool operator !=(ResourceProject left, ResourceProject right)
	{
		return !(left == right);
	}
}
