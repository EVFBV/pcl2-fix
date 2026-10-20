using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using MeloongCore;
using MeloongCore.Extensions;
using MeloongCore.Wpf;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCLCS;

namespace PCL;

[StandardModule]
public sealed class ModDownloadLib
{
	public class McInstallRequest
	{
		public string NewInstanceName;

		public string InstanceFolder;

		public McComponents Components;

		public string MinecraftJson;

		public ModDownload.DlForgeVersionEntry ForgeEntry;

		public ModDownload.DlNeoForgeListEntry NeoForgeEntry;

		public ResourceVersion FabricApi;

		public ResourceVersion OptiFabric;

		public ModDownload.DlLiteLoaderListEntry LiteLoaderEntry;

		public bool IsIntermediateOutput;

		public McInstallRequest()
		{
			MinecraftJson = null;
			ForgeEntry = null;
			NeoForgeEntry = null;
			FabricApi = null;
			OptiFabric = null;
			LiteLoaderEntry = null;
			IsIntermediateOutput = false;
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class _Closure_0024__
	{
		public static readonly _Closure_0024__ _0024I;

		public static MyIconButton.ClickEventHandler _0024IR5_002D1;

		public static MyIconButton.ClickEventHandler _0024IR5_002D2;

		public static MyIconButton.ClickEventHandler _0024IR6_002D3;

		public static MyIconButton.ClickEventHandler _0024IR6_002D4;

		public static MyIconButton.ClickEventHandler _0024IR6_002D5;

		public static Func<ModLoader.LoaderBase, bool> _0024I13_002D2;

		public static Func<ModLoader.LoaderBase, bool> _0024I13_002D3;

		public static Func<ModLoader.LoaderBase, bool> _0024I13_002D4;

		public static MyIconButton.ClickEventHandler _0024IR16_002D8;

		public static MyIconButton.ClickEventHandler _0024IR17_002D9;

		public static MyIconButton.ClickEventHandler _0024IR17_002D10;

		public static MyIconButton.ClickEventHandler _0024IR23_002D11;

		public static MyIconButton.ClickEventHandler _0024IR24_002D12;

		public static MyIconButton.ClickEventHandler _0024IR24_002D13;

		public static Func<string, bool> _0024I28_002D2;

		public static Func<ModLoader.LoaderBase, bool> _0024I30_002D4;

		public static Func<ModLoader.LoaderBase, bool> _0024I30_002D5;

		public static Func<ModLoader.LoaderBase, bool> _0024I30_002D6;

		public static Func<string, DirectoryInfo> _0024I30_002D9;

		public static Func<DirectoryInfo, bool> _0024I30_002D10;

		public static Func<DirectoryInfo, string> _0024I30_002D11;

		public static Func<string, bool> _0024I32_002D1;

		public static MyIconButton.ClickEventHandler _0024IR33_002D16;

		public static MyIconButton.ClickEventHandler _0024IR33_002D17;

		public static MyIconButton.ClickEventHandler _0024IR34_002D18;

		public static Action _0024I37_002D0;

		public static MyIconButton.ClickEventHandler _0024IR42_002D19;

		public static MyIconButton.ClickEventHandler _0024IR42_002D20;

		public static MyIconButton.ClickEventHandler _0024IR43_002D21;

		public static Func<(string Name, string Folder), string> _0024I53_002D0;

		public static Func<(string Name, JObject Json), string> _0024I53_002D1;

		public static Func<string, string> _0024I53_002D2;

		public static Action _0024I55_002D6;

		public static Action _0024I55_002D5;

		public static Func<ManagedFile, string> _0024I56_002D0;

		static _Closure_0024__()
		{
			_0024I = new _Closure_0024__();
		}

		[SpecialName]
		internal void _Lambda_0024__R5_002D1(object a0, EventArgs a1)
		{
			McDownloadMenuLog(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R5_002D2(object a0, EventArgs a1)
		{
			McDownloadMenuSaveServer(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R6_002D3(object a0, EventArgs a1)
		{
			McDownloadMenuSave(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R6_002D4(object a0, EventArgs a1)
		{
			McDownloadMenuLog(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R6_002D5(object a0, EventArgs a1)
		{
			McDownloadMenuSaveServer(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal bool _Lambda_0024__13_002D2(ModLoader.LoaderBase l)
		{
			if (Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) != 0)
			{
				return Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0;
			}
			return true;
		}

		[SpecialName]
		internal bool _Lambda_0024__13_002D3(ModLoader.LoaderBase l)
		{
			return l.State != ModBase.LoadState.Finished;
		}

		[SpecialName]
		internal bool _Lambda_0024__13_002D4(ModLoader.LoaderBase l)
		{
			return l.State != ModBase.LoadState.Finished;
		}

		[SpecialName]
		internal void _Lambda_0024__R16_002D8(object a0, EventArgs a1)
		{
			OptiFineLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R17_002D9(object a0, EventArgs a1)
		{
			OptiFineSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R17_002D10(object a0, EventArgs a1)
		{
			OptiFineLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R23_002D11(object a0, EventArgs a1)
		{
			LiteLoaderAll_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R24_002D12(object a0, EventArgs a1)
		{
			LiteLoaderSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R24_002D13(object a0, EventArgs a1)
		{
			LiteLoaderAll_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal bool _Lambda_0024__28_002D2(string l)
		{
			return Operators.CompareString(l, "true", TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__30_002D4(ModLoader.LoaderBase l)
		{
			if (Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) != 0)
			{
				return Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0;
			}
			return true;
		}

		[SpecialName]
		internal bool _Lambda_0024__30_002D5(ModLoader.LoaderBase l)
		{
			return l.State != ModBase.LoadState.Finished;
		}

		[SpecialName]
		internal bool _Lambda_0024__30_002D6(ModLoader.LoaderBase l)
		{
			return l.State != ModBase.LoadState.Finished;
		}

		[SpecialName]
		internal DirectoryInfo _Lambda_0024__30_002D9(string f)
		{
			return DirectoryUtils.GetInfo(f);
		}

		[SpecialName]
		internal bool _Lambda_0024__30_002D10(DirectoryInfo l)
		{
			if (StringExtensions.ContainsIgnoreCase(l.Name, "forge"))
			{
				return l.EnumerateFiles().Any();
			}
			return false;
		}

		[SpecialName]
		internal string _Lambda_0024__30_002D11(DirectoryInfo d)
		{
			return d.Name;
		}

		[SpecialName]
		internal bool _Lambda_0024__32_002D1(string d)
		{
			return Operators.CompareString(d, "", TextCompare: false) != 0;
		}

		[SpecialName]
		internal void _Lambda_0024__R33_002D16(object a0, EventArgs a1)
		{
			ForgeSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R33_002D17(object a0, EventArgs a1)
		{
			ForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R34_002D18(object a0, EventArgs a1)
		{
			ForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__37_002D0()
		{
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Expected O, but got Unknown
			try
			{
				Logger.Info("刷新 Forge 推荐版本缓存开始", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				string text = ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/forge/promos", null, null, null, null, "*/*", null, RequireJson: true);
				if (text.Length < 1000)
				{
					throw new Exception("获取的结果过短（" + text + "）");
				}
				JContainer val = (JContainer)StringExtensions.DeserializeJson(text);
				List<string> list = new List<string>();
				foreach (JObject item in (IEnumerable<JToken>)val)
				{
					JObject val2 = item;
					if (val2["name"] != null && val2["build"] != null)
					{
						string text2 = (string)val2["name"];
						if (StringExtensions.EndsWithF(text2, "-recommended", false))
						{
							list.Add("\"" + text2.Replace("-recommended", "\":\"" + val2["build"][(object)"version"].ToString() + "\""));
						}
					}
				}
				if (list.Count < 5)
				{
					throw new Exception("获取的推荐版本数过少（" + text + "）");
				}
				string text3 = "{" + EnumerableExtensions.Join((IEnumerable)list, ",") + "}";
				FileUtils.Write(ModBase.PathTemp + "Cache\\ForgeRecommendedList.json", text3, (Encoding)null);
				Logger.Info("刷新 Forge 推荐版本缓存成功", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "刷新 Forge 推荐版本缓存失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R42_002D19(object a0, EventArgs a1)
		{
			NeoForgeSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R42_002D20(object a0, EventArgs a1)
		{
			NeoForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal void _Lambda_0024__R43_002D21(object a0, EventArgs a1)
		{
			NeoForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		}

		[SpecialName]
		internal string _Lambda_0024__53_002D0((string Name, string Folder) Source)
		{
			return $"{Source.Name}：{Source.Folder}";
		}

		[SpecialName]
		internal string _Lambda_0024__53_002D1((string Name, JObject Json) Profile)
		{
			JToken obj = Profile.Json["minecraftArguments"];
			return ((obj != null) ? obj.ToString() : null) ?? " ";
		}

		[SpecialName]
		internal string _Lambda_0024__53_002D2(string Value)
		{
			return Value.Trim();
		}

		[SpecialName]
		internal void _Lambda_0024__55_002D5()
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			});
		}

		[SpecialName]
		internal void _Lambda_0024__55_002D6()
		{
			ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
		}

		[SpecialName]
		internal string _Lambda_0024__56_002D0(ManagedFile File)
		{
			return File.RelativePath;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__12_002D0
	{
		public AutoResetEvent _0024VB_0024Local_outputWaitHandle;

		public _Closure_0024__12_002D1 _0024VB_0024NonLocal__0024VB_0024Closure_3;

		public _Closure_0024__12_002D0(_Closure_0024__12_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_outputWaitHandle = arg0._0024VB_0024Local_outputWaitHandle;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R6(object a0, DataReceivedEventArgs a1)
		{
			_Lambda_0024__0(RuntimeHelpers.GetObjectValue(a0), a1);
		}

		[SpecialName]
		internal object _Lambda_0024__0(object sender, DataReceivedEventArgs e)
		{
			try
			{
				if (e.Data == null)
				{
					_0024VB_0024Local_outputWaitHandle.Set();
				}
				else
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResult = e.Data;
					Logger.Trace(_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResult, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_TotalLength = checked(_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_TotalLength + 1);
					_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Task.Progress += 9.999999999999999E-05;
				}
			}
			catch (ObjectDisposedException ex)
			{
				ProjectData.SetProjectError(ex);
				ObjectDisposedException ex2 = ex;
				ProjectData.ClearProjectError();
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "读取 OptiFine 安装器信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
			try
			{
				if (_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Task.State == ModBase.LoadState.Canceled && !_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.HasExited)
				{
					Logger.Info("由于任务取消，已中止 OptiFine 安装", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.Kill();
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			return null;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__12_002D1
	{
		public string _0024VB_0024Local_LastResult;

		public int _0024VB_0024Local_TotalLength;

		public Process _0024VB_0024Local_process;

		public _Closure_0024__12_002D2 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__12_002D1(_Closure_0024__12_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_LastResult = arg0._0024VB_0024Local_LastResult;
				_0024VB_0024Local_TotalLength = arg0._0024VB_0024Local_TotalLength;
				_0024VB_0024Local_process = arg0._0024VB_0024Local_process;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__12_002D2
	{
		public ModLoader.LoaderTask<List<ModNet.NetFile>, bool> _0024VB_0024Local_Task;

		public _Closure_0024__12_002D2(_Closure_0024__12_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Task = arg0._0024VB_0024Local_Task;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__12_002D3
	{
		public AutoResetEvent _0024VB_0024Local_errorWaitHandle;

		public _Closure_0024__12_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_4;

		public _Closure_0024__12_002D3(_Closure_0024__12_002D3 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_errorWaitHandle = arg0._0024VB_0024Local_errorWaitHandle;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R7(object a0, DataReceivedEventArgs a1)
		{
			_Lambda_0024__1(RuntimeHelpers.GetObjectValue(a0), a1);
		}

		[SpecialName]
		internal object _Lambda_0024__1(object sender, DataReceivedEventArgs e)
		{
			try
			{
				if (e.Data == null)
				{
					_0024VB_0024Local_errorWaitHandle.Set();
				}
				else
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResult = e.Data;
					Logger.Trace(_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResult, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_TotalLength = checked(_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_TotalLength + 1);
					_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Task.Progress += 9.999999999999999E-05;
				}
			}
			catch (ObjectDisposedException ex)
			{
				ProjectData.SetProjectError(ex);
				ObjectDisposedException ex2 = ex;
				ProjectData.ClearProjectError();
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "读取 OptiFine 安装器错误信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
			try
			{
				if (_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Task.State == ModBase.LoadState.Canceled && !_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.HasExited)
				{
					Logger.Info("由于任务取消，已中止 OptiFine 安装", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.Kill();
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			return null;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__13_002D0
	{
		public ModDownload.DlOptiFineListEntry _0024VB_0024Local_DownloadInfo;

		public string _0024VB_0024Local_Target;

		public ModLoader.LoaderCombo<string> _0024VB_0024Local_ClientDownloadLoader;

		public string _0024VB_0024Local_ClientFolder;

		public string _0024VB_0024Local_McFolder;

		public string _0024VB_0024Local_VersionFolder;

		public string _0024VB_0024Local_Id;

		public _Closure_0024__13_002D0(_Closure_0024__13_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_DownloadInfo = arg0._0024VB_0024Local_DownloadInfo;
				_0024VB_0024Local_Target = arg0._0024VB_0024Local_Target;
				_0024VB_0024Local_ClientDownloadLoader = arg0._0024VB_0024Local_ClientDownloadLoader;
				_0024VB_0024Local_ClientFolder = arg0._0024VB_0024Local_ClientFolder;
				_0024VB_0024Local_McFolder = arg0._0024VB_0024Local_McFolder;
				_0024VB_0024Local_VersionFolder = arg0._0024VB_0024Local_VersionFolder;
				_0024VB_0024Local_Id = arg0._0024VB_0024Local_Id;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
		{
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Expected O, but got Unknown
			List<string> list = new List<string>();
			string text = _0024VB_0024Local_DownloadInfo.Inherit;
			if (Operators.CompareString(text, "1.8", TextCompare: false) == 0 || Operators.CompareString(text, "1.9", TextCompare: false) == 0)
			{
				text += ".0";
			}
			if (_0024VB_0024Local_DownloadInfo.IsPreview)
			{
				list.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U_" + _0024VB_0024Local_DownloadInfo.DisplayName.Replace(_0024VB_0024Local_DownloadInfo.Inherit + " ", "").Replace(" ", "/"));
			}
			else
			{
				list.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U/" + _0024VB_0024Local_DownloadInfo.DisplayName.Replace(_0024VB_0024Local_DownloadInfo.Inherit + " ", ""));
			}
			try
			{
				string text2 = ModNet.NetRequestByClientRetry("https://optifine.net/adloadx?f=" + _0024VB_0024Local_DownloadInfo.FileName, null, null, null, null, "text/html", null, RequireJson: false, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), SimulateBrowserHeaders: true);
				Task.Progress = 0.8;
				list.Add("https://optifine.net/" + StringExtensions.RegexSearch(text2, "downloadx\\?f=[^\"']+", RegexOptions.None).First());
				Logger.Info($"OptiFine {_0024VB_0024Local_DownloadInfo.DisplayName} 官方下载地址：{list.Last()}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"获取 OptiFine {_0024VB_0024Local_DownloadInfo.DisplayName} 官方下载地址失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
			Task.Output = new List<ModNet.NetFile>
			{
				new ModNet.NetFile(list.ToArray(), _0024VB_0024Local_Target, new FileChecker
				{
					MinSize = 307200L
				})
			};
		}

		[SpecialName]
		internal void _Lambda_0024__1(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
		{
			List<ModLoader.LoaderBase> source = (from l in _0024VB_0024Local_ClientDownloadLoader.GetLoaderList()
				where Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) == 0 || Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0
				where l.State != ModBase.LoadState.Finished
				select l).ToList();
			if (source.Any())
			{
				Logger.Info("OptiFine 安装正在等待原版文件下载完成", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			while (source.Any() && !Task.IsCanceled)
			{
				source = source.Where((_Closure_0024__._0024I13_002D4 == null) ? (_Closure_0024__._0024I13_002D4 = [SpecialName] (ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished) : _Closure_0024__._0024I13_002D4).ToList();
				Thread.Sleep(50);
			}
			if (Task.IsCanceled)
			{
				return;
			}
			object vanillaSyncLock = VanillaSyncLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(vanillaSyncLock);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(vanillaSyncLock, ref lockTaken);
				string lastPart = PathUtils.GetLastPart(_0024VB_0024Local_ClientFolder);
				DirectoryUtils.Create(_0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit);
				if (!FileUtils.Exists(_0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit + "\\" + _0024VB_0024Local_DownloadInfo.Inherit + ".json"))
				{
					FileUtils.Copy($"{_0024VB_0024Local_ClientFolder}{lastPart}.json", $"{_0024VB_0024Local_McFolder}versions\\{_0024VB_0024Local_DownloadInfo.Inherit}\\{_0024VB_0024Local_DownloadInfo.Inherit}.json");
				}
				if (!FileUtils.Exists(_0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit + "\\" + _0024VB_0024Local_DownloadInfo.Inherit + ".jar"))
				{
					FileUtils.Copy($"{_0024VB_0024Local_ClientFolder}{lastPart}.jar", $"{_0024VB_0024Local_McFolder}versions\\{_0024VB_0024Local_DownloadInfo.Inherit}\\{_0024VB_0024Local_DownloadInfo.Inherit}.jar");
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(vanillaSyncLock);
				}
			}
		}

		[SpecialName]
		internal void _Lambda_0024__5(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
		{
			string text = ModMain.RequestTaskTempFolder(RequireNonSpace: true);
			string text2 = text + ".minecraft\\";
			try
			{
				if (DirectoryUtils.Exists(text2 + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit))
				{
					DirectoryUtils.Delete(text2 + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit, false);
				}
				DirectoryUtils.Create(text2 + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit + "\\");
				ModMinecraft.McFolderLauncherProfilesJsonCreate(text2);
				FileUtils.Copy(_0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit + "\\" + _0024VB_0024Local_DownloadInfo.Inherit + ".json", text2 + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit + "\\" + _0024VB_0024Local_DownloadInfo.Inherit + ".json");
				FileUtils.Copy(_0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit + "\\" + _0024VB_0024Local_DownloadInfo.Inherit + ".jar", text2 + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit + "\\" + _0024VB_0024Local_DownloadInfo.Inherit + ".jar");
				Task.Progress = 0.06;
				bool flag = true;
				while (true)
				{
					try
					{
						McDownloadOptiFineInstall(text, _0024VB_0024Local_Target, Task, flag);
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						if (flag)
						{
							Logger.Warn(ex2, "使用 JavaWrapper 安装 OptiFine 失败，将不使用 JavaWrapper 并重试", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							flag = false;
							ProjectData.ClearProjectError();
							continue;
						}
						throw new Exception("运行 OptiFine 安装器失败", ex2);
					}
					break;
				}
				Task.Progress = 0.96;
				FileUtils.Delete(text2 + "launcher_profiles.json", false);
				DirectoryUtils.Copy(text2, _0024VB_0024Local_McFolder);
				Task.Progress = 0.98;
				FileUtils.Delete(_0024VB_0024Local_Target, false);
				DirectoryUtils.Delete(text, false);
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception innerException = ex3;
				throw new Exception("安装 OptiFine（方式 A）失败", innerException);
			}
		}

		[SpecialName]
		internal void _Lambda_0024__6(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
		{
			try
			{
				FileUtils.Copy(_0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit + "\\" + _0024VB_0024Local_DownloadInfo.Inherit + ".jar", _0024VB_0024Local_VersionFolder + _0024VB_0024Local_Id + ".jar");
				Task.Progress = 0.7;
				ModMinecraft.McInstance mcInstance = new ModMinecraft.McInstance(_0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024Local_DownloadInfo.Inherit);
				string text = "{\r\n    \"id\": \"" + _0024VB_0024Local_Id + "\",\r\n    \"inheritsFrom\": \"" + _0024VB_0024Local_DownloadInfo.Inherit + "\",\r\n    \"time\": \"" + ((Operators.CompareString(_0024VB_0024Local_DownloadInfo.ReleaseTime, "", TextCompare: false) == 0) ? mcInstance.ReleaseTime.ToString("yyyy'-'MM'-'dd") : _0024VB_0024Local_DownloadInfo.ReleaseTime.Replace("/", "-")) + "T23:33:33+08:00\",\r\n    \"releaseTime\": \"" + ((Operators.CompareString(_0024VB_0024Local_DownloadInfo.ReleaseTime, "", TextCompare: false) == 0) ? mcInstance.ReleaseTime.ToString("yyyy'-'MM'-'dd") : _0024VB_0024Local_DownloadInfo.ReleaseTime.Replace("/", "-")) + "T23:33:33+08:00\",\r\n    \"type\": \"release\",\r\n    \"libraries\": [\r\n        {\"name\": \"optifine:OptiFine:" + PathUtils.GetFileNameWithoutExtension(_0024VB_0024Local_DownloadInfo.FileName).Replace("OptiFine_", "").Replace("preview_", "") + "\"},\r\n        {\"name\": \"net.minecraft:launchwrapper:1.12\"}\r\n    ],\r\n    \"mainClass\": \"net.minecraft.launchwrapper.Launch\",";
				Task.Progress = 0.8;
				text = ((!mcInstance.IsOldJson) ? (text + "\r\n    \"minimumLauncherVersion\": \"21\",\r\n    \"arguments\": {\r\n        \"game\": [\r\n            \"--tweakClass\",\r\n            \"optifine.OptiFineTweaker\"\r\n        ]\r\n    }\r\n}") : (text + "\r\n    \"minimumLauncherVersion\": 18,\r\n    \"minecraftArguments\": \"" + mcInstance.JsonObject["minecraftArguments"].ToString() + "  --tweakClass optifine.OptiFineTweaker\"\r\n}"));
				FileUtils.Write(_0024VB_0024Local_VersionFolder + _0024VB_0024Local_Id + ".json", text, (Encoding)null);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("安装 OptiFine（方式 B）失败", innerException);
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__28_002D0
	{
		public AutoResetEvent _0024VB_0024Local_outputWaitHandle;

		public _Closure_0024__28_002D1 _0024VB_0024NonLocal__0024VB_0024Closure_3;

		public _Closure_0024__28_002D0(_Closure_0024__28_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_outputWaitHandle = arg0._0024VB_0024Local_outputWaitHandle;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R14(object a0, DataReceivedEventArgs a1)
		{
			_Lambda_0024__0(RuntimeHelpers.GetObjectValue(a0), a1);
		}

		[SpecialName]
		internal object _Lambda_0024__0(object sender, DataReceivedEventArgs e)
		{
			try
			{
				if (e.Data == null)
				{
					_0024VB_0024Local_outputWaitHandle.Set();
				}
				else
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults.Enqueue(e.Data);
					if (_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults.Count > 100)
					{
						_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults.Dequeue();
					}
					ForgelikeInjectorLine(e.Data, _0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Task);
				}
			}
			catch (ObjectDisposedException ex)
			{
				ProjectData.SetProjectError(ex);
				ObjectDisposedException ex2 = ex;
				ProjectData.ClearProjectError();
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, $"读取 {_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LoaderName} 安装器信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
			try
			{
				if (_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Task.State == ModBase.LoadState.Canceled && !_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.HasExited)
				{
					Logger.Info($"由于任务取消，已中止 {_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LoaderName} 安装", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					_0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.Kill();
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			return null;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__28_002D1
	{
		public Queue<string> _0024VB_0024Local_LastResults;

		public string _0024VB_0024Local_LoaderName;

		public Process _0024VB_0024Local_process;

		public _Closure_0024__28_002D2 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__28_002D1(_Closure_0024__28_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_LastResults = arg0._0024VB_0024Local_LastResults;
				_0024VB_0024Local_LoaderName = arg0._0024VB_0024Local_LoaderName;
				_0024VB_0024Local_process = arg0._0024VB_0024Local_process;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__28_002D2
	{
		public ModLoader.LoaderTask<bool, bool> _0024VB_0024Local_Task;

		public _Closure_0024__28_002D2(_Closure_0024__28_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Task = arg0._0024VB_0024Local_Task;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__28_002D3
	{
		public AutoResetEvent _0024VB_0024Local_errorWaitHandle;

		public _Closure_0024__28_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_4;

		public _Closure_0024__28_002D3(_Closure_0024__28_002D3 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_errorWaitHandle = arg0._0024VB_0024Local_errorWaitHandle;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R15(object a0, DataReceivedEventArgs a1)
		{
			_Lambda_0024__1(RuntimeHelpers.GetObjectValue(a0), a1);
		}

		[SpecialName]
		internal object _Lambda_0024__1(object sender, DataReceivedEventArgs e)
		{
			try
			{
				if (e.Data == null)
				{
					_0024VB_0024Local_errorWaitHandle.Set();
				}
				else
				{
					_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults.Enqueue(e.Data);
					if (_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults.Count > 100)
					{
						_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults.Dequeue();
					}
					ForgelikeInjectorLine(e.Data, _0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Task);
				}
			}
			catch (ObjectDisposedException ex)
			{
				ProjectData.SetProjectError(ex);
				ObjectDisposedException ex2 = ex;
				ProjectData.ClearProjectError();
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, $"读取 {_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LoaderName} 安装器错误信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
			try
			{
				if (_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Task.State == ModBase.LoadState.Canceled && !_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.HasExited)
				{
					Logger.Info($"由于任务取消，已中止 {_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LoaderName} 安装", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					_0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.Kill();
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			return null;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__30_002D0
	{
		public string _0024VB_0024Local_Inherit;

		public string _0024VB_0024Local_LoaderVersion;

		public ModDownload.DlForgelikeEntry _0024VB_0024Local_Info;

		public string _0024VB_0024Local_LoaderName;

		public string _0024VB_0024Local_InstallerAddress;

		public string _0024VB_0024Local_CurrentMcFolder;

		public bool _0024VB_0024Local_IsNeoForge;

		public string _0024VB_0024Local_McFolder;

		public ModLoader.LoaderCombo<string> _0024VB_0024Local_ClientDownloadLoader;

		public string _0024VB_0024Local_ClientFolder;

		public string _0024VB_0024Local_VersionFolder;

		public string _0024VB_0024Local_NewInstanceName;

		public _Closure_0024__30_002D0(_Closure_0024__30_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Inherit = arg0._0024VB_0024Local_Inherit;
				_0024VB_0024Local_LoaderVersion = arg0._0024VB_0024Local_LoaderVersion;
				_0024VB_0024Local_Info = arg0._0024VB_0024Local_Info;
				_0024VB_0024Local_LoaderName = arg0._0024VB_0024Local_LoaderName;
				_0024VB_0024Local_InstallerAddress = arg0._0024VB_0024Local_InstallerAddress;
				_0024VB_0024Local_CurrentMcFolder = arg0._0024VB_0024Local_CurrentMcFolder;
				_0024VB_0024Local_IsNeoForge = arg0._0024VB_0024Local_IsNeoForge;
				_0024VB_0024Local_McFolder = arg0._0024VB_0024Local_McFolder;
				_0024VB_0024Local_ClientDownloadLoader = arg0._0024VB_0024Local_ClientDownloadLoader;
				_0024VB_0024Local_ClientFolder = arg0._0024VB_0024Local_ClientFolder;
				_0024VB_0024Local_VersionFolder = arg0._0024VB_0024Local_VersionFolder;
				_0024VB_0024Local_NewInstanceName = arg0._0024VB_0024Local_NewInstanceName;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(ModLoader.LoaderTask<string, string> Task)
		{
			ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("McDownloadForgeLoader " + _0024VB_0024Local_Inherit, ModDownload.DlForgeVersionMain);
			loaderTask.WaitForExit(_0024VB_0024Local_Inherit);
			Task.Progress = 0.8;
			foreach (ModDownload.DlForgeVersionEntry item in loaderTask.Output)
			{
				if (McUtils.CompareVersion(item.Version.ToString(), _0024VB_0024Local_LoaderVersion) == 0)
				{
					_0024VB_0024Local_Info = item;
					return;
				}
			}
			throw new Exception($"未能找到 {_0024VB_0024Local_LoaderName} " + _0024VB_0024Local_Inherit + "-" + _0024VB_0024Local_LoaderVersion + " 的详细信息！");
		}

		[SpecialName]
		internal void _Lambda_0024__1(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
		{
			//IL_010d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Expected O, but got Unknown
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			if (_0024VB_0024Local_Info.IsNeoForge)
			{
				string text = ((ModDownload.DlNeoForgeListEntry)_0024VB_0024Local_Info).UrlBase + "-installer.jar";
				list.Add(new ModNet.NetFile(new string[2]
				{
					text.Replace("maven.neoforged.net/releases", "bmclapi2.bangbang93.com/maven"),
					text
				}, _0024VB_0024Local_InstallerAddress, new FileChecker
				{
					MinSize = 65536L
				}));
			}
			else
			{
				ModDownload.DlForgeVersionEntry dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)_0024VB_0024Local_Info;
				string arg = string.Format("{0}-{1}/forge-{2}-{3}-{4}.{5}", dlForgeVersionEntry.Inherit.Replace("-", "_"), dlForgeVersionEntry.FileVersion, dlForgeVersionEntry.Inherit.Replace("-", "_"), dlForgeVersionEntry.FileVersion, dlForgeVersionEntry.Category, dlForgeVersionEntry.FileExtension);
				list.Add(new ModNet.NetFile(new string[2]
				{
					$"https://bmclapi2.bangbang93.com/maven/net/minecraftforge/forge/{arg}",
					$"https://files.minecraftforge.net/maven/net/minecraftforge/forge/{arg}"
				}, _0024VB_0024Local_InstallerAddress, new FileChecker
				{
					MinSize = 65536L,
					Hash = dlForgeVersionEntry.Hash
				}));
			}
			Task.Output = list;
		}

		[SpecialName]
		internal void _Lambda_0024__7(ModLoader.LoaderTask<bool, bool> Task)
		{
			try
			{
				_Closure_0024__30_002D2 arg = default(_Closure_0024__30_002D2);
				_Closure_0024__30_002D2 CS_0024_003C_003E8__locals0 = new _Closure_0024__30_002D2(arg);
				Logger.Info($"开始进行 Forgelike 安装：{_0024VB_0024Local_InstallerAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				CS_0024_003C_003E8__locals0._0024VB_0024Local_OldList = DirectoryUtils.EnumerateDirectories(_0024VB_0024Local_McFolder + "versions\\", false, "*").ToList();
				DirectoryUtils.Create(_0024VB_0024Local_VersionFolder);
				Task.Progress = 0.04;
				ModMinecraft.McFolderLauncherProfilesJsonCreate(_0024VB_0024Local_McFolder);
				Task.Progress = 0.05;
				bool flag = true;
				while (true)
				{
					try
					{
						ModBase.ExtractResources(ModBase.PathTemp + "Cache\\forge_installer.jar", "ForgeInstaller");
						Task.Progress = 0.06;
						ForgelikeInjector(_0024VB_0024Local_InstallerAddress, Task, _0024VB_0024Local_McFolder, flag, _0024VB_0024Local_IsNeoForge);
						Task.Progress = 0.97;
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						if (flag)
						{
							Logger.Warn(ex2, $"使用 JavaWrapper 安装 {_0024VB_0024Local_LoaderName} 失败，将不使用 JavaWrapper 并重试", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							flag = false;
							ProjectData.ClearProjectError();
							continue;
						}
						throw new Exception($"运行 {_0024VB_0024Local_LoaderName} 安装器失败", ex2);
					}
					break;
				}
				List<DirectoryInfo> list = (from f in DirectoryUtils.EnumerateDirectories(_0024VB_0024Local_McFolder + "versions\\", false, "*").SkipWhile([SpecialName] (string i) => CS_0024_003C_003E8__locals0._0024VB_0024Local_OldList.Contains(i))
					select DirectoryUtils.GetInfo(f)).ToList();
				if (list.Count > 1)
				{
					list = list.Where([SpecialName] (DirectoryInfo l) => StringExtensions.ContainsIgnoreCase(l.Name, "forge") && l.EnumerateFiles().Any()).ToList();
				}
				if (list.Count == 1)
				{
					FileInfo fileInfo = list[0].EnumerateFiles().First();
					FileUtils.Copy(fileInfo.FullName, _0024VB_0024Local_VersionFolder + _0024VB_0024Local_NewInstanceName + ".json");
					Logger.Info($"已复制新增的版本 JSON 文件：{fileInfo.FullName} -> {_0024VB_0024Local_VersionFolder}{_0024VB_0024Local_NewInstanceName}.json", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				else if (list.Count > 1)
				{
					Logger.Info(string.Format("有多个疑似的新增版本，无法确定：{0}", EnumerableExtensions.Join((IEnumerable)list.Select([SpecialName] (DirectoryInfo d) => d.Name), ";")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				else
				{
					Logger.Info("未找到新增的版本文件夹", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception innerException = ex3;
				throw new Exception($"安装新 {_0024VB_0024Local_LoaderName} 版本失败", innerException);
			}
			finally
			{
				try
				{
					FileUtils.Delete(_0024VB_0024Local_InstallerAddress, false);
				}
				catch (Exception ex4)
				{
					ProjectData.SetProjectError(ex4);
					Exception ex5 = ex4;
					Logger.Warn(ex5, $"安装 {_0024VB_0024Local_LoaderName} 清理文件时出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
			}
		}

		[SpecialName]
		internal void _Lambda_0024__12(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
		{
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Expected O, but got Unknown
			//IL_025e: Unknown result type (might be due to invalid IL or missing references)
			ZipArchive val = null;
			try
			{
				val = FileUtils.OpenZip(_0024VB_0024Local_InstallerAddress, default(CancellationToken));
				Task.Progress = 0.2;
				JObject val2 = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(val.GetEntry("install_profile.json").Open(), (Encoding)null));
				Task.Progress = 0.4;
				DirectoryUtils.Create(_0024VB_0024Local_VersionFolder);
				Task.Progress = 0.5;
				if (val2["install"] == null)
				{
					Logger.Info($"开始进行 Forge 安装，Legacy 方式 1：{_0024VB_0024Local_InstallerAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					JObject val3 = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(val.GetEntry(val2["json"].ToString().TrimStart('/')).Open(), (Encoding)null));
					val3["id"] = JToken.op_Implicit(_0024VB_0024Local_NewInstanceName);
					FileUtils.Write(_0024VB_0024Local_VersionFolder + _0024VB_0024Local_NewInstanceName + ".json", ((JToken)val3).ToString(), (Encoding)null);
					Task.Progress = 0.6;
					val.Dispose();
					FileUtils.ExtractToDirectory(_0024VB_0024Local_InstallerAddress, _0024VB_0024Local_InstallerAddress + "_unrar\\", default(CancellationToken), (ProgressProvider)null);
					if (DirectoryUtils.Exists(_0024VB_0024Local_InstallerAddress + "_unrar\\maven\\"))
					{
						DirectoryUtils.Copy(_0024VB_0024Local_InstallerAddress + "_unrar\\maven\\", _0024VB_0024Local_McFolder + "libraries\\");
					}
					DirectoryUtils.Delete(_0024VB_0024Local_InstallerAddress + "_unrar\\", false);
				}
				else
				{
					Logger.Info($"开始进行 Forge 安装，Legacy 方式 2：{_0024VB_0024Local_InstallerAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					FileUtils.Write(ModMinecraft.McLibGet((string)val2["install"][(object)"path"], WithHead: true, IgnoreLiteLoader: false, _0024VB_0024Local_McFolder), val.GetEntry((string)val2["install"][(object)"filePath"]).Open());
					Task.Progress = 0.9;
					val2["versionInfo"][(object)"id"] = JToken.op_Implicit(_0024VB_0024Local_NewInstanceName);
					if (val2["versionInfo"][(object)"inheritsFrom"] == null)
					{
						((JObject)val2["versionInfo"]).Add("inheritsFrom", JToken.op_Implicit(_0024VB_0024Local_Inherit));
					}
					FileUtils.Write(_0024VB_0024Local_VersionFolder + _0024VB_0024Local_NewInstanceName + ".json", val2["versionInfo"].ToString(), (Encoding)null);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("非新版方式安装 Forge 失败", innerException);
			}
			finally
			{
				try
				{
					if (val != null)
					{
						val.Dispose();
					}
					FileUtils.Delete(_0024VB_0024Local_InstallerAddress, false);
					DirectoryUtils.Delete(_0024VB_0024Local_InstallerAddress + "_unrar\\", false);
				}
				catch (Exception ex2)
				{
					ProjectData.SetProjectError(ex2);
					Exception ex3 = ex2;
					Logger.Warn(ex3, "非新版方式安装 Forge 清理文件时出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__30_002D1
	{
		public List<ModMinecraft.McLibEntry> _0024VB_0024Local_Libs;

		public _Closure_0024__30_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__30_002D1(_Closure_0024__30_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Libs = arg0._0024VB_0024Local_Libs;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__2(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
		{
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Expected O, but got Unknown
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			Task.Output = new List<ModNet.NetFile>();
			ZipArchive val = null;
			checked
			{
				try
				{
					val = FileUtils.OpenZip(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_InstallerAddress, default(CancellationToken));
					Task.Progress = 0.2;
					JObject val2 = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(val.GetEntry("install_profile.json").Open(), (Encoding)null));
					JObject val3 = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(val.GetEntry("version.json").Open(), (Encoding)null));
					((JContainer)val2).Merge((object)val3);
					_0024VB_0024Local_Libs = ModMinecraft.McLibListGetWithJson(val2, KeepSameNameDifferentVersionResult: true, _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CurrentMcFolder);
					if (val2["data"] != null && val2["data"][(object)"MOJMAPS"] != null)
					{
						Task.Progress = 0.4;
						JObject val4 = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByLoader(ModDownload.DlSourceLauncherOrMetaGet(Conversions.ToString(ModDownload.DlClientListGet(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit))), 45000, IsJson: true));
						string text = StringExtensions.BeforeFirst(val2["data"][(object)"MOJMAPS"][(object)"client"].ToString().Trim("[]".ToCharArray()), "@", false);
						string localPath = Path.ChangeExtension(ModMinecraft.McLibGet(text, WithHead: true, IgnoreLiteLoader: false, _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CurrentMcFolder), "." + StringExtensions.Split(val2["data"][(object)"MOJMAPS"][(object)"client"].ToString().Trim("[]".ToCharArray()), "@", false)[1]);
						JToken val5 = val4["downloads"][(object)"client_mappings"];
						_0024VB_0024Local_Libs.Add(new ModMinecraft.McLibEntry
						{
							IsNatives = false,
							LocalPath = localPath,
							OriginalName = text,
							Url = (string)val5[(object)"url"],
							Size = (long)val5[(object)"size"],
							SHA1 = (string)val5[(object)"sha1"]
						});
						Logger.Info(string.Format("需要下载 Mappings：{0} (SHA1: {1})", val5[(object)"url"], val5[(object)"sha1"]), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
					Task.Progress = 0.8;
					int num = _0024VB_0024Local_Libs.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						if (StringExtensions.EndsWithF(_0024VB_0024Local_Libs[i].LocalPath, $"{StringExtensions.Lower(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName)}-{_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit}-{_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderVersion}.jar", false) || StringExtensions.EndsWithF(_0024VB_0024Local_Libs[i].LocalPath, $"{StringExtensions.Lower(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName)}-{_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit}-{_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderVersion}-client.jar", false))
						{
							Logger.Warn($"已从待下载 {_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 支持库中移除：{_0024VB_0024Local_Libs[i].LocalPath}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							_0024VB_0024Local_Libs.RemoveAt(i);
							break;
						}
					}
					Task.Output = ModMinecraft.McLibNetFilesFromTokens(_0024VB_0024Local_Libs, _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CurrentMcFolder);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception(string.Format("获取{0}Forge 支持库列表失败", _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_IsNeoForge ? " Neo" : "新版 "), innerException);
				}
				finally
				{
					if (val != null)
					{
						val.Dispose();
					}
				}
			}
		}

		[SpecialName]
		internal void _Lambda_0024__3(ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task)
		{
			foreach (ModMinecraft.McLibEntry _0024VB_0024Local_Lib in _0024VB_0024Local_Libs)
			{
				string text = Path.Combine(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder, _0024VB_0024Local_Lib.LocalPath.Substring(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CurrentMcFolder.Length));
				if (!FileUtils.Exists(text))
				{
					FileUtils.Copy(_0024VB_0024Local_Lib.LocalPath, text);
				}
				Logger.Trace($"复制的 {_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 支持库文件：{_0024VB_0024Local_Lib.LocalPath}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			List<ModLoader.LoaderBase> source = (from l in _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_ClientDownloadLoader.GetLoaderList()
				where Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) == 0 || Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0
				where l.State != ModBase.LoadState.Finished
				select l).ToList();
			if (source.Any())
			{
				Logger.Info($"{_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 安装正在等待原版文件下载完成", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			while (source.Any() && !Task.IsCanceled)
			{
				source = source.Where((_Closure_0024__._0024I30_002D6 == null) ? (_Closure_0024__._0024I30_002D6 = [SpecialName] (ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished) : _Closure_0024__._0024I30_002D6).ToList();
				Thread.Sleep(50);
			}
			if (Task.IsCanceled)
			{
				return;
			}
			object vanillaSyncLock = VanillaSyncLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(vanillaSyncLock);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(vanillaSyncLock, ref lockTaken);
				string lastPart = PathUtils.GetLastPart(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_ClientFolder);
				DirectoryUtils.Create(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit);
				if (!FileUtils.Exists(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + "\\" + _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + ".json"))
				{
					FileUtils.Copy(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_ClientFolder + lastPart + ".json", _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + "\\" + _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + ".json");
				}
				if (!FileUtils.Exists(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + "\\" + _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + ".jar"))
				{
					FileUtils.Copy(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_ClientFolder + lastPart + ".jar", _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\" + _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + "\\" + _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + ".jar");
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(vanillaSyncLock);
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__30_002D2
	{
		public List<string> _0024VB_0024Local_OldList;

		public _Closure_0024__30_002D2(_Closure_0024__30_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_OldList = arg0._0024VB_0024Local_OldList;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__8(string i)
		{
			return _0024VB_0024Local_OldList.Contains(i);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__52_002D0
	{
		public string _0024VB_0024Local_VersionFolder;

		public string _0024VB_0024Local_OptiFineFolder;

		public bool _0024VB_0024Local_OptiFineAsMod;

		public string _0024VB_0024Local_ForgeFolder;

		public string _0024VB_0024Local_NeoForgeFolder;

		public string _0024VB_0024Local_FabricFolder;

		public string _0024VB_0024Local_LiteLoaderFolder;

		public string _0024VB_0024Local_TempMcFolder;

		public string _0024VB_0024Local_MinecraftFolder;

		public string _0024VB_0024Local_TargetFolder;

		public McInstallRequest _0024VB_0024Local_Request;

		public string _0024VB_0024Local_ModsTempFolder;

		public bool _0024VB_0024Local_Modable;

		public Dictionary<string, string> _0024VB_0024Local_ManagedFileSources;

		public string _0024VB_0024Local_ForgeVersion;

		public string _0024VB_0024Local_NeoForgeVersion;

		public _Closure_0024__52_002D0(_Closure_0024__52_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_VersionFolder = arg0._0024VB_0024Local_VersionFolder;
				_0024VB_0024Local_OptiFineFolder = arg0._0024VB_0024Local_OptiFineFolder;
				_0024VB_0024Local_OptiFineAsMod = arg0._0024VB_0024Local_OptiFineAsMod;
				_0024VB_0024Local_ForgeFolder = arg0._0024VB_0024Local_ForgeFolder;
				_0024VB_0024Local_NeoForgeFolder = arg0._0024VB_0024Local_NeoForgeFolder;
				_0024VB_0024Local_FabricFolder = arg0._0024VB_0024Local_FabricFolder;
				_0024VB_0024Local_LiteLoaderFolder = arg0._0024VB_0024Local_LiteLoaderFolder;
				_0024VB_0024Local_TempMcFolder = arg0._0024VB_0024Local_TempMcFolder;
				_0024VB_0024Local_MinecraftFolder = arg0._0024VB_0024Local_MinecraftFolder;
				_0024VB_0024Local_TargetFolder = arg0._0024VB_0024Local_TargetFolder;
				_0024VB_0024Local_Request = arg0._0024VB_0024Local_Request;
				_0024VB_0024Local_ModsTempFolder = arg0._0024VB_0024Local_ModsTempFolder;
				_0024VB_0024Local_Modable = arg0._0024VB_0024Local_Modable;
				_0024VB_0024Local_ManagedFileSources = arg0._0024VB_0024Local_ManagedFileSources;
				_0024VB_0024Local_ForgeVersion = arg0._0024VB_0024Local_ForgeVersion;
				_0024VB_0024Local_NeoForgeVersion = arg0._0024VB_0024Local_NeoForgeVersion;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R22(ModLoader.LoaderTask<int, int> a0)
		{
			_Lambda_0024__0();
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			FileUtils.Write(_0024VB_0024Local_VersionFolder + ".pclignore", "用于临时地在 PCL 的版本列表中屏蔽此版本。", (Encoding)null);
		}

		[SpecialName]
		internal void _Lambda_0024__1(ModLoader.LoaderTask<string, string> Task)
		{
			MergeJson(_0024VB_0024Local_VersionFolder, _0024VB_0024Local_OptiFineFolder, _0024VB_0024Local_OptiFineAsMod, _0024VB_0024Local_ForgeFolder, _0024VB_0024Local_NeoForgeFolder, _0024VB_0024Local_FabricFolder, _0024VB_0024Local_LiteLoaderFolder);
			Task.Progress = 0.2;
			DirectoryUtils.Copy(_0024VB_0024Local_TempMcFolder + "libraries", _0024VB_0024Local_MinecraftFolder + "libraries");
			Task.Progress = 0.8;
			_0024VB_0024Local_TargetFolder = (_0024VB_0024Local_Request.IsIntermediateOutput ? _0024VB_0024Local_VersionFolder : new ModMinecraft.McInstance(_0024VB_0024Local_VersionFolder).PathIndie);
			string text = _0024VB_0024Local_TargetFolder + "mods\\";
			if (DirectoryUtils.Exists(_0024VB_0024Local_ModsTempFolder))
			{
				DirectoryUtils.Copy(_0024VB_0024Local_ModsTempFolder, text);
			}
			else if (_0024VB_0024Local_Modable)
			{
				DirectoryUtils.Create(text);
				Logger.Info($"自动创建 Mod 文件夹：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			string text2 = _0024VB_0024Local_TargetFolder + "resourcepacks\\";
			DirectoryUtils.Create(text2);
			Logger.Info($"自动创建资源包文件夹：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		}

		[SpecialName]
		internal void _Lambda_0024__2(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
		{
			Task.Output = ModMinecraft.McLibNetFilesFromInstance(new ModMinecraft.McInstance(_0024VB_0024Local_VersionFolder));
		}

		[SpecialName]
		internal void _Lambda_0024__R23(ModLoader.LoaderTask<int, int> a0)
		{
			_Lambda_0024__3();
		}

		[SpecialName]
		internal void _Lambda_0024__3()
		{
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Expected O, but got Unknown
			//IL_0189: Unknown result type (might be due to invalid IL or missing references)
			//IL_018f: Expected O, but got Unknown
			//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Expected O, but got Unknown
			List<ManagedFile> list = new List<ManagedFile>();
			foreach (KeyValuePair<string, string> _0024VB_0024Local_ManagedFileSource in _0024VB_0024Local_ManagedFileSources)
			{
				string text = _0024VB_0024Local_ManagedFileSource.Value.Substring(_0024VB_0024Local_ModsTempFolder.Length);
				string text2 = PathUtils.ForCompare(Path.Combine(_0024VB_0024Local_TargetFolder, "mods", text));
				list.Add(new ManagedFile("mods/" + text.Replace("\\", "/").ToLowerInvariant(), CryptographyUtils.ComputeFileHash(text2, (HashMethod)1), FileUtils.GetInfo(text2).LastWriteTimeUtc, _0024VB_0024Local_ManagedFileSource.Key));
			}
			Dictionary<ModLoaders, string> dictionary = new Dictionary<ModLoaders, string>(_0024VB_0024Local_Request.Components.ModLoaderVersions);
			if (_0024VB_0024Local_ForgeVersion != null && StringExtensions.StartsWithF(_0024VB_0024Local_ForgeVersion, "1.", false) && _0024VB_0024Local_ForgeVersion.Contains("-"))
			{
				dictionary[(ModLoaders)1] = StringExtensions.AfterLast(_0024VB_0024Local_ForgeVersion, "-", false);
			}
			if (_0024VB_0024Local_NeoForgeVersion != null && StringExtensions.StartsWithF(_0024VB_0024Local_NeoForgeVersion, "1.20.1-", false))
			{
				dictionary[(ModLoaders)16] = _0024VB_0024Local_NeoForgeVersion.Replace("1.20.1-", "");
			}
			McComponents val = new McComponents(_0024VB_0024Local_Request.Components.MinecraftVersion, dictionary, _0024VB_0024Local_Request.Components.OptiFineEntry, _0024VB_0024Local_Request.Components.FabricApiVersion, _0024VB_0024Local_Request.Components.OptiFabricVersion);
			JsonConfigProvider config = new ModMinecraft.McInstance(_0024VB_0024Local_VersionFolder).Config;
			Configs.McInstallManifest.Set(new McInstallManifest(1, val, DateTime.UtcNow, (IReadOnlyList<ManagedFile>)list), (IConfigProvider)(object)config);
			config.Save();
		}

		[SpecialName]
		internal void _Lambda_0024__R24(ModLoader.LoaderTask<int, int> a0)
		{
			_Lambda_0024__4();
		}

		[SpecialName]
		internal void _Lambda_0024__4()
		{
			FileUtils.Delete(_0024VB_0024Local_VersionFolder + ".pclignore", false);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__55_002D0
	{
		public ModMinecraft.McInstance _0024VB_0024Local_Instance;

		public McInstallRequest _0024VB_0024Local_Request;

		public _Closure_0024__55_002D0(_Closure_0024__55_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Instance = arg0._0024VB_0024Local_Instance;
				_0024VB_0024Local_Request = arg0._0024VB_0024Local_Request;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R25(ModLoader.LoaderTask<int, int> a0)
		{
			_Lambda_0024__0();
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			foreach (string item in DirectoryUtils.EnumerateFiles(ModLaunch.GetNativesFolder(_0024VB_0024Local_Instance), true, "*.dll"))
			{
				if (!FileUtils.IsAvailable(item))
				{
					throw new Exception($"Natives 文件正在被占用，请关闭游戏后再次尝试：{item}");
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__55_002D1
	{
		public string _0024VB_0024Local_BuildFolder;

		public string _0024VB_0024Local_TargetFolder;

		public bool _0024VB_0024Local_RenameFailed;

		public ModLoader.LoaderCombo<string> _0024VB_0024Local_Loader;

		public _Closure_0024__55_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__55_002D1(_Closure_0024__55_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_BuildFolder = arg0._0024VB_0024Local_BuildFolder;
				_0024VB_0024Local_TargetFolder = arg0._0024VB_0024Local_TargetFolder;
				_0024VB_0024Local_RenameFailed = arg0._0024VB_0024Local_RenameFailed;
				_0024VB_0024Local_Loader = arg0._0024VB_0024Local_Loader;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__1(ModLoader.LoaderTask<int, int> Task)
		{
			_Closure_0024__55_002D2 arg = default(_Closure_0024__55_002D2);
			_Closure_0024__55_002D2 CS_0024_003C_003E8__locals0 = new _Closure_0024__55_002D2(arg)
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_3 = this
			};
			McInstallCommit(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance, Configs.McInstallManifest.Get((IConfigProvider)(object)new ModMinecraft.McInstance(_0024VB_0024Local_BuildFolder).Config), _0024VB_0024Local_BuildFolder, _0024VB_0024Local_TargetFolder);
			CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstance = null;
			if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.NewInstanceName == null)
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstance = new ModMinecraft.McInstance(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion).Load();
			}
			else
			{
				try
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstance = _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.Rename(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.NewInstanceName);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					_0024VB_0024Local_RenameFailed = true;
					Logger.Warn(ex2, $"版本修改成功，但重命名为 {_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.NewInstanceName} 失败：{ExceptionExtensions.GetDisplay(ex2, false)}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstance = new ModMinecraft.McInstance(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion).Load();
					ProjectData.ClearProjectError();
				}
			}
			if ((object)CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstance == null)
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstance = new ModMinecraft.McInstance(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion).Load();
			}
			ModBase.RunInUiWait([SpecialName] () =>
			{
				ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
				if ((object)PageInstanceLeft.Instance != null && PathUtils.IsSame(PageInstanceLeft.Instance.PathVersion, CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion))
				{
					PageInstanceLeft.Instance = CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstance;
				}
				if ((object)ModMinecraft.McInstanceSelected != null && PathUtils.IsSame(ModMinecraft.McInstanceSelected.PathVersion, CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion))
				{
					ModMinecraft.McInstanceSelected = CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstance;
				}
			});
		}

		[SpecialName]
		internal void _Lambda_0024__3(ModLoader.LoaderBase CurrentLoader)
		{
			switch (CurrentLoader.State)
			{
			case ModBase.LoadState.Finished:
				if (!_0024VB_0024Local_RenameFailed)
				{
					ModMain.Hint(CurrentLoader.Name + "成功！", ModMain.HintType.Green);
				}
				else
				{
					ModMain.Hint(CurrentLoader.Name + "成功，但重命名失败！", ModMain.HintType.Green);
				}
				break;
			case ModBase.LoadState.Failed:
				ModMain.MyMsgBox(ExceptionExtensions.GetDisplay(CurrentLoader.Error, true), CurrentLoader.Name + "失败", "确定", "", "", IsWarn: true);
				break;
			case ModBase.LoadState.Canceled:
				ModMain.Hint(CurrentLoader.Name + "已取消！");
				break;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__4()
		{
			_0024VB_0024Local_Loader.WaitForExit(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__55_002D2
	{
		public ModMinecraft.McInstance _0024VB_0024Local_NewInstance;

		public _Closure_0024__55_002D1 _0024VB_0024NonLocal__0024VB_0024Closure_3;

		public _Closure_0024__55_002D2(_Closure_0024__55_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_NewInstance = arg0._0024VB_0024Local_NewInstance;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__2()
		{
			ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
			if ((object)PageInstanceLeft.Instance != null && PathUtils.IsSame(PageInstanceLeft.Instance.PathVersion, _0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion))
			{
				PageInstanceLeft.Instance = _0024VB_0024Local_NewInstance;
			}
			if ((object)ModMinecraft.McInstanceSelected != null && PathUtils.IsSame(ModMinecraft.McInstanceSelected.PathVersion, _0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion))
			{
				ModMinecraft.McInstanceSelected = _0024VB_0024Local_NewInstance;
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__56_002D0
	{
		public string _0024VB_0024Local_TargetFolder;

		public _Closure_0024__56_002D0(_Closure_0024__56_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_TargetFolder = arg0._0024VB_0024Local_TargetFolder;
			}
		}

		[SpecialName]
		internal ManagedFile _Lambda_0024__1(ManagedFile File)
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			return new ManagedFile(File.RelativePath, File.Sha1, FileUtils.GetInfo(Path.Combine(_0024VB_0024Local_TargetFolder, File.RelativePath)).LastWriteTimeUtc, File.Component);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__8_002D0
	{
		public string _0024VB_0024Local_VersionFolder;

		public string _0024VB_0024Local_Id;

		public _Closure_0024__8_002D0(_Closure_0024__8_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_VersionFolder = arg0._0024VB_0024Local_VersionFolder;
				_0024VB_0024Local_Id = arg0._0024VB_0024Local_Id;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
		{
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0148: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Expected O, but got Unknown
			ModMinecraft.McInstance mcInstance = new ModMinecraft.McInstance(_0024VB_0024Local_VersionFolder);
			if (mcInstance.JsonObject["downloads"] == null || mcInstance.JsonObject["downloads"][(object)"server"] == null || mcInstance.JsonObject["downloads"][(object)"server"][(object)"url"] == null)
			{
				FileUtils.Delete(_0024VB_0024Local_VersionFolder + _0024VB_0024Local_Id + ".json", false);
				if (!DirectoryUtils.GetInfo(_0024VB_0024Local_VersionFolder).GetFileSystemInfos().Any())
				{
					DirectoryUtils.Delete(_0024VB_0024Local_VersionFolder, false);
				}
				Task.Output = new List<ModNet.NetFile>();
				ModMain.Hint($"Mojang 没有给 Minecraft {_0024VB_0024Local_Id} 提供官方服务端下载，没法下，撤退！", ModMain.HintType.Red);
				Thread.Sleep(2000);
				Task.Cancel();
			}
			else
			{
				string original = (string)mcInstance.JsonObject["downloads"][(object)"server"][(object)"url"];
				FileChecker checker = new FileChecker
				{
					MinSize = 1024L,
					ActualSize = (long)(mcInstance.JsonObject["downloads"][(object)"server"][(object)"size"] ?? JToken.op_Implicit(-1)),
					Hash = (string)mcInstance.JsonObject["downloads"][(object)"server"][(object)"sha1"]
				};
				Task.Output = new List<ModNet.NetFile>
				{
					new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), _0024VB_0024Local_VersionFolder + _0024VB_0024Local_Id + "-server.jar", checker)
				};
				string text = $"@echo off\r\ntitle {_0024VB_0024Local_Id} 原版服务端\r\necho 如果服务端立即停止，请右键编辑该脚本，将下一行开头的 java 替换为适合该 Minecraft 版本的完整 java.exe 的路径。\r\necho 你可以在 PCL 的 [设置 → 启动选项] 中查看已安装的 java，所需的 java.exe 一般在其中的 bin 文件夹下。\r\necho ------------------------------\r\necho 如果提示 \"You need to agree to the EULA in order to run the server\"，请打开 eula.txt，按说明阅读并同意 Minecraft EULA 后，将该文件最后一行中的 eula=false 改为 eula=true。\r\necho ------------------------------\r\n\"java\" -server -XX:+UseG1GC -Xmx6144M -Xms1024M -jar {_0024VB_0024Local_Id}-server.jar nogui\r\necho ----------------------\r\necho 服务端已停止。\r\npause";
				FileUtils.Write(_0024VB_0024Local_VersionFolder + "Launch Server.bat", text, Encoding.Default.Equals(Encoding.UTF8) ? Encoding.UTF8 : Encoding.GetEncoding("GB18030"));
				FileUtils.Delete(_0024VB_0024Local_VersionFolder + _0024VB_0024Local_Id + ".json", false);
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__9_002D0
	{
		public string _0024VB_0024Local_VersionFolder;

		public _Closure_0024__9_002D0(_Closure_0024__9_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_VersionFolder = arg0._0024VB_0024Local_VersionFolder;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task)
		{
			Task.Output = new List<ModNet.NetFile> { ModDownload.DlClientJarGet(new ModMinecraft.McInstance(_0024VB_0024Local_VersionFolder), ReturnNothingOnFileUseable: false) };
		}
	}

	private const string McDownloadClientLibName = "下载原版支持库文件";

	private const string McDownloadClientJsonName = "下载原版 json 文件";

	private static bool IsForgeRecommendedRefreshed = false;

	private static object InstallSyncLock = RuntimeHelpers.GetObjectValue(new object());

	private static object VanillaSyncLock = RuntimeHelpers.GetObjectValue(new object());

	private static List<ModLoader.LoaderBase> McDownloadClientLoader(string Id, string JsonUrl, string VersionFolder)
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		string lastPart = PathUtils.GetLastPart(VersionFolder);
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		ModMinecraft.McInstance mcInstance = null;
		if (JsonUrl == null)
		{
			list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取原版 json 文件下载地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				string original = Conversions.ToString(ModDownload.DlClientListGet(Id));
				Task.Output = new List<ModNet.NetFile>
				{
					new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), VersionFolder + lastPart + ".json")
				};
			})
			{
				ProgressWeight = 2.0,
				Show = false
			});
		}
		list.Add(new ModNet.LoaderDownload("下载原版 json 文件", new List<ModNet.NetFile>
		{
			new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(JsonUrl ?? ""), VersionFolder + lastPart + ".json", new FileChecker
			{
				IsJson = true
			})
		})
		{
			ProgressWeight = 3.0
		});
		list.Add(new ModLoader.LoaderTask<string, string>("读取 json 文件", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
		{
			mcInstance = new ModMinecraft.McInstance(VersionFolder);
			mcInstance.Load();
		})
		{
			ProgressWeight = 0.0,
			Show = false
		});
		List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
		list2.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析原版支持库文件（副加载器）", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
		{
			Logger.Info($"开始分析原版支持库文件：{VersionFolder}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			Task.Output = ModMinecraft.McLibNetFilesFromInstance(mcInstance);
		})
		{
			ProgressWeight = 1.0,
			Show = false
		});
		list2.Add(new ModNet.LoaderDownload("下载原版支持库文件（副加载器）", new List<ModNet.NetFile>())
		{
			ProgressWeight = 13.0,
			Show = false
		});
		list.Add(new ModLoader.LoaderCombo<string>("下载原版支持库文件", list2)
		{
			Block = false,
			ProgressWeight = 14.0
		});
		List<ModLoader.LoaderBase> list3 = new List<ModLoader.LoaderBase>();
		list3.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析资源文件索引地址（副加载器）", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
		{
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Expected O, but got Unknown
			try
			{
				ModNet.NetFile netFile = ModDownload.DlClientAssetIndexGet(mcInstance);
				Task.Output = ((netFile == null) ? new List<ModNet.NetFile>() : new List<ModNet.NetFile> { netFile });
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				throw new Exception("分析资源文件索引地址失败", innerException);
			}
			try
			{
				JObject val = (JObject)FileUtils.ReadAsJson(VersionFolder + lastPart + ".json", (Encoding)null, (Type)null);
				val["clientVersion"] = JToken.op_Implicit(Id);
				FileUtils.Write($"{VersionFolder}{lastPart}.json", ((JToken)val).ToString(), (Encoding)null);
			}
			catch (Exception ex2)
			{
				ProjectData.SetProjectError(ex2);
				Exception innerException2 = ex2;
				throw new Exception("添加客户端版本失败", innerException2);
			}
		})
		{
			ProgressWeight = 1.0,
			Show = false
		});
		list3.Add(new ModNet.LoaderDownload("下载资源文件索引（副加载器）", new List<ModNet.NetFile>())
		{
			ProgressWeight = 3.0,
			Show = false
		});
		list3.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析所需资源文件（副加载器）", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
		{
			ModLoader.LoaderTask<string, List<ModNet.NetFile>> loaderTask = Task;
			ModMinecraft.McInstance instance = mcInstance;
			ModLoader.LoaderBase ProgressFeed = Task;
			List<ModNet.NetFile> output = ModMinecraft.McAssetsFixList(instance, CheckHash: true, ref ProgressFeed);
			Task = (ModLoader.LoaderTask<string, List<ModNet.NetFile>>)ProgressFeed;
			loaderTask.Output = output;
		})
		{
			ProgressWeight = 0.01,
			Show = false
		});
		list3.Add(new ModNet.LoaderDownload("下载资源文件（副加载器）", new List<ModNet.NetFile>())
		{
			ProgressWeight = 14.0,
			Show = false
		});
		list.Add(new ModLoader.LoaderCombo<string>("下载原版资源文件", list3)
		{
			Block = false,
			ProgressWeight = 18.0
		});
		return list;
	}

	public static MyVirtualizingElement<MyListItem> McDownloadListItem(JObject Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
	{
		return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
		{
			JToken val = Entry["type"];
			string logo = (((string)val == "release") ? (ModBase.PathImage + "Blocks/Grass.png") : (((string)val == "snapshot") ? (ModBase.PathImage + "Blocks/CommandBlock.png") : ((!((string)val == "special")) ? (ModBase.PathImage + "Blocks/CobbleStone.png") : (ModBase.PathImage + "Blocks/GoldBlock.png"))));
			MyListItem myListItem = new MyListItem
			{
				Logo = logo,
				SnapsToDevicePixels = true,
				Title = Entry["id"].ToString(),
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Entry
			};
			if (Entry["lore"] == null)
			{
				myListItem.Info = Extensions.Value<DateTime>((IEnumerable<JToken>)Entry["releaseTime"]).ToString("yyyy'/'MM'/'dd HH':'mm");
			}
			else
			{
				myListItem.Info = Entry["lore"].ToString();
			}
			if (Entry["url"].ToString().Contains("pcl"))
			{
				myListItem.Info = "[PCL 特供下载] " + myListItem.Info;
			}
			myListItem.Click += OnClick;
			if (IsSaveOnly)
			{
				myListItem.ContentHandler = McDownloadSaveMenuBuild;
			}
			else
			{
				myListItem.ContentHandler = McDownloadMenuBuild;
			}
			return myListItem;
		})
		{
			Height = 42.0
		};
	}

	private static void McDownloadSaveMenuBuild(object sender, EventArgs e)
	{
		MyIconButton myIconButton = new MyIconButton
		{
			LogoScale = 1.05,
			Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
			ToolTip = "更新日志"
		};
		myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			McDownloadMenuLog(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		MyIconButton myIconButton2 = new MyIconButton
		{
			LogoScale = 1.0,
			Logo = "M224 160a64 64 0 0 0-64 64v576a64 64 0 0 0 64 64h576a64 64 0 0 0 64-64V224a64 64 0 0 0-64-64H224z m0 384h576v256H224v-256z m192 96v64h320v-64H416z m-128 0v64h64v-64H288zM224 224h576v256H224V224z m192 96v64h320v-64H416z m-128 0v64h64v-64H288z",
			ToolTip = "下载服务端"
		};
		myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			McDownloadMenuSaveServer(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		NewLateBinding.LateSet(sender, null, "Buttons", new object[1] { new MyIconButton[2] { myIconButton2, myIconButton } }, null, null);
	}

	private static void McDownloadMenuBuild(object sender, EventArgs e)
	{
		MyIconButton myIconButton = new MyIconButton
		{
			Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
			ToolTip = "另存为"
		};
		myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			McDownloadMenuSave(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		MyIconButton myIconButton2 = new MyIconButton
		{
			LogoScale = 1.05,
			Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
			ToolTip = "更新日志"
		};
		myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			McDownloadMenuLog(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		MyIconButton myIconButton3 = new MyIconButton
		{
			LogoScale = 1.0,
			Logo = "M224 160a64 64 0 0 0-64 64v576a64 64 0 0 0 64 64h576a64 64 0 0 0 64-64V224a64 64 0 0 0-64-64H224z m0 384h576v256H224v-256z m192 96v64h320v-64H416z m-128 0v64h64v-64H288zM224 224h576v256H224V224z m192 96v64h320v-64H416z m-128 0v64h64v-64H288z",
			ToolTip = "下载服务端"
		};
		myIconButton3.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			McDownloadMenuSaveServer(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		NewLateBinding.LateSet(sender, null, "Buttons", new object[1] { new MyIconButton[3] { myIconButton, myIconButton2, myIconButton3 } }, null, null);
	}

	private static void McDownloadMenuLog(object sender, RoutedEventArgs e)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		JToken instanceJson = ((NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null) ? ((JToken)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)) : ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((JToken)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((JToken)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null))));
		McUpdateLogShow(instanceJson);
	}

	private static void McDownloadMenuSaveServer(object sender, RoutedEventArgs e)
	{
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Expected O, but got Unknown
		MyListItem myListItem = ((sender is MyListItem) ? ((MyListItem)sender) : ((!(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null) is MyListItem)) ? ((MyListItem)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null)) : ((MyListItem)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null))));
		try
		{
			_Closure_0024__8_002D0 arg = default(_Closure_0024__8_002D0);
			_Closure_0024__8_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__8_002D0(arg);
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Id = myListItem.Title;
			string original = NewLateBinding.LateIndexGet(myListItem.Tag, new object[1] { "url" }, null).ToString();
			CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = Dialogs.SelectFolder("选择目标文件夹", false, (string)null).FirstOrDefault();
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder == null)
			{
				return;
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + CS_0024_003C_003E8__locals0._0024VB_0024Local_Id + "\\";
			foreach (ModLoader.LoaderBase item in ((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).ToList())
			{
				if (Operators.CompareString(item.Name, $"Minecraft {CS_0024_003C_003E8__locals0._0024VB_0024Local_Id} 服务端下载", TextCompare: false) == 0)
				{
					ModMain.Hint("该服务端正在下载中！", ModMain.HintType.Red);
					return;
				}
			}
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModNet.LoaderDownload("下载版本 JSON 文件", new List<ModNet.NetFile>
			{
				new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + CS_0024_003C_003E8__locals0._0024VB_0024Local_Id + ".json", new FileChecker
				{
					IsJson = true
				})
			})
			{
				ProgressWeight = 2.0
			});
			list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("构建服务端", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
				//IL_0103: Unknown result type (might be due to invalid IL or missing references)
				//IL_010f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0148: Unknown result type (might be due to invalid IL or missing references)
				//IL_0178: Expected O, but got Unknown
				ModMinecraft.McInstance mcInstance = new ModMinecraft.McInstance(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder);
				if (mcInstance.JsonObject["downloads"] == null || mcInstance.JsonObject["downloads"][(object)"server"] == null || mcInstance.JsonObject["downloads"][(object)"server"][(object)"url"] == null)
				{
					FileUtils.Delete(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + CS_0024_003C_003E8__locals0._0024VB_0024Local_Id + ".json", false);
					if (!DirectoryUtils.GetInfo(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder).GetFileSystemInfos().Any())
					{
						DirectoryUtils.Delete(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder, false);
					}
					Task.Output = new List<ModNet.NetFile>();
					ModMain.Hint($"Mojang 没有给 Minecraft {CS_0024_003C_003E8__locals0._0024VB_0024Local_Id} 提供官方服务端下载，没法下，撤退！", ModMain.HintType.Red);
					Thread.Sleep(2000);
					Task.Cancel();
				}
				else
				{
					string original2 = (string)mcInstance.JsonObject["downloads"][(object)"server"][(object)"url"];
					FileChecker checker = new FileChecker
					{
						MinSize = 1024L,
						ActualSize = (long)(mcInstance.JsonObject["downloads"][(object)"server"][(object)"size"] ?? JToken.op_Implicit(-1)),
						Hash = (string)mcInstance.JsonObject["downloads"][(object)"server"][(object)"sha1"]
					};
					Task.Output = new List<ModNet.NetFile>
					{
						new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original2), CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + CS_0024_003C_003E8__locals0._0024VB_0024Local_Id + "-server.jar", checker)
					};
					string text = $"@echo off\r\ntitle {CS_0024_003C_003E8__locals0._0024VB_0024Local_Id} 原版服务端\r\necho 如果服务端立即停止，请右键编辑该脚本，将下一行开头的 java 替换为适合该 Minecraft 版本的完整 java.exe 的路径。\r\necho 你可以在 PCL 的 [设置 → 启动选项] 中查看已安装的 java，所需的 java.exe 一般在其中的 bin 文件夹下。\r\necho ------------------------------\r\necho 如果提示 \"You need to agree to the EULA in order to run the server\"，请打开 eula.txt，按说明阅读并同意 Minecraft EULA 后，将该文件最后一行中的 eula=false 改为 eula=true。\r\necho ------------------------------\r\n\"java\" -server -XX:+UseG1GC -Xmx6144M -Xms1024M -jar {CS_0024_003C_003E8__locals0._0024VB_0024Local_Id}-server.jar nogui\r\necho ----------------------\r\necho 服务端已停止。\r\npause";
					FileUtils.Write(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + "Launch Server.bat", text, Encoding.Default.Equals(Encoding.UTF8) ? Encoding.UTF8 : Encoding.GetEncoding("GB18030"));
					FileUtils.Delete(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + CS_0024_003C_003E8__locals0._0024VB_0024Local_Id + ".json", false);
				}
			})
			{
				ProgressWeight = 0.5,
				Show = false
			});
			list.Add(new ModNet.LoaderDownload("下载服务端文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 5.0
			});
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("Minecraft " + CS_0024_003C_003E8__locals0._0024VB_0024Local_Id + " 服务端下载", list);
			loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
			loaderCombo.Start(CS_0024_003C_003E8__locals0._0024VB_0024Local_Id);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.BtnExtraDownload.Ribble();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "开始 Minecraft 服务端下载失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			ProjectData.ClearProjectError();
		}
	}

	public static void McDownloadMenuSave(object sender, RoutedEventArgs e)
	{
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		MyListItem myListItem = ((sender is MyListItem) ? ((MyListItem)sender) : ((!(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null) is MyListItem)) ? ((MyListItem)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null)) : ((MyListItem)NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null))));
		try
		{
			_Closure_0024__9_002D0 arg = default(_Closure_0024__9_002D0);
			_Closure_0024__9_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__9_002D0(arg);
			string title = myListItem.Title;
			string original = NewLateBinding.LateIndexGet(myListItem.Tag, new object[1] { "url" }, null).ToString();
			CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = Dialogs.SelectFolder("选择目标文件夹", false, (string)null).FirstOrDefault();
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder == null)
			{
				return;
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + title + "\\";
			foreach (ModLoader.LoaderBase item in ((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).ToList())
			{
				if (Operators.CompareString(item.Name, $"Minecraft {title} 下载", TextCompare: false) == 0)
				{
					ModMain.Hint("该版本正在下载中！", ModMain.HintType.Red);
					return;
				}
			}
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModNet.LoaderDownload("下载版本 JSON 文件", new List<ModNet.NetFile>
			{
				new ModNet.NetFile(ModDownload.DlSourceLauncherOrMetaGet(original), CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + title + ".json", new FileChecker
				{
					IsJson = true
				})
			})
			{
				ProgressWeight = 2.0
			});
			list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析版本 JAR 文件下载地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				Task.Output = new List<ModNet.NetFile> { ModDownload.DlClientJarGet(new ModMinecraft.McInstance(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder), ReturnNothingOnFileUseable: false) };
			})
			{
				ProgressWeight = 0.5,
				Show = false
			});
			list.Add(new ModNet.LoaderDownload("下载版本 JAR 文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 5.0
			});
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("Minecraft " + title + " 下载", list);
			loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
			loaderCombo.Start(title);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.BtnExtraDownload.Ribble();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "开始 Minecraft 下载失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			ProjectData.ClearProjectError();
		}
	}

	public static void McUpdateLogShow(JToken InstanceJson)
	{
		string text = StringExtensions.Lower(InstanceJson[(object)"id"].ToString());
		string text2;
		switch (text)
		{
		case "3d shareware v1.34":
			text2 = "3D_Shareware_v1.34";
			break;
		case "2.0":
			text2 = "Java版2.0";
			break;
		case "1.rv-pre1":
			text2 = "Java版1.RV-Pre1";
			break;
		default:
			if (!text.Contains("combat-1") && !text.Contains("combat-212796"))
			{
				if (Operators.CompareString(text, "combat test 2", TextCompare: false) == 0 || text.Contains("combat-2") || text.Contains("combat-0"))
				{
					text2 = "Java版Combat_Test_2";
					break;
				}
				switch (text)
				{
				case "combat test 3":
				case "1.14_combat-3":
					text2 = "Java版Combat_Test_3";
					break;
				case "combat test 4":
				case "1.15_combat-1":
					text2 = "Java版Combat_Test_4";
					break;
				case "combat test 5":
				case "1.15_combat-6":
					text2 = "Java版Combat_Test_5";
					break;
				case "combat test 6":
				case "1.16_combat-0":
					text2 = "Java版Combat_Test_6";
					break;
				case "combat test 7c":
				case "1.16_combat-3":
					text2 = "Java版Combat_Test_7c";
					break;
				case "combat test 8b":
				case "1.16_combat-5":
					text2 = "Java版Combat_Test_8b";
					break;
				case "combat test 8c":
				case "1.16_combat-6":
					text2 = "Java版Combat_Test_8c";
					break;
				case "1.0.0-rc2-2":
					text2 = "Java版RC2";
					break;
				default:
					if (StringExtensions.StartsWithF(text, "1.19_deep_dark_experimental_snapshot-", false) || StringExtensions.StartsWithF(text, "1_19_deep_dark_experimental_snapshot-", false))
					{
						text2 = text.Replace("1_19", "1.19").Replace("1.19_deep_dark_experimental_snapshot-", "Java版Deep_Dark_Experimental_Snapshot_");
						break;
					}
					if (Operators.CompareString(text, "b1.9-pre6", TextCompare: false) == 0)
					{
						text2 = "Java版Beta_1.9_Prerelease_6";
						break;
					}
					if (text.Contains("b1.9"))
					{
						text2 = "Java版Beta_1.9_Prerelease";
						break;
					}
					if ((string)InstanceJson[(object)"type"] == "release" || (string)InstanceJson[(object)"type"] == "snapshot" || (string)InstanceJson[(object)"type"] == "special")
					{
						text2 = (text.Contains("w") ? "" : "Java版") + text.Replace(" Pre-Release ", "-pre");
						break;
					}
					if (StringExtensions.StartsWithF(text, "b", false))
					{
						text2 = "Java版" + text.TrimEnd('a', 'b', 'c', 'd', 'e').Replace("b", "Beta_");
						break;
					}
					if (StringExtensions.StartsWithF(text, "a", false))
					{
						text2 = "Java版" + text.TrimEnd('a', 'b', 'c', 'd', 'e').Replace("a", "Alpha_v");
						break;
					}
					switch (text)
					{
					case "inf-20100618":
						text2 = "Java版Infdev_20100618";
						break;
					default:
						if (!text.Contains("生存测试"))
						{
							if (StringExtensions.StartsWithF(text, "c0.31", false))
							{
								text2 = "Java版Indev_0.31_20100130";
								break;
							}
							if (StringExtensions.StartsWithF(text, "c", false))
							{
								text2 = "Java版" + text.Replace("c", "Classic_");
								break;
							}
							if (StringExtensions.StartsWithF(text, "rd-", false))
							{
								text2 = "Java版Pre-classic_" + text;
								break;
							}
							Logger.Error($"未知的版本格式：{text}。", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							return;
						}
						goto case "c0.30_01c";
					case "c0.30_01c":
					case "c0.30_survival":
						text2 = "Java版Classic_0.30（生存模式）";
						break;
					}
					break;
				}
				break;
			}
			goto case "combat test 1";
		case "combat test 1":
			text2 = "Java版1.14.3_-_Combat_Test";
			break;
		}
		ModBase.OpenWebsite("https://zh.minecraft.wiki/w/" + text2.Replace("_experimental-snapshot-", "-exp"));
	}

	private static void McDownloadOptiFineSave(ModDownload.DlOptiFineListEntry DownloadInfo)
	{
		try
		{
			string text = Dialogs.SaveFile("选择保存位置", DownloadInfo.FileName, (string)null, (IEnumerable<ValueTuple<string, string>>)new(string, string)[1] { ("jar", "OptiFine Jar") });
			if (text == null)
			{
				return;
			}
			foreach (ModLoader.LoaderBase item in ((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).ToList())
			{
				if (Operators.CompareString(item.Name, $"OptiFine {DownloadInfo.DisplayName} 下载", TextCompare: false) == 0)
				{
					ModMain.Hint("该版本正在下载中！", ModMain.HintType.Red);
					return;
				}
			}
			ModLoader.LoaderCombo<ModDownload.DlOptiFineListEntry> loaderCombo = new ModLoader.LoaderCombo<ModDownload.DlOptiFineListEntry>("OptiFine " + DownloadInfo.DisplayName + " 下载", McDownloadOptiFineSaveLoader(DownloadInfo, text));
			loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
			loaderCombo.Start(DownloadInfo);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.BtnExtraDownload.Ribble();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "开始 OptiFine 下载失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			ProjectData.ClearProjectError();
		}
	}

	private static void McDownloadOptiFineInstall(string BaseMcFolderHome, string Target, ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task, bool UseJavaWrapper)
	{
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		_Closure_0024__12_002D2 arg = new _Closure_0024__12_002D2(arg);
		arg._0024VB_0024Local_Task = Task;
		ValueRange<Version> val = ValueRange<Version>.AtLeast(new Version(21, 0));
		checked
		{
			try
			{
				ZipArchive val2 = FileUtils.OpenZip(Target, default(CancellationToken));
				try
				{
					using Stream input = (val2.GetEntry("optifine/Installer.class") ?? throw new FileNotFoundException("未找到 optifine/Installer.class")).Open();
					using BinaryReader binaryReader = new BinaryReader(input);
					byte[] array = binaryReader.ReadBytes(8);
					if (array.Length < 8)
					{
						throw new IOException("Installer.class 长度不足");
					}
					if (array[0] != 202 || array[1] != 254 || array[2] != 186 || array[3] != 190)
					{
						throw new InvalidDataException("Installer.class 文件头无效");
					}
					int num = array[6] * 256 + array[7];
					if (num < 49)
					{
						throw new InvalidDataException("Installer.class 版本过低");
					}
					if (num > 100)
					{
						throw new InvalidDataException("Installer.class 版本过高");
					}
					val = ValueRange<Version>.AtLeast(new Version(num - 44, 0));
					Logger.Info($"OptiFine 安装器 class 版本：{num}，需要 Java {val}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				finally
				{
					((IDisposable)val2)?.Dispose();
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "读取 OptiFine 安装器 Java 版本失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
			Java val3 = ModJava.SelectOrDownloadJava(OneOf<ValueRange<Version>, ModMinecraft.McInstance>.op_Implicit(val), TryFixOrHint: true, arg._0024VB_0024Local_Task.CreateCancellationToken(), arg._0024VB_0024Local_Task.CreateSyncProgressProvider(0.0, 0.2));
			if (arg._0024VB_0024Local_Task.IsCanceled)
			{
				return;
			}
			if (val3 == null)
			{
				throw new OperationCanceledException();
			}
			string text = ((!UseJavaWrapper || Settings.Get<bool>("LaunchAdvanceDisableJLW")) ? $"-Duser.home=\"{PathUtils.RemoveSlashSuffix(BaseMcFolderHome)}\" -cp \"{Target}\" optifine.Installer" : string.Format("-Doolloo.jlw.tmpdir=\"{0}\" -Duser.home=\"{1}\" -cp \"{2}\" -jar \"{3}\" optifine.Installer", PathUtils.RemoveSlashSuffix(ModBase.PathPure.Value), PathUtils.RemoveSlashSuffix(BaseMcFolderHome), Target, LaunchUtils.ExtractPatch("Launch\\JavaWrapper.jar", ModBase.PathPure.Value)));
			if (val3.Version.Major >= 9)
			{
				text = "--add-exports cpw.mods.bootstraplauncher/cpw.mods.bootstraplauncher=ALL-UNNAMED " + text;
			}
			object installSyncLock = InstallSyncLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(installSyncLock);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(installSyncLock, ref lockTaken);
				_Closure_0024__12_002D1 arg2 = new _Closure_0024__12_002D1(arg2);
				arg2._0024VB_0024NonLocal__0024VB_0024Closure_2 = arg;
				ProcessStartInfo processStartInfo = new ProcessStartInfo
				{
					FileName = val3.JavaExePath,
					Arguments = text,
					UseShellExecute = false,
					CreateNoWindow = true,
					RedirectStandardError = true,
					RedirectStandardOutput = true,
					WorkingDirectory = PathUtils.ToShortPath(BaseMcFolderHome)
				};
				if (processStartInfo.EnvironmentVariables.ContainsKey("appdata"))
				{
					processStartInfo.EnvironmentVariables["appdata"] = BaseMcFolderHome;
				}
				else
				{
					processStartInfo.EnvironmentVariables.Add("appdata", BaseMcFolderHome);
				}
				Logger.Info($"开始安装 OptiFine：{Target}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				arg2._0024VB_0024Local_TotalLength = 0;
				arg2._0024VB_0024Local_process = new Process
				{
					StartInfo = processStartInfo
				};
				arg2._0024VB_0024Local_LastResult = "";
				_Closure_0024__12_002D0 arg3 = new _Closure_0024__12_002D0(arg3);
				arg3._0024VB_0024NonLocal__0024VB_0024Closure_3 = arg2;
				arg3._0024VB_0024Local_outputWaitHandle = new AutoResetEvent(initialState: false);
				try
				{
					_Closure_0024__12_002D3 arg4 = default(_Closure_0024__12_002D3);
					_Closure_0024__12_002D3 CS_0024_003C_003E8__locals0 = new _Closure_0024__12_002D3(arg4);
					CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4 = arg3;
					CS_0024_003C_003E8__locals0._0024VB_0024Local_errorWaitHandle = new AutoResetEvent(initialState: false);
					try
					{
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.OutputDataReceived += [SpecialName] (object a0, DataReceivedEventArgs a1) =>
						{
							CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._Lambda_0024__0(RuntimeHelpers.GetObjectValue(a0), a1);
						};
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.ErrorDataReceived += [SpecialName] (object a0, DataReceivedEventArgs a1) =>
						{
							CS_0024_003C_003E8__locals0._Lambda_0024__1(RuntimeHelpers.GetObjectValue(a0), a1);
						};
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.Start();
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.BeginOutputReadLine();
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.BeginErrorReadLine();
						while (!CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.HasExited)
						{
							Thread.Sleep(10);
						}
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024Local_outputWaitHandle.WaitOne(10000);
						CS_0024_003C_003E8__locals0._0024VB_0024Local_errorWaitHandle.WaitOne(10000);
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.Dispose();
						if (CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_TotalLength < 1000 || CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResult.Contains("at "))
						{
							throw new Exception("安装器运行出错，末行为 " + CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResult);
						}
					}
					finally
					{
						if (CS_0024_003C_003E8__locals0._0024VB_0024Local_errorWaitHandle != null)
						{
							((IDisposable)CS_0024_003C_003E8__locals0._0024VB_0024Local_errorWaitHandle).Dispose();
						}
					}
				}
				finally
				{
					if (arg3._0024VB_0024Local_outputWaitHandle != null)
					{
						((IDisposable)arg3._0024VB_0024Local_outputWaitHandle).Dispose();
					}
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(installSyncLock);
				}
			}
		}
	}

	private static List<ModLoader.LoaderBase> McDownloadOptiFineLoader(ModDownload.DlOptiFineListEntry DownloadInfo, string McFolder, ModLoader.LoaderCombo<string> ClientDownloadLoader, string ClientFolder)
	{
		_Closure_0024__13_002D0 arg = default(_Closure_0024__13_002D0);
		_Closure_0024__13_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__13_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo = DownloadInfo;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder = McFolder;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ClientDownloadLoader = ClientDownloadLoader;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ClientFolder = ClientFolder;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Id = CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.InstanceName;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_Id + "\\";
		bool flag = CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit.Contains("w") || McVersion.VersionToDrop(CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit) >= 140;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Target = (flag ? $"{ModMain.RequestTaskTempFolder(RequireNonSpace: true)}OptiFine.jar" : string.Format("{0}libraries\\optifine\\OptiFine\\{1}\\{2}", CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder, PathUtils.GetFileNameWithoutExtension(CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.FileName).Replace("OptiFine_", "").Replace("preview_", ""), CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.FileName.Replace("OptiFine_", "OptiFine-").Replace("preview_", "")));
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取 OptiFine 主文件下载地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
		{
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Expected O, but got Unknown
			List<string> list2 = new List<string>();
			string text4 = CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit;
			if (Operators.CompareString(text4, "1.8", TextCompare: false) == 0 || Operators.CompareString(text4, "1.9", TextCompare: false) == 0)
			{
				text4 += ".0";
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.IsPreview)
			{
				list2.Add("https://bmclapi2.bangbang93.com/optifine/" + text4 + "/HD_U_" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.DisplayName.Replace(CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + " ", "").Replace(" ", "/"));
			}
			else
			{
				list2.Add("https://bmclapi2.bangbang93.com/optifine/" + text4 + "/HD_U/" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.DisplayName.Replace(CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + " ", ""));
			}
			try
			{
				string text5 = ModNet.NetRequestByClientRetry("https://optifine.net/adloadx?f=" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.FileName, null, null, null, null, "text/html", null, RequireJson: false, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), SimulateBrowserHeaders: true);
				Task.Progress = 0.8;
				list2.Add("https://optifine.net/" + StringExtensions.RegexSearch(text5, "downloadx\\?f=[^\"']+", RegexOptions.None).First());
				Logger.Info($"OptiFine {CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.DisplayName} 官方下载地址：{list2.Last()}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Logger.Warn(ex6, $"获取 OptiFine {CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.DisplayName} 官方下载地址失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
			Task.Output = new List<ModNet.NetFile>
			{
				new ModNet.NetFile(list2.ToArray(), CS_0024_003C_003E8__locals0._0024VB_0024Local_Target, new FileChecker
				{
					MinSize = 307200L
				})
			};
		})
		{
			ProgressWeight = 8.0
		});
		list.Add(new ModNet.LoaderDownload("下载 OptiFine 主文件", new List<ModNet.NetFile>())
		{
			ProgressWeight = 8.0
		});
		list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>("等待原版下载", [SpecialName] (ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task) =>
		{
			List<ModLoader.LoaderBase> source = (from l in CS_0024_003C_003E8__locals0._0024VB_0024Local_ClientDownloadLoader.GetLoaderList()
				where Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) == 0 || Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0
				where l.State != ModBase.LoadState.Finished
				select l).ToList();
			if (source.Any())
			{
				Logger.Info("OptiFine 安装正在等待原版文件下载完成", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			while (source.Any() && !Task.IsCanceled)
			{
				source = source.Where((_Closure_0024__._0024I13_002D4 == null) ? (_Closure_0024__._0024I13_002D4 = [SpecialName] (ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished) : _Closure_0024__._0024I13_002D4).ToList();
				Thread.Sleep(50);
			}
			if (!Task.IsCanceled)
			{
				object vanillaSyncLock = VanillaSyncLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(vanillaSyncLock);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(vanillaSyncLock, ref lockTaken);
					string lastPart = PathUtils.GetLastPart(CS_0024_003C_003E8__locals0._0024VB_0024Local_ClientFolder);
					DirectoryUtils.Create(CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit);
					if (!FileUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + "\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + ".json"))
					{
						FileUtils.Copy($"{CS_0024_003C_003E8__locals0._0024VB_0024Local_ClientFolder}{lastPart}.json", $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder}versions\\{CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit}\\{CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit}.json");
					}
					if (!FileUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + "\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + ".jar"))
					{
						FileUtils.Copy($"{CS_0024_003C_003E8__locals0._0024VB_0024Local_ClientFolder}{lastPart}.jar", $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder}versions\\{CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit}\\{CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit}.jar");
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(vanillaSyncLock);
					}
				}
			}
		})
		{
			ProgressWeight = 0.1,
			Show = false
		});
		if (flag)
		{
			Logger.Info($"检测为新版 OptiFine：{CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>("安装 OptiFine（方式 A）", [SpecialName] (ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task) =>
			{
				string text2 = ModMain.RequestTaskTempFolder(RequireNonSpace: true);
				string text3 = text2 + ".minecraft\\";
				try
				{
					if (DirectoryUtils.Exists(text3 + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit))
					{
						DirectoryUtils.Delete(text3 + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit, false);
					}
					DirectoryUtils.Create(text3 + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + "\\");
					ModMinecraft.McFolderLauncherProfilesJsonCreate(text3);
					FileUtils.Copy(CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + "\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + ".json", text3 + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + "\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + ".json");
					FileUtils.Copy(CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + "\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + ".jar", text3 + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + "\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + ".jar");
					Task.Progress = 0.06;
					bool flag2 = true;
					while (true)
					{
						try
						{
							McDownloadOptiFineInstall(text2, CS_0024_003C_003E8__locals0._0024VB_0024Local_Target, Task, flag2);
						}
						catch (Exception ex2)
						{
							ProjectData.SetProjectError(ex2);
							Exception ex3 = ex2;
							if (!flag2)
							{
								throw new Exception("运行 OptiFine 安装器失败", ex3);
							}
							Logger.Warn(ex3, "使用 JavaWrapper 安装 OptiFine 失败，将不使用 JavaWrapper 并重试", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							flag2 = false;
							ProjectData.ClearProjectError();
							continue;
						}
						break;
					}
					Task.Progress = 0.96;
					FileUtils.Delete(text3 + "launcher_profiles.json", false);
					DirectoryUtils.Copy(text3, CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder);
					Task.Progress = 0.98;
					FileUtils.Delete(CS_0024_003C_003E8__locals0._0024VB_0024Local_Target, false);
					DirectoryUtils.Delete(text2, false);
				}
				catch (Exception ex4)
				{
					ProjectData.SetProjectError(ex4);
					Exception innerException2 = ex4;
					throw new Exception("安装 OptiFine（方式 A）失败", innerException2);
				}
			})
			{
				ProgressWeight = 8.0
			});
		}
		else
		{
			Logger.Info($"检测为旧版 OptiFine：{CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>("安装 OptiFine（方式 B）", [SpecialName] (ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task) =>
			{
				try
				{
					FileUtils.Copy(CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + "\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + ".jar", CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + CS_0024_003C_003E8__locals0._0024VB_0024Local_Id + ".jar");
					Task.Progress = 0.7;
					ModMinecraft.McInstance mcInstance = new ModMinecraft.McInstance(CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit);
					string text = "{\r\n    \"id\": \"" + CS_0024_003C_003E8__locals0._0024VB_0024Local_Id + "\",\r\n    \"inheritsFrom\": \"" + CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.Inherit + "\",\r\n    \"time\": \"" + ((Operators.CompareString(CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.ReleaseTime, "", TextCompare: false) == 0) ? mcInstance.ReleaseTime.ToString("yyyy'-'MM'-'dd") : CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.ReleaseTime.Replace("/", "-")) + "T23:33:33+08:00\",\r\n    \"releaseTime\": \"" + ((Operators.CompareString(CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.ReleaseTime, "", TextCompare: false) == 0) ? mcInstance.ReleaseTime.ToString("yyyy'-'MM'-'dd") : CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.ReleaseTime.Replace("/", "-")) + "T23:33:33+08:00\",\r\n    \"type\": \"release\",\r\n    \"libraries\": [\r\n        {\"name\": \"optifine:OptiFine:" + PathUtils.GetFileNameWithoutExtension(CS_0024_003C_003E8__locals0._0024VB_0024Local_DownloadInfo.FileName).Replace("OptiFine_", "").Replace("preview_", "") + "\"},\r\n        {\"name\": \"net.minecraft:launchwrapper:1.12\"}\r\n    ],\r\n    \"mainClass\": \"net.minecraft.launchwrapper.Launch\",";
					Task.Progress = 0.8;
					text = ((!mcInstance.IsOldJson) ? (text + "\r\n    \"minimumLauncherVersion\": \"21\",\r\n    \"arguments\": {\r\n        \"game\": [\r\n            \"--tweakClass\",\r\n            \"optifine.OptiFineTweaker\"\r\n        ]\r\n    }\r\n}") : (text + "\r\n    \"minimumLauncherVersion\": 18,\r\n    \"minecraftArguments\": \"" + mcInstance.JsonObject["minecraftArguments"].ToString() + "  --tweakClass optifine.OptiFineTweaker\"\r\n}"));
					FileUtils.Write(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + CS_0024_003C_003E8__locals0._0024VB_0024Local_Id + ".json", text, (Encoding)null);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("安装 OptiFine（方式 B）失败", innerException);
				}
			})
			{
				ProgressWeight = 1.0
			});
		}
		return list;
	}

	private static List<ModLoader.LoaderBase> McDownloadOptiFineSaveLoader(ModDownload.DlOptiFineListEntry DownloadInfo, string TargetFolder)
	{
		return new List<ModLoader.LoaderBase>
		{
			new ModLoader.LoaderTask<ModDownload.DlOptiFineListEntry, List<ModNet.NetFile>>("获取 OptiFine 下载地址", [SpecialName] (ModLoader.LoaderTask<ModDownload.DlOptiFineListEntry, List<ModNet.NetFile>> Task) =>
			{
				//IL_01af: Unknown result type (might be due to invalid IL or missing references)
				//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
				//IL_01c6: Expected O, but got Unknown
				List<string> list = new List<string>();
				string text = DownloadInfo.Inherit;
				if (Operators.CompareString(text, "1.8", TextCompare: false) == 0 || Operators.CompareString(text, "1.9", TextCompare: false) == 0)
				{
					text += ".0";
				}
				if (DownloadInfo.IsPreview)
				{
					list.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U_" + DownloadInfo.DisplayName.Replace(DownloadInfo.Inherit + " ", "").Replace(" ", "/"));
				}
				else
				{
					list.Add("https://bmclapi2.bangbang93.com/optifine/" + text + "/HD_U/" + DownloadInfo.DisplayName.Replace(DownloadInfo.Inherit + " ", ""));
				}
				try
				{
					string text2 = ModNet.NetRequestByClientRetry("https://optifine.net/adloadx?f=" + DownloadInfo.FileName, null, null, null, null, "text/html", null, RequireJson: false, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), SimulateBrowserHeaders: true);
					Task.Progress = 0.8;
					list.Add("https://optifine.net/" + StringExtensions.RegexSearch(text2, "downloadx\\?f=[^\"']+", RegexOptions.None).First());
					Logger.Info($"OptiFine {DownloadInfo.DisplayName} 官方下载地址：{list.Last()}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, $"获取 OptiFine {DownloadInfo.DisplayName} 官方下载地址失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					ProjectData.ClearProjectError();
				}
				Task.Progress = 0.9;
				Task.Output = new List<ModNet.NetFile>
				{
					new ModNet.NetFile(list.ToArray(), TargetFolder, new FileChecker
					{
						MinSize = 65536L
					})
				};
			})
			{
				ProgressWeight = 6.0
			},
			new ModNet.LoaderDownload("下载 OptiFine 主文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 10.0,
				Block = true
			}
		};
	}

	public static MyVirtualizingElement<MyListItem> OptiFineDownloadListItem(ModDownload.DlOptiFineListEntry Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
	{
		return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
		{
			MyListItem myListItem = new MyListItem
			{
				Title = Entry.DisplayName,
				SnapsToDevicePixels = true,
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Entry,
				Info = (Entry.IsPreview ? "测试版" : "正式版") + ((Operators.CompareString(Entry.ReleaseTime, "", TextCompare: false) == 0) ? "" : ("，发布于 " + Entry.ReleaseTime)) + ((Entry.RequiredForgeVersion == null) ? "，不兼容 Forge" : ((Operators.CompareString(Entry.RequiredForgeVersion, "", TextCompare: false) == 0) ? "" : ("，兼容 Forge " + Entry.RequiredForgeVersion))),
				Logo = ModBase.PathImage + "Blocks/GrassPath.png"
			};
			myListItem.Click += OnClick;
			if (IsSaveOnly)
			{
				myListItem.ContentHandler = OptiFineSaveContMenuBuild;
			}
			else
			{
				myListItem.ContentHandler = OptiFineContMenuBuild;
			}
			return myListItem;
		})
		{
			Height = 42.0
		};
	}

	private static void OptiFineSaveContMenuBuild(object sender, EventArgs e)
	{
		MyIconButton myIconButton = new MyIconButton
		{
			LogoScale = 1.05,
			Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
			ToolTip = "更新日志"
		};
		myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			OptiFineLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		NewLateBinding.LateSet(sender, null, "Buttons", new object[1] { new MyIconButton[1] { myIconButton } }, null, null);
	}

	private static void OptiFineContMenuBuild(object sender, EventArgs e)
	{
		MyIconButton myIconButton = new MyIconButton
		{
			Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
			ToolTip = "另存为"
		};
		myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			OptiFineSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		MyIconButton myIconButton2 = new MyIconButton
		{
			LogoScale = 1.05,
			Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
			ToolTip = "更新日志"
		};
		myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			OptiFineLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		NewLateBinding.LateSet(sender, null, "Buttons", new object[1] { new MyIconButton[2] { myIconButton, myIconButton2 } }, null, null);
	}

	private static void OptiFineLog_Click(object sender, RoutedEventArgs e)
	{
		ModDownload.DlOptiFineListEntry dlOptiFineListEntry = ((NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null) ? ((ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)) : ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null))));
		ModBase.OpenWebsite("https://optifine.net/changelog?f=" + dlOptiFineListEntry.FileName);
	}

	public static void OptiFineSave_Click(object sender, RoutedEventArgs e)
	{
		ModDownload.DlOptiFineListEntry downloadInfo = ((NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null) ? ((ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)) : ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlOptiFineListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null))));
		McDownloadOptiFineSave(downloadInfo);
	}

	private static void McDownloadLiteLoaderSave(ModDownload.DlLiteLoaderListEntry DownloadInfo)
	{
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		try
		{
			string inherit = DownloadInfo.Inherit;
			string text = Dialogs.SaveFile("选择保存位置", DownloadInfo.FileName.Replace("-SNAPSHOT", ""), (string)null, (IEnumerable<ValueTuple<string, string>>)new(string, string)[1] { ("jar", "LiteLoader 安装器") });
			if (text == null)
			{
				return;
			}
			foreach (ModLoader.LoaderBase item in ((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).ToList())
			{
				if (Operators.CompareString(item.Name, $"LiteLoader {inherit} 下载", TextCompare: false) == 0)
				{
					ModMain.Hint("该版本正在下载中！", ModMain.HintType.Red);
					return;
				}
			}
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			List<string> list2 = new List<string>();
			if (DownloadInfo.IsLegacy)
			{
				switch (DownloadInfo.Inherit)
				{
				case "1.7.10":
					list2.Add("https://dl.liteloader.com/redist/1.7.10/liteloader-installer-1.7.10-04.jar");
					break;
				case "1.7.2":
					list2.Add("https://dl.liteloader.com/redist/1.7.2/liteloader-installer-1.7.2-04.jar");
					break;
				case "1.6.4":
					list2.Add("https://dl.liteloader.com/redist/1.6.4/liteloader-installer-1.6.4-01.jar");
					break;
				case "1.6.2":
					list2.Add("https://dl.liteloader.com/redist/1.6.2/liteloader-installer-1.6.2-04.jar");
					break;
				case "1.5.2":
					list2.Add("https://dl.liteloader.com/redist/1.5.2/liteloader-installer-1.5.2-01.jar");
					break;
				default:
					throw new NotSupportedException("未知的 Minecraft 版本（" + DownloadInfo.Inherit + "）");
				}
			}
			else
			{
				list2.Add("http://jenkins.liteloader.com/job/LiteLoaderInstaller%20" + DownloadInfo.Inherit + "/lastSuccessfulBuild/artifact/" + ((Operators.CompareString(DownloadInfo.Inherit, "1.8", TextCompare: false) == 0) ? "ant/dist/" : "build/libs/") + DownloadInfo.FileName);
			}
			list.Add(new ModNet.LoaderDownload("下载主文件", new List<ModNet.NetFile>
			{
				new ModNet.NetFile(list2.ToArray(), text, new FileChecker
				{
					MinSize = 1048576L
				})
			})
			{
				ProgressWeight = 15.0
			});
			ModLoader.LoaderCombo<ModDownload.DlLiteLoaderListEntry> loaderCombo = new ModLoader.LoaderCombo<ModDownload.DlLiteLoaderListEntry>("LiteLoader " + inherit + " 安装器下载", list);
			loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
			loaderCombo.Start(DownloadInfo);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.BtnExtraDownload.Ribble();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "开始 LiteLoader 安装器下载失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			ProjectData.ClearProjectError();
		}
	}

	private static List<ModLoader.LoaderBase> McDownloadLiteLoaderLoader(ModDownload.DlLiteLoaderListEntry DownloadInfo, string McFolder)
	{
		return new List<ModLoader.LoaderBase>
		{
			new ModLoader.LoaderTask<string, string>("安装 LiteLoader", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
			{
				//IL_002c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0032: Expected O, but got Unknown
				//IL_0110: Unknown result type (might be due to invalid IL or missing references)
				try
				{
					string text = DownloadInfo.Inherit + "-LiteLoader";
					string text2 = McFolder + "versions\\" + text + "\\";
					JObject val = new JObject();
					val.Add("id", JToken.op_Implicit(text));
					val.Add("time", JToken.op_Implicit(DateTime.ParseExact(DownloadInfo.ReleaseTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)));
					val.Add("releaseTime", JToken.op_Implicit(DateTime.ParseExact(DownloadInfo.ReleaseTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)));
					val.Add("type", JToken.op_Implicit("release"));
					val.Add("arguments", StringExtensions.DeserializeJson("{\"game\":[\"--tweakClass\",\"" + DownloadInfo.JsonToken[(object)"tweakClass"].ToString() + "\"]}"));
					val.Add("libraries", DownloadInfo.JsonToken[(object)"libraries"]);
					((JContainer)val["libraries"]).Add((object)StringExtensions.DeserializeJson("{\"name\": \"com.mumfrey:liteloader:" + DownloadInfo.JsonToken[(object)"version"].ToString() + "\",\"url\": \"https://dl.liteloader.com/versions/\"}"));
					val.Add("mainClass", JToken.op_Implicit("net.minecraft.launchwrapper.Launch"));
					val.Add("minimumLauncherVersion", JToken.op_Implicit(18));
					val.Add("inheritsFrom", JToken.op_Implicit(DownloadInfo.Inherit));
					val.Add("jar", JToken.op_Implicit(DownloadInfo.Inherit));
					FileUtils.Write(text2 + text + ".json", ((JToken)val).ToString(), (Encoding)null);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("安装新 LiteLoader 版本失败", innerException);
				}
			})
			{
				ProgressWeight = 1.0
			}
		};
	}

	public static MyVirtualizingElement<MyListItem> LiteLoaderDownloadListItem(ModDownload.DlLiteLoaderListEntry Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
	{
		return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
		{
			MyListItem myListItem = new MyListItem
			{
				Title = Entry.Inherit,
				SnapsToDevicePixels = true,
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Entry,
				Info = (Entry.IsPreview ? "测试版" : "稳定版") + ((Operators.CompareString(Entry.ReleaseTime, "", TextCompare: false) == 0) ? "" : ("，发布于 " + Entry.ReleaseTime)),
				Logo = ModBase.PathImage + "Blocks/Egg.png"
			};
			myListItem.Click += OnClick;
			if (IsSaveOnly)
			{
				myListItem.ContentHandler = LiteLoaderSaveContMenuBuild;
			}
			else
			{
				myListItem.ContentHandler = LiteLoaderContMenuBuild;
			}
			return myListItem;
		})
		{
			Height = 42.0
		};
	}

	private static void LiteLoaderSaveContMenuBuild(MyListItem sender, EventArgs e)
	{
		if (Conversions.ToBoolean(NewLateBinding.LateGet(sender.Tag, null, "IsLegacy", new object[0], null, null, null)))
		{
			sender.Buttons = new MyIconButton[0];
			return;
		}
		MyIconButton myIconButton = new MyIconButton
		{
			Logo = "M384 128h640v128H384zM160 192m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 448h640v128H384zM160 512m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 768h640v128H384zM160 832m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0Z",
			ToolTip = "查看全部版本",
			Tag = sender
		};
		myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			LiteLoaderAll_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		sender.Buttons = new MyIconButton[1] { myIconButton };
	}

	private static void LiteLoaderContMenuBuild(MyListItem sender, EventArgs e)
	{
		MyIconButton myIconButton = new MyIconButton
		{
			Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
			ToolTip = "保存安装器",
			Tag = sender
		};
		myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			LiteLoaderSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		if (Conversions.ToBoolean(NewLateBinding.LateGet(sender.Tag, null, "IsLegacy", new object[0], null, null, null)))
		{
			sender.Buttons = new MyIconButton[1] { myIconButton };
			return;
		}
		MyIconButton myIconButton2 = new MyIconButton
		{
			Logo = "M384 128h640v128H384zM160 192m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 448h640v128H384zM160 512m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 768h640v128H384zM160 832m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0Z",
			ToolTip = "查看全部版本",
			Tag = sender
		};
		myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			LiteLoaderAll_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		sender.Buttons = new MyIconButton[2] { myIconButton, myIconButton2 };
	}

	private static void LiteLoaderAll_Click(object sender, RoutedEventArgs e)
	{
		ModDownload.DlLiteLoaderListEntry dlLiteLoaderListEntry = ((!(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) is ModDownload.DlLiteLoaderListEntry)) ? ((ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
		ModBase.OpenWebsite("https://jenkins.liteloader.com/view/" + dlLiteLoaderListEntry.Inherit);
	}

	public static void LiteLoaderSave_Click(object sender, RoutedEventArgs e)
	{
		ModDownload.DlLiteLoaderListEntry downloadInfo = ((!(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) is ModDownload.DlLiteLoaderListEntry)) ? ((ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlLiteLoaderListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
		McDownloadLiteLoaderSave(downloadInfo);
	}

	public static void McDownloadForgelikeSave(ModDownload.DlForgelikeEntry Info)
	{
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		try
		{
			string text = Dialogs.SaveFile("选择保存位置", $"{Info.LoaderName}-{Info.Inherit}-{Info.VersionName}.{Info.FileExtension}", (string)null, (IEnumerable<ValueTuple<string, string>>)new(string, string)[1] { (Info.FileExtension, $"{Info.LoaderName} 安装器") });
			if (text == null)
			{
				return;
			}
			string text2 = $"{Info.LoaderName} {Info.Inherit} - {Info.VersionName}";
			foreach (ModLoader.LoaderBase item in ((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).ToList())
			{
				if (Operators.CompareString(item.Name, $"{text2} 下载", TextCompare: false) == 0)
				{
					ModMain.Hint("该版本正在下载中！", ModMain.HintType.Red);
					return;
				}
			}
			List<ModNet.NetFile> list = new List<ModNet.NetFile>();
			if (Info.IsNeoForge)
			{
				string text3 = ((ModDownload.DlNeoForgeListEntry)Info).UrlBase + "-installer.jar";
				list.Add(new ModNet.NetFile(new string[2]
				{
					text3.Replace("maven.neoforged.net/releases", "bmclapi2.bangbang93.com/maven"),
					text3
				}, text, new FileChecker
				{
					MinSize = 65536L
				}));
			}
			else
			{
				ModDownload.DlForgeVersionEntry dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)Info;
				list.Add(new ModNet.NetFile(new string[2]
				{
					$"https://bmclapi2.bangbang93.com/maven/net/minecraftforge/forge/{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.FileVersion}/forge-{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.FileVersion}-{dlForgeVersionEntry.Category}.{dlForgeVersionEntry.FileExtension}",
					$"https://files.minecraftforge.net/maven/net/minecraftforge/forge/{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.FileVersion}/forge-{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.FileVersion}-{dlForgeVersionEntry.Category}.{dlForgeVersionEntry.FileExtension}"
				}, text, new FileChecker
				{
					MinSize = 65536L,
					Hash = dlForgeVersionEntry.Hash
				}));
			}
			List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
			list2.Add(new ModNet.LoaderDownload("下载主文件", list)
			{
				ProgressWeight = 6.0
			});
			ModLoader.LoaderCombo<ModDownload.DlForgelikeEntry> loaderCombo = new ModLoader.LoaderCombo<ModDownload.DlForgelikeEntry>(text2 + " 下载", list2);
			loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
			loaderCombo.Start(Info);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.BtnExtraDownload.Ribble();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, $"开始 {Info.LoaderName} 安装器下载失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			ProjectData.ClearProjectError();
		}
	}

	private static void ForgelikeInjector(string Target, ModLoader.LoaderTask<bool, bool> Task, string McFolder, bool UseJavaWrapper, bool IsNeoForge)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		_Closure_0024__28_002D2 arg = new _Closure_0024__28_002D2(arg);
		arg._0024VB_0024Local_Task = Task;
		Java val = ModJava.SelectOrDownloadJava(OneOf<ValueRange<Version>, ModMinecraft.McInstance>.op_Implicit(ValueRange<Version>.AtLeast(new Version(8, 0, 60))), TryFixOrHint: true, arg._0024VB_0024Local_Task.CreateCancellationToken(), arg._0024VB_0024Local_Task.CreateSyncProgressProvider(0.0, 0.2));
		if (arg._0024VB_0024Local_Task.IsCanceled)
		{
			return;
		}
		if (val == null)
		{
			throw new OperationCanceledException();
		}
		string text = ((!UseJavaWrapper || Settings.Get<bool>("LaunchAdvanceDisableJLW")) ? $"-cp \"{ModBase.PathTemp}Cache\\forge_installer.jar;{Target}\" com.bangbang93.ForgeInstaller \"{McFolder}" : string.Format("-Doolloo.jlw.tmpdir=\"{0}\" -cp \"{1}Cache\\forge_installer.jar;{2}\" -jar \"{3}\" com.bangbang93.ForgeInstaller \"{4}", PathUtils.RemoveSlashSuffix(ModBase.PathPure.Value), ModBase.PathTemp, Target, LaunchUtils.ExtractPatch("Launch\\JavaWrapper.jar", ModBase.PathPure.Value), McFolder));
		if (val.Version.Major >= 9)
		{
			text = "--add-exports cpw.mods.bootstraplauncher/cpw.mods.bootstraplauncher=ALL-UNNAMED " + text;
		}
		object installSyncLock = InstallSyncLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(installSyncLock);
		bool lockTaken = false;
		checked
		{
			try
			{
				Monitor.Enter(installSyncLock, ref lockTaken);
				_Closure_0024__28_002D1 arg2 = new _Closure_0024__28_002D1(arg2);
				arg2._0024VB_0024NonLocal__0024VB_0024Closure_2 = arg;
				ProcessStartInfo startInfo = new ProcessStartInfo
				{
					FileName = val.JavaExePath,
					Arguments = text,
					UseShellExecute = false,
					CreateNoWindow = true,
					RedirectStandardError = true,
					RedirectStandardOutput = true
				};
				arg2._0024VB_0024Local_LoaderName = (IsNeoForge ? "NeoForge" : "Forge");
				Logger.Info($"开始安装 {arg2._0024VB_0024Local_LoaderName}：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				arg2._0024VB_0024Local_process = new Process
				{
					StartInfo = startInfo
				};
				arg2._0024VB_0024Local_LastResults = new Queue<string>();
				_Closure_0024__28_002D0 arg3 = new _Closure_0024__28_002D0(arg3);
				arg3._0024VB_0024NonLocal__0024VB_0024Closure_3 = arg2;
				arg3._0024VB_0024Local_outputWaitHandle = new AutoResetEvent(initialState: false);
				try
				{
					_Closure_0024__28_002D3 arg4 = default(_Closure_0024__28_002D3);
					_Closure_0024__28_002D3 CS_0024_003C_003E8__locals0 = new _Closure_0024__28_002D3(arg4);
					CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4 = arg3;
					CS_0024_003C_003E8__locals0._0024VB_0024Local_errorWaitHandle = new AutoResetEvent(initialState: false);
					try
					{
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.OutputDataReceived += [SpecialName] (object a0, DataReceivedEventArgs a1) =>
						{
							CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._Lambda_0024__0(RuntimeHelpers.GetObjectValue(a0), a1);
						};
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.ErrorDataReceived += [SpecialName] (object a0, DataReceivedEventArgs a1) =>
						{
							CS_0024_003C_003E8__locals0._Lambda_0024__1(RuntimeHelpers.GetObjectValue(a0), a1);
						};
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.Start();
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.BeginOutputReadLine();
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.BeginErrorReadLine();
						while (!CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.HasExited)
						{
							Thread.Sleep(10);
						}
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024Local_outputWaitHandle.WaitOne(10000);
						CS_0024_003C_003E8__locals0._0024VB_0024Local_errorWaitHandle.WaitOne(10000);
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_process.Dispose();
						if (CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults.Reverse().Take(5).Any([SpecialName] (string l) => Operators.CompareString(l, "true", TextCompare: false) == 0))
						{
							return;
						}
						Logger.Warn(EnumerableExtensions.Join((IEnumerable)CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults, "\r\n"), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						string text2 = "";
						int num = Math.Max(0, CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults.Count - 5);
						int num2 = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults.Count - 1;
						for (int i = num; i <= num2; i++)
						{
							text2 = text2 + "\r\n" + CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LastResults.ElementAtOrDefault(i);
						}
						throw new Exception($"{CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_4._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024Local_LoaderName} 安装器出错，日志结束部分为：" + text2);
					}
					finally
					{
						if (CS_0024_003C_003E8__locals0._0024VB_0024Local_errorWaitHandle != null)
						{
							((IDisposable)CS_0024_003C_003E8__locals0._0024VB_0024Local_errorWaitHandle).Dispose();
						}
					}
				}
				finally
				{
					if (arg3._0024VB_0024Local_outputWaitHandle != null)
					{
						((IDisposable)arg3._0024VB_0024Local_outputWaitHandle).Dispose();
					}
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(installSyncLock);
				}
			}
		}
	}

	private static void ForgelikeInjectorLine(string Content, ModLoader.LoaderTask<bool, bool> Task)
	{
		switch (Content)
		{
		case "Extracting json":
			Task.Progress = 0.27;
			break;
		case "Downloading libraries":
			Task.Progress = 0.28;
			break;
		case "  File exists: Checksum validated.":
			Task.Progress += 0.003;
			break;
		case "Building Processors":
			Task.Progress = 0.38;
			break;
		case "Task: DOWNLOAD_MOJMAPS":
			Task.Progress = 0.4;
			break;
		case "Task: MERGE_MAPPING":
			Task.Progress = 0.5;
			break;
		case "Splitting: ":
			Task.Progress = 0.55;
			break;
		case "Parameter Annotations":
			Task.Progress = 0.6;
			break;
		case "Processing Complete":
			Task.Progress = 0.67;
			break;
		case "log: null":
			Task.Progress = 0.67;
			break;
		case "Sorting":
			Task.Progress = 0.8;
			break;
		case "Remapping final jar":
			Task.Progress = 0.85;
			break;
		case "Remapping jar... 50%":
			Task.Progress = 0.9;
			break;
		case "Remapping jar... 100%":
			Task.Progress = 0.95;
			break;
		case "Injecting profile":
			Task.Progress = 0.98;
			break;
		default:
			Logger.Trace(Content, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			return;
		}
		Logger.Info(Content, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
	}

	private static List<ModLoader.LoaderBase> McDownloadForgelikeLoader(bool IsNeoForge, string LoaderVersion, string NewInstanceName, string Inherit, ModDownload.DlForgelikeEntry Info, string McFolder, ModLoader.LoaderCombo<string> ClientDownloadLoader, string ClientFolder)
	{
		_Closure_0024__30_002D0 arg = default(_Closure_0024__30_002D0);
		_Closure_0024__30_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__30_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_IsNeoForge = IsNeoForge;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion = LoaderVersion;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstanceName = NewInstanceName;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Inherit = Inherit;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Info = Info;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder = McFolder;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ClientDownloadLoader = ClientDownloadLoader;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ClientFolder = ClientFolder;
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_IsNeoForge && CS_0024_003C_003E8__locals0._0024VB_0024Local_Info == null)
		{
			if (Operators.CompareString(CS_0024_003C_003E8__locals0._0024VB_0024Local_Inherit, "1.20.1", TextCompare: false) == 0 && !StringExtensions.StartsWithF(CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion, "1.20.1-", false))
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_Info = new ModDownload.DlNeoForgeListEntry("1.20.1-" + CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion);
			}
			else
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_Info = new ModDownload.DlNeoForgeListEntry(CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion);
			}
		}
		if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_IsNeoForge && StringExtensions.StartsWithF(CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion, "1.", false) && CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion.Contains("-"))
		{
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Inherit = StringExtensions.BeforeFirst(CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion, "-", false);
			CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion = StringExtensions.AfterLast(CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion, "-", false);
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName = (CS_0024_003C_003E8__locals0._0024VB_0024Local_IsNeoForge ? "NeoForge" : "Forge");
		CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress = ModMain.RequestTaskTempFolder(RequireNonSpace: true) + "forge_installer.jar";
		CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder}versions\\{CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstanceName}\\";
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Info == null)
		{
			list.Add(new ModLoader.LoaderTask<string, string>($"获取 {CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName} 详细信息", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
			{
				ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("McDownloadForgeLoader " + CS_0024_003C_003E8__locals0._0024VB_0024Local_Inherit, ModDownload.DlForgeVersionMain);
				loaderTask.WaitForExit(CS_0024_003C_003E8__locals0._0024VB_0024Local_Inherit);
				Task.Progress = 0.8;
				foreach (ModDownload.DlForgeVersionEntry item in loaderTask.Output)
				{
					if (McUtils.CompareVersion(item.Version.ToString(), CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion) == 0)
					{
						CS_0024_003C_003E8__locals0._0024VB_0024Local_Info = item;
						return;
					}
				}
				throw new Exception($"未能找到 {CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName} " + CS_0024_003C_003E8__locals0._0024VB_0024Local_Inherit + "-" + CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion + " 的详细信息！");
			})
			{
				ProgressWeight = 3.0
			});
		}
		list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>($"准备下载 {CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName}", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
		{
			//IL_010d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Expected O, but got Unknown
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			List<ModNet.NetFile> list3 = new List<ModNet.NetFile>();
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Info.IsNeoForge)
			{
				string text3 = ((ModDownload.DlNeoForgeListEntry)CS_0024_003C_003E8__locals0._0024VB_0024Local_Info).UrlBase + "-installer.jar";
				list3.Add(new ModNet.NetFile(new string[2]
				{
					text3.Replace("maven.neoforged.net/releases", "bmclapi2.bangbang93.com/maven"),
					text3
				}, CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress, new FileChecker
				{
					MinSize = 65536L
				}));
			}
			else
			{
				ModDownload.DlForgeVersionEntry dlForgeVersionEntry = (ModDownload.DlForgeVersionEntry)CS_0024_003C_003E8__locals0._0024VB_0024Local_Info;
				string arg4 = string.Format("{0}-{1}/forge-{2}-{3}-{4}.{5}", dlForgeVersionEntry.Inherit.Replace("-", "_"), dlForgeVersionEntry.FileVersion, dlForgeVersionEntry.Inherit.Replace("-", "_"), dlForgeVersionEntry.FileVersion, dlForgeVersionEntry.Category, dlForgeVersionEntry.FileExtension);
				list3.Add(new ModNet.NetFile(new string[2]
				{
					$"https://bmclapi2.bangbang93.com/maven/net/minecraftforge/forge/{arg4}",
					$"https://files.minecraftforge.net/maven/net/minecraftforge/forge/{arg4}"
				}, CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress, new FileChecker
				{
					MinSize = 65536L,
					Hash = dlForgeVersionEntry.Hash
				}));
			}
			Task.Output = list3;
		})
		{
			ProgressWeight = 0.5,
			Show = false
		});
		list.Add(new ModNet.LoaderDownload($"下载 {CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName} 主文件", new List<ModNet.NetFile>())
		{
			ProgressWeight = 9.0
		});
		CS_0024_003C_003E8__locals0._0024VB_0024Local_CurrentMcFolder = ModMinecraft.McFolderSelected;
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_IsNeoForge || Conversions.ToDouble(StringExtensions.BeforeFirst(CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion, ".", false)) >= 20.0)
		{
			_Closure_0024__30_002D1 arg2 = default(_Closure_0024__30_002D1);
			_Closure_0024__30_002D1 CS_0024_003C_003E8__locals1 = new _Closure_0024__30_002D1(arg2);
			CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2 = CS_0024_003C_003E8__locals0;
			Logger.Info(string.Format("检测为{0}Forge：{1}", CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_IsNeoForge ? " Neo" : "新版 ", CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderVersion), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			CS_0024_003C_003E8__locals1._0024VB_0024Local_Libs = null;
			list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>($"分析 {CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 支持库文件", checked([SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
			{
				//IL_0051: Unknown result type (might be due to invalid IL or missing references)
				//IL_0057: Expected O, but got Unknown
				//IL_0072: Unknown result type (might be due to invalid IL or missing references)
				//IL_0078: Expected O, but got Unknown
				//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
				Task.Output = new List<ModNet.NetFile>();
				ZipArchive val4 = null;
				try
				{
					val4 = FileUtils.OpenZip(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_InstallerAddress, default(CancellationToken));
					Task.Progress = 0.2;
					JObject val5 = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(val4.GetEntry("install_profile.json").Open(), (Encoding)null));
					JObject val6 = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(val4.GetEntry("version.json").Open(), (Encoding)null));
					((JContainer)val5).Merge((object)val6);
					CS_0024_003C_003E8__locals1._0024VB_0024Local_Libs = ModMinecraft.McLibListGetWithJson(val5, KeepSameNameDifferentVersionResult: true, CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CurrentMcFolder);
					if (val5["data"] != null && val5["data"][(object)"MOJMAPS"] != null)
					{
						Task.Progress = 0.4;
						JObject val7 = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByLoader(ModDownload.DlSourceLauncherOrMetaGet(Conversions.ToString(ModDownload.DlClientListGet(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit))), 45000, IsJson: true));
						string text2 = StringExtensions.BeforeFirst(val5["data"][(object)"MOJMAPS"][(object)"client"].ToString().Trim("[]".ToCharArray()), "@", false);
						string localPath = Path.ChangeExtension(ModMinecraft.McLibGet(text2, WithHead: true, IgnoreLiteLoader: false, CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CurrentMcFolder), "." + StringExtensions.Split(val5["data"][(object)"MOJMAPS"][(object)"client"].ToString().Trim("[]".ToCharArray()), "@", false)[1]);
						JToken val8 = val7["downloads"][(object)"client_mappings"];
						CS_0024_003C_003E8__locals1._0024VB_0024Local_Libs.Add(new ModMinecraft.McLibEntry
						{
							IsNatives = false,
							LocalPath = localPath,
							OriginalName = text2,
							Url = (string)val8[(object)"url"],
							Size = (long)val8[(object)"size"],
							SHA1 = (string)val8[(object)"sha1"]
						});
						Logger.Info(string.Format("需要下载 Mappings：{0} (SHA1: {1})", val8[(object)"url"], val8[(object)"sha1"]), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
					Task.Progress = 0.8;
					int num = CS_0024_003C_003E8__locals1._0024VB_0024Local_Libs.Count - 1;
					for (int j = 0; j <= num; j++)
					{
						if (StringExtensions.EndsWithF(CS_0024_003C_003E8__locals1._0024VB_0024Local_Libs[j].LocalPath, $"{StringExtensions.Lower(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName)}-{CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit}-{CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderVersion}.jar", false) || StringExtensions.EndsWithF(CS_0024_003C_003E8__locals1._0024VB_0024Local_Libs[j].LocalPath, $"{StringExtensions.Lower(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName)}-{CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit}-{CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderVersion}-client.jar", false))
						{
							Logger.Warn($"已从待下载 {CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 支持库中移除：{CS_0024_003C_003E8__locals1._0024VB_0024Local_Libs[j].LocalPath}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							CS_0024_003C_003E8__locals1._0024VB_0024Local_Libs.RemoveAt(j);
							break;
						}
					}
					Task.Output = ModMinecraft.McLibNetFilesFromTokens(CS_0024_003C_003E8__locals1._0024VB_0024Local_Libs, CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CurrentMcFolder);
				}
				catch (Exception ex9)
				{
					ProjectData.SetProjectError(ex9);
					Exception innerException3 = ex9;
					throw new Exception(string.Format("获取{0}Forge 支持库列表失败", CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_IsNeoForge ? " Neo" : "新版 "), innerException3);
				}
				finally
				{
					if (val4 != null)
					{
						val4.Dispose();
					}
				}
			}))
			{
				ProgressWeight = 1.0
			});
			list.Add(new ModNet.LoaderDownload($"下载 {CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 支持库文件", new List<ModNet.NetFile>())
			{
				ProgressWeight = 12.0
			});
			list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>($"获取 {CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 支持库文件", [SpecialName] (ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task) =>
			{
				foreach (ModMinecraft.McLibEntry _0024VB_0024Local_Lib in CS_0024_003C_003E8__locals1._0024VB_0024Local_Libs)
				{
					string text = Path.Combine(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder, _0024VB_0024Local_Lib.LocalPath.Substring(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_CurrentMcFolder.Length));
					if (!FileUtils.Exists(text))
					{
						FileUtils.Copy(_0024VB_0024Local_Lib.LocalPath, text);
					}
					Logger.Trace($"复制的 {CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 支持库文件：{_0024VB_0024Local_Lib.LocalPath}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				List<ModLoader.LoaderBase> source = (from l in CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_ClientDownloadLoader.GetLoaderList()
					where Operators.CompareString(l.Name, "下载原版支持库文件", TextCompare: false) == 0 || Operators.CompareString(l.Name, "下载原版 json 文件", TextCompare: false) == 0
					where l.State != ModBase.LoadState.Finished
					select l).ToList();
				if (source.Any())
				{
					Logger.Info($"{CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 安装正在等待原版文件下载完成", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				while (source.Any() && !Task.IsCanceled)
				{
					source = source.Where((_Closure_0024__._0024I30_002D6 == null) ? (_Closure_0024__._0024I30_002D6 = [SpecialName] (ModLoader.LoaderBase l) => l.State != ModBase.LoadState.Finished) : _Closure_0024__._0024I30_002D6).ToList();
					Thread.Sleep(50);
				}
				if (!Task.IsCanceled)
				{
					object vanillaSyncLock = VanillaSyncLock;
					ObjectFlowControl.CheckForSyncLockOnValueType(vanillaSyncLock);
					bool lockTaken = false;
					try
					{
						Monitor.Enter(vanillaSyncLock, ref lockTaken);
						string lastPart = PathUtils.GetLastPart(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_ClientFolder);
						DirectoryUtils.Create(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit);
						if (!FileUtils.Exists(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + "\\" + CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + ".json"))
						{
							FileUtils.Copy(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_ClientFolder + lastPart + ".json", CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + "\\" + CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + ".json");
						}
						if (!FileUtils.Exists(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + "\\" + CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + ".jar"))
						{
							FileUtils.Copy(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_ClientFolder + lastPart + ".jar", CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\" + CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + "\\" + CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Inherit + ".jar");
						}
					}
					finally
					{
						if (lockTaken)
						{
							Monitor.Exit(vanillaSyncLock);
						}
					}
				}
			})
			{
				ProgressWeight = 0.1,
				Show = false
			});
			list.Add(new ModLoader.LoaderTask<bool, bool>(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_IsNeoForge ? "安装 NeoForge" : "安装 Forge（方式 A）", [SpecialName] (ModLoader.LoaderTask<bool, bool> Task) =>
			{
				try
				{
					_Closure_0024__30_002D2 arg3 = default(_Closure_0024__30_002D2);
					_Closure_0024__30_002D2 CS_0024_003C_003E8__locals2 = new _Closure_0024__30_002D2(arg3);
					Logger.Info($"开始进行 Forgelike 安装：{CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_InstallerAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					CS_0024_003C_003E8__locals2._0024VB_0024Local_OldList = DirectoryUtils.EnumerateDirectories(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\", false, "*").ToList();
					DirectoryUtils.Create(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_VersionFolder);
					Task.Progress = 0.04;
					ModMinecraft.McFolderLauncherProfilesJsonCreate(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder);
					Task.Progress = 0.05;
					bool flag = true;
					while (true)
					{
						try
						{
							ModBase.ExtractResources(ModBase.PathTemp + "Cache\\forge_installer.jar", "ForgeInstaller");
							Task.Progress = 0.06;
							ForgelikeInjector(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_InstallerAddress, Task, CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder, flag, CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_IsNeoForge);
							Task.Progress = 0.97;
						}
						catch (Exception ex4)
						{
							ProjectData.SetProjectError(ex4);
							Exception ex5 = ex4;
							if (!flag)
							{
								throw new Exception($"运行 {CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 安装器失败", ex5);
							}
							Logger.Warn(ex5, $"使用 JavaWrapper 安装 {CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 失败，将不使用 JavaWrapper 并重试", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
							flag = false;
							ProjectData.ClearProjectError();
							continue;
						}
						break;
					}
					List<DirectoryInfo> list2 = (from f in DirectoryUtils.EnumerateDirectories(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_McFolder + "versions\\", false, "*").SkipWhile([SpecialName] (string i) => CS_0024_003C_003E8__locals2._0024VB_0024Local_OldList.Contains(i))
						select DirectoryUtils.GetInfo(f)).ToList();
					if (list2.Count > 1)
					{
						list2 = list2.Where([SpecialName] (DirectoryInfo l) => StringExtensions.ContainsIgnoreCase(l.Name, "forge") && l.EnumerateFiles().Any()).ToList();
					}
					if (list2.Count == 1)
					{
						FileInfo fileInfo = list2[0].EnumerateFiles().First();
						FileUtils.Copy(fileInfo.FullName, CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_VersionFolder + CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_NewInstanceName + ".json");
						Logger.Info($"已复制新增的版本 JSON 文件：{fileInfo.FullName} -> {CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_VersionFolder}{CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_NewInstanceName}.json", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
					else if (list2.Count > 1)
					{
						Logger.Info(string.Format("有多个疑似的新增版本，无法确定：{0}", EnumerableExtensions.Join((IEnumerable)list2.Select([SpecialName] (DirectoryInfo d) => d.Name), ";")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
					else
					{
						Logger.Info("未找到新增的版本文件夹", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					}
				}
				catch (Exception ex6)
				{
					ProjectData.SetProjectError(ex6);
					Exception innerException2 = ex6;
					throw new Exception($"安装新 {CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 版本失败", innerException2);
				}
				finally
				{
					try
					{
						FileUtils.Delete(CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_InstallerAddress, false);
					}
					catch (Exception ex7)
					{
						ProjectData.SetProjectError(ex7);
						Exception ex8 = ex7;
						Logger.Warn(ex8, $"安装 {CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_LoaderName} 清理文件时出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						ProjectData.ClearProjectError();
					}
				}
			})
			{
				ProgressWeight = 10.0
			});
		}
		else
		{
			Logger.Info($"检测为非新版 Forge：{CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderVersion}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			list.Add(new ModLoader.LoaderTask<List<ModNet.NetFile>, bool>($"安装 {CS_0024_003C_003E8__locals0._0024VB_0024Local_LoaderName}（方式 B）", [SpecialName] (ModLoader.LoaderTask<List<ModNet.NetFile>, bool> Task) =>
			{
				//IL_0041: Unknown result type (might be due to invalid IL or missing references)
				//IL_0047: Expected O, but got Unknown
				//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
				//IL_00d7: Expected O, but got Unknown
				//IL_025e: Unknown result type (might be due to invalid IL or missing references)
				ZipArchive val = null;
				try
				{
					val = FileUtils.OpenZip(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress, default(CancellationToken));
					Task.Progress = 0.2;
					JObject val2 = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(val.GetEntry("install_profile.json").Open(), (Encoding)null));
					Task.Progress = 0.4;
					DirectoryUtils.Create(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder);
					Task.Progress = 0.5;
					if (val2["install"] == null)
					{
						Logger.Info($"开始进行 Forge 安装，Legacy 方式 1：{CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						JObject val3 = (JObject)StringExtensions.DeserializeJson(StringExtensions.ReadString(val.GetEntry(val2["json"].ToString().TrimStart('/')).Open(), (Encoding)null));
						val3["id"] = JToken.op_Implicit(CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstanceName);
						FileUtils.Write(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstanceName + ".json", ((JToken)val3).ToString(), (Encoding)null);
						Task.Progress = 0.6;
						val.Dispose();
						FileUtils.ExtractToDirectory(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress, CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress + "_unrar\\", default(CancellationToken), (ProgressProvider)null);
						if (DirectoryUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress + "_unrar\\maven\\"))
						{
							DirectoryUtils.Copy(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress + "_unrar\\maven\\", CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder + "libraries\\");
						}
						DirectoryUtils.Delete(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress + "_unrar\\", false);
					}
					else
					{
						Logger.Info($"开始进行 Forge 安装，Legacy 方式 2：{CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						FileUtils.Write(ModMinecraft.McLibGet((string)val2["install"][(object)"path"], WithHead: true, IgnoreLiteLoader: false, CS_0024_003C_003E8__locals0._0024VB_0024Local_McFolder), val.GetEntry((string)val2["install"][(object)"filePath"]).Open());
						Task.Progress = 0.9;
						val2["versionInfo"][(object)"id"] = JToken.op_Implicit(CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstanceName);
						if (val2["versionInfo"][(object)"inheritsFrom"] == null)
						{
							((JObject)val2["versionInfo"]).Add("inheritsFrom", JToken.op_Implicit(CS_0024_003C_003E8__locals0._0024VB_0024Local_Inherit));
						}
						FileUtils.Write(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder + CS_0024_003C_003E8__locals0._0024VB_0024Local_NewInstanceName + ".json", val2["versionInfo"].ToString(), (Encoding)null);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					throw new Exception("非新版方式安装 Forge 失败", innerException);
				}
				finally
				{
					try
					{
						if (val != null)
						{
							val.Dispose();
						}
						FileUtils.Delete(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress, false);
						DirectoryUtils.Delete(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstallerAddress + "_unrar\\", false);
					}
					catch (Exception ex2)
					{
						ProjectData.SetProjectError(ex2);
						Exception ex3 = ex2;
						Logger.Warn(ex3, "非新版方式安装 Forge 清理文件时出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						ProjectData.ClearProjectError();
					}
				}
			})
			{
				ProgressWeight = 1.0
			});
		}
		return list;
	}

	public static void ForgeDownloadListItemPreload(StackPanel Stack, List<ModDownload.DlForgeVersionEntry> Entries, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
	{
		if (EnumerableExtensions.IsSingle<ModDownload.DlForgeVersionEntry>((IEnumerable<ModDownload.DlForgeVersionEntry>)Entries))
		{
			return;
		}
		ModDownload.DlForgeVersionEntry dlForgeVersionEntry = null;
		if (Entries.Any())
		{
			dlForgeVersionEntry = Entries[0];
		}
		else
		{
			Logger.Info("未找到可用的 Forge 版本", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		}
		ModDownload.DlForgeVersionEntry dlForgeVersionEntry2 = null;
		foreach (ModDownload.DlForgeVersionEntry Entry in Entries)
		{
			if (Entry.IsRecommended)
			{
				dlForgeVersionEntry2 = Entry;
			}
		}
		if (dlForgeVersionEntry != null && dlForgeVersionEntry == dlForgeVersionEntry2)
		{
			dlForgeVersionEntry = null;
		}
		if (dlForgeVersionEntry2 != null)
		{
			MyListItem myListItem = ForgeDownloadListItem(dlForgeVersionEntry2, OnClick, IsSaveOnly).Init();
			myListItem.Info = "推荐版" + ((Operators.CompareString(myListItem.Info, "", TextCompare: false) == 0) ? "" : ("，" + myListItem.Info));
			Stack.Children.Add(myListItem);
		}
		if (dlForgeVersionEntry != null)
		{
			MyListItem myListItem2 = ForgeDownloadListItem(dlForgeVersionEntry, OnClick, IsSaveOnly).Init();
			myListItem2.Info = "最新版" + ((Operators.CompareString(myListItem2.Info, "", TextCompare: false) == 0) ? "" : ("，" + myListItem2.Info));
			Stack.Children.Add(myListItem2);
		}
		Stack.Children.Add(new TextBlock
		{
			Text = "全部版本 (" + Conversions.ToString(Entries.Count) + ")",
			HorizontalAlignment = HorizontalAlignment.Left,
			Margin = new Thickness(6.0, 13.0, 0.0, 4.0)
		});
	}

	public static MyVirtualizingElement<MyListItem> ForgeDownloadListItem(ModDownload.DlForgeVersionEntry Entry, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
	{
		return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
		{
			MyListItem myListItem = new MyListItem
			{
				Title = Entry.VersionName,
				SnapsToDevicePixels = true,
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Entry,
				Info = EnumerableExtensions.Join((IEnumerable)new string[2]
				{
					(Operators.CompareString(Entry.ReleaseTime, "", TextCompare: false) == 0) ? "" : ("发布于 " + Entry.ReleaseTime),
					ModBase.ModeDebug ? ("种类：" + Entry.Category) : ""
				}.Where([SpecialName] (string d) => Operators.CompareString(d, "", TextCompare: false) != 0), "，"),
				Logo = ModBase.PathImage + "Blocks/Anvil.png"
			};
			myListItem.Click += OnClick;
			if (IsSaveOnly)
			{
				myListItem.ContentHandler = ForgeSaveContMenuBuild;
			}
			else
			{
				myListItem.ContentHandler = ForgeContMenuBuild;
			}
			return myListItem;
		})
		{
			Height = 42.0
		};
	}

	private static void ForgeContMenuBuild(MyListItem sender, EventArgs e)
	{
		MyIconButton myIconButton = new MyIconButton
		{
			Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
			ToolTip = "另存为"
		};
		myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			ForgeSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		MyIconButton myIconButton2 = new MyIconButton
		{
			LogoScale = 1.05,
			Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
			ToolTip = "更新日志"
		};
		myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			ForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		sender.Buttons = new MyIconButton[2] { myIconButton, myIconButton2 };
	}

	private static void ForgeSaveContMenuBuild(MyListItem sender, EventArgs e)
	{
		MyIconButton myIconButton = new MyIconButton
		{
			LogoScale = 1.05,
			Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
			ToolTip = "更新日志"
		};
		myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			ForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		sender.Buttons = new MyIconButton[1] { myIconButton };
	}

	private static void ForgeLog_Click(object sender, RoutedEventArgs e)
	{
		ModDownload.DlForgeVersionEntry dlForgeVersionEntry = ((NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null) ? ((ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)) : ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null))));
		ModBase.OpenWebsite($"https://files.minecraftforge.net/maven/net/minecraftforge/forge/{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.VersionName}/forge-{dlForgeVersionEntry.Inherit}-{dlForgeVersionEntry.VersionName}-changelog.txt");
	}

	public static void ForgeSave_Click(object sender, RoutedEventArgs e)
	{
		ModDownload.DlForgeVersionEntry info = ((NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null) ? ((ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)) : ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlForgeVersionEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null))));
		McDownloadForgelikeSave(info);
	}

	public static void McDownloadForgeRecommendedRefresh()
	{
		if (IsForgeRecommendedRefreshed)
		{
			return;
		}
		IsForgeRecommendedRefreshed = true;
		ModBase.RunInNewThread([SpecialName] () =>
		{
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Expected O, but got Unknown
			try
			{
				Logger.Info("刷新 Forge 推荐版本缓存开始", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				string text = ModNet.NetRequestByClientRetry("https://bmclapi2.bangbang93.com/forge/promos", null, null, null, null, "*/*", null, RequireJson: true);
				if (text.Length < 1000)
				{
					throw new Exception("获取的结果过短（" + text + "）");
				}
				JContainer val = (JContainer)StringExtensions.DeserializeJson(text);
				List<string> list = new List<string>();
				foreach (JObject item in (IEnumerable<JToken>)val)
				{
					JObject val2 = item;
					if (val2["name"] != null && val2["build"] != null)
					{
						string text2 = (string)val2["name"];
						if (StringExtensions.EndsWithF(text2, "-recommended", false))
						{
							list.Add("\"" + text2.Replace("-recommended", "\":\"" + val2["build"][(object)"version"].ToString() + "\""));
						}
					}
				}
				if (list.Count < 5)
				{
					throw new Exception("获取的推荐版本数过少（" + text + "）");
				}
				string text3 = "{" + EnumerableExtensions.Join((IEnumerable)list, ",") + "}";
				FileUtils.Write(ModBase.PathTemp + "Cache\\ForgeRecommendedList.json", text3, (Encoding)null);
				Logger.Info("刷新 Forge 推荐版本缓存成功", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "刷新 Forge 推荐版本缓存失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
		}, "ForgeRecommendedRefresh");
	}

	public static string McDownloadForgeRecommendedGet(string McVersionName)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		string result;
		try
		{
			if (McVersionName == null)
			{
				result = null;
			}
			else
			{
				string text = FileUtils.TryReadAsString(ModBase.PathTemp + "Cache\\ForgeRecommendedList.json", (Encoding)null, (Type)null);
				if (string.IsNullOrEmpty(text))
				{
					Logger.Info("没有 Forge 推荐版本缓存文件", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					result = null;
				}
				else
				{
					JObject val = (JObject)StringExtensions.DeserializeJson(text);
					result = ((val != null && (McVersionName ?? "").Contains(".") && val.ContainsKey(McVersionName)) ? (val[McVersionName] ?? JToken.op_Implicit("")).ToString() : null);
				}
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, string.Format("获取 Forge 推荐版本失败（{0}）", McVersionName ?? "null"), (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			result = null;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static void NeoForgeDownloadListItemPreload(StackPanel Stack, List<ModDownload.DlNeoForgeListEntry> Entries, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
	{
		if (EnumerableExtensions.IsSingle<ModDownload.DlNeoForgeListEntry>((IEnumerable<ModDownload.DlNeoForgeListEntry>)Entries))
		{
			return;
		}
		ModDownload.DlNeoForgeListEntry dlNeoForgeListEntry = null;
		ModDownload.DlNeoForgeListEntry dlNeoForgeListEntry2 = null;
		if (Entries.Any())
		{
			foreach (ModDownload.DlNeoForgeListEntry item in Entries.ToList())
			{
				if (item.IsBeta)
				{
					if (dlNeoForgeListEntry2 == null)
					{
						dlNeoForgeListEntry2 = item;
					}
					continue;
				}
				dlNeoForgeListEntry = item;
				break;
			}
		}
		else
		{
			Logger.Warn("未找到可用的 NeoForge 版本", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		}
		if (dlNeoForgeListEntry != null)
		{
			MyListItem myListItem = NeoForgeDownloadListItem(dlNeoForgeListEntry, OnClick, IsSaveOnly).Init();
			myListItem.Info = ((Operators.CompareString(myListItem.Info, "", TextCompare: false) == 0) ? "最新稳定版" : ("最新" + myListItem.Info));
			Stack.Children.Add(myListItem);
		}
		if (dlNeoForgeListEntry2 != null)
		{
			MyListItem myListItem2 = NeoForgeDownloadListItem(dlNeoForgeListEntry2, OnClick, IsSaveOnly).Init();
			myListItem2.Info = ((Operators.CompareString(myListItem2.Info, "", TextCompare: false) == 0) ? "最新测试版" : ("最新" + myListItem2.Info));
			Stack.Children.Add(myListItem2);
		}
		Stack.Children.Add(new TextBlock
		{
			Text = "全部版本 (" + Conversions.ToString(Entries.Count) + ")",
			HorizontalAlignment = HorizontalAlignment.Left,
			Margin = new Thickness(6.0, 13.0, 0.0, 4.0)
		});
	}

	public static MyVirtualizingElement<MyListItem> NeoForgeDownloadListItem(ModDownload.DlNeoForgeListEntry Info, MyListItem.ClickEventHandler OnClick, bool IsSaveOnly)
	{
		return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
		{
			MyListItem myListItem = new MyListItem
			{
				Title = Info.VersionName,
				SnapsToDevicePixels = true,
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Info,
				Info = (Info.IsBeta ? "测试版" : "稳定版"),
				Logo = ModBase.PathImage + "Blocks/NeoForge.png"
			};
			myListItem.Click += OnClick;
			if (IsSaveOnly)
			{
				myListItem.ContentHandler = NeoForgeSaveContMenuBuild;
			}
			else
			{
				myListItem.ContentHandler = NeoForgeContMenuBuild;
			}
			return myListItem;
		})
		{
			Height = 42.0
		};
	}

	private static void NeoForgeContMenuBuild(MyListItem sender, EventArgs e)
	{
		MyIconButton myIconButton = new MyIconButton
		{
			Logo = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z",
			ToolTip = "另存为"
		};
		myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			NeoForgeSave_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		MyIconButton myIconButton2 = new MyIconButton
		{
			LogoScale = 1.05,
			Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
			ToolTip = "更新日志"
		};
		myIconButton2.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			NeoForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		sender.Buttons = new MyIconButton[2] { myIconButton, myIconButton2 };
	}

	private static void NeoForgeSaveContMenuBuild(MyListItem sender, EventArgs e)
	{
		MyIconButton myIconButton = new MyIconButton
		{
			LogoScale = 1.05,
			Logo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z",
			ToolTip = "更新日志"
		};
		myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
		{
			NeoForgeLog_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
		};
		sender.Buttons = new MyIconButton[1] { myIconButton };
	}

	private static void NeoForgeLog_Click(object sender, RoutedEventArgs e)
	{
		ModDownload.DlNeoForgeListEntry dlNeoForgeListEntry = ((NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null) ? ((ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)) : ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null))));
		ModBase.OpenWebsite(dlNeoForgeListEntry.UrlBase + "-changelog.txt");
	}

	public static void NeoForgeSave_Click(object sender, RoutedEventArgs e)
	{
		ModDownload.DlNeoForgeListEntry info = ((NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null) != null) ? ((ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)) : ((NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null) == null) ? ((ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null)) : ((ModDownload.DlNeoForgeListEntry)NewLateBinding.LateGet(NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null))));
		McDownloadForgelikeSave(info);
	}

	public static void McDownloadFabricLoaderSave(JObject DownloadInfo)
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		try
		{
			string text = DownloadInfo["url"].ToString();
			string lastPart = PathUtils.GetLastPart(text);
			string lastPart2 = PathUtils.GetLastPart(DownloadInfo["version"].ToString());
			string text2 = Dialogs.SaveFile("选择保存位置", lastPart, (string)null, (IEnumerable<ValueTuple<string, string>>)new(string, string)[1] { ("jar", "Fabric 安装器") });
			if (text2 == null)
			{
				return;
			}
			foreach (ModLoader.LoaderBase item in ((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).ToList())
			{
				if (Operators.CompareString(item.Name, $"Fabric {lastPart2} 安装器下载", TextCompare: false) == 0)
				{
					ModMain.Hint("该版本正在下载中！", ModMain.HintType.Red);
					return;
				}
			}
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			List<string> list2 = new List<string>();
			list2.Add(text);
			list.Add(new ModNet.LoaderDownload("下载主文件", new List<ModNet.NetFile>
			{
				new ModNet.NetFile(list2.ToArray(), text2, new FileChecker
				{
					MinSize = 65536L
				})
			})
			{
				ProgressWeight = 15.0
			});
			ModLoader.LoaderCombo<JObject> loaderCombo = new ModLoader.LoaderCombo<JObject>("Fabric " + lastPart2 + " 安装器下载", list);
			loaderCombo.OnStateChanged = LoaderStateChangedHintOnly;
			loaderCombo.Start(DownloadInfo);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.BtnExtraDownload.Ribble();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "开始 Fabric 安装器下载失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			ProjectData.ClearProjectError();
		}
	}

	private static List<ModLoader.LoaderBase> McDownloadFabricLoader(string FabricVersion, string MinecraftName, string McFolder)
	{
		string text = "fabric-loader-" + FabricVersion + "-" + MinecraftName;
		string text2 = McFolder + "versions\\" + text + "\\";
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		MinecraftName = MinecraftName.Replace("∞", "infinite");
		list.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("获取 Fabric 主文件下载地址", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
		{
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Expected O, but got Unknown
			Task.Output = new List<ModNet.NetFile>
			{
				new ModNet.NetFile(new string[2]
				{
					"https://bmclapi2.bangbang93.com/fabric-meta/v2/versions/loader/" + MinecraftName + "/" + FabricVersion + "/profile/json",
					"https://meta.fabricmc.net/v2/versions/loader/" + MinecraftName + "/" + FabricVersion + "/profile/json"
				}, text2 + text + ".json", new FileChecker
				{
					IsJson = true
				})
			};
		})
		{
			ProgressWeight = 0.5
		});
		list.Add(new ModNet.LoaderDownload("下载 Fabric 主文件", new List<ModNet.NetFile>())
		{
			ProgressWeight = 2.5
		});
		return list;
	}

	public static MyVirtualizingElement<MyListItem> FabricDownloadListItem(JObject Entry, MyListItem.ClickEventHandler OnClick)
	{
		return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
		{
			MyListItem myListItem = new MyListItem();
			myListItem.Title = Entry["version"].ToString().Replace("+build", "");
			myListItem.SnapsToDevicePixels = true;
			myListItem.Height = 42.0;
			myListItem.Type = MyListItem.CheckType.Clickable;
			myListItem.Tag = Entry;
			myListItem.Info = (Entry["stable"].ToObject<bool>() ? "稳定版" : "测试版");
			myListItem.Logo = ModBase.PathImage + "Blocks/Fabric.png";
			myListItem.Click += OnClick;
			return myListItem;
		})
		{
			Height = 42.0
		};
	}

	public static MyVirtualizingElement<MyListItem> FabricApiDownloadListItem(ResourceVersion Entry, MyListItem.ClickEventHandler OnClick)
	{
		return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
		{
			MyListItem myListItem = new MyListItem();
			myListItem.Title = StringExtensions.BeforeFirst(StringExtensions.Split(Entry.Display, "]", false)[1].Replace("Fabric API ", "").Replace(" build ", "."), "+", false).Trim();
			myListItem.SnapsToDevicePixels = true;
			myListItem.Height = 42.0;
			myListItem.Type = MyListItem.CheckType.Clickable;
			myListItem.Tag = Entry;
			myListItem.Info = Entry.ReleaseTypeDisplay + "，发布于 " + Entry.ReleaseDate.ToString("yyyy'/'MM'/'dd HH':'mm");
			myListItem.Logo = ModBase.PathImage + "Blocks/Fabric.png";
			myListItem.Click += OnClick;
			return myListItem;
		})
		{
			Height = 42.0
		};
	}

	public static MyVirtualizingElement<MyListItem> OptiFabricDownloadListItem(ResourceVersion Entry, MyListItem.ClickEventHandler OnClick)
	{
		return new MyVirtualizingElement<MyListItem>([SpecialName] () =>
		{
			MyListItem obj = new MyListItem
			{
				Title = StringExtensions.Lower(Entry.Display).Replace("optifabric-", "").Replace(".jar", "")
					.Trim()
					.TrimStart('v'),
				SnapsToDevicePixels = true,
				Height = 42.0,
				Type = MyListItem.CheckType.Clickable,
				Tag = Entry,
				Info = Entry.ReleaseTypeDisplay + "，发布于 " + Entry.ReleaseDate.ToString("yyyy'/'MM'/'dd HH':'mm"),
				Logo = ModBase.PathImage + "Blocks/OptiFabric.png"
			};
			obj.Click += OnClick;
			return obj;
		})
		{
			Height = 42.0
		};
	}

	public static List<ModLoader.LoaderBase> McInstallLoadersGet(McInstallRequest Request)
	{
		_Closure_0024__52_002D0 arg = default(_Closure_0024__52_002D0);
		_Closure_0024__52_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__52_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Request = Request;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder = ModMain.RequestTaskTempFolder(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.OptiFineEntry != null || CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions.ContainsKey((ModLoaders)1) || CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions.ContainsKey((ModLoaders)16));
		JObject optiFineEntry = CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.OptiFineEntry;
		ModDownload.DlOptiFineListEntry dlOptiFineListEntry = ((optiFineEntry != null) ? ((JToken)optiFineEntry).ToObject<ModDownload.DlOptiFineListEntry>() : null);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineFolder = ((dlOptiFineListEntry == null) ? null : $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder}versions\\{dlOptiFineListEntry.InstanceName}");
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeVersion = (CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions.ContainsKey((ModLoaders)1) ? CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions[(ModLoaders)1] : null);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeFolder = ((CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeVersion == null) ? null : $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder}versions\\forge-{CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeVersion}");
		CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeVersion = (CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions.ContainsKey((ModLoaders)16) ? CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions[(ModLoaders)16] : null);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeFolder = ((CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeVersion == null) ? null : $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder}versions\\neoforge-{CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeVersion}");
		string text = (CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions.ContainsKey((ModLoaders)2) ? CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions[(ModLoaders)2] : null);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_LiteLoaderFolder = ((text == null) ? null : $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder}versions\\{CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion}-LiteLoader");
		string text2 = (CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions.ContainsKey((ModLoaders)4) ? CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions[(ModLoaders)4] : null);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_FabricFolder = ((text2 == null) ? null : $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder}versions\\fabric-loader-{text2}-{CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion}");
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Modable = CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.ModLoaderVersions.Any();
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ModsTempFolder = $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder}mods\\";
		CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineAsMod = dlOptiFineListEntry != null && CS_0024_003C_003E8__locals0._0024VB_0024Local_Modable;
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineAsMod)
		{
			Logger.Info("OptiFine 将作为 Mod 进行下载", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			if (text != null)
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineFolder = $"{CS_0024_003C_003E8__locals0._0024VB_0024Local_ModsTempFolder}{CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion}\\";
			}
			else
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineFolder = CS_0024_003C_003E8__locals0._0024VB_0024Local_ModsTempFolder;
			}
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineFolder != null)
		{
			Logger.Info($"OptiFine 缓存：{CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineFolder}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeFolder != null)
		{
			Logger.Info($"Forge 缓存：{CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeFolder}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeFolder != null)
		{
			Logger.Info($"NeoForge 缓存：{CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeFolder}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_FabricFolder != null)
		{
			Logger.Info($"Fabric 缓存：{CS_0024_003C_003E8__locals0._0024VB_0024Local_FabricFolder}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_LiteLoaderFolder != null)
		{
			Logger.Info($"LiteLoader 缓存：{CS_0024_003C_003E8__locals0._0024VB_0024Local_LiteLoaderFolder}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		}
		Logger.Info($"对应的原版版本：{CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder = PathUtils.AddSlashSuffix(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.InstanceFolder);
		if (FileUtils.Exists($"{CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder}{CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.NewInstanceName}.json"))
		{
			ModMain.Hint("版本 " + CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.NewInstanceName + " 已经存在！", ModMain.HintType.Red);
			throw new OperationCanceledException();
		}
		List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ManagedFileSources = new Dictionary<string, string>();
		list.Add(new ModLoader.LoaderTask<int, int>("添加忽略标识", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
		{
			CS_0024_003C_003E8__locals0._Lambda_0024__0();
		})
		{
			Show = false
		});
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.FabricApiVersion != null)
		{
			ModNet.NetFile netFile = CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.FabricApi.ToNetFile(CS_0024_003C_003E8__locals0._0024VB_0024Local_ModsTempFolder, ResourceVersion.DownloadReason.Dependency, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion, (ModLoaders)4);
			CS_0024_003C_003E8__locals0._0024VB_0024Local_ManagedFileSources.Add("FabricApi", netFile.LocalPath);
			list.Add(new ModNet.LoaderDownload("下载 Fabric API", new List<ModNet.NetFile> { netFile })
			{
				ProgressWeight = 3.0,
				Show = true,
				Block = false
			});
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.OptiFabricVersion != null)
		{
			ModNet.NetFile netFile2 = CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.OptiFabric.ToNetFile(CS_0024_003C_003E8__locals0._0024VB_0024Local_ModsTempFolder, ResourceVersion.DownloadReason.Dependency, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion, (ModLoaders)4);
			CS_0024_003C_003E8__locals0._0024VB_0024Local_ManagedFileSources.Add("OptiFabric", netFile2.LocalPath);
			list.Add(new ModNet.LoaderDownload("下载 OptiFabric", new List<ModNet.NetFile> { netFile2 })
			{
				ProgressWeight = 3.0,
				Show = true,
				Block = false
			});
		}
		ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>("下载原版 " + CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion, McDownloadClientLoader(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.MinecraftJson, CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder))
		{
			ProgressWeight = 39.0,
			Show = false,
			Block = (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Modable && dlOptiFineListEntry == null)
		};
		list.Add(loaderCombo);
		if (dlOptiFineListEntry != null)
		{
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineAsMod)
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_ManagedFileSources.Add("OptiFine", CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineFolder + dlOptiFineListEntry.FileName);
			}
			list.Add(new ModLoader.LoaderCombo<string>("下载 OptiFine " + dlOptiFineListEntry.DisplayName, CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineAsMod ? McDownloadOptiFineSaveLoader(dlOptiFineListEntry, CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineFolder + dlOptiFineListEntry.FileName) : McDownloadOptiFineLoader(dlOptiFineListEntry, CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder, loaderCombo, CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder))
			{
				ProgressWeight = (CS_0024_003C_003E8__locals0._0024VB_0024Local_Modable ? 16 : 24),
				Show = false,
				Block = !CS_0024_003C_003E8__locals0._0024VB_0024Local_Modable
			});
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeVersion != null)
		{
			list.Add(new ModLoader.LoaderCombo<string>("下载 Forge " + CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeVersion, McDownloadForgelikeLoader(IsNeoForge: false, CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeVersion, "forge-" + CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeVersion, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.ForgeEntry, CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder, loaderCombo, CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder))
			{
				ProgressWeight = 25.0,
				Show = false,
				Block = (text2 == null && CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.LiteLoaderEntry == null && CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.NeoForgeEntry == null)
			});
		}
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeVersion != null)
		{
			list.Add(new ModLoader.LoaderCombo<string>("下载 NeoForge " + CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeVersion, McDownloadForgelikeLoader(IsNeoForge: true, CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeVersion, "neoforge-" + CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeVersion, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.NeoForgeEntry, CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder, loaderCombo, CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder))
			{
				ProgressWeight = 25.0,
				Show = false,
				Block = (CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.ForgeEntry == null && text2 == null && CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.LiteLoaderEntry == null)
			});
		}
		if (text != null)
		{
			list.Add(new ModLoader.LoaderCombo<string>("下载 LiteLoader " + CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion, McDownloadLiteLoaderLoader(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.LiteLoaderEntry, CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder))
			{
				ProgressWeight = 1.0,
				Show = false,
				Block = (text2 == null)
			});
		}
		if (text2 != null)
		{
			list.Add(new ModLoader.LoaderCombo<string>("下载 Fabric " + text2, McDownloadFabricLoader(text2, CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Components.MinecraftVersion, CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder))
			{
				ProgressWeight = 2.0,
				Show = false,
				Block = true
			});
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftFolder = ModMinecraft.McFolderSelected;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder = null;
		list.Add(new ModLoader.LoaderTask<string, string>("安装游戏", [SpecialName] (ModLoader.LoaderTask<string, string> Task) =>
		{
			MergeJson(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder, CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineFolder, CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineAsMod, CS_0024_003C_003E8__locals0._0024VB_0024Local_ForgeFolder, CS_0024_003C_003E8__locals0._0024VB_0024Local_NeoForgeFolder, CS_0024_003C_003E8__locals0._0024VB_0024Local_FabricFolder, CS_0024_003C_003E8__locals0._0024VB_0024Local_LiteLoaderFolder);
			Task.Progress = 0.2;
			DirectoryUtils.Copy(CS_0024_003C_003E8__locals0._0024VB_0024Local_TempMcFolder + "libraries", CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftFolder + "libraries");
			Task.Progress = 0.8;
			CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder = (CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.IsIntermediateOutput ? CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder : new ModMinecraft.McInstance(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder).PathIndie);
			string text3 = CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder + "mods\\";
			if (DirectoryUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_ModsTempFolder))
			{
				DirectoryUtils.Copy(CS_0024_003C_003E8__locals0._0024VB_0024Local_ModsTempFolder, text3);
			}
			else if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Modable)
			{
				DirectoryUtils.Create(text3);
				Logger.Info($"自动创建 Mod 文件夹：{text3}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			}
			string text4 = CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder + "resourcepacks\\";
			DirectoryUtils.Create(text4);
			Logger.Info($"自动创建资源包文件夹：{text4}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		})
		{
			ProgressWeight = 2.0,
			Block = true
		});
		List<ModLoader.LoaderBase> list2 = new List<ModLoader.LoaderBase>();
		list2.Add(new ModLoader.LoaderTask<string, List<ModNet.NetFile>>("分析游戏支持库文件（副加载器）", [SpecialName] (ModLoader.LoaderTask<string, List<ModNet.NetFile>> Task) =>
		{
			Task.Output = ModMinecraft.McLibNetFilesFromInstance(new ModMinecraft.McInstance(CS_0024_003C_003E8__locals0._0024VB_0024Local_VersionFolder));
		})
		{
			ProgressWeight = 1.0,
			Show = false
		});
		list2.Add(new ModNet.LoaderDownload("下载游戏支持库文件（副加载器）", new List<ModNet.NetFile>())
		{
			ProgressWeight = 7.0,
			Show = false
		});
		list.Add(new ModLoader.LoaderCombo<string>("下载游戏支持库文件", list2)
		{
			ProgressWeight = 8.0
		});
		list.Add(new ModLoader.LoaderTask<int, int>("记录安装清单", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
		{
			CS_0024_003C_003E8__locals0._Lambda_0024__3();
		})
		{
			Show = false
		});
		if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.IsIntermediateOutput)
		{
			list.Add(new ModLoader.LoaderTask<int, int>("删除忽略标识", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
			{
				CS_0024_003C_003E8__locals0._Lambda_0024__4();
			})
			{
				Show = false
			});
		}
		return list;
	}

	private static void MergeJson(string VersionFolder, string OptiFineFolder, bool OptiFineAsMod, string ForgeFolder, string NeoForgeFolder, string FabricFolder, string LiteLoaderFolder)
	{
		List<(string, string)> list = new List<(string, string)> { ("Minecraft", VersionFolder) };
		if (OptiFineFolder != null && !OptiFineAsMod)
		{
			list.Add(("OptiFine", OptiFineFolder));
		}
		if (ForgeFolder != null)
		{
			list.Add(("Forge", ForgeFolder));
		}
		if (NeoForgeFolder != null)
		{
			list.Add(("NeoForge", NeoForgeFolder));
		}
		if (LiteLoaderFolder != null)
		{
			list.Add(("LiteLoader", LiteLoaderFolder));
		}
		if (FabricFolder != null)
		{
			list.Add(("Fabric", FabricFolder));
		}
		Logger.Info($"开始进行版本合并，输出：{VersionFolder}，" + string.Join("，", list.Select<(string, string), string>([SpecialName] ((string Name, string Folder) Source) => $"{Source.Name}：{Source.Folder}")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
		List<(string, JObject)> list2 = new List<(string, JObject)>();
		foreach (var item2 in list)
		{
			string text = Path.Combine(item2.Item2, PathUtils.GetLastPart(item2.Item2) + ".json");
			string text2 = FileUtils.TryReadAsString(text, (Encoding)null, (Type)null) ?? "";
			if (!StringExtensions.StartsWithF(text2, "{", false))
			{
				throw new Exception($"{item2.Item1} json 有误，地址：{text}，前段内容：{text2.Substring(0, Math.Min(text2.Length, 1000))}");
			}
			list2.Add((item2.Item1, StringExtensions.DeserializeJson<JObject>(text2)));
		}
		string text3 = EnumerableExtensions.Join((IEnumerable)list2.Select<(string, JObject), string>([SpecialName] ((string Name, JObject Json) Profile) =>
		{
			JToken obj = Profile.Json["minecraftArguments"];
			return ((obj != null) ? obj.ToString() : null) ?? " ";
		}), " ");
		JObject item = list2[0].Item2;
		foreach (var item3 in list2.Skip(1))
		{
			item3.Item2.Remove("releaseTime");
			item3.Item2.Remove("time");
			((JContainer)item).Merge((object)item3.Item2);
		}
		List<string> list3 = new List<string>();
		foreach (string item4 in from Value in StringExtensions.Split(text3, " ", true)
			select Value.Trim())
		{
			if (!StringExtensions.StartsWithF(item4, "-", false) && list3.Any())
			{
				string text4 = list3.Last();
				if (StringExtensions.StartsWithF(text4, "-", false) && !text4.Contains(" "))
				{
					list3[checked(list3.Count - 1)] = text4 + " " + item4;
					continue;
				}
			}
			list3.Add(item4);
		}
		if (list3.Any())
		{
			item["minecraftArguments"] = JToken.op_Implicit(EnumerableExtensions.Join((IEnumerable)list3.Distinct(), " "));
		}
		item.Remove("_comment_");
		item.Remove("inheritsFrom");
		item.Remove("jar");
		string lastPart = PathUtils.GetLastPart(VersionFolder);
		item["id"] = JToken.op_Implicit(lastPart);
		FileUtils.Write(Path.Combine(VersionFolder, lastPart + ".json"), ((JToken)item).ToString(), (Encoding)null);
		Logger.Info($"版本合并 {lastPart} 完成", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
	}

	public static bool McInstallStart(McInstallRequest Request)
	{
		bool result;
		try
		{
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(Request.NewInstanceName + " 安装", McInstallLoadersGet(Request));
			loaderCombo.OnStateChanged = McInstallState;
			loaderCombo.Start(Request.InstanceFolder);
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.BtnExtraDownload.Ribble();
			result = true;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "开始合并安装失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			result = false;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static bool McModificationStart(McInstallRequest Request, ModMinecraft.McInstance Instance)
	{
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		_Closure_0024__55_002D0 arg = new _Closure_0024__55_002D0(arg);
		arg._0024VB_0024Local_Request = Request;
		arg._0024VB_0024Local_Instance = Instance;
		bool result;
		try
		{
			_Closure_0024__55_002D1 arg2 = default(_Closure_0024__55_002D1);
			_Closure_0024__55_002D1 CS_0024_003C_003E8__locals0 = new _Closure_0024__55_002D1(arg2);
			CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2 = arg;
			CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathIndie;
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			list.Add(new ModLoader.LoaderTask<int, int>("检查 Natives 文件占用", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
			{
				CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._Lambda_0024__0();
			})
			{
				Show = false
			});
			CS_0024_003C_003E8__locals0._0024VB_0024Local_BuildFolder = ModMain.RequestTaskTempFolder() + CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.Name + "\\";
			McInstallRequest request = new McInstallRequest
			{
				NewInstanceName = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.Name,
				InstanceFolder = CS_0024_003C_003E8__locals0._0024VB_0024Local_BuildFolder,
				Components = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.Components,
				MinecraftJson = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.MinecraftJson,
				ForgeEntry = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.ForgeEntry,
				NeoForgeEntry = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.NeoForgeEntry,
				FabricApi = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.FabricApi,
				OptiFabric = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.OptiFabric,
				LiteLoaderEntry = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.LiteLoaderEntry,
				IsIntermediateOutput = true
			};
			list.AddRange(McInstallLoadersGet(request));
			CS_0024_003C_003E8__locals0._0024VB_0024Local_RenameFailed = false;
			list.Add(new ModLoader.LoaderTask<int, int>("修改版本", [SpecialName] (ModLoader.LoaderTask<int, int> Task) =>
			{
				_Closure_0024__55_002D2 arg3 = default(_Closure_0024__55_002D2);
				_Closure_0024__55_002D2 CS_0024_003C_003E8__locals1 = new _Closure_0024__55_002D2(arg3);
				CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_3 = CS_0024_003C_003E8__locals0;
				McInstallCommit(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance, Configs.McInstallManifest.Get((IConfigProvider)(object)new ModMinecraft.McInstance(CS_0024_003C_003E8__locals0._0024VB_0024Local_BuildFolder).Config), CS_0024_003C_003E8__locals0._0024VB_0024Local_BuildFolder, CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder);
				CS_0024_003C_003E8__locals1._0024VB_0024Local_NewInstance = null;
				if (CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.NewInstanceName == null)
				{
					CS_0024_003C_003E8__locals1._0024VB_0024Local_NewInstance = new ModMinecraft.McInstance(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion).Load();
				}
				else
				{
					try
					{
						CS_0024_003C_003E8__locals1._0024VB_0024Local_NewInstance = CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.Rename(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.NewInstanceName);
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						CS_0024_003C_003E8__locals0._0024VB_0024Local_RenameFailed = true;
						Logger.Warn(ex4, $"版本修改成功，但重命名为 {CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Request.NewInstanceName} 失败：{ExceptionExtensions.GetDisplay(ex4, false)}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
						CS_0024_003C_003E8__locals1._0024VB_0024Local_NewInstance = new ModMinecraft.McInstance(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion).Load();
						ProjectData.ClearProjectError();
					}
				}
				if ((object)CS_0024_003C_003E8__locals1._0024VB_0024Local_NewInstance == null)
				{
					CS_0024_003C_003E8__locals1._0024VB_0024Local_NewInstance = new ModMinecraft.McInstance(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion).Load();
				}
				ModBase.RunInUiWait([SpecialName] () =>
				{
					ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
					if ((object)PageInstanceLeft.Instance != null && PathUtils.IsSame(PageInstanceLeft.Instance.PathVersion, CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion))
					{
						PageInstanceLeft.Instance = CS_0024_003C_003E8__locals1._0024VB_0024Local_NewInstance;
					}
					if ((object)ModMinecraft.McInstanceSelected != null && PathUtils.IsSame(ModMinecraft.McInstanceSelected.PathVersion, CS_0024_003C_003E8__locals1._0024VB_0024NonLocal__0024VB_0024Closure_3._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion))
					{
						ModMinecraft.McInstanceSelected = CS_0024_003C_003E8__locals1._0024VB_0024Local_NewInstance;
					}
				});
			})
			{
				ProgressWeight = 1.0,
				Block = true
			});
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader = new ModLoader.LoaderCombo<string>(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.Name + " 修改", list)
			{
				OnStateChanged = [SpecialName] (ModLoader.LoaderBase CurrentLoader) =>
				{
					switch (CurrentLoader.State)
					{
					case ModBase.LoadState.Finished:
						if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_RenameFailed)
						{
							ModMain.Hint(CurrentLoader.Name + "成功！", ModMain.HintType.Green);
						}
						else
						{
							ModMain.Hint(CurrentLoader.Name + "成功，但重命名失败！", ModMain.HintType.Green);
						}
						break;
					case ModBase.LoadState.Failed:
						ModMain.MyMsgBox(ExceptionExtensions.GetDisplay(CurrentLoader.Error, true), CurrentLoader.Name + "失败", "确定", "", "", IsWarn: true);
						break;
					case ModBase.LoadState.Canceled:
						ModMain.Hint(CurrentLoader.Name + "已取消！");
						break;
					}
				}
			};
			RedoableWorker val = new RedoableWorker((Action)([SpecialName] () =>
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.WaitForExit(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion);
			}), "McModificationStart");
			if (ModMinecraft.McInstanceLock.TryAcquire((IWorker)(object)val, CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Instance.PathVersion))
			{
				((RedoableWorkerBase<NoType>)(object)val).Stopped += [SpecialName] () =>
				{
					ModBase.RunInUi([SpecialName] () =>
					{
						ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
					});
				};
				((RedoableWorkerBase<NoType>)(object)val).Start(default(CancellationToken), (ProgressProvider)null);
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
				ModLoader.LoaderTaskbarAdd(CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader);
				ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
				ModMain.FrmMain.BtnExtraDownload.Ribble();
				result = true;
			}
			else
			{
				Logger.Error("版本被其他 Worker 占用，版本修改失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				result = false;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			Logger.Error(ex2, "开始修改版本失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
			result = false;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static void McInstallCommit(ModMinecraft.McInstance Instance, McInstallManifest NewManifest, string BuildFolder, string TargetFolder)
	{
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Expected O, but got Unknown
		_Closure_0024__56_002D0 arg = default(_Closure_0024__56_002D0);
		_Closure_0024__56_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__56_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder = TargetFolder;
		McInstallManifest obj = Configs.McInstallManifest.Get((IConfigProvider)(object)Instance.Config);
		IReadOnlyList<ManagedFile> readOnlyList = (IReadOnlyList<ManagedFile>)(((object)((obj != null) ? obj.ManagedFiles : null)) ?? ((object)new ManagedFile[0]));
		IReadOnlyList<ManagedFile> managedFiles = NewManifest.ManagedFiles;
		List<string> list = new List<string>();
		List<(string, string)> list2 = new List<(string, string)>();
		foreach (ManagedFile item in managedFiles)
		{
			list2.Add((Path.Combine(BuildFolder, item.RelativePath), Path.Combine(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder, item.RelativePath)));
		}
		HashSet<string> hashSet = new HashSet<string>(managedFiles.Select([SpecialName] (ManagedFile File) => File.RelativePath));
		foreach (ManagedFile item2 in readOnlyList)
		{
			if (!hashSet.Contains(item2.RelativePath))
			{
				list.Add(PathUtils.ForCompare(Path.Combine(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder, item2.RelativePath)));
			}
		}
		if (DirectoryUtils.Exists(BuildFolder + "resources\\"))
		{
			DirectoryUtils.Copy(BuildFolder + "resources\\", CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder + "resources\\");
		}
		string jsonPath = Instance.GetJsonPath();
		list.Add(jsonPath);
		list.Add(Path.ChangeExtension(jsonPath, ".jar"));
		list.Add(Instance.PathVersion + Instance.Name + ".jar");
		list = list.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
		Instance.Config.Save();
		foreach (string item3 in list)
		{
			FileUtils.Delete(item3, true);
		}
		FileUtils.Move(BuildFolder + Instance.Name + ".jar", Instance.PathVersion + Instance.Name + ".jar", false);
		FileUtils.Move(BuildFolder + Instance.Name + ".json", jsonPath, false);
		foreach (var item4 in list2)
		{
			DirectoryUtils.Create(PathUtils.RemoveLastPart(item4.Item2));
			FileUtils.Copy(item4.Item1, item4.Item2);
		}
		List<ManagedFile> list3 = ((IEnumerable<ManagedFile>)managedFiles).Select((Func<ManagedFile, ManagedFile>)([SpecialName] (ManagedFile File) => new ManagedFile(File.RelativePath, File.Sha1, FileUtils.GetInfo(Path.Combine(CS_0024_003C_003E8__locals0._0024VB_0024Local_TargetFolder, File.RelativePath)).LastWriteTimeUtc, File.Component))).ToList();
		Configs.McInstallManifest.Set(new McInstallManifest(NewManifest.SchemaVersion, NewManifest.Components, NewManifest.LastModifiedUtc, (IReadOnlyList<ManagedFile>)list3), (IConfigProvider)(object)Instance.Config);
		Instance.Config.Save();
	}

	public static void LoaderStateChangedHintOnly(ModLoader.LoaderBase Loader)
	{
		switch (Loader.State)
		{
		case ModBase.LoadState.Finished:
			ModMain.Hint(Loader.Name + "成功！", ModMain.HintType.Green);
			break;
		case ModBase.LoadState.Failed:
			ModMain.Hint(Loader.Name + "失败：" + ExceptionExtensions.GetDisplay(Loader.Error, false), ModMain.HintType.Red);
			break;
		case ModBase.LoadState.Canceled:
			ModMain.Hint(Loader.Name + "已取消！");
			break;
		}
	}

	public static void McInstallState(ModLoader.LoaderBase Loader)
	{
		switch (Loader.State)
		{
		case ModBase.LoadState.Finished:
			ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
			ModMain.Hint(Loader.Name + "成功！", ModMain.HintType.Green);
			break;
		case ModBase.LoadState.Failed:
			ModMain.MyMsgBox(ExceptionExtensions.GetDisplay(Loader.Error, true), Loader.Name + "失败", "确定", "", "", IsWarn: true);
			break;
		case ModBase.LoadState.Canceled:
			ModMain.Hint(Loader.Name + "已取消！");
			break;
		case ModBase.LoadState.Loading:
			return;
		}
		McInstallFailedClearFolder(Loader);
		ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
	}

	public static void McInstallFailedClearFolder(object Loader)
	{
		if (Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(Loader, null, "State", new object[0], null, null, null), ModBase.LoadState.Failed, TextCompare: false) && Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(Loader, null, "State", new object[0], null, null, null), ModBase.LoadState.Canceled, TextCompare: false))
		{
			return;
		}
		ModBase.RunInThread([SpecialName] () =>
		{
			try
			{
				Thread.Sleep(1000);
				if (DirectoryUtils.Exists(Conversions.ToString(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null), "saves\\"))) || DirectoryUtils.Exists(Conversions.ToString(Operators.ConcatenateObject(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null), "versions\\"))))
				{
					Logger.Warn(string.Format("由于版本已被独立启动，不清理版本文件夹：{0}", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null))), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				}
				else
				{
					Logger.Warn(string.Format("由于下载失败或取消，清理版本文件夹：{0}", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null))), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
					DirectoryUtils.Delete(Conversions.ToString(NewLateBinding.LateGet(Loader, null, "Input", new object[0], null, null, null)), false);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "下载失败或取消后清理版本文件夹失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\ModDownloadLib.vb");
				ProjectData.ClearProjectError();
			}
		});
	}
}
