using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCLCS;

namespace PCL;

public class ResourceVersion
{
	public enum ReleaseTypes
	{
		Release = 1,
		Beta,
		Alpha
	}

	public enum DownloadReason
	{
		Standalone,
		Dependency,
		ModPack,
		Update
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__23_002D0
	{
		public ResourceProject _0024VB_0024Local_TargetProject;

		public _Closure_0024__23_002D0(_Closure_0024__23_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_TargetProject = arg0._0024VB_0024Local_TargetProject;
			}
		}

		[SpecialName]
		internal ResourceVersion _Lambda_0024__0(JToken a)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			return FromPlatformJson((JObject)a, _0024VB_0024Local_TargetProject.Types);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__27_002D0
	{
		public bool _0024VB_0024Local_IsBadDisplay;

		public MyListItem.ClickEventHandler _0024VB_0024Local_ClickHandler;

		public MyIconButton.ClickEventHandler _0024VB_0024Local_SaveAsButtonHandler;

		public ResourceVersion _0024VB_0024Me;

		[SpecialName]
		internal MyListItem _Lambda_0024__0()
		{
			MyListItem myListItem = new MyListItem();
			_Closure_0024__27_002D1 closure_0024__27_002D = new _Closure_0024__27_002D1();
			closure_0024__27_002D._0024VB_0024NonLocal__0024VB_0024Closure_3 = this;
			closure_0024__27_002D._0024W2 = myListItem;
			closure_0024__27_002D._0024W2.SnapsToDevicePixels = true;
			closure_0024__27_002D._0024W2.Height = 42.0;
			closure_0024__27_002D._0024W2.Type = MyListItem.CheckType.Clickable;
			closure_0024__27_002D._0024W2.Tag = _0024VB_0024Me;
			closure_0024__27_002D._0024W2.Title = StringExtensions.BeforeLastOfAny(_0024VB_0024Local_IsBadDisplay ? _0024VB_0024Me.FileName : _0024VB_0024Me.Display, (IEnumerable<string>)new string[4] { ".zip", ".jar", ".mrpack", ".litemod" }, true);
			closure_0024__27_002D._0024W2.Logo = $"{ModBase.PathImage}ReleaseTypes/{_0024VB_0024Me.ReleaseType}.png";
			closure_0024__27_002D._0024W2.Click += _0024VB_0024Local_ClickHandler;
			closure_0024__27_002D._0024W2.Info = EnumerableExtensions.Join((IEnumerable)closure_0024__27_002D._Lambda_0024__1(), '，');
			if (_0024VB_0024Local_SaveAsButtonHandler != null)
			{
				MyIconButton myIconButton = new MyIconButton
				{
					Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
					ToolTip = "另存为"
				};
				myIconButton.Click += _0024VB_0024Local_SaveAsButtonHandler;
				closure_0024__27_002D._0024W2.Buttons = new MyIconButton[1] { myIconButton };
			}
			return myListItem;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__27_002D1
	{
		[CompilerGenerated]
		private sealed class VB_0024StateMachine___Lambda_0024__1 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			public int _0024State;

			public string _0024Current;

			public int _0024InitialThreadId;

			internal _Closure_0024__27_002D1 _0024VB_0024NonLocal__Closure_0024__27_002D1;

			private string Current => _0024Current;

			object IEnumerator.Current => _0024Current;

			public VB_0024StateMachine___Lambda_0024__1(int _0024State)
			{
				this._0024State = _0024State;
				_0024InitialThreadId = Environment.CurrentManagedThreadId;
			}

			private void Dispose()
			{
				_0024State = -2;
			}

			void IDisposable.Dispose()
			{
				//ILSpy generated this explicit interface implementation from .override directive in Dispose
				this.Dispose();
			}

			[CompilerGenerated]
			private bool MoveNext()
			{
				//IL_0048: Unknown result type (might be due to invalid IL or missing references)
				//IL_0060: Unknown result type (might be due to invalid IL or missing references)
				int num;
				switch (_0024State)
				{
				default:
					return false;
				case 0:
					num = (_0024State = -1);
					if ((int)_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.ModLoaders != 0)
					{
						_0024Current = EnumerableExtensions.Join((IEnumerable)EnumerableExtensions.Flags<ModLoaders>(_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.ModLoaders), '、');
						num = (_0024State = 1);
						return true;
					}
					goto IL_008d;
				case 1:
					num = (_0024State = -1);
					goto IL_008d;
				case 2:
					num = (_0024State = -1);
					goto IL_00f3;
				case 3:
					num = (_0024State = -1);
					goto IL_0152;
				case 4:
					num = (_0024State = -1);
					goto IL_01d5;
				case 5:
					num = (_0024State = -1);
					goto IL_028a;
				case 6:
					num = (_0024State = -1);
					if (_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.ReleaseType != ReleaseTypes.Release)
					{
						_0024Current = _0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.ReleaseTypeDisplay;
						num = (_0024State = 7);
						return true;
					}
					break;
				case 7:
					{
						num = (_0024State = -1);
						break;
					}
					IL_0152:
					if (_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.GameVersions.All([SpecialName] (string v) =>
					{
						_Closure_0024__27_002D2 CS_0024_003C_003E8__locals0 = new _Closure_0024__27_002D2
						{
							_0024VB_0024Local_v = v
						};
						return !CS_0024_003C_003E8__locals0._0024VB_0024Local_v.Contains(".") || new string[6] { "w", "snapshot", "rc", "pre", "experimental", "-" }.Any([SpecialName] (string s) => StringExtensions.ContainsIgnoreCase(CS_0024_003C_003E8__locals0._0024VB_0024Local_v, s));
					}))
					{
						_0024Current = $"游戏版本 {EnumerableExtensions.Join((IEnumerable)_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.GameVersions, '、')}";
						num = (_0024State = 4);
						return true;
					}
					goto IL_01d5;
					IL_01d5:
					if (_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.DownloadCount > 0)
					{
						_0024Current = string.Format("下载 {0}次", (_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.DownloadCount > 100000) ? (Conversions.ToString(Math.Round((double)_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.DownloadCount / 10000.0)) + " 万") : (_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.DownloadCount + " "));
						num = (_0024State = 5);
						return true;
					}
					goto IL_028a;
					IL_008d:
					if (Operators.CompareString(_0024VB_0024NonLocal__Closure_0024__27_002D1._0024W2.Title, PathUtils.GetFileNameWithoutExtension(_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.FileName), TextCompare: false) != 0)
					{
						_0024Current = PathUtils.GetFileNameWithoutExtension(_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.FileName);
						num = (_0024State = 2);
						return true;
					}
					goto IL_00f3;
					IL_00f3:
					if (_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.Dependencies.Any())
					{
						_0024Current = $"{_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.Dependencies.Count} 项前置";
						num = (_0024State = 3);
						return true;
					}
					goto IL_0152;
					IL_028a:
					_0024Current = $"更新于 {StringUtils.FormatTimeSpan(_0024VB_0024NonLocal__Closure_0024__27_002D1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Me.ReleaseDate - DateTime.Now, false)}";
					num = (_0024State = 6);
					return true;
				}
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			private IEnumerator<string> GetEnumerator()
			{
				VB_0024StateMachine___Lambda_0024__1 result;
				if (_0024State == -2 && _0024InitialThreadId == Environment.CurrentManagedThreadId)
				{
					_0024State = 0;
					result = this;
				}
				else
				{
					result = new VB_0024StateMachine___Lambda_0024__1(0)
					{
						_0024VB_0024NonLocal__Closure_0024__27_002D1 = _0024VB_0024NonLocal__Closure_0024__27_002D1
					};
				}
				return result;
			}

			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
				return this.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			private void Reset()
			{
				throw new NotSupportedException();
			}

			void IEnumerator.Reset()
			{
				//ILSpy generated this explicit interface implementation from .override directive in Reset
				this.Reset();
			}
		}

		public MyListItem _0024W2;

		public _Closure_0024__27_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_3;

		[SpecialName]
		[IteratorStateMachine(typeof(VB_0024StateMachine___Lambda_0024__1))]
		internal IEnumerable<string> _Lambda_0024__1()
		{
			//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
			return new VB_0024StateMachine___Lambda_0024__1(-2)
			{
				_0024VB_0024NonLocal__Closure_0024__27_002D1 = this
			};
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__27_002D2
	{
		public string _0024VB_0024Local_v;

		[SpecialName]
		internal bool _Lambda_0024__3(string s)
		{
			return StringExtensions.ContainsIgnoreCase(_0024VB_0024Local_v, s);
		}
	}

	public string Id;

	public ResourceTypes ResourceType;

	public string Display;

	public DateTime ReleaseDate;

	public int DownloadCount;

	public string Version;

	public ModLoaders ModLoaders;

	public List<string> GameVersions;

	public ReleaseTypes ReleaseType;

	public string FileName;

	public List<string> DownloadUrls;

	public string Hash;

	public int Size;

	private List<string> RawDependencies;

	public List<string> Dependencies;

	protected internal static Dictionary<string, List<ResourceVersion>> ProjectFilesCache = new Dictionary<string, List<ResourceVersion>>();

	public string ReleaseTypeDisplay => ReleaseType switch
	{
		ReleaseTypes.Release => "正式版", 
		ReleaseTypes.Beta => ModBase.ModeDebug ? "Beta 版" : "测试版", 
		_ => ModBase.ModeDebug ? "Alpha 版" : "早期测试版", 
	};

	public bool DownloadAvailable
	{
		get
		{
			if (FileName != null)
			{
				return DownloadUrls != null;
			}
			return false;
		}
	}

	public ResourceVersion()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		ModLoaders = (ModLoaders)0;
		FileName = null;
		Hash = null;
		Size = -1;
		RawDependencies = new List<string>();
		Dependencies = new List<string>();
	}

	public static ResourceVersion FromPlatformJson(JObject Data, ResourceTypes ExpectedResourceType)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Expected O, but got Unknown
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Invalid comparison between Unknown and I4
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a8: Invalid comparison between Unknown and I4
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		ResourceVersion resourceVersion = new ResourceVersion();
		ResourceVersion resourceVersion2 = resourceVersion;
		resourceVersion2.ResourceType = ExpectedResourceType;
		if (Data.ContainsKey("gameId"))
		{
			resourceVersion2.Id = (string)Data["id"];
			resourceVersion2.Display = Data["displayName"].ToString().Replace("\t", "").Trim(' ');
			resourceVersion2.Version = null;
			resourceVersion2.ReleaseDate = (DateTime)Data["fileDate"];
			resourceVersion2.ReleaseType = (ReleaseTypes)Data["releaseType"].ToObject<int>();
			resourceVersion2.DownloadCount = (int)Data["downloadCount"];
			resourceVersion2.FileName = (string)Data["fileName"];
			resourceVersion2.Size = (int)Data["fileLength"];
			if (resourceVersion2.Size == 0)
			{
				resourceVersion2.Size = -1;
			}
			ResourceVersion resourceVersion3 = resourceVersion2;
			JToken obj = ((IEnumerable<JToken>)(JArray)Data["hashes"]).ToList().FirstOrDefault([SpecialName] (JToken s) => s[(object)"algo"].ToObject<int>() == 1);
			object hash;
			if (obj == null)
			{
				hash = null;
			}
			else
			{
				JToken obj2 = obj[(object)"value"];
				hash = ((obj2 != null) ? StringExtensions.Lower(obj2.ToString()) : null);
			}
			resourceVersion3.Hash = (string)hash;
			if (resourceVersion2.Hash == null)
			{
				ResourceVersion resourceVersion4 = resourceVersion2;
				JToken obj3 = ((IEnumerable<JToken>)(JArray)Data["hashes"]).ToList().FirstOrDefault([SpecialName] (JToken s) => s[(object)"algo"].ToObject<int>() == 2);
				object hash2;
				if (obj3 == null)
				{
					hash2 = null;
				}
				else
				{
					JToken obj4 = obj3[(object)"value"];
					hash2 = ((obj4 != null) ? StringExtensions.Lower(obj4.ToString()) : null);
				}
				resourceVersion4.Hash = (string)hash2;
			}
			string text = Data["downloadUrl"].ToString();
			if (Operators.CompareString(text, "", TextCompare: false) == 0)
			{
				text = $"https://edge.forgecdn.net/files/{Conversions.ToInteger(resourceVersion2.Id.ToString().Substring(0, 4))}/{Conversions.ToInteger(resourceVersion2.Id.ToString().Substring(4))}/{resourceVersion2.FileName}";
			}
			resourceVersion2.DownloadUrls = ParseCurseForgeDownloadUrls(text.Replace(resourceVersion2.FileName, StringUtils.UrlEscape(resourceVersion2.FileName)));
			resourceVersion2.DownloadUrls.Add(ModDownload.DlSourceModGet(text));
			resourceVersion2.DownloadUrls = resourceVersion2.DownloadUrls.Distinct().ToList();
			if (Data.ContainsKey("dependencies"))
			{
				resourceVersion2.RawDependencies = (from d in (IEnumerable<JToken>)Data["dependencies"]
					where d[(object)"relationType"].ToObject<int>() == 3 && d[(object)"modId"].ToObject<int>() != 306612 && d[(object)"modId"].ToObject<int>() != 634179
					select d[(object)"modId"].ToString()).ToList();
			}
			List<string> list = ((IEnumerable<JToken>)Data["gameVersions"]).Select([SpecialName] (JToken t) => StringExtensions.Lower(t.ToString().Trim())).ToList();
			resourceVersion2.GameVersions = (from v in list
				where McVersion.IsStandardFormat(v)
				select v.Replace("-snapshot", " 预览版")).Distinct().ToList();
			if (EnumerableExtensions.IsSingle<string>((IEnumerable<string>)resourceVersion2.GameVersions))
			{
				resourceVersion2.GameVersions = resourceVersion2.GameVersions.ToList();
			}
			else if (resourceVersion2.GameVersions.Count > 1)
			{
				resourceVersion2.GameVersions = ModBase.SortByComparison((IList<string>)resourceVersion2.GameVersions, (Func<string, string, bool>)McUtils.CompareVersionGE).ToList();
				if ((int)resourceVersion2.ResourceType == 2)
				{
					resourceVersion2.GameVersions = new string[1] { resourceVersion2.GameVersions[0] }.ToList();
				}
			}
			else
			{
				resourceVersion2.GameVersions = new List<string> { "未知版本" };
			}
			resourceVersion2.ModLoaders = (ModLoaders)0;
			foreach (ModLoaders allFlag in EnumUtils.GetAllFlags<ModLoaders>())
			{
				ModLoaders current = allFlag;
				if (list.Contains(StringExtensions.Lower(((Enum)(ModLoaders)(ref current)).ToString())))
				{
					resourceVersion2.ModLoaders |= current;
				}
			}
		}
		else
		{
			resourceVersion2.Id = (string)Data["id"];
			resourceVersion2.Display = Data["name"].ToString().Replace("\t", "").Trim(' ');
			resourceVersion2.Version = (string)Data["version_number"];
			resourceVersion2.ReleaseDate = (DateTime)Data["date_published"];
			resourceVersion2.ReleaseType = ((Operators.CompareString(Data["version_type"].ToString(), "release", TextCompare: false) == 0) ? ReleaseTypes.Release : ((Operators.CompareString(Data["version_type"].ToString(), "beta", TextCompare: false) == 0) ? ReleaseTypes.Beta : ReleaseTypes.Alpha));
			resourceVersion2.DownloadCount = (int)Data["downloads"];
			if (((IEnumerable<JToken>)(JArray)Data["files"]).Any())
			{
				JToken val = Data["files"][(object)0];
				resourceVersion2.FileName = (string)val[(object)"filename"];
				resourceVersion2.DownloadUrls = new List<string>
				{
					(string)val[(object)"url"],
					ModDownload.DlSourceModGet((string)val[(object)"url"])
				}.Distinct().ToList();
				resourceVersion2.Size = (int)val[(object)"size"];
				ResourceVersion resourceVersion5 = resourceVersion2;
				JToken obj5 = val[(object)"hashes"][(object)"sha1"];
				resourceVersion5.Hash = ((obj5 != null) ? StringExtensions.Lower(obj5.ToString()) : null);
			}
			List<string> list2 = ((IEnumerable<JToken>)Data["loaders"]).Select([SpecialName] (JToken v) => v.ToString()).ToList();
			resourceVersion2.ModLoaders = (ModLoaders)0;
			foreach (ModLoaders allFlag2 in EnumUtils.GetAllFlags<ModLoaders>())
			{
				ModLoaders current2 = allFlag2;
				if (list2.Contains(StringExtensions.Lower(((Enum)(ModLoaders)(ref current2)).ToString())))
				{
					resourceVersion2.ModLoaders |= current2;
				}
			}
			if (EnumUtils.HasFlagF<ResourceTypes>(resourceVersion2.ResourceType, (ResourceTypes)1) || EnumUtils.HasFlagF<ResourceTypes>(resourceVersion2.ResourceType, (ResourceTypes)16))
			{
				if (list2.Intersect(new string[5] { "bukkit", "folia", "paper", "purpur", "spigot" }).Any())
				{
					resourceVersion2.ResourceType = (ResourceTypes)32;
				}
				if (list2.Contains("datapack"))
				{
					resourceVersion2.ResourceType = (ResourceTypes)16;
				}
				if (EnumerableExtensions.Flags<ModLoaders>(resourceVersion2.ModLoaders).Any())
				{
					resourceVersion2.ResourceType = (ResourceTypes)1;
				}
			}
			if (Data.ContainsKey("dependencies"))
			{
				resourceVersion2.RawDependencies = (from d in (IEnumerable<JToken>)Data["dependencies"]
					where (string)d[(object)"dependency_type"] == "required" && (string)d[(object)"project_id"] != "P7dR8mSH" && (string)d[(object)"project_id"] != "qvIfYCYJ" && d[(object)"project_id"].ToString().Length > 0
					select d[(object)"project_id"].ToString()).ToList();
			}
			List<string> source = ((IEnumerable<JToken>)Data["game_versions"]).Select([SpecialName] (JToken t) => StringExtensions.Lower(t.ToString().Trim())).ToList();
			resourceVersion2.GameVersions = (from v in source
				where v.Contains(".")
				select (!v.Contains("-")) ? ((!StringExtensions.StartsWithF(v, "b1.", false)) ? v : "远古版本") : (StringExtensions.BeforeFirst(v, "-", false) + " 预览版")).Distinct().ToList();
			if (!EnumerableExtensions.IsSingle<string>((IEnumerable<string>)resourceVersion2.GameVersions))
			{
				if (resourceVersion2.GameVersions.Count > 1)
				{
					resourceVersion2.GameVersions = ModBase.SortByComparison((IList<string>)resourceVersion2.GameVersions, (Func<string, string, bool>)McUtils.CompareVersionGE).ToList();
					if ((int)resourceVersion2.ResourceType == 2)
					{
						resourceVersion2.GameVersions = new List<string> { resourceVersion2.GameVersions[0] };
					}
				}
				else if (source.Any([SpecialName] (string v) => StringExtensions.RegexCheck(v, "[0-9]{2}w[0-9]{2}[a-z]", RegexOptions.None)))
				{
					resourceVersion2.GameVersions = source.Where([SpecialName] (string v) => StringExtensions.RegexCheck(v, "[0-9]{2}w[0-9]{2}[a-z]", RegexOptions.None)).ToList();
				}
				else
				{
					resourceVersion2.GameVersions = new List<string> { "未知版本" };
				}
			}
		}
		resourceVersion2 = null;
		return resourceVersion;
	}

	public static List<ResourceVersion> FromProjectId(string ProjectId, ResourcePlatforms Platform, bool LoadDependencies = true)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Invalid comparison between Unknown and I4
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		_Closure_0024__23_002D0 arg = default(_Closure_0024__23_002D0);
		_Closure_0024__23_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__23_002D0(arg);
		if (!EnumerableExtensions.IsSingle<ResourcePlatforms>(EnumerableExtensions.Flags<ResourcePlatforms>(Platform)))
		{
			throw new ArgumentException($"必须指定单一平台，当前值为 {Platform}");
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetProject = ResourceProject.FromProjectId(ProjectId, Platform);
		if (!ProjectFilesCache.ContainsKey(ProjectId))
		{
			Logger.Info($"开始获取项目版本列表：{ProjectId}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceVersion.vb");
			JArray source = null;
			if ((int)Platform == 1)
			{
				int num = 0;
				checked
				{
					do
					{
						JObject val = (JObject)ModDownload.DlModRequest($"https://api.curseforge.com/v1/mods/{ProjectId}/files?pageSize=" + Conversions.ToString(10000 + num));
						if (val["pagination"][(object)"resultCount"].ToObject<int>() == val["pagination"][(object)"totalCount"].ToObject<int>())
						{
							source = (JArray)val["data"];
							break;
						}
						if (num < 2)
						{
							Logger.Warn($"CurseForge 返回的资源版本列表存在缺失，即将进行第 {num + 1} 次重试", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceVersion.vb");
							Logger.Info(string.Format("返回的原始内容如下：{0}{1}", "\r\n", val), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceVersion.vb");
							num++;
							continue;
						}
						Logger.Info(string.Format("CurseForge 返回的资源版本列表存在缺失，返回的原始内容如下：{0}{1}", "\r\n", val), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceVersion.vb");
						throw new Exception("CurseForge 返回的资源版本列表存在缺失");
					}
					while (num <= 2);
				}
			}
			else
			{
				source = (JArray)ModDownload.DlModRequest($"https://api.modrinth.com/v2/project/{ProjectId}/version?include_changelog=false");
			}
			ProjectFilesCache[ProjectId] = EnumerableExtensions.DistinctBy<ResourceVersion, string>(from a in (IEnumerable<JToken>)source
				select FromPlatformJson((JObject)a, CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetProject.Types) into a
				where a.DownloadAvailable
				select a, (Func<ResourceVersion, string>)([SpecialName] (ResourceVersion a) => a.Id)).ToList();
		}
		List<ResourceVersion> list = ProjectFilesCache[ProjectId];
		if (!LoadDependencies)
		{
			return list;
		}
		List<string> list2 = list.SelectMany([SpecialName] (ResourceVersion f) => f.RawDependencies).Distinct().ToList();
		if (list2.Any())
		{
			Logger.Info(string.Format("项目 {0} 中需要加载的前置：{1}", ProjectId, EnumerableExtensions.Join((IEnumerable)list2, "，")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceVersion.vb");
			foreach (ResourceProject item in ResourceProject.FromProjectIds(list2, CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetProject.Platform))
			{
				foreach (ResourceVersion item2 in list)
				{
					if (item2.RawDependencies.Contains(item.Id) && Operators.CompareString(item.Id, ProjectId, TextCompare: false) != 0 && !item2.Dependencies.Contains(item.Id))
					{
						item2.Dependencies.Add(item.Id);
					}
				}
			}
		}
		return list;
	}

	public static ResourceVersion FromCacheJson(JObject Data)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		ResourceVersion resourceVersion;
		ResourceVersion result = (resourceVersion = new ResourceVersion());
		resourceVersion.ResourceType = Data["ResourceType"].ToObject<ResourceTypes>();
		resourceVersion.Id = Data["Id"].ToString();
		resourceVersion.Display = Data["DisplayName"].ToString();
		if (Data.ContainsKey("Version"))
		{
			resourceVersion.Version = Data["Version"].ToString();
		}
		resourceVersion.ReleaseDate = Data["ReleaseDate"].ToObject<DateTime>();
		resourceVersion.DownloadCount = Data["DownloadCount"].ToObject<int>();
		resourceVersion.ReleaseType = Data["ReleaseType"].ToObject<ReleaseTypes>();
		if (Data.ContainsKey("FileName"))
		{
			resourceVersion.FileName = Data["FileName"].ToString();
		}
		if (Data.ContainsKey("DownloadUrls"))
		{
			resourceVersion.DownloadUrls = Data["DownloadUrls"].ToObject<List<string>>();
		}
		if (Data.ContainsKey("ModLoaders"))
		{
			resourceVersion.ModLoaders = Data["ModLoaders"].ToObject<ModLoaders>();
		}
		if (Data.ContainsKey("Size"))
		{
			resourceVersion.Size = Data["Size"].ToObject<int>();
		}
		if (Data.ContainsKey("Hash"))
		{
			resourceVersion.Hash = Data["Hash"].ToString();
		}
		if (Data.ContainsKey("GameVersions"))
		{
			resourceVersion.GameVersions = Data["GameVersions"].ToObject<List<string>>();
		}
		if (Data.ContainsKey("RawDependencies"))
		{
			resourceVersion.RawDependencies = Data["RawDependencies"].ToObject<List<string>>();
		}
		if (Data.ContainsKey("Dependencies"))
		{
			resourceVersion.Dependencies = Data["Dependencies"].ToObject<List<string>>();
		}
		resourceVersion = null;
		return result;
	}

	public JObject ToCacheJson()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected I4, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected I4, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017d: Expected O, but got Unknown
		JObject val = new JObject();
		JObject val2 = val;
		val2.Add("ResourceType", JToken.op_Implicit((int)ResourceType));
		val2.Add("Id", JToken.op_Implicit(Id));
		if (Version != null)
		{
			val2.Add("Version", JToken.op_Implicit(Version));
		}
		val2.Add("DisplayName", JToken.op_Implicit(Display));
		val2.Add("ReleaseDate", JToken.op_Implicit(ReleaseDate));
		val2.Add("DownloadCount", JToken.op_Implicit(DownloadCount));
		val2.Add("ModLoaders", JToken.op_Implicit((int)ModLoaders));
		val2.Add("GameVersions", (JToken)new JArray((object)GameVersions));
		val2.Add("ReleaseType", JToken.op_Implicit((int)ReleaseType));
		if (FileName != null)
		{
			val2.Add("FileName", JToken.op_Implicit(FileName));
		}
		if (DownloadUrls != null)
		{
			val2.Add("DownloadUrls", (JToken)new JArray((object)DownloadUrls));
		}
		if (Hash != null)
		{
			val2.Add("Hash", JToken.op_Implicit(Hash));
		}
		if (Size >= 0)
		{
			val2.Add("Size", JToken.op_Implicit(Size));
		}
		val2.Add("RawDependencies", (JToken)new JArray((object)RawDependencies));
		val2.Add("Dependencies", (JToken)new JArray((object)Dependencies));
		val2 = null;
		return val;
	}

	public MyVirtualizingElement<MyListItem> ToListItem(MyListItem.ClickEventHandler ClickHandler, MyIconButton.ClickEventHandler SaveAsButtonHandler = null, bool IsBadDisplay = false)
	{
		_Closure_0024__27_002D0 CS_0024_003C_003E8__locals0;
		return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
		{
			MyListItem myListItem = new MyListItem();
			_Closure_0024__27_002D1 closure_0024__27_002D = new _Closure_0024__27_002D1();
			closure_0024__27_002D._0024VB_0024NonLocal__0024VB_0024Closure_3 = CS_0024_003C_003E8__locals0;
			closure_0024__27_002D._0024W2 = myListItem;
			closure_0024__27_002D._0024W2.SnapsToDevicePixels = true;
			closure_0024__27_002D._0024W2.Height = 42.0;
			closure_0024__27_002D._0024W2.Type = MyListItem.CheckType.Clickable;
			closure_0024__27_002D._0024W2.Tag = this;
			closure_0024__27_002D._0024W2.Title = StringExtensions.BeforeLastOfAny(IsBadDisplay ? FileName : Display, (IEnumerable<string>)new string[4] { ".zip", ".jar", ".mrpack", ".litemod" }, true);
			closure_0024__27_002D._0024W2.Logo = $"{ModBase.PathImage}ReleaseTypes/{ReleaseType}.png";
			closure_0024__27_002D._0024W2.Click += ClickHandler;
			closure_0024__27_002D._0024W2.Info = EnumerableExtensions.Join((IEnumerable)closure_0024__27_002D._Lambda_0024__1(), '，');
			if (SaveAsButtonHandler != null)
			{
				MyIconButton myIconButton = new MyIconButton
				{
					Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
					ToolTip = "另存为"
				};
				myIconButton.Click += SaveAsButtonHandler;
				closure_0024__27_002D._0024W2.Buttons = new MyIconButton[1] { myIconButton };
			}
			return myListItem;
		})
		{
			Height = 42.0
		};
	}

	public ModNet.NetFile ToNetFile(string LocalFileOrFolder, DownloadReason? TrackDownloadReason = null, string TrackGameVersion = null, ModLoaders TrackLoader = 0)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		return new ModNet.NetFile(ParseModrinthTrackArguments(DownloadUrls, TrackDownloadReason, TrackGameVersion, TrackLoader), LocalFileOrFolder + (StringExtensions.EndsWithF(LocalFileOrFolder, "\\", false) ? FileName : ""), new FileChecker
		{
			Hash = Hash,
			ActualSize = Size
		}, SimulateBrowserHeaders: true);
	}

	public static IEnumerable<string> ParseModrinthTrackArguments(List<string> Urls, DownloadReason? TrackDownloadReason = null, string TrackGameVersion = null, ModLoaders TrackLoader = 0)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		if (TrackDownloadReason.HasValue)
		{
			dictionary.Add("mr_download_reason", StringExtensions.Lower(TrackDownloadReason.ToString()));
		}
		if (TrackGameVersion != null)
		{
			dictionary.Add("mr_game_version", TrackGameVersion);
		}
		if (EnumerableExtensions.Flags<ModLoaders>(TrackLoader).Any())
		{
			ModLoaders val = EnumerableExtensions.Flags<ModLoaders>(TrackLoader).First();
			dictionary.Add("mr_loader", StringExtensions.Lower(((Enum)(ModLoaders)(ref val)).ToString()));
		}
		string arg = EnumerableExtensions.Join((IEnumerable)dictionary.Select([SpecialName] (KeyValuePair<string, string> a) => $"{a.Key}={a.Value}"), "&");
		foreach (string Url in Urls)
		{
			if (!StringExtensions.ContainsIgnoreCase(Url, "cdn.modrinth.com/"))
			{
				yield return Url;
			}
			yield return string.Format("{0}{1}{2}", Url, Url.Contains("?") ? "&" : "?", arg);
		}
	}

	public static List<string> ParseCurseForgeDownloadUrls(string Url)
	{
		return new string[5]
		{
			Url.Replace("-service.overwolf.wtf", ".forgecdn.net").Replace("://edge.", "://mediafilez.").Replace("://media.", "://mediafilez."),
			Url.Replace("://edge.", "://mediafilez.").Replace("://media.", "://mediafilez."),
			Url.Replace("-service.overwolf.wtf", ".forgecdn.net"),
			Url.Replace("://media.", "://edge."),
			Url
		}.Distinct().ToList();
	}

	public override string ToString()
	{
		return $"{Id}: {FileName}";
	}
}
