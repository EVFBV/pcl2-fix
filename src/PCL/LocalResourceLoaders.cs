using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCLCS;

namespace PCL;

[StandardModule]
public sealed class LocalResourceLoaders
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__2_002D0
	{
		public LocalResourceFile _0024VB_0024Local_ModEntry;

		public _Closure_0024__2_002D0(_Closure_0024__2_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_ModEntry = arg0._0024VB_0024Local_ModEntry;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__4(LocalResourceFile m)
		{
			return Operators.CompareString(m.EnabledName, _0024VB_0024Local_ModEntry.EnabledName, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__4_002D0
	{
		public List<LocalResourceFile> _0024VB_0024Local_Mods;

		public ModLoader.LoaderTask<(List<LocalResourceFile> ModList, JObject Cache, McVersion Target), int> _0024VB_0024Local_Loader;

		public Thread _0024VB_0024Local_CurrentTaskThread;

		public string _0024VB_0024Local_VanillaVersion;

		public bool _0024VB_0024Local_IsFailed;

		public int _0024VB_0024Local_EndedThreadCount;

		public _Closure_0024__4_002D0(_Closure_0024__4_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Mods = arg0._0024VB_0024Local_Mods;
				_0024VB_0024Local_Loader = arg0._0024VB_0024Local_Loader;
				_0024VB_0024Local_CurrentTaskThread = arg0._0024VB_0024Local_CurrentTaskThread;
				_0024VB_0024Local_VanillaVersion = arg0._0024VB_0024Local_VanillaVersion;
				_0024VB_0024Local_IsFailed = arg0._0024VB_0024Local_IsFailed;
				_0024VB_0024Local_EndedThreadCount = arg0._0024VB_0024Local_EndedThreadCount;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__2()
		{
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Expected O, but got Unknown
			//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Expected O, but got Unknown
			//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01af: Expected O, but got Unknown
			//IL_0313: Unknown result type (might be due to invalid IL or missing references)
			//IL_031d: Expected O, but got Unknown
			//IL_0401: Unknown result type (might be due to invalid IL or missing references)
			//IL_0408: Expected O, but got Unknown
			//IL_0490: Unknown result type (might be due to invalid IL or missing references)
			//IL_049b: Expected O, but got Unknown
			checked
			{
				try
				{
					_Closure_0024__4_002D1 arg = default(_Closure_0024__4_002D1);
					_Closure_0024__4_002D1 CS_0024_003C_003E8__locals0 = new _Closure_0024__4_002D1(arg)
					{
						_0024VB_0024NonLocal__0024VB_0024Closure_2 = this
					};
					List<string> list = _0024VB_0024Local_Mods.Select([SpecialName] (LocalResourceFile m) => m.ModrinthHash).ToList();
					JObject val = (JObject)ModDownload.DlModRequest("https://api.modrinth.com/v2/version_files", HttpMethod.Post, string.Format("{{\"hashes\": [\"{0}\"], \"algorithm\": \"sha1\"}}", EnumerableExtensions.Join((IEnumerable)list, "\",\"")), "application/json");
					Logger.Info($"从 Modrinth 获取到 {((JContainer)val).Count} 个本地 Mod 的对应信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					if (((JContainer)val).Count == 0)
					{
						return;
					}
					Dictionary<string, List<LocalResourceFile>> dictionary = new Dictionary<string, List<LocalResourceFile>>();
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthLoaders = new Dictionary<LocalResourceFile, List<string>>();
					foreach (LocalResourceFile _0024VB_0024Local_Mod in _0024VB_0024Local_Mods)
					{
						if (val.ContainsKey(_0024VB_0024Local_Mod.ModrinthHash) && !((string)val[_0024VB_0024Local_Mod.ModrinthHash][(object)"files"][(object)0][(object)"hashes"][(object)"sha1"] != _0024VB_0024Local_Mod.ModrinthHash))
						{
							string key = val[_0024VB_0024Local_Mod.ModrinthHash][(object)"project_id"].ToString();
							if (ResourceProject.Cache.ContainsKey(key) && (object)_0024VB_0024Local_Mod.Project == null)
							{
								_0024VB_0024Local_Mod.Project = ResourceProject.Cache[key];
							}
							if (!dictionary.ContainsKey(key))
							{
								dictionary[key] = new List<LocalResourceFile>();
							}
							dictionary[key].Add(_0024VB_0024Local_Mod);
							ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)val[_0024VB_0024Local_Mod.ModrinthHash], (ResourceTypes)1);
							CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthLoaders[_0024VB_0024Local_Mod] = (from l in ((IEnumerable<JToken>)val[_0024VB_0024Local_Mod.ModrinthHash][(object)"loaders"]).Select([SpecialName] (JToken l) => StringExtensions.Lower(l.ToString())).Distinct()
								orderby l
								select l).ToList();
							if (_0024VB_0024Local_Mod.ProjectVersion == null || DateTime.Compare(_0024VB_0024Local_Mod.ProjectVersion.ReleaseDate, resourceVersion.ReleaseDate) < 0)
							{
								_0024VB_0024Local_Mod.ProjectVersion = resourceVersion;
							}
							else
							{
								_0024VB_0024Local_Mod.ProjectVersion.Version = resourceVersion.Version;
							}
						}
					}
					if (_0024VB_0024Local_Loader.IsCanceledWithThread(_0024VB_0024Local_CurrentTaskThread))
					{
						return;
					}
					Logger.Info($"需要从 Modrinth 获取 {dictionary.Count} 个本地 Mod 的工程信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					if (!dictionary.Any())
					{
						return;
					}
					JArray val2 = (JArray)ModDownload.DlModRequest(string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", EnumerableExtensions.Join((IEnumerable)dictionary.Keys, "\",\"")));
					foreach (JToken item in val2)
					{
						ResourceProject resourceProject = new ResourceProject((JObject)item);
						foreach (LocalResourceFile item2 in dictionary[resourceProject.Id])
						{
							item2.Project = resourceProject;
						}
					}
					Logger.Info("已从 Modrinth 获取本地 Mod 信息，继续获取更新信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					List<LocalResourceFile> source = dictionary.SelectMany([SpecialName] (KeyValuePair<string, List<LocalResourceFile>> l) => l.Value).ToList();
					if (!source.Any())
					{
						return;
					}
					string arg2 = EnumerableExtensions.Join((IEnumerable)source.Select([SpecialName] (LocalResourceFile m) => string.Format("{{\"hash\": \"{0}\", \"loaders\": [\"{1}\"], \"game_versions\": [\"{2}\"]}}", m.ModrinthHash, EnumerableExtensions.Join((IEnumerable)CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthLoaders[m], "\",\""), CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_VanillaVersion)), ",");
					JObject val3 = (JObject)ModDownload.DlModRequest("https://api.modrinth.com/v2/version_files/update_individual", HttpMethod.Post, $"{{\"algorithm\": \"sha1\", \"hashes\": [{arg2}]}}", "application/json");
					using (List<LocalResourceFile>.Enumerator enumerator4 = _0024VB_0024Local_Mods.GetEnumerator())
					{
						_Closure_0024__4_002D2 closure_0024__4_002D = default(_Closure_0024__4_002D2);
						_Closure_0024__4_002D3 closure_0024__4_002D2 = default(_Closure_0024__4_002D3);
						while (enumerator4.MoveNext())
						{
							closure_0024__4_002D = new _Closure_0024__4_002D2(closure_0024__4_002D)
							{
								_0024VB_0024Local_Entry = enumerator4.Current
							};
							closure_0024__4_002D2 = new _Closure_0024__4_002D3(closure_0024__4_002D2)
							{
								_0024VB_0024NonLocal__0024VB_0024Closure_3 = closure_0024__4_002D
							};
							if (!val3.ContainsKey(closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ModrinthHash) || closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ProjectVersion == null)
							{
								continue;
							}
							closure_0024__4_002D2._0024VB_0024Local_UpdateFile = ResourceVersion.FromPlatformJson((JObject)val3[closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ModrinthHash], (ResourceTypes)1);
							if (!closure_0024__4_002D2._0024VB_0024Local_UpdateFile.DownloadAvailable || !(from l in ((IEnumerable<JToken>)val3[closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ModrinthHash][(object)"loaders"]).Select([SpecialName] (JToken l) => StringExtensions.Lower(l.ToString())).Distinct()
								orderby l
								select l).ToList().SequenceEqual(CS_0024_003C_003E8__locals0._0024VB_0024Local_ModrinthLoaders[closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry]))
							{
								continue;
							}
							Logger.Trace((Func<string>)closure_0024__4_002D2._Lambda_0024__10, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
							if (DateTime.Compare(closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ProjectVersion.ReleaseDate, closure_0024__4_002D2._0024VB_0024Local_UpdateFile.ReleaseDate) < 0 && Operators.CompareString(closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ProjectVersion.Hash, closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Hash, TextCompare: false) != 0)
							{
								if (closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile != null && Operators.CompareString(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Hash, closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile.Hash, TextCompare: false) == 0)
								{
									closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ChangelogUrls.Add(string.Format("https://modrinth.com/mod/{0}/changelog?g={1}", val3[closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ModrinthHash][(object)"project_id"], _0024VB_0024Local_VanillaVersion));
									closure_0024__4_002D2._0024VB_0024Local_UpdateFile.DownloadUrls.AddRange(closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile.DownloadUrls);
									closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile = closure_0024__4_002D2._0024VB_0024Local_UpdateFile;
								}
								else if (closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile == null || DateTime.Compare(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.ReleaseDate, closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile.ReleaseDate) >= 0)
								{
									closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ChangelogUrls = new List<string> { string.Format("https://modrinth.com/mod/{0}/changelog?g={1}", val3[closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ModrinthHash][(object)"project_id"], _0024VB_0024Local_VanillaVersion) };
									closure_0024__4_002D2._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile = closure_0024__4_002D2._0024VB_0024Local_UpdateFile;
								}
							}
						}
					}
					Logger.Info("从 Modrinth 获取本地 Mod 信息结束", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "从 Modrinth 获取本地 Mod 信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					_0024VB_0024Local_IsFailed = true;
					ProjectData.ClearProjectError();
				}
				finally
				{
					_0024VB_0024Local_EndedThreadCount++;
				}
			}
		}

		[SpecialName]
		internal void _Lambda_0024__11()
		{
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Expected O, but got Unknown
			//IL_019f: Unknown result type (might be due to invalid IL or missing references)
			//IL_01aa: Expected O, but got Unknown
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f7: Expected O, but got Unknown
			//IL_0349: Unknown result type (might be due to invalid IL or missing references)
			//IL_034f: Invalid comparison between Unknown and I4
			//IL_0523: Unknown result type (might be due to invalid IL or missing references)
			//IL_052e: Expected O, but got Unknown
			//IL_0567: Unknown result type (might be due to invalid IL or missing references)
			//IL_0574: Unknown result type (might be due to invalid IL or missing references)
			//IL_0594: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				_Closure_0024__4_002D4 arg = new _Closure_0024__4_002D4(arg)
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_4 = this
				};
				List<uint> list = new List<uint>();
				foreach (LocalResourceFile _0024VB_0024Local_Mod in _0024VB_0024Local_Mods)
				{
					list.Add(_0024VB_0024Local_Mod.CurseForgeHash);
					if (_0024VB_0024Local_Loader.IsCanceledWithThread(_0024VB_0024Local_CurrentTaskThread))
					{
						return;
					}
				}
				JContainer val = (JContainer)ModDownload.DlModRequest("https://api.curseforge.com/v1/fingerprints/432", HttpMethod.Post, string.Format("{{\"fingerprints\": [{0}]}}", EnumerableExtensions.Join((IEnumerable)list, ",")), "application/json")[(object)"data"][(object)"exactMatches"];
				Logger.Info($"从 CurseForge 获取到 {val.Count} 个本地 Mod 的对应信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
				if (!((IEnumerable<JToken>)val).Any())
				{
					return;
				}
				Dictionary<int, List<LocalResourceFile>> dictionary = new Dictionary<int, List<LocalResourceFile>>();
				arg._0024VB_0024Local_CurseForgeLoaders = new Dictionary<LocalResourceFile, ModLoaders>();
				foreach (JToken item in (IEnumerable<JToken>)val)
				{
					string text = item[(object)"id"].ToString();
					uint num = (uint)item[(object)"file"][(object)"fileFingerprint"];
					foreach (LocalResourceFile _0024VB_0024Local_Mod2 in _0024VB_0024Local_Mods)
					{
						if (_0024VB_0024Local_Mod2.CurseForgeHash == num)
						{
							if (ResourceProject.Cache.ContainsKey(text) && (object)_0024VB_0024Local_Mod2.Project == null)
							{
								_0024VB_0024Local_Mod2.Project = ResourceProject.Cache[text];
							}
							EnumerableExtensions.AddIntoValueCollection<int, LocalResourceFile>((IDictionary<int, List<LocalResourceFile>>)dictionary, Conversions.ToInteger(text), _0024VB_0024Local_Mod2);
							ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)item[(object)"file"], (ResourceTypes)1);
							arg._0024VB_0024Local_CurseForgeLoaders[_0024VB_0024Local_Mod2] = resourceVersion.ModLoaders;
							if (_0024VB_0024Local_Mod2.ProjectVersion == null || DateTime.Compare(_0024VB_0024Local_Mod2.ProjectVersion.ReleaseDate, resourceVersion.ReleaseDate) < 0)
							{
								_0024VB_0024Local_Mod2.ProjectVersion = resourceVersion;
							}
						}
					}
				}
				if (_0024VB_0024Local_Loader.IsCanceledWithThread(_0024VB_0024Local_CurrentTaskThread))
				{
					return;
				}
				Logger.Info($"需要从 CurseForge 获取 {dictionary.Count} 个本地 Mod 的工程信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
				if (!dictionary.Any())
				{
					return;
				}
				JToken val2 = ModDownload.DlModRequest("https://api.curseforge.com/v1/mods", HttpMethod.Post, string.Format("{{\"modIds\": [{0}]}}", EnumerableExtensions.Join((IEnumerable)dictionary.Keys, ",")), "application/json")[(object)"data"];
				Dictionary<int, List<LocalResourceFile>> dictionary2 = new Dictionary<int, List<LocalResourceFile>>();
				Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
				_Closure_0024__4_002D5 closure_0024__4_002D = default(_Closure_0024__4_002D5);
				foreach (JToken item2 in (IEnumerable<JToken>)val2)
				{
					if (item2[(object)"isAvailable"] != null && !item2[(object)"isAvailable"].ToObject<bool>())
					{
						continue;
					}
					ResourceProject resourceProject = new ResourceProject((JObject)item2);
					using List<LocalResourceFile>.Enumerator enumerator5 = dictionary[Conversions.ToInteger(resourceProject.Id)].GetEnumerator();
					while (enumerator5.MoveNext())
					{
						closure_0024__4_002D = new _Closure_0024__4_002D5(closure_0024__4_002D)
						{
							_0024VB_0024NonLocal__0024VB_0024Closure_5 = arg,
							_0024VB_0024Local_LocalFile = enumerator5.Current
						};
						ResourceProject project = closure_0024__4_002D._0024VB_0024Local_LocalFile.Project;
						if ((object)project != null && (int)project.Platform == 2)
						{
							closure_0024__4_002D._0024VB_0024Local_LocalFile.Project = closure_0024__4_002D._0024VB_0024Local_LocalFile.Project;
						}
						else
						{
							closure_0024__4_002D._0024VB_0024Local_LocalFile.Project = resourceProject;
						}
						List<JToken> list2 = EnumerableExtensions.MaxByAll<JToken, string>(((IEnumerable<JToken>)item2[(object)"latestFilesIndexes"]).Where(closure_0024__4_002D._Lambda_0024__12), (Func<JToken, string>)([SpecialName] (JToken Entry) => Entry[(object)"gameVersion"].ToString()), (IComparer<string>)Comparer<string>.Create([SpecialName] (string x, string y) => McUtils.CompareVersion(x, y)));
						foreach (JToken item3 in list2)
						{
							int key = item3[(object)"fileId"].ToObject<int>();
							if (!dictionary2.ContainsKey(key))
							{
								dictionary2[key] = new List<LocalResourceFile>();
							}
							dictionary2[key].Add(closure_0024__4_002D._0024VB_0024Local_LocalFile);
							dictionary3[key] = resourceProject.Slug;
						}
					}
				}
				Logger.Info($"已从 CurseForge 获取本地 Mod 信息，需要获取 {dictionary2.Count} 个用于检查更新的文件信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
				if (!dictionary2.Any())
				{
					return;
				}
				JToken val3 = ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/files", HttpMethod.Post, string.Format("{{\"fileIds\": [{0}]}}", EnumerableExtensions.Join((IEnumerable)dictionary2.Keys, ",")), "application/json")[(object)"data"];
				Dictionary<LocalResourceFile, ResourceVersion> dictionary4 = new Dictionary<LocalResourceFile, ResourceVersion>();
				foreach (JObject item4 in (IEnumerable<JToken>)val3)
				{
					ResourceVersion resourceVersion2 = ResourceVersion.FromPlatformJson(item4, (ResourceTypes)1);
					if (!resourceVersion2.DownloadAvailable)
					{
						continue;
					}
					foreach (LocalResourceFile item5 in dictionary2[Conversions.ToInteger(resourceVersion2.Id)])
					{
						if (resourceVersion2.ModLoaders != arg._0024VB_0024Local_CurseForgeLoaders[item5])
						{
							Logger.Warn($"从 CurseForge 获取到的更新文件 {resourceVersion2.FileName} 加载器为 {resourceVersion2.ModLoaders}，与本地文件 {item5.File.Name} 的加载器 {arg._0024VB_0024Local_CurseForgeLoaders[item5]} 不一致，已忽略", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						}
						else if (!dictionary4.ContainsKey(item5) || DateTime.Compare(dictionary4[item5].ReleaseDate, resourceVersion2.ReleaseDate) < 0)
						{
							dictionary4[item5] = resourceVersion2;
						}
					}
				}
				_Closure_0024__4_002D6 closure_0024__4_002D2 = default(_Closure_0024__4_002D6);
				foreach (KeyValuePair<LocalResourceFile, ResourceVersion> item6 in dictionary4)
				{
					closure_0024__4_002D2 = new _Closure_0024__4_002D6(closure_0024__4_002D2)
					{
						_0024VB_0024Local_Entry = item6.Key,
						_0024VB_0024Local_UpdateFile = item6.Value
					};
					Logger.Trace((Func<string>)closure_0024__4_002D2._Lambda_0024__15, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					if (DateTime.Compare(closure_0024__4_002D2._0024VB_0024Local_Entry.ProjectVersion.ReleaseDate, closure_0024__4_002D2._0024VB_0024Local_UpdateFile.ReleaseDate) < 0 && Operators.CompareString(closure_0024__4_002D2._0024VB_0024Local_Entry.ProjectVersion.Hash, closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Hash, TextCompare: false) != 0)
					{
						if (closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile != null && Operators.CompareString(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Hash, closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile.Hash, TextCompare: false) == 0)
						{
							closure_0024__4_002D2._0024VB_0024Local_Entry.ChangelogUrls.Add($"https://www.curseforge.com/minecraft/mc-mods/{dictionary3[Conversions.ToInteger(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Id)]}/files/{closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Id}");
							closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile.DownloadUrls.AddRange(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.DownloadUrls);
						}
						else if (closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile == null || DateTime.Compare(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.ReleaseDate, closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile.ReleaseDate) > 0)
						{
							closure_0024__4_002D2._0024VB_0024Local_Entry.ChangelogUrls = new List<string> { $"https://www.curseforge.com/minecraft/mc-mods/{dictionary3[Conversions.ToInteger(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Id)]}/files/{closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Id}" };
							closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile = closure_0024__4_002D2._0024VB_0024Local_UpdateFile;
						}
					}
				}
				Logger.Info("从 CurseForge 获取 Mod 更新信息结束", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "从 CurseForge 获取本地 Mod 信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
				_0024VB_0024Local_IsFailed = true;
				ProjectData.ClearProjectError();
			}
			finally
			{
				checked
				{
					_0024VB_0024Local_EndedThreadCount++;
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__4_002D1
	{
		public Dictionary<LocalResourceFile, List<string>> _0024VB_0024Local_ModrinthLoaders;

		public _Closure_0024__4_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__4_002D1(_Closure_0024__4_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_ModrinthLoaders = arg0._0024VB_0024Local_ModrinthLoaders;
			}
		}

		[SpecialName]
		internal string _Lambda_0024__7(LocalResourceFile m)
		{
			return string.Format("{{\"hash\": \"{0}\", \"loaders\": [\"{1}\"], \"game_versions\": [\"{2}\"]}}", m.ModrinthHash, EnumerableExtensions.Join((IEnumerable)_0024VB_0024Local_ModrinthLoaders[m], "\",\""), _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_VanillaVersion);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__4_002D2
	{
		public LocalResourceFile _0024VB_0024Local_Entry;

		public _Closure_0024__4_002D2(_Closure_0024__4_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Entry = arg0._0024VB_0024Local_Entry;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__4_002D3
	{
		public ResourceVersion _0024VB_0024Local_UpdateFile;

		public _Closure_0024__4_002D2 _0024VB_0024NonLocal__0024VB_0024Closure_3;

		public _Closure_0024__4_002D3(_Closure_0024__4_002D3 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_UpdateFile = arg0._0024VB_0024Local_UpdateFile;
			}
		}

		[SpecialName]
		internal string _Lambda_0024__10()
		{
			return $"本地文件 {_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ProjectVersion.FileName} 在 Modrinth 上的最新版为 {_0024VB_0024Local_UpdateFile.FileName}";
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__4_002D4
	{
		public Dictionary<LocalResourceFile, ModLoaders> _0024VB_0024Local_CurseForgeLoaders;

		public _Closure_0024__4_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_4;

		public _Closure_0024__4_002D4(_Closure_0024__4_002D4 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_CurseForgeLoaders = arg0._0024VB_0024Local_CurseForgeLoaders;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__4_002D5
	{
		public LocalResourceFile _0024VB_0024Local_LocalFile;

		public _Closure_0024__4_002D4 _0024VB_0024NonLocal__0024VB_0024Closure_5;

		public _Closure_0024__4_002D5(_Closure_0024__4_002D5 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_LocalFile = arg0._0024VB_0024Local_LocalFile;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__12(JToken Entry)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			ModLoaders val = (ModLoaders)((Entry[(object)"modLoader"] != null) ? ((int)Resource.FromCurseForgeModLoaderType(Entry[(object)"modLoader"].ToObject<int>())) : 0);
			if ((int)_0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024Local_CurseForgeLoaders[_0024VB_0024Local_LocalFile] == 0)
			{
				if ((int)val != 0)
				{
					return false;
				}
			}
			else if (!EnumUtils.HasFlagF<ModLoaders>(_0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024Local_CurseForgeLoaders[_0024VB_0024Local_LocalFile], val))
			{
				return false;
			}
			string text = (string)Entry[(object)"gameVersion"];
			return text != null && Operators.CompareString(text, _0024VB_0024NonLocal__0024VB_0024Closure_5._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024Local_VanillaVersion, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__4_002D6
	{
		public LocalResourceFile _0024VB_0024Local_Entry;

		public ResourceVersion _0024VB_0024Local_UpdateFile;

		public _Closure_0024__4_002D6(_Closure_0024__4_002D6 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Entry = arg0._0024VB_0024Local_Entry;
				_0024VB_0024Local_UpdateFile = arg0._0024VB_0024Local_UpdateFile;
			}
		}

		[SpecialName]
		internal string _Lambda_0024__15()
		{
			return $"本地文件 {_0024VB_0024Local_Entry.ProjectVersion.FileName} 在 CurseForge 上的最新版为 {_0024VB_0024Local_UpdateFile.FileName}";
		}
	}

	public static ModLoader.LoaderTask<string, List<LocalResourceFile>> LocalResourceLoader = new ModLoader.LoaderTask<string, List<LocalResourceFile>>("LocalResourceLoader", LocalResourceFileLoad);

	public static ModLoader.LoaderTask<(List<LocalResourceFile> ModList, JObject Cache, McVersion Target), int> LocalResourceOnlineLoader = new ModLoader.LoaderTask<(List<LocalResourceFile>, JObject, McVersion), int>("LocalResourceOnlineLoader", LocalResourceOnlineLoad);

	[SpecialName]
	private static Func<string, bool> _0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile_0024Init;

	private static void LocalResourceFileLoad(ModLoader.LoaderTask<string, List<LocalResourceFile>> Loader)
	{
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected O, but got Unknown
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Expected O, but got Unknown
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			ModBase.RunInUiWait([SpecialName] () =>
			{
				if (ModMain.FrmInstanceMod != null)
				{
					ModMain.FrmInstanceMod.Load.ShowProgress = false;
				}
			});
			if (PageInstanceMod.UpdatingInstanceModFolders.Contains(Loader.Input))
			{
				Logger.Info($"等待 Mod 更新完成后才能继续加载 Mod 列表：{Loader.Input}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
				try
				{
					ModBase.RunInUiWait([SpecialName] () =>
					{
						if (ModMain.FrmInstanceMod != null)
						{
							ModMain.FrmInstanceMod.Load.Text = "正在更新 Mod";
						}
					});
					while (PageInstanceMod.UpdatingInstanceModFolders.Contains(Loader.Input))
					{
						if (Loader.IsCanceled)
						{
							return;
						}
						Thread.Sleep(100);
					}
				}
				finally
				{
					ModBase.RunInUiWait([SpecialName] () =>
					{
						if (ModMain.FrmInstanceMod != null)
						{
							ModMain.FrmInstanceMod.Load.Text = "正在加载 Mod 列表";
						}
					});
				}
				ModMain.FrmInstanceMod.LoaderRun(ModLoader.LoaderFolderRunType.UpdateOnly);
			}
			List<string> list = new List<string>();
			if (DirectoryUtils.Exists(Loader.Input))
			{
				foreach (string item in DirectoryUtils.EnumerateFiles(Loader.Input, true, "*"))
				{
					if (!PathUtils.IsSame(PathUtils.RemoveLastPart(item), Loader.Input) && ((object)PageInstanceLeft.Instance == null || !EnumUtils.HasFlagF<ModLoaders>(PageInstanceLeft.Instance.Version.ModLoaders, (ModLoaders)1) || PageInstanceLeft.Instance.Version.Vanilla.Major >= 13 || Operators.CompareString(PathUtils.GetLastPart(PathUtils.RemoveLastPart(item)), $"1.{PageInstanceLeft.Instance.Version.Vanilla.Major}.{PageInstanceLeft.Instance.Version.Vanilla.Build}", TextCompare: false) != 0))
					{
						continue;
					}
					if (_0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile_0024Init == null)
					{
						Interlocked.CompareExchange(ref _0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile_0024Init, new StaticLocalInitFlag(), null);
					}
					bool lockTaken = false;
					try
					{
						Monitor.Enter(_0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile_0024Init, ref lockTaken);
						if (_0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile_0024Init.State == 0)
						{
							_0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile_0024Init.State = 2;
							_0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile = [SpecialName] (string FullName) =>
							{
								if (FullName == null)
								{
									return false;
								}
								if (new string[2] { "disabled", "old" }.Contains(PathUtils.GetExtension(FullName)))
								{
									FullName = PathUtils.GetFileNameWithoutExtension(FullName);
								}
								return new string[3] { "jar", "zip", "litemod" }.Contains(PathUtils.GetExtension(FullName));
							};
						}
						else if (_0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile_0024Init.State == 2)
						{
							throw new IncompleteInitialization();
						}
					}
					finally
					{
						_0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile_0024Init.State = 1;
						if (lockTaken)
						{
							Monitor.Exit(_0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile_0024Init);
						}
					}
					if (_0024STATIC_0024LocalResourceFileLoad_0024011151286FC2E1512816911280C8_0024IsModFile(item))
					{
						list.Add(item);
					}
				}
			}
			string text = ModBase.PathTemp + "Cache\\LocalMod.json";
			JObject val = new JObject();
			try
			{
				string text2 = FileUtils.TryReadAsString(text, (Encoding)null, (Type)null);
				if (!string.IsNullOrWhiteSpace(text2))
				{
					val = (JObject)StringExtensions.DeserializeJson(text2);
					if (!val.ContainsKey("version") || val["version"].ToObject<int>() != 21)
					{
						Logger.Warn("本地 Mod 信息缓存版本已过期，将弃用这些缓存信息", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
						val = new JObject();
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "读取本地 Mod 信息缓存失败，已重置", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
				val = new JObject();
				ProjectData.ClearProjectError();
			}
			val["version"] = JToken.op_Implicit(21);
			List<LocalResourceFile> list2 = new List<LocalResourceFile>();
			_Closure_0024__2_002D0 closure_0024__2_002D = default(_Closure_0024__2_002D0);
			foreach (string item2 in list)
			{
				closure_0024__2_002D = new _Closure_0024__2_002D0(closure_0024__2_002D);
				if (Loader.IsCanceled)
				{
					return;
				}
				closure_0024__2_002D._0024VB_0024Local_ModEntry = new LocalResourceFile(item2);
				LocalResourceFile localResourceFile = list2.FirstOrDefault(closure_0024__2_002D._Lambda_0024__4);
				if (localResourceFile != null)
				{
					LocalResourceFile localResourceFile2 = (localResourceFile.IsEnabled ? closure_0024__2_002D._0024VB_0024Local_ModEntry : localResourceFile);
					Logger.Warn($"重复的 Mod 文件：{localResourceFile.File.Name} 与 {closure_0024__2_002D._0024VB_0024Local_ModEntry.File.Name}，已忽略 {localResourceFile2.File.Name}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					if (localResourceFile2 == closure_0024__2_002D._0024VB_0024Local_ModEntry)
					{
						continue;
					}
					list2.Remove(localResourceFile2);
				}
				list2.Add(closure_0024__2_002D._0024VB_0024Local_ModEntry);
			}
			Logger.Info($"共发现 {list2.Count} 个 Mod", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
			list2 = list2.OrderBy([SpecialName] (LocalResourceFile m) => m.File.Name).ToList();
			if (!Loader.IsCanceled)
			{
				Loader.Output = list2;
				LocalResourceOnlineLoader.Start((list2, val, PageInstanceLeft.Instance.Version), IsForceRestart: true);
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Warn(ex4, "Mod 列表加载失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
			throw;
		}
	}

	private static void LocalResourceOnlineLoad(ModLoader.LoaderTask<(List<LocalResourceFile> ModList, JObject Cache, McVersion Target), int> Loader)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected I4, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Expected I4, but got Unknown
		_Closure_0024__4_002D0 arg = default(_Closure_0024__4_002D0);
		_Closure_0024__4_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__4_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader = Loader;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods = new List<LocalResourceFile>();
		JObject item = CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.Input.Cache;
		ModLoaders modLoaders = CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.Input.Target.ModLoaders;
		foreach (LocalResourceFile item3 in CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.Input.ModList)
		{
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.IsCanceled)
			{
				return;
			}
			string text = item3.ModrinthHash + CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.Input.Target.VanillaName + Conversions.ToString((int)modLoaders);
			if (item.ContainsKey(text))
			{
				item3.FromJson((JObject)item[text]);
				if (item3.OnlineDataLoaded && DateTime.Now - item[text][(object)"Project"][(object)"CacheTime"].ToObject<DateTime>() < new TimeSpan(6, 0, 0))
				{
					continue;
				}
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods.Add(item3);
		}
		Logger.Info($"有 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods.Where([SpecialName] (LocalResourceFile m) => (object)m.Project == null).Count()} 个 Mod 需要联网获取信息，{CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods.Where([SpecialName] (LocalResourceFile m) => (object)m.Project != null).Count()} 个 Mod 需要更新信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
		if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods.Any())
		{
			return;
		}
		McVersion item2 = CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.Input.Target;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_VanillaVersion = item2.VanillaName;
		Logger.Info($"正在检查 Mod 信息的版本：{CS_0024_003C_003E8__locals0._0024VB_0024Local_VanillaVersion}，加载器 {modLoaders}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
		CS_0024_003C_003E8__locals0._0024VB_0024Local_EndedThreadCount = 0;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_IsFailed = false;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_CurrentTaskThread = Thread.CurrentThread;
		checked
		{
			ModBase.RunInNewThread([SpecialName] () =>
			{
				//IL_006c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0072: Expected O, but got Unknown
				//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
				//IL_02fd: Expected O, but got Unknown
				//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
				//IL_01af: Expected O, but got Unknown
				//IL_0313: Unknown result type (might be due to invalid IL or missing references)
				//IL_031d: Expected O, but got Unknown
				//IL_0401: Unknown result type (might be due to invalid IL or missing references)
				//IL_0408: Expected O, but got Unknown
				//IL_0490: Unknown result type (might be due to invalid IL or missing references)
				//IL_049b: Expected O, but got Unknown
				try
				{
					_Closure_0024__4_002D1 arg3 = default(_Closure_0024__4_002D1);
					_Closure_0024__4_002D1 CS_0024_003C_003E8__locals1 = new _Closure_0024__4_002D1(arg3);
					CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2 = CS_0024_003C_003E8__locals0;
					List<string> list3 = CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods.Select([SpecialName] (LocalResourceFile m) => m.ModrinthHash).ToList();
					JObject val4 = (JObject)ModDownload.DlModRequest("https://api.modrinth.com/v2/version_files", HttpMethod.Post, string.Format("{{\"hashes\": [\"{0}\"], \"algorithm\": \"sha1\"}}", EnumerableExtensions.Join((IEnumerable)list3, "\",\"")), "application/json");
					Logger.Info($"从 Modrinth 获取到 {((JContainer)val4).Count} 个本地 Mod 的对应信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					if (((JContainer)val4).Count != 0)
					{
						Dictionary<string, List<LocalResourceFile>> dictionary5 = new Dictionary<string, List<LocalResourceFile>>();
						CS_0024_003C_003E8__locals1._0024VB_0024Local_ModrinthLoaders = new Dictionary<LocalResourceFile, List<string>>();
						foreach (LocalResourceFile _0024VB_0024Local_Mod in CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods)
						{
							if (val4.ContainsKey(_0024VB_0024Local_Mod.ModrinthHash) && !((string)val4[_0024VB_0024Local_Mod.ModrinthHash][(object)"files"][(object)0][(object)"hashes"][(object)"sha1"] != _0024VB_0024Local_Mod.ModrinthHash))
							{
								string key2 = val4[_0024VB_0024Local_Mod.ModrinthHash][(object)"project_id"].ToString();
								if (ResourceProject.Cache.ContainsKey(key2) && (object)_0024VB_0024Local_Mod.Project == null)
								{
									_0024VB_0024Local_Mod.Project = ResourceProject.Cache[key2];
								}
								if (!dictionary5.ContainsKey(key2))
								{
									dictionary5[key2] = new List<LocalResourceFile>();
								}
								dictionary5[key2].Add(_0024VB_0024Local_Mod);
								ResourceVersion resourceVersion3 = ResourceVersion.FromPlatformJson((JObject)val4[_0024VB_0024Local_Mod.ModrinthHash], (ResourceTypes)1);
								CS_0024_003C_003E8__locals1._0024VB_0024Local_ModrinthLoaders[_0024VB_0024Local_Mod] = (from l in ((IEnumerable<JToken>)val4[_0024VB_0024Local_Mod.ModrinthHash][(object)"loaders"]).Select([SpecialName] (JToken l) => StringExtensions.Lower(l.ToString())).Distinct()
									orderby l
									select l).ToList();
								if (_0024VB_0024Local_Mod.ProjectVersion == null || DateTime.Compare(_0024VB_0024Local_Mod.ProjectVersion.ReleaseDate, resourceVersion3.ReleaseDate) < 0)
								{
									_0024VB_0024Local_Mod.ProjectVersion = resourceVersion3;
								}
								else
								{
									_0024VB_0024Local_Mod.ProjectVersion.Version = resourceVersion3.Version;
								}
							}
						}
						if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.IsCanceledWithThread(CS_0024_003C_003E8__locals0._0024VB_0024Local_CurrentTaskThread))
						{
							Logger.Info($"需要从 Modrinth 获取 {dictionary5.Count} 个本地 Mod 的工程信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
							if (dictionary5.Any())
							{
								JArray val5 = (JArray)ModDownload.DlModRequest(string.Format("https://api.modrinth.com/v2/projects?ids=[\"{0}\"]", EnumerableExtensions.Join((IEnumerable)dictionary5.Keys, "\",\"")));
								foreach (JToken item4 in val5)
								{
									ResourceProject resourceProject2 = new ResourceProject((JObject)item4);
									foreach (LocalResourceFile item5 in dictionary5[resourceProject2.Id])
									{
										item5.Project = resourceProject2;
									}
								}
								Logger.Info("已从 Modrinth 获取本地 Mod 信息，继续获取更新信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
								List<LocalResourceFile> source = dictionary5.SelectMany([SpecialName] (KeyValuePair<string, List<LocalResourceFile>> l) => l.Value).ToList();
								if (source.Any())
								{
									string arg4 = EnumerableExtensions.Join((IEnumerable)source.Select([SpecialName] (LocalResourceFile m) => string.Format("{{\"hash\": \"{0}\", \"loaders\": [\"{1}\"], \"game_versions\": [\"{2}\"]}}", m.ModrinthHash, EnumerableExtensions.Join((IEnumerable)CS_0024_003C_003E8__locals1._0024VB_0024Local_ModrinthLoaders[m], "\",\""), CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_VanillaVersion)), ",");
									JObject val6 = (JObject)ModDownload.DlModRequest("https://api.modrinth.com/v2/version_files/update_individual", HttpMethod.Post, $"{{\"algorithm\": \"sha1\", \"hashes\": [{arg4}]}}", "application/json");
									using (List<LocalResourceFile>.Enumerator enumerator15 = CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods.GetEnumerator())
									{
										_Closure_0024__4_002D2 closure_0024__4_002D3 = default(_Closure_0024__4_002D2);
										_Closure_0024__4_002D3 closure_0024__4_002D4 = default(_Closure_0024__4_002D3);
										while (enumerator15.MoveNext())
										{
											closure_0024__4_002D3 = new _Closure_0024__4_002D2(closure_0024__4_002D3);
											closure_0024__4_002D3._0024VB_0024Local_Entry = enumerator15.Current;
											closure_0024__4_002D4 = new _Closure_0024__4_002D3(closure_0024__4_002D4);
											closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3 = closure_0024__4_002D3;
											if (val6.ContainsKey(closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ModrinthHash) && closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ProjectVersion != null)
											{
												closure_0024__4_002D4._0024VB_0024Local_UpdateFile = ResourceVersion.FromPlatformJson((JObject)val6[closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ModrinthHash], (ResourceTypes)1);
												if (closure_0024__4_002D4._0024VB_0024Local_UpdateFile.DownloadAvailable && (from l in ((IEnumerable<JToken>)val6[closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ModrinthHash][(object)"loaders"]).Select([SpecialName] (JToken l) => StringExtensions.Lower(l.ToString())).Distinct()
													orderby l
													select l).ToList().SequenceEqual(CS_0024_003C_003E8__locals1._0024VB_0024Local_ModrinthLoaders[closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry]))
												{
													Logger.Trace((Func<string>)closure_0024__4_002D4._Lambda_0024__10, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
													if (DateTime.Compare(closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ProjectVersion.ReleaseDate, closure_0024__4_002D4._0024VB_0024Local_UpdateFile.ReleaseDate) < 0 && Operators.CompareString(closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ProjectVersion.Hash, closure_0024__4_002D4._0024VB_0024Local_UpdateFile.Hash, TextCompare: false) != 0)
													{
														if (closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile != null && Operators.CompareString(closure_0024__4_002D4._0024VB_0024Local_UpdateFile.Hash, closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile.Hash, TextCompare: false) == 0)
														{
															closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ChangelogUrls.Add(string.Format("https://modrinth.com/mod/{0}/changelog?g={1}", val6[closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ModrinthHash][(object)"project_id"], CS_0024_003C_003E8__locals0._0024VB_0024Local_VanillaVersion));
															closure_0024__4_002D4._0024VB_0024Local_UpdateFile.DownloadUrls.AddRange(closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile.DownloadUrls);
															closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile = closure_0024__4_002D4._0024VB_0024Local_UpdateFile;
														}
														else if (closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile == null || DateTime.Compare(closure_0024__4_002D4._0024VB_0024Local_UpdateFile.ReleaseDate, closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile.ReleaseDate) >= 0)
														{
															closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ChangelogUrls = new List<string> { string.Format("https://modrinth.com/mod/{0}/changelog?g={1}", val6[closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.ModrinthHash][(object)"project_id"], CS_0024_003C_003E8__locals0._0024VB_0024Local_VanillaVersion) };
															closure_0024__4_002D4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_Entry.UpdateFile = closure_0024__4_002D4._0024VB_0024Local_UpdateFile;
														}
													}
												}
											}
										}
									}
									Logger.Info("从 Modrinth 获取本地 Mod 信息结束", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
								}
							}
						}
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, "从 Modrinth 获取本地 Mod 信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					CS_0024_003C_003E8__locals0._0024VB_0024Local_IsFailed = true;
					ProjectData.ClearProjectError();
				}
				finally
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_EndedThreadCount++;
				}
			}, "Mod List Detail Loader Modrinth");
			ModBase.RunInNewThread([SpecialName] () =>
			{
				//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
				//IL_00ad: Expected O, but got Unknown
				//IL_019f: Unknown result type (might be due to invalid IL or missing references)
				//IL_01aa: Expected O, but got Unknown
				//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
				//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
				//IL_02f7: Expected O, but got Unknown
				//IL_0349: Unknown result type (might be due to invalid IL or missing references)
				//IL_034f: Invalid comparison between Unknown and I4
				//IL_0523: Unknown result type (might be due to invalid IL or missing references)
				//IL_052e: Expected O, but got Unknown
				//IL_0567: Unknown result type (might be due to invalid IL or missing references)
				//IL_0574: Unknown result type (might be due to invalid IL or missing references)
				//IL_0594: Unknown result type (might be due to invalid IL or missing references)
				//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
				try
				{
					_Closure_0024__4_002D4 arg2 = new _Closure_0024__4_002D4(arg2);
					arg2._0024VB_0024NonLocal__0024VB_0024Closure_4 = CS_0024_003C_003E8__locals0;
					List<uint> list = new List<uint>();
					foreach (LocalResourceFile _0024VB_0024Local_Mod2 in CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods)
					{
						list.Add(_0024VB_0024Local_Mod2.CurseForgeHash);
						if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.IsCanceledWithThread(CS_0024_003C_003E8__locals0._0024VB_0024Local_CurrentTaskThread))
						{
							return;
						}
					}
					JContainer val = (JContainer)ModDownload.DlModRequest("https://api.curseforge.com/v1/fingerprints/432", HttpMethod.Post, string.Format("{{\"fingerprints\": [{0}]}}", EnumerableExtensions.Join((IEnumerable)list, ",")), "application/json")[(object)"data"][(object)"exactMatches"];
					Logger.Info($"从 CurseForge 获取到 {val.Count} 个本地 Mod 的对应信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					if (((IEnumerable<JToken>)val).Any())
					{
						Dictionary<int, List<LocalResourceFile>> dictionary = new Dictionary<int, List<LocalResourceFile>>();
						arg2._0024VB_0024Local_CurseForgeLoaders = new Dictionary<LocalResourceFile, ModLoaders>();
						foreach (JToken item6 in (IEnumerable<JToken>)val)
						{
							string text2 = item6[(object)"id"].ToString();
							uint num = (uint)item6[(object)"file"][(object)"fileFingerprint"];
							foreach (LocalResourceFile _0024VB_0024Local_Mod3 in CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods)
							{
								if (_0024VB_0024Local_Mod3.CurseForgeHash == num)
								{
									if (ResourceProject.Cache.ContainsKey(text2) && (object)_0024VB_0024Local_Mod3.Project == null)
									{
										_0024VB_0024Local_Mod3.Project = ResourceProject.Cache[text2];
									}
									EnumerableExtensions.AddIntoValueCollection<int, LocalResourceFile>((IDictionary<int, List<LocalResourceFile>>)dictionary, Conversions.ToInteger(text2), _0024VB_0024Local_Mod3);
									ResourceVersion resourceVersion = ResourceVersion.FromPlatformJson((JObject)item6[(object)"file"], (ResourceTypes)1);
									arg2._0024VB_0024Local_CurseForgeLoaders[_0024VB_0024Local_Mod3] = resourceVersion.ModLoaders;
									if (_0024VB_0024Local_Mod3.ProjectVersion == null || DateTime.Compare(_0024VB_0024Local_Mod3.ProjectVersion.ReleaseDate, resourceVersion.ReleaseDate) < 0)
									{
										_0024VB_0024Local_Mod3.ProjectVersion = resourceVersion;
									}
								}
							}
						}
						if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.IsCanceledWithThread(CS_0024_003C_003E8__locals0._0024VB_0024Local_CurrentTaskThread))
						{
							Logger.Info($"需要从 CurseForge 获取 {dictionary.Count} 个本地 Mod 的工程信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
							if (dictionary.Any())
							{
								JToken val2 = ModDownload.DlModRequest("https://api.curseforge.com/v1/mods", HttpMethod.Post, string.Format("{{\"modIds\": [{0}]}}", EnumerableExtensions.Join((IEnumerable)dictionary.Keys, ",")), "application/json")[(object)"data"];
								Dictionary<int, List<LocalResourceFile>> dictionary2 = new Dictionary<int, List<LocalResourceFile>>();
								Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
								_Closure_0024__4_002D5 closure_0024__4_002D = default(_Closure_0024__4_002D5);
								foreach (JToken item7 in (IEnumerable<JToken>)val2)
								{
									if (item7[(object)"isAvailable"] == null || item7[(object)"isAvailable"].ToObject<bool>())
									{
										ResourceProject resourceProject = new ResourceProject((JObject)item7);
										using List<LocalResourceFile>.Enumerator enumerator7 = dictionary[Conversions.ToInteger(resourceProject.Id)].GetEnumerator();
										while (enumerator7.MoveNext())
										{
											closure_0024__4_002D = new _Closure_0024__4_002D5(closure_0024__4_002D);
											closure_0024__4_002D._0024VB_0024NonLocal__0024VB_0024Closure_5 = arg2;
											closure_0024__4_002D._0024VB_0024Local_LocalFile = enumerator7.Current;
											ResourceProject project = closure_0024__4_002D._0024VB_0024Local_LocalFile.Project;
											if ((object)project != null && unchecked((int)project.Platform) == 2)
											{
												closure_0024__4_002D._0024VB_0024Local_LocalFile.Project = closure_0024__4_002D._0024VB_0024Local_LocalFile.Project;
											}
											else
											{
												closure_0024__4_002D._0024VB_0024Local_LocalFile.Project = resourceProject;
											}
											List<JToken> list2 = EnumerableExtensions.MaxByAll<JToken, string>(((IEnumerable<JToken>)item7[(object)"latestFilesIndexes"]).Where(closure_0024__4_002D._Lambda_0024__12), (Func<JToken, string>)([SpecialName] (JToken Entry) => Entry[(object)"gameVersion"].ToString()), (IComparer<string>)Comparer<string>.Create([SpecialName] (string x, string y) => McUtils.CompareVersion(x, y)));
											foreach (JToken item8 in list2)
											{
												int key = item8[(object)"fileId"].ToObject<int>();
												if (!dictionary2.ContainsKey(key))
												{
													dictionary2[key] = new List<LocalResourceFile>();
												}
												dictionary2[key].Add(closure_0024__4_002D._0024VB_0024Local_LocalFile);
												dictionary3[key] = resourceProject.Slug;
											}
										}
									}
								}
								Logger.Info($"已从 CurseForge 获取本地 Mod 信息，需要获取 {dictionary2.Count} 个用于检查更新的文件信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
								if (dictionary2.Any())
								{
									JToken val3 = ModDownload.DlModRequest("https://api.curseforge.com/v1/mods/files", HttpMethod.Post, string.Format("{{\"fileIds\": [{0}]}}", EnumerableExtensions.Join((IEnumerable)dictionary2.Keys, ",")), "application/json")[(object)"data"];
									Dictionary<LocalResourceFile, ResourceVersion> dictionary4 = new Dictionary<LocalResourceFile, ResourceVersion>();
									foreach (JObject item9 in (IEnumerable<JToken>)val3)
									{
										ResourceVersion resourceVersion2 = ResourceVersion.FromPlatformJson(item9, (ResourceTypes)1);
										if (resourceVersion2.DownloadAvailable)
										{
											foreach (LocalResourceFile item10 in dictionary2[Conversions.ToInteger(resourceVersion2.Id)])
											{
												if (resourceVersion2.ModLoaders != arg2._0024VB_0024Local_CurseForgeLoaders[item10])
												{
													Logger.Warn($"从 CurseForge 获取到的更新文件 {resourceVersion2.FileName} 加载器为 {resourceVersion2.ModLoaders}，与本地文件 {item10.File.Name} 的加载器 {arg2._0024VB_0024Local_CurseForgeLoaders[item10]} 不一致，已忽略", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
												}
												else if (!dictionary4.ContainsKey(item10) || DateTime.Compare(dictionary4[item10].ReleaseDate, resourceVersion2.ReleaseDate) < 0)
												{
													dictionary4[item10] = resourceVersion2;
												}
											}
										}
									}
									_Closure_0024__4_002D6 closure_0024__4_002D2 = default(_Closure_0024__4_002D6);
									foreach (KeyValuePair<LocalResourceFile, ResourceVersion> item11 in dictionary4)
									{
										closure_0024__4_002D2 = new _Closure_0024__4_002D6(closure_0024__4_002D2);
										closure_0024__4_002D2._0024VB_0024Local_Entry = item11.Key;
										closure_0024__4_002D2._0024VB_0024Local_UpdateFile = item11.Value;
										Logger.Trace((Func<string>)closure_0024__4_002D2._Lambda_0024__15, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
										if (DateTime.Compare(closure_0024__4_002D2._0024VB_0024Local_Entry.ProjectVersion.ReleaseDate, closure_0024__4_002D2._0024VB_0024Local_UpdateFile.ReleaseDate) < 0 && Operators.CompareString(closure_0024__4_002D2._0024VB_0024Local_Entry.ProjectVersion.Hash, closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Hash, TextCompare: false) != 0)
										{
											if (closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile != null && Operators.CompareString(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Hash, closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile.Hash, TextCompare: false) == 0)
											{
												closure_0024__4_002D2._0024VB_0024Local_Entry.ChangelogUrls.Add($"https://www.curseforge.com/minecraft/mc-mods/{dictionary3[Conversions.ToInteger(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Id)]}/files/{closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Id}");
												closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile.DownloadUrls.AddRange(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.DownloadUrls);
											}
											else if (closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile == null || DateTime.Compare(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.ReleaseDate, closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile.ReleaseDate) > 0)
											{
												closure_0024__4_002D2._0024VB_0024Local_Entry.ChangelogUrls = new List<string> { $"https://www.curseforge.com/minecraft/mc-mods/{dictionary3[Conversions.ToInteger(closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Id)]}/files/{closure_0024__4_002D2._0024VB_0024Local_UpdateFile.Id}" };
												closure_0024__4_002D2._0024VB_0024Local_Entry.UpdateFile = closure_0024__4_002D2._0024VB_0024Local_UpdateFile;
											}
										}
									}
									Logger.Info("从 CurseForge 获取 Mod 更新信息结束", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "从 CurseForge 获取本地 Mod 信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
					CS_0024_003C_003E8__locals0._0024VB_0024Local_IsFailed = true;
					ProjectData.ClearProjectError();
				}
				finally
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_EndedThreadCount++;
				}
			}, "Mod List Detail Loader CurseForge");
		}
		do
		{
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_EndedThreadCount != 2)
			{
				Thread.Sleep(10);
				continue;
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods = CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods.Where([SpecialName] (LocalResourceFile m) => (object)m.Project != null).ToList();
			Logger.Info($"联网获取本地 Mod 信息完成，为 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods.Count} 个 Mod 更新缓存", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Resource\\LocalResourceLoaders.vb");
			if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods.Any() || CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.IsCanceled)
			{
				break;
			}
			foreach (LocalResourceFile _0024VB_0024Local_Mod4 in CS_0024_003C_003E8__locals0._0024VB_0024Local_Mods)
			{
				_0024VB_0024Local_Mod4.OnlineDataLoaded = !CS_0024_003C_003E8__locals0._0024VB_0024Local_IsFailed;
				item[_0024VB_0024Local_Mod4.ModrinthHash + CS_0024_003C_003E8__locals0._0024VB_0024Local_VanillaVersion + Conversions.ToString((int)modLoaders)] = (JToken)(object)_0024VB_0024Local_Mod4.ToJson();
			}
			FileUtils.Write(ModBase.PathTemp + "Cache\\LocalMod.json", ((JToken)item).ToString((Formatting)(ModBase.ModeDebug ? 1 : 0), (JsonConverter[])(object)new JsonConverter[0]), (Encoding)null);
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.IsCanceled)
			{
				break;
			}
			PageInstanceMod frmInstanceMod = ModMain.FrmInstanceMod;
			if (frmInstanceMod != null && frmInstanceMod.Filter == PageInstanceMod.FilterType.CanUpdate)
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMain.FrmInstanceMod?.RefreshUI();
				});
			}
			else
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMain.FrmInstanceMod?.RefreshBars();
				});
			}
			break;
		}
		while (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.IsCanceled);
	}
}
