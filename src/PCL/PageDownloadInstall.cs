using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCLCS;

namespace PCL;

[DesignerGenerated]
public class PageDownloadInstall : MyPageRight, IComponentConnector
{
	[Serializable]
	[CompilerGenerated]
	internal new sealed class _Closure_0024__
	{
		public static readonly _Closure_0024__ _0024I;

		public static Func<JObject, DateTime> _0024I35_002D0;

		public static Func<ModDownload.DlOptiFineListEntry, ModDownload.DlOptiFineListEntry, bool> _0024I41_002D0;

		public static Func<ModDownload.DlForgeVersionEntry, ModDownload.DlForgeVersionEntry> _0024I51_002D1;

		public static Func<ResourceVersion, DateTime> _0024I68_002D0;

		public static Func<ResourceVersion, DateTime> _0024I75_002D0;

		static _Closure_0024__()
		{
			_0024I = new _Closure_0024__();
		}

		[SpecialName]
		internal DateTime _Lambda_0024__35_002D0(JObject j)
		{
			return Extensions.Value<DateTime>((IEnumerable<JToken>)j["releaseTime"]);
		}

		[SpecialName]
		internal bool _Lambda_0024__41_002D0(ModDownload.DlOptiFineListEntry Left, ModDownload.DlOptiFineListEntry Right)
		{
			if (!Left.IsPreview && Right.IsPreview)
			{
				return true;
			}
			if (Left.IsPreview && !Right.IsPreview)
			{
				return false;
			}
			return McUtils.CompareVersionGE(Left.DisplayName, Right.DisplayName);
		}

		[SpecialName]
		internal ModDownload.DlForgeVersionEntry _Lambda_0024__51_002D1(ModDownload.DlForgeVersionEntry v)
		{
			return v;
		}

		[SpecialName]
		internal DateTime _Lambda_0024__68_002D0(ResourceVersion v)
		{
			return v.ReleaseDate;
		}

		[SpecialName]
		internal DateTime _Lambda_0024__75_002D0(ResourceVersion v)
		{
			return v.ReleaseDate;
		}
	}

	private bool IsLoad;

	public bool IsInSelectPage;

	public string VanillaName;

	public JObject VanillaData;

	public string VanillaIcon;

	public ModDownload.DlOptiFineListEntry SelectedOptiFine;

	public ModDownload.DlLiteLoaderListEntry SelectedLiteLoader;

	public ModDownload.DlForgeVersionEntry SelectedForge;

	public ModDownload.DlNeoForgeListEntry SelectedNeoForge;

	public string SelectedFabric;

	public ResourceVersion SelectedFabricApi;

	public ResourceVersion SelectedOptiFabric;

	public bool IsSelectNameEdited;

	public JArray MinecraftVersions;

	public static string VersionWaitingSelect = null;

	public bool AutoSelectedFabricApi;

	public bool AutoSelectedOptiFabric;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnBack")]
	private MyIconButton _BtnBack;

	[CompilerGenerated]
	[AccessedThroughProperty("TextSelectName")]
	private MyTextBox _TextSelectName;

	[CompilerGenerated]
	[AccessedThroughProperty("CardForge")]
	private MyCard _CardForge;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnForgeClear")]
	private MyIconButton _BtnForgeClear;

	[CompilerGenerated]
	[AccessedThroughProperty("CardNeoForge")]
	private MyCard _CardNeoForge;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnNeoForgeClear")]
	private MyIconButton _BtnNeoForgeClear;

	[CompilerGenerated]
	[AccessedThroughProperty("CardFabric")]
	private MyCard _CardFabric;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnFabricClear")]
	private MyIconButton _BtnFabricClear;

	[CompilerGenerated]
	[AccessedThroughProperty("CardFabricApi")]
	private MyCard _CardFabricApi;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnFabricApiClear")]
	private MyIconButton _BtnFabricApiClear;

	[CompilerGenerated]
	[AccessedThroughProperty("CardOptiFine")]
	private MyCard _CardOptiFine;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnOptiFineClear")]
	private MyIconButton _BtnOptiFineClear;

	[CompilerGenerated]
	[AccessedThroughProperty("CardOptiFabric")]
	private MyCard _CardOptiFabric;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnOptiFabricClear")]
	private MyIconButton _BtnOptiFabricClear;

	[CompilerGenerated]
	[AccessedThroughProperty("CardLiteLoader")]
	private MyCard _CardLiteLoader;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnLiteLoaderClear")]
	private MyIconButton _BtnLiteLoaderClear;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnStart")]
	private MyExtraTextButton _BtnStart;

	[CompilerGenerated]
	[AccessedThroughProperty("LoadOptiFine")]
	private MyLoading _LoadOptiFine;

	[CompilerGenerated]
	[AccessedThroughProperty("LoadForge")]
	private MyLoading _LoadForge;

	[CompilerGenerated]
	[AccessedThroughProperty("LoadNeoForge")]
	private MyLoading _LoadNeoForge;

	[CompilerGenerated]
	[AccessedThroughProperty("LoadLiteLoader")]
	private MyLoading _LoadLiteLoader;

	[CompilerGenerated]
	[AccessedThroughProperty("LoadFabric")]
	private MyLoading _LoadFabric;

	[CompilerGenerated]
	[AccessedThroughProperty("LoadFabricApi")]
	private MyLoading _LoadFabricApi;

	[CompilerGenerated]
	[AccessedThroughProperty("LoadOptiFabric")]
	private MyLoading _LoadOptiFabric;

	private bool _contentLoaded;

	[SpecialName]
	private bool _0024STATIC_0024ReloadSelected_00242001_0024Ongoing;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024ReloadSelected_00242001_0024Ongoing_0024Init;

	private int VanillaDrop => McVersion.VersionToDrop(VanillaName);

	[field: AccessedThroughProperty("PanAllBack")]
	internal virtual Grid PanAllBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyScrollViewer PanBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanInner")]
	internal virtual Grid PanInner
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanMinecraft")]
	internal virtual StackPanel PanMinecraft
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanSelect")]
	internal virtual StackPanel PanSelect
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardSelectName")]
	internal virtual MyCard CardSelectName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnBack
	{
		[CompilerGenerated]
		get
		{
			return _BtnBack;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				ExitSelectPage();
			};
			MyIconButton btnBack = _BtnBack;
			if (btnBack != null)
			{
				btnBack.Click -= obj;
			}
			_BtnBack = value;
			btnBack = _BtnBack;
			if (btnBack != null)
			{
				btnBack.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("ImgLogo")]
	internal virtual MyImage ImgLogo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyTextBox TextSelectName
	{
		[CompilerGenerated]
		get
		{
			return _TextSelectName;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TextChangedEventHandler value2 = TextSelectName_TextChanged;
			MyTextBox.ValidateChangedEventHandler obj = TextSelectName_ValidateChanged;
			KeyEventHandler value3 = TextSelectName_KeyDown;
			MyTextBox textSelectName = _TextSelectName;
			if (textSelectName != null)
			{
				textSelectName.TextChanged -= value2;
				MyTextBox.ValidateChanged -= obj;
				textSelectName.KeyDown -= value3;
			}
			_TextSelectName = value;
			textSelectName = _TextSelectName;
			if (textSelectName != null)
			{
				textSelectName.TextChanged += value2;
				MyTextBox.ValidateChanged += obj;
				textSelectName.KeyDown += value3;
			}
		}
	}

	[field: AccessedThroughProperty("LabSelectName")]
	internal virtual TextBlock LabSelectName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("BtnOriginalNameRestore")]
	internal virtual MyIconButton BtnOriginalNameRestore
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("HintSelectName")]
	internal virtual MyHint HintSelectName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("HintFabricAPI")]
	internal virtual MyHint HintFabricAPI
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("HintOptiFabric")]
	internal virtual MyHint HintOptiFabric
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("HintOptiFabricOld")]
	internal virtual MyHint HintOptiFabricOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("HintModOptiFine")]
	internal virtual MyHint HintModOptiFine
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardMinecraft")]
	internal virtual MyCard CardMinecraft
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanMinecraftInfo")]
	internal virtual Grid PanMinecraftInfo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgMinecraft")]
	internal virtual MyImage ImgMinecraft
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabMinecraftVersion")]
	internal virtual TextBlock LabMinecraftVersion
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanMinecraftCompare")]
	internal virtual Grid PanMinecraftCompare
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgMinecraftOld")]
	internal virtual MyImage ImgMinecraftOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabMinecraftOld")]
	internal virtual TextBlock LabMinecraftOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgMinecraftNew")]
	internal virtual MyImage ImgMinecraftNew
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabMinecraftNew")]
	internal virtual TextBlock LabMinecraftNew
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("BtnMinecraftRestore")]
	internal virtual MyIconButton BtnMinecraftRestore
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCard CardForge
	{
		[CompilerGenerated]
		get
		{
			return _CardForge;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCard.SwapEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				ReloadSelected();
			};
			MyCard.PreviewSwapEventHandler obj2 = CardForge_PreviewSwap;
			MyCard cardForge = _CardForge;
			if (cardForge != null)
			{
				cardForge.Swap -= obj;
				cardForge.PreviewSwap -= obj2;
			}
			_CardForge = value;
			cardForge = _CardForge;
			if (cardForge != null)
			{
				cardForge.Swap += obj;
				cardForge.PreviewSwap += obj2;
			}
		}
	}

	[field: AccessedThroughProperty("PanForge")]
	internal virtual StackPanel PanForge
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanForgeInfo")]
	internal virtual Grid PanForgeInfo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgForge")]
	internal virtual MyImage ImgForge
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabForge")]
	internal virtual TextBlock LabForge
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanForgeCompare")]
	internal virtual Grid PanForgeCompare
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabForgeOld")]
	internal virtual TextBlock LabForgeOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnForgeClear
	{
		[CompilerGenerated]
		get
		{
			return _BtnForgeClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = Forge_Clear;
			MyIconButton btnForgeClear = _BtnForgeClear;
			if (btnForgeClear != null)
			{
				btnForgeClear.Click -= obj;
			}
			_BtnForgeClear = value;
			btnForgeClear = _BtnForgeClear;
			if (btnForgeClear != null)
			{
				btnForgeClear.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("BtnForgeRestore")]
	internal virtual MyIconButton BtnForgeRestore
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCard CardNeoForge
	{
		[CompilerGenerated]
		get
		{
			return _CardNeoForge;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCard.SwapEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				ReloadSelected();
			};
			MyCard.PreviewSwapEventHandler obj2 = CardNeoForge_PreviewSwap;
			MyCard cardNeoForge = _CardNeoForge;
			if (cardNeoForge != null)
			{
				cardNeoForge.Swap -= obj;
				cardNeoForge.PreviewSwap -= obj2;
			}
			_CardNeoForge = value;
			cardNeoForge = _CardNeoForge;
			if (cardNeoForge != null)
			{
				cardNeoForge.Swap += obj;
				cardNeoForge.PreviewSwap += obj2;
			}
		}
	}

	[field: AccessedThroughProperty("PanNeoForge")]
	internal virtual StackPanel PanNeoForge
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanNeoForgeInfo")]
	internal virtual Grid PanNeoForgeInfo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgNeoForge")]
	internal virtual MyImage ImgNeoForge
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabNeoForge")]
	internal virtual TextBlock LabNeoForge
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanNeoForgeCompare")]
	internal virtual Grid PanNeoForgeCompare
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabNeoForgeOld")]
	internal virtual TextBlock LabNeoForgeOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnNeoForgeClear
	{
		[CompilerGenerated]
		get
		{
			return _BtnNeoForgeClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = NeoForge_Clear;
			MyIconButton btnNeoForgeClear = _BtnNeoForgeClear;
			if (btnNeoForgeClear != null)
			{
				btnNeoForgeClear.Click -= obj;
			}
			_BtnNeoForgeClear = value;
			btnNeoForgeClear = _BtnNeoForgeClear;
			if (btnNeoForgeClear != null)
			{
				btnNeoForgeClear.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("BtnNeoForgeRestore")]
	internal virtual MyIconButton BtnNeoForgeRestore
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCard CardFabric
	{
		[CompilerGenerated]
		get
		{
			return _CardFabric;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCard.SwapEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				ReloadSelected();
			};
			MyCard.PreviewSwapEventHandler obj2 = CardFabric_PreviewSwap;
			MyCard cardFabric = _CardFabric;
			if (cardFabric != null)
			{
				cardFabric.Swap -= obj;
				cardFabric.PreviewSwap -= obj2;
			}
			_CardFabric = value;
			cardFabric = _CardFabric;
			if (cardFabric != null)
			{
				cardFabric.Swap += obj;
				cardFabric.PreviewSwap += obj2;
			}
		}
	}

	[field: AccessedThroughProperty("PanFabric")]
	internal virtual StackPanel PanFabric
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanFabricInfo")]
	internal virtual Grid PanFabricInfo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgFabric")]
	internal virtual MyImage ImgFabric
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabFabric")]
	internal virtual TextBlock LabFabric
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanFabricCompare")]
	internal virtual Grid PanFabricCompare
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabFabricOld")]
	internal virtual TextBlock LabFabricOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnFabricClear
	{
		[CompilerGenerated]
		get
		{
			return _BtnFabricClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = Fabric_Clear;
			MyIconButton btnFabricClear = _BtnFabricClear;
			if (btnFabricClear != null)
			{
				btnFabricClear.Click -= obj;
			}
			_BtnFabricClear = value;
			btnFabricClear = _BtnFabricClear;
			if (btnFabricClear != null)
			{
				btnFabricClear.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("BtnFabricRestore")]
	internal virtual MyIconButton BtnFabricRestore
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCard CardFabricApi
	{
		[CompilerGenerated]
		get
		{
			return _CardFabricApi;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCard.SwapEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				ReloadSelected();
			};
			MyCard.PreviewSwapEventHandler obj2 = CardFabricApi_PreviewSwap;
			MyCard cardFabricApi = _CardFabricApi;
			if (cardFabricApi != null)
			{
				cardFabricApi.Swap -= obj;
				cardFabricApi.PreviewSwap -= obj2;
			}
			_CardFabricApi = value;
			cardFabricApi = _CardFabricApi;
			if (cardFabricApi != null)
			{
				cardFabricApi.Swap += obj;
				cardFabricApi.PreviewSwap += obj2;
			}
		}
	}

	[field: AccessedThroughProperty("PanFabricApi")]
	internal virtual StackPanel PanFabricApi
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanFabricApiInfo")]
	internal virtual Grid PanFabricApiInfo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgFabricApi")]
	internal virtual MyImage ImgFabricApi
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabFabricApi")]
	internal virtual TextBlock LabFabricApi
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanFabricApiCompare")]
	internal virtual Grid PanFabricApiCompare
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabFabricApiOld")]
	internal virtual TextBlock LabFabricApiOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnFabricApiClear
	{
		[CompilerGenerated]
		get
		{
			return _BtnFabricApiClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = FabricApi_Clear;
			MyIconButton btnFabricApiClear = _BtnFabricApiClear;
			if (btnFabricApiClear != null)
			{
				btnFabricApiClear.Click -= obj;
			}
			_BtnFabricApiClear = value;
			btnFabricApiClear = _BtnFabricApiClear;
			if (btnFabricApiClear != null)
			{
				btnFabricApiClear.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("BtnFabricApiRestore")]
	internal virtual MyIconButton BtnFabricApiRestore
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCard CardOptiFine
	{
		[CompilerGenerated]
		get
		{
			return _CardOptiFine;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCard.SwapEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				ReloadSelected();
			};
			MyCard.PreviewSwapEventHandler obj2 = CardOptiFine_PreviewSwap;
			MyCard cardOptiFine = _CardOptiFine;
			if (cardOptiFine != null)
			{
				cardOptiFine.Swap -= obj;
				cardOptiFine.PreviewSwap -= obj2;
			}
			_CardOptiFine = value;
			cardOptiFine = _CardOptiFine;
			if (cardOptiFine != null)
			{
				cardOptiFine.Swap += obj;
				cardOptiFine.PreviewSwap += obj2;
			}
		}
	}

	[field: AccessedThroughProperty("PanOptiFine")]
	internal virtual StackPanel PanOptiFine
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanOptiFineInfo")]
	internal virtual Grid PanOptiFineInfo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgOptiFine")]
	internal virtual MyImage ImgOptiFine
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabOptiFine")]
	internal virtual TextBlock LabOptiFine
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanOptiFineCompare")]
	internal virtual Grid PanOptiFineCompare
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabOptiFineOld")]
	internal virtual TextBlock LabOptiFineOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnOptiFineClear
	{
		[CompilerGenerated]
		get
		{
			return _BtnOptiFineClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = OptiFine_Clear;
			MyIconButton btnOptiFineClear = _BtnOptiFineClear;
			if (btnOptiFineClear != null)
			{
				btnOptiFineClear.Click -= obj;
			}
			_BtnOptiFineClear = value;
			btnOptiFineClear = _BtnOptiFineClear;
			if (btnOptiFineClear != null)
			{
				btnOptiFineClear.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("BtnOptiFineRestore")]
	internal virtual MyIconButton BtnOptiFineRestore
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCard CardOptiFabric
	{
		[CompilerGenerated]
		get
		{
			return _CardOptiFabric;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCard.SwapEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				ReloadSelected();
			};
			MyCard.PreviewSwapEventHandler obj2 = CardOptiFabric_PreviewSwap;
			MyCard cardOptiFabric = _CardOptiFabric;
			if (cardOptiFabric != null)
			{
				cardOptiFabric.Swap -= obj;
				cardOptiFabric.PreviewSwap -= obj2;
			}
			_CardOptiFabric = value;
			cardOptiFabric = _CardOptiFabric;
			if (cardOptiFabric != null)
			{
				cardOptiFabric.Swap += obj;
				cardOptiFabric.PreviewSwap += obj2;
			}
		}
	}

	[field: AccessedThroughProperty("PanOptiFabric")]
	internal virtual StackPanel PanOptiFabric
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanOptiFabricInfo")]
	internal virtual Grid PanOptiFabricInfo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgOptiFabric")]
	internal virtual MyImage ImgOptiFabric
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabOptiFabric")]
	internal virtual TextBlock LabOptiFabric
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanOptiFabricCompare")]
	internal virtual Grid PanOptiFabricCompare
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabOptiFabricOld")]
	internal virtual TextBlock LabOptiFabricOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnOptiFabricClear
	{
		[CompilerGenerated]
		get
		{
			return _BtnOptiFabricClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = OptiFabric_Clear;
			MyIconButton btnOptiFabricClear = _BtnOptiFabricClear;
			if (btnOptiFabricClear != null)
			{
				btnOptiFabricClear.Click -= obj;
			}
			_BtnOptiFabricClear = value;
			btnOptiFabricClear = _BtnOptiFabricClear;
			if (btnOptiFabricClear != null)
			{
				btnOptiFabricClear.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("BtnOptiFabricRestore")]
	internal virtual MyIconButton BtnOptiFabricRestore
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCard CardLiteLoader
	{
		[CompilerGenerated]
		get
		{
			return _CardLiteLoader;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCard.SwapEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				ReloadSelected();
			};
			MyCard.PreviewSwapEventHandler obj2 = CardLiteLoader_PreviewSwap;
			MyCard cardLiteLoader = _CardLiteLoader;
			if (cardLiteLoader != null)
			{
				cardLiteLoader.Swap -= obj;
				cardLiteLoader.PreviewSwap -= obj2;
			}
			_CardLiteLoader = value;
			cardLiteLoader = _CardLiteLoader;
			if (cardLiteLoader != null)
			{
				cardLiteLoader.Swap += obj;
				cardLiteLoader.PreviewSwap += obj2;
			}
		}
	}

	[field: AccessedThroughProperty("PanLiteLoader")]
	internal virtual StackPanel PanLiteLoader
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanLiteLoaderInfo")]
	internal virtual Grid PanLiteLoaderInfo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgLiteLoader")]
	internal virtual MyImage ImgLiteLoader
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabLiteLoader")]
	internal virtual TextBlock LabLiteLoader
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanLiteLoaderCompare")]
	internal virtual Grid PanLiteLoaderCompare
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabLiteLoaderOld")]
	internal virtual TextBlock LabLiteLoaderOld
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnLiteLoaderClear
	{
		[CompilerGenerated]
		get
		{
			return _BtnLiteLoaderClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = LiteLoader_Clear;
			MyIconButton btnLiteLoaderClear = _BtnLiteLoaderClear;
			if (btnLiteLoaderClear != null)
			{
				btnLiteLoaderClear.Click -= obj;
			}
			_BtnLiteLoaderClear = value;
			btnLiteLoaderClear = _BtnLiteLoaderClear;
			if (btnLiteLoaderClear != null)
			{
				btnLiteLoaderClear.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("BtnLiteLoaderRestore")]
	internal virtual MyIconButton BtnLiteLoaderRestore
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyExtraTextButton BtnStart
	{
		[CompilerGenerated]
		get
		{
			return _BtnStart;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyExtraTextButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				BtnStart_Click();
			};
			MyExtraTextButton btnStart = _BtnStart;
			if (btnStart != null)
			{
				btnStart.Click -= obj;
			}
			_BtnStart = value;
			btnStart = _BtnStart;
			if (btnStart != null)
			{
				btnStart.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanLoad")]
	internal virtual MyCard PanLoad
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LoadMinecraft")]
	internal virtual MyLoading LoadMinecraft
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyLoading LoadOptiFine
	{
		[CompilerGenerated]
		get
		{
			return _LoadOptiFine;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyLoading.StateChangedEventHandler obj = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				ReloadSelected();
			};
			MyLoading.StateChangedEventHandler obj2 = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				OptiFine_Loaded();
			};
			MyLoading loadOptiFine = _LoadOptiFine;
			if (loadOptiFine != null)
			{
				loadOptiFine.StateChanged -= obj;
				loadOptiFine.StateChanged -= obj2;
			}
			_LoadOptiFine = value;
			loadOptiFine = _LoadOptiFine;
			if (loadOptiFine != null)
			{
				loadOptiFine.StateChanged += obj;
				loadOptiFine.StateChanged += obj2;
			}
		}
	}

	internal virtual MyLoading LoadForge
	{
		[CompilerGenerated]
		get
		{
			return _LoadForge;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyLoading.StateChangedEventHandler obj = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				ReloadSelected();
			};
			MyLoading.StateChangedEventHandler obj2 = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				Forge_Loaded();
			};
			MyLoading loadForge = _LoadForge;
			if (loadForge != null)
			{
				loadForge.StateChanged -= obj;
				loadForge.StateChanged -= obj2;
			}
			_LoadForge = value;
			loadForge = _LoadForge;
			if (loadForge != null)
			{
				loadForge.StateChanged += obj;
				loadForge.StateChanged += obj2;
			}
		}
	}

	internal virtual MyLoading LoadNeoForge
	{
		[CompilerGenerated]
		get
		{
			return _LoadNeoForge;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyLoading.StateChangedEventHandler obj = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				ReloadSelected();
			};
			MyLoading.StateChangedEventHandler obj2 = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				NeoForge_Loaded();
			};
			MyLoading loadNeoForge = _LoadNeoForge;
			if (loadNeoForge != null)
			{
				loadNeoForge.StateChanged -= obj;
				loadNeoForge.StateChanged -= obj2;
			}
			_LoadNeoForge = value;
			loadNeoForge = _LoadNeoForge;
			if (loadNeoForge != null)
			{
				loadNeoForge.StateChanged += obj;
				loadNeoForge.StateChanged += obj2;
			}
		}
	}

	internal virtual MyLoading LoadLiteLoader
	{
		[CompilerGenerated]
		get
		{
			return _LoadLiteLoader;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyLoading.StateChangedEventHandler obj = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				ReloadSelected();
			};
			MyLoading.StateChangedEventHandler obj2 = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				LiteLoader_Loaded();
			};
			MyLoading loadLiteLoader = _LoadLiteLoader;
			if (loadLiteLoader != null)
			{
				loadLiteLoader.StateChanged -= obj;
				loadLiteLoader.StateChanged -= obj2;
			}
			_LoadLiteLoader = value;
			loadLiteLoader = _LoadLiteLoader;
			if (loadLiteLoader != null)
			{
				loadLiteLoader.StateChanged += obj;
				loadLiteLoader.StateChanged += obj2;
			}
		}
	}

	internal virtual MyLoading LoadFabric
	{
		[CompilerGenerated]
		get
		{
			return _LoadFabric;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyLoading.StateChangedEventHandler obj = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				ReloadSelected();
			};
			MyLoading.StateChangedEventHandler obj2 = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				Fabric_Loaded();
			};
			MyLoading loadFabric = _LoadFabric;
			if (loadFabric != null)
			{
				loadFabric.StateChanged -= obj;
				loadFabric.StateChanged -= obj2;
			}
			_LoadFabric = value;
			loadFabric = _LoadFabric;
			if (loadFabric != null)
			{
				loadFabric.StateChanged += obj;
				loadFabric.StateChanged += obj2;
			}
		}
	}

	internal virtual MyLoading LoadFabricApi
	{
		[CompilerGenerated]
		get
		{
			return _LoadFabricApi;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyLoading.StateChangedEventHandler obj = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				ReloadSelected();
			};
			MyLoading.StateChangedEventHandler obj2 = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				FabricApi_Loaded();
			};
			MyLoading loadFabricApi = _LoadFabricApi;
			if (loadFabricApi != null)
			{
				loadFabricApi.StateChanged -= obj;
				loadFabricApi.StateChanged -= obj2;
			}
			_LoadFabricApi = value;
			loadFabricApi = _LoadFabricApi;
			if (loadFabricApi != null)
			{
				loadFabricApi.StateChanged += obj;
				loadFabricApi.StateChanged += obj2;
			}
		}
	}

	internal virtual MyLoading LoadOptiFabric
	{
		[CompilerGenerated]
		get
		{
			return _LoadOptiFabric;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyLoading.StateChangedEventHandler obj = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				ReloadSelected();
			};
			MyLoading.StateChangedEventHandler obj2 = [SpecialName] (object a0, MyLoading.MyLoadingState a1, MyLoading.MyLoadingState a2) =>
			{
				OptiFabric_Loaded();
			};
			MyLoading loadOptiFabric = _LoadOptiFabric;
			if (loadOptiFabric != null)
			{
				loadOptiFabric.StateChanged -= obj;
				loadOptiFabric.StateChanged -= obj2;
			}
			_LoadOptiFabric = value;
			loadOptiFabric = _LoadOptiFabric;
			if (loadOptiFabric != null)
			{
				loadOptiFabric.StateChanged += obj;
				loadOptiFabric.StateChanged += obj2;
			}
		}
	}

	public PageDownloadInstall()
	{
		base.Initialized += [SpecialName] (object a0, EventArgs a1) =>
		{
			LoaderInit();
		};
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			Init();
		};
		IsLoad = false;
		IsInSelectPage = false;
		SelectedOptiFine = null;
		SelectedLiteLoader = null;
		SelectedForge = null;
		SelectedNeoForge = null;
		SelectedFabric = null;
		SelectedFabricApi = null;
		SelectedOptiFabric = null;
		IsSelectNameEdited = false;
		AutoSelectedFabricApi = false;
		AutoSelectedOptiFabric = false;
		InitializeComponent();
	}

	public void ControlInit()
	{
		DisabledPageAnimControls.Add(BtnStart);
		LoadOptiFine.State = ModDownload.DlOptiFineListLoader;
		LoadLiteLoader.State = ModDownload.DlLiteLoaderListLoader;
		LoadFabric.State = ModDownload.DlFabricListLoader;
		LoadFabricApi.State = ModDownload.DlFabricApiLoader;
		LoadNeoForge.State = ModDownload.DlNeoForgeListLoader;
		LoadOptiFabric.State = ModDownload.DlOptiFabricLoader;
		ModDownloadLib.McDownloadForgeRecommendedRefresh();
	}

	public virtual void LoaderInit()
	{
		ControlInit();
		PageLoaderInit(LoadMinecraft, PanLoad, PanAllBack, null, ModDownload.DlClientListLoader, [SpecialName] (ModLoader.LoaderTask<string, ModDownload.DlClientListResult> a0) =>
		{
			LoadMinecraft_OnFinish();
		});
	}

	public virtual void Init()
	{
		if (IsLoad)
		{
			IsSelectNameEdited = false;
			ExitSelectPage();
			if (ModDownload.DlClientListLoader.State == ModBase.LoadState.Finished)
			{
				TrySelectWaitingVersion();
			}
		}
		IsLoad = true;
		PanBack.ScrollToHome();
		ModDownload.DlOptiFineListLoader.Start();
		ModDownload.DlLiteLoaderListLoader.Start();
		ModDownload.DlFabricListLoader.Start();
		ModDownload.DlNeoForgeListLoader.Start();
		TextSelectName.ValidateRules = new Collection<Validate>
		{
			new ValidateFolderName(ModMinecraft.McFolderSelected + "versions")
		};
		TextSelectName.Validate();
		ReloadSelected();
	}

	public void PrepareSelectPage()
	{
		IsInSelectPage = true;
		PanInner.Margin = new Thickness(25.0, 10.0, 25.0, 40.0);
		PanSelect.Visibility = Visibility.Visible;
		PanSelect.IsHitTestVisible = true;
		PanMinecraft.IsHitTestVisible = false;
		PanBack.IsHitTestVisible = false;
		PanBack.ScrollToHome();
		DisabledPageAnimControls.Remove(BtnStart);
		BtnStart.Show = true;
		MyCard[] array = new MyCard[7] { CardOptiFine, CardLiteLoader, CardForge, CardNeoForge, CardFabric, CardFabricApi, CardOptiFabric };
		for (int i = 0; i < array.Length; i = checked(i + 1))
		{
			array[i].IsSwapped = true;
		}
		foreach (FrameworkElement allAnimControl in GetAllAnimControls(PanSelect))
		{
			allAnimControl.Opacity = 1.0;
			if (allAnimControl.RenderTransform == null || allAnimControl.RenderTransform is TranslateTransform)
			{
				allAnimControl.RenderTransform = new TranslateTransform();
			}
		}
	}

	private void EnterSelectPage()
	{
		if (!IsInSelectPage)
		{
			AutoSelectedFabricApi = false;
			AutoSelectedOptiFabric = false;
			PrepareSelectPage();
			if (McVersion.IsStandardFormat(VanillaName))
			{
				ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = new ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>("DlForgeVersion " + VanillaName, ModDownload.DlForgeVersionMain);
				LoadForge.State = loaderTask;
				loaderTask.Start(VanillaName);
			}
			ModDownload.DlFabricApiLoader.Start();
			ModDownload.DlOptiFabricLoader.Start();
			ModAnimation.AniStart(new ModAnimation.AniData[6]
			{
				ModAnimation.AaOpacity(PanMinecraft, 0.0 - PanMinecraft.Opacity, 70, 10),
				ModAnimation.AaTranslateX(PanMinecraft, -50.0 - ((TranslateTransform)PanMinecraft.RenderTransform).X, 90, 10),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PanBack.ScrollToHome();
					ReloadComponentLists();
					PanMinecraft.Visibility = Visibility.Collapsed;
				}, 0, After: true),
				ModAnimation.AaOpacity(PanSelect, 1.0 - PanSelect.Opacity, 70, 100),
				ModAnimation.AaTranslateX(PanSelect, 0.0 - ((TranslateTransform)PanSelect.RenderTransform).X, 160, 100, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong)),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PanBack.IsHitTestVisible = true;
				}, 0, After: true)
			}, "FrmDownloadInstall SelectPageSwitch", RefreshTime: true);
		}
	}

	public void ReloadComponentLists()
	{
		TextSelectName.Validate();
		OptiFine_Loaded();
		LiteLoader_Loaded();
		Forge_Loaded();
		NeoForge_Loaded();
		Fabric_Loaded();
		FabricApi_Loaded();
		OptiFabric_Loaded();
		ReloadSelected();
	}

	public void ExitSelectPage()
	{
		if (IsInSelectPage)
		{
			IsInSelectPage = false;
			PanInner.Margin = new Thickness(25.0, 10.0, 25.0, 25.0);
			DisabledPageAnimControls.Add(BtnStart);
			BtnStart.Show = false;
			ClearSelected();
			PanMinecraft.Visibility = Visibility.Visible;
			PanSelect.IsHitTestVisible = false;
			PanMinecraft.IsHitTestVisible = true;
			PanBack.IsHitTestVisible = false;
			PanBack.ScrollToHome();
			ModAnimation.AniStart(new ModAnimation.AniData[6]
			{
				ModAnimation.AaOpacity(PanSelect, 0.0 - PanSelect.Opacity, 70, 10),
				ModAnimation.AaTranslateX(PanSelect, 50.0 - ((TranslateTransform)PanSelect.RenderTransform).X, 90, 10),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PanBack.ScrollToHome();
				}, 0, After: true),
				ModAnimation.AaOpacity(PanMinecraft, 1.0 - PanMinecraft.Opacity, 70, 100),
				ModAnimation.AaTranslateX(PanMinecraft, 0.0 - ((TranslateTransform)PanMinecraft.RenderTransform).X, 160, 100, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong)),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PanSelect.Visibility = Visibility.Collapsed;
					PanBack.IsHitTestVisible = true;
				}, 0, After: true)
			}, "FrmDownloadInstall SelectPageSwitch");
		}
	}

	public virtual void MinecraftSelected(MyListItem sender, MouseButtonEventArgs e)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		VanillaName = sender.Title;
		VanillaData = (JObject)sender.Tag;
		VanillaIcon = sender.Logo;
		LabMinecraftVersion.Text = VanillaName;
		EnterSelectPage();
	}

	public virtual void ReloadSelected()
	{
		if (_0024STATIC_0024ReloadSelected_00242001_0024Ongoing_0024Init == null)
		{
			Interlocked.CompareExchange(ref _0024STATIC_0024ReloadSelected_00242001_0024Ongoing_0024Init, new StaticLocalInitFlag(), null);
		}
		bool lockTaken = false;
		try
		{
			Monitor.Enter(_0024STATIC_0024ReloadSelected_00242001_0024Ongoing_0024Init, ref lockTaken);
			if (_0024STATIC_0024ReloadSelected_00242001_0024Ongoing_0024Init.State == 0)
			{
				_0024STATIC_0024ReloadSelected_00242001_0024Ongoing_0024Init.State = 2;
				_0024STATIC_0024ReloadSelected_00242001_0024Ongoing = false;
			}
			else if (_0024STATIC_0024ReloadSelected_00242001_0024Ongoing_0024Init.State == 2)
			{
				throw new IncompleteInitialization();
			}
		}
		finally
		{
			_0024STATIC_0024ReloadSelected_00242001_0024Ongoing_0024Init.State = 1;
			if (lockTaken)
			{
				Monitor.Exit(_0024STATIC_0024ReloadSelected_00242001_0024Ongoing_0024Init);
			}
		}
		if (VanillaName == null || _0024STATIC_0024ReloadSelected_00242001_0024Ongoing)
		{
			return;
		}
		_0024STATIC_0024ReloadSelected_00242001_0024Ongoing = true;
		SelectNameUpdate();
		ImgLogo.Source = GetSelectLogo();
		ImgMinecraft.Source = VanillaIcon;
		string text = LoadOptiFineGetError();
		CardOptiFine.MainSwap.Visibility = ((text != null) ? Visibility.Collapsed : Visibility.Visible);
		if (text != null)
		{
			CardOptiFine.IsSwapped = true;
		}
		SetPanelVisibility(PanOptiFineInfo, CardOptiFine.IsSwapped);
		if (SelectedOptiFine == null)
		{
			BtnOptiFineClear.Visibility = Visibility.Collapsed;
			ImgOptiFine.Visibility = Visibility.Collapsed;
			LabOptiFine.Text = text ?? "可以添加";
			LabOptiFine.Foreground = ModSecret.ColorGray4;
		}
		else
		{
			BtnOptiFineClear.Visibility = Visibility.Visible;
			ImgOptiFine.Visibility = Visibility.Visible;
			LabOptiFine.Text = SelectedOptiFine.DisplayName.Replace(VanillaName + " ", "");
			LabOptiFine.Foreground = ModSecret.ColorGray1;
		}
		if (VanillaDrop >= 130)
		{
			CardLiteLoader.Visibility = Visibility.Collapsed;
		}
		else
		{
			CardLiteLoader.Visibility = Visibility.Visible;
			string text2 = LoadLiteLoaderGetError();
			CardLiteLoader.MainSwap.Visibility = ((text2 != null) ? Visibility.Collapsed : Visibility.Visible);
			if (text2 != null)
			{
				CardLiteLoader.IsSwapped = true;
			}
			SetPanelVisibility(PanLiteLoaderInfo, CardLiteLoader.IsSwapped);
			if (SelectedLiteLoader == null)
			{
				BtnLiteLoaderClear.Visibility = Visibility.Collapsed;
				ImgLiteLoader.Visibility = Visibility.Collapsed;
				LabLiteLoader.Text = text2 ?? "可以添加";
				LabLiteLoader.Foreground = ModSecret.ColorGray4;
			}
			else
			{
				BtnLiteLoaderClear.Visibility = Visibility.Visible;
				ImgLiteLoader.Visibility = Visibility.Visible;
				LabLiteLoader.Text = SelectedLiteLoader.Inherit;
				LabLiteLoader.Foreground = ModSecret.ColorGray1;
			}
		}
		if (!McVersion.IsStandardFormat(VanillaName))
		{
			CardForge.Visibility = Visibility.Collapsed;
		}
		else
		{
			CardForge.Visibility = Visibility.Visible;
			string text3 = LoadForgeGetError();
			CardForge.MainSwap.Visibility = ((text3 != null) ? Visibility.Collapsed : Visibility.Visible);
			if (text3 != null)
			{
				CardForge.IsSwapped = true;
			}
			SetPanelVisibility(PanForgeInfo, CardForge.IsSwapped);
			if (SelectedForge == null)
			{
				BtnForgeClear.Visibility = Visibility.Collapsed;
				ImgForge.Visibility = Visibility.Collapsed;
				LabForge.Text = text3 ?? "可以添加";
				LabForge.Foreground = ModSecret.ColorGray4;
			}
			else
			{
				BtnForgeClear.Visibility = Visibility.Visible;
				ImgForge.Visibility = Visibility.Visible;
				LabForge.Text = SelectedForge.VersionName;
				LabForge.Foreground = ModSecret.ColorGray1;
			}
		}
		if (DateTime.Compare(VanillaData["releaseTime"].ToObject<DateTime>(), new DateTime(2023, 6, 11)) < 0)
		{
			CardNeoForge.Visibility = Visibility.Collapsed;
		}
		else
		{
			CardNeoForge.Visibility = Visibility.Visible;
			string text4 = LoadNeoForgeGetError();
			CardNeoForge.MainSwap.Visibility = ((text4 != null) ? Visibility.Collapsed : Visibility.Visible);
			if (text4 != null)
			{
				CardNeoForge.IsSwapped = true;
			}
			SetPanelVisibility(PanNeoForgeInfo, CardNeoForge.IsSwapped);
			if (SelectedNeoForge == null)
			{
				BtnNeoForgeClear.Visibility = Visibility.Collapsed;
				ImgNeoForge.Visibility = Visibility.Collapsed;
				LabNeoForge.Text = text4 ?? "可以添加";
				LabNeoForge.Foreground = ModSecret.ColorGray4;
			}
			else
			{
				BtnNeoForgeClear.Visibility = Visibility.Visible;
				ImgNeoForge.Visibility = Visibility.Visible;
				LabNeoForge.Text = SelectedNeoForge.VersionName;
				LabNeoForge.Foreground = ModSecret.ColorGray1;
			}
		}
		if (VanillaDrop <= 130)
		{
			CardFabric.Visibility = Visibility.Collapsed;
		}
		else
		{
			CardFabric.Visibility = Visibility.Visible;
			string text5 = LoadFabricGetError();
			CardFabric.MainSwap.Visibility = ((text5 != null) ? Visibility.Collapsed : Visibility.Visible);
			if (text5 != null)
			{
				CardFabric.IsSwapped = true;
			}
			SetPanelVisibility(PanFabricInfo, CardFabric.IsSwapped);
			if (SelectedFabric == null)
			{
				BtnFabricClear.Visibility = Visibility.Collapsed;
				ImgFabric.Visibility = Visibility.Collapsed;
				LabFabric.Text = text5 ?? "可以添加";
				LabFabric.Foreground = ModSecret.ColorGray4;
			}
			else
			{
				BtnFabricClear.Visibility = Visibility.Visible;
				ImgFabric.Visibility = Visibility.Visible;
				LabFabric.Text = SelectedFabric.Replace("+build", "");
				LabFabric.Foreground = ModSecret.ColorGray1;
			}
		}
		if (SelectedFabric == null)
		{
			CardFabricApi.Visibility = Visibility.Collapsed;
		}
		else
		{
			CardFabricApi.Visibility = Visibility.Visible;
			string text6 = LoadFabricApiGetError();
			CardFabricApi.MainSwap.Visibility = ((text6 != null) ? Visibility.Collapsed : Visibility.Visible);
			if (text6 != null || SelectedFabric == null)
			{
				CardFabricApi.IsSwapped = true;
			}
			SetPanelVisibility(PanFabricApiInfo, CardFabricApi.IsSwapped);
			if (SelectedFabricApi == null)
			{
				BtnFabricApiClear.Visibility = Visibility.Collapsed;
				ImgFabricApi.Visibility = Visibility.Collapsed;
				LabFabricApi.Text = text6 ?? "可以添加";
				LabFabricApi.Foreground = ModSecret.ColorGray4;
			}
			else
			{
				BtnFabricApiClear.Visibility = Visibility.Visible;
				ImgFabricApi.Visibility = Visibility.Visible;
				LabFabricApi.Text = StringExtensions.Split(StringExtensions.Split(SelectedFabricApi.Display, "]", false)[1].Replace("Fabric API ", "").Replace(" build ", "."), "+", false).First().Trim();
				LabFabricApi.Foreground = ModSecret.ColorGray1;
			}
		}
		if (SelectedFabric == null || SelectedOptiFine == null)
		{
			CardOptiFabric.Visibility = Visibility.Collapsed;
		}
		else
		{
			CardOptiFabric.Visibility = Visibility.Visible;
			string text7 = LoadOptiFabricGetError();
			CardOptiFabric.MainSwap.Visibility = ((text7 != null) ? Visibility.Collapsed : Visibility.Visible);
			if (text7 != null || SelectedFabric == null)
			{
				CardOptiFabric.IsSwapped = true;
			}
			SetPanelVisibility(PanOptiFabricInfo, CardOptiFabric.IsSwapped);
			if (SelectedOptiFabric == null)
			{
				BtnOptiFabricClear.Visibility = Visibility.Collapsed;
				ImgOptiFabric.Visibility = Visibility.Collapsed;
				LabOptiFabric.Text = text7 ?? "可以添加";
				LabOptiFabric.Foreground = ModSecret.ColorGray4;
			}
			else
			{
				BtnOptiFabricClear.Visibility = Visibility.Visible;
				ImgOptiFabric.Visibility = Visibility.Visible;
				LabOptiFabric.Text = StringExtensions.Lower(SelectedOptiFabric.Display).Replace("optifabric-", "").Replace(".jar", "")
					.Trim()
					.TrimStart('v');
				LabOptiFabric.Foreground = ModSecret.ColorGray1;
			}
		}
		if (SelectedFabric != null && SelectedFabricApi == null)
		{
			HintFabricAPI.Visibility = Visibility.Visible;
		}
		else
		{
			HintFabricAPI.Visibility = Visibility.Collapsed;
		}
		if (SelectedFabric != null && SelectedOptiFine != null && SelectedOptiFabric == null)
		{
			if (VanillaDrop >= 140 && VanillaDrop <= 150)
			{
				HintOptiFabric.Visibility = Visibility.Collapsed;
				HintOptiFabricOld.Visibility = Visibility.Visible;
			}
			else
			{
				HintOptiFabric.Visibility = Visibility.Visible;
				HintOptiFabricOld.Visibility = Visibility.Collapsed;
			}
		}
		else
		{
			HintOptiFabric.Visibility = Visibility.Collapsed;
			HintOptiFabricOld.Visibility = Visibility.Collapsed;
		}
		if (VanillaDrop >= 160 && SelectedOptiFine != null && (SelectedForge != null || SelectedFabric != null))
		{
			HintModOptiFine.Visibility = Visibility.Visible;
		}
		else
		{
			HintModOptiFine.Visibility = Visibility.Collapsed;
		}
		_0024STATIC_0024ReloadSelected_00242001_0024Ongoing = false;
	}

	public void ClearSelected()
	{
		VanillaName = null;
		VanillaData = null;
		VanillaIcon = null;
		SelectedOptiFine = null;
		SelectedLiteLoader = null;
		SelectedForge = null;
		SelectedNeoForge = null;
		SelectedFabric = null;
		SelectedFabricApi = null;
		SelectedOptiFabric = null;
		MyIconButton[] array = new MyIconButton[7] { BtnOptiFineClear, BtnLiteLoaderClear, BtnForgeClear, BtnNeoForgeClear, BtnFabricClear, BtnFabricApiClear, BtnOptiFabricClear };
		for (int i = 0; i < array.Length; i = checked(i + 1))
		{
			array[i].Visibility = Visibility.Collapsed;
		}
	}

	private void SetPanelVisibility(Grid Panel, bool IsVisible)
	{
		if (!Operators.ConditionalCompareObjectEqual(Panel.Tag, IsVisible.ToString(), TextCompare: false))
		{
			Panel.Tag = IsVisible.ToString();
			if (IsVisible)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaTranslateY(Panel, 0.0 - ((TranslateTransform)Panel.RenderTransform).Y, 150, 0, new ModAnimation.AniEaseOutFluent()),
					ModAnimation.AaOpacity(Panel, 1.0 - Panel.Opacity, 60)
				}, "PageDownloadInstall Visibility " + Panel.Name);
			}
			else
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaTranslateY(Panel, 6.0 - ((TranslateTransform)Panel.RenderTransform).Y, 60),
					ModAnimation.AaOpacity(Panel, 0.0 - Panel.Opacity, 60)
				}, "PageDownloadInstall Visibility " + Panel.Name);
			}
		}
	}

	public virtual string GetSelectLogo()
	{
		if (SelectedFabric != null)
		{
			return "pack://application:,,,/images/Blocks/Fabric.png";
		}
		if (SelectedForge != null)
		{
			return "pack://application:,,,/images/Blocks/Anvil.png";
		}
		if (SelectedNeoForge != null)
		{
			return "pack://application:,,,/images/Blocks/NeoForge.png";
		}
		if (SelectedLiteLoader != null)
		{
			return "pack://application:,,,/images/Blocks/Egg.png";
		}
		if (SelectedOptiFine != null)
		{
			return "pack://application:,,,/images/Blocks/GrassPath.png";
		}
		return VanillaIcon;
	}

	public string GetSelectName()
	{
		string text = VanillaName;
		if (SelectedFabric != null)
		{
			text = text + "-Fabric " + StringExtensions.BeforeFirst(SelectedFabric, "+", false);
		}
		if (SelectedForge != null)
		{
			text = text + "-Forge_" + SelectedForge.VersionName;
		}
		if (SelectedNeoForge != null)
		{
			text = text + "-NeoForge_" + StringExtensions.BeforeFirst(SelectedNeoForge.VersionName, "+", false);
		}
		if (SelectedLiteLoader != null)
		{
			text += "-LiteLoader";
		}
		if (SelectedOptiFine != null)
		{
			text = text + "-OptiFine_" + SelectedOptiFine.DisplayName.Replace(VanillaName + " ", "").Replace(" ", "_");
		}
		return text;
	}

	private void SelectNameUpdate()
	{
		if (!IsSelectNameEdited)
		{
			TextSelectName.Text = GetSelectName();
			IsSelectNameEdited = false;
		}
	}

	private void TextSelectName_TextChanged(object sender, TextChangedEventArgs e)
	{
		IsSelectNameEdited = true;
	}

	private void TextSelectName_ValidateChanged(object sender, EventArgs e)
	{
		BtnStart.IsEnabled = TextSelectName.IsValidated;
		HintSelectName.Text = TextSelectName.ValidateResult;
		HintSelectName.Visibility = (TextSelectName.IsValidated ? Visibility.Collapsed : Visibility.Visible);
	}

	private void LoadMinecraft_OnFinish()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		MinecraftVersions = (JArray)ModDownload.DlClientListLoader.Output.Value["versions"].DeepClone();
		ShowMinecraftVersionList();
		TrySelectWaitingVersion();
	}

	public virtual void ShowMinecraftVersionList()
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Expected O, but got Unknown
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Expected O, but got Unknown
		ExitSelectPage();
		try
		{
			Dictionary<string, List<JObject>> dictionary = new Dictionary<string, List<JObject>>
			{
				{
					"正式版",
					new List<JObject>()
				},
				{
					"预览版",
					new List<JObject>()
				},
				{
					"远古版",
					new List<JObject>()
				},
				{
					"愚人节版",
					new List<JObject>()
				}
			};
			foreach (JObject minecraftVersion in MinecraftVersions)
			{
				JObject val = minecraftVersion;
				string key;
				switch ((string)val["type"])
				{
				case "release":
					key = "正式版";
					break;
				case "snapshot":
					key = "预览版";
					if (StringExtensions.StartsWithF(val["id"].ToString(), "1.", false) && !StringExtensions.Lower(val["id"].ToString()).Contains("combat") && !StringExtensions.Lower(val["id"].ToString()).Contains("rc") && !StringExtensions.Lower(val["id"].ToString()).Contains("experimental") && !StringExtensions.Lower(val["id"].ToString()).Contains("pre"))
					{
						key = "正式版";
						val["type"] = JToken.op_Implicit("release");
					}
					switch (StringExtensions.Lower(val["id"].ToString()))
					{
					case "20w14infinite":
					case "20w14∞":
						key = "愚人节版";
						val["id"] = JToken.op_Implicit("20w14∞");
						val["type"] = JToken.op_Implicit("special");
						val.Add("lore", JToken.op_Implicit(ModMinecraft.GetMcFoolName((string)val["id"])));
						break;
					case "3d shareware v1.34":
					case "1.rv-pre1":
					case "15w14a":
					case "2.0":
					case "22w13oneblockatatime":
					case "23w13a_or_b":
					case "24w14potato":
					case "25w14craftmine":
					case "26w14a":
						key = "愚人节版";
						val["type"] = JToken.op_Implicit("special");
						val.Add("lore", JToken.op_Implicit(ModMinecraft.GetMcFoolName((string)val["id"])));
						break;
					default:
					{
						DateTime dateTime = Extensions.Value<DateTime>((IEnumerable<JToken>)val["releaseTime"]).ToUniversalTime().AddHours(2.0);
						if (dateTime.Month == 4 && dateTime.Day == 1)
						{
							key = "愚人节版";
							val["type"] = JToken.op_Implicit("special");
						}
						break;
					}
					}
					break;
				case "special":
					key = "愚人节版";
					break;
				default:
					key = "远古版";
					break;
				}
				dictionary[key].Add(val);
			}
			foreach (KeyValuePair<string, List<JObject>> item in dictionary.ToList())
			{
				dictionary[item.Key] = item.Value.OrderByDescending((_Closure_0024__._0024I35_002D0 == null) ? (_Closure_0024__._0024I35_002D0 = [SpecialName] (JObject j) => Extensions.Value<DateTime>((IEnumerable<JToken>)j["releaseTime"])) : _Closure_0024__._0024I35_002D0).ToList();
			}
			PanMinecraft.Children.Clear();
			MyCard myCard = new MyCard
			{
				Title = "最新版本",
				Margin = new Thickness(0.0, 15.0, 0.0, 15.0)
			};
			List<JObject> list = new List<JObject>();
			JObject val2 = (JObject)((JToken)dictionary["正式版"][0]).DeepClone();
			val2["lore"] = JToken.op_Implicit("最新正式版，发布于 " + Extensions.Value<DateTime>((IEnumerable<JToken>)val2["releaseTime"]).ToString("yyyy'/'MM'/'dd HH':'mm"));
			list.Add(val2);
			if (DateTime.Compare(Extensions.Value<DateTime>((IEnumerable<JToken>)dictionary["正式版"][0]["releaseTime"]), Extensions.Value<DateTime>((IEnumerable<JToken>)dictionary["预览版"][0]["releaseTime"])) < 0)
			{
				JObject val3 = (JObject)((JToken)dictionary["预览版"][0]).DeepClone();
				val3["lore"] = JToken.op_Implicit("最新预览版，发布于 " + Extensions.Value<DateTime>((IEnumerable<JToken>)val3["releaseTime"]).ToString("yyyy'/'MM'/'dd HH':'mm"));
				list.Add(val3);
			}
			StackPanel stackPanel = new StackPanel
			{
				Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
				VerticalAlignment = VerticalAlignment.Top,
				RenderTransform = new TranslateTransform(0.0, 0.0)
			};
			myCard.Children.Add(stackPanel);
			myCard.SwapControl = stackPanel;
			myCard.SetContentBuilder(list, [SpecialName] (JObject Version) => ModDownloadLib.McDownloadListItem(Version, [SpecialName] (object sender, MouseButtonEventArgs e) =>
			{
				MinecraftSelected((MyListItem)sender, e);
			}, IsSaveOnly: false));
			myCard.BuildContent();
			PanMinecraft.Children.Insert(0, myCard);
			foreach (KeyValuePair<string, List<JObject>> item2 in dictionary)
			{
				if (!item2.Value.Any())
				{
					continue;
				}
				MyCard myCard2 = new MyCard
				{
					Title = item2.Key,
					Margin = new Thickness(0.0, 0.0, 0.0, 15.0)
				};
				StackPanel stackPanel2 = new StackPanel
				{
					Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
					VerticalAlignment = VerticalAlignment.Top,
					RenderTransform = new TranslateTransform(0.0, 0.0)
				};
				myCard2.Children.Add(stackPanel2);
				myCard2.SwapControl = stackPanel2;
				myCard2.SetContentBuilder(item2.Value, [SpecialName] (JObject Version) => ModDownloadLib.McDownloadListItem(Version, [SpecialName] (object sender, MouseButtonEventArgs e) =>
				{
					MinecraftSelected((MyListItem)sender, e);
				}, IsSaveOnly: false));
				myCard2.IsSwapped = true;
				PanMinecraft.Children.Add(myCard2);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "可视化安装版本列表出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void TrySelectWaitingVersion()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (VersionWaitingSelect == null)
		{
			return;
		}
		foreach (JObject minecraftVersion in MinecraftVersions)
		{
			JObject val = minecraftVersion;
			if (Operators.CompareString(val["id"].ToString(), VersionWaitingSelect, TextCompare: false) == 0)
			{
				Logger.Info($"自动选择 MC 版本：{VersionWaitingSelect}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
				MinecraftSelected(ModDownloadLib.McDownloadListItem(val, null, IsSaveOnly: false).Init(), null);
				VersionWaitingSelect = null;
				break;
			}
		}
	}

	public string LoadOptiFineGetError()
	{
		if (SelectedNeoForge != null)
		{
			return "与 NeoForge 不兼容";
		}
		if (SelectedForge != null && McUtils.CompareVersionGE(VanillaName, "1.13") && McUtils.CompareVersionGE("1.14.3", VanillaName))
		{
			return "与 Forge 不兼容";
		}
		if (SelectedFabric != null && McUtils.CompareVersion(VanillaName, "1.20.4") > 0)
		{
			return "与 Fabric 不兼容";
		}
		if (GetLoaderError(LoadOptiFine) != null)
		{
			return GetLoaderError(LoadOptiFine);
		}
		bool flag = false;
		bool flag2 = false;
		foreach (ModDownload.DlOptiFineListEntry item in ModDownload.DlOptiFineListLoader.Output.Value)
		{
			if (StringExtensions.StartsWithF(item.DisplayName, VanillaName + " ", false))
			{
				flag = true;
				if (SelectedForge == null)
				{
					return null;
				}
				if (IsOptiFineSuitForForge(item, SelectedForge))
				{
					return null;
				}
				if (item.RequiredForgeVersion != null)
				{
					flag2 = true;
				}
			}
		}
		if (!flag)
		{
			return "无";
		}
		if (flag2)
		{
			return "仅兼容特定版本的 Forge";
		}
		return "与 Forge 不兼容";
	}

	public static bool IsOptiFineSuitForForge(ModDownload.DlOptiFineListEntry OptiFine, ModDownload.DlForgeVersionEntry Forge)
	{
		if (Operators.CompareString(Forge.Inherit, OptiFine.Inherit, TextCompare: false) != 0)
		{
			return false;
		}
		if (OptiFine.RequiredForgeVersion == null)
		{
			return false;
		}
		if (string.IsNullOrWhiteSpace(OptiFine.RequiredForgeVersion))
		{
			return true;
		}
		if (OptiFine.RequiredForgeVersion.Contains("."))
		{
			return McUtils.CompareVersion(Forge.Version.ToString(), OptiFine.RequiredForgeVersion) == 0;
		}
		return (double)Forge.Version.Revision == Conversions.ToDouble(OptiFine.RequiredForgeVersion);
	}

	private void CardOptiFine_PreviewSwap(object sender, ModBase.RouteEventArgs e)
	{
		if (LoadOptiFineGetError() != null)
		{
			e.Handled = true;
		}
	}

	private void OptiFine_Loaded()
	{
		try
		{
			if (ModDownload.DlOptiFineListLoader.State != ModBase.LoadState.Finished)
			{
				return;
			}
			List<ModDownload.DlOptiFineListEntry> list = new List<ModDownload.DlOptiFineListEntry>();
			foreach (ModDownload.DlOptiFineListEntry item in ModDownload.DlOptiFineListLoader.Output.Value)
			{
				if ((SelectedForge == null || IsOptiFineSuitForForge(item, SelectedForge)) && StringExtensions.StartsWithF(item.DisplayName, VanillaName + " ", false))
				{
					list.Add(item);
				}
			}
			if (!list.Any())
			{
				return;
			}
			list = ModBase.SortByComparison(list, [SpecialName] (ModDownload.DlOptiFineListEntry Left, ModDownload.DlOptiFineListEntry Right) =>
			{
				if (!Left.IsPreview && Right.IsPreview)
				{
					return true;
				}
				return (!Left.IsPreview || Right.IsPreview) && McUtils.CompareVersionGE(Left.DisplayName, Right.DisplayName);
			});
			PanOptiFine.Children.Clear();
			foreach (ModDownload.DlOptiFineListEntry item2 in list)
			{
				PanOptiFine.Children.Add(ModDownloadLib.OptiFineDownloadListItem(item2, [SpecialName] (object a0, MouseButtonEventArgs a1) =>
				{
					OptiFine_Selected((MyListItem)a0, a1);
				}, IsSaveOnly: false));
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "可视化 OptiFine 安装版本列表出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void OptiFine_Selected(MyListItem sender, EventArgs e)
	{
		SelectedOptiFine = (ModDownload.DlOptiFineListEntry)sender.Tag;
		if (SelectedForge != null && !IsOptiFineSuitForForge(SelectedOptiFine, SelectedForge))
		{
			SelectedForge = null;
		}
		OptiFabric_Loaded();
		Forge_Loaded();
		NeoForge_Loaded();
		CardOptiFine.IsSwapped = true;
		ReloadSelected();
	}

	private void OptiFine_Clear(object sender, EventArgs e)
	{
		SelectedOptiFine = null;
		SelectedOptiFabric = null;
		BtnOptiFabricClear.Visibility = Visibility.Collapsed;
		AutoSelectedOptiFabric = false;
		CardOptiFine.IsSwapped = true;
		Forge_Loaded();
		NeoForge_Loaded();
		ReloadSelected();
	}

	public string LoadLiteLoaderGetError()
	{
		if (GetLoaderError(LoadLiteLoader) != null)
		{
			return GetLoaderError(LoadLiteLoader);
		}
		return ModDownload.DlLiteLoaderListLoader.Output.Value.Any([SpecialName] (ModDownload.DlLiteLoaderListEntry v) => Operators.CompareString(v.Inherit, VanillaName, TextCompare: false) == 0) ? null : "无";
	}

	private void CardLiteLoader_PreviewSwap(object sender, ModBase.RouteEventArgs e)
	{
		if (LoadLiteLoaderGetError() != null)
		{
			e.Handled = true;
		}
	}

	private void LiteLoader_Loaded()
	{
		try
		{
			if (ModDownload.DlLiteLoaderListLoader.State != ModBase.LoadState.Finished)
			{
				return;
			}
			List<ModDownload.DlLiteLoaderListEntry> list = new List<ModDownload.DlLiteLoaderListEntry>();
			foreach (ModDownload.DlLiteLoaderListEntry item in ModDownload.DlLiteLoaderListLoader.Output.Value)
			{
				if (Operators.CompareString(item.Inherit, VanillaName, TextCompare: false) == 0)
				{
					list.Add(item);
				}
			}
			if (!list.Any())
			{
				return;
			}
			PanLiteLoader.Children.Clear();
			foreach (ModDownload.DlLiteLoaderListEntry item2 in list)
			{
				PanLiteLoader.Children.Add(ModDownloadLib.LiteLoaderDownloadListItem(item2, [SpecialName] (object a0, MouseButtonEventArgs a1) =>
				{
					LiteLoader_Selected((MyListItem)a0, a1);
				}, IsSaveOnly: false));
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "可视化 LiteLoader 安装版本列表出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void LiteLoader_Selected(MyListItem sender, EventArgs e)
	{
		SelectedLiteLoader = (ModDownload.DlLiteLoaderListEntry)sender.Tag;
		CardLiteLoader.IsSwapped = true;
		ReloadSelected();
	}

	private void LiteLoader_Clear(object sender, EventArgs e)
	{
		SelectedLiteLoader = null;
		CardLiteLoader.IsSwapped = true;
		ReloadSelected();
	}

	public string LoadForgeGetError()
	{
		if (McUtils.CompareVersionGE("1.5.1", VanillaName) && McUtils.CompareVersionGE(VanillaName, "1.1"))
		{
			return "无";
		}
		if (GetLoaderError(LoadForge) != null)
		{
			return GetLoaderError(LoadForge);
		}
		ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = (ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>)LoadForge.State;
		if (Operators.CompareString(VanillaName, loaderTask.Input, TextCompare: false) != 0)
		{
			return "获取中……";
		}
		foreach (ModDownload.DlForgeVersionEntry item in loaderTask.Output)
		{
			if (Operators.CompareString(item.Category, "universal", TextCompare: false) != 0 && Operators.CompareString(item.Category, "client", TextCompare: false) != 0)
			{
				if (SelectedNeoForge != null)
				{
					return "与 NeoForge 不兼容";
				}
				if (SelectedFabric != null)
				{
					return "与 Fabric 不兼容";
				}
				if (SelectedOptiFine != null && McUtils.CompareVersionGE(VanillaName, "1.13") && McUtils.CompareVersionGE("1.14.3", VanillaName))
				{
					return "与 OptiFine 不兼容";
				}
				if (SelectedOptiFine == null || IsOptiFineSuitForForge(SelectedOptiFine, item))
				{
					return null;
				}
			}
		}
		return "与 OptiFine 不兼容";
	}

	private void CardForge_PreviewSwap(object sender, ModBase.RouteEventArgs e)
	{
		if (LoadForgeGetError() != null)
		{
			e.Handled = true;
		}
	}

	private void Forge_Loaded()
	{
		try
		{
			if (!LoadForge.State.IsLoader)
			{
				return;
			}
			ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>> loaderTask = (ModLoader.LoaderTask<string, List<ModDownload.DlForgeVersionEntry>>)LoadForge.State;
			if (Operators.CompareString(VanillaName, loaderTask.Input, TextCompare: false) != 0 || loaderTask.State != ModBase.LoadState.Finished)
			{
				return;
			}
			List<ModDownload.DlForgeVersionEntry> source = loaderTask.Output.ToList();
			if (!loaderTask.Output.Any())
			{
				return;
			}
			PanForge.Children.Clear();
			source = (from v in source.Where([SpecialName] (ModDownload.DlForgeVersionEntry v) =>
				{
					if (Operators.CompareString(v.Category, "universal", TextCompare: false) == 0 || Operators.CompareString(v.Category, "client", TextCompare: false) == 0)
					{
						return false;
					}
					return (SelectedOptiFine == null || IsOptiFineSuitForForge(SelectedOptiFine, v)) ? true : false;
				})
				orderby v descending
				select v).ToList();
			ModDownloadLib.ForgeDownloadListItemPreload(PanForge, source, [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Forge_Selected((MyListItem)a0, a1);
			}, IsSaveOnly: false);
			foreach (ModDownload.DlForgeVersionEntry item in source)
			{
				PanForge.Children.Add(ModDownloadLib.ForgeDownloadListItem(item, [SpecialName] (object a0, MouseButtonEventArgs a1) =>
				{
					Forge_Selected((MyListItem)a0, a1);
				}, IsSaveOnly: false));
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "可视化 Forge 安装版本列表出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void Forge_Selected(MyListItem sender, EventArgs e)
	{
		SelectedForge = (ModDownload.DlForgeVersionEntry)sender.Tag;
		CardForge.IsSwapped = true;
		if (SelectedOptiFine != null && !IsOptiFineSuitForForge(SelectedOptiFine, SelectedForge))
		{
			SelectedOptiFine = null;
		}
		OptiFine_Loaded();
		ReloadSelected();
	}

	private void Forge_Clear(object sender, EventArgs e)
	{
		SelectedForge = null;
		CardForge.IsSwapped = true;
		OptiFine_Loaded();
		ReloadSelected();
	}

	public string LoadNeoForgeGetError()
	{
		if (SelectedOptiFine != null)
		{
			return "与 OptiFine 不兼容";
		}
		if (SelectedForge != null)
		{
			return "与 Forge 不兼容";
		}
		if (SelectedFabric != null)
		{
			return "与 Fabric 不兼容";
		}
		if (GetLoaderError(LoadNeoForge) != null)
		{
			return GetLoaderError(LoadNeoForge);
		}
		return ModDownload.DlNeoForgeListLoader.Output.Value.Any([SpecialName] (ModDownload.DlNeoForgeListEntry v) => Operators.CompareString(v.Inherit, VanillaName, TextCompare: false) == 0) ? null : "无";
	}

	private void CardNeoForge_PreviewSwap(object sender, ModBase.RouteEventArgs e)
	{
		if (LoadNeoForgeGetError() != null)
		{
			e.Handled = true;
		}
	}

	private void NeoForge_Loaded()
	{
		try
		{
			if (ModDownload.DlNeoForgeListLoader.State != ModBase.LoadState.Finished)
			{
				return;
			}
			List<ModDownload.DlNeoForgeListEntry> list = ModDownload.DlNeoForgeListLoader.Output.Value.Where([SpecialName] (ModDownload.DlNeoForgeListEntry v) => Operators.CompareString(v.Inherit, VanillaName, TextCompare: false) == 0).ToList();
			if (!list.Any())
			{
				return;
			}
			PanNeoForge.Children.Clear();
			ModDownloadLib.NeoForgeDownloadListItemPreload(PanNeoForge, list, [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				NeoForge_Selected((MyListItem)a0, a1);
			}, IsSaveOnly: false);
			foreach (ModDownload.DlNeoForgeListEntry item in list)
			{
				PanNeoForge.Children.Add(ModDownloadLib.NeoForgeDownloadListItem(item, [SpecialName] (object a0, MouseButtonEventArgs a1) =>
				{
					NeoForge_Selected((MyListItem)a0, a1);
				}, IsSaveOnly: false));
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "可视化 NeoForge 安装版本列表出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void NeoForge_Selected(MyListItem sender, EventArgs e)
	{
		SelectedNeoForge = (ModDownload.DlNeoForgeListEntry)sender.Tag;
		CardNeoForge.IsSwapped = true;
		OptiFine_Loaded();
		ReloadSelected();
	}

	private void NeoForge_Clear(object sender, EventArgs e)
	{
		SelectedNeoForge = null;
		CardNeoForge.IsSwapped = true;
		OptiFine_Loaded();
		ReloadSelected();
	}

	public string LoadFabricGetError()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (SelectedOptiFine != null && McUtils.CompareVersionGE(VanillaName, "1.20.5"))
		{
			return "与 OptiFine 不兼容";
		}
		if (GetLoaderError(LoadFabric) != null)
		{
			return GetLoaderError(LoadFabric);
		}
		foreach (JObject item in (IEnumerable<JToken>)ModDownload.DlFabricListLoader.Output.Value["game"])
		{
			if (Operators.CompareString(item["version"].ToString(), VanillaName.Replace("∞", "infinite").Replace("Combat Test 7c", "1.16_combat-3"), TextCompare: false) == 0)
			{
				if (SelectedForge != null)
				{
					return "与 Forge 不兼容";
				}
				if (SelectedNeoForge != null)
				{
					return "与 NeoForge 不兼容";
				}
				return null;
			}
		}
		return "无";
	}

	private void CardFabric_PreviewSwap(object sender, ModBase.RouteEventArgs e)
	{
		if (LoadFabricGetError() != null)
		{
			e.Handled = true;
		}
	}

	private void Fabric_Loaded()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		try
		{
			if (ModDownload.DlFabricListLoader.State != ModBase.LoadState.Finished)
			{
				return;
			}
			JArray source = (JArray)ModDownload.DlFabricListLoader.Output.Value["loader"];
			if (!((IEnumerable<JToken>)source).Any())
			{
				return;
			}
			PanFabric.Children.Clear();
			CardFabric.SwapControl = PanFabric;
			CardFabric.SetContentBuilder(((IEnumerable)source).OfType<JObject>(), [SpecialName] (JObject Version) => ModDownloadLib.FabricDownloadListItem(Version, [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Fabric_Selected((MyListItem)a0, a1);
			}));
			if (!CardFabric.IsSwapped)
			{
				CardFabric.BuildContent();
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "可视化 Fabric 安装版本列表出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	public void Fabric_Selected(MyListItem sender, EventArgs e)
	{
		SelectedFabric = NewLateBinding.LateIndexGet(sender.Tag, new object[1] { "version" }, null).ToString();
		FabricApi_Loaded();
		OptiFabric_Loaded();
		CardFabric.IsSwapped = true;
		ReloadSelected();
	}

	private void Fabric_Clear(object sender, EventArgs e)
	{
		SelectedFabric = null;
		SelectedFabricApi = null;
		BtnFabricApiClear.Visibility = Visibility.Collapsed;
		AutoSelectedFabricApi = false;
		SelectedOptiFabric = null;
		BtnOptiFabricClear.Visibility = Visibility.Collapsed;
		AutoSelectedOptiFabric = false;
		CardFabric.IsSwapped = true;
		ReloadSelected();
	}

	public static bool IsFabricApiCompatible(ResourceVersion FabricApi, string MinecraftVersion)
	{
		string text = FabricApi.Display;
		checked
		{
			bool result;
			try
			{
				if (text == null || MinecraftVersion == null)
				{
					result = false;
				}
				else
				{
					text = StringExtensions.Lower(text);
					string text2 = StringExtensions.Lower(MinecraftVersion.Replace("∞", "infinite").Replace("Combat Test 7c", "1.16_combat-3"));
					if (StringExtensions.StartsWithF(text, "[" + text2 + "]", false))
					{
						result = true;
					}
					else if (!text.Contains("/") || !text.Contains("]"))
					{
						result = false;
					}
					else
					{
						string[] array = StringExtensions.Split(StringExtensions.BeforeFirst(text, "]", false).TrimStart('['), "/", false);
						int num = 0;
						while (true)
						{
							if (num < array.Length)
							{
								if (Operators.CompareString(array[num], text2, TextCompare: false) == 0)
								{
									result = true;
									break;
								}
								num++;
								continue;
							}
							List<string> list = StringExtensions.RegexSearch(StringExtensions.BeforeFirst(text, "]", false), "[a-z/]+|[0-9/]+", RegexOptions.None).ToList();
							List<string> list2 = StringExtensions.RegexSearch(StringExtensions.BeforeFirst(text2, "]", false), "[a-z/]+|[0-9/]+", RegexOptions.None).ToList();
							int num2 = 0;
							while (true)
							{
								if (list.Count - 1 < num2 && list2.Count - 1 < num2)
								{
									result = true;
									break;
								}
								string text3 = ((list.Count - 1 < num2) ? "-1" : list[num2]);
								string text4 = ((list2.Count - 1 < num2) ? "-1" : list2[num2]);
								if (!text3.Contains("/"))
								{
									if (Operators.CompareString(text3, text4, TextCompare: false) != 0)
									{
										result = false;
										break;
									}
								}
								else if (!text3.Contains(text4))
								{
									result = false;
									break;
								}
								num2++;
							}
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"判断 Fabric API 版本适配性出错（{text}, {MinecraftVersion}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}

	public string LoadFabricApiGetError()
	{
		if (GetLoaderError(LoadFabricApi) != null)
		{
			return GetLoaderError(LoadFabricApi);
		}
		if (ModDownload.DlFabricApiLoader.Output == null)
		{
			return (SelectedFabric == null) ? "需要安装 Fabric" : "获取中……";
		}
		if (ModDownload.DlFabricApiLoader.Output.Any([SpecialName] (ResourceVersion f) => IsFabricApiCompatible(f, VanillaName)))
		{
			return (SelectedFabric == null) ? "需要安装 Fabric" : null;
		}
		return "无";
	}

	private void CardFabricApi_PreviewSwap(object sender, ModBase.RouteEventArgs e)
	{
		if (LoadFabricApiGetError() != null)
		{
			e.Handled = true;
		}
	}

	private void FabricApi_Loaded()
	{
		try
		{
			if (ModDownload.DlFabricApiLoader.State != ModBase.LoadState.Finished || VanillaName == null || SelectedFabric == null)
			{
				return;
			}
			List<ResourceVersion> list = new List<ResourceVersion>();
			foreach (ResourceVersion item in ModDownload.DlFabricApiLoader.Output)
			{
				if (IsFabricApiCompatible(item, VanillaName))
				{
					if (!StringExtensions.StartsWithF(item.Display, "[", false))
					{
						Logger.Warn($"已特判修改 Fabric API 显示名：{item.Display}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
						item.Display = "[" + VanillaName + "] " + item.Display;
					}
					list.Add(item);
				}
			}
			if (!list.Any())
			{
				return;
			}
			list = list.OrderByDescending([SpecialName] (ResourceVersion v) => v.ReleaseDate).ToList();
			PanFabricApi.Children.Clear();
			foreach (ResourceVersion item2 in list)
			{
				PanFabricApi.Children.Add(ModDownloadLib.FabricApiDownloadListItem(item2, [SpecialName] (object a0, MouseButtonEventArgs a1) =>
				{
					FabricApi_Selected((MyListItem)a0, a1);
				}));
			}
			if (!AutoSelectedFabricApi)
			{
				AutoSelectedFabricApi = true;
				MyListItem myListItem = (MyListItem)MyVirtualizingElement.TryInit((FrameworkElement)PanFabricApi.Children[0]);
				Logger.Info($"已自动选择 Fabric API：{myListItem.Title}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
				FabricApi_Selected(myListItem, null);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "可视化 Fabric API 安装版本列表出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void FabricApi_Selected(MyListItem sender, EventArgs e)
	{
		SelectedFabricApi = (ResourceVersion)sender.Tag;
		CardFabricApi.IsSwapped = true;
		ReloadSelected();
	}

	private void FabricApi_Clear(object sender, EventArgs e)
	{
		SelectedFabricApi = null;
		CardFabricApi.IsSwapped = true;
		ReloadSelected();
	}

	public static bool IsOptiFabricCompatible(ResourceVersion ModFile, string MinecraftVersion)
	{
		bool result;
		try
		{
			result = MinecraftVersion != null && ModFile.GameVersions.Contains(MinecraftVersion);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, $"判断 OptiFabric 版本适配性出错（{MinecraftVersion}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
			result = false;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public string LoadOptiFabricGetError()
	{
		if (VanillaDrop >= 140 && VanillaDrop <= 150)
		{
			return "不兼容老版本 Fabric，请手动下载 OptiFabric Origins";
		}
		if (GetLoaderError(LoadOptiFabric) != null)
		{
			return GetLoaderError(LoadOptiFabric);
		}
		if (ModDownload.DlOptiFabricLoader.Output == null)
		{
			if (SelectedFabric == null && SelectedOptiFine == null)
			{
				return "需要安装 OptiFine 与 Fabric";
			}
			if (SelectedFabric == null)
			{
				return "需要安装 Fabric";
			}
			if (SelectedOptiFine == null)
			{
				return "需要安装 OptiFine";
			}
			return "获取中……";
		}
		foreach (ResourceVersion item in ModDownload.DlOptiFabricLoader.Output)
		{
			if (IsOptiFabricCompatible(item, VanillaName))
			{
				if (SelectedFabric == null && SelectedOptiFine == null)
				{
					return "需要安装 OptiFine 与 Fabric";
				}
				if (SelectedFabric == null)
				{
					return "需要安装 Fabric";
				}
				if (SelectedOptiFine == null)
				{
					return "需要安装 OptiFine";
				}
				return null;
			}
		}
		return "无";
	}

	private void CardOptiFabric_PreviewSwap(object sender, ModBase.RouteEventArgs e)
	{
		if (LoadOptiFabricGetError() != null)
		{
			e.Handled = true;
		}
	}

	private void OptiFabric_Loaded()
	{
		try
		{
			if (ModDownload.DlOptiFabricLoader.State != ModBase.LoadState.Finished || VanillaName == null || SelectedFabric == null || SelectedOptiFine == null)
			{
				return;
			}
			List<ResourceVersion> list = new List<ResourceVersion>();
			foreach (ResourceVersion item in ModDownload.DlOptiFabricLoader.Output)
			{
				if (IsOptiFabricCompatible(item, VanillaName))
				{
					list.Add(item);
				}
			}
			if (!list.Any())
			{
				return;
			}
			list = list.OrderByDescending([SpecialName] (ResourceVersion v) => v.ReleaseDate).ToList();
			PanOptiFabric.Children.Clear();
			foreach (ResourceVersion item2 in list)
			{
				PanOptiFabric.Children.Add(ModDownloadLib.OptiFabricDownloadListItem(item2, [SpecialName] (object a0, MouseButtonEventArgs a1) =>
				{
					OptiFabric_Selected((MyListItem)a0, a1);
				}));
			}
			if (!AutoSelectedOptiFabric && (VanillaDrop < 140 || VanillaDrop > 150))
			{
				AutoSelectedOptiFabric = true;
				MyListItem myListItem = (MyListItem)MyVirtualizingElement.TryInit((FrameworkElement)PanOptiFabric.Children[0]);
				Logger.Info($"已自动选择 OptiFabric：{myListItem.Title}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
				OptiFabric_Selected(myListItem, null);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "可视化 OptiFabric 安装版本列表出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadInstall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void OptiFabric_Selected(MyListItem sender, EventArgs e)
	{
		SelectedOptiFabric = (ResourceVersion)sender.Tag;
		CardOptiFabric.IsSwapped = true;
		ReloadSelected();
	}

	private void OptiFabric_Clear(object sender, EventArgs e)
	{
		SelectedOptiFabric = null;
		CardOptiFabric.IsSwapped = true;
		ReloadSelected();
	}

	public McComponents GetSelectedComponents()
	{
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		string vanillaName = VanillaName;
		JObject obj = ((SelectedOptiFine == null) ? null : JObject.FromObject((object)SelectedOptiFine));
		string obj2 = SelectedForge?.VersionName;
		string obj3 = SelectedNeoForge?.VersionName;
		string selectedFabric = SelectedFabric;
		string obj4 = SelectedFabricApi?.Version ?? SelectedFabricApi?.Id ?? SelectedFabricApi?.Display;
		string obj5 = SelectedOptiFabric?.Version ?? SelectedOptiFabric?.Id ?? SelectedOptiFabric?.Display;
		ModDownload.DlLiteLoaderListEntry selectedLiteLoader = SelectedLiteLoader;
		object obj6;
		if (selectedLiteLoader == null)
		{
			obj6 = null;
		}
		else
		{
			JToken jsonToken = selectedLiteLoader.JsonToken;
			obj6 = ((jsonToken != null) ? jsonToken[(object)"version"].ToString() : null);
		}
		return new McComponents(vanillaName, obj, obj2, obj3, selectedFabric, obj4, obj5, (string)obj6);
	}

	private void TextSelectName_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return && IsInSelectPage && BtnStart.IsEnabled)
		{
			BtnStart_Click();
		}
	}

	private void BtnStart_Click()
	{
		string text = TextSelectName.Text;
		ModDownloadLib.McInstallRequest request = new ModDownloadLib.McInstallRequest
		{
			NewInstanceName = text,
			InstanceFolder = $"{ModMinecraft.McFolderSelected}versions\\{text}\\",
			Components = GetSelectedComponents(),
			MinecraftJson = VanillaData["url"].ToString(),
			ForgeEntry = SelectedForge,
			NeoForgeEntry = SelectedNeoForge,
			FabricApi = SelectedFabricApi,
			OptiFabric = SelectedOptiFabric,
			LiteLoaderEntry = SelectedLiteLoader
		};
		StartInstall(request);
	}

	public virtual void StartInstall(ModDownloadLib.McInstallRequest Request)
	{
		if (((SelectedForge == null && SelectedNeoForge == null && SelectedFabric == null) || (Settings.Get<int>("LaunchArgumentIndieV2") != 0 && Settings.Get<int>("LaunchArgumentIndieV2") != 2) || ModMain.MyMsgBox("你尚未开启版本隔离，多个 MC 版本会共用同一个 Mod 文件夹。\r\n因此，游戏可能会因为读取到与当前版本不符的 Mod 而崩溃。\r\n推荐先在 设置 → 启动选项 → 默认版本隔离 中开启版本隔离！", "版本隔离提示", "取消下载", "继续") != 1) && ModDownloadLib.McInstallStart(Request))
		{
			ExitSelectPage();
		}
	}

	private string GetLoaderError(MyLoading Loader)
	{
		if (Loader == null)
		{
			return "获取中……";
		}
		if (!Loader.State.IsLoader)
		{
			return "获取中……";
		}
		switch (Loader.State.LoadingState)
		{
		case MyLoading.MyLoadingState.Run:
			return "获取中……";
		case MyLoading.MyLoadingState.Error:
		{
			string message = ((ModLoader.LoaderBase)Loader.State).Error.Message;
			return (Operators.CompareString(message, "无", TextCompare: false) == 0) ? "无" : ("获取失败：" + message);
		}
		case MyLoading.MyLoadingState.Unloaded:
			return "未知错误，状态为 Unloaded";
		default:
			return null;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadinstall.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
		}
	}

	void IComponentConnector.InitializeComponent()
	{
		//ILSpy generated this explicit interface implementation from .override directive in InitializeComponent
		this.InitializeComponent();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	internal Delegate _CreateDelegate(Type delegateType, string handler)
	{
		return Delegate.CreateDelegate(delegateType, this, handler);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			PanAllBack = (Grid)target;
			break;
		case 2:
			PanBack = (MyScrollViewer)target;
			break;
		case 3:
			PanInner = (Grid)target;
			break;
		case 4:
			PanMinecraft = (StackPanel)target;
			break;
		case 5:
			PanSelect = (StackPanel)target;
			break;
		case 6:
			CardSelectName = (MyCard)target;
			break;
		case 7:
			BtnBack = (MyIconButton)target;
			break;
		case 8:
			ImgLogo = (MyImage)target;
			break;
		case 9:
			TextSelectName = (MyTextBox)target;
			break;
		case 10:
			LabSelectName = (TextBlock)target;
			break;
		case 11:
			BtnOriginalNameRestore = (MyIconButton)target;
			break;
		case 12:
			HintSelectName = (MyHint)target;
			break;
		case 13:
			HintFabricAPI = (MyHint)target;
			break;
		case 14:
			HintOptiFabric = (MyHint)target;
			break;
		case 15:
			HintOptiFabricOld = (MyHint)target;
			break;
		case 16:
			HintModOptiFine = (MyHint)target;
			break;
		case 17:
			CardMinecraft = (MyCard)target;
			break;
		case 18:
			PanMinecraftInfo = (Grid)target;
			break;
		case 19:
			ImgMinecraft = (MyImage)target;
			break;
		case 20:
			LabMinecraftVersion = (TextBlock)target;
			break;
		case 21:
			PanMinecraftCompare = (Grid)target;
			break;
		case 22:
			ImgMinecraftOld = (MyImage)target;
			break;
		case 23:
			LabMinecraftOld = (TextBlock)target;
			break;
		case 24:
			ImgMinecraftNew = (MyImage)target;
			break;
		case 25:
			LabMinecraftNew = (TextBlock)target;
			break;
		case 26:
			BtnMinecraftRestore = (MyIconButton)target;
			break;
		case 27:
			CardForge = (MyCard)target;
			break;
		case 28:
			PanForge = (StackPanel)target;
			break;
		case 29:
			PanForgeInfo = (Grid)target;
			break;
		case 30:
			ImgForge = (MyImage)target;
			break;
		case 31:
			LabForge = (TextBlock)target;
			break;
		case 32:
			PanForgeCompare = (Grid)target;
			break;
		case 33:
			LabForgeOld = (TextBlock)target;
			break;
		case 34:
			BtnForgeClear = (MyIconButton)target;
			break;
		case 35:
			BtnForgeRestore = (MyIconButton)target;
			break;
		case 36:
			CardNeoForge = (MyCard)target;
			break;
		case 37:
			PanNeoForge = (StackPanel)target;
			break;
		case 38:
			PanNeoForgeInfo = (Grid)target;
			break;
		case 39:
			ImgNeoForge = (MyImage)target;
			break;
		case 40:
			LabNeoForge = (TextBlock)target;
			break;
		case 41:
			PanNeoForgeCompare = (Grid)target;
			break;
		case 42:
			LabNeoForgeOld = (TextBlock)target;
			break;
		case 43:
			BtnNeoForgeClear = (MyIconButton)target;
			break;
		case 44:
			BtnNeoForgeRestore = (MyIconButton)target;
			break;
		case 45:
			CardFabric = (MyCard)target;
			break;
		case 46:
			PanFabric = (StackPanel)target;
			break;
		case 47:
			PanFabricInfo = (Grid)target;
			break;
		case 48:
			ImgFabric = (MyImage)target;
			break;
		case 49:
			LabFabric = (TextBlock)target;
			break;
		case 50:
			PanFabricCompare = (Grid)target;
			break;
		case 51:
			LabFabricOld = (TextBlock)target;
			break;
		case 52:
			BtnFabricClear = (MyIconButton)target;
			break;
		case 53:
			BtnFabricRestore = (MyIconButton)target;
			break;
		case 54:
			CardFabricApi = (MyCard)target;
			break;
		case 55:
			PanFabricApi = (StackPanel)target;
			break;
		case 56:
			PanFabricApiInfo = (Grid)target;
			break;
		case 57:
			ImgFabricApi = (MyImage)target;
			break;
		case 58:
			LabFabricApi = (TextBlock)target;
			break;
		case 59:
			PanFabricApiCompare = (Grid)target;
			break;
		case 60:
			LabFabricApiOld = (TextBlock)target;
			break;
		case 61:
			BtnFabricApiClear = (MyIconButton)target;
			break;
		case 62:
			BtnFabricApiRestore = (MyIconButton)target;
			break;
		case 63:
			CardOptiFine = (MyCard)target;
			break;
		case 64:
			PanOptiFine = (StackPanel)target;
			break;
		case 65:
			PanOptiFineInfo = (Grid)target;
			break;
		case 66:
			ImgOptiFine = (MyImage)target;
			break;
		case 67:
			LabOptiFine = (TextBlock)target;
			break;
		case 68:
			PanOptiFineCompare = (Grid)target;
			break;
		case 69:
			LabOptiFineOld = (TextBlock)target;
			break;
		case 70:
			BtnOptiFineClear = (MyIconButton)target;
			break;
		case 71:
			BtnOptiFineRestore = (MyIconButton)target;
			break;
		case 72:
			CardOptiFabric = (MyCard)target;
			break;
		case 73:
			PanOptiFabric = (StackPanel)target;
			break;
		case 74:
			PanOptiFabricInfo = (Grid)target;
			break;
		case 75:
			ImgOptiFabric = (MyImage)target;
			break;
		case 76:
			LabOptiFabric = (TextBlock)target;
			break;
		case 77:
			PanOptiFabricCompare = (Grid)target;
			break;
		case 78:
			LabOptiFabricOld = (TextBlock)target;
			break;
		case 79:
			BtnOptiFabricClear = (MyIconButton)target;
			break;
		case 80:
			BtnOptiFabricRestore = (MyIconButton)target;
			break;
		case 81:
			CardLiteLoader = (MyCard)target;
			break;
		case 82:
			PanLiteLoader = (StackPanel)target;
			break;
		case 83:
			PanLiteLoaderInfo = (Grid)target;
			break;
		case 84:
			ImgLiteLoader = (MyImage)target;
			break;
		case 85:
			LabLiteLoader = (TextBlock)target;
			break;
		case 86:
			PanLiteLoaderCompare = (Grid)target;
			break;
		case 87:
			LabLiteLoaderOld = (TextBlock)target;
			break;
		case 88:
			BtnLiteLoaderClear = (MyIconButton)target;
			break;
		case 89:
			BtnLiteLoaderRestore = (MyIconButton)target;
			break;
		case 90:
			BtnStart = (MyExtraTextButton)target;
			break;
		case 91:
			PanLoad = (MyCard)target;
			break;
		case 92:
			LoadMinecraft = (MyLoading)target;
			break;
		case 93:
			LoadOptiFine = (MyLoading)target;
			break;
		case 94:
			LoadForge = (MyLoading)target;
			break;
		case 95:
			LoadNeoForge = (MyLoading)target;
			break;
		case 96:
			LoadLiteLoader = (MyLoading)target;
			break;
		case 97:
			LoadFabric = (MyLoading)target;
			break;
		case 98:
			LoadFabricApi = (MyLoading)target;
			break;
		case 99:
			LoadOptiFabric = (MyLoading)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}

	void IComponentConnector.Connect(int connectionId, object target)
	{
		//ILSpy generated this explicit interface implementation from .override directive in System_Windows_Markup_IComponentConnector_Connect
		this.System_Windows_Markup_IComponentConnector_Connect(connectionId, target);
	}
}
