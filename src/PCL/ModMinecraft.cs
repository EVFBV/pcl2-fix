using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Documents;
using MeloongCore;
using MeloongCore.Extensions;
using MeloongCore.Wpf;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCL.My;
using PCLCS;

namespace PCL;

[StandardModule]
public sealed class ModMinecraft
{
	public class McFolder
	{
		public enum Types
		{
			Vanilla,
			RenamedVanilla,
			Custom
		}

		public string Name;

		public string Location;

		public Types Type;

		public override bool Equals(object obj)
		{
			if (!(obj is McFolder))
			{
				return false;
			}
			McFolder mcFolder = (McFolder)obj;
			return Operators.CompareString(Name, mcFolder.Name, TextCompare: false) == 0 && PathUtils.IsSame(Location, mcFolder.Location) && Type == mcFolder.Type;
		}

		public override string ToString()
		{
			return Location;
		}
	}

	public class McInstance
	{
		public enum McInstanceState
		{
			Error,
			Original,
			Snapshot,
			Fool,
			OptiFine,
			Old,
			Forge,
			NeoForge,
			LiteLoader,
			Fabric
		}

		private string _Name;

		public string Info;

		public McInstanceState State;

		public string Logo;

		public bool IsStar;

		public McInstanceCardType DisplayType;

		private McVersion _Version;

		private string _JsonText;

		private JObject _JsonObject;

		private JObject _JsonVersion;

		private string _InheritName;

		public JsonConfigProvider Config;

		public bool IsLoaded;

		[SpecialName]
		private bool _0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited;

		[SpecialName]
		private StaticLocalInitFlag _0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited_0024Init;

		public string PathVersion { get; }

		public string PathIndie
		{
			get
			{
				if (!Settings.HasSaved("VersionArgumentIndieV2", this))
				{
					if (!IsLoaded)
					{
						Load();
					}
					VB_0024AnonymousDelegate_0<bool> vB_0024AnonymousDelegate_ = [SpecialName] () =>
					{
						if (Settings.HasSaved("VersionArgumentIndie", this) && Settings.Get<int>("VersionArgumentIndie", this) > 0)
						{
							Logger.Info($"版本隔离初始化（{Name}）：从老的版本独立设置中迁移", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							return Settings.Get<int>("VersionArgumentIndie", this) == 1;
						}
						DirectoryInfo info = DirectoryUtils.GetInfo(PathVersion + "mods\\");
						DirectoryInfo info2 = DirectoryUtils.GetInfo(PathVersion + "saves\\");
						if ((info.Exists && info.EnumerateFiles().Any()) || (info2.Exists && info2.EnumerateDirectories().Any()))
						{
							Logger.Info($"版本隔离初始化（{Name}）：版本文件夹下存在 mods 或 saves 文件夹，自动开启", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							return true;
						}
						bool flag = State != McInstanceState.Fool && State != McInstanceState.Old && State != McInstanceState.Snapshot;
						Logger.Info(string.Format("版本隔离初始化（{0}）：从全局默认设置中（{1}）判断，State {2}，IsRelease {3}，Modable {4}", Name, Settings.Get<int>("LaunchArgumentIndieV2"), State, flag, Modable), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						return Settings.Get<int>("LaunchArgumentIndieV2") switch
						{
							0 => false, 
							1 => Modable, 
							2 => !flag, 
							3 => !flag || Modable, 
							_ => true, 
						};
					};
					Settings.Set("VersionArgumentIndieV2", vB_0024AnonymousDelegate_(), this);
				}
				if (!Settings.Get<bool>("VersionArgumentIndieV2", this))
				{
					return McFolderSelected;
				}
				return PathVersion;
			}
		}

		public string Name
		{
			get
			{
				if (_Name == null && Operators.CompareString(PathVersion, "", TextCompare: false) != 0)
				{
					_Name = PathUtils.GetLastPart(PathVersion);
				}
				return _Name;
			}
		}

		public bool Modable
		{
			get
			{
				//IL_0015: Unknown result type (might be due to invalid IL or missing references)
				if (!IsLoaded)
				{
					Load();
				}
				if (!EnumerableExtensions.Flags<ModLoaders>(Version.ModLoaders).Any())
				{
					return DisplayType == McInstanceCardType.API;
				}
				return true;
			}
		}

		public McVersion Version
		{
			get
			{
				//IL_0073: Unknown result type (might be due to invalid IL or missing references)
				//IL_007d: Expected O, but got Unknown
				//IL_0027: Unknown result type (might be due to invalid IL or missing references)
				//IL_002d: Expected O, but got Unknown
				if (_Version != null)
				{
					return _Version;
				}
				try
				{
					if (Operators.CompareString((_Version = new McVersion(Name, JsonObject, JsonVersion)).VanillaName, "Unknown", TextCompare: false) == 0)
					{
						Info = "PCL 无法识别该版本的 MC 版本号";
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "识别 Minecraft 版本时出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					_Version = new McVersion("Unknown", true);
					Info = "无法识别：" + ex2.Message;
					ProjectData.ClearProjectError();
				}
				return _Version;
			}
			set
			{
				_Version = value;
			}
		}

		public DateTime ReleaseTime => Version.ReleaseTime;

		public string JsonText
		{
			get
			{
				VB_0024AnonymousDelegate_19<string, bool> vB_0024AnonymousDelegate_ = [SpecialName] (string Json) =>
				{
					string text = Json.Trim();
					return StringExtensions.StartsWithF(text, "{", false) && StringExtensions.EndsWithF(text, "}", false);
				};
				if (_JsonText == null)
				{
					string jsonPath = GetJsonPath();
					_JsonText = FileUtils.TryReadAsString(jsonPath, (Encoding)null, (Type)null) ?? "";
					if (!vB_0024AnonymousDelegate_(_JsonText))
					{
						if (ModBase.RunInUi())
						{
							Logger.Warn("版本 JSON 文件为空或有误，由于代码在主线程运行，将不再进行重试", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							StringExtensions.DeserializeJson(_JsonText);
						}
						else
						{
							Logger.Warn($"版本 JSON 文件为空或有误，将在 2s 后重试读取（{jsonPath}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							Thread.Sleep(2000);
							_JsonText = FileUtils.ReadAsString(jsonPath, (Encoding)null, (Type)null);
							if (!vB_0024AnonymousDelegate_(_JsonText))
							{
								StringExtensions.DeserializeJson(_JsonText);
							}
						}
					}
				}
				return _JsonText;
			}
			set
			{
				_JsonText = value;
			}
		}

		public JObject JsonObject
		{
			get
			{
				//IL_0019: Unknown result type (might be due to invalid IL or missing references)
				//IL_0023: Expected O, but got Unknown
				//IL_007c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0083: Expected O, but got Unknown
				//IL_0255: Unknown result type (might be due to invalid IL or missing references)
				//IL_025c: Expected O, but got Unknown
				if (_JsonObject == null)
				{
					string jsonText = JsonText;
					try
					{
						_JsonObject = (JObject)StringExtensions.DeserializeJson(jsonText);
						if (_JsonObject.ContainsKey("patches") && !_JsonObject.ContainsKey("time"))
						{
							IsHmclFormatJson = true;
							JObject val = null;
							List<JObject> list = new List<JObject>();
							foreach (JObject item2 in (IEnumerable<JToken>)_JsonObject["patches"])
							{
								JObject item = item2;
								list.Add(item);
							}
							list = ModBase.SortByComparison(list, [SpecialName] (JObject Left, JObject Right) => ModBase.Val((Left["priority"] ?? JToken.op_Implicit("0")).ToString()) < ModBase.Val((Right["priority"] ?? JToken.op_Implicit("0")).ToString()));
							foreach (JObject item3 in list)
							{
								string text = (string)item3["id"];
								if (text != null)
								{
									Logger.Info($"合并 HMCL 分支项：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
									if (val != null)
									{
										((JContainer)val).Merge((object)item3);
									}
									else
									{
										val = item3;
									}
								}
								else
								{
									Logger.Info("存在为空的 HMCL 分支项", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
								}
							}
							_JsonObject = val;
							_JsonObject["id"] = JToken.op_Implicit(Name);
							if (_JsonObject.ContainsKey("inheritsFrom"))
							{
								_JsonObject.Remove("inheritsFrom");
							}
						}
						object obj = null;
						try
						{
							obj = ((_JsonObject["inheritsFrom"] == null) ? "" : _JsonObject["inheritsFrom"].ToString());
							if (Operators.ConditionalCompareObjectEqual(obj, Name, TextCompare: false))
							{
								Logger.Warn($"自引用的继承版本：{Name}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
								obj = "";
							}
							else
							{
								while (Operators.ConditionalCompareObjectNotEqual(obj, "", TextCompare: false))
								{
									McInstance mcInstance = new McInstance(Conversions.ToString(obj));
									if (Operators.ConditionalCompareObjectEqual(mcInstance.InheritName, obj, TextCompare: false))
									{
										throw new Exception(Conversions.ToString(Operators.ConcatenateObject("版本依赖项出现嵌套：", obj)));
									}
									obj = mcInstance.InheritName;
									JArray val2 = (JArray)_JsonObject["libraries"].DeepClone();
									foreach (JToken item4 in (IEnumerable<JToken>)mcInstance.JsonObject["libraries"])
									{
										val2.Add(item4);
									}
									((JContainer)mcInstance.JsonObject).Merge((object)_JsonObject);
									_JsonObject = mcInstance.JsonObject;
									_JsonObject["libraries"] = (JToken)(object)val2;
								}
							}
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							Logger.Warn(ex2, string.Format("合并版本依赖项 JSON 失败（{0}）", RuntimeHelpers.GetObjectValue(obj ?? "null")), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
							ProjectData.ClearProjectError();
						}
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception innerException = ex3;
						throw new Exception("初始化版本 JSON 时失败（" + (Name ?? "null") + "）", innerException);
					}
				}
				return _JsonObject;
			}
			set
			{
				_JsonObject = value;
			}
		}

		public bool IsOldJson
		{
			get
			{
				if (JsonObject["minecraftArguments"] != null)
				{
					return (string)JsonObject["minecraftArguments"] != "";
				}
				return false;
			}
		}

		public bool IsHmclFormatJson { get; set; }

		public JObject JsonVersion
		{
			get
			{
				//IL_0100: Unknown result type (might be due to invalid IL or missing references)
				//IL_010a: Expected O, but got Unknown
				if (_0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited_0024Init == null)
				{
					Interlocked.CompareExchange(ref _0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited_0024Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken = false;
				try
				{
					Monitor.Enter(_0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited_0024Init, ref lockTaken);
					if (_0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited_0024Init.State == 0)
					{
						_0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited_0024Init.State = 2;
						_0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited = false;
					}
					else if (_0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited_0024Init.State = 1;
					if (lockTaken)
					{
						Monitor.Exit(_0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited_0024Init);
					}
				}
				if (!_0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited)
				{
					_0024STATIC_0024get_JsonVersion_00242001281F1_0024JsonVersionInited = true;
					try
					{
						if (FileUtils.Exists($"{PathVersion}{Name}.jar"))
						{
							ZipArchive val = FileUtils.OpenZip(PathVersion + Name + ".jar", default(CancellationToken));
							try
							{
								ZipArchiveEntry entry = val.GetEntry("version.json");
								if (entry != null)
								{
									using StreamReader streamReader = new StreamReader(entry.Open());
									_JsonVersion = (JObject)StringExtensions.DeserializeJson(streamReader.ReadToEnd());
								}
							}
							finally
							{
								((IDisposable)val)?.Dispose();
							}
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, $"从版本 jar 中读取 version.json 失败（{PathVersion}{Name}.jar）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						ProjectData.ClearProjectError();
					}
				}
				return _JsonVersion;
			}
		}

		public string InheritName
		{
			get
			{
				if (_InheritName == null)
				{
					_InheritName = (JsonObject["inheritsFrom"] ?? JToken.op_Implicit("")).ToString();
					if (JsonText.Contains("liteloader") && Operators.CompareString(Version.VanillaName, Name, TextCompare: false) != 0 && !JsonText.Contains("logging") && Operators.CompareString((JsonObject["jar"] ?? JToken.op_Implicit(Version.VanillaName)).ToString(), Version.VanillaName, TextCompare: false) == 0)
					{
						_InheritName = Version.VanillaName;
					}
					if (IsHmclFormatJson)
					{
						_InheritName = "";
					}
				}
				return _InheritName;
			}
		}

		public string GetJsonPath()
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			string text = PathVersion + Name + ".json";
			if (!FileUtils.Exists(text))
			{
				text = null;
				foreach (string item in DirectoryUtils.EnumerateFiles(PathVersion, false, "*.json"))
				{
					try
					{
						JObject val = (JObject)FileUtils.ReadAsJson(item, (Encoding)null, (Type)null);
						if (!val.ContainsKey("mainClass") || !val.ContainsKey("type") || !val.ContainsKey("id"))
						{
							continue;
						}
						goto IL_008d;
					}
					catch (Exception projectError)
					{
						ProjectData.SetProjectError(projectError);
						ProjectData.ClearProjectError();
					}
					continue;
					IL_008d:
					text = item;
					break;
				}
				if (text == null)
				{
					throw new Exception($"未找到版本 JSON 文件：{PathVersion}{Name}.json");
				}
				Logger.Warn($"未找到同名版本 JSON，自动换用 {text}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			}
			return text;
		}

		public void ResetSettingsCache()
		{
			ModBase.IniClearCache(PathIndie + "options.txt");
			ModBase.IniClearCache(PathVersion + "PCL\\Setup.ini");
			Config.DiscardCache();
			foreach (Settings.Setting value in Settings.Entries.Values)
			{
				if (value.Source == Settings.Sources.Instance)
				{
					EnumerableExtensions.Remove<string, object>(value.ValueCache, PathVersion);
				}
			}
		}

		public McInstance(string NameOrFullPath)
		{
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Expected O, but got Unknown
			_Name = null;
			Info = "该版本未被加载，请向作者反馈此问题";
			State = McInstanceState.Error;
			IsStar = false;
			DisplayType = McInstanceCardType.Auto;
			_Version = null;
			_JsonText = null;
			_JsonObject = null;
			IsHmclFormatJson = false;
			_JsonVersion = null;
			_InheritName = null;
			IsLoaded = false;
			NameOrFullPath = PathUtils.RemoveExtendedPrefix(NameOrFullPath);
			PathVersion = PathUtils.AddSlashSuffix((NameOrFullPath.Contains(":") ? "" : (McFolderSelected + "versions\\")) + NameOrFullPath);
			Config = new JsonConfigProvider(Path.Combine(PathVersion, "PCL\\config.json"));
		}

		public bool Check()
		{
			bool result;
			if (!DirectoryUtils.Exists(PathVersion))
			{
				State = McInstanceState.Error;
				Info = "未找到版本 " + Name;
				result = false;
			}
			else
			{
				try
				{
					if (JsonObject["mainClass"] == null)
					{
						throw new Exception("版本 JSON 中没有 mainClass 项！");
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, $"版本 JSON 可用性检查失败（{PathVersion}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					JsonText = "";
					JsonObject = null;
					Info = ex2.Message;
					State = McInstanceState.Error;
					result = false;
					ProjectData.ClearProjectError();
					goto IL_01c9;
				}
				try
				{
					if (string.IsNullOrEmpty(Version.VanillaName))
					{
						throw new Exception("无法获取版本号，结果为空");
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, $"版本号获取失败（{Name}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					State = McInstanceState.Error;
					Info = "版本号获取失败：" + ExceptionExtensions.GetDisplay(ex4, false);
					result = false;
					ProjectData.ClearProjectError();
					goto IL_01c9;
				}
				try
				{
					if (Operators.CompareString(InheritName, "", TextCompare: false) != 0 && !FileUtils.Exists($"{PathUtils.RemoveLastPart(PathVersion)}\\{InheritName}\\{InheritName}.json"))
					{
						State = McInstanceState.Error;
						Info = "需要安装 " + InheritName + " 作为前置版本";
						result = false;
						goto IL_01c9;
					}
				}
				catch (Exception ex5)
				{
					ProjectData.SetProjectError(ex5);
					Exception ex6 = ex5;
					Logger.Warn(ex6, $"依赖版本检查出错（{Name}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					State = McInstanceState.Error;
					Info = "未知错误：" + ExceptionExtensions.GetDisplay(ex6, false);
					result = false;
					ProjectData.ClearProjectError();
					goto IL_01c9;
				}
				State = McInstanceState.Original;
				result = true;
			}
			goto IL_01c9;
			IL_01c9:
			return result;
		}

		public McInstance Load()
		{
			//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_042a: Unknown result type (might be due to invalid IL or missing references)
			//IL_045a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_049b: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				if (Check())
				{
					string vanillaName = Version.VanillaName;
					if (Operators.CompareString(vanillaName, "Unknown", TextCompare: false) != 0)
					{
						if (Operators.CompareString(vanillaName, "Old", TextCompare: false) == 0)
						{
							State = McInstanceState.Old;
						}
						else
						{
							if (Version.IsFool)
							{
								State = McInstanceState.Fool;
							}
							else if (Version.IsSnapshot)
							{
								State = McInstanceState.Snapshot;
							}
							if (Version.HasOptiFine)
							{
								State = McInstanceState.OptiFine;
							}
							if (EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)2))
							{
								State = McInstanceState.LiteLoader;
							}
							if (EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)4))
							{
								State = McInstanceState.Fabric;
							}
							else if (EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)1))
							{
								State = McInstanceState.Forge;
							}
							else if (EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)16))
							{
								State = McInstanceState.NeoForge;
							}
						}
					}
					else
					{
						State = McInstanceState.Error;
					}
				}
				Logo = ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "Logo");
				if (Operators.CompareString(Logo, "", TextCompare: false) == 0 || !Conversions.ToBoolean(ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "LogoCustom", Conversions.ToString(Value: false))))
				{
					switch (State)
					{
					case McInstanceState.Original:
						Logo = ModBase.PathImage + "Blocks/Grass.png";
						break;
					case McInstanceState.Snapshot:
						Logo = ModBase.PathImage + "Blocks/CommandBlock.png";
						break;
					case McInstanceState.Old:
						Logo = ModBase.PathImage + "Blocks/CobbleStone.png";
						break;
					case McInstanceState.Forge:
						Logo = ModBase.PathImage + "Blocks/Anvil.png";
						break;
					case McInstanceState.NeoForge:
						Logo = ModBase.PathImage + "Blocks/NeoForge.png";
						break;
					case McInstanceState.Fabric:
						Logo = ModBase.PathImage + "Blocks/Fabric.png";
						break;
					case McInstanceState.OptiFine:
						Logo = ModBase.PathImage + "Blocks/GrassPath.png";
						break;
					case McInstanceState.LiteLoader:
						Logo = ModBase.PathImage + "Blocks/Egg.png";
						break;
					case McInstanceState.Fool:
						Logo = ModBase.PathImage + "Blocks/GoldBlock.png";
						break;
					default:
						Logo = ModBase.PathImage + "Blocks/RedstoneBlock.png";
						break;
					}
				}
				RefreshInfo();
				IsStar = Conversions.ToBoolean(ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "IsStar", Conversions.ToString(Value: false)));
				DisplayType = (McInstanceCardType)Conversions.ToInteger(ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
				if (DirectoryUtils.Exists(PathVersion))
				{
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "State", Conversions.ToString((int)State));
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "Info", Info);
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "Logo", Logo);
				}
				if (State != 0 && Version.Reliable)
				{
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "ReleaseTime", ReleaseTime.ToString("yyyy'-'MM'-'dd HH':'mm"));
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionFabric", EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)4) ? Version.ModLoaderVersions[(ModLoaders)4] : null);
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionOptiFine", Version.OptiFineAsMod ? null : Version.OptiFine);
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionLiteLoader", Conversions.ToString(EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)2)));
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionForge", EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)1) ? Version.ModLoaderVersions[(ModLoaders)1] : null);
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionNeoForge", EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)16) ? Version.ModLoaderVersions[(ModLoaders)16] : null);
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionVanillaName", Version.VanillaName);
					ModBase.WriteIni(PathVersion + "PCL\\Setup.ini", "VersionVanilla", Version.Vanilla.ToString());
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Info = ExceptionExtensions.GetDisplay(ex2, false);
				Logo = ModBase.PathImage + "Blocks/RedstoneBlock.png";
				State = McInstanceState.Error;
				Logger.Warn(ex2, $"加载版本失败（{Name}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
			finally
			{
				IsLoaded = true;
			}
			return this;
		}

		public void RefreshInfo()
		{
			string text = ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "CustomInfo");
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				Info = text;
				return;
			}
			Info = VersionDisplayName();
			if (State == McInstanceState.Fool)
			{
				Info = GetMcFoolName(Version.VanillaName);
			}
			else if (State != 0)
			{
				if (Settings.Get<int>("VersionServerLogin", this) == 3)
				{
					Info += ", 统一通行证验证";
				}
				if (Settings.Get<int>("VersionServerLogin", this) == 4)
				{
					Info += ", Authlib 验证";
				}
			}
		}

		public string VersionDisplayName()
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0197: Unknown result type (might be due to invalid IL or missing references)
			string text = "";
			if (EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)1))
			{
				text = text + ", Forge" + ((Operators.CompareString(Version.ModLoaderVersions[(ModLoaders)1], "未知版本", TextCompare: false) == 0) ? "" : (" " + Version.ModLoaderVersions[(ModLoaders)1]));
			}
			if (EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)16))
			{
				text = text + ", NeoForge" + ((Operators.CompareString(Version.ModLoaderVersions[(ModLoaders)16], "未知版本", TextCompare: false) == 0) ? "" : (" " + Version.ModLoaderVersions[(ModLoaders)16]));
			}
			if (EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)4))
			{
				text = text + ", Fabric" + ((Operators.CompareString(Version.ModLoaderVersions[(ModLoaders)4], "未知版本", TextCompare: false) == 0) ? "" : (" " + Version.ModLoaderVersions[(ModLoaders)4]));
			}
			if (Version.HasOptiFine)
			{
				text = text + ", OptiFine" + ((Operators.CompareString(Version.OptiFine, "未知版本", TextCompare: false) == 0) ? "" : (" " + Version.OptiFine.Replace("-", " ").Replace("_", " ")));
			}
			if (EnumUtils.HasFlagF<ModLoaders>(Version.ModLoaders, (ModLoaders)2))
			{
				text += ", LiteLoader";
			}
			string text2;
			switch (State)
			{
			case McInstanceState.Original:
			case McInstanceState.Snapshot:
			case McInstanceState.OptiFine:
			case McInstanceState.Forge:
			case McInstanceState.NeoForge:
			case McInstanceState.LiteLoader:
			case McInstanceState.Fabric:
				text2 = ((!StringExtensions.ContainsIgnoreCase(Version.VanillaName, "pre")) ? ((!StringExtensions.ContainsIgnoreCase(Version.VanillaName, "rc")) ? ((!StringExtensions.ContainsIgnoreCase(Version.VanillaName, "experimental")) ? ((Operators.CompareString(Version.VanillaName, "pending", TextCompare: false) != 0) ? ((!Version.IsSnapshot) ? (Version.Reliable ? ("正式版 " + Version.VanillaName) : "正式版") : (Version.Reliable ? ("快照版 " + Version.VanillaName.Replace("-snapshot", "")) : "快照版")) : "实验性快照") : ("实验性快照" + Version.VanillaName)) : ("发布候选 " + Version.VanillaName)) : ("预发布版 " + Version.VanillaName));
				break;
			case McInstanceState.Old:
				text2 = "远古版本";
				break;
			case McInstanceState.Fool:
				text2 = "愚人节版本 " + Version.VanillaName;
				break;
			case McInstanceState.Error:
				return Info;
			default:
				return "发生了未知错误，请向作者反馈此问题";
			}
			return (text2 + text).Replace("_", "-");
		}

		public MyVirtualizingElement<MyListItem> ToListItem(Action<MyListItem, EventArgs> ContentHandler = null)
		{
			return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
			{
				MyListItem myListItem = new MyListItem
				{
					Info = Info,
					Height = 42.0,
					Tag = this,
					SnapsToDevicePixels = true,
					Type = MyListItem.CheckType.Clickable,
					ContentHandler = ContentHandler
				};
				myListItem.Inlines.Clear();
				myListItem.Inlines.Add(new Run(Name));
				if (Operators.CompareString(ModBase.ReadIni(PathVersion + "PCL\\Setup.ini", "CustomInfo"), "", TextCompare: false) != 0)
				{
					myListItem.Inlines.Add(new Run("  |  " + VersionDisplayName())
					{
						Foreground = new ModBase.MyColor(215.0, 215.0, 215.0),
						FontSize = 12.0
					});
				}
				try
				{
					if (StringExtensions.EndsWithF(Logo, "PCL\\Logo.png", false))
					{
						myListItem.Logo = PathVersion + "PCL\\Logo.png";
					}
					else
					{
						myListItem.Logo = Logo;
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "加载版本图标失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					myListItem.Logo = "pack://application:,,,/images/Blocks/RedstoneBlock.png";
					ProjectData.ClearProjectError();
				}
				return myListItem;
			})
			{
				Height = 42.0
			};
		}

		public McInstance Rename(string NewName)
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			string name = Name;
			string pathVersion = PathVersion;
			string text = PathUtils.AddSlashSuffix(Path.Combine(PathUtils.RemoveLastPart(pathVersion), NewName));
			string jsonPath = GetJsonPath();
			JObject val;
			try
			{
				val = (JObject)FileUtils.ReadAsJson(jsonPath, (Encoding)null, (Type)null);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "在重命名读取 JSON 时失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				val = JsonObject;
				ProjectData.ClearProjectError();
			}
			Config.Save();
			ResetSettingsCache();
			DirectoryUtils.Move(pathVersion, text);
			if (DirectoryUtils.Exists($"{text}{name}-natives"))
			{
				DirectoryUtils.Move($"{text}{name}-natives", $"{text}{NewName}-natives");
			}
			if (FileUtils.Exists($"{text}{name}.jar"))
			{
				FileUtils.Move($"{text}{name}.jar", $"{text}{NewName}.jar", false);
			}
			if (FileUtils.Exists(text + "PCL\\Setup.ini"))
			{
				FileUtils.Write(text + "PCL\\Setup.ini", FileUtils.ReadAsString(text + "PCL\\Setup.ini", (Encoding)null, (Type)null).Replace(pathVersion, text), (Encoding)null);
			}
			if (Operators.CompareString(ModBase.ReadIni(McFolderSelected + "PCL.ini", "Version"), name, TextCompare: false) == 0)
			{
				ModBase.WriteIni(McFolderSelected + "PCL.ini", "Version", NewName);
			}
			try
			{
				val["id"] = JToken.op_Implicit(NewName);
				FileUtils.Delete(text + PathUtils.GetLastPart(jsonPath), false);
				FileUtils.Write(text + NewName + ".json", ((JToken)val).ToString(), (Encoding)null);
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "重命名版本 json 失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
			return new McInstance(text).Load();
		}

		public override bool Equals(object obj)
		{
			if (obj is McInstance mcInstance)
			{
				return PathUtils.IsSame(PathVersion, mcInstance.PathVersion);
			}
			return false;
		}

		public static bool operator ==(McInstance a, McInstance b)
		{
			if ((object)a == null && (object)b == null)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return PathUtils.IsSame(a.PathVersion, b.PathVersion);
		}

		public static bool operator !=(McInstance a, McInstance b)
		{
			return !(a == b);
		}
	}

	public sealed class McInstanceLock
	{
		private static readonly ConcurrentDictionary<string, IWorker> LockedPaths = new ConcurrentDictionary<string, IWorker>(StringComparer.OrdinalIgnoreCase);

		internal static bool IsAnyLocked => !LockedPaths.IsEmpty;

		public static bool TryAcquire(IWorker Worker, string InstancePath)
		{
			InstancePath = PathUtils.ForCompare(InstancePath);
			if (!LockedPaths.TryAdd(InstancePath, Worker))
			{
				return false;
			}
			Worker.Stopped += [SpecialName] () =>
			{
				ConcurrentDictionary<string, IWorker> lockedPaths = LockedPaths;
				string key = InstancePath;
				IWorker value = null;
				lockedPaths.TryRemove(key, out value);
			};
			return true;
		}

		public static bool IsLocked(McInstance Instance)
		{
			if ((object)Instance == null)
			{
				return false;
			}
			return LockedPaths.ContainsKey(PathUtils.ForCompare(Instance.PathVersion));
		}
	}

	public enum McInstanceCardType
	{
		Star = -1,
		Auto,
		Hidden,
		API,
		OriginalLike,
		Rubbish,
		Fool,
		Error
	}

	public struct McSkinInfo
	{
		public bool IsSlim;

		public string LocalFile;

		public bool IsVaild;
	}

	public class McLibEntry
	{
		public string LocalPath;

		public long Size;

		public bool IsNatives;

		public string SHA1;

		private string _Url;

		public string OriginalName;

		public string Url
		{
			get
			{
				return _Url;
			}
			set
			{
				_Url = (string.IsNullOrWhiteSpace(value) ? null : value);
			}
		}

		public string Name
		{
			get
			{
				if (OriginalName == null)
				{
					return null;
				}
				List<string> list = new List<string>(StringExtensions.Split(OriginalName, ":", false));
				list.RemoveAt(2);
				return EnumerableExtensions.Join((IEnumerable)list, ':');
			}
		}

		public McLibEntry()
		{
			Size = 0L;
			IsNatives = false;
			SHA1 = null;
		}

		public override string ToString()
		{
			return (IsNatives ? "[Native] " : "") + StringUtils.FormatByteSize(Size) + " | " + LocalPath;
		}
	}

	private struct McAssetsToken
	{
		public string LocalPath;

		public string SourcePath;

		public long Size;

		public string Hash;

		public override string ToString()
		{
			return StringUtils.FormatByteSize(Size) + " | " + LocalPath;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__23_002D0
	{
		public McInstance _0024VB_0024Local_Instance;

		public _Closure_0024__23_002D0(_Closure_0024__23_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Instance = arg0._0024VB_0024Local_Instance;
			}
		}

		[SpecialName]
		internal string _Lambda_0024__2(string Key)
		{
			string text = ModBase.ReadIni(_0024VB_0024Local_Instance.PathVersion + "PCL\\Setup.ini", Key, null);
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				return text;
			}
			return null;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__24_002D0
	{
		public VB_0024AnonymousDelegate_21<McInstance, int> _0024VB_0024Local_GetComponentCode;

		public _Closure_0024__24_002D0(_Closure_0024__24_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_GetComponentCode = arg0._0024VB_0024Local_GetComponentCode;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__3(McInstance Left, McInstance Right)
		{
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			if ((Left.ReleaseTime.Year >= 2000 || Right.ReleaseTime.Year >= 2000) && DateTime.Compare(Left.ReleaseTime, Right.ReleaseTime) != 0)
			{
				return DateTime.Compare(Left.ReleaseTime, Right.ReleaseTime) > 0;
			}
			if (EnumUtils.HasFlagF<ModLoaders>(Left.Version.ModLoaders, (ModLoaders)4) != EnumUtils.HasFlagF<ModLoaders>(Right.Version.ModLoaders, (ModLoaders)4))
			{
				return EnumUtils.HasFlagF<ModLoaders>(Left.Version.ModLoaders, (ModLoaders)4);
			}
			if (EnumUtils.HasFlagF<ModLoaders>(Left.Version.ModLoaders, (ModLoaders)16) != EnumUtils.HasFlagF<ModLoaders>(Right.Version.ModLoaders, (ModLoaders)16))
			{
				return EnumUtils.HasFlagF<ModLoaders>(Left.Version.ModLoaders, (ModLoaders)16);
			}
			if (EnumUtils.HasFlagF<ModLoaders>(Left.Version.ModLoaders, (ModLoaders)1) != EnumUtils.HasFlagF<ModLoaders>(Right.Version.ModLoaders, (ModLoaders)1))
			{
				return EnumUtils.HasFlagF<ModLoaders>(Left.Version.ModLoaders, (ModLoaders)1);
			}
			if (Left.Version.HasOptiFine != Right.Version.HasOptiFine)
			{
				return Left.Version.HasOptiFine;
			}
			if (EnumUtils.HasFlagF<ModLoaders>(Left.Version.ModLoaders, (ModLoaders)2) != EnumUtils.HasFlagF<ModLoaders>(Right.Version.ModLoaders, (ModLoaders)2))
			{
				return EnumUtils.HasFlagF<ModLoaders>(Left.Version.ModLoaders, (ModLoaders)2);
			}
			if (_0024VB_0024Local_GetComponentCode(Left) != _0024VB_0024Local_GetComponentCode(Right))
			{
				return _0024VB_0024Local_GetComponentCode(Left) > _0024VB_0024Local_GetComponentCode(Right);
			}
			return Operators.CompareString(Left.Name, Right.Name, TextCompare: false) > 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__48_002D0
	{
		public JToken _0024VB_0024Local_Version;

		public _Closure_0024__48_002D0(_Closure_0024__48_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Version = arg0._0024VB_0024Local_Version;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			ModDownloadLib.McUpdateLogShow(_0024VB_0024Local_Version);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__48_002D1
	{
		public string _0024VB_0024Local_VersionName;

		public _Closure_0024__48_002D1(_Closure_0024__48_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_VersionName = arg0._0024VB_0024Local_VersionName;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			PageDownloadInstall.VersionWaitingSelect = _0024VB_0024Local_VersionName;
			ModMain.FrmMain.PageChange(FormMain.PageType.Download);
		}
	}

	private static string _McFolderSelected;

	public static List<McFolder> McFolderList = new List<McFolder>();

	public static ModLoader.LoaderTask<int, int> McFolderListLoader = new ModLoader.LoaderTask<int, int>("Minecraft Folder List", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
	{
		McFolderListLoadSub();
	}, null, ThreadPriority.AboveNormal);

	private static McInstance _McInstanceSelected;

	public static Dictionary<McInstanceCardType, List<McInstance>> McInstanceList = new Dictionary<McInstanceCardType, List<McInstance>>();

	public static bool McInstanceListForceRefresh = false;

	private static bool IsFirstMcInstanceListLoad = true;

	public static ModLoader.LoaderTask<string, int> McInstanceListLoader = new ModLoader.LoaderTask<string, int>("Minecraft Instance List", InitMcInstanceList)
	{
		ReloadTimeout = 1
	};

	private static readonly object McSkinDownloadLock = RuntimeHelpers.GetObjectValue(new object());

	private static string OSVersion = MyWpfExtension.Computer.Info.OSVersion;

	[SpecialName]
	private static object _0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast_0024Init;

	public static string McFolderSelected
	{
		get
		{
			if (_McFolderSelected == null)
			{
				SetMcFolder(Settings.Get<string>("LaunchFolderSelect"));
			}
			return _McFolderSelected;
		}
		set
		{
			SetMcFolder(value);
		}
	}

	public static McInstance McInstanceSelected
	{
		get
		{
			return _McInstanceSelected;
		}
		set
		{
			if (_0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast_0024Init == null)
			{
				Interlocked.CompareExchange(ref _0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast_0024Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast_0024Init, ref lockTaken);
				if (_0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast_0024Init.State == 0)
				{
					_0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast_0024Init.State = 2;
					_0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast = 0;
				}
				else if (_0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast_0024Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast_0024Init);
				}
			}
			if (object.ReferenceEquals(RuntimeHelpers.GetObjectValue(_0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast), value))
			{
				return;
			}
			_McInstanceSelected = value;
			_0024STATIC_0024set_McInstanceSelected_00240111287E8_0024_McInstanceSelectedLast = value;
			Logger.Info(string.Format("当前选择的 Minecraft 版本：{0}", value?.PathVersion ?? "无"), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			ModBase.WriteIni(McFolderSelected + "PCL.ini", "Version", value?.Name ?? "");
			if ((object)value != null)
			{
				PageDownloadResourceDetail.CachedFolder.Clear();
				if (ModAnimation.AniControlEnabled == 0 && Operators.CompareString(Settings.Get<string>("VersionServerNide", value), Settings.Get<string>("CacheNideServer"), TextCompare: false) != 0 && Settings.Get<int>("VersionServerLogin", value) == 3)
				{
					Settings.Set("CacheNideAccess", "");
					Logger.Info("服务器改变，要求重新登录统一通行证", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				}
				if (Settings.Get<int>("VersionServerLogin", value) == 3)
				{
					Settings.Set("CacheNideServer", Settings.Get<string>("VersionServerNide", value));
				}
				if (ModAnimation.AniControlEnabled == 0 && Operators.CompareString(Settings.Get<string>("VersionServerAuthServer", value), Settings.Get<string>("CacheAuthServerServer"), TextCompare: false) != 0 && Settings.Get<int>("VersionServerLogin", value) == 4)
				{
					Settings.Set("CacheAuthAccess", "");
					Logger.Info("服务器改变，要求重新登录 Authlib-Injector", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				}
				if (Settings.Get<int>("VersionServerLogin", value) == 4)
				{
					Settings.Set("CacheAuthServerServer", Settings.Get<string>("VersionServerAuthServer", value));
					Settings.Set("CacheAuthServerName", Settings.Get<string>("VersionServerAuthName", value));
					Settings.Set("CacheAuthServerRegister", Settings.Get<string>("VersionServerAuthRegister", value));
				}
			}
		}
	}

	private static void SetMcFolder(string Value)
	{
		if (!PathUtils.IsSame(_McFolderSelected, Value.Replace("$", Paths.Base)))
		{
			_McFolderSelected = Value.Replace("$", Paths.Base);
			_McFolderSelected = PathUtils.AddSlashSuffix(PathUtils.ForCompare(_McFolderSelected));
			Settings.Set("LaunchFolderSelect", Value.Replace(Paths.Base, "$"));
			Logger.Info($"当前选择的 Minecraft 文件夹：{_McFolderSelected}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
		}
	}

	private static void McFolderListLoadSub()
	{
		try
		{
			List<McFolder> list = new List<McFolder>();
			try
			{
				if (DirectoryUtils.Exists(Paths.Base + "versions\\"))
				{
					list.Add(new McFolder
					{
						Name = "当前文件夹",
						Location = Paths.Base,
						Type = McFolder.Types.Vanilla
					});
				}
				DirectoryInfo[] directories = DirectoryUtils.GetInfo(Paths.Base).GetDirectories();
				foreach (DirectoryInfo directoryInfo in directories)
				{
					if (DirectoryUtils.Exists(directoryInfo.FullName + "versions\\") || Operators.CompareString(directoryInfo.Name, ".minecraft", TextCompare: false) == 0)
					{
						list.Add(new McFolder
						{
							Name = "当前文件夹",
							Location = PathUtils.AddSlashSuffix(PathUtils.ForCompare(directoryInfo.FullName)),
							Type = McFolder.Types.Vanilla
						});
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "扫描 PCL 所在文件夹中是否有 MC 文件夹失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
			string text = Paths.AppData + ".minecraft\\";
			if ((!list.Any() || !PathUtils.IsSame(text, list[0].Location)) && DirectoryUtils.Exists(text + "versions\\"))
			{
				list.Add(new McFolder
				{
					Name = "官方启动器文件夹",
					Location = text,
					Type = McFolder.Types.Vanilla
				});
			}
			string[] array = StringExtensions.Split(Settings.Get<string>("LaunchFolders").ToString(), '|', true);
			foreach (string text2 in array)
			{
				if (Operators.CompareString(text2, "", TextCompare: false) == 0)
				{
					continue;
				}
				if (!text2.Contains(">") || !StringExtensions.EndsWithF(text2, "\\", false))
				{
					ModMain.Hint("无效的 Minecraft 文件夹：" + text2, ModMain.HintType.Red);
					continue;
				}
				string name = StringExtensions.BeforeFirst(text2, ">", false);
				string text3 = StringExtensions.AfterFirst(text2, ">", false);
				try
				{
					ModBase.CheckPermissionWithException(text3);
					bool flag = false;
					foreach (McFolder item in list)
					{
						if (PathUtils.IsSame(item.Location, text3))
						{
							item.Name = name;
							item.Type = McFolder.Types.RenamedVanilla;
							flag = true;
						}
					}
					if (!flag)
					{
						list.Add(new McFolder
						{
							Name = name,
							Location = PathUtils.AddSlashSuffix(PathUtils.ForCompare(text3)),
							Type = McFolder.Types.Custom
						});
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					ModMain.MyMsgBox("失效的 Minecraft 文件夹：\r\n" + text3 + "\r\n\r\n" + ExceptionExtensions.GetDisplay(ex4, false), "Minecraft 文件夹失效", "确定", "", "", IsWarn: true);
					Logger.Warn(ex4, $"无法访问 Minecraft 文件夹 {text3}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					ProjectData.ClearProjectError();
				}
			}
			List<string> list2 = new List<string>();
			foreach (McFolder item2 in list)
			{
				if (item2.Type != 0)
				{
					list2.Add(item2.Name + ">" + item2.Location);
				}
			}
			if (!list2.Any())
			{
				list2.Add("");
			}
			Settings.Set("LaunchFolders", EnumerableExtensions.Join((IEnumerable)list2, '|'));
			if (!list.Any())
			{
				DirectoryUtils.Create(Paths.Base + ".minecraft\\versions\\");
				list.Add(new McFolder
				{
					Name = "当前文件夹",
					Location = Paths.Base + ".minecraft\\",
					Type = McFolder.Types.Vanilla
				});
			}
			foreach (McFolder item3 in list)
			{
				McFolderLauncherProfilesJsonCreate(item3.Location);
			}
			if (Settings.Get<bool>("SystemDebugDelay"))
			{
				Thread.Sleep(ModBase.RandomInteger(200, 2000));
			}
			McFolderList = list;
		}
		catch (Exception ex5)
		{
			ProjectData.SetProjectError(ex5);
			Exception ex6 = ex5;
			Logger.Error(ex6, "加载 Minecraft 文件夹列表失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			ProjectData.ClearProjectError();
		}
	}

	public static void McFolderLauncherProfilesJsonCreate(string Folder)
	{
		try
		{
			if (!FileUtils.Exists(Folder + "launcher_profiles.json"))
			{
				string text = "{\r\n    \"profiles\":  {\r\n        \"PCL\": {\r\n            \"icon\": \"Grass\",\r\n            \"name\": \"PCL\",\r\n            \"lastVersionId\": \"latest-release\",\r\n            \"type\": \"latest-release\",\r\n            \"lastUsed\": \"" + DateTime.Now.ToString("yyyy'-'MM'-'dd") + "T" + DateTime.Now.ToString("HH':'mm':'ss") + ".0000Z\"\r\n        }\r\n    },\r\n    \"selectedProfile\": \"PCL\",\r\n    \"clientToken\": \"23323323323323323323323323323333\"\r\n}";
				FileUtils.Write(Folder + "launcher_profiles.json", text, Encoding.GetEncoding("GB18030"));
				Logger.Info($"已创建 launcher_profiles.json：{Folder}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, $"创建 launcher_profiles.json 失败（{Folder}）", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			ProjectData.ClearProjectError();
		}
	}

	public static string GetMcFoolName(string Name)
	{
		Name = StringExtensions.Lower(Name);
		if (StringExtensions.StartsWithF(Name, "2.0", false))
		{
			return "2013 | 这个秘密计划了两年的更新将游戏推向了一个新高度！";
		}
		switch (Name)
		{
		case "15w14a":
			return "2015 | 作为一款全年龄向的游戏，我们需要和平，需要爱与拥抱。";
		case "1.rv-pre1":
			return "2016 | 是时候将现代科技带入 Minecraft 了！";
		case "3d shareware v1.34":
			return "2019 | 我们从地下室的废墟里找到了这个开发于 1994 年的杰作！";
		default:
			if (!StringExtensions.StartsWithF(Name, "20w14inf", false))
			{
				switch (Name)
				{
				case "20w14∞":
					break;
				case "22w13oneblockatatime":
					return "2022 | 一次一个方块更新！迎接全新的挖掘、合成与骑乘玩法吧！";
				case "23w13a_or_b":
					return "2023 | 研究表明：玩家喜欢作出选择——越多越好！";
				case "24w14potato":
					return "2024 | 毒马铃薯一直都被大家忽视和低估，于是我们超级加强了它！";
				case "25w14craftmine":
					return "2025 | 你可以合成任何东西——包括合成你的世界！";
				case "26w14a":
					return "2026 | 为什么需要物品栏？让方块们跟着你走吧！";
				default:
					return "";
				}
			}
			return "2020 | 我们加入了 20 亿个新的维度，让无限的想象变成了现实！";
		}
	}

	private static void InitMcInstanceList(ModLoader.LoaderTask<string, int> Loader)
	{
		string input = Loader.Input;
		try
		{
			McInstanceList = new Dictionary<McInstanceCardType, List<McInstance>>();
			List<string> list = new List<string>();
			if (DirectoryUtils.Exists(input + "versions"))
			{
				try
				{
					DirectoryInfo[] directories = DirectoryUtils.GetInfo(input + "versions").GetDirectories();
					foreach (DirectoryInfo directoryInfo in directories)
					{
						list.Add(directoryInfo.Name);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("无法读取版本文件夹，可能是由于没有权限（" + input + "versions）", innerException);
				}
			}
			if (!list.Any())
			{
				ModBase.WriteIni(input + "PCL.ini", "InstanceCache", "");
				goto IL_0167;
			}
			int num = Convert.ToInt32(decimal.Remainder(new decimal(StringExtensions.GetStableHashCode(Conversions.ToString(39) + "#" + EnumerableExtensions.Join((IEnumerable)list, '#'))), 2147483646m));
			if (!McInstanceListForceRefresh && ModBase.Val(ModBase.ReadIni(input + "PCL.ini", "InstanceCache")) == (double)num)
			{
				Dictionary<McInstanceCardType, List<McInstance>> dictionary = InitMcInstanceListWithCache(input);
				if (dictionary != null)
				{
					McInstanceList = dictionary;
					goto IL_0161;
				}
			}
			McInstanceListForceRefresh = false;
			Logger.Info("文件夹列表变更，重载所有版本", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			ModBase.WriteIni(input + "PCL.ini", "InstanceCache", Conversions.ToString(num));
			McInstanceList = InitMcInstanceListWithoutCache(input);
			goto IL_0161;
			IL_0161:
			IsFirstMcInstanceListLoad = false;
			goto IL_0167;
			IL_0167:
			if (Loader.IsCanceled)
			{
				return;
			}
			if (McInstanceList.Any([SpecialName] (KeyValuePair<McInstanceCardType, List<McInstance>> v) => v.Key != McInstanceCardType.Error))
			{
				string text = ModBase.ReadIni(input + "PCL.ini", "Version");
				if (Operators.CompareString(text, "", TextCompare: false) != 0)
				{
					foreach (KeyValuePair<McInstanceCardType, List<McInstance>> mcInstance in McInstanceList)
					{
						foreach (McInstance item in mcInstance.Value)
						{
							if (Operators.CompareString(item.Name, text, TextCompare: false) == 0 && item.State != 0)
							{
								McInstanceSelected = item;
								Logger.Info($"选择该文件夹储存的 Minecraft 版本：{McInstanceSelected.PathVersion}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
								return;
							}
						}
					}
				}
				if (McInstanceList.First().Value[0].State != 0)
				{
					McInstanceSelected = McInstanceList.First().Value[0];
					Logger.Info($"自动选择 Minecraft 版本：{McInstanceSelected.PathVersion}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				}
			}
			else
			{
				McInstanceSelected = null;
				Logger.Info("未找到可用 Minecraft 版本", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			}
			if (Settings.Get<bool>("SystemDebugDelay"))
			{
				Thread.Sleep(ModBase.RandomInteger(200, 3000));
			}
		}
		catch (Exception ex2)
		{
			ProjectData.SetProjectError(ex2);
			Exception ex3 = ex2;
			if (Loader.IsCanceled || ExceptionExtensions.IsCanceled(ex3))
			{
				ProjectData.ClearProjectError();
				return;
			}
			ModBase.WriteIni(input + "PCL.ini", "InstanceCache", "");
			Logger.Error(ex3, "加载 .minecraft 版本列表失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			ProjectData.ClearProjectError();
		}
	}

	private static Dictionary<McInstanceCardType, List<McInstance>> InitMcInstanceListWithCache(string Folder)
	{
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Expected O, but got Unknown
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Expected O, but got Unknown
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<McInstanceCardType, List<McInstance>> dictionary = new Dictionary<McInstanceCardType, List<McInstance>>();
		Dictionary<McInstanceCardType, List<McInstance>> result;
		try
		{
			int num = Conversions.ToInteger(ModBase.ReadIni(Folder + "PCL.ini", "CardCount", Conversions.ToString(-1)));
			if (num == -1)
			{
				result = null;
			}
			else
			{
				int num2 = checked(num - 1);
				int num3 = 0;
				_Closure_0024__23_002D0 closure_0024__23_002D = default(_Closure_0024__23_002D0);
				while (true)
				{
					if (num3 <= num2)
					{
						McInstanceCardType key = (McInstanceCardType)Conversions.ToInteger(ModBase.ReadIni(Folder + "PCL.ini", "CardKey" + Conversions.ToString(checked(num3 + 1)), ":"));
						List<McInstance> list = new List<McInstance>();
						checked
						{
							string[] array = StringExtensions.Split(ModBase.ReadIni(Folder + "PCL.ini", "CardValue" + Conversions.ToString(num3 + 1), ":"), ":", false);
							int num4 = 0;
							while (true)
							{
								unchecked
								{
									if (num4 < array.Length)
									{
										string text = array[num4];
										if (Operators.CompareString(text, "", TextCompare: false) != 0)
										{
											string text2 = $"{Folder}versions\\{text}\\";
											if (FileUtils.Exists(text2 + ".pclignore"))
											{
												if (!IsFirstMcInstanceListLoad)
												{
													Logger.Info($"跳过要求忽略的项目：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
													goto IL_06d1;
												}
												Logger.Info($"清理残留的忽略项目：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
												FileUtils.Delete(text2 + ".pclignore", false);
											}
											try
											{
												closure_0024__23_002D = new _Closure_0024__23_002D0(closure_0024__23_002D);
												closure_0024__23_002D._0024VB_0024Local_Instance = new McInstance(text2);
												list.Add(closure_0024__23_002D._0024VB_0024Local_Instance);
												closure_0024__23_002D._0024VB_0024Local_Instance.Info = ModBase.ReadIni(closure_0024__23_002D._0024VB_0024Local_Instance.PathVersion + "PCL\\Setup.ini", "CustomInfo");
												if (Operators.CompareString(closure_0024__23_002D._0024VB_0024Local_Instance.Info, "", TextCompare: false) == 0)
												{
													closure_0024__23_002D._0024VB_0024Local_Instance.Info = ModBase.ReadIni(closure_0024__23_002D._0024VB_0024Local_Instance.PathVersion + "PCL\\Setup.ini", "Info", closure_0024__23_002D._0024VB_0024Local_Instance.Info);
												}
												closure_0024__23_002D._0024VB_0024Local_Instance.Logo = ModBase.ReadIni(closure_0024__23_002D._0024VB_0024Local_Instance.PathVersion + "PCL\\Setup.ini", "Logo", closure_0024__23_002D._0024VB_0024Local_Instance.Logo);
												closure_0024__23_002D._0024VB_0024Local_Instance.State = (McInstance.McInstanceState)Conversions.ToInteger(ModBase.ReadIni(closure_0024__23_002D._0024VB_0024Local_Instance.PathVersion + "PCL\\Setup.ini", "State", Conversions.ToString((int)closure_0024__23_002D._0024VB_0024Local_Instance.State)));
												closure_0024__23_002D._0024VB_0024Local_Instance.IsStar = Conversions.ToBoolean(ModBase.ReadIni(closure_0024__23_002D._0024VB_0024Local_Instance.PathVersion + "PCL\\Setup.ini", "IsStar", Conversions.ToString(Value: false)));
												closure_0024__23_002D._0024VB_0024Local_Instance.DisplayType = (McInstanceCardType)Conversions.ToInteger(ModBase.ReadIni(Folder + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
												McInstallManifest val = ((closure_0024__23_002D._0024VB_0024Local_Instance.State == McInstance.McInstanceState.Error) ? null : Configs.McInstallManifest.Get((IConfigProvider)(object)closure_0024__23_002D._0024VB_0024Local_Instance.Config));
												if (val != null && val.SchemaVersion <= 1 && val.ManagedFiles.Any([SpecialName] (ManagedFile File) => File.Component != null))
												{
													McInstallManifest val2 = val.RefreshModComponents(closure_0024__23_002D._0024VB_0024Local_Instance.PathIndie);
													if (val2 != val)
													{
														val = val2;
														Configs.McInstallManifest.Set(val, (IConfigProvider)(object)closure_0024__23_002D._0024VB_0024Local_Instance.Config);
														closure_0024__23_002D._0024VB_0024Local_Instance.Config.Save();
													}
												}
												string text3 = ModBase.ReadIni(closure_0024__23_002D._0024VB_0024Local_Instance.PathVersion + "PCL\\Setup.ini", "VersionVanillaName", "Unknown");
												if (closure_0024__23_002D._0024VB_0024Local_Instance.State != 0 && (val != null || Operators.CompareString(text3, "Unknown", TextCompare: false) != 0))
												{
													McVersion val3;
													if (val != null)
													{
														val3 = new McVersion(val.Components)
														{
															OptiFineAsMod = val.ManagedFiles.Any([SpecialName] (ManagedFile File) => Operators.CompareString(File.Component, "OptiFine", TextCompare: false) == 0)
														};
													}
													else
													{
														VB_0024AnonymousDelegate_20<string, string> vB_0024AnonymousDelegate_ = closure_0024__23_002D._Lambda_0024__2;
														val3 = new McVersion(text3, true)
														{
															OptiFine = vB_0024AnonymousDelegate_("VersionOptiFine")
														};
														ModLoaders[] array2 = new ModLoaders[3];
														RuntimeHelpers.InitializeArray(array2, (RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/);
														ModLoaders[] array3 = (ModLoaders[])(object)array2;
														foreach (ModLoaders val4 in array3)
														{
															string text4 = vB_0024AnonymousDelegate_($"Version{val4}");
															if (text4 != null)
															{
																val3.ModLoaderVersions[val4] = text4;
															}
														}
														if (Conversions.ToBoolean(ModBase.ReadIni(closure_0024__23_002D._0024VB_0024Local_Instance.PathVersion + "PCL\\Setup.ini", "VersionLiteLoader", Conversions.ToString(Value: false))))
														{
															val3.ModLoaderVersions[(ModLoaders)2] = "未知版本";
														}
													}
													val3.ReleaseTime = Conversions.ToDate(ModBase.ReadIni(closure_0024__23_002D._0024VB_0024Local_Instance.PathVersion + "PCL\\Setup.ini", "ReleaseTime", Conversions.ToString(new DateTime(1970, 1, 1, 15, 0, 0))));
													val3.IsFool = val3.IsFool || closure_0024__23_002D._0024VB_0024Local_Instance.State == McInstance.McInstanceState.Fool;
													val3.IsSnapshot = val3.IsSnapshot || closure_0024__23_002D._0024VB_0024Local_Instance.State == McInstance.McInstanceState.Snapshot || StringExtensions.ContainsIgnoreCase(closure_0024__23_002D._0024VB_0024Local_Instance.Name, "combat");
													closure_0024__23_002D._0024VB_0024Local_Instance.Version = val3;
													if (val != null)
													{
														closure_0024__23_002D._0024VB_0024Local_Instance.RefreshInfo();
													}
												}
												else
												{
													closure_0024__23_002D._0024VB_0024Local_Instance.Load();
												}
												if (closure_0024__23_002D._0024VB_0024Local_Instance.State == McInstance.McInstanceState.Error)
												{
													string info = closure_0024__23_002D._0024VB_0024Local_Instance.Info;
													closure_0024__23_002D._0024VB_0024Local_Instance.State = McInstance.McInstanceState.Original;
													closure_0024__23_002D._0024VB_0024Local_Instance.Check();
													string left = ModBase.ReadIni(closure_0024__23_002D._0024VB_0024Local_Instance.PathVersion + "PCL\\Setup.ini", "CustomInfo");
													if (closure_0024__23_002D._0024VB_0024Local_Instance.State == McInstance.McInstanceState.Original || (Operators.CompareString(left, "", TextCompare: false) == 0 && Operators.CompareString(info, closure_0024__23_002D._0024VB_0024Local_Instance.Info, TextCompare: false) != 0))
													{
														Logger.Info($"版本 {closure_0024__23_002D._0024VB_0024Local_Instance.Name} 的错误状态已变更，新的状态为：{closure_0024__23_002D._0024VB_0024Local_Instance.Info}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
														result = null;
														goto end_IL_06fb;
													}
												}
												if (Operators.CompareString(closure_0024__23_002D._0024VB_0024Local_Instance.Logo, "", TextCompare: false) == 0)
												{
													Logger.Info($"版本 {closure_0024__23_002D._0024VB_0024Local_Instance.Name} 未被加载", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
													result = null;
													goto end_IL_06fb;
												}
											}
											catch (Exception ex)
											{
												ProjectData.SetProjectError(ex);
												Exception ex2 = ex;
												Logger.Warn(ex2, $"读取版本加载缓存失败（{text}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
												result = null;
												ProjectData.ClearProjectError();
												goto end_IL_06fb;
											}
										}
										goto IL_06d1;
									}
									if (list.Any())
									{
										dictionary.Add(key, list);
									}
								}
								num3++;
								break;
								IL_06d1:
								num4++;
							}
							continue;
						}
					}
					result = dictionary;
					break;
					continue;
					end_IL_06fb:
					break;
				}
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Warn(ex4, "读取版本缓存失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			result = null;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	private static Dictionary<McInstanceCardType, List<McInstance>> InitMcInstanceListWithoutCache(string Folder)
	{
		List<McInstance> InstanceList = new List<McInstance>();
		DirectoryInfo[] directories = DirectoryUtils.GetInfo(Folder + "versions").GetDirectories();
		foreach (DirectoryInfo directoryInfo in directories)
		{
			if (!directoryInfo.Exists || !directoryInfo.EnumerateFiles().Any())
			{
				Logger.Info($"跳过空文件夹：{directoryInfo.FullName}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				continue;
			}
			if ((Operators.CompareString(directoryInfo.Name, "cache", TextCompare: false) == 0 || Operators.CompareString(directoryInfo.Name, "BLClient", TextCompare: false) == 0 || Operators.CompareString(directoryInfo.Name, "PCL", TextCompare: false) == 0) && !FileUtils.Exists(directoryInfo.FullName + "\\" + directoryInfo.Name + ".json"))
			{
				Logger.Info($"跳过可能不是版本文件夹的项目：{directoryInfo.FullName}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				continue;
			}
			string text = directoryInfo.FullName + "\\";
			if (FileUtils.Exists(text + ".pclignore"))
			{
				if (!IsFirstMcInstanceListLoad)
				{
					Logger.Info($"跳过要求忽略的项目：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					continue;
				}
				Logger.Info($"清理残留的忽略项目：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				FileUtils.Delete(text + ".pclignore", false);
			}
			McInstance mcInstance = new McInstance(text);
			InstanceList.Add(mcInstance);
			mcInstance.Load();
		}
		Dictionary<McInstanceCardType, List<McInstance>> dictionary = new Dictionary<McInstanceCardType, List<McInstance>>();
		try
		{
			Dictionary<McInstanceCardType, List<McInstance>> Target = new Dictionary<McInstanceCardType, List<McInstance>>();
			List<McInstance> list = new List<McInstance>();
			foreach (McInstance item in InstanceList.ToList())
			{
				if (item.IsStar && item.DisplayType != McInstanceCardType.Hidden)
				{
					list.Add(item);
					InstanceList.Remove(item);
				}
			}
			if (list.Any())
			{
				Target.Add(McInstanceCardType.Star, list);
			}
			McInstanceFilter(ref InstanceList, ref Target, new McInstance.McInstanceState[1], McInstanceCardType.Error);
			McInstanceFilter(ref InstanceList, ref Target, new McInstance.McInstanceState[1] { McInstance.McInstanceState.Fool }, McInstanceCardType.Fool);
			McInstanceFilter(ref InstanceList, ref Target, new McInstance.McInstanceState[4]
			{
				McInstance.McInstanceState.Forge,
				McInstance.McInstanceState.NeoForge,
				McInstance.McInstanceState.LiteLoader,
				McInstance.McInstanceState.Fabric
			}, McInstanceCardType.API);
			List<McInstance> list2 = new List<McInstance>();
			List<McInstance> KeepList = new List<McInstance>();
			McInstanceFilter(ref InstanceList, new McInstance.McInstanceState[1] { McInstance.McInstanceState.Old }, ref KeepList);
			McInstance mcInstance2 = EnumerableExtensions.MaxBy<McInstance, DateTime>(InstanceList.Where([SpecialName] (McInstance v) => v.State == McInstance.McInstanceState.Original || v.State == McInstance.McInstanceState.Snapshot), (Func<McInstance, DateTime>)([SpecialName] (McInstance v) => v.ReleaseTime));
			if ((object)mcInstance2 != null && mcInstance2.State == McInstance.McInstanceState.Snapshot)
			{
				list2.Add(mcInstance2);
				InstanceList.Remove(mcInstance2);
			}
			McInstanceFilter(ref InstanceList, new McInstance.McInstanceState[1] { McInstance.McInstanceState.Snapshot }, ref KeepList);
			Dictionary<string, McInstance> dictionary2 = new Dictionary<string, McInstance>();
			List<int> list3 = new List<int>();
			foreach (McInstance item2 in InstanceList)
			{
				if (!item2.Version.Valid)
				{
					continue;
				}
				if (!list3.Contains(item2.Version.Drop))
				{
					list3.Add(item2.Version.Drop);
				}
				string key = Conversions.ToString(item2.Version.Drop) + "-" + Conversions.ToString((int)item2.State);
				if (!dictionary2.ContainsKey(key))
				{
					dictionary2.Add(key, item2);
				}
				else if (item2.State == McInstance.McInstanceState.OptiFine)
				{
					if (item2.Version.OptiFineCode > dictionary2[key].Version.OptiFineCode)
					{
						dictionary2[key] = item2;
					}
				}
				else if (DateTime.Compare(item2.ReleaseTime, dictionary2[key].ReleaseTime) > 0)
				{
					dictionary2[key] = item2;
				}
			}
			foreach (int item3 in list3)
			{
				if (dictionary2.ContainsKey(Conversions.ToString(item3) + "-" + Conversions.ToString(4)) && dictionary2.ContainsKey(Conversions.ToString(item3) + "-" + Conversions.ToString(1)))
				{
					McInstance mcInstance3 = dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(1)];
					McInstance mcInstance4 = dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(4)];
					if (mcInstance3.Version.Drop > mcInstance4.Version.Drop)
					{
						list2.Add(mcInstance3);
						InstanceList.Remove(mcInstance3);
					}
					list2.Add(mcInstance4);
					InstanceList.Remove(mcInstance4);
				}
				else if (dictionary2.ContainsKey(Conversions.ToString(item3) + "-" + Conversions.ToString(4)))
				{
					list2.Add(dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(4)]);
					InstanceList.Remove(dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(4)]);
				}
				else if (dictionary2.ContainsKey(Conversions.ToString(item3) + "-" + Conversions.ToString(1)))
				{
					list2.Add(dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(1)]);
					InstanceList.Remove(dictionary2[Conversions.ToString(item3) + "-" + Conversions.ToString(1)]);
				}
			}
			KeepList.AddRange(InstanceList);
			if (list2.Any())
			{
				Target.Add(McInstanceCardType.OriginalLike, list2);
			}
			if (KeepList.Any())
			{
				Target.Add(McInstanceCardType.Rubbish, KeepList);
			}
			foreach (KeyValuePair<McInstanceCardType, List<McInstance>> item4 in Target)
			{
				foreach (McInstance item5 in item4.Value)
				{
					McInstanceCardType key2 = ((item5.DisplayType == McInstanceCardType.Auto || item4.Key == McInstanceCardType.Star) ? item4.Key : item5.DisplayType);
					if (!dictionary.ContainsKey(key2))
					{
						dictionary.Add(key2, new List<McInstance>());
					}
					dictionary[key2].Add(item5);
				}
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			dictionary.Clear();
			Logger.Error(ex2, "分类版本列表失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			ProjectData.ClearProjectError();
		}
		Dictionary<McInstanceCardType, List<McInstance>> dictionary3 = new Dictionary<McInstanceCardType, List<McInstance>>();
		McInstanceCardType[] array = new McInstanceCardType[7]
		{
			McInstanceCardType.Star,
			McInstanceCardType.API,
			McInstanceCardType.OriginalLike,
			McInstanceCardType.Rubbish,
			McInstanceCardType.Fool,
			McInstanceCardType.Error,
			McInstanceCardType.Hidden
		};
		for (int j = 0; j < array.Length; j = checked(j + 1))
		{
			string value = Conversions.ToString((int)array[j]);
			if (dictionary.ContainsKey((McInstanceCardType)Conversions.ToInteger(value)))
			{
				dictionary3.Add((McInstanceCardType)Conversions.ToInteger(value), dictionary[(McInstanceCardType)Conversions.ToInteger(value)]);
			}
		}
		dictionary = dictionary3;
		McInstanceCardType[] array2 = new McInstanceCardType[5]
		{
			McInstanceCardType.Star,
			McInstanceCardType.API,
			McInstanceCardType.OriginalLike,
			McInstanceCardType.Rubbish,
			McInstanceCardType.Fool
		};
		_Closure_0024__24_002D0 closure_0024__24_002D = default(_Closure_0024__24_002D0);
		foreach (McInstanceCardType key3 in array2)
		{
			closure_0024__24_002D = new _Closure_0024__24_002D0(closure_0024__24_002D);
			if (!dictionary.ContainsKey(key3))
			{
				continue;
			}
			closure_0024__24_002D._0024VB_0024Local_GetComponentCode = [SpecialName] (McInstance Instance) =>
			{
				if (Instance.Version.ForgelikeCode > 0)
				{
					return Instance.Version.ForgelikeCode;
				}
				return Instance.Version.HasOptiFine ? Instance.Version.OptiFineCode : 0;
			};
			dictionary[key3] = ModBase.SortByComparison(dictionary[key3], closure_0024__24_002D._Lambda_0024__3);
		}
		ModBase.WriteIni(Folder + "PCL.ini", "CardCount", Conversions.ToString(dictionary.Count));
		checked
		{
			int num = dictionary.Count - 1;
			for (int l = 0; l <= num; l++)
			{
				ModBase.WriteIni(Folder + "PCL.ini", "CardKey" + Conversions.ToString(l + 1), Conversions.ToString(unchecked((int)dictionary.Keys.ElementAtOrDefault(l))));
				string text2 = "";
				foreach (McInstance item6 in dictionary.Values.ElementAtOrDefault(l))
				{
					text2 = text2 + item6.Name + ":";
				}
				ModBase.WriteIni(Folder + "PCL.ini", "CardValue" + Conversions.ToString(l + 1), text2);
			}
			return dictionary;
		}
	}

	private static void McInstanceFilter(ref List<McInstance> InstanceList, ref Dictionary<McInstanceCardType, List<McInstance>> Target, McInstance.McInstanceState[] Formula, McInstanceCardType CardType)
	{
		List<McInstance> list = InstanceList.Where([SpecialName] (McInstance v) => Formula.Contains(v.State)).ToList();
		if (list.Any())
		{
			Target.Add(CardType, list);
			InstanceList = InstanceList.Except(list).ToList();
		}
	}

	private static void McInstanceFilter(ref List<McInstance> InstanceList, McInstance.McInstanceState[] Formula, ref List<McInstance> KeepList)
	{
		KeepList.AddRange(InstanceList.Where([SpecialName] (McInstance v) => Formula.Contains(v.State)));
		if (KeepList.Any())
		{
			InstanceList = InstanceList.Except(KeepList).ToList();
		}
	}

	public static McSkinInfo McSkinSelect()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		string text = Dialogs.SelectFile("选择皮肤文件", false, (string)null, (OneOf<IEnumerable<ValueTuple<string[], string>>, string>?)OneOf<IEnumerable<(string[], string)>, string>.op_Implicit((IEnumerable<(string[], string)>)new(string[], string)[1] { (new string[4] { "png", "jpg", "jpeg", "webp" }, "皮肤文件") })).FirstOrDefault();
		McSkinInfo result;
		if (string.IsNullOrEmpty(text))
		{
			McSkinInfo mcSkinInfo = default(McSkinInfo);
			mcSkinInfo.IsVaild = false;
			result = mcSkinInfo;
		}
		else
		{
			try
			{
				MyBitmap myBitmap = new MyBitmap(text);
				if (myBitmap.Pic.Width != 64 || (myBitmap.Pic.Height != 32 && myBitmap.Pic.Height != 64))
				{
					ModMain.Hint("皮肤图片大小应为 64x32 像素或 64x64 像素！", ModMain.HintType.Red);
					McSkinInfo mcSkinInfo = default(McSkinInfo);
					mcSkinInfo.IsVaild = false;
					result = mcSkinInfo;
					goto IL_01d8;
				}
				FileInfo info = FileUtils.GetInfo(text);
				if (info.Length > 24576)
				{
					ModMain.Hint("皮肤文件大小需小于 24 KB，而所选文件大小为 " + Conversions.ToString(Math.Round((double)info.Length / 1024.0, 2)) + " KB", ModMain.HintType.Red);
					McSkinInfo mcSkinInfo = default(McSkinInfo);
					mcSkinInfo.IsVaild = false;
					result = mcSkinInfo;
					goto IL_01d8;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "皮肤文件存在错误", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				McSkinInfo mcSkinInfo = default(McSkinInfo);
				mcSkinInfo.IsVaild = false;
				result = mcSkinInfo;
				ProjectData.ClearProjectError();
				goto IL_01d8;
			}
			int num = ModMain.MyMsgBox("此皮肤为 Steve 模型（粗手臂）还是 Alex 模型（细手臂）？", "选择皮肤种类", "Steve 模型", "Alex 模型", "我不知道", IsWarn: false, HighLight: false);
			if (num == 3)
			{
				ModMain.Hint("请在皮肤下载页面确认皮肤种类后再使用此皮肤！");
				McSkinInfo mcSkinInfo = default(McSkinInfo);
				mcSkinInfo.IsVaild = false;
				result = mcSkinInfo;
			}
			else
			{
				McSkinInfo mcSkinInfo = default(McSkinInfo);
				mcSkinInfo.IsVaild = true;
				mcSkinInfo.IsSlim = num == 2;
				mcSkinInfo.LocalFile = text;
				result = mcSkinInfo;
			}
		}
		goto IL_01d8;
		IL_01d8:
		return result;
	}

	public static string McSkinGetAddress(string UUID, string Type)
	{
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Expected O, but got Unknown
		if (Operators.CompareString(UUID, "", TextCompare: false) == 0)
		{
			throw new Exception("UUID 为空。");
		}
		if (StringExtensions.StartsWithF(UUID, "00000", false) && Operators.CompareString(Type, "Auth", TextCompare: false) != 0)
		{
			throw new Exception("离线 UUID 无正版皮肤文件：" + UUID);
		}
		string text = ModBase.ReadIni(ModBase.PathTemp + "Cache\\Skin\\Index" + Type + ".ini", UUID);
		if (Operators.CompareString(text, "", TextCompare: false) != 0)
		{
			return text;
		}
		string text2;
		switch (Type)
		{
		case "Mojang":
		case "Ms":
			text2 = "https://sessionserver.mojang.com/session/minecraft/profile/";
			break;
		case "Nide":
			text2 = "https://auth.mc-user.com:233/" + (((object)McInstanceSelected == null) ? Settings.Get<string>("CacheNideServer") : Settings.Get<string>("VersionServerNide", McInstanceSelected)) + "/sessionserver/session/minecraft/profile/";
			break;
		case "Auth":
			text2 = (((object)McInstanceSelected == null) ? Settings.Get<string>("CacheAuthServerServer") : Settings.Get<string>("VersionServerAuthServer", McInstanceSelected)) + "/sessionserver/session/minecraft/profile/";
			break;
		default:
			throw new ArgumentException("皮肤地址种类无效：" + (Type ?? "null"));
		}
		string text3 = ModNet.NetRequestByClientRetry(text2 + UUID, null, null, null, null, "*/*", null, RequireJson: true);
		if (Operators.CompareString(text3, "", TextCompare: false) == 0)
		{
			throw new Exception("皮肤返回值为空，可能是未设置自定义皮肤的用户");
		}
		string s;
		try
		{
			foreach (JToken item in (IEnumerable<JToken>)StringExtensions.DeserializeJson(text3)[(object)"properties"])
			{
				if (!((string)item[(object)"name"] == "textures"))
				{
					continue;
				}
				s = (string)item[(object)"value"];
				goto end_IL_0178;
			}
			throw new Exception("未从皮肤返回值中找到符合条件的 Property");
			end_IL_0178:;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, $"无法完成解析的皮肤返回值，可能是未设置自定义皮肤的用户：{text3}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			throw new Exception("皮肤返回值中不包含皮肤数据项，可能是未设置自定义皮肤的用户", ex2);
		}
		text3 = Encoding.UTF8.GetString(Convert.FromBase64String(s));
		JObject val = (JObject)StringExtensions.DeserializeJson(StringExtensions.Lower(text3));
		if (val["textures"] == null || val["textures"][(object)"skin"] == null || val["textures"][(object)"skin"][(object)"url"] == null)
		{
			throw new Exception("用户未设置自定义皮肤");
		}
		string text4 = val["textures"][(object)"skin"][(object)"url"].ToString();
		s = (text4.Contains("minecraft.net/") ? text4.Replace("http://", "https://") : text4);
		ModBase.WriteIni(ModBase.PathTemp + "Cache\\Skin\\Index" + Type + ".ini", UUID, s);
		Logger.Info($"UUID {UUID} 对应的皮肤文件为 {s}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
		return s;
	}

	public static string McSkinDownload(string Address)
	{
		PathUtils.GetLastPart(Address);
		string text = ModBase.PathTemp + "Cache\\Skin\\" + Conversions.ToString(StringExtensions.GetStableHashCode(Address)) + ".png";
		object mcSkinDownloadLock = McSkinDownloadLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(mcSkinDownloadLock);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(mcSkinDownloadLock, ref lockTaken);
			if (!FileUtils.Exists(text))
			{
				ModNet.NetDownloadByClient(Address, text + ".PCLDownloading");
				FileUtils.Move(text + ".PCLDownloading", text, false);
				Logger.Info($"皮肤下载成功：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			}
			return text;
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(mcSkinDownloadLock);
			}
		}
	}

	public static string McSkinSex(string Uuid)
	{
		if (Uuid.Length != 32)
		{
			return "Steve";
		}
		int num = int.Parse(Conversions.ToString(Uuid[7]), NumberStyles.AllowHexSpecifier);
		int num2 = int.Parse(Conversions.ToString(Uuid[15]), NumberStyles.AllowHexSpecifier);
		int num3 = int.Parse(Conversions.ToString(Uuid[23]), NumberStyles.AllowHexSpecifier);
		int num4 = int.Parse(Conversions.ToString(Uuid[31]), NumberStyles.AllowHexSpecifier);
		return ((num ^ num2 ^ num3 ^ num4) % 2 != 0) ? "Alex" : "Steve";
	}

	public static bool McJsonRuleCheck(JToken RuleToken)
	{
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		if (RuleToken == null)
		{
			return true;
		}
		bool result = false;
		foreach (JToken item in (IEnumerable<JToken>)RuleToken)
		{
			bool flag = true;
			if (item[(object)"os"] != null)
			{
				if (item[(object)"os"][(object)"name"] != null)
				{
					string left = item[(object)"os"][(object)"name"].ToString();
					if (Operators.CompareString(left, "unknown", TextCompare: false) != 0)
					{
						if (Operators.CompareString(left, "windows", TextCompare: false) == 0)
						{
							if (item[(object)"os"][(object)"version"] != null)
							{
								flag = flag && StringExtensions.RegexCheck(OSVersion, item[(object)"os"][(object)"version"].ToString(), RegexOptions.None);
							}
						}
						else
						{
							flag = false;
						}
					}
				}
				if (item[(object)"os"][(object)"arch"] != null)
				{
					flag = flag && Operators.CompareString(item[(object)"os"][(object)"arch"].ToString(), "x86", TextCompare: false) != 0 && !((IEnumerable<JToken>)item[(object)"os"][(object)"arch"]).Contains(JToken.op_Implicit("arm"));
				}
			}
			if (!Information.IsNothing(item[(object)"features"]))
			{
				flag = flag && Information.IsNothing(item[(object)"features"][(object)"is_demo_user"]);
				if (((IEnumerable<JToken>)(object)((JContainer)(JObject)item[(object)"features"]).Children()).Any([SpecialName] (JToken a0) => ((VB_0024AnonymousDelegate_22<JProperty, bool>)([SpecialName] (JProperty j) => j.Name.Contains("quick_play")))((JProperty)a0)))
				{
					flag = false;
				}
			}
			if (Operators.CompareString(item[(object)"action"].ToString(), "allow", TextCompare: false) == 0)
			{
				if (flag)
				{
					result = true;
				}
			}
			else if (flag)
			{
				result = false;
			}
		}
		return result;
	}

	public static List<McLibEntry> McLibListGet(McInstance Instance, bool IncludeMainJar)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		Logger.Info($"获取支持库列表：{Instance.Name}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
		List<McLibEntry> list = McLibListGetWithJson(Instance.JsonObject);
		if (IncludeMainJar)
		{
			JToken obj = Instance.JsonObject["jar"];
			string text = ((obj != null) ? obj.ToString() : null);
			McInstance mcInstance2;
			if (Instance.IsHmclFormatJson || text == null)
			{
				McInstance mcInstance = Instance;
				if ((!EnumUtils.HasFlagF<ModLoaders>(Instance.Version.ModLoaders, (ModLoaders)1) && !EnumUtils.HasFlagF<ModLoaders>(Instance.Version.ModLoaders, (ModLoaders)16)) || Instance.Version.Vanilla.Major < 17)
				{
					while (Operators.CompareString(mcInstance.InheritName, "", TextCompare: false) != 0 && Operators.CompareString(mcInstance.InheritName, mcInstance.Name, TextCompare: false) != 0)
					{
						mcInstance = new McInstance(McFolderSelected + "versions\\" + mcInstance.InheritName + "\\");
					}
				}
				mcInstance2 = new McInstance(mcInstance.PathVersion);
			}
			else
			{
				mcInstance2 = new McInstance(text);
			}
			if (!FileUtils.Exists(mcInstance2.PathVersion + mcInstance2.Name + ".json"))
			{
				mcInstance2 = Instance;
				Logger.Warn($"可能缺少前置版本 {mcInstance2.Name}，找不到对应的 json 文件", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			}
			string url;
			string sHA;
			if (mcInstance2.JsonObject["downloads"] != null && mcInstance2.JsonObject["downloads"][(object)"client"] != null)
			{
				url = (string)mcInstance2.JsonObject["downloads"][(object)"client"][(object)"url"];
				sHA = (string)mcInstance2.JsonObject["downloads"][(object)"client"][(object)"sha1"];
			}
			else
			{
				url = null;
				sHA = null;
			}
			list.Add(new McLibEntry
			{
				LocalPath = mcInstance2.PathVersion + mcInstance2.Name + ".jar",
				Size = 0L,
				IsNatives = false,
				Url = url,
				SHA1 = sHA
			});
		}
		return list;
	}

	public static List<McLibEntry> McLibListGetWithJson(JObject JsonObject, bool KeepSameNameDifferentVersionResult = false, string CustomMcFolder = null)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Invalid comparison between Unknown and I4
		CustomMcFolder = CustomMcFolder ?? McFolderSelected;
		List<McLibEntry> list = new List<McLibEntry>();
		checked
		{
			foreach (JObject item in ((JContainer)(JArray)JsonObject["libraries"]).Children())
			{
				JObject val = item;
				for (int i = val.Properties().Count() - 1; i >= 0; i += -1)
				{
					if (unchecked((int)val.Properties().ElementAtOrDefault(i).Value.Type) == 10)
					{
						val.Remove(val.Properties().ElementAtOrDefault(i).Name);
					}
				}
				if (!McJsonRuleCheck(val["rules"]))
				{
					continue;
				}
				string text = (string)val["url"];
				if (text != null)
				{
					text += McLibGet((string)val["name"], WithHead: false, IgnoreLiteLoader: true, CustomMcFolder).Replace("\\", "/");
				}
				if (val["natives"] == null)
				{
					string localPath = McLibGet((string)val["name"], WithHead: true, IgnoreLiteLoader: false, CustomMcFolder);
					try
					{
						if (val["downloads"] != null && val["downloads"][(object)"artifact"] != null)
						{
							McLibEntry mcLibEntry = new McLibEntry
							{
								OriginalName = (string)val["name"],
								Url = (string)(val["downloads"][(object)"artifact"][(object)"url"] ?? JToken.op_Implicit(text)),
								LocalPath = ((val["downloads"][(object)"artifact"][(object)"path"] == null) ? McLibGet((string)val["name"], WithHead: true, IgnoreLiteLoader: false, CustomMcFolder) : (CustomMcFolder + "libraries\\" + val["downloads"][(object)"artifact"][(object)"path"].ToString().Replace("/", "\\"))),
								Size = (long)Math.Round(ModBase.Val(val["downloads"][(object)"artifact"][(object)"size"].ToString())),
								IsNatives = false
							};
							JToken obj = val["downloads"][(object)"artifact"][(object)"sha1"];
							mcLibEntry.SHA1 = ((obj != null) ? obj.ToString() : null);
							list.Add(mcLibEntry);
						}
						else
						{
							list.Add(new McLibEntry
							{
								OriginalName = (string)val["name"],
								Url = text,
								LocalPath = localPath,
								Size = 0L,
								IsNatives = false,
								SHA1 = null
							});
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, string.Format("处理实际支持库列表失败（无 Natives，{0}）", val["name"] ?? JToken.op_Implicit("Nothing")), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						list.Add(new McLibEntry
						{
							OriginalName = (string)val["name"],
							Url = text,
							LocalPath = localPath,
							Size = 0L,
							IsNatives = false,
							SHA1 = null
						});
						ProjectData.ClearProjectError();
					}
				}
				else
				{
					if (val["natives"][(object)"windows"] == null)
					{
						continue;
					}
					string text2 = (string)val["natives"][(object)"windows"];
					try
					{
						if (val["downloads"] != null && val["downloads"][(object)"classifiers"] != null && val["downloads"][(object)"classifiers"][(object)text2] != null)
						{
							JToken val2 = val["downloads"][(object)"classifiers"][(object)text2];
							list.Add(new McLibEntry
							{
								OriginalName = (string)val["name"],
								Url = (string)(val2[(object)"url"] ?? JToken.op_Implicit(text)),
								LocalPath = ((val2[(object)"path"] == null) ? McLibGet((string)val["name"], WithHead: true, IgnoreLiteLoader: false, CustomMcFolder).Replace(".jar", "-" + text2 + ".jar").Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32") : string.Format("{0}libraries\\{1}", CustomMcFolder, val2[(object)"path"].ToString().Replace("/", "\\"))),
								Size = (long)Math.Round(ModBase.Val(val2[(object)"size"].ToString())),
								IsNatives = true,
								SHA1 = val2[(object)"sha1"].ToString()
							});
						}
						else
						{
							list.Add(new McLibEntry
							{
								OriginalName = (string)val["name"],
								Url = text,
								Size = 0L,
								IsNatives = true,
								SHA1 = null,
								LocalPath = McLibGet((string)val["name"], WithHead: true, IgnoreLiteLoader: false, CustomMcFolder).Replace(".jar", $"-{text2}.jar").Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32")
							});
						}
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						Logger.Warn(ex4, string.Format("处理实际支持库列表失败（有 Natives，{0}）", val["name"] ?? JToken.op_Implicit("Nothing")), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						list.Add(new McLibEntry
						{
							OriginalName = (string)val["name"],
							Url = text,
							Size = 0L,
							IsNatives = true,
							SHA1 = null,
							LocalPath = McLibGet((string)val["name"], WithHead: true, IgnoreLiteLoader: false, CustomMcFolder).Replace(".jar", $"-{text2}.jar").Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32")
						});
						ProjectData.ClearProjectError();
					}
				}
			}
			Dictionary<string, McLibEntry> dictionary = new Dictionary<string, McLibEntry>();
			VB_0024AnonymousDelegate_23<McLibEntry, string> vB_0024AnonymousDelegate_ = [SpecialName] (McLibEntry Token) => PathUtils.GetLastPart(PathUtils.RemoveLastPart(Token.LocalPath));
			int num = list.Count - 1;
			for (int j = 0; j <= num; j++)
			{
				string text3 = list[j].Name + list[j].IsNatives;
				if (dictionary.ContainsKey(text3))
				{
					string text4 = vB_0024AnonymousDelegate_(list[j]);
					string text5 = vB_0024AnonymousDelegate_(dictionary[text3]);
					if (Operators.CompareString(text4, text5, TextCompare: false) != 0 && KeepSameNameDifferentVersionResult)
					{
						Logger.Info($"发现疑似重复的支持库：{list[j]} ({text4}) 与 {dictionary[text3]} ({text5})", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
						dictionary.Add(text3 + Conversions.ToString(ModBase.GetUuid()), list[j]);
						continue;
					}
					Logger.Info($"发现重复的支持库：{list[j]} ({text4}) 与 {dictionary[text3]} ({text5})，已忽略其中之一", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					if (McUtils.CompareVersionGE(text4, text5))
					{
						dictionary[text3] = list[j];
					}
				}
				else
				{
					dictionary.Add(text3, list[j]);
				}
			}
			return dictionary.Values.ToList();
		}
	}

	public static List<ModNet.NetFile> McLibNetFilesFromInstance(McInstance Instance)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Expected O, but got Unknown
		if (!Instance.IsLoaded)
		{
			Instance.Load();
		}
		List<ModNet.NetFile> list = new List<ModNet.NetFile>();
		try
		{
			ModNet.NetFile netFile = ModDownload.DlClientJarGet(Instance, ReturnNothingOnFileUseable: true);
			if (netFile != null)
			{
				list.Add(netFile);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "版本缺失主 jar 文件所必须的信息", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			ProjectData.ClearProjectError();
		}
		list.AddRange(McLibNetFilesFromTokens(McLibListGet(Instance, IncludeMainJar: false)));
		if (Settings.Get<int>("VersionServerLogin", Instance) == 3)
		{
			string text = ModBase.PathPure.Value + "nide8auth.jar";
			try
			{
				Logger.Info("开始获取统一通行证下载信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				JObject val = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://auth.mc-user.com:233/" + Settings.Get<string>("VersionServerNide", Instance), null, null, null, null, "*/*", null, RequireJson: true));
				list.Add(new ModNet.NetFile(new string[1] { "https://login.mc-user.com:233/index/jar" }, text, new FileChecker
				{
					Hash = val["jarHash"].ToString()
				}));
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				if (!FileUtils.Exists(text))
				{
					throw new Exception("获取统一通行证下载信息失败", ex4);
				}
				Logger.Warn(ex4, "获取统一通行证下载信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
		}
		if (Settings.Get<int>("VersionServerLogin", Instance) == 4)
		{
			string text2 = ModBase.PathPure.Value + "authlib-injector.jar";
			try
			{
				Logger.Info("开始获取 Authlib-Injector 下载信息", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				JObject val2 = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://authlib-injector.yushi.moe/artifact/latest.json", null, "https://bmclapi2.bangbang93.com/mirrors/authlib-injector/artifact/latest.json", null, null, "*/*", null, RequireJson: true));
				string text3 = val2["download_url"].ToString().Replace("bmclapi2.bangbang93.com/mirrors/authlib-injector", "authlib-injector.yushi.moe");
				list.Add(new ModNet.NetFile(new string[2]
				{
					text3,
					text3.Replace("authlib-injector.yushi.moe", "bmclapi2.bangbang93.com/mirrors/authlib-injector")
				}, text2, new FileChecker
				{
					Hash = val2["checksums"][(object)"sha256"].ToString()
				}));
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				if (!FileUtils.Exists(text2))
				{
					throw new Exception("获取 Authlib-Injector 下载信息失败", ex6);
				}
				Logger.Warn(ex6, "获取 Authlib-Injector 下载信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
		}
		if (Conversions.ToBoolean(ShouldIgnoreFileCheck(Instance)))
		{
			Logger.Warn("设置要求尽量忽略文件检查，这可能会保留有误的文件", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			list = list.Where([SpecialName] (ModNet.NetFile f) =>
			{
				if (FileUtils.Exists(f.LocalPath))
				{
					Logger.Warn($"跳过下载的支持库文件：{f.LocalPath}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
					return false;
				}
				return true;
			}).ToList();
		}
		return list;
	}

	public static List<ModNet.NetFile> McLibNetFilesFromTokens(List<McLibEntry> Libs, string CustomMcFolder = null)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		CustomMcFolder = CustomMcFolder ?? McFolderSelected;
		List<ModNet.NetFile> list = new List<ModNet.NetFile>();
		foreach (McLibEntry Lib in Libs)
		{
			FileChecker checker = ((!StringExtensions.ContainsIgnoreCase(Lib.Name, "labymod")) ? new FileChecker
			{
				ActualSize = ((Lib.Size == 0L) ? (-1) : Lib.Size),
				Hash = Lib.SHA1
			} : new FileChecker());
			List<string> list2 = new List<string>();
			if (Lib.Url == null && Operators.CompareString(Lib.Name, "net.minecraftforge:forge:universal", TextCompare: false) == 0)
			{
				Lib.Url = "https://maven.minecraftforge.net" + Lib.LocalPath.Replace(CustomMcFolder + "libraries", "").Replace("\\", "/");
			}
			if (Lib.Url != null)
			{
				list2.Add(Lib.Url);
				if (Lib.Url.Contains("launcher.mojang.com/v1/objects") || Lib.Url.Contains("client.txt") || Lib.Url.Contains(".tsrg"))
				{
					list2.AddRange(ModDownload.DlSourceLauncherOrMetaGet(Lib.Url));
				}
				if (Lib.Url.Contains("maven"))
				{
					string item = Lib.Url.Replace(Lib.Url.Substring(0, StringExtensions.IndexOfF(Lib.Url, "maven", false)), "https://bmclapi2.bangbang93.com/").Replace("maven.fabricmc.net", "maven").Replace("maven.minecraftforge.net", "maven")
						.Replace("maven.neoforged.net/releases", "maven");
					if (ModDownload.DlSourcePreferMojang)
					{
						list2.Add(item);
					}
					else
					{
						list2.Insert(0, item);
					}
				}
			}
			if (Lib.LocalPath.Contains("transformer-discovery-service"))
			{
				if (!FileUtils.Exists(Lib.LocalPath))
				{
					ModBase.ExtractResources(Lib.LocalPath, "Transformer");
				}
				Logger.Warn("已自动释放 Transformer Discovery Service", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				continue;
			}
			if (Lib.LocalPath.Contains("optifine\\OptiFine"))
			{
				string text = StringExtensions.Split(Lib.LocalPath.Replace(CustomMcFolder + "libraries\\optifine\\OptiFine\\", ""), "_", false)[0] + "/" + PathUtils.GetLastPart(Lib.LocalPath).Replace("-", "_");
				text = "/maven/com/optifine/" + text;
				if (text.Contains("_pre"))
				{
					text = text.Replace("com/optifine/", "com/optifine/preview_");
				}
				list2.Add("https://bmclapi2.bangbang93.com" + text);
			}
			else if (list2.Count <= 2)
			{
				list2.AddRange(ModDownload.DlSourceLibraryGet("https://libraries.minecraft.net" + Lib.LocalPath.Replace(CustomMcFolder + "libraries", "").Replace("\\", "/")));
			}
			list.Add(new ModNet.NetFile(list2.Distinct(), Lib.LocalPath, checker));
		}
		return EnumerableExtensions.DistinctBy<ModNet.NetFile, string>((IEnumerable<ModNet.NetFile>)list, (Func<ModNet.NetFile, string>)([SpecialName] (ModNet.NetFile a) => a.LocalPath)).ToList();
	}

	public static string McLibGet(string Original, bool WithHead = true, bool IgnoreLiteLoader = false, string CustomMcFolder = null)
	{
		CustomMcFolder = CustomMcFolder ?? McFolderSelected;
		string[] array = StringExtensions.Split(Original, ":", false);
		return (WithHead ? (CustomMcFolder + "libraries\\") : "") + string.Format("{0}\\{1}\\{2}\\{3}.jar", array[0].Replace(".", "\\"), array[1], array[2], EnumerableExtensions.Join((IEnumerable)array.Skip(1), "-"));
	}

	public static object ShouldIgnoreFileCheck(McInstance Instance)
	{
		return Settings.Get<bool>("VersionAdvanceAssetsV2", Instance) || Settings.Get<int>("VersionAdvanceAssets", Instance) == 2;
	}

	public static JToken McAssetsGetIndex(McInstance Instance, bool ReturnLegacyOnError = false, bool CheckURLEmpty = false)
	{
		try
		{
			while (true)
			{
				JToken val = Instance.JsonObject["assetIndex"];
				if (val != null && val[(object)"id"] != null)
				{
					return val;
				}
				if (Instance.JsonObject["assets"] != null)
				{
					Instance.JsonObject["assets"].ToString();
				}
				if (CheckURLEmpty && val[(object)"url"] != null)
				{
					return val;
				}
				if (Operators.CompareString(Instance.InheritName, "", TextCompare: false) != 0)
				{
					Instance = new McInstance(McFolderSelected + "versions\\" + Instance.InheritName);
					continue;
				}
				break;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		if (ReturnLegacyOnError)
		{
			Logger.Info("无法获取资源文件索引下载地址，使用默认的 legacy 下载地址", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			return StringExtensions.DeserializeJson("{\r\n                \"id\": \"legacy\",\r\n                \"sha1\": \"c0fd82e8ce9fbc93119e40d96d5a4e62cfa3f729\",\r\n                \"size\": 134284,\r\n                \"url\": \"https://launchermeta.mojang.com/mc-staging/assets/legacy/c0fd82e8ce9fbc93119e40d96d5a4e62cfa3f729/legacy.json\",\r\n                \"totalSize\": 111220701\r\n            }");
		}
		throw new Exception("该版本不存在资源文件索引信息");
	}

	public static string McAssetsGetIndexName(McInstance Instance)
	{
		try
		{
			while (true)
			{
				if (Instance.JsonObject["assetIndex"] != null && Instance.JsonObject["assetIndex"][(object)"id"] != null)
				{
					return Instance.JsonObject["assetIndex"][(object)"id"].ToString();
				}
				if (Instance.JsonObject["assets"] != null)
				{
					return Instance.JsonObject["assets"].ToString();
				}
				if (Operators.CompareString(Instance.InheritName, "", TextCompare: false) != 0)
				{
					Instance = new McInstance(McFolderSelected + "versions\\" + Instance.InheritName);
					continue;
				}
				break;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "获取资源文件索引名失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			ProjectData.ClearProjectError();
		}
		return "legacy";
	}

	private static List<McAssetsToken> McAssetsListGet(McInstance Instance)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		string text = McAssetsGetIndexName(Instance);
		try
		{
			if (!FileUtils.Exists($"{McFolderSelected}assets\\indexes\\{text}.json"))
			{
				throw new FileNotFoundException("未找到 Asset Index", McFolderSelected + "assets\\indexes\\" + text + ".json");
			}
			List<McAssetsToken> list = new List<McAssetsToken>();
			JObject val = (JObject)FileUtils.ReadAsJson($"{McFolderSelected}assets\\indexes\\{text}.json", (Encoding)null, (Type)null);
			foreach (JProperty item in val["objects"].Children())
			{
				JProperty val2 = item;
				string text2 = ((val["map_to_resources"] != null && val["map_to_resources"].ToObject<bool>()) ? $"{Instance.PathIndie}resources\\{val2.Name}" : ((val["virtual"] == null || !val["virtual"].ToObject<bool>()) ? string.Format("{0}assets\\objects\\{1}\\{2}", McFolderSelected, Strings.Left(val2.Value[(object)"hash"].ToString(), 2), val2.Value[(object)"hash"]) : $"{McFolderSelected}assets\\virtual\\legacy\\{val2.Name}"));
				list.Add(new McAssetsToken
				{
					LocalPath = text2.Replace("/", "\\"),
					SourcePath = val2.Name,
					Hash = val2.Value[(object)"hash"].ToString(),
					Size = Conversions.ToLong(val2.Value[(object)"size"].ToString())
				});
			}
			return list;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, $"获取资源文件列表失败：{text}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			throw;
		}
	}

	public static List<ModNet.NetFile> McAssetsFixList(McInstance Instance, bool CheckHash, [Optional][DefaultParameterValue(null)] ref ModLoader.LoaderBase ProgressFeed)
	{
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		if (CheckHash)
		{
			return (from Token in McAssetsListGet(Instance)
				select new ModNet.NetFile(ModDownload.DlSourceAssetsGet($"https://resources.download.minecraft.net/{Strings.Left(Token.Hash, 2)}/{Token.Hash}"), Token.LocalPath, new FileChecker
				{
					ActualSize = ((Token.Size == 0L) ? (-1) : Token.Size),
					Hash = Token.Hash
				})).ToList();
		}
		List<ModNet.NetFile> list = new List<ModNet.NetFile>();
		checked
		{
			try
			{
				List<McAssetsToken> list2 = McAssetsListGet(Instance);
				if (ProgressFeed != null)
				{
					ProgressFeed.Progress = 0.04;
				}
				int num = list2.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					McAssetsToken mcAssetsToken = list2[i];
					if (ProgressFeed != null)
					{
						ProgressFeed.Progress = 0.05 + 0.94 * (double)i / (double)list2.Count;
					}
					FileInfo info = FileUtils.GetInfo(mcAssetsToken.LocalPath);
					if (!info.Exists || (mcAssetsToken.Size != 0L && mcAssetsToken.Size != info.Length))
					{
						list.Add(new ModNet.NetFile(ModDownload.DlSourceAssetsGet($"https://resources.download.minecraft.net/{Strings.Left(mcAssetsToken.Hash, 2)}/{mcAssetsToken.Hash}"), mcAssetsToken.LocalPath, new FileChecker
						{
							ActualSize = ((mcAssetsToken.Size == 0L) ? (-1) : mcAssetsToken.Size),
							Hash = mcAssetsToken.Hash
						}));
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "获取版本缺失的资源文件下载列表失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
				ProjectData.ClearProjectError();
			}
			if (ProgressFeed != null)
			{
				ProgressFeed.Progress = 0.99;
			}
			return list;
		}
	}

	public static void McDownloadClientUpdateHint(string VersionName, JObject Json)
	{
		_Closure_0024__48_002D1 arg = default(_Closure_0024__48_002D1);
		_Closure_0024__48_002D1 CS_0024_003C_003E8__locals0 = new _Closure_0024__48_002D1(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionName = VersionName;
		try
		{
			_Closure_0024__48_002D0 arg2 = default(_Closure_0024__48_002D0);
			_Closure_0024__48_002D0 CS_0024_003C_003E8__locals1 = new _Closure_0024__48_002D0(arg2);
			CS_0024_003C_003E8__locals1._0024VB_0024Local_Version = null;
			foreach (JToken item in (IEnumerable<JToken>)Json["versions"])
			{
				if (item[(object)"id"] != null && Operators.CompareString(item[(object)"id"].ToString(), CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionName, TextCompare: false) == 0)
				{
					CS_0024_003C_003E8__locals1._0024VB_0024Local_Version = item;
					break;
				}
			}
			if (CS_0024_003C_003E8__locals1._0024VB_0024Local_Version == null)
			{
				return;
			}
			DateTime dateTime = (DateTime)CS_0024_003C_003E8__locals1._0024VB_0024Local_Version[(object)"releaseTime"];
			if (ModMain.MyMsgBox(string.Format("新版本：{0}{1}", CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionName, "\r\n") + (((DateTime.Now - dateTime).TotalDays > 1.0) ? ("更新时间：" + dateTime.ToString()) : ("更新于：" + StringUtils.FormatTimeSpan(dateTime - DateTime.Now, false))), "Minecraft 更新提示", "确定", "下载", ((DateTime.Now - dateTime).TotalHours > 3.0) ? "更新日志" : "", IsWarn: false, HighLight: true, ForceWait: false, null, null, [SpecialName] () =>
			{
				ModDownloadLib.McUpdateLogShow(CS_0024_003C_003E8__locals1._0024VB_0024Local_Version);
			}) == 2)
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					PageDownloadInstall.VersionWaitingSelect = CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionName;
					ModMain.FrmMain.PageChange(FormMain.PageType.Download);
				});
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, string.Format("Minecraft 更新提示发送失败（{0}）", CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionName ?? "Nothing"), (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Minecraft\\ModMinecraft.vb");
			ProjectData.ClearProjectError();
		}
	}

	public static string FilterAccessToken(string Raw, char FilterChar)
	{
		if (Raw.Contains("accessToken "))
		{
			foreach (string item in StringExtensions.RegexSearch(Raw, "(?<=accessToken ([^ ]{5}))[^ ]+(?=[^ ]{5})", RegexOptions.None))
			{
				Raw = Raw.Replace(item, new string(FilterChar, item.Count()));
			}
		}
		string accessToken = ModLaunch.McLoginLoader.Output.AccessToken;
		if (accessToken != null && accessToken.Length >= 10 && StringExtensions.ContainsIgnoreCase(Raw, accessToken) && Operators.CompareString(ModLaunch.McLoginLoader.Output.Uuid, ModLaunch.McLoginLoader.Output.AccessToken, TextCompare: false) != 0)
		{
			Raw = Raw.Replace(accessToken, Strings.Left(accessToken, 5) + new string(FilterChar, checked(accessToken.Length - 10)) + Strings.Right(accessToken, 5));
		}
		return Raw;
	}

	public static string FilterUserName(string Raw, char FilterChar)
	{
		if (Raw.Contains(":\\Users\\"))
		{
			Raw = Regex.Replace(Raw, "(?<=:\\\\Users\\\\)[^\\\\]+", [SpecialName] (Match Token) => new string(FilterChar, Token.Length));
		}
		if (Raw.Contains(":/Users/"))
		{
			Raw = Regex.Replace(Raw, "(?<=:/Users/)[^/]+", [SpecialName] (Match Token) => new string(FilterChar, Token.Length));
		}
		return Raw;
	}
}
