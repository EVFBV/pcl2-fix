using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageSetupSystem : MyPageRight, IComponentConnector
{
	[CompilerGenerated]
	[AccessedThroughProperty("SliderDownloadThread")]
	private MySlider _SliderDownloadThread;

	[CompilerGenerated]
	[AccessedThroughProperty("ComboSystemUpdate")]
	private MyComboBox _ComboSystemUpdate;

	[CompilerGenerated]
	[AccessedThroughProperty("ComboSystemActivity")]
	private MyComboBox _ComboSystemActivity;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnSystemUpdate")]
	private MyButton _BtnSystemUpdate;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnSystemSettingExp")]
	private MyButton _BtnSystemSettingExp;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnSystemSettingImp")]
	private MyButton _BtnSystemSettingImp;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnSystemIdentify")]
	private MyButton _BtnSystemIdentify;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnSystemUnlock")]
	private MyButton _BtnSystemUnlock;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckDebugMode")]
	private MyCheckBox _CheckDebugMode;

	private bool _contentLoaded;

	[SpecialName]
	private bool _0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded_0024Init;

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

	[field: AccessedThroughProperty("ComboDownloadSource")]
	internal virtual MyComboBox ComboDownloadSource
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboDownloadVersion")]
	internal virtual MyComboBox ComboDownloadVersion
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MySlider SliderDownloadThread
	{
		[CompilerGenerated]
		get
		{
			return _SliderDownloadThread;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MySlider.PreviewChangeEventHandler obj = SliderDownloadThread_PreviewChange;
			MySlider sliderDownloadThread = _SliderDownloadThread;
			if (sliderDownloadThread != null)
			{
				sliderDownloadThread.PreviewChange -= obj;
			}
			_SliderDownloadThread = value;
			sliderDownloadThread = _SliderDownloadThread;
			if (sliderDownloadThread != null)
			{
				sliderDownloadThread.PreviewChange += obj;
			}
		}
	}

	[field: AccessedThroughProperty("SliderDownloadSpeed")]
	internal virtual MySlider SliderDownloadSpeed
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboDownloadMod")]
	internal virtual MyComboBox ComboDownloadMod
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboDownloadTranslateV2")]
	internal virtual MyComboBox ComboDownloadTranslateV2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboModLocalNameStyle")]
	internal virtual MyComboBox ComboModLocalNameStyle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckUpdateRelease")]
	internal virtual MyCheckBox CheckUpdateRelease
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckUpdateSnapshot")]
	internal virtual MyCheckBox CheckUpdateSnapshot
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckHelpChinese")]
	internal virtual MyCheckBox CheckHelpChinese
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyComboBox ComboSystemUpdate
	{
		[CompilerGenerated]
		get
		{
			return _ComboSystemUpdate;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SelectionChangedEventHandler value2 = ComboSystemUpdate_SelectionChanged;
			MyComboBox comboSystemUpdate = _ComboSystemUpdate;
			if (comboSystemUpdate != null)
			{
				comboSystemUpdate.SelectionChanged -= value2;
			}
			_ComboSystemUpdate = value;
			comboSystemUpdate = _ComboSystemUpdate;
			if (comboSystemUpdate != null)
			{
				comboSystemUpdate.SelectionChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ItemSystemUpdateDownload")]
	internal virtual MyComboBoxItem ItemSystemUpdateDownload
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyComboBox ComboSystemActivity
	{
		[CompilerGenerated]
		get
		{
			return _ComboSystemActivity;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SelectionChangedEventHandler value2 = ComboSystemActivity_SelectionChanged;
			MyComboBox comboSystemActivity = _ComboSystemActivity;
			if (comboSystemActivity != null)
			{
				comboSystemActivity.SelectionChanged -= value2;
			}
			_ComboSystemActivity = value;
			comboSystemActivity = _ComboSystemActivity;
			if (comboSystemActivity != null)
			{
				comboSystemActivity.SelectionChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TextSystemCache")]
	internal virtual MyTextBox TextSystemCache
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckSystemTelemetry")]
	internal virtual MyCheckBox CheckSystemTelemetry
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnSystemUpdate
	{
		[CompilerGenerated]
		get
		{
			return _BtnSystemUpdate;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnSystemUpdate_Click;
			MyButton btnSystemUpdate = _BtnSystemUpdate;
			if (btnSystemUpdate != null)
			{
				btnSystemUpdate.Click -= obj;
			}
			_BtnSystemUpdate = value;
			btnSystemUpdate = _BtnSystemUpdate;
			if (btnSystemUpdate != null)
			{
				btnSystemUpdate.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnSystemSettingExp
	{
		[CompilerGenerated]
		get
		{
			return _BtnSystemSettingExp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnSystemSettingExp_Click;
			MyButton btnSystemSettingExp = _BtnSystemSettingExp;
			if (btnSystemSettingExp != null)
			{
				btnSystemSettingExp.Click -= obj;
			}
			_BtnSystemSettingExp = value;
			btnSystemSettingExp = _BtnSystemSettingExp;
			if (btnSystemSettingExp != null)
			{
				btnSystemSettingExp.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnSystemSettingImp
	{
		[CompilerGenerated]
		get
		{
			return _BtnSystemSettingImp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnSystemSettingImp_Click;
			MyButton btnSystemSettingImp = _BtnSystemSettingImp;
			if (btnSystemSettingImp != null)
			{
				btnSystemSettingImp.Click -= obj;
			}
			_BtnSystemSettingImp = value;
			btnSystemSettingImp = _BtnSystemSettingImp;
			if (btnSystemSettingImp != null)
			{
				btnSystemSettingImp.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanDonate")]
	internal virtual Grid PanDonate
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnSystemIdentify
	{
		[CompilerGenerated]
		get
		{
			return _BtnSystemIdentify;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnSystemIdentify_Click;
			MyButton btnSystemIdentify = _BtnSystemIdentify;
			if (btnSystemIdentify != null)
			{
				btnSystemIdentify.Click -= obj;
			}
			_BtnSystemIdentify = value;
			btnSystemIdentify = _BtnSystemIdentify;
			if (btnSystemIdentify != null)
			{
				btnSystemIdentify.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnSystemUnlock
	{
		[CompilerGenerated]
		get
		{
			return _BtnSystemUnlock;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnSystemUnlock_Click;
			MyButton btnSystemUnlock = _BtnSystemUnlock;
			if (btnSystemUnlock != null)
			{
				btnSystemUnlock.Click -= obj;
			}
			_BtnSystemUnlock = value;
			btnSystemUnlock = _BtnSystemUnlock;
			if (btnSystemUnlock != null)
			{
				btnSystemUnlock.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("CardDebug")]
	internal virtual MyCard CardDebug
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanDebugAnim")]
	internal virtual Grid PanDebugAnim
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SliderDebugAnim")]
	internal virtual MySlider SliderDebugAnim
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CheckDebugSkipCopy")]
	internal virtual MyCheckBox CheckDebugSkipCopy
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCheckBox CheckDebugMode
	{
		[CompilerGenerated]
		get
		{
			return _CheckDebugMode;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				CheckDebugMode_Change();
			};
			MyCheckBox checkDebugMode = _CheckDebugMode;
			if (checkDebugMode != null)
			{
				checkDebugMode.Change -= obj;
			}
			_CheckDebugMode = value;
			checkDebugMode = _CheckDebugMode;
			if (checkDebugMode != null)
			{
				checkDebugMode.Change += obj;
			}
		}
	}

	[field: AccessedThroughProperty("CheckDebugDelay")]
	internal virtual MyCheckBox CheckDebugDelay
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public PageSetupSystem()
	{
		base.Loaded += PageSetupSystem_Loaded;
		InitializeComponent();
	}

	private void PageSetupSystem_Loaded(object sender, RoutedEventArgs e)
	{
		PanBack.ScrollToHome();
		PanDonate.Visibility = Visibility.Visible;
		ItemSystemUpdateDownload.Content = "在有新版本时自动下载（更新快照版前需要先输入土豆码）";
		if (_0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded_0024Init == null)
		{
			Interlocked.CompareExchange(ref _0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded_0024Init, new StaticLocalInitFlag(), null);
		}
		bool lockTaken = false;
		try
		{
			Monitor.Enter(_0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded_0024Init, ref lockTaken);
			if (_0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded_0024Init.State == 0)
			{
				_0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded_0024Init.State = 2;
				_0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded = false;
			}
			else if (_0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded_0024Init.State == 2)
			{
				throw new IncompleteInitialization();
			}
		}
		finally
		{
			_0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded_0024Init.State = 1;
			if (lockTaken)
			{
				Monitor.Exit(_0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded_0024Init);
			}
		}
		checked
		{
			if (!_0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded)
			{
				_0024STATIC_0024PageSetupSystem_Loaded_002420211C128141_0024IsLoaded = true;
				ModAnimation.AniControlEnabled++;
				Reload();
				SliderLoad();
				ModAnimation.AniControlEnabled--;
			}
		}
	}

	public void Reload()
	{
		SettingService.RefreshSettings(this);
	}

	public void Reset()
	{
		try
		{
			SettingService.ResetSettings(this);
			Logger.Info("已初始化启动器页设置", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupSystem.xaml.vb");
			ModMain.Hint("已初始化启动器页设置！", ModMain.HintType.Green, Log: false);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "初始化启动器页设置失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupSystem.xaml.vb");
			ProjectData.ClearProjectError();
		}
		Reload();
	}

	private void SliderLoad()
	{
		SliderDownloadThread.GetHintText = (VB_0024AnonymousDelegate_25<object, object>)([SpecialName] (object v) => Operators.AddObject(v, 1));
		SliderDownloadSpeed.GetHintText = (VB_0024AnonymousDelegate_25<object, object>)([SpecialName] (object v) =>
		{
			if (Operators.ConditionalCompareObjectLessEqual(v, 14, TextCompare: false))
			{
				return Operators.ConcatenateObject(Operators.MultiplyObject(Operators.AddObject(v, 1), 0.1), " M/s");
			}
			if (Operators.ConditionalCompareObjectLessEqual(v, 31, TextCompare: false))
			{
				return Operators.ConcatenateObject(Operators.MultiplyObject(Operators.SubtractObject(v, 11), 0.5), " M/s");
			}
			return Operators.ConditionalCompareObjectLessEqual(v, 41, TextCompare: false) ? Operators.ConcatenateObject(Operators.SubtractObject(v, 21), " M/s") : "无限制";
		});
		SliderDebugAnim.GetHintText = (VB_0024AnonymousDelegate_25<object, object>)([SpecialName] (object v) => (!Operators.ConditionalCompareObjectGreater(v, 29, TextCompare: false)) ? Operators.ConcatenateObject(Operators.AddObject(Operators.DivideObject(v, 10), 0.1), "x") : "关闭");
	}

	private void SliderDownloadThread_PreviewChange(object sender, ModBase.RouteEventArgs e)
	{
		if (SliderDownloadThread.Value >= 100 && !Settings.Get<bool>("HintDownloadThread"))
		{
			Settings.Set("HintDownloadThread", true);
			ModMain.MyMsgBox("如果设置过多的下载线程，可能会导致下载时出现非常严重的卡顿。\r\n一般设置 64 线程即可满足大多数下载需求，除非你知道你在干什么，否则不建议设置更多的线程数！", "警告", "我知道了", "", "", IsWarn: true);
		}
	}

	private void BtnSystemIdentify_Click(object sender, MouseButtonEventArgs e)
	{
		PageOtherAbout.CopyIdentify();
	}

	private void BtnSystemUnlock_Click(object sender, MouseButtonEventArgs e)
	{
		ModSecret.InputPotatoCode(IsUpdating: false);
	}

	private void CheckDebugMode_Change()
	{
		if (ModAnimation.AniControlEnabled == 0)
		{
			ModMain.Hint("部分调试信息将在刷新或启动器重启后切换显示！", ModMain.HintType.Blue, Log: false);
		}
	}

	private void ComboSystemActivity_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (ModAnimation.AniControlEnabled == 0 && ComboSystemActivity.SelectedIndex == 2 && ModMain.MyMsgBox("若选择此项，即使在将来出现严重问题时，你也无法获取相关通知。\r\n例如，如果发现某个版本游戏存在严重 Bug，你可能就会因为无法得到通知而导致无法预知的后果。\r\n\r\n一般选择 仅在有重要通知时显示公告 就可以让你尽量不受打扰了。\r\n除非你在制作服务器整合包，或时常手动更新启动器，否则极度不推荐选择此项！", "警告", "我知道我在做什么", "取消", "", IsWarn: true) == 2 && e.RemovedItems.Count > 0)
		{
			ComboSystemActivity.SelectedItem = RuntimeHelpers.GetObjectValue(e.RemovedItems[0]);
		}
	}

	private void ComboSystemUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (ModAnimation.AniControlEnabled == 0 && ComboSystemUpdate.SelectedIndex == 3 && ModMain.MyMsgBox("若选择此项，即使在启动器将来出现严重问题时，你也无法获取更新并获得修复。\r\n例如，如果官方修改了登录方式，从而导致现有启动器无法登录，你可能就会因为无法更新而无法开始游戏。\r\n\r\n一般选择 仅在有重大漏洞更新时显示提示 就可以让你尽量不受打扰了。\r\n除非你在制作服务器整合包，或时常手动更新启动器，否则极度不推荐选择此项！", "警告", "我知道我在做什么", "取消", "", IsWarn: true) == 2 && e.RemovedItems.Count > 0)
		{
			ComboSystemUpdate.SelectedItem = RuntimeHelpers.GetObjectValue(e.RemovedItems[0]);
		}
	}

	private void BtnSystemUpdate_Click(object sender, EventArgs e)
	{
		ModSecret.UpdateCheckByButton();
	}

	public static bool? IsLauncherNewest()
	{
		bool? result;
		try
		{
			string text = FileUtils.TryReadAsString(ModBase.PathTemp + "Cache\\Notice.cfg", (Encoding)null, (Type)null) ?? "";
			if (StringExtensions.Split(text, "|", false).Count() >= 3)
			{
				return Conversions.ToDouble(StringExtensions.Split(text, "|", false)[1]) <= 412.0;
			}
			result = null;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "确认启动器更新失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupSystem.xaml.vb");
			result = null;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	private void BtnSystemSettingExp_Click(object sender, MouseButtonEventArgs e)
	{
		ModMain.Hint("该功能尚在开发中！");
	}

	private void BtnSystemSettingImp_Click(object sender, MouseButtonEventArgs e)
	{
		ModMain.Hint("该功能尚在开发中！");
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagesetup/pagesetupsystem.xaml", UriKind.Relative);
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
			ComboDownloadSource = (MyComboBox)target;
			break;
		case 4:
			ComboDownloadVersion = (MyComboBox)target;
			break;
		case 5:
			SliderDownloadThread = (MySlider)target;
			break;
		case 6:
			SliderDownloadSpeed = (MySlider)target;
			break;
		case 7:
			ComboDownloadMod = (MyComboBox)target;
			break;
		case 8:
			ComboDownloadTranslateV2 = (MyComboBox)target;
			break;
		case 9:
			ComboModLocalNameStyle = (MyComboBox)target;
			break;
		case 10:
			CheckUpdateRelease = (MyCheckBox)target;
			break;
		case 11:
			CheckUpdateSnapshot = (MyCheckBox)target;
			break;
		case 12:
			CheckHelpChinese = (MyCheckBox)target;
			break;
		case 13:
			ComboSystemUpdate = (MyComboBox)target;
			break;
		case 14:
			ItemSystemUpdateDownload = (MyComboBoxItem)target;
			break;
		case 15:
			ComboSystemActivity = (MyComboBox)target;
			break;
		case 16:
			TextSystemCache = (MyTextBox)target;
			break;
		case 17:
			CheckSystemTelemetry = (MyCheckBox)target;
			break;
		case 18:
			BtnSystemUpdate = (MyButton)target;
			break;
		case 19:
			BtnSystemSettingExp = (MyButton)target;
			break;
		case 20:
			BtnSystemSettingImp = (MyButton)target;
			break;
		case 21:
			PanDonate = (Grid)target;
			break;
		case 22:
			BtnSystemIdentify = (MyButton)target;
			break;
		case 23:
			BtnSystemUnlock = (MyButton)target;
			break;
		case 24:
			CardDebug = (MyCard)target;
			break;
		case 25:
			PanDebugAnim = (Grid)target;
			break;
		case 26:
			SliderDebugAnim = (MySlider)target;
			break;
		case 27:
			CheckDebugSkipCopy = (MyCheckBox)target;
			break;
		case 28:
			CheckDebugMode = (MyCheckBox)target;
			break;
		case 29:
			CheckDebugDelay = (MyCheckBox)target;
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
