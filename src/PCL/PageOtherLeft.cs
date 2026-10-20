using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageOtherLeft : MyPageLeft, IComponentConnector
{
	private bool IsLoad;

	private bool IsPageSwitched;

	public FormMain.PageSubType PageID;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemHelp")]
	private MyListItem _ItemHelp;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemAbout")]
	private MyListItem _ItemAbout;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemTest")]
	private MyListItem _ItemTest;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemFeedback")]
	private MyListItem _ItemFeedback;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemVote")]
	private MyListItem _ItemVote;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("PanItem")]
	internal virtual StackPanel PanItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyListItem ItemHelp
	{
		[CompilerGenerated]
		get
		{
			return _ItemHelp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemHelp = _ItemHelp;
			if (itemHelp != null)
			{
				itemHelp.Check -= obj;
			}
			_ItemHelp = value;
			itemHelp = _ItemHelp;
			if (itemHelp != null)
			{
				itemHelp.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemAbout
	{
		[CompilerGenerated]
		get
		{
			return _ItemAbout;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemAbout = _ItemAbout;
			if (itemAbout != null)
			{
				itemAbout.Check -= obj;
			}
			_ItemAbout = value;
			itemAbout = _ItemAbout;
			if (itemAbout != null)
			{
				itemAbout.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemTest
	{
		[CompilerGenerated]
		get
		{
			return _ItemTest;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemTest = _ItemTest;
			if (itemTest != null)
			{
				itemTest.Check -= obj;
			}
			_ItemTest = value;
			itemTest = _ItemTest;
			if (itemTest != null)
			{
				itemTest.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemFeedback
	{
		[CompilerGenerated]
		get
		{
			return _ItemFeedback;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.ChangedEventHandler obj = TryFeedback;
			MyListItem itemFeedback = _ItemFeedback;
			if (itemFeedback != null)
			{
				itemFeedback.Changed -= obj;
			}
			_ItemFeedback = value;
			itemFeedback = _ItemFeedback;
			if (itemFeedback != null)
			{
				itemFeedback.Changed += obj;
			}
		}
	}

	internal virtual MyListItem ItemVote
	{
		[CompilerGenerated]
		get
		{
			return _ItemVote;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.ChangedEventHandler obj = TryVote;
			MyListItem itemVote = _ItemVote;
			if (itemVote != null)
			{
				itemVote.Changed -= obj;
			}
			_ItemVote = value;
			itemVote = _ItemVote;
			if (itemVote != null)
			{
				itemVote.Changed += obj;
			}
		}
	}

	private void PageOtherLeft_Loaded(object sender, RoutedEventArgs e)
	{
		bool flag = false;
		if (ItemHelp.Checked && Settings.Get<bool>("UiHiddenOtherHelp"))
		{
			flag = true;
		}
		if (ItemAbout.Checked && Settings.Get<bool>("UiHiddenOtherAbout"))
		{
			flag = true;
		}
		if (ItemTest.Checked && Settings.Get<bool>("UiHiddenOtherTest"))
		{
			flag = true;
		}
		if (PageSetupUI.HiddenForceShow)
		{
			flag = false;
		}
		if (IsLoad && !flag)
		{
			return;
		}
		IsLoad = true;
		PageSetupUI.HiddenRefresh();
		if (!IsPageSwitched)
		{
			if (!Settings.Get<bool>("UiHiddenOtherHelp"))
			{
				ItemHelp.SetChecked(value: true, user: false, anime: false);
			}
			else if (!Settings.Get<bool>("UiHiddenOtherAbout"))
			{
				ItemAbout.SetChecked(value: true, user: false, anime: false);
			}
			else
			{
				ItemTest.SetChecked(value: true, user: false, anime: false);
			}
		}
	}

	private void PageOtherLeft_Unloaded(object sender, RoutedEventArgs e)
	{
		IsPageSwitched = false;
	}

	public PageOtherLeft()
	{
		base.Loaded += PageOtherLeft_Loaded;
		base.Unloaded += PageOtherLeft_Unloaded;
		IsLoad = false;
		IsPageSwitched = false;
		InitializeComponent();
		if (!Settings.Get<bool>("UiHiddenOtherHelp"))
		{
			PageID = FormMain.PageSubType.Default;
		}
		else if (!Settings.Get<bool>("UiHiddenOtherAbout"))
		{
			PageID = FormMain.PageSubType.SetupLink;
		}
		else
		{
			PageID = FormMain.PageSubType.DownloadMod;
		}
	}

	private void PageCheck(FrameworkElement sender, ModBase.RouteEventArgs e)
	{
		if (sender.Tag != null)
		{
			PageChange((FormMain.PageSubType)checked((int)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(sender.Tag)))));
		}
	}

	public object PageGet(FormMain.PageSubType ID = (FormMain.PageSubType)(-1))
	{
		if (ID == (FormMain.PageSubType)(-1))
		{
			ID = PageID;
		}
		switch (ID)
		{
		case FormMain.PageSubType.Default:
			if (ModMain.FrmOtherHelp == null)
			{
				ModMain.FrmOtherHelp = new PageOtherHelp();
			}
			return ModMain.FrmOtherHelp;
		case FormMain.PageSubType.SetupLink:
			if (ModMain.FrmOtherAbout == null)
			{
				ModMain.FrmOtherAbout = new PageOtherAbout();
			}
			return ModMain.FrmOtherAbout;
		case FormMain.PageSubType.DownloadMod:
			if (ModMain.FrmOtherTest == null)
			{
				ModMain.FrmOtherTest = new PageOtherTest();
			}
			return ModMain.FrmOtherTest;
		default:
			throw new Exception("未知的更多子页面种类：" + Conversions.ToString((int)ID));
		}
	}

	public void PageChange(FormMain.PageSubType ID)
	{
		if (PageID == ID)
		{
			return;
		}
		checked
		{
			ModAnimation.AniControlEnabled++;
			IsPageSwitched = true;
			try
			{
				PageChangeRun((MyPageRight)PageGet(ID));
				PageID = ID;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"切换分页面失败（ID {ID}）", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageOther\\PageOtherLeft.xaml.vb");
				ProjectData.ClearProjectError();
			}
			finally
			{
				ModAnimation.AniControlEnabled--;
			}
		}
	}

	private static void PageChangeRun(MyPageRight Target)
	{
		ModAnimation.AniStop("FrmMain PageChangeRight");
		if (Target.Parent != null)
		{
			Target.SetValue(ContentPresenter.ContentProperty, null);
		}
		ModMain.FrmMain.PageRight = Target;
		((MyPageRight)ModMain.FrmMain.PanMainRight.Child).PageOnExit();
		ModAnimation.AniStart(new ModAnimation.AniData[2]
		{
			ModAnimation.AaCode([SpecialName] () =>
			{
				((MyPageRight)ModMain.FrmMain.PanMainRight.Child).PageOnForceExit();
				ModMain.FrmMain.PanMainRight.Child = ModMain.FrmMain.PageRight;
				ModMain.FrmMain.PageRight.Opacity = 0.0;
			}, 130),
			ModAnimation.AaCode([SpecialName] () =>
			{
				ModMain.FrmMain.PageRight.Opacity = 1.0;
				ModMain.FrmMain.PageRight.PageOnEnter();
			}, 30, After: true)
		}, "PageLeft PageChange");
	}

	public void Refresh(object sender, EventArgs e)
	{
		if (ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null))) == 0.0)
		{
			RefreshHelp();
			ItemHelp.Checked = true;
		}
		ModMain.Hint("正在刷新……", ModMain.HintType.Blue, Log: false);
	}

	public static void RefreshHelp()
	{
		Settings.Set("SystemHelpVersion", 0);
		ModMain.FrmOtherHelp.PageLoaderRestart();
		ModMain.FrmOtherHelp.SearchBox.Text = "";
	}

	private void TryFeedback(object sender, ModBase.RouteEventArgs e)
	{
		if (ItemFeedback.Checked)
		{
			TryFeedback();
			e.Handled = true;
		}
	}

	public static void TryFeedback()
	{
		if (false.Equals(PageSetupSystem.IsLauncherNewest()))
		{
			if (ModMain.MyMsgBox(string.Format("你的 PCL 不是最新版，因此无法提交反馈。{0}请在更新后，确认该问题在最新版中依然存在，然后再提交反馈。", "\r\n"), "无法提交反馈", "更新", "取消") == 1)
			{
				ModSecret.UpdateCheckByButton();
			}
			return;
		}
		switch (ModMain.MyMsgBox("在提交新反馈前，建议先搜索反馈列表，以避免重复提交。\r\n如果无法打开该网页，请使用 VPN 改善网络环境。", "反馈", "提交新反馈", "查看反馈列表", "取消"))
		{
		case 1:
			ModMain.Feedback();
			break;
		case 2:
			ModBase.OpenWebsite("https://github.com/Meloong-Git/PCL/issues/");
			break;
		}
	}

	private void TryVote(object sender, ModBase.RouteEventArgs e)
	{
		if (ItemVote.Checked)
		{
			TryVote();
			e.Handled = true;
		}
	}

	public static void TryVote()
	{
		if (ModMain.MyMsgBox("是否要打开新功能投票网页？\r\n如果无法打开该网页，请使用 VPN 改善网络环境。", "新功能投票", "打开", "取消") != 2)
		{
			ModBase.OpenWebsite("https://github.com/Meloong-Git/PCL/discussions/categories/%E5%8A%9F%E8%83%BD%E6%8A%95%E7%A5%A8?discussions_q=category%3A%E5%8A%9F%E8%83%BD%E6%8A%95%E7%A5%A8+sort%3Adate_created");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageother/pageotherleft.xaml", UriKind.Relative);
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
			PanItem = (StackPanel)target;
			break;
		case 2:
			ItemHelp = (MyListItem)target;
			break;
		case 3:
			ItemAbout = (MyListItem)target;
			break;
		case 4:
			ItemTest = (MyListItem)target;
			break;
		case 5:
			ItemFeedback = (MyListItem)target;
			break;
		case 6:
			ItemVote = (MyListItem)target;
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
