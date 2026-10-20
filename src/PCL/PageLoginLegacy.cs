using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageLoginLegacy : Grid, IComponentConnector
{
	public bool IsReloaded;

	[CompilerGenerated]
	[AccessedThroughProperty("Skin")]
	private MySkin _Skin;

	[CompilerGenerated]
	[AccessedThroughProperty("ComboName")]
	private MyComboBox _ComboName;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("PanMain")]
	internal virtual StackPanel PanMain
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("HintChinese")]
	internal virtual MyHint HintChinese
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MySkin Skin
	{
		[CompilerGenerated]
		get
		{
			return _Skin;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MySkin.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Skin_Click();
			};
			MySkin skin = _Skin;
			if (skin != null)
			{
				skin.Click -= obj;
			}
			_Skin = value;
			skin = _Skin;
			if (skin != null)
			{
				skin.Click += obj;
			}
		}
	}

	internal virtual MyComboBox ComboName
	{
		[CompilerGenerated]
		get
		{
			return _ComboName;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = ComboName_PreviewKeyDown;
			MyComboBox.TextChangedEventHandler obj = ComboLegacy_TextChanged;
			MyComboBox comboName = _ComboName;
			if (comboName != null)
			{
				comboName.PreviewKeyDown -= value2;
				comboName.TextChanged -= obj;
			}
			_ComboName = value;
			comboName = _ComboName;
			if (comboName != null)
			{
				comboName.PreviewKeyDown += value2;
				comboName.TextChanged += obj;
			}
		}
	}

	public PageLoginLegacy()
	{
		base.Loaded += PageLoginLegacy_Loaded;
		IsReloaded = false;
		InitializeComponent();
		Skin.Loader = PageLaunchLeft.SkinLegacy;
	}

	private void PageLoginLegacy_Loaded(object sender, RoutedEventArgs e)
	{
		Skin.Loader.Start();
	}

	public void Reload(bool KeepInput)
	{
		if (KeepInput && IsReloaded)
		{
			string text = ComboName.Text.Trim();
			ComboName.ItemsSource = ((Operators.CompareString(Settings.Get<string>("LoginLegacyName"), "", TextCompare: false) == 0) ? null : StringExtensions.Split(Settings.Get<string>("LoginLegacyName"), "\u00a8", false));
			ComboName.Text = text;
		}
		else if (Operators.CompareString(Settings.Get<string>("LoginLegacyName"), "", TextCompare: false) == 0)
		{
			ComboName.ItemsSource = null;
		}
		else
		{
			ComboName.ItemsSource = StringExtensions.Split(Settings.Get<string>("LoginLegacyName").ToString(), "\u00a8", false);
			ComboName.Text = StringExtensions.BeforeFirst(Settings.Get<string>("LoginLegacyName").ToString(), "\u00a8", false).Trim();
		}
		IsReloaded = true;
	}

	public static ModLaunch.McLoginData GetLoginData()
	{
		string userName = ((ModMain.FrmLoginLegacy == null) ? "" : ModMain.FrmLoginLegacy.ComboName.Text.Replace("\u00a8", "").Trim());
		return new ModLaunch.McLoginLegacy
		{
			UserName = userName,
			SkinType = Settings.Get<int>("LaunchSkinType"),
			SkinName = Settings.Get<string>("LaunchSkinID")
		};
	}

	public static string IsVaild(ModLaunch.McLoginLegacy LoginData)
	{
		if (Operators.CompareString(LoginData.UserName.Trim(), "", TextCompare: false) == 0)
		{
			return "玩家名不能为空！";
		}
		if (LoginData.UserName.Contains("\""))
		{
			return "玩家名不能包含英文引号！";
		}
		if ((object)ModMinecraft.McInstanceSelected != null && ModMinecraft.McInstanceSelected.Version.Valid && ModMinecraft.McInstanceSelected.Version.Vanilla >= new Version(20, 0, 3) && LoginData.UserName.Trim().Length > 16)
		{
			return "自 1.20.3 起，玩家名至多只能包含 16 个字符！";
		}
		return "";
	}

	public string IsVaild()
	{
		return IsVaild((ModLaunch.McLoginLegacy)GetLoginData());
	}

	private void ComboName_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Space)
		{
			int caretIndex = ((MyTextBox)ComboName.Template.FindName("PART_EditableTextBox", ComboName)).CaretIndex;
			if (caretIndex == ComboName.Text.Length || caretIndex == 0)
			{
				e.Handled = true;
			}
		}
	}

	private void ComboLegacy_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (Settings.Get<int>("LaunchSkinType") == 0)
		{
			PageLaunchLeft.SkinLegacy.Start(null, IsForceRestart: true);
		}
		HintChinese.Visibility = (StringExtensions.RegexCheck(ComboName.Text, "^[0-9A-Za-z_]*$", RegexOptions.None) ? Visibility.Collapsed : Visibility.Visible);
		ModBase.RunInUi([SpecialName] () =>
		{
			PanMain.InvalidateMeasure();
		}, ForceWaitUntilLoaded: true);
	}

	private void Skin_Click()
	{
		if ((Settings.Get<bool>("UiHiddenPageSetup") || Settings.Get<bool>("UiHiddenSetupLaunch")) && !PageSetupUI.HiddenForceShow)
		{
			ModMain.Hint("启动设置已被禁用！", ModMain.HintType.Red);
		}
		else
		{
			ModMain.FrmMain.PageChange(FormMain.PageType.Setup);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginlegacy.xaml", UriKind.Relative);
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
			PanMain = (StackPanel)target;
			break;
		case 2:
			HintChinese = (MyHint)target;
			break;
		case 3:
			Skin = (MySkin)target;
			break;
		case 4:
			ComboName = (MyComboBox)target;
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
