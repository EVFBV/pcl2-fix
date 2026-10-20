using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MeloongCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageSetupLeft : MyPageLeft, IComponentConnector
{
	private bool IsLoad;

	private bool IsPageSwitched;

	public FormMain.PageSubType PageID;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemLaunch")]
	private MyListItem _ItemLaunch;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemLink")]
	private MyListItem _ItemLink;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemUI")]
	private MyListItem _ItemUI;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemSystem")]
	private MyListItem _ItemSystem;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("PanItem")]
	internal virtual StackPanel PanItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyListItem ItemLaunch
	{
		[CompilerGenerated]
		get
		{
			return _ItemLaunch;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((MyListItem)a0, a1);
			};
			MyListItem itemLaunch = _ItemLaunch;
			if (itemLaunch != null)
			{
				itemLaunch.Check -= obj;
			}
			_ItemLaunch = value;
			itemLaunch = _ItemLaunch;
			if (itemLaunch != null)
			{
				itemLaunch.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemLink
	{
		[CompilerGenerated]
		get
		{
			return _ItemLink;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((MyListItem)a0, a1);
			};
			MyListItem itemLink = _ItemLink;
			if (itemLink != null)
			{
				itemLink.Check -= obj;
			}
			_ItemLink = value;
			itemLink = _ItemLink;
			if (itemLink != null)
			{
				itemLink.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemUI
	{
		[CompilerGenerated]
		get
		{
			return _ItemUI;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((MyListItem)a0, a1);
			};
			MyListItem itemUI = _ItemUI;
			if (itemUI != null)
			{
				itemUI.Check -= obj;
			}
			_ItemUI = value;
			itemUI = _ItemUI;
			if (itemUI != null)
			{
				itemUI.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemSystem
	{
		[CompilerGenerated]
		get
		{
			return _ItemSystem;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((MyListItem)a0, a1);
			};
			MyListItem itemSystem = _ItemSystem;
			if (itemSystem != null)
			{
				itemSystem.Check -= obj;
			}
			_ItemSystem = value;
			itemSystem = _ItemSystem;
			if (itemSystem != null)
			{
				itemSystem.Check += obj;
			}
		}
	}

	private void PageSetupLeft_Loaded(object sender, RoutedEventArgs e)
	{
		bool flag = false;
		if (ItemLaunch.Checked && Settings.Get<bool>("UiHiddenSetupLaunch"))
		{
			flag = true;
		}
		if (ItemUI.Checked && Settings.Get<bool>("UiHiddenSetupUi"))
		{
			flag = true;
		}
		if (ItemSystem.Checked && Settings.Get<bool>("UiHiddenSetupSystem"))
		{
			flag = true;
		}
		if (ItemLink.Checked && Settings.Get<bool>("UiHiddenSetupLink"))
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
			if (!Settings.Get<bool>("UiHiddenSetupLaunch"))
			{
				ItemLaunch.SetChecked(value: true, user: false, anime: false);
			}
			else if (!Settings.Get<bool>("UiHiddenSetupUi"))
			{
				ItemUI.SetChecked(value: true, user: false, anime: false);
			}
			else if (!Settings.Get<bool>("UiHiddenSetupSystem"))
			{
				ItemSystem.SetChecked(value: true, user: false, anime: false);
			}
			else if (!Settings.Get<bool>("UiHiddenSetupLink"))
			{
				ItemLink.SetChecked(value: true, user: false, anime: false);
			}
			else
			{
				ItemLaunch.SetChecked(value: true, user: false, anime: false);
			}
		}
	}

	private void PageOtherLeft_Unloaded(object sender, RoutedEventArgs e)
	{
		IsPageSwitched = false;
	}

	public PageSetupLeft()
	{
		base.Loaded += PageSetupLeft_Loaded;
		base.Unloaded += PageOtherLeft_Unloaded;
		IsLoad = false;
		IsPageSwitched = false;
		InitializeComponent();
		if (!Settings.Get<bool>("UiHiddenSetupLaunch"))
		{
			PageID = FormMain.PageSubType.Default;
		}
		else if (!Settings.Get<bool>("UiHiddenSetupUi"))
		{
			PageID = FormMain.PageSubType.DownloadMod;
		}
		else if (!Settings.Get<bool>("UiHiddenSetupSystem"))
		{
			PageID = FormMain.PageSubType.DownloadPack;
		}
		else if (!Settings.Get<bool>("UiHiddenSetupLink"))
		{
			PageID = FormMain.PageSubType.SetupLink;
		}
		else
		{
			PageID = FormMain.PageSubType.Default;
		}
	}

	private void PageCheck(MyListItem sender, EventArgs e)
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
			if (ModMain.FrmSetupLaunch == null)
			{
				ModMain.FrmSetupLaunch = new PageSetupLaunch();
			}
			return ModMain.FrmSetupLaunch;
		case FormMain.PageSubType.DownloadMod:
			if (ModMain.FrmSetupUI == null)
			{
				ModMain.FrmSetupUI = new PageSetupUI();
			}
			return ModMain.FrmSetupUI;
		case FormMain.PageSubType.SetupLink:
			if (ModMain.FrmSetupLink == null)
			{
				ModMain.FrmSetupLink = new PageSetupLink();
			}
			return ModMain.FrmSetupLink;
		case FormMain.PageSubType.DownloadPack:
			if (ModMain.FrmSetupSystem == null)
			{
				ModMain.FrmSetupSystem = new PageSetupSystem();
			}
			return ModMain.FrmSetupSystem;
		default:
			throw new Exception("未知的设置子页面种类：" + Conversions.ToString((int)ID));
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
				switch (ID)
				{
				case FormMain.PageSubType.Default:
					if (Information.IsNothing(ModMain.FrmSetupLaunch))
					{
						ModMain.FrmSetupLaunch = new PageSetupLaunch();
					}
					PageChangeRun(ModMain.FrmSetupLaunch);
					break;
				case FormMain.PageSubType.DownloadMod:
					if (Information.IsNothing(ModMain.FrmSetupUI))
					{
						ModMain.FrmSetupUI = new PageSetupUI();
					}
					PageChangeRun(ModMain.FrmSetupUI);
					break;
				case FormMain.PageSubType.SetupLink:
					if (Information.IsNothing(ModMain.FrmSetupLink))
					{
						ModMain.FrmSetupLink = new PageSetupLink();
					}
					PageChangeRun(ModMain.FrmSetupLink);
					break;
				case FormMain.PageSubType.DownloadPack:
					if (Information.IsNothing(ModMain.FrmSetupSystem))
					{
						ModMain.FrmSetupSystem = new PageSetupSystem();
					}
					PageChangeRun(ModMain.FrmSetupSystem);
					break;
				default:
					throw new Exception("未知的设置子页面种类：" + Conversions.ToString(unchecked((int)ID)));
				}
				PageID = ID;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"切换分页面失败（ID {ID}）", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupLeft.xaml.vb");
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

	public void Reset(object sender, EventArgs e)
	{
		double num = ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)));
		if (num == 0.0)
		{
			if (ModMain.MyMsgBox("是否要初始化 启动 页面的所有设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", IsWarn: true) == 1)
			{
				if (Information.IsNothing(ModMain.FrmSetupLaunch))
				{
					ModMain.FrmSetupLaunch = new PageSetupLaunch();
				}
				ModMain.FrmSetupLaunch.Reset();
				ItemLaunch.Checked = true;
			}
		}
		else if (num == 2.0)
		{
			if (ModMain.MyMsgBox("是否要初始化 个性化 页面的所有设置？该操作不可撤销。\r\n（背景图片与音乐、主页等外部文件不会被删除）", "初始化确认", "确定", "取消", "", IsWarn: true) == 1)
			{
				if (Information.IsNothing(ModMain.FrmSetupUI))
				{
					ModMain.FrmSetupUI = new PageSetupUI();
				}
				ModMain.FrmSetupUI.Reset();
				ItemUI.Checked = true;
			}
		}
		else if (num == 3.0)
		{
			if (ModMain.MyMsgBox("是否要初始化 其他 页面的所有设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", IsWarn: true) == 1)
			{
				if (Information.IsNothing(ModMain.FrmSetupSystem))
				{
					ModMain.FrmSetupSystem = new PageSetupSystem();
				}
				ModMain.FrmSetupSystem.Reset();
				ItemSystem.Checked = true;
			}
		}
		else if (num == 1.0 && ModMain.MyMsgBox("是否要初始化 联机 页面的所有设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", IsWarn: true) == 1)
		{
			if (Information.IsNothing(ModMain.FrmSetupLink))
			{
				ModMain.FrmSetupLink = new PageSetupLink();
			}
			ModMain.FrmSetupLink.Reset();
			ItemLink.Checked = true;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagesetup/pagesetupleft.xaml", UriKind.Relative);
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
			ItemLaunch = (MyListItem)target;
			break;
		case 3:
			ItemLink = (MyListItem)target;
			break;
		case 4:
			ItemUI = (MyListItem)target;
			break;
		case 5:
			ItemSystem = (MyListItem)target;
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
