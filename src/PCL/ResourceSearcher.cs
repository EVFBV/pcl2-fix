using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCLCS;

namespace PCL;

public class ResourceSearcher
{
	public class SearchRequest
	{
		public SearchResult Storage;

		public int TargetResultCount;

		public ResourceTypes Type;

		public string Tag;

		public ModLoaders ModLoaders;

		public string GameVersion;

		public string SearchText;

		public ResourcePlatforms Sources;

		public bool CanContinue
		{
			get
			{
				//IL_0014: Unknown result type (might be due to invalid IL or missing references)
				//IL_0041: Unknown result type (might be due to invalid IL or missing references)
				if (StringExtensions.StartsWithF(Tag, "/", false) || !EnumUtils.HasFlagF<ResourcePlatforms>(Sources, (ResourcePlatforms)1))
				{
					Storage.CurseForgeTotal = 0;
				}
				if (StringExtensions.EndsWithF(Tag, "/", false) || !EnumUtils.HasFlagF<ResourcePlatforms>(Sources, (ResourcePlatforms)2))
				{
					Storage.ModrinthTotal = 0;
				}
				if (Storage.CurseForgeTotal == -1 || Storage.ModrinthTotal == -1)
				{
					return true;
				}
				return Storage.CurseForgeOffset < Storage.CurseForgeTotal || Storage.ModrinthOffset < Storage.ModrinthTotal;
			}
		}

		public SearchRequest(ResourceTypes Type, SearchResult Storage, int TargetResultCount)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			Tag = "";
			ModLoaders = (ModLoaders)0;
			GameVersion = null;
			SearchText = null;
			Sources = (ResourcePlatforms)3;
			this.Type = Type;
			this.Storage = Storage;
			this.TargetResultCount = TargetResultCount;
		}

		public string GetCurseForgeAddress(string SearchText, bool IgnoreModLoaderFilter)
		{
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected I4, but got Unknown
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Invalid comparison between Unknown and I4
			//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Invalid comparison between Unknown and I4
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			if (StringExtensions.StartsWithF(Tag, "/", false))
			{
				Storage.CurseForgeTotal = 0;
			}
			string text = $"https://api.curseforge.com/v1/mods/search?gameId=432&sortField=2&sortOrder=desc&pageSize={40}";
			ResourceTypes type = Type;
			switch (type - 1)
			{
			default:
				if ((int)type != 8)
				{
					if ((int)type == 16)
					{
						text += "&classId=6945";
					}
				}
				else
				{
					text += "&classId=6552";
				}
				break;
			case 0:
				text += "&classId=6";
				break;
			case 1:
				text += "&classId=4471";
				break;
			case 3:
				text += "&classId=12";
				break;
			case 2:
				break;
			}
			if (Operators.CompareString(Tag, "", TextCompare: false) != 0)
			{
				text = text + "&categoryId=" + StringExtensions.BeforeFirst(Tag, "/", false);
			}
			if ((int)ModLoaders != 0 && !IgnoreModLoaderFilter)
			{
				text = text + "&modLoaderType=" + Conversions.ToString(Resource.ToCurseForgeModLoaderType(EnumerableExtensions.Flags<ModLoaders>(ModLoaders).First()));
			}
			if (!string.IsNullOrEmpty(GameVersion))
			{
				text = text + "&gameVersion=" + GameVersion;
			}
			if (!string.IsNullOrEmpty(SearchText))
			{
				text = text + "&searchFilter=" + StringUtils.UrlEscape(SearchText);
			}
			if (Storage.CurseForgeOffset > 0)
			{
				text = text + "&index=" + Conversions.ToString(Storage.CurseForgeOffset);
			}
			return text;
		}

		public string GetModrinthAddress(string SearchText, bool IgnoreModLoaderFilter)
		{
			//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			if (StringExtensions.EndsWithF(Tag, "/", false))
			{
				Storage.ModrinthTotal = 0;
			}
			string text = $"https://api.modrinth.com/v2/search?limit={40}&index=relevance";
			if (!string.IsNullOrEmpty(SearchText))
			{
				text = text + "&query=" + StringUtils.UrlEscape(SearchText);
			}
			if (Storage.ModrinthOffset > 0)
			{
				text = text + "&offset=" + Conversions.ToString(Storage.ModrinthOffset);
			}
			List<string> list = new List<string>();
			list.Add($"[\"project_type:{StringExtensions.Lower(((Enum)(ResourceTypes)(ref Type)).ToString())}\"]");
			if (!string.IsNullOrEmpty(Tag))
			{
				list.Add(string.Format("[\"categories:'{0}'\"]", StringExtensions.AfterLast(Tag, "/", false)));
			}
			if ((int)ModLoaders != 0 && !IgnoreModLoaderFilter)
			{
				list.Add(string.Format("[\"categories:{0}\"]", EnumerableExtensions.Join((IEnumerable)(from f in EnumerableExtensions.Flags<ModLoaders>(ModLoaders)
					select StringExtensions.Lower(((Enum)(ModLoaders)(ref f)).ToString())), "\",\"categories:")));
			}
			if (!string.IsNullOrEmpty(GameVersion))
			{
				list.Add($"[\"versions:'{GameVersion}'\"]");
			}
			return text + "&facets=[" + string.Join(",", list) + "]";
		}

		public override bool Equals(object obj)
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			if (obj is SearchRequest searchRequest && Type == searchRequest.Type && TargetResultCount == searchRequest.TargetResultCount && Operators.CompareString(Tag, searchRequest.Tag, TextCompare: false) == 0 && ModLoaders == searchRequest.ModLoaders && Sources == searchRequest.Sources && Operators.CompareString(GameVersion, searchRequest.GameVersion, TextCompare: false) == 0)
			{
				return Operators.CompareString(SearchText, searchRequest.SearchText, TextCompare: false) == 0;
			}
			return false;
		}

		public static bool operator ==(SearchRequest left, SearchRequest right)
		{
			return EqualityComparer<SearchRequest>.Default.Equals(left, right);
		}

		public static bool operator !=(SearchRequest left, SearchRequest right)
		{
			return !(left == right);
		}

		public override int GetHashCode()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			return (Type, Tag, ModLoaders, GameVersion, SearchText, Sources).GetHashCode();
		}
	}

	public class SearchResult
	{
		public int CurseForgeOffset;

		public int CurseForgeTotal;

		public int ModrinthOffset;

		public int ModrinthTotal;

		public List<ResourceProject> Results;

		public string ErrorMessage;

		public SearchResult()
		{
			CurseForgeOffset = 0;
			CurseForgeTotal = -1;
			ModrinthOffset = 0;
			ModrinthTotal = -1;
			Results = new List<ResourceProject>();
			ErrorMessage = null;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D0
	{
		public List<string> _0024VB_0024Local_Candidates;

		public Func<string, bool> _0024VB_0024Local_CanForm;

		public _Closure_0024__6_002D0(_Closure_0024__6_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Candidates = arg0._0024VB_0024Local_Candidates;
				_0024VB_0024Local_CanForm = arg0._0024VB_0024Local_CanForm;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__4(string s)
		{
			_Closure_0024__6_002D1 arg = default(_Closure_0024__6_002D1);
			_Closure_0024__6_002D1 CS_0024_003C_003E8__locals0 = new _Closure_0024__6_002D1(arg)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2 = this,
				_0024VB_0024Local_s = s
			};
			if (!_0024VB_0024Local_Candidates.Contains(CS_0024_003C_003E8__locals0._0024VB_0024Local_s))
			{
				return _0024VB_0024Local_Candidates.Any([SpecialName] (string c) => CS_0024_003C_003E8__locals0._0024VB_0024Local_s.StartsWith(c) && CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CanForm(CS_0024_003C_003E8__locals0._0024VB_0024Local_s.Substring(c.Length)));
			}
			return true;
		}

		[SpecialName]
		internal bool _Lambda_0024__6(string w)
		{
			_Closure_0024__6_002D2 arg = default(_Closure_0024__6_002D2);
			_Closure_0024__6_002D2 CS_0024_003C_003E8__locals0 = new _Closure_0024__6_002D2(arg)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3 = this,
				_0024VB_0024Local_w = w
			};
			return !_0024VB_0024Local_Candidates.Any([SpecialName] (string c) => c.Length < CS_0024_003C_003E8__locals0._0024VB_0024Local_w.Length && CS_0024_003C_003E8__locals0._0024VB_0024Local_w.StartsWith(c) && CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_CanForm(CS_0024_003C_003E8__locals0._0024VB_0024Local_w.Substring(c.Length)));
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D1
	{
		public string _0024VB_0024Local_s;

		public _Closure_0024__6_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__6_002D1(_Closure_0024__6_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_s = arg0._0024VB_0024Local_s;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__5(string c)
		{
			if (_0024VB_0024Local_s.StartsWith(c))
			{
				return _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CanForm(_0024VB_0024Local_s.Substring(c.Length));
			}
			return false;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D2
	{
		public string _0024VB_0024Local_w;

		public _Closure_0024__6_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_3;

		public _Closure_0024__6_002D2(_Closure_0024__6_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_w = arg0._0024VB_0024Local_w;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__7(string c)
		{
			if (c.Length < _0024VB_0024Local_w.Length && _0024VB_0024Local_w.StartsWith(c))
			{
				return _0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_CanForm(_0024VB_0024Local_w.Substring(c.Length));
			}
			return false;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D3
	{
		public string _0024VB_0024Local_RawSearchText;

		public SearchRequest _0024VB_0024Local_Request;

		public string _0024VB_0024Local_CurseForgeAltSearchText;

		public bool _0024VB_0024Local_IgnoreModLoaderFilter;

		public ConcurrentBag<ResourceProject> _0024VB_0024Local_SearchResults;

		public SearchResult _0024VB_0024Local_Storage;

		public ConcurrentBag<(Exception Ex, ResourcePlatforms Source)> _0024VB_0024Local_Errors;

		public ModLoader.LoaderTask<SearchRequest, int> _0024VB_0024Local_Task;

		public string _0024VB_0024Local_ModrinthAltSearchText;

		public List<string> _0024VB_0024Local_ModrinthSlugs;

		public List<ResourceProject> _0024VB_0024Local_RealResults;

		public Func<ModBase.SearchSource, bool> _0024I12;

		public Action<ResourceProject> _0024I16;

		public Action<ResourceProject> _0024I18;

		public Func<JToken, bool> _0024I20;

		public Func<string, bool> _0024I21;

		public Action<ResourceProject> _0024I26;

		public _Closure_0024__6_002D3(_Closure_0024__6_002D3 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_RawSearchText = arg0._0024VB_0024Local_RawSearchText;
				_0024VB_0024Local_Request = arg0._0024VB_0024Local_Request;
				_0024VB_0024Local_CurseForgeAltSearchText = arg0._0024VB_0024Local_CurseForgeAltSearchText;
				_0024VB_0024Local_IgnoreModLoaderFilter = arg0._0024VB_0024Local_IgnoreModLoaderFilter;
				_0024VB_0024Local_SearchResults = arg0._0024VB_0024Local_SearchResults;
				_0024VB_0024Local_Storage = arg0._0024VB_0024Local_Storage;
				_0024VB_0024Local_Errors = arg0._0024VB_0024Local_Errors;
				_0024VB_0024Local_Task = arg0._0024VB_0024Local_Task;
				_0024VB_0024Local_ModrinthAltSearchText = arg0._0024VB_0024Local_ModrinthAltSearchText;
				_0024VB_0024Local_ModrinthSlugs = arg0._0024VB_0024Local_ModrinthSlugs;
				_0024VB_0024Local_RealResults = arg0._0024VB_0024Local_RealResults;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__12(ModBase.SearchSource s)
		{
			return s.Aliases.Contains(_0024VB_0024Local_RawSearchText);
		}

		[SpecialName]
		internal void _Lambda_0024__15()
		{
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Expected O, but got Unknown
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Invalid comparison between Unknown and I4
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a4: Invalid comparison between Unknown and I4
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				string curseForgeAddress = _0024VB_0024Local_Request.GetCurseForgeAddress(_0024VB_0024Local_CurseForgeAltSearchText ?? _0024VB_0024Local_RawSearchText, _0024VB_0024Local_IgnoreModLoaderFilter);
				Logger.Info($"开始 CurseForge 搜索：{curseForgeAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				JObject val = (JObject)ModDownload.DlModRequest(curseForgeAddress);
				List<ResourceProject> list = new List<ResourceProject>();
				foreach (JObject item in (IEnumerable<JToken>)val["data"])
				{
					JObject data = item;
					ResourceProject resourceProject = new ResourceProject(data);
					if (((int)_0024VB_0024Local_Request.Type != 4 || !resourceProject.Tags.Contains("数据包")) && ((int)_0024VB_0024Local_Request.Type == 127 || EnumUtils.HasFlagF<ResourceTypes>(resourceProject.Types, _0024VB_0024Local_Request.Type)))
					{
						list.Add(resourceProject);
					}
				}
				list.ForEach([SpecialName] (ResourceProject p) =>
				{
					_0024VB_0024Local_SearchResults.Add(p);
				});
				checked
				{
					_0024VB_0024Local_Storage.CurseForgeOffset += ((IEnumerable<JToken>)val["data"]).Count();
					_0024VB_0024Local_Storage.CurseForgeTotal = val["pagination"][(object)"totalCount"].ToObject<int>();
					Logger.Info($"从 CurseForge 搜索到了 {list.Count} 个工程（总计已获取 {_0024VB_0024Local_Storage.CurseForgeOffset} 个，共 {_0024VB_0024Local_Storage.CurseForgeTotal} 个）", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "CurseForge 搜索失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				_0024VB_0024Local_Storage.CurseForgeTotal = -1;
				_0024VB_0024Local_Errors.Add((ex2, (ResourcePlatforms)1));
				ProjectData.ClearProjectError();
			}
			if (_0024VB_0024Local_Task.Progress < 0.75)
			{
				_0024VB_0024Local_Task.Progress += 0.25;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__16(ResourceProject p)
		{
			_0024VB_0024Local_SearchResults.Add(p);
		}

		[SpecialName]
		internal void _Lambda_0024__17()
		{
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Expected O, but got Unknown
			checked
			{
				try
				{
					string modrinthAddress = _0024VB_0024Local_Request.GetModrinthAddress(_0024VB_0024Local_ModrinthAltSearchText ?? _0024VB_0024Local_RawSearchText, _0024VB_0024Local_IgnoreModLoaderFilter);
					Logger.Info($"开始 Modrinth 搜索：{modrinthAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					JObject val = (JObject)ModDownload.DlModRequest(modrinthAddress);
					List<ResourceProject> list = new List<ResourceProject>();
					foreach (JObject item in (IEnumerable<JToken>)val["hits"])
					{
						JObject data = item;
						list.Add(new ResourceProject(data));
					}
					list.ForEach([SpecialName] (ResourceProject p) =>
					{
						_0024VB_0024Local_SearchResults.Add(p);
					});
					_0024VB_0024Local_Storage.ModrinthOffset += ((IEnumerable<JToken>)val["hits"]).Count();
					_0024VB_0024Local_Storage.ModrinthTotal = val["total_hits"].ToObject<int>();
					Logger.Info($"从 Modrinth 搜索到了 {list.Count} 个工程（总计已获取 {_0024VB_0024Local_Storage.ModrinthOffset} 个，共 {_0024VB_0024Local_Storage.ModrinthTotal} 个）", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "Modrinth 搜索失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					_0024VB_0024Local_Storage.ModrinthTotal = -1;
					_0024VB_0024Local_Errors.Add((ex2, (ResourcePlatforms)2));
					ProjectData.ClearProjectError();
				}
				if (_0024VB_0024Local_Task.Progress < 0.75)
				{
					_0024VB_0024Local_Task.Progress += 0.25;
				}
			}
		}

		[SpecialName]
		internal void _Lambda_0024__18(ResourceProject p)
		{
			_0024VB_0024Local_SearchResults.Add(p);
		}

		[SpecialName]
		internal void _Lambda_0024__19()
		{
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Invalid comparison between Unknown and I4
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00de: Unknown result type (might be due to invalid IL or missing references)
			//IL_018c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				string text = string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", EnumerableExtensions.Join((IEnumerable)_0024VB_0024Local_ModrinthSlugs, "\",\""));
				Logger.Info($"开始 Modrinth 直接获取：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				List<ResourceProject> list = new List<ResourceProject>();
				foreach (JObject item in (IEnumerable<JToken>)ModDownload.DlModRequest(text))
				{
					JObject val = item;
					ResourceProject resourceProject = new ResourceProject(val);
					if (((int)_0024VB_0024Local_Request.Type == 127 || EnumUtils.HasFlagF<ResourceTypes>(resourceProject.Types, _0024VB_0024Local_Request.Type)) && (string.IsNullOrEmpty(_0024VB_0024Local_Request.Tag) || ((IEnumerable<JToken>)val["categories"]).Any([SpecialName] (JToken c) => Operators.CompareString(c.ToString(), StringExtensions.AfterLast(_0024VB_0024Local_Request.Tag, "/", false), TextCompare: false) == 0)) && ((int)_0024VB_0024Local_Request.ModLoaders == 0 || _0024VB_0024Local_IgnoreModLoaderFilter || EnumerableExtensions.Flags<ModLoaders>(resourceProject.ModLoaders).Intersect(EnumerableExtensions.Flags<ModLoaders>(_0024VB_0024Local_Request.ModLoaders)).Any()) && (string.IsNullOrEmpty(_0024VB_0024Local_Request.GameVersion) || resourceProject.UnsafeGameVersions.Any([SpecialName] (string d) => Operators.CompareString(d, _0024VB_0024Local_Request.GameVersion, TextCompare: false) == 0)))
					{
						list.Add(resourceProject);
					}
				}
				if (list.Any() && (int)_0024VB_0024Local_Request.ModLoaders != 0 && !_0024VB_0024Local_IgnoreModLoaderFilter && !string.IsNullOrEmpty(_0024VB_0024Local_Request.GameVersion))
				{
					_Closure_0024__6_002D4 arg = default(_Closure_0024__6_002D4);
					_Closure_0024__6_002D4 CS_0024_003C_003E8__locals0 = new _Closure_0024__6_002D4(arg);
					string arg2 = EnumerableExtensions.Join((IEnumerable)list.Select([SpecialName] (ResourceProject p) => p.Id), "\",\"project_id:");
					string arg3 = EnumerableExtensions.Join((IEnumerable)(from f in EnumerableExtensions.Flags<ModLoaders>(_0024VB_0024Local_Request.ModLoaders)
						select StringExtensions.Lower(((Enum)(ModLoaders)(ref f)).ToString())), "\",\"categories:");
					string url = $"https://api.modrinth.com/v2/search?limit={list.Count}&facets=" + $"[[\"project_id:{arg2}\"],[\"categories:{arg3}\"],[\"versions:{_0024VB_0024Local_Request.GameVersion}\"]]";
					CS_0024_003C_003E8__locals0._0024VB_0024Local_MatchedIds = ((IEnumerable<JToken>)ModDownload.DlModRequest(url)[(object)"hits"]).Select([SpecialName] (JToken h) => h[(object)"project_id"].ToString()).ToList();
					EnumerableExtensions.KeepIf<ResourceProject>((ICollection<ResourceProject>)list, (Func<ResourceProject, bool>)([SpecialName] (ResourceProject p) => CS_0024_003C_003E8__locals0._0024VB_0024Local_MatchedIds.Contains(p.Id)));
				}
				list.ForEach([SpecialName] (ResourceProject p) =>
				{
					_0024VB_0024Local_SearchResults.Add(p);
				});
				Logger.Info($"从 Modrinth 直接获取到了 {list.Count} 个工程", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				_0024VB_0024Local_ModrinthSlugs.Clear();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "Modrinth 直接获取失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				_0024VB_0024Local_Errors.Add((ex2, (ResourcePlatforms)2));
				ProjectData.ClearProjectError();
			}
			if (_0024VB_0024Local_Task.Progress < 0.75)
			{
				_0024VB_0024Local_Task.Progress += 0.25;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__20(JToken c)
		{
			return Operators.CompareString(c.ToString(), StringExtensions.AfterLast(_0024VB_0024Local_Request.Tag, "/", false), TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__21(string d)
		{
			return Operators.CompareString(d, _0024VB_0024Local_Request.GameVersion, TextCompare: false) == 0;
		}

		[SpecialName]
		internal void _Lambda_0024__26(ResourceProject p)
		{
			_0024VB_0024Local_SearchResults.Add(p);
		}

		[SpecialName]
		internal bool _Lambda_0024__32(ResourceProject r)
		{
			_Closure_0024__6_002D5 arg = default(_Closure_0024__6_002D5);
			_Closure_0024__6_002D5 CS_0024_003C_003E8__locals0 = new _Closure_0024__6_002D5(arg)
			{
				_0024VB_0024Local_r = r
			};
			if (!_0024VB_0024Local_RealResults.Any([SpecialName] (ResourceProject b) => CS_0024_003C_003E8__locals0._0024VB_0024Local_r.IsLike(b)))
			{
				return !_0024VB_0024Local_Storage.Results.Any([SpecialName] (ResourceProject b) => CS_0024_003C_003E8__locals0._0024VB_0024Local_r.IsLike(b));
			}
			return false;
		}

		[SpecialName]
		internal double _Lambda_0024__35(ResourceProject Project)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Invalid comparison between Unknown and I4
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Invalid comparison between Unknown and I4
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Invalid comparison between Unknown and I4
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Invalid comparison between Unknown and I4
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Invalid comparison between Unknown and I4
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Invalid comparison between Unknown and I4
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Invalid comparison between Unknown and I4
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Invalid comparison between Unknown and I4
			ResourceTypes type = _0024VB_0024Local_Request.Type;
			if ((int)type <= 4)
			{
				if (type - 1 <= 1)
				{
					return ((int)Project.Platform == 1) ? 1 : 5;
				}
				if ((int)type == 4)
				{
					goto IL_004a;
				}
			}
			else
			{
				if ((int)type == 8)
				{
					goto IL_004a;
				}
				if ((int)type == 16)
				{
					return ((int)Project.Platform != 1) ? 1 : 10;
				}
			}
			return 1.0;
			IL_004a:
			return ((int)Project.Platform == 1) ? 1 : 4;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D4
	{
		public List<string> _0024VB_0024Local_MatchedIds;

		public _Closure_0024__6_002D4(_Closure_0024__6_002D4 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_MatchedIds = arg0._0024VB_0024Local_MatchedIds;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__25(ResourceProject p)
		{
			return _0024VB_0024Local_MatchedIds.Contains(p.Id);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D5
	{
		public ResourceProject _0024VB_0024Local_r;

		public _Closure_0024__6_002D5(_Closure_0024__6_002D5 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_r = arg0._0024VB_0024Local_r;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__33(ResourceProject b)
		{
			return _0024VB_0024Local_r.IsLike(b);
		}

		[SpecialName]
		internal bool _Lambda_0024__34(ResourceProject b)
		{
			return _0024VB_0024Local_r.IsLike(b);
		}
	}

	private const int RESULT_PAGE_SIZE = 40;

	private const string MSG_NO_CHINESE_RESULT = "无搜索结果，请尝试搜索其英文名称";

	[SpecialName]
	private static List<ModBase.SearchEntry<WikiEntry>> _0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries_0024Init;

	[SpecialName]
	private static List<ModBase.SearchEntry<WikiEntry>> _0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries_0024Init;

	public static ModLoader.LoaderTask<SearchRequest, int> GetLoader(Func<SearchRequest> InputDelegate)
	{
		return new ModLoader.LoaderTask<SearchRequest, int>("社区资源获取", Run, InputDelegate)
		{
			ReloadTimeout = 60000
		};
	}

	private static void Run(ModLoader.LoaderTask<SearchRequest, int> Task)
	{
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Invalid comparison between Unknown and I4
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Invalid comparison between Unknown and I4
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Invalid comparison between Unknown and I4
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Invalid comparison between Unknown and I4
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Invalid comparison between Unknown and I4
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Invalid comparison between Unknown and I4
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0886: Invalid comparison between Unknown and I4
		_Closure_0024__6_002D3 arg = default(_Closure_0024__6_002D3);
		_Closure_0024__6_002D3 CS_0024_003C_003E8__locals0 = new _Closure_0024__6_002D3(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Task = Task;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Request = CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage = CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Storage;
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.Results.Count >= CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.TargetResultCount)
		{
			Logger.Info($"已有 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.Results.Count} 个结果，多于所需的 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.TargetResultCount} 个结果，结束处理", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
			return;
		}
		if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.CanContinue)
		{
			if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.Results.Any())
			{
				throw new Exception("没有符合条件的结果");
			}
			Logger.Info($"已有 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.Results.Count} 个结果，少于所需的 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.TargetResultCount} 个结果，但无法继续获取，结束处理", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
			return;
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText = (CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.SearchText ?? "").Trim();
		CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText = StringExtensions.Lower(CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText);
		Logger.Info($"工程列表搜索原始文本：{CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
		CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText = Strings.StrConv(CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText, VbStrConv.SimplifiedChinese);
		bool flag = StringExtensions.RegexCheck(CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText, "[\\u4e00-\\u9fbb]", RegexOptions.None) && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText) && ((int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Type == 1 || (int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Type == 16);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_CurseForgeAltSearchText = null;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthAltSearchText = null;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthSlugs = new List<string>();
		if (flag)
		{
			VB_0024AnonymousDelegate_2<ModBase.SearchEntry<WikiEntry>, ResourcePlatforms, IEnumerable<string>> vB_0024AnonymousDelegate_ = [SpecialName] (ModBase.SearchEntry<WikiEntry> Result, ResourcePlatforms Source) =>
			{
				//IL_001d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0036: Unknown result type (might be due to invalid IL or missing references)
				_Closure_0024__6_002D0 arg6 = default(_Closure_0024__6_002D0);
				_Closure_0024__6_002D0 CS_0024_003C_003E8__locals3 = new _Closure_0024__6_002D0(arg6);
				CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates = new List<string>();
				if (Result.Item.Slugs.ContainsKey(Source))
				{
					CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates.Add(Result.Item.Slugs[Source].Replace("-", " ").Replace("/", " "));
				}
				if (Result.Item.ChineseName != null)
				{
					CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates.Add(StringExtensions.BeforeFirst(StringExtensions.AfterLast(Result.Item.ChineseName, " (", false).TrimEnd(')'), " - ", false).Replace("-", " ").Replace("/", " ")
						.Replace(":", " ")
						.Replace("(", " ")
						.Replace(")", ""));
				}
				CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates = (from w in CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates.SelectMany([SpecialName] (string c) => StringExtensions.Split(c, " ", false))
					select StringExtensions.Lower(w.TrimStart('{').TrimEnd('}'))).Where([SpecialName] (string w) =>
				{
					if (w.Length <= 1)
					{
						return false;
					}
					if (new string[9] { "the", "of", "mod", "and", "forge", "fabric", "for", "quilt", "neoforge" }.Contains(w))
					{
						return false;
					}
					if (ModBase.Val(w) > 0.0)
					{
						return false;
					}
					return StringExtensions.IsAsciiOnly(w) ? true : false;
				}).Distinct().ToList();
				CS_0024_003C_003E8__locals3._0024VB_0024Local_CanForm = null;
				CS_0024_003C_003E8__locals3._0024VB_0024Local_CanForm = [SpecialName] (string s) =>
				{
					_Closure_0024__6_002D1 arg8 = default(_Closure_0024__6_002D1);
					_Closure_0024__6_002D1 CS_0024_003C_003E8__locals5 = new _Closure_0024__6_002D1(arg8);
					CS_0024_003C_003E8__locals5._0024VB_0024NonLocal__0024VB_0024Closure_2 = CS_0024_003C_003E8__locals3;
					CS_0024_003C_003E8__locals5._0024VB_0024Local_s = s;
					return CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates.Contains(CS_0024_003C_003E8__locals5._0024VB_0024Local_s) || CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates.Any([SpecialName] (string c) => CS_0024_003C_003E8__locals5._0024VB_0024Local_s.StartsWith(c) && CS_0024_003C_003E8__locals5._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CanForm(CS_0024_003C_003E8__locals5._0024VB_0024Local_s.Substring(c.Length)));
				};
				CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates = CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates.Where([SpecialName] (string w) =>
				{
					_Closure_0024__6_002D2 arg7 = default(_Closure_0024__6_002D2);
					_Closure_0024__6_002D2 CS_0024_003C_003E8__locals4 = new _Closure_0024__6_002D2(arg7);
					CS_0024_003C_003E8__locals4._0024VB_0024NonLocal__0024VB_0024Closure_3 = CS_0024_003C_003E8__locals3;
					CS_0024_003C_003E8__locals4._0024VB_0024Local_w = w;
					return !CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates.Any([SpecialName] (string c) => c.Length < CS_0024_003C_003E8__locals4._0024VB_0024Local_w.Length && CS_0024_003C_003E8__locals4._0024VB_0024Local_w.StartsWith(c) && CS_0024_003C_003E8__locals4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_CanForm(CS_0024_003C_003E8__locals4._0024VB_0024Local_w.Substring(c.Length)));
				}).ToList();
				return CS_0024_003C_003E8__locals3._0024VB_0024Local_Candidates;
			};
			VB_0024AnonymousDelegate_3<ResourcePlatforms, IEnumerable<ModBase.SearchEntry<WikiEntry>>> vB_0024AnonymousDelegate_2 = [SpecialName] [IteratorStateMachine(typeof(_Closure_0024__.VB_0024StateMachine___Lambda_0024__6_002D8))] (ResourcePlatforms Source) =>
			{
				//IL_000f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				foreach (WikiEntry item in WikiEntry.All.Value)
				{
					if (item.Slugs.ContainsKey(Source))
					{
						if (item.ChineseName != null)
						{
							yield return new ModBase.SearchEntry<WikiEntry>
							{
								Item = item,
								SearchSource = new List<ModBase.SearchSource>
								{
									new ModBase.SearchSource(StringExtensions.Split(Strings.StrConv(StringExtensions.BeforeFirst(item.ChineseName, " (", false), VbStrConv.SimplifiedChinese), '/', true)),
									new ModBase.SearchSource(Strings.StrConv(StringExtensions.AfterFirst(item.ChineseName, " (", false), VbStrConv.SimplifiedChinese) + item.Slugs[Source], 0.5)
								}
							};
						}
						else
						{
							yield return new ModBase.SearchEntry<WikiEntry>
							{
								Item = item,
								SearchSource = new List<ModBase.SearchSource>
								{
									new ModBase.SearchSource(item.Slugs[Source], 0.5)
								}
							};
						}
					}
				}
			};
			if (EnumUtils.HasFlagF<ResourcePlatforms>(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Sources, (ResourcePlatforms)1))
			{
				if (_0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries_0024Init == null)
				{
					Interlocked.CompareExchange(ref _0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries_0024Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken = false;
				try
				{
					Monitor.Enter(_0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries_0024Init, ref lockTaken);
					if (_0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries_0024Init.State == 0)
					{
						_0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries_0024Init.State = 2;
						_0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries = vB_0024AnonymousDelegate_2((ResourcePlatforms)1).ToList();
					}
					else if (_0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries_0024Init.State = 1;
					if (lockTaken)
					{
						Monitor.Exit(_0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries_0024Init);
					}
				}
				List<ModBase.SearchEntry<WikiEntry>> list = ModBase.Search(_0024STATIC_0024Run_0024011151286FC21283B48_0024CurseForgeSearchEntries, CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText, 100, 0.25);
				if (list.Any())
				{
					ModBase.SearchEntry<WikiEntry> result = EnumerableExtensions.MaxBy<ModBase.SearchEntry<WikiEntry>, int>((IEnumerable<ModBase.SearchEntry<WikiEntry>>)(list.First().AbsoluteRight ? list.Where([SpecialName] (ModBase.SearchEntry<WikiEntry> s) => s.AbsoluteRight).ToList() : EnumerableExtensions.MaxByAll<ModBase.SearchEntry<WikiEntry>, double>((IEnumerable<ModBase.SearchEntry<WikiEntry>>)list, (Func<ModBase.SearchEntry<WikiEntry>, double>)([SpecialName] (ModBase.SearchEntry<WikiEntry> s) => s.Similarity))), (Func<ModBase.SearchEntry<WikiEntry>, int>)([SpecialName] (ModBase.SearchEntry<WikiEntry> s) => s.Item.Popularity));
					CS_0024_003C_003E8__locals0._0024VB_0024Local_CurseForgeAltSearchText = EnumerableExtensions.Join((IEnumerable)vB_0024AnonymousDelegate_(result, (ResourcePlatforms)1), " ");
					Logger.Warn($"中文搜索关键词（CurseForge）：{CS_0024_003C_003E8__locals0._0024VB_0024Local_CurseForgeAltSearchText}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				}
			}
			if (EnumUtils.HasFlagF<ResourcePlatforms>(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Sources, (ResourcePlatforms)2))
			{
				if (_0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries_0024Init == null)
				{
					Interlocked.CompareExchange(ref _0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries_0024Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken2 = false;
				try
				{
					Monitor.Enter(_0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries_0024Init, ref lockTaken2);
					if (_0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries_0024Init.State == 0)
					{
						_0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries_0024Init.State = 2;
						_0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries = vB_0024AnonymousDelegate_2((ResourcePlatforms)2).ToList();
					}
					else if (_0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries_0024Init.State = 1;
					if (lockTaken2)
					{
						Monitor.Exit(_0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries_0024Init);
					}
				}
				List<ModBase.SearchEntry<WikiEntry>> list2 = ModBase.Search(_0024STATIC_0024Run_0024011151286FC21283B48_0024ModrinthSearchEntries, CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText, 100, 0.25);
				if (list2.Any())
				{
					Dictionary<string, double> dictionary = new Dictionary<string, double>();
					foreach (ModBase.SearchEntry<WikiEntry> item2 in list2)
					{
						foreach (string item3 in vB_0024AnonymousDelegate_(item2, (ResourcePlatforms)2))
						{
							if (!dictionary.ContainsKey(item3))
							{
								dictionary.Add(item3, 0.0);
							}
							double num = (item2.SearchSource.Any([SpecialName] (ModBase.SearchSource s) => s.Aliases.Contains(CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText)) ? 1000.0 : item2.Similarity);
							dictionary[item3] += num * (double)item2.Item.Popularity;
						}
					}
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthAltSearchText = EnumerableExtensions.MaxBy<KeyValuePair<string, double>, double>((IEnumerable<KeyValuePair<string, double>>)dictionary, (Func<KeyValuePair<string, double>, double>)([SpecialName] (KeyValuePair<string, double> w) => w.Value)).Key;
					Logger.Warn($"中文搜索关键词（Modrinth）：{CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthAltSearchText}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthSlugs = (from r in list2.Take(100)
						select r.Item.Slugs[(ResourcePlatforms)2]).ToList();
				}
			}
			if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_CurseForgeAltSearchText) && string.IsNullOrEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthAltSearchText) && !CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthSlugs.Any())
			{
				throw new Exception("无搜索结果，请尝试搜索其英文名称");
			}
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Progress = 0.05;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_RealResults = new List<ResourceProject>();
		while (true)
		{
			List<ResourceProject> list3 = new List<ResourceProject>();
			List<Thread> list4 = new List<Thread>();
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Errors = new ConcurrentBag<(Exception, ResourcePlatforms)>();
			CS_0024_003C_003E8__locals0._0024VB_0024Local_IgnoreModLoaderFilter = (int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.ModLoaders == 1 && McVersion.VersionToDrop(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.GameVersion) < 140;
			CS_0024_003C_003E8__locals0._0024VB_0024Local_SearchResults = new ConcurrentBag<ResourceProject>();
			try
			{
				checked
				{
					if (EnumUtils.HasFlagF<ResourcePlatforms>(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Sources, (ResourcePlatforms)1) && (CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.CurseForgeTotal <= -1 || CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.CurseForgeTotal > CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.CurseForgeOffset) && (!flag || (flag && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_CurseForgeAltSearchText))))
					{
						list4.Add(ModBase.RunInNewThread([SpecialName] () =>
						{
							//IL_0041: Unknown result type (might be due to invalid IL or missing references)
							//IL_0047: Expected O, but got Unknown
							//IL_0066: Unknown result type (might be due to invalid IL or missing references)
							//IL_006d: Expected O, but got Unknown
							//IL_007c: Unknown result type (might be due to invalid IL or missing references)
							//IL_0082: Invalid comparison between Unknown and I4
							//IL_009d: Unknown result type (might be due to invalid IL or missing references)
							//IL_00a4: Invalid comparison between Unknown and I4
							//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
							//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
							try
							{
								string curseForgeAddress = CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.GetCurseForgeAddress(CS_0024_003C_003E8__locals0._0024VB_0024Local_CurseForgeAltSearchText ?? CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText, CS_0024_003C_003E8__locals0._0024VB_0024Local_IgnoreModLoaderFilter);
								Logger.Info($"开始 CurseForge 搜索：{curseForgeAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
								JObject val3 = (JObject)ModDownload.DlModRequest(curseForgeAddress);
								List<ResourceProject> list9 = new List<ResourceProject>();
								foreach (JObject item4 in (IEnumerable<JToken>)val3["data"])
								{
									JObject data2 = item4;
									ResourceProject resourceProject2 = new ResourceProject(data2);
									if (unchecked(((int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Type != 4 || !resourceProject2.Tags.Contains("数据包")) && ((int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Type == 127 || EnumUtils.HasFlagF<ResourceTypes>(resourceProject2.Types, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Type))))
									{
										list9.Add(resourceProject2);
									}
								}
								list9.ForEach([SpecialName] (ResourceProject p) =>
								{
									CS_0024_003C_003E8__locals0._0024VB_0024Local_SearchResults.Add(p);
								});
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.CurseForgeOffset += ((IEnumerable<JToken>)val3["data"]).Count();
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.CurseForgeTotal = val3["pagination"][(object)"totalCount"].ToObject<int>();
								Logger.Info($"从 CurseForge 搜索到了 {list9.Count} 个工程（总计已获取 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.CurseForgeOffset} 个，共 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.CurseForgeTotal} 个）", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
							}
							catch (Exception ex5)
							{
								ProjectData.SetProjectError(ex5);
								Exception ex6 = ex5;
								Logger.Warn(ex6, "CurseForge 搜索失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.CurseForgeTotal = -1;
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Errors.Add((ex6, (ResourcePlatforms)1));
								ProjectData.ClearProjectError();
							}
							if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Progress < 0.75)
							{
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Progress += 0.25;
							}
						}, "CurseForge Search"));
					}
					if (EnumUtils.HasFlagF<ResourcePlatforms>(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Sources, (ResourcePlatforms)2) && (CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthTotal <= -1 || CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthTotal > CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthOffset) && (!flag || (flag && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthAltSearchText))))
					{
						list4.Add(ModBase.RunInNewThread([SpecialName] () =>
						{
							//IL_0041: Unknown result type (might be due to invalid IL or missing references)
							//IL_0047: Expected O, but got Unknown
							//IL_0066: Unknown result type (might be due to invalid IL or missing references)
							//IL_006d: Expected O, but got Unknown
							try
							{
								string modrinthAddress = CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.GetModrinthAddress(CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthAltSearchText ?? CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText, CS_0024_003C_003E8__locals0._0024VB_0024Local_IgnoreModLoaderFilter);
								Logger.Info($"开始 Modrinth 搜索：{modrinthAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
								JObject val2 = (JObject)ModDownload.DlModRequest(modrinthAddress);
								List<ResourceProject> list8 = new List<ResourceProject>();
								foreach (JObject item5 in (IEnumerable<JToken>)val2["hits"])
								{
									JObject data = item5;
									list8.Add(new ResourceProject(data));
								}
								list8.ForEach([SpecialName] (ResourceProject p) =>
								{
									CS_0024_003C_003E8__locals0._0024VB_0024Local_SearchResults.Add(p);
								});
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthOffset += ((IEnumerable<JToken>)val2["hits"]).Count();
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthTotal = val2["total_hits"].ToObject<int>();
								Logger.Info($"从 Modrinth 搜索到了 {list8.Count} 个工程（总计已获取 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthOffset} 个，共 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthTotal} 个）", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
							}
							catch (Exception ex3)
							{
								ProjectData.SetProjectError(ex3);
								Exception ex4 = ex3;
								Logger.Warn(ex4, "Modrinth 搜索失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthTotal = -1;
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Errors.Add((ex4, (ResourcePlatforms)2));
								ProjectData.ClearProjectError();
							}
							if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Progress < 0.75)
							{
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Progress += 0.25;
							}
						}, "Modrinth Search"));
					}
				}
				if (EnumUtils.HasFlagF<ResourcePlatforms>(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Sources, (ResourcePlatforms)2) && (CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthTotal <= -1 || CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthTotal > CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ModrinthOffset) && CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthSlugs.Any())
				{
					list4.Add(ModBase.RunInNewThread([SpecialName] () =>
					{
						//IL_0051: Unknown result type (might be due to invalid IL or missing references)
						//IL_0057: Expected O, but got Unknown
						//IL_0065: Unknown result type (might be due to invalid IL or missing references)
						//IL_006c: Invalid comparison between Unknown and I4
						//IL_0070: Unknown result type (might be due to invalid IL or missing references)
						//IL_007b: Unknown result type (might be due to invalid IL or missing references)
						//IL_00de: Unknown result type (might be due to invalid IL or missing references)
						//IL_018c: Unknown result type (might be due to invalid IL or missing references)
						//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
						//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
						//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
						try
						{
							string text = string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", EnumerableExtensions.Join((IEnumerable)CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthSlugs, "\",\""));
							Logger.Info($"开始 Modrinth 直接获取：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
							List<ResourceProject> list7 = new List<ResourceProject>();
							foreach (JObject item6 in (IEnumerable<JToken>)ModDownload.DlModRequest(text))
							{
								JObject val = item6;
								ResourceProject resourceProject = new ResourceProject(val);
								if (((int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Type == 127 || EnumUtils.HasFlagF<ResourceTypes>(resourceProject.Types, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Type)) && (string.IsNullOrEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Tag) || ((IEnumerable<JToken>)val["categories"]).Any([SpecialName] (JToken c) => Operators.CompareString(c.ToString(), StringExtensions.AfterLast(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Tag, "/", false), TextCompare: false) == 0)) && ((int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.ModLoaders == 0 || CS_0024_003C_003E8__locals0._0024VB_0024Local_IgnoreModLoaderFilter || EnumerableExtensions.Flags<ModLoaders>(resourceProject.ModLoaders).Intersect(EnumerableExtensions.Flags<ModLoaders>(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.ModLoaders)).Any()) && (string.IsNullOrEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.GameVersion) || resourceProject.UnsafeGameVersions.Any([SpecialName] (string d) => Operators.CompareString(d, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.GameVersion, TextCompare: false) == 0)))
								{
									list7.Add(resourceProject);
								}
							}
							if (list7.Any() && (int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.ModLoaders != 0 && !CS_0024_003C_003E8__locals0._0024VB_0024Local_IgnoreModLoaderFilter && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.GameVersion))
							{
								_Closure_0024__6_002D4 arg3 = default(_Closure_0024__6_002D4);
								_Closure_0024__6_002D4 CS_0024_003C_003E8__locals2 = new _Closure_0024__6_002D4(arg3);
								string arg4 = EnumerableExtensions.Join((IEnumerable)list7.Select([SpecialName] (ResourceProject p) => p.Id), "\",\"project_id:");
								string arg5 = EnumerableExtensions.Join((IEnumerable)(from f in EnumerableExtensions.Flags<ModLoaders>(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.ModLoaders)
									select StringExtensions.Lower(((Enum)(ModLoaders)(ref f)).ToString())), "\",\"categories:");
								string url = $"https://api.modrinth.com/v2/search?limit={list7.Count}&facets=" + $"[[\"project_id:{arg4}\"],[\"categories:{arg5}\"],[\"versions:{CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.GameVersion}\"]]";
								CS_0024_003C_003E8__locals2._0024VB_0024Local_MatchedIds = ((IEnumerable<JToken>)ModDownload.DlModRequest(url)[(object)"hits"]).Select([SpecialName] (JToken h) => h[(object)"project_id"].ToString()).ToList();
								EnumerableExtensions.KeepIf<ResourceProject>((ICollection<ResourceProject>)list7, (Func<ResourceProject, bool>)([SpecialName] (ResourceProject p) => CS_0024_003C_003E8__locals2._0024VB_0024Local_MatchedIds.Contains(p.Id)));
							}
							list7.ForEach([SpecialName] (ResourceProject p) =>
							{
								CS_0024_003C_003E8__locals0._0024VB_0024Local_SearchResults.Add(p);
							});
							Logger.Info($"从 Modrinth 直接获取到了 {list7.Count} 个工程", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
							CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthSlugs.Clear();
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							Logger.Warn(ex2, "Modrinth 直接获取失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
							CS_0024_003C_003E8__locals0._0024VB_0024Local_Errors.Add((ex2, (ResourcePlatforms)2));
							ProjectData.ClearProjectError();
						}
						if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Progress < 0.75)
						{
							CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Progress += 0.25;
						}
					}, "Modrinth Get"));
				}
				foreach (Thread item7 in list4)
				{
					item7.Join();
					if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.IsCanceled)
					{
						return;
					}
				}
				list3 = new List<ResourceProject>(CS_0024_003C_003E8__locals0._0024VB_0024Local_SearchResults);
				if (CS_0024_003C_003E8__locals0._0024VB_0024Local_IgnoreModLoaderFilter)
				{
					EnumerableExtensions.KeepIf<ResourceProject>((ICollection<ResourceProject>)list3, (Func<ResourceProject, bool>)([SpecialName] (ResourceProject p) => (int)p.ModLoaders == 0 || EnumUtils.HasFlagF<ModLoaders>(p.ModLoaders, (ModLoaders)1)));
				}
				CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ErrorMessage = null;
				if (!list3.Any())
				{
					if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Errors.Any())
					{
						throw CS_0024_003C_003E8__locals0._0024VB_0024Local_Errors.First().Ex;
					}
					if (flag && (int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Type != 1 && (int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Type != 16)
					{
						throw new Exception("无搜索结果，请尝试搜索其英文名称");
					}
					if ((int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Sources == 1 && StringExtensions.StartsWithF(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Tag, "/", false))
					{
						throw new Exception("CurseForge 不兼容所选的类型");
					}
					if ((int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Sources == 2 && StringExtensions.EndsWithF(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Tag, "/", false))
					{
						throw new Exception("Modrinth 不兼容所选的类型");
					}
					throw new Exception("没有搜索结果");
				}
				if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Errors.Any())
				{
					if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Errors.Any([SpecialName] ((Exception Ex, ResourcePlatforms Source) e) => (int)e.Source == 1))
					{
						CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ErrorMessage = string.Format("无法连接到 CurseForge，所以目前仅显示了来自 Modrinth 的内容，搜索结果可能不全。{0}请稍后再试，或使用 VPN 改善网络环境。", "\r\n");
					}
					else
					{
						CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.ErrorMessage = string.Format("无法连接到 Modrinth，所以目前仅显示了来自 CurseForge 的内容，搜索结果可能不全。{0}请稍后再试，或使用 VPN 改善网络环境。", "\r\n");
					}
				}
			}
			finally
			{
				foreach (Thread item8 in list4)
				{
					if (item8.IsAlive)
					{
						item8.Interrupt();
					}
				}
			}
			list3 = list3.Where([SpecialName] (ResourceProject x) => (int)x.Platform == 1).Concat(list3.Where([SpecialName] (ResourceProject x) => (int)x.Platform != 1)).ToList();
			list3 = EnumerableExtensions.Distinct<ResourceProject>((IList<ResourceProject>)list3, (Func<ResourceProject, ResourceProject, bool>)([SpecialName] (ResourceProject a, ResourceProject b) => a.IsLike(b))).ToList();
			list3 = list3.Where([SpecialName] (ResourceProject r) =>
			{
				_Closure_0024__6_002D5 arg2 = default(_Closure_0024__6_002D5);
				_Closure_0024__6_002D5 CS_0024_003C_003E8__locals1 = new _Closure_0024__6_002D5(arg2);
				CS_0024_003C_003E8__locals1._0024VB_0024Local_r = r;
				return !CS_0024_003C_003E8__locals0._0024VB_0024Local_RealResults.Any([SpecialName] (ResourceProject b) => CS_0024_003C_003E8__locals1._0024VB_0024Local_r.IsLike(b)) && !CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.Results.Any([SpecialName] (ResourceProject b) => CS_0024_003C_003E8__locals1._0024VB_0024Local_r.IsLike(b));
			}).ToList();
			CS_0024_003C_003E8__locals0._0024VB_0024Local_RealResults.AddRange(list3);
			Logger.Info($"去重、筛选后累计新增结果 {CS_0024_003C_003E8__locals0._0024VB_0024Local_RealResults.Count} 个（目前已有结果 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.Results.Count} 个）", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
			checked
			{
				if (CS_0024_003C_003E8__locals0._0024VB_0024Local_RealResults.Count + CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.Results.Count >= CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.TargetResultCount)
				{
					break;
				}
				Logger.Info($"总结果数需求最少 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.TargetResultCount} 个，仅获得了 {CS_0024_003C_003E8__locals0._0024VB_0024Local_RealResults.Count + CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.Results.Count} 个", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.CanContinue && !CS_0024_003C_003E8__locals0._0024VB_0024Local_Errors.Any())
				{
					Logger.Info("将继续尝试加载下一页", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
					continue;
				}
				Logger.Info("无法继续加载，将强制结束", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\ResourceSearcher.vb");
				break;
			}
		}
		Dictionary<ResourceProject, double> dictionary2 = new Dictionary<ResourceProject, double>();
		VB_0024AnonymousDelegate_4<ResourceProject, double> vB_0024AnonymousDelegate_3 = [SpecialName] (ResourceProject Project) =>
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Invalid comparison between Unknown and I4
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Invalid comparison between Unknown and I4
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Invalid comparison between Unknown and I4
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Invalid comparison between Unknown and I4
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Invalid comparison between Unknown and I4
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Invalid comparison between Unknown and I4
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Invalid comparison between Unknown and I4
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Invalid comparison between Unknown and I4
			ResourceTypes type = CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Type;
			if ((int)type <= 4)
			{
				if (type - 1 <= 1)
				{
					return ((int)Project.Platform == 1) ? 1 : 5;
				}
				if ((int)type == 4)
				{
					goto IL_004a;
				}
			}
			else
			{
				if ((int)type == 8)
				{
					goto IL_004a;
				}
				if ((int)type == 16)
				{
					return ((int)Project.Platform != 1) ? 1 : 10;
				}
			}
			return 1.0;
			IL_004a:
			return ((int)Project.Platform == 1) ? 1 : 4;
		};
		if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText))
		{
			foreach (ResourceProject _0024VB_0024Local_RealResult in CS_0024_003C_003E8__locals0._0024VB_0024Local_RealResults)
			{
				dictionary2[_0024VB_0024Local_RealResult] = (double)_0024VB_0024Local_RealResult.DownloadCount * vB_0024AnonymousDelegate_3(_0024VB_0024Local_RealResult);
			}
		}
		else
		{
			List<ModBase.SearchEntry<ResourceProject>> list5 = new List<ModBase.SearchEntry<ResourceProject>>();
			foreach (ResourceProject _0024VB_0024Local_RealResult2 in CS_0024_003C_003E8__locals0._0024VB_0024Local_RealResults)
			{
				dictionary2[_0024VB_0024Local_RealResult2] = ((_0024VB_0024Local_RealResult2.WikiId > 0) ? 0.2 : 0.0) + Math.Log10((double)Math.Max(_0024VB_0024Local_RealResult2.DownloadCount, 1) * vB_0024AnonymousDelegate_3(_0024VB_0024Local_RealResult2)) / 9.0;
				list5.Add(new ModBase.SearchEntry<ResourceProject>
				{
					Item = _0024VB_0024Local_RealResult2,
					SearchSource = new List<ModBase.SearchSource>
					{
						new ModBase.SearchSource(StringExtensions.Split(flag ? _0024VB_0024Local_RealResult2.TranslatedName : _0024VB_0024Local_RealResult2.RawName, '/', true)),
						new ModBase.SearchSource(_0024VB_0024Local_RealResult2.Description, 0.05)
					}
				});
			}
			List<ModBase.SearchEntry<ResourceProject>> list6 = ModBase.Search(list5, CS_0024_003C_003E8__locals0._0024VB_0024Local_RawSearchText, 10000, -1.0);
			foreach (ModBase.SearchEntry<ResourceProject> item9 in list6)
			{
				dictionary2[item9.Item] += (item9.AbsoluteRight ? 10.0 : item9.Similarity) / (list6.First().AbsoluteRight ? 10.0 : list6.First().Similarity);
			}
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.IsCanceled)
		{
			throw new OperationCanceledException();
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Storage.Results.AddRange(from s in dictionary2
			orderby s.Value descending
			select s into r
			select r.Key);
	}
}
