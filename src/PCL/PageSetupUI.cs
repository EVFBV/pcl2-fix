using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using MeloongCore;
using MeloongCore.Extensions;
using MeloongCore.Wpf;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageSetupUI : MyPageRight, IComponentConnector
{
	private static bool _HiddenForceShow = false;

	[CompilerGenerated]
	[AccessedThroughProperty("SliderLauncherHue")]
	private MySlider _SliderLauncherHue;

	[CompilerGenerated]
	[AccessedThroughProperty("SliderLauncherDelta")]
	private MySlider _SliderLauncherDelta;

	[CompilerGenerated]
	[AccessedThroughProperty("SliderLauncherSat")]
	private MySlider _SliderLauncherSat;

	[CompilerGenerated]
	[AccessedThroughProperty("SliderLauncherLight")]
	private MySlider _SliderLauncherLight;

	[CompilerGenerated]
	[AccessedThroughProperty("LabLauncherTheme5Unlock")]
	private TextBlock _LabLauncherTheme5Unlock;

	[CompilerGenerated]
	[AccessedThroughProperty("RadioLauncherTheme8")]
	private MyRadioBox _RadioLauncherTheme8;

	[CompilerGenerated]
	[AccessedThroughProperty("LabLauncherTheme8Copy")]
	private TextBlock _LabLauncherTheme8Copy;

	[CompilerGenerated]
	[AccessedThroughProperty("RadioLauncherTheme9")]
	private MyRadioBox _RadioLauncherTheme9;

	[CompilerGenerated]
	[AccessedThroughProperty("LabLauncherTheme9Copy")]
	private TextBlock _LabLauncherTheme9Copy;

	[CompilerGenerated]
	[AccessedThroughProperty("RadioLauncherTheme11")]
	private MyRadioBox _RadioLauncherTheme11;

	[CompilerGenerated]
	[AccessedThroughProperty("LabLauncherTheme11Click")]
	private TextBlock _LabLauncherTheme11Click;

	[CompilerGenerated]
	[AccessedThroughProperty("RadioLauncherTheme14")]
	private MyRadioBox _RadioLauncherTheme14;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnBackgroundOpen")]
	private MyButton _BtnBackgroundOpen;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnBackgroundRefresh")]
	private MyButton _BtnBackgroundRefresh;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnBackgroundClear")]
	private MyButton _BtnBackgroundClear;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckMusicStart")]
	private MyCheckBox _CheckMusicStart;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckMusicStop")]
	private MyCheckBox _CheckMusicStop;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnMusicOpen")]
	private MyButton _BtnMusicOpen;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnMusicRefresh")]
	private MyButton _BtnMusicRefresh;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnMusicClear")]
	private MyButton _BtnMusicClear;

	[CompilerGenerated]
	[AccessedThroughProperty("RadioLogoType3")]
	private MyRadioBox _RadioLogoType3;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnLogoChange")]
	private MyButton _BtnLogoChange;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnLogoDelete")]
	private MyButton _BtnLogoDelete;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnCustomRefresh")]
	private MyButton _BtnCustomRefresh;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnCustomFile")]
	private MyButton _BtnCustomFile;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnCustomTutorial")]
	private MyButton _BtnCustomTutorial;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnCustomFolder")]
	private MyButton _BtnCustomFolder;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenPageSetup")]
	private MyCheckBox _CheckHiddenPageSetup;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenPageOther")]
	private MyCheckBox _CheckHiddenPageOther;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenSetupLaunch")]
	private MyCheckBox _CheckHiddenSetupLaunch;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenSetupLink")]
	private MyCheckBox _CheckHiddenSetupLink;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenSetupUI")]
	private MyCheckBox _CheckHiddenSetupUI;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenSetupSystem")]
	private MyCheckBox _CheckHiddenSetupSystem;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenOtherHelp")]
	private MyCheckBox _CheckHiddenOtherHelp;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenOtherAbout")]
	private MyCheckBox _CheckHiddenOtherAbout;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenOtherTest")]
	private MyCheckBox _CheckHiddenOtherTest;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenOtherFeedback")]
	private MyCheckBox _CheckHiddenOtherFeedback;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenOtherVote")]
	private MyCheckBox _CheckHiddenOtherVote;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckHiddenFunctionHidden")]
	private MyCheckBox _CheckHiddenFunctionHidden;

	private bool _contentLoaded;

	[SpecialName]
	private bool _0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded_0024Init;

	public static bool HiddenForceShow
	{
		get
		{
			return _HiddenForceShow;
		}
		set
		{
			_HiddenForceShow = value;
			HiddenRefresh();
		}
	}

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyScrollViewer PanBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanMain")]
	internal virtual StackPanel PanMain
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardLauncher")]
	internal virtual MyCard CardLauncher
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SliderLauncherTransparent")]
	internal virtual MySlider SliderLauncherTransparent
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabLauncherHue")]
	internal virtual TextBlock LabLauncherHue
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MySlider SliderLauncherHue
	{
		[CompilerGenerated]
		get
		{
			return _SliderLauncherHue;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MySlider.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				HSL_Change();
			};
			MySlider sliderLauncherHue = _SliderLauncherHue;
			if (sliderLauncherHue != null)
			{
				sliderLauncherHue.Change -= obj;
			}
			_SliderLauncherHue = value;
			sliderLauncherHue = _SliderLauncherHue;
			if (sliderLauncherHue != null)
			{
				sliderLauncherHue.Change += obj;
			}
		}
	}

	[field: AccessedThroughProperty("LabLauncherDelta")]
	internal virtual TextBlock LabLauncherDelta
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MySlider SliderLauncherDelta
	{
		[CompilerGenerated]
		get
		{
			return _SliderLauncherDelta;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MySlider.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				HSL_Change();
			};
			MySlider sliderLauncherDelta = _SliderLauncherDelta;
			if (sliderLauncherDelta != null)
			{
				sliderLauncherDelta.Change -= obj;
			}
			_SliderLauncherDelta = value;
			sliderLauncherDelta = _SliderLauncherDelta;
			if (sliderLauncherDelta != null)
			{
				sliderLauncherDelta.Change += obj;
			}
		}
	}

	[field: AccessedThroughProperty("LabLauncherSat")]
	internal virtual TextBlock LabLauncherSat
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MySlider SliderLauncherSat
	{
		[CompilerGenerated]
		get
		{
			return _SliderLauncherSat;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MySlider.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				HSL_Change();
			};
			MySlider sliderLauncherSat = _SliderLauncherSat;
			if (sliderLauncherSat != null)
			{
				sliderLauncherSat.Change -= obj;
			}
			_SliderLauncherSat = value;
			sliderLauncherSat = _SliderLauncherSat;
			if (sliderLauncherSat != null)
			{
				sliderLauncherSat.Change += obj;
			}
		}
	}

	[field: AccessedThroughProperty("LabLauncherLight")]
	internal virtual TextBlock LabLauncherLight
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MySlider SliderLauncherLight
	{
		[CompilerGenerated]
		get
		{
			return _SliderLauncherLight;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MySlider.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				HSL_Change();
			};
			MySlider sliderLauncherLight = _SliderLauncherLight;
			if (sliderLauncherLight != null)
			{
				sliderLauncherLight.Change -= obj;
			}
			_SliderLauncherLight = value;
			sliderLauncherLight = _SliderLauncherLight;
			if (sliderLauncherLight != null)
			{
				sliderLauncherLight.Change += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanLauncherTheme")]
	internal virtual Grid PanLauncherTheme
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLauncherTheme0")]
	internal virtual MyRadioBox RadioLauncherTheme0
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLauncherTheme1")]
	internal virtual MyRadioBox RadioLauncherTheme1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLauncherTheme2")]
	internal virtual MyRadioBox RadioLauncherTheme2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLauncherTheme3")]
	internal virtual MyRadioBox RadioLauncherTheme3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLauncherTheme4")]
	internal virtual MyRadioBox RadioLauncherTheme4
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLauncherTheme5")]
	internal virtual MyRadioBox RadioLauncherTheme5
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLauncherTheme5Gray")]
	internal virtual MyRadioBox RadioLauncherTheme5Gray
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual TextBlock LabLauncherTheme5Unlock
	{
		[CompilerGenerated]
		get
		{
			return _LabLauncherTheme5Unlock;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = LabLauncherTheme5Unlock_MouseLeftButtonUp;
			TextBlock labLauncherTheme5Unlock = _LabLauncherTheme5Unlock;
			if (labLauncherTheme5Unlock != null)
			{
				labLauncherTheme5Unlock.MouseLeftButtonUp -= value2;
			}
			_LabLauncherTheme5Unlock = value;
			labLauncherTheme5Unlock = _LabLauncherTheme5Unlock;
			if (labLauncherTheme5Unlock != null)
			{
				labLauncherTheme5Unlock.MouseLeftButtonUp += value2;
			}
		}
	}

	[field: AccessedThroughProperty("RadioLauncherTheme12")]
	internal virtual MyRadioBox RadioLauncherTheme12
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLauncherTheme6")]
	internal virtual MyRadioBox RadioLauncherTheme6
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLauncherTheme7")]
	internal virtual MyRadioBox RadioLauncherTheme7
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLauncherTheme13")]
	internal virtual MyRadioBox RadioLauncherTheme13
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyRadioBox RadioLauncherTheme8
	{
		[CompilerGenerated]
		get
		{
			return _RadioLauncherTheme8;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				LabLauncherTheme8Copy_MouseRightButtonUp();
			};
			MyRadioBox radioLauncherTheme = _RadioLauncherTheme8;
			if (radioLauncherTheme != null)
			{
				radioLauncherTheme.MouseRightButtonUp -= value2;
			}
			_RadioLauncherTheme8 = value;
			radioLauncherTheme = _RadioLauncherTheme8;
			if (radioLauncherTheme != null)
			{
				radioLauncherTheme.MouseRightButtonUp += value2;
			}
		}
	}

	internal virtual TextBlock LabLauncherTheme8Copy
	{
		[CompilerGenerated]
		get
		{
			return _LabLauncherTheme8Copy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				LabLauncherTheme8Copy_MouseRightButtonUp();
			};
			TextBlock labLauncherTheme8Copy = _LabLauncherTheme8Copy;
			if (labLauncherTheme8Copy != null)
			{
				labLauncherTheme8Copy.MouseRightButtonUp -= value2;
			}
			_LabLauncherTheme8Copy = value;
			labLauncherTheme8Copy = _LabLauncherTheme8Copy;
			if (labLauncherTheme8Copy != null)
			{
				labLauncherTheme8Copy.MouseRightButtonUp += value2;
			}
		}
	}

	internal virtual MyRadioBox RadioLauncherTheme9
	{
		[CompilerGenerated]
		get
		{
			return _RadioLauncherTheme9;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				LabLauncherTheme9Copy_MouseRightButtonUp();
			};
			MyRadioBox radioLauncherTheme = _RadioLauncherTheme9;
			if (radioLauncherTheme != null)
			{
				radioLauncherTheme.MouseRightButtonUp -= value2;
			}
			_RadioLauncherTheme9 = value;
			radioLauncherTheme = _RadioLauncherTheme9;
			if (radioLauncherTheme != null)
			{
				radioLauncherTheme.MouseRightButtonUp += value2;
			}
		}
	}

	internal virtual TextBlock LabLauncherTheme9Copy
	{
		[CompilerGenerated]
		get
		{
			return _LabLauncherTheme9Copy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				LabLauncherTheme9Copy_MouseRightButtonUp();
			};
			TextBlock labLauncherTheme9Copy = _LabLauncherTheme9Copy;
			if (labLauncherTheme9Copy != null)
			{
				labLauncherTheme9Copy.MouseRightButtonUp -= value2;
			}
			_LabLauncherTheme9Copy = value;
			labLauncherTheme9Copy = _LabLauncherTheme9Copy;
			if (labLauncherTheme9Copy != null)
			{
				labLauncherTheme9Copy.MouseRightButtonUp += value2;
			}
		}
	}

	[field: AccessedThroughProperty("RadioLauncherTheme10")]
	internal virtual MyRadioBox RadioLauncherTheme10
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyRadioBox RadioLauncherTheme11
	{
		[CompilerGenerated]
		get
		{
			return _RadioLauncherTheme11;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				LabLauncherTheme11Click_MouseLeftButtonUp();
			};
			MyRadioBox radioLauncherTheme = _RadioLauncherTheme11;
			if (radioLauncherTheme != null)
			{
				radioLauncherTheme.MouseRightButtonUp -= value2;
			}
			_RadioLauncherTheme11 = value;
			radioLauncherTheme = _RadioLauncherTheme11;
			if (radioLauncherTheme != null)
			{
				radioLauncherTheme.MouseRightButtonUp += value2;
			}
		}
	}

	internal virtual TextBlock LabLauncherTheme11Click
	{
		[CompilerGenerated]
		get
		{
			return _LabLauncherTheme11Click;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				LabLauncherTheme11Click_MouseLeftButtonUp();
			};
			TextBlock labLauncherTheme11Click = _LabLauncherTheme11Click;
			if (labLauncherTheme11Click != null)
			{
				labLauncherTheme11Click.MouseLeftButtonUp -= value2;
			}
			_LabLauncherTheme11Click = value;
			labLauncherTheme11Click = _LabLauncherTheme11Click;
			if (labLauncherTheme11Click != null)
			{
				labLauncherTheme11Click.MouseLeftButtonUp += value2;
			}
		}
	}

	internal virtual MyRadioBox RadioLauncherTheme14
	{
		[CompilerGenerated]
		get
		{
			return _RadioLauncherTheme14;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.ChangedEventHandler obj = RadioLauncherTheme14_Change;
			MyRadioBox radioLauncherTheme = _RadioLauncherTheme14;
			if (radioLauncherTheme != null)
			{
				radioLauncherTheme.Changed -= obj;
			}
			_RadioLauncherTheme14 = value;
			radioLauncherTheme = _RadioLauncherTheme14;
			if (radioLauncherTheme != null)
			{
				radioLauncherTheme.Changed += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanLauncherHide")]
	internal virtual Border PanLauncherHide
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardBackground")]
	internal virtual MyCard CardBackground
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanBackgroundSuit")]
	internal virtual Grid PanBackgroundSuit
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanBackgroundOpacity")]
	internal virtual Grid PanBackgroundOpacity
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SliderBackgroundOpacity")]
	internal virtual MySlider SliderBackgroundOpacity
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanBackgroundBlur")]
	internal virtual Grid PanBackgroundBlur
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SliderBackgroundBlur")]
	internal virtual MySlider SliderBackgroundBlur
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnBackgroundOpen
	{
		[CompilerGenerated]
		get
		{
			return _BtnBackgroundOpen;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnUIBgOpen_Click;
			MyButton btnBackgroundOpen = _BtnBackgroundOpen;
			if (btnBackgroundOpen != null)
			{
				btnBackgroundOpen.Click -= obj;
			}
			_BtnBackgroundOpen = value;
			btnBackgroundOpen = _BtnBackgroundOpen;
			if (btnBackgroundOpen != null)
			{
				btnBackgroundOpen.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnBackgroundRefresh
	{
		[CompilerGenerated]
		get
		{
			return _BtnBackgroundRefresh;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnBackgroundRefresh_Click;
			MyButton btnBackgroundRefresh = _BtnBackgroundRefresh;
			if (btnBackgroundRefresh != null)
			{
				btnBackgroundRefresh.Click -= obj;
			}
			_BtnBackgroundRefresh = value;
			btnBackgroundRefresh = _BtnBackgroundRefresh;
			if (btnBackgroundRefresh != null)
			{
				btnBackgroundRefresh.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnBackgroundClear
	{
		[CompilerGenerated]
		get
		{
			return _BtnBackgroundClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnBackgroundClear_Click;
			MyButton btnBackgroundClear = _BtnBackgroundClear;
			if (btnBackgroundClear != null)
			{
				btnBackgroundClear.Click -= obj;
			}
			_BtnBackgroundClear = value;
			btnBackgroundClear = _BtnBackgroundClear;
			if (btnBackgroundClear != null)
			{
				btnBackgroundClear.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("CardMusic")]
	internal virtual MyCard CardMusic
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanMusicVolume")]
	internal virtual Grid PanMusicVolume
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SliderMusicVolume")]
	internal virtual MySlider SliderMusicVolume
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanMusicDetail")]
	internal virtual StackPanel PanMusicDetail
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckMusicRandom")]
	internal virtual MyCheckBox CheckMusicRandom
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckMusicAuto")]
	internal virtual MyCheckBox CheckMusicAuto
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCheckBox CheckMusicStart
	{
		[CompilerGenerated]
		get
		{
			return _CheckMusicStart;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				CheckMusicStart_Change();
			};
			MyCheckBox checkMusicStart = _CheckMusicStart;
			if (checkMusicStart != null)
			{
				checkMusicStart.Change -= obj;
			}
			_CheckMusicStart = value;
			checkMusicStart = _CheckMusicStart;
			if (checkMusicStart != null)
			{
				checkMusicStart.Change += obj;
			}
		}
	}

	internal virtual MyCheckBox CheckMusicStop
	{
		[CompilerGenerated]
		get
		{
			return _CheckMusicStop;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				CheckMusicStop_Change();
			};
			MyCheckBox checkMusicStop = _CheckMusicStop;
			if (checkMusicStop != null)
			{
				checkMusicStop.Change -= obj;
			}
			_CheckMusicStop = value;
			checkMusicStop = _CheckMusicStop;
			if (checkMusicStop != null)
			{
				checkMusicStop.Change += obj;
			}
		}
	}

	internal virtual MyButton BtnMusicOpen
	{
		[CompilerGenerated]
		get
		{
			return _BtnMusicOpen;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnMusicOpen_Click;
			MyButton btnMusicOpen = _BtnMusicOpen;
			if (btnMusicOpen != null)
			{
				btnMusicOpen.Click -= obj;
			}
			_BtnMusicOpen = value;
			btnMusicOpen = _BtnMusicOpen;
			if (btnMusicOpen != null)
			{
				btnMusicOpen.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnMusicRefresh
	{
		[CompilerGenerated]
		get
		{
			return _BtnMusicRefresh;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnMusicRefresh_Click;
			MyButton btnMusicRefresh = _BtnMusicRefresh;
			if (btnMusicRefresh != null)
			{
				btnMusicRefresh.Click -= obj;
			}
			_BtnMusicRefresh = value;
			btnMusicRefresh = _BtnMusicRefresh;
			if (btnMusicRefresh != null)
			{
				btnMusicRefresh.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnMusicClear
	{
		[CompilerGenerated]
		get
		{
			return _BtnMusicClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnMusicClear_Click;
			MyButton btnMusicClear = _BtnMusicClear;
			if (btnMusicClear != null)
			{
				btnMusicClear.Click -= obj;
			}
			_BtnMusicClear = value;
			btnMusicClear = _BtnMusicClear;
			if (btnMusicClear != null)
			{
				btnMusicClear.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("CardLogo")]
	internal virtual MyCard CardLogo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLogoType0")]
	internal virtual MyRadioBox RadioLogoType0
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLogoType1")]
	internal virtual MyRadioBox RadioLogoType1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioLogoType2")]
	internal virtual MyRadioBox RadioLogoType2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyRadioBox RadioLogoType3
	{
		[CompilerGenerated]
		get
		{
			return _RadioLogoType3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyRadioBox.PreviewCheckEventHandler obj = RadioLogoType3_Check;
			MyRadioBox radioLogoType = _RadioLogoType3;
			if (radioLogoType != null)
			{
				radioLogoType.PreviewCheck -= obj;
			}
			_RadioLogoType3 = value;
			radioLogoType = _RadioLogoType3;
			if (radioLogoType != null)
			{
				radioLogoType.PreviewCheck += obj;
			}
		}
	}

	[field: AccessedThroughProperty("CheckLogoLeft")]
	internal virtual MyCheckBox CheckLogoLeft
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanLogoText")]
	internal virtual Grid PanLogoText
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextLogoText")]
	internal virtual MyTextBox TextLogoText
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanLogoChange")]
	internal virtual Grid PanLogoChange
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnLogoChange
	{
		[CompilerGenerated]
		get
		{
			return _BtnLogoChange;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnLogoChange_Click;
			MyButton btnLogoChange = _BtnLogoChange;
			if (btnLogoChange != null)
			{
				btnLogoChange.Click -= obj;
			}
			_BtnLogoChange = value;
			btnLogoChange = _BtnLogoChange;
			if (btnLogoChange != null)
			{
				btnLogoChange.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnLogoDelete
	{
		[CompilerGenerated]
		get
		{
			return _BtnLogoDelete;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnLogoDelete_Click;
			MyButton btnLogoDelete = _BtnLogoDelete;
			if (btnLogoDelete != null)
			{
				btnLogoDelete.Click -= obj;
			}
			_BtnLogoDelete = value;
			btnLogoDelete = _BtnLogoDelete;
			if (btnLogoDelete != null)
			{
				btnLogoDelete.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("CardCustom")]
	internal virtual MyCard CardCustom
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioCustomType0")]
	internal virtual MyRadioBox RadioCustomType0
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioCustomType3")]
	internal virtual MyRadioBox RadioCustomType3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioCustomType1")]
	internal virtual MyRadioBox RadioCustomType1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RadioCustomType2")]
	internal virtual MyRadioBox RadioCustomType2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("HintCustom")]
	internal virtual MyHint HintCustom
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("HintCustomWarn")]
	internal virtual MyHint HintCustomWarn
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanCustomLocal")]
	internal virtual WrapPanel PanCustomLocal
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnCustomRefresh
	{
		[CompilerGenerated]
		get
		{
			return _BtnCustomRefresh;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				BtnCustomRefresh_Click();
			};
			MyButton btnCustomRefresh = _BtnCustomRefresh;
			if (btnCustomRefresh != null)
			{
				btnCustomRefresh.Click -= obj;
			}
			_BtnCustomRefresh = value;
			btnCustomRefresh = _BtnCustomRefresh;
			if (btnCustomRefresh != null)
			{
				btnCustomRefresh.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnCustomFile
	{
		[CompilerGenerated]
		get
		{
			return _BtnCustomFile;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnCustomFile_Click;
			MyButton btnCustomFile = _BtnCustomFile;
			if (btnCustomFile != null)
			{
				btnCustomFile.Click -= obj;
			}
			_BtnCustomFile = value;
			btnCustomFile = _BtnCustomFile;
			if (btnCustomFile != null)
			{
				btnCustomFile.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnCustomTutorial
	{
		[CompilerGenerated]
		get
		{
			return _BtnCustomTutorial;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnCustomTutorial_Click;
			MyButton btnCustomTutorial = _BtnCustomTutorial;
			if (btnCustomTutorial != null)
			{
				btnCustomTutorial.Click -= obj;
			}
			_BtnCustomTutorial = value;
			btnCustomTutorial = _BtnCustomTutorial;
			if (btnCustomTutorial != null)
			{
				btnCustomTutorial.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnCustomFolder
	{
		[CompilerGenerated]
		get
		{
			return _BtnCustomFolder;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnCustomFolder_Click;
			MyButton btnCustomFolder = _BtnCustomFolder;
			if (btnCustomFolder != null)
			{
				btnCustomFolder.Click -= obj;
			}
			_BtnCustomFolder = value;
			btnCustomFolder = _BtnCustomFolder;
			if (btnCustomFolder != null)
			{
				btnCustomFolder.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanCustomNet")]
	internal virtual Grid PanCustomNet
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextCustomNet")]
	internal virtual MyTextBox TextCustomNet
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanCustomPreset")]
	internal virtual Grid PanCustomPreset
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardSwitch")]
	internal virtual MyCard CardSwitch
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckHiddenPageDownload")]
	internal virtual MyCheckBox CheckHiddenPageDownload
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckHiddenPageLink")]
	internal virtual MyCheckBox CheckHiddenPageLink
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCheckBox CheckHiddenPageSetup
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenPageSetup;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				HiddenSetupMain();
			};
			MyCheckBox.ChangeEventHandler obj2 = HiddenHint;
			MyCheckBox checkHiddenPageSetup = _CheckHiddenPageSetup;
			if (checkHiddenPageSetup != null)
			{
				checkHiddenPageSetup.Change -= obj;
				checkHiddenPageSetup.Change -= obj2;
			}
			_CheckHiddenPageSetup = value;
			checkHiddenPageSetup = _CheckHiddenPageSetup;
			if (checkHiddenPageSetup != null)
			{
				checkHiddenPageSetup.Change += obj;
				checkHiddenPageSetup.Change += obj2;
			}
		}
	}

	internal virtual MyCheckBox CheckHiddenPageOther
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenPageOther;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				HiddenOtherMain();
			};
			MyCheckBox checkHiddenPageOther = _CheckHiddenPageOther;
			if (checkHiddenPageOther != null)
			{
				checkHiddenPageOther.Change -= obj;
			}
			_CheckHiddenPageOther = value;
			checkHiddenPageOther = _CheckHiddenPageOther;
			if (checkHiddenPageOther != null)
			{
				checkHiddenPageOther.Change += obj;
			}
		}
	}

	internal virtual MyCheckBox CheckHiddenSetupLaunch
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenSetupLaunch;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				HiddenSetupSub();
			};
			MyCheckBox checkHiddenSetupLaunch = _CheckHiddenSetupLaunch;
			if (checkHiddenSetupLaunch != null)
			{
				checkHiddenSetupLaunch.Change -= obj;
			}
			_CheckHiddenSetupLaunch = value;
			checkHiddenSetupLaunch = _CheckHiddenSetupLaunch;
			if (checkHiddenSetupLaunch != null)
			{
				checkHiddenSetupLaunch.Change += obj;
			}
		}
	}

	internal virtual MyCheckBox CheckHiddenSetupLink
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenSetupLink;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				HiddenSetupSub();
			};
			MyCheckBox checkHiddenSetupLink = _CheckHiddenSetupLink;
			if (checkHiddenSetupLink != null)
			{
				checkHiddenSetupLink.Change -= obj;
			}
			_CheckHiddenSetupLink = value;
			checkHiddenSetupLink = _CheckHiddenSetupLink;
			if (checkHiddenSetupLink != null)
			{
				checkHiddenSetupLink.Change += obj;
			}
		}
	}

	internal virtual MyCheckBox CheckHiddenSetupUI
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenSetupUI;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				HiddenSetupSub();
			};
			MyCheckBox.ChangeEventHandler obj2 = HiddenHint;
			MyCheckBox checkHiddenSetupUI = _CheckHiddenSetupUI;
			if (checkHiddenSetupUI != null)
			{
				checkHiddenSetupUI.Change -= obj;
				checkHiddenSetupUI.Change -= obj2;
			}
			_CheckHiddenSetupUI = value;
			checkHiddenSetupUI = _CheckHiddenSetupUI;
			if (checkHiddenSetupUI != null)
			{
				checkHiddenSetupUI.Change += obj;
				checkHiddenSetupUI.Change += obj2;
			}
		}
	}

	internal virtual MyCheckBox CheckHiddenSetupSystem
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenSetupSystem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				HiddenSetupSub();
			};
			MyCheckBox checkHiddenSetupSystem = _CheckHiddenSetupSystem;
			if (checkHiddenSetupSystem != null)
			{
				checkHiddenSetupSystem.Change -= obj;
			}
			_CheckHiddenSetupSystem = value;
			checkHiddenSetupSystem = _CheckHiddenSetupSystem;
			if (checkHiddenSetupSystem != null)
			{
				checkHiddenSetupSystem.Change += obj;
			}
		}
	}

	internal virtual MyCheckBox CheckHiddenOtherHelp
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenOtherHelp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = HiddenOtherSub;
			MyCheckBox checkHiddenOtherHelp = _CheckHiddenOtherHelp;
			if (checkHiddenOtherHelp != null)
			{
				checkHiddenOtherHelp.Change -= obj;
			}
			_CheckHiddenOtherHelp = value;
			checkHiddenOtherHelp = _CheckHiddenOtherHelp;
			if (checkHiddenOtherHelp != null)
			{
				checkHiddenOtherHelp.Change += obj;
			}
		}
	}

	internal virtual MyCheckBox CheckHiddenOtherAbout
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenOtherAbout;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = HiddenOtherSub;
			MyCheckBox checkHiddenOtherAbout = _CheckHiddenOtherAbout;
			if (checkHiddenOtherAbout != null)
			{
				checkHiddenOtherAbout.Change -= obj;
			}
			_CheckHiddenOtherAbout = value;
			checkHiddenOtherAbout = _CheckHiddenOtherAbout;
			if (checkHiddenOtherAbout != null)
			{
				checkHiddenOtherAbout.Change += obj;
			}
		}
	}

	internal virtual MyCheckBox CheckHiddenOtherTest
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenOtherTest;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = HiddenOtherSub;
			MyCheckBox checkHiddenOtherTest = _CheckHiddenOtherTest;
			if (checkHiddenOtherTest != null)
			{
				checkHiddenOtherTest.Change -= obj;
			}
			_CheckHiddenOtherTest = value;
			checkHiddenOtherTest = _CheckHiddenOtherTest;
			if (checkHiddenOtherTest != null)
			{
				checkHiddenOtherTest.Change += obj;
			}
		}
	}

	internal virtual MyCheckBox CheckHiddenOtherFeedback
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenOtherFeedback;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = HiddenOtherNet;
			MyCheckBox checkHiddenOtherFeedback = _CheckHiddenOtherFeedback;
			if (checkHiddenOtherFeedback != null)
			{
				checkHiddenOtherFeedback.Change -= obj;
			}
			_CheckHiddenOtherFeedback = value;
			checkHiddenOtherFeedback = _CheckHiddenOtherFeedback;
			if (checkHiddenOtherFeedback != null)
			{
				checkHiddenOtherFeedback.Change += obj;
			}
		}
	}

	internal virtual MyCheckBox CheckHiddenOtherVote
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenOtherVote;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = HiddenOtherNet;
			MyCheckBox checkHiddenOtherVote = _CheckHiddenOtherVote;
			if (checkHiddenOtherVote != null)
			{
				checkHiddenOtherVote.Change -= obj;
			}
			_CheckHiddenOtherVote = value;
			checkHiddenOtherVote = _CheckHiddenOtherVote;
			if (checkHiddenOtherVote != null)
			{
				checkHiddenOtherVote.Change += obj;
			}
		}
	}

	[field: AccessedThroughProperty("CheckLauncherEmail")]
	internal virtual MyCheckBox CheckLauncherEmail
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckHiddenFunctionSelect")]
	internal virtual MyCheckBox CheckHiddenFunctionSelect
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckHiddenFunctionModUpdate")]
	internal virtual MyCheckBox CheckHiddenFunctionModUpdate
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCheckBox CheckHiddenFunctionHidden
	{
		[CompilerGenerated]
		get
		{
			return _CheckHiddenFunctionHidden;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = HiddenHint;
			MyCheckBox checkHiddenFunctionHidden = _CheckHiddenFunctionHidden;
			if (checkHiddenFunctionHidden != null)
			{
				checkHiddenFunctionHidden.Change -= obj;
			}
			_CheckHiddenFunctionHidden = value;
			checkHiddenFunctionHidden = _CheckHiddenFunctionHidden;
			if (checkHiddenFunctionHidden != null)
			{
				checkHiddenFunctionHidden.Change += obj;
			}
		}
	}

	[field: AccessedThroughProperty("CheckHiddenFunctionModify")]
	internal virtual MyCheckBox CheckHiddenFunctionModify
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public PageSetupUI()
	{
		base.Loaded += PageSetupUI_Loaded;
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			HiddenRefresh();
		};
		InitializeComponent();
	}

	private void PageSetupUI_Loaded(object sender, RoutedEventArgs e)
	{
		PanBack.ScrollToHome();
		ModSecret.ThemeCheckAll(EffectSetup: true);
		if (ModSecret.ThemeDontClick != 0)
		{
			string text = null;
			switch (ModSecret.ThemeDontClick)
			{
			case 1:
				text = "眼瞎白";
				break;
			case 2:
				text = "真·滑稽彩";
				break;
			}
			foreach (object child in PanLauncherTheme.Children)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(child);
				if (objectValue is MyRadioBox && ((MyRadioBox)objectValue).IsEnabled)
				{
					((MyRadioBox)objectValue).Text = text;
				}
			}
		}
		checked
		{
			ModAnimation.AniControlEnabled++;
			Refresh();
			ModAnimation.AniControlEnabled--;
			if (_0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded_0024Init == null)
			{
				Interlocked.CompareExchange(ref _0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded_0024Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded_0024Init, ref lockTaken);
				if (_0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded_0024Init.State == 0)
				{
					_0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded_0024Init.State = 2;
					_0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded = false;
				}
				else if (_0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded_0024Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded_0024Init);
				}
			}
			if (!_0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded)
			{
				_0024STATIC_0024PageSetupUI_Loaded_002420211C128141_0024Reloaded = true;
				SliderLoad();
				if (!RadioLauncherTheme8.IsEnabled)
				{
					LabLauncherTheme8Copy.ToolTip = $"累积赞助达到 ¥23.33 后，在爱发电私信发送【土豆 {ModBase.Identify}】以解锁。" + "\r\n右键打开赞助页面，如果觉得 PCL 做得还不错就支持一下吧 =w=！";
				}
				RadioLauncherTheme8.ToolTip = $"累积赞助达到 ¥23.33 后，在爱发电私信发送【土豆 {ModBase.Identify}】以解锁";
				if (!RadioLauncherTheme9.IsEnabled)
				{
					LabLauncherTheme9Copy.ToolTip = "· 反馈一个 Bug，在标记为 [完成] 后回复识别码要求解锁（右键打开反馈页面）\r\n· 提交一个 Pull Request 或主页预设，在标记为 [完成] 后回复识别码要求解锁";
				}
				RadioLauncherTheme9.ToolTip = "· 反馈一个 Bug，在标记为 [完成] 后回复识别码要求解锁\r\n· 提交一个 Pull Request 或主页预设，在标记为 [完成] 后回复识别码要求解锁";
			}
		}
	}

	public void Refresh()
	{
		try
		{
			SettingService.RefreshSettings(this);
			BackgroundRefresh(IsHint: false, Refresh: false);
			CheckLogoLeft.Visibility = ((!RadioLogoType0.Checked) ? Visibility.Collapsed : Visibility.Visible);
			PanLogoText.Visibility = ((!RadioLogoType2.Checked) ? Visibility.Collapsed : Visibility.Visible);
			PanLogoChange.Visibility = ((!RadioLogoType3.Checked) ? Visibility.Collapsed : Visibility.Visible);
			MusicRefreshUI();
			OnMainPageTypeChanged();
		}
		catch (NullReferenceException ex)
		{
			ProjectData.SetProjectError(ex);
			NullReferenceException ex2 = ex;
			Logger.Error((Exception)ex2, "个性化设置项存在异常，已被自动重置", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
			Reset();
			ProjectData.ClearProjectError();
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Error(ex4, "重载个性化设置时出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	public void Reset()
	{
		try
		{
			SettingService.ResetSettings(this);
			Logger.Info("已初始化个性化设置！", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
			ModMain.Hint("已初始化个性化设置", ModMain.HintType.Green, Log: false);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "初始化个性化设置失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
			ProjectData.ClearProjectError();
		}
		Refresh();
	}

	private void BtnUIBgOpen_Click(object sender, EventArgs e)
	{
		ModBase.OpenExplorer(Paths.Base + "PCL\\Pictures\\");
	}

	private void BtnBackgroundRefresh_Click(object sender, EventArgs e)
	{
		BackgroundRefresh(IsHint: true, Refresh: true);
	}

	public void BackgroundRefreshUI(bool Show, int Count)
	{
		if (!Information.IsNothing(PanBackgroundOpacity))
		{
			if (Show)
			{
				PanBackgroundOpacity.Visibility = Visibility.Visible;
				PanBackgroundBlur.Visibility = Visibility.Visible;
				PanBackgroundSuit.Visibility = Visibility.Visible;
				BtnBackgroundClear.Visibility = Visibility.Visible;
				CardBackground.Title = "背景图片（" + Conversions.ToString(Count) + " 张）";
			}
			else
			{
				PanBackgroundOpacity.Visibility = Visibility.Collapsed;
				PanBackgroundBlur.Visibility = Visibility.Collapsed;
				PanBackgroundSuit.Visibility = Visibility.Collapsed;
				BtnBackgroundClear.Visibility = Visibility.Collapsed;
				CardBackground.Title = "背景图片";
			}
			CardBackground.TriggerForceResize();
		}
	}

	private void BtnBackgroundClear_Click(object sender, EventArgs e)
	{
		if (ModMain.MyMsgBox("即将删除背景图片文件夹中的所有文件。\r\n此操作不可撤销，是否确定？", "警告", "确定", "取消", "", IsWarn: true) == 1)
		{
			DirectoryUtils.Delete(Paths.Base + "PCL\\Pictures", false);
			BackgroundRefresh(IsHint: false, Refresh: true);
			ModMain.Hint("背景图片已清空！", ModMain.HintType.Green);
		}
	}

	public static void BackgroundRefresh(bool IsHint, bool Refresh)
	{
		try
		{
			DirectoryUtils.Create(Paths.Base + "PCL\\Pictures\\");
			List<string> list = new List<string>();
			foreach (string item in DirectoryUtils.EnumerateFiles(Paths.Base + "PCL\\Pictures\\", true, "*"))
			{
				string extension = PathUtils.GetExtension(item);
				if (Operators.CompareString(extension, "ini", TextCompare: false) != 0 && Operators.CompareString(extension, "db", TextCompare: false) != 0)
				{
					list.Add(item);
				}
			}
			if (!list.Any())
			{
				if (Refresh)
				{
					if (ModMain.FrmMain.ImgBack.Visibility == Visibility.Collapsed)
					{
						if (IsHint)
						{
							ModMain.Hint("未检测到可用背景图片！", ModMain.HintType.Red);
						}
					}
					else
					{
						ModMain.FrmMain.ImgBack.Visibility = Visibility.Collapsed;
						if (IsHint)
						{
							ModMain.Hint("背景图片已清除！", ModMain.HintType.Green);
						}
					}
				}
				if (!Information.IsNothing(ModMain.FrmSetupUI))
				{
					ModMain.FrmSetupUI.BackgroundRefreshUI(Show: false, 0);
				}
				return;
			}
			if (Refresh)
			{
				string text = ModBase.RandomOne(list);
				try
				{
					Logger.Info($"加载背景图片：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
					ModMain.FrmMain.ImgBack.Background = (ImageBrush)new MyBitmap(text);
					ModMain.FrmMain.ImgBack.Visibility = Visibility.Visible;
					ModMain.FrmMain.UpdateBackgroundAndTitleBar();
					if (IsHint)
					{
						ModMain.Hint("背景图片已刷新：" + PathUtils.GetLastPart(text), ModMain.HintType.Green, Log: false);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (ex2.Message.Contains("参数无效"))
					{
						Logger.Error(string.Format("刷新背景图片失败，该图片文件可能并非标准格式。{0}你可以尝试使用画图打开该文件并重新保存，这会让图片变为标准格式。{1}文件：{2}", "\r\n", "\r\n", text), (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
					}
					else
					{
						Logger.Error(ex2, $"刷新背景图片失败（{text}）", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
					}
					ProjectData.ClearProjectError();
				}
			}
			if (!Information.IsNothing(ModMain.FrmSetupUI))
			{
				ModMain.FrmSetupUI.BackgroundRefreshUI(Show: true, list.Count);
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Error(ex4, "刷新背景图片时出现未知错误", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void BtnLogoChange_Click(object sender, EventArgs e)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		string text = Dialogs.SelectFile("选择图片", false, (string)null, (OneOf<IEnumerable<ValueTuple<string[], string>>, string>?)OneOf<IEnumerable<(string[], string)>, string>.op_Implicit((IEnumerable<(string[], string)>)new(string[], string)[1] { (new string[5] { "png", "jpg", "jpeg", "gif", "webp" }, "常用图片文件") })).FirstOrDefault();
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		string text2 = Paths.Base + "PCL\\Logo.png";
		try
		{
			FileUtils.Copy(text, text2);
			ModMain.FrmMain.ImageTitleLogo.Source = null;
			ModMain.FrmMain.ImageTitleLogo.Source = text2;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			if (ex2.Message.Contains("参数无效"))
			{
				Logger.Error(string.Format("改变标题栏图片失败，该图片文件可能并非标准格式。{0}你可以尝试使用画图打开该文件并重新保存，这会让图片变为标准格式。{1}文件：{2}", "\r\n", "\r\n", text2), (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
			}
			else
			{
				Logger.Error(ex2, "设置标题栏图片失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
			}
			ModMain.FrmMain.ImageTitleLogo.Source = null;
			ProjectData.ClearProjectError();
		}
	}

	private void RadioLogoType3_Check(object sender, ModBase.RouteEventArgs e)
	{
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		if (ModAnimation.AniControlEnabled != 0 || !e.RaiseByMouse)
		{
			return;
		}
		while (true)
		{
			string text = Paths.Base + "PCL\\Logo.png";
			if (FileUtils.Exists(text))
			{
				try
				{
					ModMain.FrmMain.ImageTitleLogo.Source = null;
					ModMain.FrmMain.ImageTitleLogo.Source = text;
					return;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (ex2.Message.Contains("参数无效"))
					{
						Logger.Error(string.Format("改变标题栏图片失败，该图片文件可能并非标准格式。{0}你可以尝试使用画图打开该文件并重新保存，这会让图片变为标准格式。{1}文件：{2}", "\r\n", "\r\n", text), (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
					}
					else
					{
						Logger.Error(ex2, "调整标题栏图片失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
					}
					ModMain.FrmMain.ImageTitleLogo.Source = null;
					e.Handled = true;
					try
					{
						FileUtils.Delete(text, false);
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						Logger.Error(ex4, "清理错误的标题栏图片失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
						ProjectData.ClearProjectError();
					}
					ProjectData.ClearProjectError();
					return;
				}
			}
			string text2 = Dialogs.SelectFile("选择图片", false, (string)null, (OneOf<IEnumerable<ValueTuple<string[], string>>, string>?)OneOf<IEnumerable<(string[], string)>, string>.op_Implicit((IEnumerable<(string[], string)>)new(string[], string)[1] { (new string[5] { "png", "jpeg", "jpg", "gif", "webp" }, "常用图片文件") })).FirstOrDefault();
			if (string.IsNullOrEmpty(text2))
			{
				break;
			}
			try
			{
				FileUtils.Copy(text2, text);
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Logger.Error(ex6, "复制标题栏图片失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				ProjectData.ClearProjectError();
				return;
			}
		}
		ModMain.FrmMain.ImageTitleLogo.Source = null;
		e.Handled = true;
	}

	private void BtnLogoDelete_Click(object sender, EventArgs e)
	{
		try
		{
			FileUtils.Delete(Paths.Base + "PCL\\Logo.png", false);
			RadioLogoType1.SetChecked(value: true, user: true);
			ModMain.Hint("标题栏图片已清空！", ModMain.HintType.Green);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "清空标题栏图片失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void BtnMusicOpen_Click(object sender, EventArgs e)
	{
		ModBase.OpenExplorer(Paths.Base + "PCL\\Musics\\");
	}

	private void BtnMusicRefresh_Click(object sender, EventArgs e)
	{
		ModMusic.MusicRefreshPlay(ShowHint: true);
	}

	public void MusicRefreshUI()
	{
		if (PanBackgroundOpacity == null)
		{
			return;
		}
		if (ModMusic.MusicAllList.Any())
		{
			PanMusicVolume.Visibility = Visibility.Visible;
			PanMusicDetail.Visibility = Visibility.Visible;
			BtnMusicClear.Visibility = Visibility.Visible;
			CardMusic.Title = "背景音乐（" + Conversions.ToString(DirectoryUtils.EnumerateFiles(Paths.Base + "PCL\\Musics\\", true, "*").Count([SpecialName] (string f) => !new string[7] { "ini", "jpg", "txt", "cfg", "lrc", "db", "png" }.Contains(PathUtils.GetExtension(f)))) + " 首）";
		}
		else
		{
			PanMusicVolume.Visibility = Visibility.Collapsed;
			PanMusicDetail.Visibility = Visibility.Collapsed;
			BtnMusicClear.Visibility = Visibility.Collapsed;
			CardMusic.Title = "背景音乐";
		}
		CardMusic.TriggerForceResize();
	}

	private void BtnMusicClear_Click(object sender, EventArgs e)
	{
		if (ModMain.MyMsgBox("即将删除背景音乐文件夹中的所有文件。\r\n此操作不可撤销，是否确定？", "警告", "确定", "取消", "", IsWarn: true) != 1)
		{
			return;
		}
		ModBase.RunInThread([SpecialName] () =>
		{
			ModMain.Hint("正在删除背景音乐……");
			ModMusic.MusicNAudio = null;
			ModMusic.MusicWaitingList = new List<string>();
			ModMusic.MusicAllList = new List<string>();
			Thread.Sleep(200);
			try
			{
				DirectoryUtils.Delete(Paths.Base + "PCL\\Musics", false);
				ModMain.Hint("背景音乐已删除！", ModMain.HintType.Green);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "删除背景音乐失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				ProjectData.ClearProjectError();
			}
			try
			{
				DirectoryUtils.Create(Paths.Base + "PCL\\Musics\\");
				ModBase.RunInUi([SpecialName] () =>
				{
					ModMusic.MusicRefreshPlay(ShowHint: false);
				});
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, "重建背景音乐文件夹失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				ProjectData.ClearProjectError();
			}
		});
	}

	private void CheckMusicStart_Change()
	{
		if (ModAnimation.AniControlEnabled == 0 && CheckMusicStart.Checked)
		{
			CheckMusicStop.Checked = false;
		}
	}

	private void CheckMusicStop_Change()
	{
		if (ModAnimation.AniControlEnabled == 0 && CheckMusicStop.Checked)
		{
			CheckMusicStart.Checked = false;
		}
	}

	private void BtnCustomFile_Click(object sender, EventArgs e)
	{
		try
		{
			if (!FileUtils.Exists(Paths.Base + "PCL\\Custom.xaml") || ModMain.MyMsgBox("当前已存在布局文件，继续生成教学文件将会覆盖现有布局文件！", "覆盖确认", "继续", "取消", "", IsWarn: true) != 2)
			{
				ModBase.ExtractResources(Paths.Base + "PCL\\Custom.xaml", "Custom");
				ModMain.Hint("教学文件已生成！", ModMain.HintType.Green);
				ModBase.OpenExplorer(Paths.Base + "PCL\\Custom.xaml");
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "生成教学文件失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void BtnCustomRefresh_Click()
	{
		ModMain.FrmLaunchRight.ForceRefresh();
		ModMain.Hint("已刷新主页！", ModMain.HintType.Green);
	}

	private void BtnCustomTutorial_Click(object sender, EventArgs e)
	{
		ModMain.MyMsgBox("1. 点击 生成教学文件 按钮，这会在 PCL 文件夹下生成 Custom.xaml 布局文件。\r\n2. 使用记事本等工具打开这个文件并进行修改，修改完记得保存。\r\n3. 点击 刷新主页 按钮，查看主页现在长啥样了。\r\n\r\n你可以在生成教学文件后直接刷新主页，对照着进行修改，更有助于理解。\r\n直接将主页文件拖进 PCL 窗口也可以快捷加载。", "主页自定义教程");
	}

	public static void OnMainPageTypeChanged()
	{
		if (ModMain.FrmSetupUI != null)
		{
			switch (Settings.Get<int>("UiCustomType"))
			{
			case 0:
				ModMain.FrmSetupUI.PanCustomPreset.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanCustomLocal.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanCustomNet.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.HintCustom.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.HintCustomWarn.Visibility = Visibility.Collapsed;
				break;
			case 1:
				ModMain.FrmSetupUI.PanCustomPreset.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanCustomLocal.Visibility = Visibility.Visible;
				ModMain.FrmSetupUI.PanCustomNet.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.HintCustom.Visibility = Visibility.Visible;
				ModMain.FrmSetupUI.HintCustomWarn.Visibility = (Settings.Get<bool>("HintCustomWarn") ? Visibility.Collapsed : Visibility.Visible);
				ModMain.FrmSetupUI.HintCustom.Text = string.Format("从 PCL 文件夹下的 Custom.xaml 读取主页内容。{0}你可以手动编辑该文件，向主页添加文本、图片、常用网站、快捷启动等功能。", "\r\n");
				CustomEventService.SetEventType(ModMain.FrmSetupUI.HintCustom, CustomEvent.EventType.None);
				break;
			case 2:
				ModMain.FrmSetupUI.PanCustomPreset.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanCustomLocal.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanCustomNet.Visibility = Visibility.Visible;
				ModMain.FrmSetupUI.HintCustom.Visibility = Visibility.Visible;
				ModMain.FrmSetupUI.HintCustomWarn.Visibility = (Settings.Get<bool>("HintCustomWarn") ? Visibility.Collapsed : Visibility.Visible);
				ModMain.FrmSetupUI.HintCustom.Text = string.Format("从指定网址联网获取主页内容。服主也可以用于动态更新服务器公告。{0}如果你制作了稳定运行的联网主页，可以点击这条提示投稿，若合格即可加入预设！", "\r\n");
				CustomEventService.SetEventType(ModMain.FrmSetupUI.HintCustom, CustomEvent.EventType.打开网页);
				CustomEventService.SetEventData(ModMain.FrmSetupUI.HintCustom, "https://github.com/Meloong-Git/PCL/discussions/2528");
				break;
			case 3:
				ModMain.FrmSetupUI.PanCustomPreset.Visibility = Visibility.Visible;
				ModMain.FrmSetupUI.PanCustomLocal.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanCustomNet.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.HintCustom.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.HintCustomWarn.Visibility = Visibility.Collapsed;
				break;
			}
			ModMain.FrmSetupUI.CardCustom.TriggerForceResize();
		}
	}

	private void BtnCustomFolder_Click(object sender, MouseButtonEventArgs e)
	{
		string text = Paths.Base + "PCL\\Custom.xaml";
		if (FileUtils.Exists(text))
		{
			ModBase.OpenExplorer(text);
		}
		else
		{
			ModBase.OpenExplorer(Paths.Base + "PCL\\");
		}
	}

	private void LabLauncherTheme5Unlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		RadioLauncherTheme5Gray.Opacity -= 0.7;
		RadioLauncherTheme5.Opacity += 0.7;
		ModAnimation.AniStart(checked(new ModAnimation.AniData[2]
		{
			ModAnimation.AaOpacity(RadioLauncherTheme5Gray, 1.0, (int)Math.Round(1000.0 * ModAnimation.AniSpeed), (int)Math.Round(500.0 * ModAnimation.AniSpeed), new ModAnimation.AniEaseInFluent()),
			ModAnimation.AaOpacity(RadioLauncherTheme5, -1.0, (int)Math.Round(1000.0 * ModAnimation.AniSpeed), (int)Math.Round(500.0 * ModAnimation.AniSpeed), new ModAnimation.AniEaseInFluent())
		}), "ThemeUnlock");
		if (RadioLauncherTheme5Gray.Opacity < 0.02)
		{
			ModSecret.ThemeUnlock(5, ShowDoubleHint: true, "隐藏主题 玄素黑 已解锁！");
			ModAnimation.AniStop("ThemeUnlock");
			RadioLauncherTheme5.Checked = true;
		}
	}

	private void LabLauncherTheme11Click_MouseLeftButtonUp()
	{
		if ((LabLauncherTheme11Click.Visibility == Visibility.Collapsed || (LabLauncherTheme11Click.ToolTip ?? "").ToString().Contains("点击")) && ModMain.MyMsgBox("1. 不爬取或攻击相关服务或网站，不盗取相关账号，没有谜题可以或需要以此来解决。\r\n2. 不得篡改或损毁相关公开信息，请尽量让它们保持原状。\r\n3. 在你感到迷茫的时候，看看回声洞可能会给你带来惊喜。\r\n\r\n若违规，可能会被从任意相关群中踢出！", "解密游戏的基本规则", "我知道了", "恕我拒绝") == 1)
		{
			ModMain.MyMsgBox("你需要用自己的智慧来找到下一步的线索……\r\n初始线索：gnp.dorC61\\60\\20\\0202\\moc.x1xa.2s\\\\:sp" + StringExtensions.Lower("T") + "th", "解密游戏");
		}
	}

	private void LabLauncherTheme8Copy_MouseRightButtonUp()
	{
		ModBase.OpenWebsite("https://meloong.com/afd/a/LTCat");
	}

	private void LabLauncherTheme9Copy_MouseRightButtonUp()
	{
		PageOtherLeft.TryFeedback();
	}

	private void RadioLauncherTheme14_Change(object sender, ModBase.RouteEventArgs e)
	{
		if (RadioLauncherTheme14.Checked)
		{
			if (LabLauncherHue.Visibility == Visibility.Visible)
			{
				return;
			}
			LabLauncherHue.Visibility = Visibility.Visible;
			SliderLauncherHue.Visibility = Visibility.Visible;
			LabLauncherSat.Visibility = Visibility.Visible;
			SliderLauncherSat.Visibility = Visibility.Visible;
			LabLauncherDelta.Visibility = Visibility.Visible;
			SliderLauncherDelta.Visibility = Visibility.Visible;
			LabLauncherLight.Visibility = Visibility.Visible;
			SliderLauncherLight.Visibility = Visibility.Visible;
		}
		else
		{
			if (LabLauncherHue.Visibility == Visibility.Collapsed)
			{
				return;
			}
			LabLauncherHue.Visibility = Visibility.Collapsed;
			SliderLauncherHue.Visibility = Visibility.Collapsed;
			LabLauncherSat.Visibility = Visibility.Collapsed;
			SliderLauncherSat.Visibility = Visibility.Collapsed;
			LabLauncherDelta.Visibility = Visibility.Collapsed;
			SliderLauncherDelta.Visibility = Visibility.Collapsed;
			LabLauncherLight.Visibility = Visibility.Collapsed;
			SliderLauncherLight.Visibility = Visibility.Collapsed;
		}
		CardLauncher.TriggerForceResize();
	}

	private void HSL_Change()
	{
		if (ModAnimation.AniControlEnabled == 0 && SliderLauncherSat != null && SliderLauncherSat.IsLoaded)
		{
			ModSecret.ThemeRefresh();
		}
	}

	public static void HiddenRefresh()
	{
		if (ModMain.FrmMain.PanTitleSelect == null || !ModMain.FrmMain.PanTitleSelect.IsLoaded)
		{
			return;
		}
		checked
		{
			try
			{
				if (!HiddenForceShow && Settings.Get<bool>("UiHiddenPageDownload") && Settings.Get<bool>("UiHiddenPageLink") && Settings.Get<bool>("UiHiddenPageSetup") && Settings.Get<bool>("UiHiddenPageOther"))
				{
					ModMain.FrmMain.PanTitleSelect.Visibility = Visibility.Collapsed;
				}
				else
				{
					ModMain.FrmMain.PanTitleSelect.Visibility = Visibility.Visible;
					ModMain.FrmMain.BtnTitleSelect1.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenPageDownload")) ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmMain.BtnTitleSelect2.Visibility = Visibility.Collapsed;
					ModMain.FrmMain.BtnTitleSelect3.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenPageSetup")) ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmMain.BtnTitleSelect4.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenPageOther")) ? Visibility.Collapsed : Visibility.Visible);
				}
				ModMain.FrmLaunchLeft.RefreshButtonsUI();
				if (ModMain.FrmSetupUI != null)
				{
					ModMain.FrmSetupUI.CardSwitch.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenFunctionHidden")) ? Visibility.Collapsed : Visibility.Visible);
				}
				if (ModMain.FrmInstanceLeft != null)
				{
					ModMain.FrmInstanceLeft.ItemModify.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenFunctionInstanceModify")) ? Visibility.Collapsed : Visibility.Visible);
				}
				if (ModMain.FrmSetupLeft != null)
				{
					ModMain.FrmSetupLeft.ItemLaunch.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenSetupLaunch")) ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmSetupLeft.ItemUI.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenSetupUi")) ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmSetupLeft.ItemLink.Visibility = Visibility.Collapsed;
					ModMain.FrmSetupLeft.ItemSystem.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenSetupSystem")) ? Visibility.Collapsed : Visibility.Visible);
					int num = 0;
					if (!Settings.Get<bool>("UiHiddenSetupLaunch"))
					{
						num++;
					}
					if (!Settings.Get<bool>("UiHiddenSetupUi"))
					{
						num++;
					}
					if (!Settings.Get<bool>("UiHiddenSetupSystem"))
					{
						num++;
					}
					ModMain.FrmSetupLeft.PanItem.Visibility = ((num < 2 && !HiddenForceShow) ? Visibility.Collapsed : Visibility.Visible);
				}
				int num2 = 0;
				if (!Settings.Get<bool>("UiHiddenOtherHelp"))
				{
					num2++;
				}
				if (!Settings.Get<bool>("UiHiddenOtherAbout"))
				{
					num2++;
				}
				if (!Settings.Get<bool>("UiHiddenOtherTest"))
				{
					num2++;
				}
				if (!Settings.Get<bool>("UiHiddenOtherFeedback"))
				{
					num2++;
				}
				if (!Settings.Get<bool>("UiHiddenOtherVote"))
				{
					num2++;
				}
				if (ModMain.FrmOtherLeft != null)
				{
					ModMain.FrmOtherLeft.ItemHelp.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenOtherHelp")) ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmOtherLeft.ItemFeedback.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenOtherFeedback")) ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmOtherLeft.ItemVote.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenOtherVote")) ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmOtherLeft.ItemAbout.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenOtherAbout")) ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmOtherLeft.ItemTest.Visibility = ((!HiddenForceShow && Settings.Get<bool>("UiHiddenOtherTest")) ? Visibility.Collapsed : Visibility.Visible);
					ModMain.FrmOtherLeft.PanItem.Visibility = ((num2 < 2 && !HiddenForceShow) ? Visibility.Collapsed : Visibility.Visible);
				}
				if (num2 == 1 && !HiddenForceShow)
				{
					if (!Settings.Get<bool>("UiHiddenOtherHelp"))
					{
						ModMain.FrmMain.BtnTitleSelect4.Text = "帮助";
					}
					else if (!Settings.Get<bool>("UiHiddenOtherAbout"))
					{
						ModMain.FrmMain.BtnTitleSelect4.Text = "关于";
					}
					else
					{
						ModMain.FrmMain.BtnTitleSelect4.Text = "百宝箱";
					}
				}
				else
				{
					ModMain.FrmMain.BtnTitleSelect4.Text = "更多";
				}
				if (ModMain.FrmMain.PageCurrent == FormMain.PageType.InstanceSelect)
				{
					ModMain.FrmSelectRight.BtnEmptyDownload_Loaded();
				}
				if (ModMain.FrmMain.PageCurrent == FormMain.PageType.Launch)
				{
					ModMain.FrmLaunchLeft.RefreshButtonsUI();
				}
				if (ModMain.FrmMain.PageCurrent == FormMain.PageType.InstanceSetup && ModMain.FrmInstanceModDisabled != null)
				{
					ModMain.FrmInstanceModDisabled.BtnDownload_Loaded();
				}
				if (ModMain.FrmSetupUI != null)
				{
					ModMain.FrmSetupUI.CardSwitch.Title = (HiddenForceShow ? "功能隐藏（已暂时关闭，按 F12 以重新启用）" : "功能隐藏");
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "刷新功能隐藏项目失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupUI.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
	}

	private void HiddenSetupMain()
	{
		if (CheckHiddenPageSetup.Checked)
		{
			CheckHiddenSetupLaunch.Checked = true;
			CheckHiddenSetupSystem.Checked = true;
			CheckHiddenSetupLink.Checked = true;
			CheckHiddenSetupUI.Checked = true;
		}
		else if (Settings.Get<bool>("UiHiddenSetupLaunch") && Settings.Get<bool>("UiHiddenSetupUi") && Settings.Get<bool>("UiHiddenSetupSystem") && Settings.Get<bool>("UiHiddenSetupLink"))
		{
			CheckHiddenSetupLaunch.Checked = false;
			CheckHiddenSetupSystem.Checked = false;
			CheckHiddenSetupLink.Checked = false;
			CheckHiddenSetupUI.Checked = false;
		}
	}

	private void HiddenSetupSub()
	{
		if (Settings.Get<bool>("UiHiddenSetupLaunch") && Settings.Get<bool>("UiHiddenSetupUi") && Settings.Get<bool>("UiHiddenSetupSystem") && Settings.Get<bool>("UiHiddenSetupLink"))
		{
			CheckHiddenPageSetup.Checked = true;
		}
		else
		{
			CheckHiddenPageSetup.Checked = false;
		}
	}

	private void HiddenOtherMain()
	{
		if (CheckHiddenPageOther.Checked)
		{
			CheckHiddenOtherAbout.Checked = true;
			CheckHiddenOtherTest.Checked = true;
			CheckHiddenOtherFeedback.Checked = true;
			CheckHiddenOtherVote.Checked = true;
			CheckHiddenOtherHelp.Checked = true;
		}
		else if (Settings.Get<bool>("UiHiddenOtherHelp") && Settings.Get<bool>("UiHiddenOtherAbout") && Settings.Get<bool>("UiHiddenOtherTest") && Settings.Get<bool>("UiHiddenOtherVote") && Settings.Get<bool>("UiHiddenOtherFeedback"))
		{
			CheckHiddenOtherAbout.Checked = false;
			CheckHiddenOtherTest.Checked = false;
			CheckHiddenOtherFeedback.Checked = false;
			CheckHiddenOtherVote.Checked = false;
			CheckHiddenOtherHelp.Checked = false;
		}
	}

	private void HiddenOtherSub(object sender, bool user)
	{
		if (Settings.Get<bool>("UiHiddenOtherHelp") && Settings.Get<bool>("UiHiddenOtherAbout") && Settings.Get<bool>("UiHiddenOtherTest"))
		{
			CheckHiddenPageOther.Checked = true;
		}
		else
		{
			CheckHiddenPageOther.Checked = false;
		}
		if (user && Settings.Get<bool>("UiHiddenOtherHelp") && Settings.Get<bool>("UiHiddenOtherAbout") && Settings.Get<bool>("UiHiddenOtherTest"))
		{
			CheckHiddenOtherFeedback.Checked = true;
			CheckHiddenOtherVote.Checked = true;
		}
	}

	private void HiddenOtherNet(object sender, bool user)
	{
		if (user && Settings.Get<bool>("UiHiddenOtherHelp") && Settings.Get<bool>("UiHiddenOtherAbout") && Settings.Get<bool>("UiHiddenOtherTest") && (!Settings.Get<bool>("UiHiddenOtherFeedback") || !Settings.Get<bool>("UiHiddenOtherVote")))
		{
			CheckHiddenOtherAbout.Checked = false;
			CheckHiddenOtherTest.Checked = false;
			CheckHiddenOtherHelp.Checked = false;
		}
	}

	private void HiddenHint(object sender, bool user)
	{
		if (Conversions.ToBoolean(ModAnimation.AniControlEnabled == 0 && Conversions.ToBoolean(NewLateBinding.LateGet(sender, null, "Checked", new object[0], null, null, null))))
		{
			ModMain.Hint("按 F12 即可暂时关闭功能隐藏设置。千万别忘了，要不然设置就改不回来了……");
		}
	}

	private void SliderLoad()
	{
		SliderMusicVolume.GetHintText = (VB_0024AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(NewLateBinding.LateGet(null, typeof(Math), "Ceiling", new object[1] { Operators.MultiplyObject(v, 0.1) }, null, null, null), "%"));
		SliderLauncherTransparent.GetHintText = (VB_0024AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(NewLateBinding.LateGet(null, typeof(Math), "Round", new object[1] { Operators.AddObject(40, Operators.MultiplyObject(v, 0.1)) }, null, null, null), "%"));
		SliderLauncherHue.GetHintText = (VB_0024AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(v, "°"));
		SliderLauncherSat.GetHintText = (VB_0024AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(v, "%"));
		checked
		{
			SliderLauncherDelta.GetHintText = (VB_0024AnonymousDelegate_26<int, string>)([SpecialName] (int Value) =>
			{
				if (Value > 90)
				{
					return "+" + Conversions.ToString(Value - 90);
				}
				return (Value == 90) ? Conversions.ToString(0) : Conversions.ToString(Value - 90);
			});
			SliderLauncherLight.GetHintText = (VB_0024AnonymousDelegate_26<int, string>)([SpecialName] (int Value) =>
			{
				if (Value > 20)
				{
					return "+" + Conversions.ToString(Value - 20);
				}
				return (Value == 20) ? Conversions.ToString(0) : Conversions.ToString(Value - 20);
			});
			SliderBackgroundOpacity.GetHintText = (VB_0024AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(NewLateBinding.LateGet(null, typeof(Math), "Round", new object[1] { Operators.MultiplyObject(v, 0.1) }, null, null, null), "%"));
			SliderBackgroundBlur.GetHintText = (VB_0024AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.ConcatenateObject(v, " 像素"));
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagesetup/pagesetupui.xaml", UriKind.Relative);
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
			PanBack = (MyScrollViewer)target;
			break;
		case 2:
			PanMain = (StackPanel)target;
			break;
		case 3:
			CardLauncher = (MyCard)target;
			break;
		case 4:
			SliderLauncherTransparent = (MySlider)target;
			break;
		case 5:
			LabLauncherHue = (TextBlock)target;
			break;
		case 6:
			SliderLauncherHue = (MySlider)target;
			break;
		case 7:
			LabLauncherDelta = (TextBlock)target;
			break;
		case 8:
			SliderLauncherDelta = (MySlider)target;
			break;
		case 9:
			LabLauncherSat = (TextBlock)target;
			break;
		case 10:
			SliderLauncherSat = (MySlider)target;
			break;
		case 11:
			LabLauncherLight = (TextBlock)target;
			break;
		case 12:
			SliderLauncherLight = (MySlider)target;
			break;
		case 13:
			PanLauncherTheme = (Grid)target;
			break;
		case 14:
			RadioLauncherTheme0 = (MyRadioBox)target;
			break;
		case 15:
			RadioLauncherTheme1 = (MyRadioBox)target;
			break;
		case 16:
			RadioLauncherTheme2 = (MyRadioBox)target;
			break;
		case 17:
			RadioLauncherTheme3 = (MyRadioBox)target;
			break;
		case 18:
			RadioLauncherTheme4 = (MyRadioBox)target;
			break;
		case 19:
			RadioLauncherTheme5 = (MyRadioBox)target;
			break;
		case 20:
			RadioLauncherTheme5Gray = (MyRadioBox)target;
			break;
		case 21:
			LabLauncherTheme5Unlock = (TextBlock)target;
			break;
		case 22:
			RadioLauncherTheme12 = (MyRadioBox)target;
			break;
		case 23:
			RadioLauncherTheme6 = (MyRadioBox)target;
			break;
		case 24:
			RadioLauncherTheme7 = (MyRadioBox)target;
			break;
		case 25:
			RadioLauncherTheme13 = (MyRadioBox)target;
			break;
		case 26:
			RadioLauncherTheme8 = (MyRadioBox)target;
			break;
		case 27:
			LabLauncherTheme8Copy = (TextBlock)target;
			break;
		case 28:
			RadioLauncherTheme9 = (MyRadioBox)target;
			break;
		case 29:
			LabLauncherTheme9Copy = (TextBlock)target;
			break;
		case 30:
			RadioLauncherTheme10 = (MyRadioBox)target;
			break;
		case 31:
			RadioLauncherTheme11 = (MyRadioBox)target;
			break;
		case 32:
			LabLauncherTheme11Click = (TextBlock)target;
			break;
		case 33:
			RadioLauncherTheme14 = (MyRadioBox)target;
			break;
		case 34:
			PanLauncherHide = (Border)target;
			break;
		case 35:
			CardBackground = (MyCard)target;
			break;
		case 36:
			PanBackgroundSuit = (Grid)target;
			break;
		case 37:
			PanBackgroundOpacity = (Grid)target;
			break;
		case 38:
			SliderBackgroundOpacity = (MySlider)target;
			break;
		case 39:
			PanBackgroundBlur = (Grid)target;
			break;
		case 40:
			SliderBackgroundBlur = (MySlider)target;
			break;
		case 41:
			BtnBackgroundOpen = (MyButton)target;
			break;
		case 42:
			BtnBackgroundRefresh = (MyButton)target;
			break;
		case 43:
			BtnBackgroundClear = (MyButton)target;
			break;
		case 44:
			CardMusic = (MyCard)target;
			break;
		case 45:
			PanMusicVolume = (Grid)target;
			break;
		case 46:
			SliderMusicVolume = (MySlider)target;
			break;
		case 47:
			PanMusicDetail = (StackPanel)target;
			break;
		case 48:
			CheckMusicRandom = (MyCheckBox)target;
			break;
		case 49:
			CheckMusicAuto = (MyCheckBox)target;
			break;
		case 50:
			CheckMusicStart = (MyCheckBox)target;
			break;
		case 51:
			CheckMusicStop = (MyCheckBox)target;
			break;
		case 52:
			BtnMusicOpen = (MyButton)target;
			break;
		case 53:
			BtnMusicRefresh = (MyButton)target;
			break;
		case 54:
			BtnMusicClear = (MyButton)target;
			break;
		case 55:
			CardLogo = (MyCard)target;
			break;
		case 56:
			RadioLogoType0 = (MyRadioBox)target;
			break;
		case 57:
			RadioLogoType1 = (MyRadioBox)target;
			break;
		case 58:
			RadioLogoType2 = (MyRadioBox)target;
			break;
		case 59:
			RadioLogoType3 = (MyRadioBox)target;
			break;
		case 60:
			CheckLogoLeft = (MyCheckBox)target;
			break;
		case 61:
			PanLogoText = (Grid)target;
			break;
		case 62:
			TextLogoText = (MyTextBox)target;
			break;
		case 63:
			PanLogoChange = (Grid)target;
			break;
		case 64:
			BtnLogoChange = (MyButton)target;
			break;
		case 65:
			BtnLogoDelete = (MyButton)target;
			break;
		case 66:
			CardCustom = (MyCard)target;
			break;
		case 67:
			RadioCustomType0 = (MyRadioBox)target;
			break;
		case 68:
			RadioCustomType3 = (MyRadioBox)target;
			break;
		case 69:
			RadioCustomType1 = (MyRadioBox)target;
			break;
		case 70:
			RadioCustomType2 = (MyRadioBox)target;
			break;
		case 71:
			HintCustom = (MyHint)target;
			break;
		case 72:
			HintCustomWarn = (MyHint)target;
			break;
		case 73:
			PanCustomLocal = (WrapPanel)target;
			break;
		case 74:
			BtnCustomRefresh = (MyButton)target;
			break;
		case 75:
			BtnCustomFile = (MyButton)target;
			break;
		case 76:
			BtnCustomTutorial = (MyButton)target;
			break;
		case 77:
			BtnCustomFolder = (MyButton)target;
			break;
		case 78:
			PanCustomNet = (Grid)target;
			break;
		case 79:
			TextCustomNet = (MyTextBox)target;
			break;
		case 80:
			PanCustomPreset = (Grid)target;
			break;
		case 81:
			CardSwitch = (MyCard)target;
			break;
		case 82:
			CheckHiddenPageDownload = (MyCheckBox)target;
			break;
		case 83:
			CheckHiddenPageLink = (MyCheckBox)target;
			break;
		case 84:
			CheckHiddenPageSetup = (MyCheckBox)target;
			break;
		case 85:
			CheckHiddenPageOther = (MyCheckBox)target;
			break;
		case 86:
			CheckHiddenSetupLaunch = (MyCheckBox)target;
			break;
		case 87:
			CheckHiddenSetupLink = (MyCheckBox)target;
			break;
		case 88:
			CheckHiddenSetupUI = (MyCheckBox)target;
			break;
		case 89:
			CheckHiddenSetupSystem = (MyCheckBox)target;
			break;
		case 90:
			CheckHiddenOtherHelp = (MyCheckBox)target;
			break;
		case 91:
			CheckHiddenOtherAbout = (MyCheckBox)target;
			break;
		case 92:
			CheckHiddenOtherTest = (MyCheckBox)target;
			break;
		case 93:
			CheckHiddenOtherFeedback = (MyCheckBox)target;
			break;
		case 94:
			CheckHiddenOtherVote = (MyCheckBox)target;
			break;
		case 95:
			CheckLauncherEmail = (MyCheckBox)target;
			break;
		case 96:
			CheckHiddenFunctionSelect = (MyCheckBox)target;
			break;
		case 97:
			CheckHiddenFunctionModUpdate = (MyCheckBox)target;
			break;
		case 98:
			CheckHiddenFunctionHidden = (MyCheckBox)target;
			break;
		case 99:
			CheckHiddenFunctionModify = (MyCheckBox)target;
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
