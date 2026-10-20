using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Windows.Threading;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using PCL.My;
using PCLCS;

namespace PCL;

[DesignerGenerated]
public class PageInstanceSetup : MyPageRight, IComponentConnector
{
	[Serializable]
	[CompilerGenerated]
	internal new sealed class _Closure_0024__
	{
		public static readonly _Closure_0024__ _0024I;

		public static Func<string, Version> _0024I2_002D1;

		public static Func<string, string> _0024I2_002D0;

		public static Func<FileInfo, bool> _0024I12_002D0;

		public static MouseButtonEventHandler _0024I23_002D0;

		public static MouseButtonEventHandler _0024I23_002D4;

		static _Closure_0024__()
		{
			_0024I = new _Closure_0024__();
		}

		[SpecialName]
		internal string _Lambda_0024__2_002D0(string Str)
		{
			string result;
			try
			{
				if (string.IsNullOrWhiteSpace(Str))
				{
					result = "不能为空";
				}
				else
				{
					ValueRange<Version> val = ValueRange<Version>.FromString(Str, (Func<string, Version>)([SpecialName] (string s) => StringUtils.ParseVersionWithDefaults(s)));
					result = (val.IsEmpty() ? "范围下限的版本号比上限更高，导致该范围无法匹配到任何版本" : ((val.HasLower && val.Lower.Major <= 1 && (val.Lower.Minor > 0 || val.Lower.Build > 0 || val.Lower.Revision > 0)) ? "不应使用 1.x 格式进行匹配（例如，若想匹配 Java 17，应填写 17 而非 1.17）" : ((val.HasUpper && val.Upper.Major <= 1 && (val.Upper.Minor > 0 || val.Upper.Build > 0 || val.Upper.Revision > 0)) ? "不应使用 1.x 格式进行匹配（例如，若想匹配 Java 17，应填写 17 而非 1.17）" : ((val.HasUpper && val.Upper.Major <= 4) ? "该范围所要求的 Java 版本过低" : ((val.HasUpper && val.IsUpperInclusive && val.Upper.Minor <= 0 && val.Upper.Build <= 0) ? $"右侧闭区间的意图并不明确。如果不想允许 Java {val.Upper.Major}，请改为 {ModBase.vbLQ}{val.Upper.Major}){ModBase.vbRQ}。如果想允许 Java {val.Upper.Major}，请改为 {ModBase.vbLQ}{checked(val.Upper.Major + 1)}){ModBase.vbRQ}。" : ((!val.Intersect(ValueRange<Version>.OpenClosed(new Version(5, 0), new Version(99, 0))).IsEmpty()) ? "" : "该范围无法匹配到任何常见的 Java"))))));
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				result = ex2.Message;
				ProjectData.ClearProjectError();
			}
			return result;
		}

		[SpecialName]
		internal Version _Lambda_0024__2_002D1(string s)
		{
			return StringUtils.ParseVersionWithDefaults(s);
		}

		[SpecialName]
		internal bool _Lambda_0024__12_002D0(FileInfo f)
		{
			return new string[3] { ".jar", ".zip", ".litemod" }.Contains(StringExtensions.Lower(f.Extension));
		}

		[SpecialName]
		internal void _Lambda_0024__23_002D0(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		[SpecialName]
		internal void _Lambda_0024__23_002D4(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			ModJava.ManuallyImportJava();
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__23_002D0
	{
		public Java _0024VB_0024Local_JavaEntry;

		public PageInstanceSetup _0024VB_0024Me;

		public _Closure_0024__23_002D0(_Closure_0024__23_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_JavaEntry = arg0._0024VB_0024Local_JavaEntry;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R2(object a0, MouseButtonEventArgs a1)
		{
			_Lambda_0024__1();
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			Configs.JavaForced.Set(_0024VB_0024Local_JavaEntry, (IConfigProvider)(object)PageInstanceLeft.Instance.Config);
			_0024VB_0024Me.UpdateJavaList();
			_0024VB_0024Me.ComboArgumentJavaSelect.IsDropDownOpen = false;
		}

		[SpecialName]
		internal void _Lambda_0024__R3(object a0, EventArgs a1)
		{
			_Lambda_0024__2();
		}

		[SpecialName]
		internal void _Lambda_0024__2()
		{
			ModJava.ManuallyRemoveJava(_0024VB_0024Local_JavaEntry);
		}

		[SpecialName]
		internal void _Lambda_0024__R4(object a0, EventArgs a1)
		{
			_Lambda_0024__3();
		}

		[SpecialName]
		internal void _Lambda_0024__3()
		{
			ModBase.OpenExplorer(_0024VB_0024Local_JavaEntry.JavaExePath);
		}
	}

	private bool LoadedFirst;

	private int RamTextLeft;

	private int RamTextRight;

	private int ComboServerLoginLast;

	[CompilerGenerated]
	[AccessedThroughProperty("ComboArgumentIndieV2")]
	private MyComboBox _ComboArgumentIndieV2;

	[CompilerGenerated]
	[AccessedThroughProperty("ComboArgumentJava")]
	private MyComboBox _ComboArgumentJava;

	[CompilerGenerated]
	[AccessedThroughProperty("TextArgumentJavaRange")]
	private MyTextBox _TextArgumentJavaRange;

	[CompilerGenerated]
	[AccessedThroughProperty("HintArgumentJava")]
	private MyHint _HintArgumentJava;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnArgumentJavaSearch")]
	private MyIconButton _BtnArgumentJavaSearch;

	[CompilerGenerated]
	[AccessedThroughProperty("RadioRamType2")]
	private MyRadioBox _RadioRamType2;

	[CompilerGenerated]
	[AccessedThroughProperty("RadioRamType0")]
	private MyRadioBox _RadioRamType0;

	[CompilerGenerated]
	[AccessedThroughProperty("RadioRamType1")]
	private MyRadioBox _RadioRamType1;

	[CompilerGenerated]
	[AccessedThroughProperty("SliderRamCustom")]
	private MySlider _SliderRamCustom;

	[CompilerGenerated]
	[AccessedThroughProperty("RectRamGame")]
	private Rectangle _RectRamGame;

	[CompilerGenerated]
	[AccessedThroughProperty("RectRamEmpty")]
	private Rectangle _RectRamEmpty;

	[CompilerGenerated]
	[AccessedThroughProperty("LabRamGame")]
	private TextBlock _LabRamGame;

	[CompilerGenerated]
	[AccessedThroughProperty("ComboServerLogin")]
	private MyComboBox _ComboServerLogin;

	[CompilerGenerated]
	[AccessedThroughProperty("TextServerNide")]
	private MyTextBox _TextServerNide;

	[CompilerGenerated]
	[AccessedThroughProperty("TextServerAuthServer")]
	private MyTextBox _TextServerAuthServer;

	[CompilerGenerated]
	[AccessedThroughProperty("TextServerAuthRegister")]
	private MyTextBox _TextServerAuthRegister;

	[CompilerGenerated]
	[AccessedThroughProperty("TextServerEnter")]
	private MyTextBox _TextServerEnter;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnServerAuthLittle")]
	private MyButton _BtnServerAuthLittle;

	[CompilerGenerated]
	[AccessedThroughProperty("TextAdvanceJvm")]
	private MyTextBox _TextAdvanceJvm;

	[CompilerGenerated]
	[AccessedThroughProperty("TextAdvanceGame")]
	private MyTextBox _TextAdvanceGame;

	[CompilerGenerated]
	[AccessedThroughProperty("TextAdvanceRun")]
	private MyTextBox _TextAdvanceRun;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnSwitch")]
	private MyExtraTextButton _BtnSwitch;

	private bool _contentLoaded;

	[SpecialName]
	private bool _0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting_0024Init;

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

	[field: AccessedThroughProperty("CardArgument")]
	internal virtual MyCard CardArgument
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyComboBox ComboArgumentIndieV2
	{
		[CompilerGenerated]
		get
		{
			return _ComboArgumentIndieV2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SelectionChangedEventHandler value2 = ComboArgumentIndieV2_SelectionChanged;
			MyComboBox comboArgumentIndieV = _ComboArgumentIndieV2;
			if (comboArgumentIndieV != null)
			{
				comboArgumentIndieV.SelectionChanged -= value2;
			}
			_ComboArgumentIndieV2 = value;
			comboArgumentIndieV = _ComboArgumentIndieV2;
			if (comboArgumentIndieV != null)
			{
				comboArgumentIndieV.SelectionChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TextArgumentTitle")]
	internal virtual MyTextBox TextArgumentTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextArgumentInfo")]
	internal virtual MyTextBox TextArgumentInfo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyComboBox ComboArgumentJava
	{
		[CompilerGenerated]
		get
		{
			return _ComboArgumentJava;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SelectionChangedEventHandler value2 = [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
			{
				ReloadCurrentJava();
			};
			SelectionChangedEventHandler value3 = [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
			{
				UpdateJavaRelatedUi();
			};
			MyComboBox comboArgumentJava = _ComboArgumentJava;
			if (comboArgumentJava != null)
			{
				comboArgumentJava.SelectionChanged -= value2;
				comboArgumentJava.SelectionChanged -= value3;
			}
			_ComboArgumentJava = value;
			comboArgumentJava = _ComboArgumentJava;
			if (comboArgumentJava != null)
			{
				comboArgumentJava.SelectionChanged += value2;
				comboArgumentJava.SelectionChanged += value3;
			}
		}
	}

	internal virtual MyTextBox TextArgumentJavaRange
	{
		[CompilerGenerated]
		get
		{
			return _TextArgumentJavaRange;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				ReloadCurrentJava();
			};
			TextChangedEventHandler value3 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
			{
				UpdateJavaRelatedUi();
			};
			MyTextBox textArgumentJavaRange = _TextArgumentJavaRange;
			if (textArgumentJavaRange != null)
			{
				textArgumentJavaRange.ValidatedTextChanged -= value2;
				textArgumentJavaRange.TextChanged -= value3;
			}
			_TextArgumentJavaRange = value;
			textArgumentJavaRange = _TextArgumentJavaRange;
			if (textArgumentJavaRange != null)
			{
				textArgumentJavaRange.ValidatedTextChanged += value2;
				textArgumentJavaRange.TextChanged += value3;
			}
		}
	}

	internal virtual MyHint HintArgumentJava
	{
		[CompilerGenerated]
		get
		{
			return _HintArgumentJava;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyHint.ClickEventHandler obj = HintArgumentJava_Click;
			MyHint hintArgumentJava = _HintArgumentJava;
			if (hintArgumentJava != null)
			{
				hintArgumentJava.Click -= obj;
			}
			_HintArgumentJava = value;
			hintArgumentJava = _HintArgumentJava;
			if (hintArgumentJava != null)
			{
				hintArgumentJava.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanArgumentJavaSelect")]
	internal virtual Grid PanArgumentJavaSelect
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboArgumentJavaSelect")]
	internal virtual MyComboBox ComboArgumentJavaSelect
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabArgumentJava")]
	internal virtual TextBlock LabArgumentJava
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnArgumentJavaSearch
	{
		[CompilerGenerated]
		get
		{
			return _BtnArgumentJavaSearch;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				BtnAdvanceJavaSearch_Click();
			};
			MyIconButton btnArgumentJavaSearch = _BtnArgumentJavaSearch;
			if (btnArgumentJavaSearch != null)
			{
				btnArgumentJavaSearch.Click -= obj;
			}
			_BtnArgumentJavaSearch = value;
			btnArgumentJavaSearch = _BtnArgumentJavaSearch;
			if (btnArgumentJavaSearch != null)
			{
				btnArgumentJavaSearch.Click += obj;
			}
		}
	}

	internal virtual MyRadioBox RadioRamType2
	{
		[CompilerGenerated]
		get
		{
			return _RadioRamType2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				RefreshRam();
			};
			MyRadioBox radioRamType = _RadioRamType2;
			if (radioRamType != null)
			{
				radioRamType.Check -= obj;
			}
			_RadioRamType2 = value;
			radioRamType = _RadioRamType2;
			if (radioRamType != null)
			{
				radioRamType.Check += obj;
			}
		}
	}

	internal virtual MyRadioBox RadioRamType0
	{
		[CompilerGenerated]
		get
		{
			return _RadioRamType0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				RefreshRam();
			};
			MyRadioBox radioRamType = _RadioRamType0;
			if (radioRamType != null)
			{
				radioRamType.Check -= obj;
			}
			_RadioRamType0 = value;
			radioRamType = _RadioRamType0;
			if (radioRamType != null)
			{
				radioRamType.Check += obj;
			}
		}
	}

	internal virtual MyRadioBox RadioRamType1
	{
		[CompilerGenerated]
		get
		{
			return _RadioRamType1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				RefreshRam();
			};
			MyRadioBox radioRamType = _RadioRamType1;
			if (radioRamType != null)
			{
				radioRamType.Check -= obj;
			}
			_RadioRamType1 = value;
			radioRamType = _RadioRamType1;
			if (radioRamType != null)
			{
				radioRamType.Check += obj;
			}
		}
	}

	internal virtual MySlider SliderRamCustom
	{
		[CompilerGenerated]
		get
		{
			return _SliderRamCustom;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MySlider.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				RefreshRam();
			};
			MySlider sliderRamCustom = _SliderRamCustom;
			if (sliderRamCustom != null)
			{
				sliderRamCustom.Change -= obj;
			}
			_SliderRamCustom = value;
			sliderRamCustom = _SliderRamCustom;
			if (sliderRamCustom != null)
			{
				sliderRamCustom.Change += obj;
			}
		}
	}

	[field: AccessedThroughProperty("ComboRamOptimize")]
	internal virtual MyComboBox ComboRamOptimize
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanRamDisplay")]
	internal virtual Grid PanRamDisplay
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnRamUsed")]
	internal virtual ColumnDefinition ColumnRamUsed
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnRamGame")]
	internal virtual ColumnDefinition ColumnRamGame
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnRamEmpty")]
	internal virtual ColumnDefinition ColumnRamEmpty
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RectRamUsed")]
	internal virtual Rectangle RectRamUsed
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Rectangle RectRamGame
	{
		[CompilerGenerated]
		get
		{
			return _RectRamGame;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SizeChangedEventHandler value2 = [SpecialName] (object a0, SizeChangedEventArgs a1) =>
			{
				RefreshRamText();
			};
			Rectangle rectRamGame = _RectRamGame;
			if (rectRamGame != null)
			{
				rectRamGame.SizeChanged -= value2;
			}
			_RectRamGame = value;
			rectRamGame = _RectRamGame;
			if (rectRamGame != null)
			{
				rectRamGame.SizeChanged += value2;
			}
		}
	}

	internal virtual Rectangle RectRamEmpty
	{
		[CompilerGenerated]
		get
		{
			return _RectRamEmpty;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SizeChangedEventHandler value2 = [SpecialName] (object a0, SizeChangedEventArgs a1) =>
			{
				RefreshRamText();
			};
			Rectangle rectRamEmpty = _RectRamEmpty;
			if (rectRamEmpty != null)
			{
				rectRamEmpty.SizeChanged -= value2;
			}
			_RectRamEmpty = value;
			rectRamEmpty = _RectRamEmpty;
			if (rectRamEmpty != null)
			{
				rectRamEmpty.SizeChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LabRamUsedTitle")]
	internal virtual TextBlock LabRamUsedTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabRamGameTitle")]
	internal virtual TextBlock LabRamGameTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabRamUsed")]
	internal virtual TextBlock LabRamUsed
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabRamTotal")]
	internal virtual TextBlock LabRamTotal
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual TextBlock LabRamGame
	{
		[CompilerGenerated]
		get
		{
			return _LabRamGame;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SizeChangedEventHandler value2 = [SpecialName] (object a0, SizeChangedEventArgs a1) =>
			{
				RefreshRamText();
			};
			TextBlock labRamGame = _LabRamGame;
			if (labRamGame != null)
			{
				labRamGame.SizeChanged -= value2;
			}
			_LabRamGame = value;
			labRamGame = _LabRamGame;
			if (labRamGame != null)
			{
				labRamGame.SizeChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("CardServer")]
	internal virtual MyCard CardServer
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyComboBox ComboServerLogin
	{
		[CompilerGenerated]
		get
		{
			return _ComboServerLogin;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SelectionChangedEventHandler value2 = [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
			{
				ComboServerLogin_Changed();
			};
			MyComboBox comboServerLogin = _ComboServerLogin;
			if (comboServerLogin != null)
			{
				comboServerLogin.SelectionChanged -= value2;
			}
			_ComboServerLogin = value;
			comboServerLogin = _ComboServerLogin;
			if (comboServerLogin != null)
			{
				comboServerLogin.SelectionChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LabServerNide")]
	internal virtual TextBlock LabServerNide
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyTextBox TextServerNide
	{
		[CompilerGenerated]
		get
		{
			return _TextServerNide;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				ComboServerLogin_Changed();
			};
			MyTextBox textServerNide = _TextServerNide;
			if (textServerNide != null)
			{
				textServerNide.ValidatedTextChanged -= value2;
			}
			_TextServerNide = value;
			textServerNide = _TextServerNide;
			if (textServerNide != null)
			{
				textServerNide.ValidatedTextChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LabServerAuthServer")]
	internal virtual TextBlock LabServerAuthServer
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyTextBox TextServerAuthServer
	{
		[CompilerGenerated]
		get
		{
			return _TextServerAuthServer;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				ComboServerLogin_Changed();
			};
			MyTextBox textServerAuthServer = _TextServerAuthServer;
			if (textServerAuthServer != null)
			{
				textServerAuthServer.ValidatedTextChanged -= value2;
			}
			_TextServerAuthServer = value;
			textServerAuthServer = _TextServerAuthServer;
			if (textServerAuthServer != null)
			{
				textServerAuthServer.ValidatedTextChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LabServerAuthRegister")]
	internal virtual TextBlock LabServerAuthRegister
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyTextBox TextServerAuthRegister
	{
		[CompilerGenerated]
		get
		{
			return _TextServerAuthRegister;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				ComboServerLogin_Changed();
			};
			MyTextBox textServerAuthRegister = _TextServerAuthRegister;
			if (textServerAuthRegister != null)
			{
				textServerAuthRegister.ValidatedTextChanged -= value2;
			}
			_TextServerAuthRegister = value;
			textServerAuthRegister = _TextServerAuthRegister;
			if (textServerAuthRegister != null)
			{
				textServerAuthRegister.ValidatedTextChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LabServerAuthName")]
	internal virtual TextBlock LabServerAuthName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextServerAuthName")]
	internal virtual MyTextBox TextServerAuthName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyTextBox TextServerEnter
	{
		[CompilerGenerated]
		get
		{
			return _TextServerEnter;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TextChangedEventHandler value2 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
			{
				TextServerEnter_Change();
			};
			MyTextBox textServerEnter = _TextServerEnter;
			if (textServerEnter != null)
			{
				textServerEnter.TextChanged -= value2;
			}
			_TextServerEnter = value;
			textServerEnter = _TextServerEnter;
			if (textServerEnter != null)
			{
				textServerEnter.TextChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("PanServerNide")]
	internal virtual Grid PanServerNide
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnServerAuthLittle
	{
		[CompilerGenerated]
		get
		{
			return _BtnServerAuthLittle;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnServerAuthLittle_Click;
			MyButton btnServerAuthLittle = _BtnServerAuthLittle;
			if (btnServerAuthLittle != null)
			{
				btnServerAuthLittle.Click -= obj;
			}
			_BtnServerAuthLittle = value;
			btnServerAuthLittle = _BtnServerAuthLittle;
			if (btnServerAuthLittle != null)
			{
				btnServerAuthLittle.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("CardAdvance")]
	internal virtual MyCard CardAdvance
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyTextBox TextAdvanceJvm
	{
		[CompilerGenerated]
		get
		{
			return _TextAdvanceJvm;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TextChangedEventHandler value2 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
			{
				ReplaceEnter((MyTextBox)a0, a1);
			};
			MyTextBox textAdvanceJvm = _TextAdvanceJvm;
			if (textAdvanceJvm != null)
			{
				textAdvanceJvm.TextChanged -= value2;
			}
			_TextAdvanceJvm = value;
			textAdvanceJvm = _TextAdvanceJvm;
			if (textAdvanceJvm != null)
			{
				textAdvanceJvm.TextChanged += value2;
			}
		}
	}

	internal virtual MyTextBox TextAdvanceGame
	{
		[CompilerGenerated]
		get
		{
			return _TextAdvanceGame;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TextChangedEventHandler value2 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
			{
				ReplaceEnter((MyTextBox)a0, a1);
			};
			MyTextBox textAdvanceGame = _TextAdvanceGame;
			if (textAdvanceGame != null)
			{
				textAdvanceGame.TextChanged -= value2;
			}
			_TextAdvanceGame = value;
			textAdvanceGame = _TextAdvanceGame;
			if (textAdvanceGame != null)
			{
				textAdvanceGame.TextChanged += value2;
			}
		}
	}

	internal virtual MyTextBox TextAdvanceRun
	{
		[CompilerGenerated]
		get
		{
			return _TextAdvanceRun;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TextChangedEventHandler value2 = TextAdvanceRun_TextChanged;
			MyTextBox textAdvanceRun = _TextAdvanceRun;
			if (textAdvanceRun != null)
			{
				textAdvanceRun.TextChanged -= value2;
			}
			_TextAdvanceRun = value;
			textAdvanceRun = _TextAdvanceRun;
			if (textAdvanceRun != null)
			{
				textAdvanceRun.TextChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("CheckAdvanceRunWait")]
	internal virtual MyCheckBox CheckAdvanceRunWait
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckAdvanceDisableModUpdate")]
	internal virtual MyCheckBox CheckAdvanceDisableModUpdate
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckAdvanceAssetsV2")]
	internal virtual MyCheckBox CheckAdvanceAssetsV2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckAdvanceDisableJLW")]
	internal virtual MyCheckBox CheckAdvanceDisableJLW
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyExtraTextButton BtnSwitch
	{
		[CompilerGenerated]
		get
		{
			return _BtnSwitch;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyExtraTextButton.ClickEventHandler obj = BtnSwitch_Click;
			MyExtraTextButton btnSwitch = _BtnSwitch;
			if (btnSwitch != null)
			{
				btnSwitch.Click -= obj;
			}
			_BtnSwitch = value;
			btnSwitch = _BtnSwitch;
			if (btnSwitch != null)
			{
				btnSwitch.Click += obj;
			}
		}
	}

	public PageInstanceSetup()
	{
		base.Loaded += PageSetupSystem_Loaded;
		LoadedFirst = false;
		RamTextLeft = 2;
		RamTextRight = 1;
		InitializeComponent();
	}

	private void PageSetupSystem_Loaded(object sender, RoutedEventArgs e)
	{
		PanBack.ScrollToHome();
		RefreshRam(ShowAnim: false);
		checked
		{
			ModAnimation.AniControlEnabled++;
			Reload();
			ModAnimation.AniControlEnabled--;
			if (LoadedFirst)
			{
				return;
			}
			LoadedFirst = true;
			((RedoableWorkerBase<Java>)(object)PageInstanceLeft.CurrentJavaWorker).Started += UpdateJavaRelatedUi;
			((RedoableWorkerBase<Java>)(object)PageInstanceLeft.CurrentJavaWorker).Stopped += UpdateJavaRelatedUi;
			TextArgumentJavaRange.ValidateRules = new Collection<Validate>
			{
				new ValidateFunc([SpecialName] (string Str) =>
				{
					string result;
					try
					{
						if (string.IsNullOrWhiteSpace(Str))
						{
							result = "不能为空";
						}
						else
						{
							ValueRange<Version> val = ValueRange<Version>.FromString(Str, (Func<string, Version>)([SpecialName] (string s) => StringUtils.ParseVersionWithDefaults(s)));
							result = (val.IsEmpty() ? "范围下限的版本号比上限更高，导致该范围无法匹配到任何版本" : ((val.HasLower && val.Lower.Major <= 1 && (val.Lower.Minor > 0 || val.Lower.Build > 0 || val.Lower.Revision > 0)) ? "不应使用 1.x 格式进行匹配（例如，若想匹配 Java 17，应填写 17 而非 1.17）" : ((val.HasUpper && val.Upper.Major <= 1 && (val.Upper.Minor > 0 || val.Upper.Build > 0 || val.Upper.Revision > 0)) ? "不应使用 1.x 格式进行匹配（例如，若想匹配 Java 17，应填写 17 而非 1.17）" : ((val.HasUpper && val.Upper.Major <= 4) ? "该范围所要求的 Java 版本过低" : ((val.HasUpper && val.IsUpperInclusive && val.Upper.Minor <= 0 && val.Upper.Build <= 0) ? $"右侧闭区间的意图并不明确。如果不想允许 Java {val.Upper.Major}，请改为 {ModBase.vbLQ}{val.Upper.Major}){ModBase.vbRQ}。如果想允许 Java {val.Upper.Major}，请改为 {ModBase.vbLQ}{val.Upper.Major + 1}){ModBase.vbRQ}。" : ((!val.Intersect(ValueRange<Version>.OpenClosed(new Version(5, 0), new Version(99, 0))).IsEmpty()) ? "" : "该范围无法匹配到任何常见的 Java"))))));
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						result = ex2.Message;
						ProjectData.ClearProjectError();
					}
					return result;
				})
			};
			DispatcherTimer dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
			dispatcherTimer.Tick += [SpecialName] (object a0, EventArgs a1) =>
			{
				RefreshRam();
			};
			dispatcherTimer.Start();
		}
	}

	public void Reload()
	{
		try
		{
			_ = PageInstanceLeft.Instance.PathIndie;
			ComboArgumentIndieV2.SelectedIndex = ((!Settings.Get<bool>("VersionArgumentIndieV2", PageInstanceLeft.Instance)) ? 1 : 0);
			UpdateJavaList();
			ComboServerLogin.SelectedIndex = Settings.Get<int>("VersionServerLogin", PageInstanceLeft.Instance);
			ComboServerLoginLast = ComboServerLogin.SelectedIndex;
			UpdateServerLoginUI();
			if (Settings.Get<int>("VersionAdvanceAssets", PageInstanceLeft.Instance) == 2)
			{
				Logger.Info("已迁移老版本的关闭文件校验设置", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceSetup.xaml.vb");
				Settings.Reset("VersionAdvanceAssets", PageInstanceLeft.Instance);
				Settings.Set("VersionAdvanceAssetsV2", true, PageInstanceLeft.Instance);
			}
			SettingService.RefreshSettings(this);
			OnVersionRamTypeChanged(Settings.Get<int>("VersionRamType", PageInstanceLeft.Instance));
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "重载版本独立设置时出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceSetup.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	public void Reset()
	{
		try
		{
			SettingService.ResetSettings(this);
			Settings.Reset("VersionServerLogin", PageInstanceLeft.Instance);
			Settings.Reset("VersionArgumentIndieV2", PageInstanceLeft.Instance);
			Settings.Reset("VersionAdvanceAssets", PageInstanceLeft.Instance);
			Configs.JavaForced.Reset((IConfigProvider)(object)PageInstanceLeft.Instance.Config);
			((RedoableWorkerBase<NoType>)(object)ModJava.JavaListRefreshWorker).Start(default(CancellationToken), (ProgressProvider)null);
			Logger.Info("已初始化版本独立设置", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceSetup.xaml.vb");
			ModMain.Hint("已初始化版本独立设置！", ModMain.HintType.Green, Log: false);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "初始化版本独立设置失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceSetup.xaml.vb");
			ProjectData.ClearProjectError();
		}
		Reload();
	}

	public static void OnVersionRamTypeChanged(int Type)
	{
		if (ModMain.FrmInstanceSetup != null)
		{
			ModMain.FrmInstanceSetup.RamType(Type);
		}
	}

	public void RamType(int Type)
	{
		if (SliderRamCustom != null)
		{
			SliderRamCustom.IsEnabled = Type == 1;
		}
	}

	public void RefreshRam(bool ShowAnim)
	{
		checked
		{
			if (LabRamGame != null && LabRamUsed != null && !(ModMain.FrmMain.PageCurrent != FormMain.PageType.InstanceSetup) && ModMain.FrmInstanceLeft.PageID == FormMain.PageSubType.SetupLink)
			{
				double num = Math.Round(GetRam(PageInstanceLeft.Instance), 5);
				double num2 = Math.Round((double)MyWpfExtension.Computer.Info.TotalPhysicalMemory / 1024.0 / 1024.0 / 1024.0, 1);
				double num3 = Math.Round((double)MyWpfExtension.Computer.Info.AvailablePhysicalMemory / 1024.0 / 1024.0 / 1024.0, 1);
				double num4 = Math.Round(Math.Min(num, num3), 5);
				double num5 = Math.Round(num2 - num3, 5);
				double num6 = Math.Round(MathUtils.Clamp<double>(num2 - num5 - num, 0.0, 1000.0), 1);
				if (num2 <= 1.5)
				{
					SliderRamCustom.MaxValue = (int)Math.Round(Math.Max(Math.Floor((num2 - 0.3) / 0.1), 1.0));
				}
				else if (num2 <= 8.0)
				{
					SliderRamCustom.MaxValue = (int)Math.Round(Math.Floor((num2 - 1.5) / 0.5) + 12.0);
				}
				else if (num2 <= 16.0)
				{
					SliderRamCustom.MaxValue = (int)Math.Round(Math.Floor((num2 - 8.0) / 1.0) + 25.0);
				}
				else
				{
					SliderRamCustom.MaxValue = (int)Math.Round(Math.Floor((num2 - 16.0) / 2.0) + 33.0);
				}
				LabRamGame.Text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject((num == Math.Floor(num)) ? (Conversions.ToString(num) + ".0") : ((object)num), " GB"), (num != num4) ? Operators.ConcatenateObject(Operators.ConcatenateObject(" (可用 ", (num4 == Math.Floor(num4)) ? (Conversions.ToString(num4) + ".0") : ((object)num4)), " GB)") : ""));
				LabRamUsed.Text = Conversions.ToString(Operators.ConcatenateObject((num5 == Math.Floor(num5)) ? (Conversions.ToString(num5) + ".0") : ((object)num5), " GB"));
				LabRamTotal.Text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(" / ", (num2 == Math.Floor(num2)) ? (Conversions.ToString(num2) + ".0") : ((object)num2)), " GB"));
				if (ShowAnim)
				{
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaGridLengthWidth(ColumnRamUsed, num5 - ColumnRamUsed.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)),
						ModAnimation.AaGridLengthWidth(ColumnRamGame, num4 - ColumnRamGame.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)),
						ModAnimation.AaGridLengthWidth(ColumnRamEmpty, num6 - ColumnRamEmpty.Width.Value, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong))
					}, "VersionSetup Ram Grid");
				}
				else
				{
					ColumnRamUsed.Width = new GridLength(num5, GridUnitType.Star);
					ColumnRamGame.Width = new GridLength(num4, GridUnitType.Star);
					ColumnRamEmpty.Width = new GridLength(num6, GridUnitType.Star);
				}
			}
		}
	}

	private void RefreshRam()
	{
		RefreshRam(ShowAnim: true);
	}

	private void RefreshRamText()
	{
		double actualWidth = RectRamUsed.ActualWidth;
		double actualWidth2 = PanRamDisplay.ActualWidth;
		double actualWidth3 = LabRamGame.ActualWidth;
		double actualWidth4 = LabRamUsed.ActualWidth;
		double actualWidth5 = LabRamTotal.ActualWidth;
		double actualWidth6 = LabRamGameTitle.ActualWidth;
		double actualWidth7 = LabRamUsedTitle.ActualWidth;
		int num = ((!(actualWidth - 30.0 < actualWidth4) && !(actualWidth - 30.0 < actualWidth7)) ? ((actualWidth - 25.0 < actualWidth4 + actualWidth5) ? 1 : 2) : 0);
		if (RamTextLeft != num)
		{
			RamTextLeft = num;
			switch (num)
			{
			case 0:
				ModAnimation.AniStart(new ModAnimation.AniData[3]
				{
					ModAnimation.AaOpacity(LabRamUsed, 0.0 - LabRamUsed.Opacity, 100),
					ModAnimation.AaOpacity(LabRamTotal, 0.0 - LabRamTotal.Opacity, 100),
					ModAnimation.AaOpacity(LabRamUsedTitle, 0.0 - LabRamUsedTitle.Opacity, 100)
				}, "VersionSetup Ram TextLeft");
				break;
			case 1:
				ModAnimation.AniStart(new ModAnimation.AniData[3]
				{
					ModAnimation.AaOpacity(LabRamUsed, 1.0 - LabRamUsed.Opacity, 100),
					ModAnimation.AaOpacity(LabRamTotal, 0.0 - LabRamTotal.Opacity, 100),
					ModAnimation.AaOpacity(LabRamUsedTitle, 0.7 - LabRamUsedTitle.Opacity, 100)
				}, "VersionSetup Ram TextLeft");
				break;
			case 2:
				ModAnimation.AniStart(new ModAnimation.AniData[3]
				{
					ModAnimation.AaOpacity(LabRamUsed, 1.0 - LabRamUsed.Opacity, 100),
					ModAnimation.AaOpacity(LabRamTotal, 1.0 - LabRamTotal.Opacity, 100),
					ModAnimation.AaOpacity(LabRamUsedTitle, 0.7 - LabRamUsedTitle.Opacity, 100)
				}, "VersionSetup Ram TextLeft");
				break;
			}
		}
		int num2 = ((!(actualWidth2 < actualWidth3 + 2.0 + actualWidth) && !(actualWidth2 < actualWidth6 + 2.0 + actualWidth)) ? 1 : 0);
		if (num2 == 0)
		{
			if (ModAnimation.AniControlEnabled == 0 && (RamTextRight != num2 || ModAnimation.AniIsRun("VersionSetup Ram TextRight")))
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaX(LabRamGame, actualWidth2 - actualWidth3 - LabRamGame.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaX(LabRamGameTitle, actualWidth2 - actualWidth6 - LabRamGameTitle.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak))
				}, "VersionSetup Ram TextRight");
			}
			else
			{
				LabRamGame.Margin = new Thickness(actualWidth2 - actualWidth3, 3.0, 0.0, 0.0);
				LabRamGameTitle.Margin = new Thickness(actualWidth2 - actualWidth6, 0.0, 0.0, 5.0);
			}
		}
		else if (ModAnimation.AniControlEnabled == 0 && (RamTextRight != num2 || ModAnimation.AniIsRun("VersionSetup Ram TextRight")))
		{
			ModAnimation.AniStart(new ModAnimation.AniData[2]
			{
				ModAnimation.AaX(LabRamGame, 2.0 + actualWidth - LabRamGame.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaX(LabRamGameTitle, 2.0 + actualWidth - LabRamGameTitle.Margin.Left, 100, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak))
			}, "VersionSetup Ram TextRight");
		}
		else
		{
			LabRamGame.Margin = new Thickness(2.0 + actualWidth, 3.0, 0.0, 0.0);
			LabRamGameTitle.Margin = new Thickness(2.0 + actualWidth, 0.0, 0.0, 5.0);
		}
		RamTextRight = num2;
	}

	public static double GetRam(ModMinecraft.McInstance Instance)
	{
		if (Settings.Get<int>("VersionRamType", Instance) == 2)
		{
			return PageSetupLaunch.GetRam(Instance, UseVersionJavaSetup: true);
		}
		double num7 = default(double);
		if (Settings.Get<int>("VersionRamType", Instance) == 0)
		{
			double num = Math.Round((double)MyWpfExtension.Computer.Info.AvailablePhysicalMemory / 1024.0 / 1024.0 / 1024.0 * 10.0) / 10.0;
			if ((object)Instance != null && !Instance.IsLoaded)
			{
				Instance.Load();
			}
			double val;
			double num3;
			double num4;
			double num5;
			if ((object)Instance != null && Instance.Modable)
			{
				DirectoryInfo info = DirectoryUtils.GetInfo(Instance.PathIndie + "mods\\");
				int num2 = (info.Exists ? info.GetFiles().Count([SpecialName] (FileInfo f) => new string[3] { ".jar", ".zip", ".litemod" }.Contains(StringExtensions.Lower(f.Extension))) : 0);
				val = 0.5 + (double)num2 / 150.0;
				num3 = 1.5 + (double)num2 / 90.0;
				num4 = 2.7 + (double)num2 / 50.0;
				num5 = 4.5 + (double)num2 / 25.0;
			}
			else if ((object)Instance != null && Instance.Version.HasOptiFine)
			{
				val = 0.5;
				num3 = 1.5;
				num4 = 3.0;
				num5 = 5.0;
			}
			else
			{
				val = 0.5;
				num3 = 1.5;
				num4 = 2.5;
				num5 = 4.0;
			}
			double num6 = num3;
			num7 += Math.Min(num, num6);
			num -= num6;
			if (!(num < 0.1))
			{
				num6 = num4 - num3;
				num7 += Math.Min(num * 0.7, num6);
				num -= num6 / 0.7;
				if (!(num < 0.1))
				{
					num6 = num5 - num4;
					num7 += Math.Min(num * 0.4, num6);
					num -= num6 / 0.4;
					if (!(num < 0.1))
					{
						num6 = num5;
						num7 += Math.Min(num * 0.15, num6);
						num -= num6 / 0.15;
						_ = 0.1;
					}
				}
			}
			num7 = Math.Round(Math.Max(num7, val), 1);
		}
		else
		{
			int num8 = Settings.Get<int>("VersionRamCustom", Instance);
			num7 = ((num8 <= 12) ? ((double)num8 * 0.1 + 0.3) : checked((num8 <= 25) ? ((double)(num8 - 12) * 0.5 + 1.5) : ((num8 > 33) ? ((double)((num8 - 33) * 2 + 16)) : ((double)((num8 - 25) * 1 + 8)))));
		}
		return num7;
	}

	public static void OnVersionServerLoginChanged(int Type)
	{
		if (ModMain.FrmInstanceSetup != null)
		{
			ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
			if ((object)PageInstanceLeft.Instance != null)
			{
				PageInstanceLeft.Instance = new ModMinecraft.McInstance(PageInstanceLeft.Instance.Name).Load();
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			}
		}
	}

	private void TextServerEnter_Change()
	{
		string text = TextServerEnter.Text.Replace("：", ":").Replace("。", ".");
		if (Operators.CompareString(text, TextServerEnter.Text, TextCompare: false) != 0)
		{
			int selectionStart = TextServerEnter.SelectionStart;
			TextServerEnter.Text = text;
			TextServerEnter.SelectionStart = selectionStart;
		}
	}

	private void ComboServerLogin_Changed()
	{
		if (ModAnimation.AniControlEnabled == 0)
		{
			UpdateServerLoginUI();
			if ((ComboServerLogin.SelectedIndex != 3 || TextServerNide.IsValidated) && (ComboServerLogin.SelectedIndex != 4 || TextServerAuthServer.IsValidated) && ComboServerLoginLast != ComboServerLogin.SelectedIndex)
			{
				ComboServerLoginLast = ComboServerLogin.SelectedIndex;
				Settings.Set("VersionServerLogin", ComboServerLogin.SelectedIndex, PageInstanceLeft.Instance);
			}
		}
	}

	private void UpdateServerLoginUI()
	{
		if (LabServerNide != null)
		{
			int selectedIndex = ComboServerLogin.SelectedIndex;
			LabServerNide.Visibility = ((selectedIndex != 3) ? Visibility.Collapsed : Visibility.Visible);
			TextServerNide.Visibility = ((selectedIndex != 3) ? Visibility.Collapsed : Visibility.Visible);
			PanServerNide.Visibility = ((selectedIndex != 3) ? Visibility.Collapsed : Visibility.Visible);
			LabServerAuthName.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
			TextServerAuthName.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
			LabServerAuthRegister.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
			TextServerAuthRegister.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
			LabServerAuthServer.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
			TextServerAuthServer.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
			BtnServerAuthLittle.Visibility = ((selectedIndex != 4) ? Visibility.Collapsed : Visibility.Visible);
			CardServer.TriggerForceResize();
		}
	}

	private void BtnServerAuthLittle_Click(object sender, EventArgs e)
	{
		if (Operators.CompareString(TextServerAuthServer.Text, "", TextCompare: false) == 0 || Operators.CompareString(TextServerAuthServer.Text, "https://littleskin.cn/api/yggdrasil", TextCompare: false) == 0 || ModMain.MyMsgBox("即将把第三方登录设置覆盖为 LittleSkin 登录。\r\n除非你是服主，或者服主要求你这样做，否则请不要继续。\r\n\r\n是否确实需要覆盖当前设置？", "设置覆盖确认", "继续", "取消") != 2)
		{
			TextServerAuthServer.Text = "https://littleskin.cn/api/yggdrasil";
			TextServerAuthRegister.Text = "https://littleskin.cn/auth/register";
			TextServerAuthName.Text = "LittleSkin 登录";
		}
	}

	private void ReloadCurrentJava()
	{
		PageInstanceLeft.ReloadCurrentJava();
	}

	private void UpdateJavaRelatedUi()
	{
		ModBase.RunInUi(_UpdateJavaRelatedUi);
	}

	private void _UpdateJavaRelatedUi()
	{
		RedoableWorker<Java> currentJavaWorker = PageInstanceLeft.CurrentJavaWorker;
		switch (ComboArgumentJava.SelectedIndex)
		{
		case 0:
		case 2:
			HintArgumentJava.Visibility = Visibility.Visible;
			TextArgumentJavaRange.Visibility = Visibility.Collapsed;
			PanArgumentJavaSelect.Visibility = Visibility.Collapsed;
			if (!((RedoableWorkerBase<Java>)(object)currentJavaWorker).LastSucceeded || ((RedoableWorkerBase<Java>)(object)currentJavaWorker).Running)
			{
				if (!((RedoableWorkerBase<Java>)(object)currentJavaWorker).Running)
				{
					((RedoableWorkerBase<Java>)(object)currentJavaWorker).Start(default(CancellationToken), (ProgressProvider)null);
				}
				HintArgumentJava.Text = "正在查找 Java……";
				HintArgumentJava.Theme = MyHint.Themes.Blue;
			}
			else if (((RedoableWorkerBase<Java>)(object)currentJavaWorker).LastResult == null)
			{
				if (ComboArgumentJava.SelectedIndex == 0)
				{
					HintArgumentJava.Text = "你的电脑上没有可供该版本使用的 Java，PCL 会在启动游戏时自动下载。";
					HintArgumentJava.Theme = MyHint.Themes.Yellow;
				}
				else
				{
					HintArgumentJava.Text = string.Format("该版本的版本文件夹中没有发现任何 Java！{0}请点击此处打开版本文件夹，然后将 Java 文件夹复制进去。", "\r\n");
					HintArgumentJava.Theme = MyHint.Themes.Red;
				}
			}
			else
			{
				HintArgumentJava.Text = $"将会使用：{((RedoableWorkerBase<Java>)(object)currentJavaWorker).LastResult}";
				HintArgumentJava.Theme = MyHint.Themes.Blue;
			}
			break;
		case 1:
			HintArgumentJava.Visibility = Visibility.Visible;
			TextArgumentJavaRange.Visibility = Visibility.Visible;
			PanArgumentJavaSelect.Visibility = Visibility.Collapsed;
			if (!Settings.HasSaved("VersionArgumentJavaRange", PageInstanceLeft.Instance))
			{
				string text = ModJava.GetJavaRequirement(PageInstanceLeft.Instance).Range.ToString().Replace("-∞", "").Replace("+∞", "");
				Settings.Set("VersionArgumentJavaRange", text, PageInstanceLeft.Instance);
				TextArgumentJavaRange.Text = text;
			}
			if (!string.IsNullOrEmpty(TextArgumentJavaRange.ValidateResult))
			{
				HintArgumentJava.Text = $"Java 版本范围错误：{TextArgumentJavaRange.ValidateResult}";
				HintArgumentJava.Theme = MyHint.Themes.Red;
			}
			else if (!((RedoableWorkerBase<Java>)(object)currentJavaWorker).LastSucceeded || ((RedoableWorkerBase<Java>)(object)currentJavaWorker).Running)
			{
				if (!((RedoableWorkerBase<Java>)(object)currentJavaWorker).Running)
				{
					((RedoableWorkerBase<Java>)(object)currentJavaWorker).Start(default(CancellationToken), (ProgressProvider)null);
				}
				HintArgumentJava.Text = "正在查找 Java……";
				HintArgumentJava.Theme = MyHint.Themes.Blue;
			}
			else if (((RedoableWorkerBase<Java>)(object)currentJavaWorker).LastResult == null)
			{
				HintArgumentJava.Text = string.Format("你的电脑上没有任何 Java 符合该版本范围！{0}如果 Mojang 提供了对应版本的 Java，PCL 会在启动游戏时自动下载。", "\r\n");
				HintArgumentJava.Theme = MyHint.Themes.Yellow;
			}
			else
			{
				HintArgumentJava.Text = $"将会使用：{((RedoableWorkerBase<Java>)(object)currentJavaWorker).LastResult}";
				HintArgumentJava.Theme = MyHint.Themes.Blue;
			}
			break;
		case 3:
			HintArgumentJava.Visibility = Visibility.Collapsed;
			TextArgumentJavaRange.Visibility = Visibility.Collapsed;
			PanArgumentJavaSelect.Visibility = Visibility.Visible;
			break;
		}
	}

	private void HintArgumentJava_Click(object sender, MouseButtonEventArgs e)
	{
		if (ComboArgumentJava.SelectedIndex == 2)
		{
			PageInstanceOverall.OpenInstanceFolder(PageInstanceLeft.Instance);
		}
	}

	public void UpdateJavaList()
	{
		if (ComboArgumentJavaSelect == null || LabArgumentJava == null || !ComboArgumentJavaSelect.IsLoaded)
		{
			return;
		}
		ComboArgumentJavaSelect.Items.Clear();
		if (((RedoableWorkerBase<NoType>)(object)ModJava.JavaListRefreshWorker).Running)
		{
			BtnArgumentJavaSearch.IsEnabled = false;
			ComboArgumentJavaSelect.IsEnabled = false;
			LabArgumentJava.Text = "搜索中\u200a…";
			return;
		}
		BtnArgumentJavaSearch.IsEnabled = true;
		ComboArgumentJavaSelect.IsEnabled = true;
		Java val = Configs.JavaForced.Get((IConfigProvider)(object)PageInstanceLeft.Instance.Config);
		if (!((IEnumerable<Java>)Configs.JavaList.Get()).Any())
		{
			LabArgumentJava.Text = "未找到 Java，点击以导入已有的 Java";
		}
		else if (val == null)
		{
			LabArgumentJava.Text = "点击选择 Java\u200a…";
		}
		else
		{
			LabArgumentJava.Text = val.ToString();
		}
		try
		{
			using (IEnumerator<Java> enumerator = Configs.JavaList.Get().GetEnumerator())
			{
				_Closure_0024__23_002D0 closure_0024__23_002D = default(_Closure_0024__23_002D0);
				while (enumerator.MoveNext())
				{
					closure_0024__23_002D = new _Closure_0024__23_002D0(closure_0024__23_002D);
					closure_0024__23_002D._0024VB_0024Me = this;
					closure_0024__23_002D._0024VB_0024Local_JavaEntry = enumerator.Current;
					MyListItem myListItem = new MyListItem
					{
						FontSize = 13.0,
						Height = 24.0,
						IsScaleAnimationEnabled = false,
						Type = MyListItem.CheckType.Clickable,
						Tag = closure_0024__23_002D._0024VB_0024Local_JavaEntry,
						Title = closure_0024__23_002D._0024VB_0024Local_JavaEntry.ToString()
					};
					myListItem.MouseLeftButtonUp += ((_Closure_0024__._0024I23_002D0 == null) ? (_Closure_0024__._0024I23_002D0 = [SpecialName] (object sender, MouseButtonEventArgs e) =>
					{
						e.Handled = true;
					}) : _Closure_0024__._0024I23_002D0);
					myListItem.Click += closure_0024__23_002D._Lambda_0024__R2;
					ComboArgumentJavaSelect.Items.Add(myListItem);
					List<MyIconButton> list = new List<MyIconButton>();
					bool flag = PathUtils.IsParentOf($"{Paths.AppData}.minecraft\\runtime\\", closure_0024__23_002D._0024VB_0024Local_JavaEntry.Folder);
					MyIconButton myIconButton = new MyIconButton
					{
						Logo = "M508 990.4c-261.6 0-474.4-212-474.4-474.4S246.4 41.6 508 41.6s474.4 212 474.4 474.4S769.6 990.4 508 990.4zM508 136.8c-209.6 0-379.2 169.6-379.2 379.2 0 209.6 169.6 379.2 379.2 379.2s379.2-169.6 379.2-379.2C887.2 306.4 717.6 136.8 508 136.8zM697.6 563.2 318.4 563.2c-26.4 0-47.2-21.6-47.2-47.2 0-26.4 21.6-47.2 47.2-47.2l379.2 0c26.4 0 47.2 21.6 47.2 47.2C744.8 542.4 724 563.2 697.6 563.2z",
						Height = 24.0,
						Width = 24.0
					};
					myIconButton.IsEnabled = !flag;
					myIconButton.ToolTip = (myIconButton.IsEnabled ? "从列表中移除" : "无法移除官方 Java");
					myIconButton.Click += closure_0024__23_002D._Lambda_0024__R3;
					ToolTipService.SetShowOnDisabled(myIconButton, value: true);
					list.Add(myIconButton);
					MyIconButton myIconButton2 = new MyIconButton
					{
						Logo = "M889.018182 418.909091H884.363636V316.509091a93.090909 93.090909 0 0 0-99.607272-89.832727h-302.545455l-93.090909-76.334546A46.545455 46.545455 0 0 0 358.865455 139.636364H146.152727A93.090909 93.090909 0 0 0 46.545455 229.469091V837.818182a46.545455 46.545455 0 0 0 46.545454 46.545454 46.545455 46.545455 0 0 0 16.756364-3.258181 109.381818 109.381818 0 0 0 25.134545 3.258181h586.472727a85.178182 85.178182 0 0 0 87.04-63.301818l163.374546-302.545454a46.545455 46.545455 0 0 0 5.585454-21.876364A82.385455 82.385455 0 0 0 889.018182 418.909091z m-744.727273-186.181818h198.283636l93.09091 76.334545a46.545455 46.545455 0 0 0 29.323636 10.705455h319.301818a12.101818 12.101818 0 0 1 6.516364 0V418.909091H302.545455a85.178182 85.178182 0 0 0-87.04 63.301818L139.636364 622.778182V232.727273a19.549091 19.549091 0 0 1 6.516363 0z m578.094546 552.029091a27.461818 27.461818 0 0 0-2.792728 6.516363H154.530909l147.083636-272.290909a27.461818 27.461818 0 0 0 2.792728-6.981818h565.061818z",
						LogoScale = 1.1,
						Height = 24.0,
						Width = 24.0
					};
					myIconButton2.ToolTip = "打开文件夹";
					myIconButton2.Click += closure_0024__23_002D._Lambda_0024__R4;
					list.Add(myIconButton2);
					myListItem.Buttons = list;
				}
			}
			MyListItem myListItem2 = new MyListItem
			{
				FontSize = 13.0,
				Height = 24.0,
				IsScaleAnimationEnabled = false,
				Title = "\u200a导入电脑中已有的 Java…",
				Type = MyListItem.CheckType.Clickable
			};
			myListItem2.MouseLeftButtonUp += [SpecialName] (object sender, MouseButtonEventArgs e) =>
			{
				e.Handled = true;
				ModJava.ManuallyImportJava();
			};
			ComboArgumentJavaSelect.Items.Add(myListItem2);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "更新 Java 下拉框失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceSetup.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void BtnAdvanceJavaSearch_Click()
	{
		ModJava.ManuallySearchJava();
	}

	private void ComboArgumentIndieV2_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (ModAnimation.AniControlEnabled != 0)
		{
			return;
		}
		if (_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting_0024Init == null)
		{
			Interlocked.CompareExchange(ref _0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting_0024Init, new StaticLocalInitFlag(), null);
		}
		bool lockTaken = false;
		try
		{
			Monitor.Enter(_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting_0024Init, ref lockTaken);
			if (_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting_0024Init.State == 0)
			{
				_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting_0024Init.State = 2;
				_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting = false;
			}
			else if (_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting_0024Init.State == 2)
			{
				throw new IncompleteInitialization();
			}
		}
		finally
		{
			_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting_0024Init.State = 1;
			if (lockTaken)
			{
				Monitor.Exit(_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting_0024Init);
			}
		}
		if (_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting)
		{
			return;
		}
		if (ModMain.MyMsgBox("调整版本隔离设置后，你需要游戏存档、Mod 等文件手动迁移到新的游戏文件夹中。\r\n如果发现存档消失，把这项设置改回来就能恢复。\r\n如果你不会迁移存档，不建议修改这项设置！", "警告", "我知道我在做什么", "取消", "", IsWarn: true) == 2)
		{
			_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting = true;
			if (e.RemovedItems.Count > 0)
			{
				ComboArgumentIndieV2.SelectedItem = RuntimeHelpers.GetObjectValue(e.RemovedItems[0]);
			}
			_0024STATIC_0024ComboArgumentIndieV2_SelectionChanged_002420211C128135_0024IsReverting = false;
		}
		Settings.Set("VersionArgumentIndieV2", RuntimeHelpers.GetObjectValue(Operators.CompareObjectEqual(NewLateBinding.LateGet(sender, null, "SelectedIndex", new object[0], null, null, null), 0, TextCompare: false)), PageInstanceLeft.Instance);
	}

	private void TextAdvanceRun_TextChanged(object sender, TextChangedEventArgs e)
	{
		CheckAdvanceRunWait.Visibility = ModBase.ToVisibility(Operators.CompareString(TextAdvanceRun.Text, "", TextCompare: false) != 0);
	}

	private void BtnSwitch_Click(object sender, MouseButtonEventArgs e)
	{
		ModMain.FrmMain.PageChange(FormMain.PageType.Setup);
	}

	private void ReplaceEnter(MyTextBox sender, TextChangedEventArgs e)
	{
		string text = StringExtensions.ReplaceLineEndings(sender.Text, " ", true);
		if (Operators.CompareString(text, sender.Text, TextCompare: false) != 0)
		{
			int caretIndex = sender.CaretIndex;
			sender.Text = text;
			sender.CaretIndex = Math.Max(0, checked(caretIndex - 1));
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageinstance/pageinstancesetup.xaml", UriKind.Relative);
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
			CardArgument = (MyCard)target;
			break;
		case 4:
			ComboArgumentIndieV2 = (MyComboBox)target;
			break;
		case 5:
			TextArgumentTitle = (MyTextBox)target;
			break;
		case 6:
			TextArgumentInfo = (MyTextBox)target;
			break;
		case 7:
			ComboArgumentJava = (MyComboBox)target;
			break;
		case 8:
			TextArgumentJavaRange = (MyTextBox)target;
			break;
		case 9:
			HintArgumentJava = (MyHint)target;
			break;
		case 10:
			PanArgumentJavaSelect = (Grid)target;
			break;
		case 11:
			ComboArgumentJavaSelect = (MyComboBox)target;
			break;
		case 12:
			LabArgumentJava = (TextBlock)target;
			break;
		case 13:
			BtnArgumentJavaSearch = (MyIconButton)target;
			break;
		case 14:
			RadioRamType2 = (MyRadioBox)target;
			break;
		case 15:
			RadioRamType0 = (MyRadioBox)target;
			break;
		case 16:
			RadioRamType1 = (MyRadioBox)target;
			break;
		case 17:
			SliderRamCustom = (MySlider)target;
			break;
		case 18:
			ComboRamOptimize = (MyComboBox)target;
			break;
		case 19:
			PanRamDisplay = (Grid)target;
			break;
		case 20:
			ColumnRamUsed = (ColumnDefinition)target;
			break;
		case 21:
			ColumnRamGame = (ColumnDefinition)target;
			break;
		case 22:
			ColumnRamEmpty = (ColumnDefinition)target;
			break;
		case 23:
			RectRamUsed = (Rectangle)target;
			break;
		case 24:
			RectRamGame = (Rectangle)target;
			break;
		case 25:
			RectRamEmpty = (Rectangle)target;
			break;
		case 26:
			LabRamUsedTitle = (TextBlock)target;
			break;
		case 27:
			LabRamGameTitle = (TextBlock)target;
			break;
		case 28:
			LabRamUsed = (TextBlock)target;
			break;
		case 29:
			LabRamTotal = (TextBlock)target;
			break;
		case 30:
			LabRamGame = (TextBlock)target;
			break;
		case 31:
			CardServer = (MyCard)target;
			break;
		case 32:
			ComboServerLogin = (MyComboBox)target;
			break;
		case 33:
			LabServerNide = (TextBlock)target;
			break;
		case 34:
			TextServerNide = (MyTextBox)target;
			break;
		case 35:
			LabServerAuthServer = (TextBlock)target;
			break;
		case 36:
			TextServerAuthServer = (MyTextBox)target;
			break;
		case 37:
			LabServerAuthRegister = (TextBlock)target;
			break;
		case 38:
			TextServerAuthRegister = (MyTextBox)target;
			break;
		case 39:
			LabServerAuthName = (TextBlock)target;
			break;
		case 40:
			TextServerAuthName = (MyTextBox)target;
			break;
		case 41:
			TextServerEnter = (MyTextBox)target;
			break;
		case 42:
			PanServerNide = (Grid)target;
			break;
		case 43:
			BtnServerAuthLittle = (MyButton)target;
			break;
		case 44:
			CardAdvance = (MyCard)target;
			break;
		case 45:
			TextAdvanceJvm = (MyTextBox)target;
			break;
		case 46:
			TextAdvanceGame = (MyTextBox)target;
			break;
		case 47:
			TextAdvanceRun = (MyTextBox)target;
			break;
		case 48:
			CheckAdvanceRunWait = (MyCheckBox)target;
			break;
		case 49:
			CheckAdvanceDisableModUpdate = (MyCheckBox)target;
			break;
		case 50:
			CheckAdvanceAssetsV2 = (MyCheckBox)target;
			break;
		case 51:
			CheckAdvanceDisableJLW = (MyCheckBox)target;
			break;
		case 52:
			BtnSwitch = (MyExtraTextButton)target;
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
