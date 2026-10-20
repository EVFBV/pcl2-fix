using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCLCS;

namespace PCL;

public class PageDownloadModify : PageDownloadInstall
{
	[Serializable]
	[CompilerGenerated]
	internal new sealed class _Closure_0024__
	{
		public static readonly _Closure_0024__ _0024I;

		public static Func<ModMinecraft.McInstance> _0024I9_002D0;

		public static Func<KeyValuePair<ModLoaders, string>, bool> _0024I12_002D1;

		public static Func<KeyValuePair<ModLoaders, string>, string> _0024I12_002D2;

		public static Func<ModLoader.LoaderBase, bool> _0024I12_002D3;

		public static Func<ModLoader.LoaderBase, double> _0024I12_002D4;

		public static Func<ResourceVersion, bool> _0024I12_002D20;

		public static Func<(FileInfo File, string Sha1, string OriginalName), bool> _0024I12_002D24;

		public static Func<ModDownload.DlOptiFineListEntry, bool> _0024I17_002D4;

		public static Func<ModDownload.DlOptiFineListEntry, string> _0024I17_002D5;

		public static Func<ModDownload.DlForgeVersionEntry, ModDownload.DlForgeVersionEntry> _0024I17_002D8;

		public static Func<ResourceVersion, DateTime> _0024I17_002D15;

		public static Func<ResourceVersion, DateTime> _0024I17_002D18;

		static _Closure_0024__()
		{
			_0024I = new _Closure_0024__();
		}

		[SpecialName]
		internal ModMinecraft.McInstance _Lambda_0024__9_002D0()
		{
			return PageInstanceLeft.Instance;
		}

		[SpecialName]
		internal bool _Lambda_0024__12_002D1(KeyValuePair<ModLoaders, string> Loader)
		{
			if (!string.IsNullOrWhiteSpace(Loader.Value))
			{
				return Operators.CompareString(Loader.Value, "未知版本", TextCompare: false) == 0;
			}
			return true;
		}

		[SpecialName]
		internal string _Lambda_0024__12_002D2(KeyValuePair<ModLoaders, string> Loader)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			ModLoaders key = Loader.Key;
			return ((Enum)(ModLoaders)(ref key)).ToString();
		}

		[SpecialName]
		internal bool _Lambda_0024__12_002D3(ModLoader.LoaderBase Loader)
		{
			if (Loader.State != ModBase.LoadState.Finished)
			{
				return Loader.State == ModBase.LoadState.Failed;
			}
			return true;
		}

		[SpecialName]
		internal double _Lambda_0024__12_002D4(ModLoader.LoaderBase Loader)
		{
			return Loader.Progress;
		}

		[SpecialName]
		internal bool _Lambda_0024__12_002D20(ResourceVersion v)
		{
			return (v.Hash ?? "").Length == 32;
		}

		[SpecialName]
		internal bool _Lambda_0024__12_002D24((FileInfo File, string Sha1, string OriginalName) Entry)
		{
			return Entry.OriginalName != null;
		}

		[SpecialName]
		internal bool _Lambda_0024__17_002D4(ModDownload.DlOptiFineListEntry v)
		{
			return v.IsPreview;
		}

		[SpecialName]
		internal string _Lambda_0024__17_002D5(ModDownload.DlOptiFineListEntry v)
		{
			return v.DisplayName;
		}

		[SpecialName]
		internal ModDownload.DlForgeVersionEntry _Lambda_0024__17_002D8(ModDownload.DlForgeVersionEntry v)
		{
			return v;
		}

		[SpecialName]
		internal DateTime _Lambda_0024__17_002D15(ResourceVersion v)
		{
			return v.ReleaseDate;
		}

		[SpecialName]
		internal DateTime _Lambda_0024__17_002D18(ResourceVersion v)
		{
			return v.ReleaseDate;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__12_002D0
	{
		public string _0024VB_0024Local_MinecraftVersion;

		public string _0024VB_0024Local_OptiFineVersion;

		public McComponents _0024VB_0024Local_Components;

		public ModLoader.LoaderTask<ModMinecraft.McInstance, object> _0024VB_0024Local_Task;

		public PageDownloadModify _0024VB_0024Me;

		public _Closure_0024__12_002D0(_Closure_0024__12_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_MinecraftVersion = arg0._0024VB_0024Local_MinecraftVersion;
				_0024VB_0024Local_OptiFineVersion = arg0._0024VB_0024Local_OptiFineVersion;
				_0024VB_0024Local_Components = arg0._0024VB_0024Local_Components;
				_0024VB_0024Local_Task = arg0._0024VB_0024Local_Task;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__5(JObject v)
		{
			return Operators.CompareString(v["id"].ToString().Replace("∞", "infinite"), _0024VB_0024Local_MinecraftVersion.Replace("∞", "infinite"), TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__7(ModDownload.DlOptiFineListEntry v)
		{
			if (Operators.CompareString(v.Inherit, _0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0)
			{
				return Operators.CompareString(StringExtensions.AfterLast(v.InstanceName, "-OptiFine_", false).Replace("HD_U_", ""), _0024VB_0024Local_OptiFineVersion.Replace("HD_U_", ""), TextCompare: false) == 0;
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__8(ModDownload.DlForgeVersionEntry v)
		{
			if (Operators.CompareString(v.Inherit, _0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0 && Operators.CompareString(v.VersionName, _0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)1], TextCompare: false) == 0 && Operators.CompareString(v.Category, "universal", TextCompare: false) != 0 && Operators.CompareString(v.Category, "client", TextCompare: false) != 0)
			{
				if (_0024VB_0024Me.SelectedOptiFine != null)
				{
					return PageDownloadInstall.IsOptiFineSuitForForge(_0024VB_0024Me.SelectedOptiFine, v);
				}
				return true;
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__9(ModDownload.DlNeoForgeListEntry v)
		{
			if (Operators.CompareString(v.Inherit, _0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0)
			{
				return Operators.CompareString(v.VersionName, _0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)16], TextCompare: false) == 0;
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__10(JObject v)
		{
			return Operators.CompareString(v["version"].ToString(), _0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)4], TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__11(JObject v)
		{
			return Operators.CompareString(v["version"].ToString().Replace("+build", ""), _0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)4], TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__12(JObject v)
		{
			return Operators.CompareString(v["version"].ToString(), _0024VB_0024Local_MinecraftVersion.Replace("∞", "infinite").Replace("Combat Test 7c", "1.16_combat-3"), TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__13(ModDownload.DlLiteLoaderListEntry v)
		{
			if (Operators.CompareString(v.Inherit, _0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0)
			{
				return Operators.CompareString(v.JsonToken[(object)"version"].ToString(), _0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)2], TextCompare: false) == 0;
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__14(ResourceVersion v)
		{
			if (Operators.CompareString(v.Version, _0024VB_0024Local_Components.FabricApiVersion, TextCompare: false) == 0 || Operators.CompareString(v.Id, _0024VB_0024Local_Components.FabricApiVersion, TextCompare: false) == 0 || Operators.CompareString(v.Display, _0024VB_0024Local_Components.FabricApiVersion, TextCompare: false) == 0)
			{
				return PageDownloadInstall.IsFabricApiCompatible(v, _0024VB_0024Local_MinecraftVersion);
			}
			return false;
		}

		[SpecialName]
		internal bool _Lambda_0024__15(ResourceVersion v)
		{
			if (Operators.CompareString(v.Version, _0024VB_0024Local_Components.OptiFabricVersion, TextCompare: false) == 0 || Operators.CompareString(v.Id, _0024VB_0024Local_Components.OptiFabricVersion, TextCompare: false) == 0 || Operators.CompareString(v.Display, _0024VB_0024Local_Components.OptiFabricVersion, TextCompare: false) == 0)
			{
				return PageDownloadInstall.IsOptiFabricCompatible(v, _0024VB_0024Local_MinecraftVersion);
			}
			return false;
		}

		[SpecialName]
		internal List<(FileInfo, string)> _Lambda_0024__16()
		{
			string text = _0024VB_0024Local_Task.Input.PathIndie + "mods\\";
			List<string> list = DirectoryUtils.EnumerateFiles(text, false, "*.jar").ToList();
			if (_0024VB_0024Me.SelectedLiteLoader != null)
			{
				list.AddRange(DirectoryUtils.EnumerateFiles(Path.Combine(text, _0024VB_0024Local_MinecraftVersion), false, "*.jar"));
			}
			List<(FileInfo, string)> list2 = new List<(FileInfo, string)>();
			foreach (string item in list)
			{
				if (_0024VB_0024Local_Task.IsCanceled)
				{
					return list2;
				}
				FileInfo info = FileUtils.GetInfo(item);
				list2.Add((info, new LocalResourceFile(info).ModrinthHash));
				_0024VB_0024Local_Task.Progress = 0.7 + 0.28 * (double)list2.Count / (double)list.Count;
			}
			return list2;
		}

		[SpecialName]
		internal bool _Lambda_0024__17(ResourceVersion v)
		{
			return PageDownloadInstall.IsFabricApiCompatible(v, _0024VB_0024Local_MinecraftVersion);
		}

		[SpecialName]
		internal bool _Lambda_0024__19(ResourceVersion v)
		{
			return PageDownloadInstall.IsOptiFabricCompatible(v, _0024VB_0024Local_MinecraftVersion);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__12_002D1
	{
		public (FileInfo File, string Sha1) _0024VB_0024Local_Entry;

		public _Closure_0024__12_002D1(_Closure_0024__12_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Entry = arg0._0024VB_0024Local_Entry;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__18(ResourceVersion v)
		{
			return Operators.CompareString(v.Hash, _0024VB_0024Local_Entry.Sha1, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__12_002D2
	{
		public (FileInfo File, string Sha1) _0024VB_0024Local_Entry;

		public _Closure_0024__12_002D2(_Closure_0024__12_002D2 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Entry = arg0._0024VB_0024Local_Entry;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__21(ResourceVersion v)
		{
			return Operators.CompareString(v.Hash, _0024VB_0024Local_Entry.Sha1, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__12_002D3
	{
		public string _0024VB_0024Local_Md5;

		public _Closure_0024__12_002D3(_Closure_0024__12_002D3 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Md5 = arg0._0024VB_0024Local_Md5;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__22(ResourceVersion v)
		{
			return Operators.CompareString(v.Hash, _0024VB_0024Local_Md5, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__12_002D4
	{
		public (FileInfo File, string Sha1, string OriginalName) _0024VB_0024Local_Entry;

		public _Closure_0024__12_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__12_002D4(_Closure_0024__12_002D4 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Entry = arg0._0024VB_0024Local_Entry;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__25(ModDownload.DlOptiFineListEntry v)
		{
			if (Operators.CompareString(v.Inherit, _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0 && Operators.CompareString(v.FileName, _0024VB_0024Local_Entry.OriginalName, TextCompare: false) == 0)
			{
				if (_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.SelectedForge != null)
				{
					return PageDownloadInstall.IsOptiFineSuitForForge(v, _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.SelectedForge);
				}
				return true;
			}
			return false;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__17_002D0
	{
		public string _0024VB_0024Local_Version;

		public _Closure_0024__17_002D0(_Closure_0024__17_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Version = arg0._0024VB_0024Local_Version;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__1(JObject v)
		{
			return Operators.CompareString(v["version"].ToString(), _0024VB_0024Local_Version, TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__3(ModDownload.DlOptiFineListEntry v)
		{
			return Operators.CompareString(StringExtensions.AfterLast(v.InstanceName, "-OptiFine_", false), _0024VB_0024Local_Version, TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__7(ModDownload.DlForgeVersionEntry v)
		{
			return Operators.CompareString(v.VersionName, _0024VB_0024Local_Version, TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__10(ModDownload.DlNeoForgeListEntry v)
		{
			return Operators.CompareString(v.VersionName, _0024VB_0024Local_Version, TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__12(ModDownload.DlLiteLoaderListEntry v)
		{
			return Operators.CompareString(v.JsonToken[(object)"version"].ToString(), _0024VB_0024Local_Version, TextCompare: false) == 0;
		}

		[SpecialName]
		internal bool _Lambda_0024__14(ResourceVersion v)
		{
			if (Operators.CompareString(v.Version, _0024VB_0024Local_Version, TextCompare: false) != 0 && Operators.CompareString(v.Id, _0024VB_0024Local_Version, TextCompare: false) != 0)
			{
				return Operators.CompareString(v.Display, _0024VB_0024Local_Version, TextCompare: false) == 0;
			}
			return true;
		}

		[SpecialName]
		internal bool _Lambda_0024__17(ResourceVersion v)
		{
			if (Operators.CompareString(v.Version, _0024VB_0024Local_Version, TextCompare: false) != 0 && Operators.CompareString(v.Id, _0024VB_0024Local_Version, TextCompare: false) != 0)
			{
				return Operators.CompareString(v.Display, _0024VB_0024Local_Version, TextCompare: false) == 0;
			}
			return true;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__18_002D0
	{
		public string _0024VB_0024Local_OldVersion;

		public _Closure_0024__18_002D0(_Closure_0024__18_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_OldVersion = arg0._0024VB_0024Local_OldVersion;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__0(JObject v)
		{
			return Operators.CompareString(v["version"].ToString(), _0024VB_0024Local_OldVersion, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__18_002D1
	{
		public string _0024VB_0024Local_TargetVersion;

		public _Closure_0024__18_002D1(_Closure_0024__18_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_TargetVersion = arg0._0024VB_0024Local_TargetVersion;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__1(JObject v)
		{
			return Operators.CompareString(v["version"].ToString(), _0024VB_0024Local_TargetVersion, TextCompare: false) == 0;
		}
	}

	private ModLoader.LoaderTask<ModMinecraft.McInstance, object> InitLoader;

	private ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> ForgeVersionLoader;

	private McComponents InitialTarget;

	private bool IsVanillaVersionReliable;

	private string OriginalName;

	private string CustomLogo;

	private readonly Dictionary<MyLoading, string> PendingComponents;

	private bool IsRestoringComponents;

	[SpecialName]
	private bool _0024STATIC_0024Init_00242001_0024IsLoad;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024Init_00242001_0024IsLoad_0024Init;

	[SpecialName]
	private JObject _0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes_0024Init;

	internal override MyIconButton BtnMinecraftRestore
	{
		[CompilerGenerated]
		get
		{
			return base.BtnMinecraftRestore;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = RestoreComponent;
			MyIconButton btnMinecraftRestore = base.BtnMinecraftRestore;
			if (btnMinecraftRestore != null)
			{
				btnMinecraftRestore.Click -= obj;
			}
			base.BtnMinecraftRestore = value;
			btnMinecraftRestore = base.BtnMinecraftRestore;
			if (btnMinecraftRestore != null)
			{
				btnMinecraftRestore.Click += obj;
			}
		}
	}

	internal override MyIconButton BtnOptiFineRestore
	{
		[CompilerGenerated]
		get
		{
			return base.BtnOptiFineRestore;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = RestoreComponent;
			MyIconButton btnOptiFineRestore = base.BtnOptiFineRestore;
			if (btnOptiFineRestore != null)
			{
				btnOptiFineRestore.Click -= obj;
			}
			base.BtnOptiFineRestore = value;
			btnOptiFineRestore = base.BtnOptiFineRestore;
			if (btnOptiFineRestore != null)
			{
				btnOptiFineRestore.Click += obj;
			}
		}
	}

	internal override MyIconButton BtnForgeRestore
	{
		[CompilerGenerated]
		get
		{
			return base.BtnForgeRestore;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = RestoreComponent;
			MyIconButton btnForgeRestore = base.BtnForgeRestore;
			if (btnForgeRestore != null)
			{
				btnForgeRestore.Click -= obj;
			}
			base.BtnForgeRestore = value;
			btnForgeRestore = base.BtnForgeRestore;
			if (btnForgeRestore != null)
			{
				btnForgeRestore.Click += obj;
			}
		}
	}

	internal override MyIconButton BtnNeoForgeRestore
	{
		[CompilerGenerated]
		get
		{
			return base.BtnNeoForgeRestore;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = RestoreComponent;
			MyIconButton btnNeoForgeRestore = base.BtnNeoForgeRestore;
			if (btnNeoForgeRestore != null)
			{
				btnNeoForgeRestore.Click -= obj;
			}
			base.BtnNeoForgeRestore = value;
			btnNeoForgeRestore = base.BtnNeoForgeRestore;
			if (btnNeoForgeRestore != null)
			{
				btnNeoForgeRestore.Click += obj;
			}
		}
	}

	internal override MyIconButton BtnFabricRestore
	{
		[CompilerGenerated]
		get
		{
			return base.BtnFabricRestore;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = RestoreComponent;
			MyIconButton btnFabricRestore = base.BtnFabricRestore;
			if (btnFabricRestore != null)
			{
				btnFabricRestore.Click -= obj;
			}
			base.BtnFabricRestore = value;
			btnFabricRestore = base.BtnFabricRestore;
			if (btnFabricRestore != null)
			{
				btnFabricRestore.Click += obj;
			}
		}
	}

	internal override MyIconButton BtnFabricApiRestore
	{
		[CompilerGenerated]
		get
		{
			return base.BtnFabricApiRestore;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = RestoreComponent;
			MyIconButton btnFabricApiRestore = base.BtnFabricApiRestore;
			if (btnFabricApiRestore != null)
			{
				btnFabricApiRestore.Click -= obj;
			}
			base.BtnFabricApiRestore = value;
			btnFabricApiRestore = base.BtnFabricApiRestore;
			if (btnFabricApiRestore != null)
			{
				btnFabricApiRestore.Click += obj;
			}
		}
	}

	internal override MyIconButton BtnOptiFabricRestore
	{
		[CompilerGenerated]
		get
		{
			return base.BtnOptiFabricRestore;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = RestoreComponent;
			MyIconButton btnOptiFabricRestore = base.BtnOptiFabricRestore;
			if (btnOptiFabricRestore != null)
			{
				btnOptiFabricRestore.Click -= obj;
			}
			base.BtnOptiFabricRestore = value;
			btnOptiFabricRestore = base.BtnOptiFabricRestore;
			if (btnOptiFabricRestore != null)
			{
				btnOptiFabricRestore.Click += obj;
			}
		}
	}

	internal override MyIconButton BtnLiteLoaderRestore
	{
		[CompilerGenerated]
		get
		{
			return base.BtnLiteLoaderRestore;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = RestoreComponent;
			MyIconButton btnLiteLoaderRestore = base.BtnLiteLoaderRestore;
			if (btnLiteLoaderRestore != null)
			{
				btnLiteLoaderRestore.Click -= obj;
			}
			base.BtnLiteLoaderRestore = value;
			btnLiteLoaderRestore = base.BtnLiteLoaderRestore;
			if (btnLiteLoaderRestore != null)
			{
				btnLiteLoaderRestore.Click += obj;
			}
		}
	}

	internal override MyCard CardMinecraft
	{
		[CompilerGenerated]
		get
		{
			return base.CardMinecraft;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = CardMinecraft_MouseLeftButtonUp;
			MyCard cardMinecraft = base.CardMinecraft;
			if (cardMinecraft != null)
			{
				cardMinecraft.MouseLeftButtonUp -= value2;
			}
			base.CardMinecraft = value;
			cardMinecraft = base.CardMinecraft;
			if (cardMinecraft != null)
			{
				cardMinecraft.MouseLeftButtonUp += value2;
			}
		}
	}

	internal override MyTextBox TextSelectName
	{
		[CompilerGenerated]
		get
		{
			return base.TextSelectName;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TextChangedEventHandler value2 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
			{
				UpdateStartButton();
			};
			MyTextBox.ValidateChangedEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				UpdateStartButton();
			};
			TextChangedEventHandler value3 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
			{
				UpdateOriginalNameVisibility();
			};
			MyTextBox textSelectName = base.TextSelectName;
			if (textSelectName != null)
			{
				textSelectName.TextChanged -= value2;
				MyTextBox.ValidateChanged -= obj;
				textSelectName.TextChanged -= value3;
			}
			base.TextSelectName = value;
			textSelectName = base.TextSelectName;
			if (textSelectName != null)
			{
				textSelectName.TextChanged += value2;
				MyTextBox.ValidateChanged += obj;
				textSelectName.TextChanged += value3;
			}
		}
	}

	internal override MyIconButton BtnOriginalNameRestore
	{
		[CompilerGenerated]
		get
		{
			return base.BtnOriginalNameRestore;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = BtnOriginalNameRestore_Click;
			MyIconButton btnOriginalNameRestore = base.BtnOriginalNameRestore;
			if (btnOriginalNameRestore != null)
			{
				btnOriginalNameRestore.Click -= obj;
			}
			base.BtnOriginalNameRestore = value;
			btnOriginalNameRestore = base.BtnOriginalNameRestore;
			if (btnOriginalNameRestore != null)
			{
				btnOriginalNameRestore.Click += obj;
			}
		}
	}

	public PageDownloadModify()
	{
		base.Unloaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			PageUnloaded();
		};
		PendingComponents = new Dictionary<MyLoading, string>();
	}

	public override void LoaderInit()
	{
		ControlInit();
		InitLoader = new ModLoader.LoaderTask<ModMinecraft.McInstance, object>("初始化版本修改", LoadInstance);
		PageLoaderInit(LoadMinecraft, PanLoad, PanAllBack, null, InitLoader, LoadInstanceFinished, [SpecialName] () => PageInstanceLeft.Instance);
	}

	public override void Init()
	{
		PanBack.ScrollToHome();
		if (_0024STATIC_0024Init_00242001_0024IsLoad_0024Init == null)
		{
			Interlocked.CompareExchange(ref _0024STATIC_0024Init_00242001_0024IsLoad_0024Init, new StaticLocalInitFlag(), null);
		}
		bool lockTaken = false;
		try
		{
			Monitor.Enter(_0024STATIC_0024Init_00242001_0024IsLoad_0024Init, ref lockTaken);
			if (_0024STATIC_0024Init_00242001_0024IsLoad_0024Init.State == 0)
			{
				_0024STATIC_0024Init_00242001_0024IsLoad_0024Init.State = 2;
				_0024STATIC_0024Init_00242001_0024IsLoad = false;
			}
			else if (_0024STATIC_0024Init_00242001_0024IsLoad_0024Init.State == 2)
			{
				throw new IncompleteInitialization();
			}
		}
		finally
		{
			_0024STATIC_0024Init_00242001_0024IsLoad_0024Init.State = 1;
			if (lockTaken)
			{
				Monitor.Exit(_0024STATIC_0024Init_00242001_0024IsLoad_0024Init);
			}
		}
		if (_0024STATIC_0024Init_00242001_0024IsLoad)
		{
			PageLoaderRestart();
		}
		_0024STATIC_0024Init_00242001_0024IsLoad = true;
	}

	private void PageUnloaded()
	{
		InitLoader.Cancel();
	}

	private void LoadInstance(ModLoader.LoaderTask<ModMinecraft.McInstance, object> Task)
	{
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Expected O, but got Unknown
		//IL_09f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fd: Expected O, but got Unknown
		//IL_0c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5c: Expected O, but got Unknown
		//IL_0bb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbc: Expected O, but got Unknown
		//IL_0f07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f11: Expected O, but got Unknown
		//IL_0f49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f63: Expected O, but got Unknown
		//IL_0e3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e48: Expected O, but got Unknown
		_Closure_0024__12_002D0 arg = default(_Closure_0024__12_002D0);
		_Closure_0024__12_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__12_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Me = this;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Task = Task;
		ModBase.RunInUiWait([SpecialName] () =>
		{
			ModAnimation.AniStop("FrmDownloadInstall SelectPageSwitch");
			IsSelectNameEdited = false;
			ClearSelected();
			PendingComponents.Clear();
			InitialTarget = null;
			IsInSelectPage = false;
			PanAllBack.IsHitTestVisible = false;
		});
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.Config.DiscardCache();
		McInstallManifest val = Configs.McInstallManifest.Get((IConfigProvider)(object)CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.Config);
		if (val != null && val.SchemaVersion > 1)
		{
			Logger.Warn($"安装清单由更新版本的 PCL 创建（清单版本：{val.SchemaVersion}，当前版本：{1}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadModify.vb");
			throw new Exception("更新 PCL 后才能修改它！点击【设置 → 其他 → 检查更新】以更新。");
		}
		if (val != null)
		{
			McInstallManifest val2 = val.RefreshModComponents(CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.PathIndie);
			if (val2 != val)
			{
				val = val2;
				Configs.McInstallManifest.Set(val, (IConfigProvider)(object)CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.Config);
				CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.Config.Save();
			}
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineVersion = null;
		if (val != null)
		{
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Components = val.Components;
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Components == null)
			{
				throw new Exception("安装清单缺少组件信息，无法还原已安装组件。");
			}
			IsVanillaVersionReliable = true;
		}
		else
		{
			McVersion version = new ModMinecraft.McInstance(CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.PathVersion).Version;
			if (Operators.CompareString(version.VanillaName, "Unknown", TextCompare: false) == 0)
			{
				throw new Exception("无法确定 Minecraft 版本，无法修改");
			}
			List<string> list = version.ModLoaderVersions.Where([SpecialName] (KeyValuePair<ModLoaders, string> Loader) => string.IsNullOrWhiteSpace(Loader.Value) || Operators.CompareString(Loader.Value, "未知版本", TextCompare: false) == 0).Select([SpecialName] (KeyValuePair<ModLoaders, string> Loader) =>
			{
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				ModLoaders key = Loader.Key;
				return ((Enum)(ModLoaders)(ref key)).ToString();
			}).ToList();
			if (list.Any())
			{
				throw new Exception(string.Format("无法确定 {0} 的版本，无法修改", EnumerableExtensions.Join((IEnumerable)list, "、")));
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Components = new McComponents(version.VanillaName, version.ModLoaderVersions, (JObject)null, (string)null, (string)null);
			CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineVersion = version.OptiFine;
			IsVanillaVersionReliable = version.Reliable;
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion = CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.MinecraftVersion;
		if (string.IsNullOrWhiteSpace(CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion))
		{
			throw new Exception("无法确定当前 Minecraft 版本，请检查版本文件");
		}
		Dictionary<string, ModLoader.LoaderBase> dictionary = new Dictionary<string, ModLoader.LoaderBase>
		{
			{
				"Minecraft",
				ModDownload.DlClientListLoader
			},
			{
				"OptiFine",
				ModDownload.DlOptiFineListLoader
			},
			{
				"LiteLoader",
				ModDownload.DlLiteLoaderListLoader
			},
			{
				"NeoForge",
				ModDownload.DlNeoForgeListLoader
			},
			{
				"Fabric",
				ModDownload.DlFabricListLoader
			},
			{
				"Fabric API",
				ModDownload.DlFabricApiLoader
			},
			{
				"OptiFabric",
				ModDownload.DlOptiFabricLoader
			}
		};
		ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = null;
		if (McVersion.IsStandardFormat(CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion))
		{
			loaderTask = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("DlForgeVersion " + CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion, ModDownload.DlForgeVersionMain);
			dictionary.Add("Forge", loaderTask);
		}
		foreach (ModLoader.LoaderBase value2 in dictionary.Values)
		{
			value2.Start((value2 == loaderTask) ? CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion : null, CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.IsForceRestarting);
		}
		_Closure_0024__12_002D1 closure_0024__12_002D = default(_Closure_0024__12_002D1);
		_Closure_0024__12_002D2 closure_0024__12_002D2 = default(_Closure_0024__12_002D2);
		_Closure_0024__12_002D3 closure_0024__12_002D3 = default(_Closure_0024__12_002D3);
		_Closure_0024__12_002D4 closure_0024__12_002D4 = default(_Closure_0024__12_002D4);
		while (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.IsCanceled)
		{
			foreach (KeyValuePair<string, ModLoader.LoaderBase> item3 in dictionary)
			{
				ModLoader.LoaderBase value = item3.Value;
				if (value.State == ModBase.LoadState.Failed)
				{
					if (value != loaderTask || Operators.CompareString(value.Error.Message, "无", TextCompare: false) != 0)
					{
						throw new Exception($"获取 {item3.Key} 版本列表失败，请稍后再试，或使用 VPN 改善网络环境", value.Error);
					}
				}
				else if (value.State == ModBase.LoadState.Canceled)
				{
					throw new Exception($"获取 {item3.Key} 版本列表被中断，请稍后再试，或使用 VPN 改善网络环境");
				}
			}
			if (!dictionary.Values.All([SpecialName] (ModLoader.LoaderBase Loader) => Loader.State == ModBase.LoadState.Finished || Loader.State == ModBase.LoadState.Failed))
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Progress = 0.7 * dictionary.Values.Average((_Closure_0024__._0024I12_002D4 == null) ? (_Closure_0024__._0024I12_002D4 = [SpecialName] (ModLoader.LoaderBase Loader) => Loader.Progress) : _Closure_0024__._0024I12_002D4);
				Thread.Sleep(10);
				continue;
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.IsCanceled)
			{
				break;
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Progress = 0.7;
			MinecraftVersions = (JArray)ModDownload.DlClientListLoader.Output.Value["versions"].DeepClone();
			VanillaData = ((IEnumerable)MinecraftVersions).OfType<JObject>().FirstOrDefault([SpecialName] (JObject v) => Operators.CompareString(v["id"].ToString().Replace("∞", "infinite"), CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion.Replace("∞", "infinite"), TextCompare: false) == 0);
			if (VanillaData == null)
			{
				throw new Exception($"Minecraft 列表中没有已安装的 {CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion}");
			}
			VanillaData["id"] = JToken.op_Implicit(CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion);
			if (val != null)
			{
				JObject optiFineEntry = CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.OptiFineEntry;
				SelectedOptiFine = ((optiFineEntry != null) ? ((JToken)optiFineEntry).ToObject<ModDownload.DlOptiFineListEntry>() : null);
				if (SelectedOptiFine != null && !ModDownload.DlOptiFineListLoader.Output.Value.Any([SpecialName] (ModDownload.DlOptiFineListEntry v) => Operators.CompareString(v.FileName, SelectedOptiFine.FileName, TextCompare: false) == 0))
				{
					if (ModDownload.DlOptiFineListBmclapiLoader.State != ModBase.LoadState.Finished)
					{
						ModDownload.DlOptiFineListBmclapiLoader.WaitForExit();
					}
					ModDownload.DlOptiFineListLoader.Output = ModDownload.DlOptiFineListBmclapiLoader.Output;
				}
			}
			else if (CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineVersion != null && Operators.CompareString(CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineVersion, "未知版本", TextCompare: false) != 0)
			{
				SelectedOptiFine = ModDownload.DlOptiFineListLoader.Output.Value.FirstOrDefault([SpecialName] (ModDownload.DlOptiFineListEntry v) => Operators.CompareString(v.Inherit, CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0 && Operators.CompareString(StringExtensions.AfterLast(v.InstanceName, "-OptiFine_", false).Replace("HD_U_", ""), CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineVersion.Replace("HD_U_", ""), TextCompare: false) == 0);
				if (SelectedOptiFine == null)
				{
					throw new Exception($"OptiFine 列表中没有已安装的 {CS_0024_003C_003E8__locals0._0024VB_0024Local_OptiFineVersion}");
				}
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions.ContainsKey((ModLoaders)1))
			{
				SelectedForge = loaderTask?.Output?.FirstOrDefault([SpecialName] (ModDownload.DlForgeVersionEntry v) => Operators.CompareString(v.Inherit, CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0 && Operators.CompareString(v.VersionName, CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)1], TextCompare: false) == 0 && Operators.CompareString(v.Category, "universal", TextCompare: false) != 0 && Operators.CompareString(v.Category, "client", TextCompare: false) != 0 && (CS_0024_003C_003E8__locals0._0024VB_0024Me.SelectedOptiFine == null || PageDownloadInstall.IsOptiFineSuitForForge(CS_0024_003C_003E8__locals0._0024VB_0024Me.SelectedOptiFine, v)));
				if (SelectedForge == null)
				{
					throw new Exception($"Forge 列表中没有已安装的 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)1]}");
				}
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions.ContainsKey((ModLoaders)16))
			{
				SelectedNeoForge = ModDownload.DlNeoForgeListLoader.Output.Value.FirstOrDefault([SpecialName] (ModDownload.DlNeoForgeListEntry v) => Operators.CompareString(v.Inherit, CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0 && Operators.CompareString(v.VersionName, CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)16], TextCompare: false) == 0);
				if (SelectedNeoForge == null)
				{
					throw new Exception($"NeoForge 列表中没有已安装的 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)16]}");
				}
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions.ContainsKey((ModLoaders)4))
			{
				JObject val3 = EnumerableExtensions.FirstOrDefault<JObject>(((IEnumerable)ModDownload.DlFabricListLoader.Output.Value["loader"]).OfType<JObject>(), new Func<JObject, bool>[2]
				{
					[SpecialName] (JObject v) => Operators.CompareString(v["version"].ToString(), CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)4], TextCompare: false) == 0,
					[SpecialName] (JObject v) => Operators.CompareString(v["version"].ToString().Replace("+build", ""), CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)4], TextCompare: false) == 0
				});
				if (val3 == null || !((IEnumerable)ModDownload.DlFabricListLoader.Output.Value["game"]).OfType<JObject>().Any([SpecialName] (JObject v) => Operators.CompareString(v["version"].ToString(), CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion.Replace("∞", "infinite").Replace("Combat Test 7c", "1.16_combat-3"), TextCompare: false) == 0))
				{
					throw new Exception($"Fabric 列表中没有已安装的 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)4]}");
				}
				SelectedFabric = val3["version"].ToString();
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions.ContainsKey((ModLoaders)2))
			{
				SelectedLiteLoader = ModDownload.DlLiteLoaderListLoader.Output.Value.FirstOrDefault([SpecialName] (ModDownload.DlLiteLoaderListEntry v) => Operators.CompareString(v.Inherit, CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion, TextCompare: false) == 0 && Operators.CompareString(v.JsonToken[(object)"version"].ToString(), CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)2], TextCompare: false) == 0);
				if (SelectedLiteLoader == null)
				{
					throw new Exception($"LiteLoader 列表中没有已安装的 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions[(ModLoaders)2]}");
				}
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.FabricApiVersion != null)
			{
				SelectedFabricApi = ModDownload.DlFabricApiLoader.Output.FirstOrDefault([SpecialName] (ResourceVersion v) => (Operators.CompareString(v.Version, CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.FabricApiVersion, TextCompare: false) == 0 || Operators.CompareString(v.Id, CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.FabricApiVersion, TextCompare: false) == 0 || Operators.CompareString(v.Display, CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.FabricApiVersion, TextCompare: false) == 0) && PageDownloadInstall.IsFabricApiCompatible(v, CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion));
				if (SelectedFabricApi == null)
				{
					throw new Exception($"Fabric API 列表中没有已安装的 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.FabricApiVersion}");
				}
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.OptiFabricVersion != null)
			{
				SelectedOptiFabric = ModDownload.DlOptiFabricLoader.Output.FirstOrDefault([SpecialName] (ResourceVersion v) => (Operators.CompareString(v.Version, CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.OptiFabricVersion, TextCompare: false) == 0 || Operators.CompareString(v.Id, CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.OptiFabricVersion, TextCompare: false) == 0 || Operators.CompareString(v.Display, CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.OptiFabricVersion, TextCompare: false) == 0) && PageDownloadInstall.IsOptiFabricCompatible(v, CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion));
				if (SelectedOptiFabric == null)
				{
					throw new Exception($"OptiFabric 列表中没有已安装的 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.OptiFabricVersion}");
				}
			}
			List<ManagedFile> list2 = new List<ManagedFile>();
			if (val != null)
			{
				list2.AddRange(val.ManagedFiles);
			}
			Lazy<List<(FileInfo, string)>> lazy = new Lazy<List<(FileInfo, string)>>([SpecialName] () =>
			{
				string text2 = CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.PathIndie + "mods\\";
				List<string> list7 = DirectoryUtils.EnumerateFiles(text2, false, "*.jar").ToList();
				if (CS_0024_003C_003E8__locals0._0024VB_0024Me.SelectedLiteLoader != null)
				{
					list7.AddRange(DirectoryUtils.EnumerateFiles(Path.Combine(text2, CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion), false, "*.jar"));
				}
				List<(FileInfo, string)> list8 = new List<(FileInfo, string)>();
				foreach (string item4 in list7)
				{
					if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.IsCanceled)
					{
						return list8;
					}
					FileInfo info = FileUtils.GetInfo(item4);
					list8.Add((info, new LocalResourceFile(info).ModrinthHash));
					CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Progress = 0.7 + 0.28 * (double)list8.Count / (double)list7.Count;
				}
				return list8;
			});
			if (SelectedFabric != null && SelectedFabricApi == null)
			{
				List<ResourceVersion> source = ModDownload.DlFabricApiLoader.Output.Where([SpecialName] (ResourceVersion v) => PageDownloadInstall.IsFabricApiCompatible(v, CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion)).ToList();
				List<(FileInfo, string, ResourceVersion)> list3 = new List<(FileInfo, string, ResourceVersion)>();
				using (List<(FileInfo, string)>.Enumerator enumerator3 = lazy.Value.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						closure_0024__12_002D = new _Closure_0024__12_002D1(closure_0024__12_002D);
						closure_0024__12_002D._0024VB_0024Local_Entry = enumerator3.Current;
						ResourceVersion resourceVersion = source.FirstOrDefault(closure_0024__12_002D._Lambda_0024__18);
						if (resourceVersion != null)
						{
							list3.Add((closure_0024__12_002D._0024VB_0024Local_Entry.File, closure_0024__12_002D._0024VB_0024Local_Entry.Sha1, resourceVersion));
						}
					}
				}
				(FileInfo, string, ResourceVersion)? tuple = SelectMod("Fabric API", list3);
				if (tuple.HasValue)
				{
					SelectedFabricApi = tuple.Value.Item3;
					list2.Add(new ManagedFile("mods/" + StringExtensions.Lower(tuple.Value.Item1.Name), tuple.Value.Item2, tuple.Value.Item1.LastWriteTimeUtc, "FabricApi"));
				}
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.IsCanceled)
			{
				break;
			}
			if (SelectedFabric != null && SelectedOptiFabric == null)
			{
				List<ResourceVersion> source2 = ModDownload.DlOptiFabricLoader.Output.Where([SpecialName] (ResourceVersion v) => PageDownloadInstall.IsOptiFabricCompatible(v, CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion)).ToList();
				bool flag = source2.Any([SpecialName] (ResourceVersion v) => (v.Hash ?? "").Length == 32);
				List<(FileInfo, string, ResourceVersion)> list4 = new List<(FileInfo, string, ResourceVersion)>();
				using (List<(FileInfo, string)>.Enumerator enumerator4 = lazy.Value.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						closure_0024__12_002D2 = new _Closure_0024__12_002D2(closure_0024__12_002D2);
						closure_0024__12_002D2._0024VB_0024Local_Entry = enumerator4.Current;
						ResourceVersion resourceVersion2 = source2.FirstOrDefault(closure_0024__12_002D2._Lambda_0024__21);
						if (resourceVersion2 == null && flag)
						{
							closure_0024__12_002D3 = new _Closure_0024__12_002D3(closure_0024__12_002D3);
							closure_0024__12_002D3._0024VB_0024Local_Md5 = CryptographyUtils.ComputeFileHash(closure_0024__12_002D2._0024VB_0024Local_Entry.File.FullName, (HashMethod)0);
							resourceVersion2 = source2.FirstOrDefault(closure_0024__12_002D3._Lambda_0024__22);
						}
						if (resourceVersion2 != null)
						{
							list4.Add((closure_0024__12_002D2._0024VB_0024Local_Entry.File, closure_0024__12_002D2._0024VB_0024Local_Entry.Sha1, resourceVersion2));
						}
					}
				}
				(FileInfo, string, ResourceVersion)? tuple2 = SelectMod("OptiFabric", list4);
				if (tuple2.HasValue)
				{
					SelectedOptiFabric = tuple2.Value.Item3;
					list2.Add(new ManagedFile("mods/" + StringExtensions.Lower(tuple2.Value.Item1.Name), tuple2.Value.Item2, tuple2.Value.Item1.LastWriteTimeUtc, "OptiFabric"));
				}
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.IsCanceled)
			{
				break;
			}
			List<(FileInfo, string, string)> list5 = new List<(FileInfo, string, string)>();
			if (SelectedOptiFine == null && (SelectedForge != null || SelectedLiteLoader != null || SelectedFabric != null))
			{
				if (_0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes_0024Init == null)
				{
					Interlocked.CompareExchange(ref _0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes_0024Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken = false;
				try
				{
					Monitor.Enter(_0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes_0024Init, ref lockTaken);
					if (_0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes_0024Init.State == 0)
					{
						_0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes_0024Init.State = 2;
						_0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes = (JObject)FileUtils.ReadAsJson("Minecraft\\OptiFineHashes.json", (Encoding)null, typeof(Resource));
					}
					else if (_0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes_0024Init.State = 1;
					if (lockTaken)
					{
						Monitor.Exit(_0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes_0024Init);
					}
				}
				list5 = (from Entry in lazy.Value.Select<(FileInfo, string), (FileInfo, string, string)>([SpecialName] ((FileInfo File, string Sha1) Entry) =>
					{
						FileInfo item = Entry.File;
						string item2 = Entry.Sha1;
						JToken obj = _0024STATIC_0024LoadInstance_00242011151286FC21287E81C_0024OptiFineHashes[Entry.Sha1];
						return (item, item2, (obj != null) ? obj.ToString() : null);
					})
					where Entry.OriginalName != null
					select Entry).ToList();
				List<(FileInfo, string, ModDownload.DlOptiFineListEntry)> list6 = new List<(FileInfo, string, ModDownload.DlOptiFineListEntry)>();
				using (List<(FileInfo, string, string)>.Enumerator enumerator5 = list5.GetEnumerator())
				{
					while (enumerator5.MoveNext())
					{
						closure_0024__12_002D4 = new _Closure_0024__12_002D4(closure_0024__12_002D4);
						closure_0024__12_002D4._0024VB_0024NonLocal__0024VB_0024Closure_2 = CS_0024_003C_003E8__locals0;
						closure_0024__12_002D4._0024VB_0024Local_Entry = enumerator5.Current;
						ModDownload.DlOptiFineListEntry dlOptiFineListEntry = ModDownload.DlOptiFineListLoader.Output.Value.FirstOrDefault(closure_0024__12_002D4._Lambda_0024__25);
						if (dlOptiFineListEntry == null)
						{
							ModMain.Hint($"无法从 OptiFine 列表还原 {closure_0024__12_002D4._0024VB_0024Local_Entry.File.Name} 对应的 {closure_0024__12_002D4._0024VB_0024Local_Entry.OriginalName}，将跳过该文件。");
						}
						else
						{
							list6.Add((closure_0024__12_002D4._0024VB_0024Local_Entry.File, closure_0024__12_002D4._0024VB_0024Local_Entry.Sha1, dlOptiFineListEntry));
						}
					}
				}
				(FileInfo, string, ModDownload.DlOptiFineListEntry)? tuple3 = SelectMod("OptiFine", list6);
				if (tuple3.HasValue)
				{
					SelectedOptiFine = tuple3.Value.Item3;
					string text = StringExtensions.Lower(PathUtils.RemoveExtendedPrefix(tuple3.Value.Item1.FullName).Substring(CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.PathIndie.Length).Replace("\\", "/"));
					list2.Add(new ManagedFile(text, tuple3.Value.Item2, tuple3.Value.Item1.LastWriteTimeUtc, "OptiFine"));
				}
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.IsCanceled)
			{
				break;
			}
			if (IsVanillaVersionReliable)
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_Components = new McComponents(CS_0024_003C_003E8__locals0._0024VB_0024Local_MinecraftVersion, CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.ModLoaderVersions, (SelectedOptiFine == null) ? null : JObject.FromObject((object)SelectedOptiFine), SelectedFabricApi?.Version ?? SelectedFabricApi?.Id ?? SelectedFabricApi?.Display, SelectedOptiFabric?.Version ?? SelectedOptiFabric?.Id ?? SelectedOptiFabric?.Display);
				if (val == null || !CS_0024_003C_003E8__locals0._0024VB_0024Local_Components.IsEquivalentTo(val.Components) || !list2.SequenceEqual(val.ManagedFiles))
				{
					Configs.McInstallManifest.Set(new McInstallManifest(1, CS_0024_003C_003E8__locals0._0024VB_0024Local_Components, DateTime.UtcNow, (IReadOnlyList<ManagedFile>)list2), (IConfigProvider)(object)CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.Config);
					CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.Config.Save();
					Logger.Info($"已为 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.Input.Name} 更新安装清单", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadModify.vb");
				}
			}
			if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Task.IsCanceled)
			{
				ForgeVersionLoader = loaderTask;
			}
			break;
		}
	}

	private (FileInfo File, string Sha1, T Version)? SelectMod<T>(string ComponentName, List<(FileInfo File, string Sha1, T Version)> Matches)
	{
		(FileInfo, string, T)? result;
		if (Matches.Count == 0)
		{
			result = null;
		}
		else if (Matches.Count == 1)
		{
			result = Matches[0];
		}
		else
		{
			int? num = null;
			ModBase.RunInUiWait([SpecialName] () =>
			{
				num = ModMain.MyMsgBoxSelect(((IEnumerable<(FileInfo, string, T)>)Matches).Select((Func<(FileInfo, string, T), IMyRadio>)([SpecialName] ((FileInfo File, string Sha1, T Version) Entry) => new MyRadioBox
				{
					Text = Entry.File.Name
				})), $"选择当前使用的 {ComponentName}");
			});
			if (!num.HasValue)
			{
				throw new Exception($"发现了多个 {ComponentName}");
			}
			result = Matches[num.Value];
		}
		return result;
	}

	private void LoadInstanceFinished(ModLoader.LoaderTask<ModMinecraft.McInstance, object> Task)
	{
		OriginalName = Task.Input.Name;
		CustomLogo = null;
		if (Conversions.ToBoolean(ModBase.ReadIni(Task.Input.PathVersion + "PCL\\Setup.ini", "LogoCustom", Conversions.ToString(Value: false))))
		{
			CustomLogo = (StringExtensions.EndsWithF(Task.Input.Logo, "PCL\\Logo.png", false) ? (Task.Input.PathVersion + "PCL\\Logo.png") : Task.Input.Logo);
		}
		MyListItem myListItem = ModDownloadLib.McDownloadListItem(VanillaData, null, IsSaveOnly: false).Init();
		VanillaName = myListItem.Title;
		VanillaIcon = myListItem.Logo;
		LabMinecraftVersion.Text = VanillaName;
		TextSelectName.Text = OriginalName;
		IsSelectNameEdited = !string.Equals(OriginalName, GetSelectName(), StringComparison.Ordinal);
		TextSelectName.ValidateRules = new Collection<Validate>
		{
			new ValidateFolderName(ModMinecraft.McFolderSelected + "versions", UseMinecraftCharCheck: true, new List<string> { OriginalName })
		};
		CardMinecraft.Visibility = Visibility.Visible;
		BtnBack.Visibility = Visibility.Collapsed;
		ImgLogo.Margin = new Thickness(15.0, 15.0, 0.0, 0.0);
		TextSelectName.Margin = new Thickness(56.0, 16.0, 16.0, 0.0);
		LabSelectName.Text = $"原名\u200a:\u200a\u200a\u200a{OriginalName}";
		UpdateOriginalNameVisibility();
		BtnStart.Text = "开始修改";
		BtnStart.Logo = ModMain.FrmInstanceLeft.ItemModify.Logo;
		AutoSelectedFabricApi = SelectedFabric != null;
		AutoSelectedOptiFabric = SelectedFabric != null && SelectedOptiFine != null;
		if (ForgeVersionLoader != null)
		{
			LoadForge.State = ForgeVersionLoader;
		}
		PrepareSelectPage();
		ReloadComponentLists();
		PanMinecraft.Visibility = Visibility.Collapsed;
		PanMinecraft.Opacity = 0.0;
		PanMinecraft.RenderTransform = new TranslateTransform(-50.0, 0.0);
		PanSelect.Opacity = 1.0;
		PanSelect.RenderTransform = new TranslateTransform();
		PanBack.IsHitTestVisible = true;
		PanAllBack.IsHitTestVisible = true;
		InitialTarget = GetSelectedComponents();
		BtnMinecraftRestore.Tag = VanillaData;
		BtnOptiFineRestore.Tag = SelectedOptiFine;
		BtnForgeRestore.Tag = SelectedForge;
		BtnNeoForgeRestore.Tag = SelectedNeoForge;
		BtnFabricRestore.Tag = SelectedFabric;
		BtnFabricApiRestore.Tag = SelectedFabricApi;
		BtnOptiFabricRestore.Tag = SelectedOptiFabric;
		BtnLiteLoaderRestore.Tag = SelectedLiteLoader;
		ImgMinecraftOld.Source = VanillaIcon;
		LabMinecraftOld.Text = LabMinecraftVersion.Text;
		LabOptiFineOld.Text = ((SelectedOptiFine == null) ? "无" : LabOptiFine.Text);
		LabForgeOld.Text = ((SelectedForge == null) ? "无" : LabForge.Text);
		LabNeoForgeOld.Text = ((SelectedNeoForge == null) ? "无" : LabNeoForge.Text);
		LabFabricOld.Text = ((SelectedFabric == null) ? "无" : LabFabric.Text);
		LabLiteLoaderOld.Text = ((SelectedLiteLoader == null) ? "无" : LabLiteLoader.Text);
		LabFabricApiOld.Text = ((SelectedFabricApi == null) ? "无" : LabFabricApi.Text);
		LabOptiFabricOld.Text = ((SelectedOptiFabric == null) ? "无" : LabOptiFabric.Text);
		RefreshComparisons();
	}

	public override string GetSelectLogo()
	{
		return CustomLogo ?? base.GetSelectLogo();
	}

	public override void ShowMinecraftVersionList()
	{
		if (IsInSelectPage)
		{
			McComponents selectedComponents = GetSelectedComponents();
			(MyLoading, string)[] obj = new(MyLoading, string)[7]
			{
				(LoadOptiFine, selectedComponents.OptiFineVersion),
				(LoadFabricApi, selectedComponents.FabricApiVersion),
				(LoadOptiFabric, selectedComponents.OptiFabricVersion),
				(LoadForge, SelectedForge?.VersionName),
				(LoadNeoForge, SelectedNeoForge?.VersionName),
				(LoadFabric, SelectedFabric),
				default((MyLoading, string))
			};
			MyLoading loadLiteLoader = LoadLiteLoader;
			ModDownload.DlLiteLoaderListEntry selectedLiteLoader = SelectedLiteLoader;
			object item;
			if (selectedLiteLoader == null)
			{
				item = null;
			}
			else
			{
				JToken jsonToken = selectedLiteLoader.JsonToken;
				item = ((jsonToken != null) ? jsonToken[(object)"version"].ToString() : null);
			}
			obj[6] = (loadLiteLoader, (string)item);
			(MyLoading, string)[] array = obj;
			for (int i = 0; i < array.Length; i = checked(i + 1))
			{
				(MyLoading, string) tuple = array[i];
				if (tuple.Item2 != null)
				{
					PendingComponents[tuple.Item1] = tuple.Item2;
				}
			}
		}
		base.ShowMinecraftVersionList();
	}

	public override void ReloadSelected()
	{
		if (IsRestoringComponents)
		{
			return;
		}
		if (IsInSelectPage && VanillaName != null && PendingComponents.Any())
		{
			IsRestoringComponents = true;
			try
			{
				bool flag = false;
				MyLoading[] array = new MyLoading[7] { LoadFabric, LoadForge, LoadNeoForge, LoadLiteLoader, LoadOptiFine, LoadFabricApi, LoadOptiFabric };
				_Closure_0024__17_002D0 closure_0024__17_002D = default(_Closure_0024__17_002D0);
				foreach (MyLoading myLoading in array)
				{
					closure_0024__17_002D = new _Closure_0024__17_002D0(closure_0024__17_002D);
					closure_0024__17_002D._0024VB_0024Local_Version = null;
					if (!PendingComponents.TryGetValue(myLoading, out closure_0024__17_002D._0024VB_0024Local_Version) || !myLoading.State.IsLoader)
					{
						continue;
					}
					ModLoader.LoaderBase loaderBase = (ModLoader.LoaderBase)myLoading.State;
					if (myLoading == LoadOptiFine)
					{
						if (new MyLoading[4] { LoadFabric, LoadForge, LoadNeoForge, LoadLiteLoader }.Any([SpecialName] (MyLoading Component) => PendingComponents.ContainsKey(Component)))
						{
							continue;
						}
					}
					else if (myLoading == LoadFabricApi)
					{
						if (PendingComponents.ContainsKey(LoadFabric))
						{
							continue;
						}
					}
					else if (myLoading == LoadForge)
					{
						if (!McVersion.IsStandardFormat(VanillaName))
						{
							PendingComponents.Remove(myLoading);
							continue;
						}
						if (Operators.CompareString(((ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>)loaderBase).Input, VanillaName, TextCompare: false) != 0)
						{
							continue;
						}
						if (loaderBase.State == ModBase.LoadState.Failed && Operators.CompareString(loaderBase.Error.Message, "无", TextCompare: false) == 0)
						{
							PendingComponents.Remove(myLoading);
							continue;
						}
					}
					else if (myLoading == LoadOptiFabric && (PendingComponents.ContainsKey(LoadFabric) || PendingComponents.ContainsKey(LoadOptiFine)))
					{
						continue;
					}
					if (loaderBase.State != ModBase.LoadState.Finished)
					{
						continue;
					}
					PendingComponents.Remove(myLoading);
					flag = true;
					if (myLoading == LoadFabric)
					{
						if (SelectedFabric == null && LoadFabricGetError() == null)
						{
							IEnumerable<JObject> source = ((IEnumerable)ModDownload.DlFabricListLoader.Output.Value["loader"]).OfType<JObject>();
							JObject obj = source.FirstOrDefault(closure_0024__17_002D._Lambda_0024__1) ?? source.FirstOrDefault();
							SelectedFabric = ((obj != null) ? obj["version"].ToString() : null);
						}
					}
					else if (myLoading == LoadOptiFine)
					{
						if (SelectedOptiFine == null && LoadOptiFineGetError() == null)
						{
							IEnumerable<ModDownload.DlOptiFineListEntry> source2 = ModDownload.DlOptiFineListLoader.Output.Value.Where([SpecialName] (ModDownload.DlOptiFineListEntry v) => Operators.CompareString(v.Inherit, VanillaName, TextCompare: false) == 0 && (SelectedForge == null || PageDownloadInstall.IsOptiFineSuitForForge(v, SelectedForge)));
							SelectedOptiFine = source2.FirstOrDefault(closure_0024__17_002D._Lambda_0024__3) ?? source2.OrderBy([SpecialName] (ModDownload.DlOptiFineListEntry v) => v.IsPreview).ThenByDescending([SpecialName] (ModDownload.DlOptiFineListEntry v) => v.DisplayName, Comparer<string>.Create((Comparison<string>)McUtils.CompareVersion)).FirstOrDefault();
						}
					}
					else if (myLoading == LoadForge)
					{
						if (SelectedForge == null && LoadForgeGetError() == null)
						{
							IEnumerable<ModDownload.DlForgeVersionEntry> source3 = ((ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>)loaderBase).Output.Where([SpecialName] (ModDownload.DlForgeVersionEntry v) => Operators.CompareString(v.Inherit, VanillaName, TextCompare: false) == 0 && Operators.CompareString(v.Category, "universal", TextCompare: false) != 0 && Operators.CompareString(v.Category, "client", TextCompare: false) != 0 && (SelectedOptiFine == null || PageDownloadInstall.IsOptiFineSuitForForge(SelectedOptiFine, v)));
							SelectedForge = source3.FirstOrDefault(closure_0024__17_002D._Lambda_0024__7) ?? source3.OrderByDescending([SpecialName] (ModDownload.DlForgeVersionEntry v) => v).FirstOrDefault();
						}
					}
					else if (myLoading == LoadNeoForge)
					{
						if (SelectedNeoForge == null && LoadNeoForgeGetError() == null)
						{
							IEnumerable<ModDownload.DlNeoForgeListEntry> source4 = ModDownload.DlNeoForgeListLoader.Output.Value.Where([SpecialName] (ModDownload.DlNeoForgeListEntry v) => Operators.CompareString(v.Inherit, VanillaName, TextCompare: false) == 0);
							SelectedNeoForge = source4.FirstOrDefault(closure_0024__17_002D._Lambda_0024__10) ?? source4.FirstOrDefault();
						}
					}
					else if (myLoading == LoadLiteLoader)
					{
						if (SelectedLiteLoader == null && LoadLiteLoaderGetError() == null)
						{
							IEnumerable<ModDownload.DlLiteLoaderListEntry> source5 = ModDownload.DlLiteLoaderListLoader.Output.Value.Where([SpecialName] (ModDownload.DlLiteLoaderListEntry v) => Operators.CompareString(v.Inherit, VanillaName, TextCompare: false) == 0);
							SelectedLiteLoader = source5.FirstOrDefault(closure_0024__17_002D._Lambda_0024__12) ?? source5.FirstOrDefault();
						}
					}
					else if (myLoading == LoadFabricApi)
					{
						if (SelectedFabricApi == null && LoadFabricApiGetError() == null)
						{
							IEnumerable<ResourceVersion> source6 = ModDownload.DlFabricApiLoader.Output.Where([SpecialName] (ResourceVersion v) => PageDownloadInstall.IsFabricApiCompatible(v, VanillaName));
							SelectedFabricApi = source6.FirstOrDefault(closure_0024__17_002D._Lambda_0024__14) ?? source6.OrderByDescending([SpecialName] (ResourceVersion v) => v.ReleaseDate).FirstOrDefault();
						}
						if (SelectedFabricApi != null)
						{
							AutoSelectedFabricApi = true;
						}
					}
					else
					{
						if (myLoading != LoadOptiFabric)
						{
							continue;
						}
						if (SelectedOptiFabric == null && LoadOptiFabricGetError() == null)
						{
							IEnumerable<ResourceVersion> source7 = ModDownload.DlOptiFabricLoader.Output.Where([SpecialName] (ResourceVersion v) => PageDownloadInstall.IsOptiFabricCompatible(v, VanillaName));
							SelectedOptiFabric = source7.FirstOrDefault(closure_0024__17_002D._Lambda_0024__17) ?? source7.OrderByDescending([SpecialName] (ResourceVersion v) => v.ReleaseDate).FirstOrDefault();
						}
						if (SelectedOptiFabric != null)
						{
							AutoSelectedOptiFabric = true;
						}
					}
				}
				if (flag)
				{
					ReloadComponentLists();
				}
			}
			finally
			{
				IsRestoringComponents = false;
			}
		}
		base.ReloadSelected();
		if (InitialTarget != null)
		{
			RefreshComparisons();
		}
	}

	private void RefreshComparisons()
	{
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Invalid comparison between Unknown and I4
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Invalid comparison between Unknown and I4
		McComponents selectedComponents = GetSelectedComponents();
		ImgMinecraftNew.Source = VanillaIcon;
		bool flag = Operators.CompareString(InitialTarget.MinecraftVersion, selectedComponents.MinecraftVersion, TextCompare: false) == 0;
		ShowComparison(PanMinecraftCompare, PanMinecraftInfo, InitialTarget.MinecraftVersion, selectedComponents.MinecraftVersion, LabMinecraftVersion.Text, BtnMinecraftRestore, CanRestore: true);
		ModDownload.DlOptiFineListEntry dlOptiFineListEntry = BtnOptiFineRestore.Tag as ModDownload.DlOptiFineListEntry;
		ShowComparison(PanOptiFineCompare, PanOptiFineInfo, InitialTarget.OptiFineVersion, selectedComponents.OptiFineVersion, LabOptiFine.Text, BtnOptiFineRestore, InitialTarget.OptiFineEntry == null || (flag && CardOptiFine.MainSwap.Visibility == Visibility.Visible && dlOptiFineListEntry != null && (SelectedForge == null || PageDownloadInstall.IsOptiFineSuitForForge(dlOptiFineListEntry, SelectedForge))), BtnOptiFineClear);
		ShowComparison(PanFabricApiCompare, PanFabricApiInfo, InitialTarget.FabricApiVersion, selectedComponents.FabricApiVersion, LabFabricApi.Text, BtnFabricApiRestore, InitialTarget.FabricApiVersion == null || (flag && SelectedFabric != null && CardFabricApi.MainSwap.Visibility == Visibility.Visible), BtnFabricApiClear);
		ShowComparison(PanOptiFabricCompare, PanOptiFabricInfo, InitialTarget.OptiFabricVersion, selectedComponents.OptiFabricVersion, LabOptiFabric.Text, BtnOptiFabricRestore, InitialTarget.OptiFabricVersion == null || (flag && SelectedFabric != null && SelectedOptiFine != null && CardOptiFabric.MainSwap.Visibility == Visibility.Visible), BtnOptiFabricClear);
		(ModLoaders, MyCard, Grid, Grid, TextBlock, MyIconButton, MyIconButton)[] array = new(ModLoaders, MyCard, Grid, Grid, TextBlock, MyIconButton, MyIconButton)[4]
		{
			((ModLoaders)1, CardForge, PanForgeCompare, PanForgeInfo, LabForge, BtnForgeRestore, BtnForgeClear),
			((ModLoaders)16, CardNeoForge, PanNeoForgeCompare, PanNeoForgeInfo, LabNeoForge, BtnNeoForgeRestore, BtnNeoForgeClear),
			((ModLoaders)4, CardFabric, PanFabricCompare, PanFabricInfo, LabFabric, BtnFabricRestore, BtnFabricClear),
			((ModLoaders)2, CardLiteLoader, PanLiteLoaderCompare, PanLiteLoaderInfo, LabLiteLoader, BtnLiteLoaderRestore, BtnLiteLoaderClear)
		};
		_Closure_0024__18_002D0 closure_0024__18_002D = default(_Closure_0024__18_002D0);
		_Closure_0024__18_002D1 closure_0024__18_002D2 = default(_Closure_0024__18_002D1);
		for (int i = 0; i < array.Length; i = checked(i + 1))
		{
			(ModLoaders, MyCard, Grid, Grid, TextBlock, MyIconButton, MyIconButton) tuple = array[i];
			closure_0024__18_002D = new _Closure_0024__18_002D0(closure_0024__18_002D);
			closure_0024__18_002D._0024VB_0024Local_OldVersion = null;
			string value = null;
			InitialTarget.ModLoaderVersions.TryGetValue(tuple.Item1, out closure_0024__18_002D._0024VB_0024Local_OldVersion);
			selectedComponents.ModLoaderVersions.TryGetValue(tuple.Item1, out value);
			bool flag2 = closure_0024__18_002D._0024VB_0024Local_OldVersion == null || (flag && tuple.Item2.MainSwap.Visibility == Visibility.Visible && tuple.Item6.Tag != null);
			if ((int)tuple.Item1 == 1 && closure_0024__18_002D._0024VB_0024Local_OldVersion != null && flag2)
			{
				flag2 = SelectedOptiFine == null || PageDownloadInstall.IsOptiFineSuitForForge(SelectedOptiFine, (ModDownload.DlForgeVersionEntry)tuple.Item6.Tag);
			}
			if ((int)tuple.Item1 == 4 && closure_0024__18_002D._0024VB_0024Local_OldVersion != null)
			{
				closure_0024__18_002D2 = new _Closure_0024__18_002D1(closure_0024__18_002D2);
				closure_0024__18_002D2._0024VB_0024Local_TargetVersion = VanillaName.Replace("∞", "infinite").Replace("Combat Test 7c", "1.16_combat-3");
				flag2 = CardFabric.Visibility == Visibility.Visible && CardFabric.MainSwap.Visibility == Visibility.Visible && ((IEnumerable)ModDownload.DlFabricListLoader.Output.Value["loader"]).OfType<JObject>().Any(closure_0024__18_002D._Lambda_0024__0) && ((IEnumerable)ModDownload.DlFabricListLoader.Output.Value["game"]).OfType<JObject>().Any(closure_0024__18_002D2._Lambda_0024__1);
			}
			ShowComparison(tuple.Item3, tuple.Item4, closure_0024__18_002D._0024VB_0024Local_OldVersion, value, tuple.Item5.Text, tuple.Item6, flag2, tuple.Item7);
		}
		UpdateStartButton();
	}

	private void ShowComparison(Grid ComparePanel, FrameworkElement CurrentPanel, string OldVersion, string NewVersion, string NewText, MyIconButton RestoreButton, bool CanRestore, FrameworkElement ClearButton = null)
	{
		string text = null;
		if (NewVersion == null)
		{
			if (RestoreButton == BtnOptiFineRestore)
			{
				text = LoadOptiFineGetError();
			}
			else if (RestoreButton == BtnForgeRestore)
			{
				text = (McVersion.IsStandardFormat(VanillaName) ? LoadForgeGetError() : "无");
			}
			else if (RestoreButton == BtnNeoForgeRestore)
			{
				text = ((CardNeoForge.Visibility == Visibility.Collapsed) ? "无" : LoadNeoForgeGetError());
			}
			else if (RestoreButton == BtnFabricRestore)
			{
				text = ((CardFabric.Visibility == Visibility.Collapsed) ? "无" : LoadFabricGetError());
			}
			else if (RestoreButton == BtnLiteLoaderRestore)
			{
				text = ((CardLiteLoader.Visibility == Visibility.Collapsed) ? "无" : LoadLiteLoaderGetError());
			}
			else if (RestoreButton == BtnFabricApiRestore)
			{
				text = LoadFabricApiGetError();
			}
			else if (RestoreButton == BtnOptiFabricRestore)
			{
				text = LoadOptiFabricGetError();
			}
			NewText = text ?? "可以添加";
		}
		bool flag = NewVersion == null && (Operators.CompareString(NewText, "获取中……", TextCompare: false) == 0 || StringExtensions.StartsWithF(NewText, "获取失败：", false));
		bool flag2 = Operators.CompareString(OldVersion, NewVersion, TextCompare: false) != 0 && !flag;
		CanRestore = flag2 && OldVersion != null && CanRestore;
		ComparePanel.Visibility = ((!flag2) ? Visibility.Collapsed : Visibility.Visible);
		CurrentPanel.Visibility = (flag2 ? Visibility.Collapsed : Visibility.Visible);
		RestoreButton.Visibility = ((!CanRestore) ? Visibility.Collapsed : Visibility.Visible);
		if (ClearButton != null)
		{
			ClearButton.ToolTip = ((OldVersion == null) ? "取消安装" : "移除");
		}
		bool flag3 = ClearButton != null && ClearButton.Visibility == Visibility.Visible;
		RestoreButton.Margin = new Thickness(0.0, 5.0, (CanRestore && flag3) ? 60 : 32, 0.0);
		ComparePanel.Margin = new Thickness(ComparePanel.Margin.Left, ComparePanel.Margin.Top, (CanRestore && flag3) ? 110 : 80, ComparePanel.Margin.Bottom);
		if (!flag2)
		{
			return;
		}
		if (OldVersion != null)
		{
			MyCard myCard = (MyCard)ComparePanel.Parent;
			if (myCard.Visibility == Visibility.Collapsed)
			{
				myCard.IsSwapped = true;
				if (myCard.MainSwap != null)
				{
					myCard.MainSwap.Visibility = Visibility.Collapsed;
				}
				myCard.Visibility = Visibility.Visible;
			}
		}
		DockPanel dockPanel = (DockPanel)ComparePanel.Children[0];
		DockPanel obj = (DockPanel)ComparePanel.Children[2];
		dockPanel.Children[0].Visibility = ((OldVersion == null) ? Visibility.Collapsed : Visibility.Visible);
		((TextBlock)dockPanel.Children[1]).Margin = ((OldVersion == null) ? new Thickness(0.0) : new Thickness(7.0, 0.0, 0.0, 0.0));
		obj.Children[0].Visibility = ((NewVersion == null) ? Visibility.Collapsed : Visibility.Visible);
		TextBlock textBlock = (TextBlock)obj.Children[1];
		textBlock.Margin = ((NewVersion == null) ? new Thickness(0.0) : new Thickness(7.0, 0.0, 0.0, 0.0));
		textBlock.Text = ((NewVersion == null) ? "移除" : NewText);
		if (NewVersion == null && text != null)
		{
			textBlock.Inlines.Add(new Run(string.Format("（{0}）", (Operators.CompareString(text, "无", TextCompare: false) == 0) ? "无可用版本" : text))
			{
				Foreground = ModSecret.ColorGray4
			});
		}
	}

	private void RestoreComponent(object sender, EventArgs e)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		MyIconButton myIconButton = (MyIconButton)sender;
		if (myIconButton == BtnMinecraftRestore)
		{
			IsRestoringComponents = true;
			try
			{
				PendingComponents.Clear();
				MinecraftSelected(ModDownloadLib.McDownloadListItem((JObject)myIconButton.Tag, null, IsSaveOnly: false).Init(), null);
				if (ForgeVersionLoader != null)
				{
					LoadForge.State = ForgeVersionLoader;
				}
				SelectedOptiFine = BtnOptiFineRestore.Tag as ModDownload.DlOptiFineListEntry;
				SelectedForge = BtnForgeRestore.Tag as ModDownload.DlForgeVersionEntry;
				SelectedNeoForge = BtnNeoForgeRestore.Tag as ModDownload.DlNeoForgeListEntry;
				SelectedFabric = BtnFabricRestore.Tag as string;
				SelectedLiteLoader = BtnLiteLoaderRestore.Tag as ModDownload.DlLiteLoaderListEntry;
				SelectedFabricApi = BtnFabricApiRestore.Tag as ResourceVersion;
				SelectedOptiFabric = BtnOptiFabricRestore.Tag as ResourceVersion;
				AutoSelectedFabricApi = SelectedFabric != null;
				AutoSelectedOptiFabric = SelectedFabric != null && SelectedOptiFine != null;
			}
			finally
			{
				IsRestoringComponents = false;
			}
			ReloadComponentLists();
			return;
		}
		if (myIconButton == BtnOptiFineRestore)
		{
			SelectedOptiFine = myIconButton.Tag as ModDownload.DlOptiFineListEntry;
			AutoSelectedOptiFabric = SelectedOptiFine != null && SelectedFabric != null;
			if (SelectedOptiFine == null)
			{
				SelectedOptiFabric = null;
			}
		}
		else if (myIconButton == BtnForgeRestore)
		{
			SelectedForge = myIconButton.Tag as ModDownload.DlForgeVersionEntry;
		}
		else if (myIconButton == BtnNeoForgeRestore)
		{
			SelectedNeoForge = myIconButton.Tag as ModDownload.DlNeoForgeListEntry;
		}
		else if (myIconButton == BtnFabricRestore)
		{
			SelectedFabric = myIconButton.Tag as string;
			AutoSelectedFabricApi = SelectedFabric != null;
			AutoSelectedOptiFabric = SelectedFabric != null && SelectedOptiFine != null;
			if (SelectedFabric == null)
			{
				SelectedFabricApi = null;
				SelectedOptiFabric = null;
			}
		}
		else if (myIconButton == BtnFabricApiRestore)
		{
			SelectedFabricApi = myIconButton.Tag as ResourceVersion;
			AutoSelectedFabricApi = true;
		}
		else if (myIconButton == BtnOptiFabricRestore)
		{
			SelectedOptiFabric = myIconButton.Tag as ResourceVersion;
			AutoSelectedOptiFabric = true;
		}
		else if (myIconButton == BtnLiteLoaderRestore)
		{
			SelectedLiteLoader = myIconButton.Tag as ModDownload.DlLiteLoaderListEntry;
		}
		((MyCard)myIconButton.Parent).IsSwapped = true;
		ReloadComponentLists();
	}

	private void CardMinecraft_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		ShowMinecraftVersionList();
		e.Handled = true;
	}

	private void UpdateStartButton()
	{
		BtnStart.IsEnabled = TextSelectName.IsValidated && InitialTarget != null && !PendingComponents.Any() && (!IsVanillaVersionReliable || !InitialTarget.IsEquivalentTo(GetSelectedComponents()) || !string.Equals(TextSelectName.Text, OriginalName, StringComparison.Ordinal));
	}

	private void UpdateOriginalNameVisibility()
	{
		bool flag = !string.Equals(TextSelectName.Text, OriginalName, StringComparison.Ordinal);
		Visibility visibility = ((!flag) ? Visibility.Collapsed : Visibility.Visible);
		if (LabSelectName.Visibility == visibility)
		{
			return;
		}
		LabSelectName.Visibility = visibility;
		BtnOriginalNameRestore.Visibility = visibility;
		int num = (flag ? 40 : 16);
		if (IsInSelectPage && ModAnimation.AniControlEnabled == 0)
		{
			ModAnimation.AniStart(ModAnimation.AaDouble([SpecialName] (object Delta) =>
			{
				TextSelectName.Margin = new Thickness(56.0, 16.0, Conversions.ToDouble(Operators.AddObject(TextSelectName.Margin.Right, Delta)), 0.0);
			}, (double)num - TextSelectName.Margin.Right, 150, 0, new ModAnimation.AniEaseOutFluent()), "DownloadModify Name Margin");
		}
		else
		{
			ModAnimation.AniStop("DownloadModify Name Margin");
			TextSelectName.Margin = new Thickness(56.0, 16.0, num, 0.0);
		}
	}

	private void BtnOriginalNameRestore_Click(object sender, EventArgs e)
	{
		TextSelectName.Text = OriginalName;
		IsSelectNameEdited = !string.Equals(OriginalName, GetSelectName(), StringComparison.Ordinal);
	}

	public override void StartInstall(ModDownloadLib.McInstallRequest Request)
	{
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		UpdateStartButton();
		if (!BtnStart.IsEnabled)
		{
			return;
		}
		ModMinecraft.McInstance input = InitLoader.Input;
		McComponents components = Request.Components;
		string newInstanceName = Request.NewInstanceName;
		bool flag = !string.Equals(newInstanceName, input.Name, StringComparison.Ordinal);
		Request.NewInstanceName = (flag ? newInstanceName : null);
		Request.InstanceFolder = input.PathVersion;
		List<string> list = new List<string>();
		if ((SelectedForge != null || SelectedNeoForge != null || SelectedFabric != null) && !PathUtils.IsSame(input.PathIndie, input.PathVersion))
		{
			list.Add("该版本尚未开启版本隔离，可能与别的 MC 版本共用同一个 Mod 和存档文件夹。\r\n修改版本后，已有的 Mod、存档等需要手动迁移到新的游戏目录。");
		}
		bool flag2 = Operators.CompareString(InitialTarget.MinecraftVersion, components.MinecraftVersion, TextCompare: false) != 0;
		if (!IsVanillaVersionReliable && !flag2)
		{
			list.Add($"PCL 未能精准识别该版本，请确认它是不是 Minecraft {InitialTarget.MinecraftVersion}，如果不正确，请取消并选择正确的版本！");
		}
		if (!Configs.HintModifyInstanceWarning.Get())
		{
			list.Add("如果该版本安装了 PCL 不兼容的加载器，或经过第三方修改，这些改动将会丢失！");
		}
		bool flag3 = false;
		foreach (ModLoaders item in InitialTarget.ModLoaderVersions.Keys.Union(components.ModLoaderVersions.Keys))
		{
			string value = null;
			string value2 = null;
			bool num = InitialTarget.ModLoaderVersions.TryGetValue(item, out value);
			bool flag4 = components.ModLoaderVersions.TryGetValue(item, out value2);
			if (num != flag4 || Operators.CompareString(value, value2, TextCompare: false) != 0)
			{
				flag3 = true;
				break;
			}
		}
		if ((flag3 || flag2) && InitialTarget.ModLoaderVersions.Any())
		{
			list.Add("现有的 Mod、资源包等在修改后可能会不兼容！");
		}
		if (list.Any() && ModMain.MyMsgBox("请注意：\r\n• " + EnumerableExtensions.Join((IEnumerable)list, "\r\n• "), "修改确认", "开始修改", "取消", "", IsWarn: true) != 1)
		{
			return;
		}
		if (!Configs.HintModifyInstanceWarning.Get())
		{
			Configs.HintModifyInstanceWarning.Set(true);
		}
		if (IsVanillaVersionReliable && InitialTarget.IsEquivalentTo(components))
		{
			try
			{
				string pathVersion = input.PathVersion;
				ModMinecraft.McInstance mcInstance = input.Rename(newInstanceName);
				if ((object)PageInstanceLeft.Instance != null && PathUtils.IsSame(PageInstanceLeft.Instance.PathVersion, pathVersion))
				{
					PageInstanceLeft.Instance = mcInstance;
				}
				if ((object)ModMinecraft.McInstanceSelected != null && PathUtils.IsSame(ModMinecraft.McInstanceSelected.PathVersion, pathVersion))
				{
					ModMinecraft.McInstanceSelected = mcInstance;
				}
				PageInstanceLeft.ReloadCurrentJava();
				ModMain.Hint("重命名成功！", ModMain.HintType.Green);
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
				ModMain.FrmMain.PageBack();
				return;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "重命名版本失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadModify.vb");
				ProjectData.ClearProjectError();
				return;
			}
		}
		if (ModDownloadLib.McModificationStart(Request, input))
		{
			ModMain.FrmMain.PageBack();
		}
	}
}
