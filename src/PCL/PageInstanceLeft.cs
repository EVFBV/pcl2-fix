using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MeloongCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using PCLCS;

namespace PCL;

[DesignerGenerated]
public class PageInstanceLeft : MyPageLeft, IRefreshable, IComponentConnector
{
	public static ModMinecraft.McInstance Instance = null;

	public FormMain.PageSubType PageID;

	public static RedoableWorker<Java> CurrentJavaWorker = new RedoableWorker<Java>((Func<CancellationToken?, ProgressProvider, Java>)([SpecialName] (CancellationToken? c, ProgressProvider p) => ModJava.SelectOrDownloadJava(OneOf<ValueRange<Version>, ModMinecraft.McInstance>.op_Implicit(Instance), TryFixOrHint: false, c.Value, p)), "CurrentJavaWorker");

	[CompilerGenerated]
	[AccessedThroughProperty("ItemOverall")]
	private MyListItem _ItemOverall;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemSetup")]
	private MyListItem _ItemSetup;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemModify")]
	private MyListItem _ItemModify;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemMod")]
	private MyListItem _ItemMod;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemModDisabled")]
	private MyListItem _ItemModDisabled;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemExport")]
	private MyListItem _ItemExport;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("PanItem")]
	internal virtual StackPanel PanItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyListItem ItemOverall
	{
		[CompilerGenerated]
		get
		{
			return _ItemOverall;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemOverall = _ItemOverall;
			if (itemOverall != null)
			{
				itemOverall.Check -= obj;
			}
			_ItemOverall = value;
			itemOverall = _ItemOverall;
			if (itemOverall != null)
			{
				itemOverall.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemSetup
	{
		[CompilerGenerated]
		get
		{
			return _ItemSetup;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemSetup = _ItemSetup;
			if (itemSetup != null)
			{
				itemSetup.Check -= obj;
			}
			_ItemSetup = value;
			itemSetup = _ItemSetup;
			if (itemSetup != null)
			{
				itemSetup.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemModify
	{
		[CompilerGenerated]
		get
		{
			return _ItemModify;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemModify = _ItemModify;
			if (itemModify != null)
			{
				itemModify.Check -= obj;
			}
			_ItemModify = value;
			itemModify = _ItemModify;
			if (itemModify != null)
			{
				itemModify.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemMod
	{
		[CompilerGenerated]
		get
		{
			return _ItemMod;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemMod = _ItemMod;
			if (itemMod != null)
			{
				itemMod.Check -= obj;
			}
			_ItemMod = value;
			itemMod = _ItemMod;
			if (itemMod != null)
			{
				itemMod.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemModDisabled
	{
		[CompilerGenerated]
		get
		{
			return _ItemModDisabled;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemModDisabled = _ItemModDisabled;
			if (itemModDisabled != null)
			{
				itemModDisabled.Check -= obj;
			}
			_ItemModDisabled = value;
			itemModDisabled = _ItemModDisabled;
			if (itemModDisabled != null)
			{
				itemModDisabled.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemExport
	{
		[CompilerGenerated]
		get
		{
			return _ItemExport;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemExport = _ItemExport;
			if (itemExport != null)
			{
				itemExport.Check -= obj;
			}
			_ItemExport = value;
			itemExport = _ItemExport;
			if (itemExport != null)
			{
				itemExport.Check += obj;
			}
		}
	}

	public PageInstanceLeft()
	{
		base.Loaded += PageInstanceLeft_Loaded;
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshModDisabled();
		};
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			ReloadCurrentJava();
		};
		PageID = FormMain.PageSubType.Default;
		InitializeComponent();
	}

	private void PageInstanceLeft_Loaded(object sender, RoutedEventArgs e)
	{
		PageSetupUI.HiddenRefresh();
	}

	public void RefreshModDisabled()
	{
		if ((object)Instance != null && Instance.Modable)
		{
			ItemMod.Visibility = Visibility.Visible;
			ItemModDisabled.Visibility = Visibility.Collapsed;
		}
		else
		{
			ItemMod.Visibility = Visibility.Collapsed;
			ItemModDisabled.Visibility = Visibility.Visible;
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
			if (ModMain.FrmInstanceOverall == null)
			{
				ModMain.FrmInstanceOverall = new PageInstanceOverall();
			}
			return ModMain.FrmInstanceOverall;
		case FormMain.PageSubType.DownloadPack:
			if (ModMain.FrmInstanceMod == null)
			{
				ModMain.FrmInstanceMod = new PageInstanceMod();
			}
			return ModMain.FrmInstanceMod;
		case FormMain.PageSubType.DownloadDataPack:
			if (ModMain.FrmInstanceModDisabled == null)
			{
				ModMain.FrmInstanceModDisabled = new PageInstanceModDisabled();
			}
			return ModMain.FrmInstanceModDisabled;
		case FormMain.PageSubType.SetupLink:
			if (ModMain.FrmInstanceSetup == null)
			{
				ModMain.FrmInstanceSetup = new PageInstanceSetup();
			}
			return ModMain.FrmInstanceSetup;
		case FormMain.PageSubType.DownloadResourcePack:
			if (ModMain.FrmInstanceExport == null)
			{
				ModMain.FrmInstanceExport = new PageInstanceExport();
			}
			return ModMain.FrmInstanceExport;
		case FormMain.PageSubType.DownloadMod:
			if (ModMain.FrmDownloadModify == null)
			{
				ModMain.FrmDownloadModify = new PageDownloadModify();
			}
			return ModMain.FrmDownloadModify;
		default:
			throw new Exception("未知的版本设置子页面种类：" + Conversions.ToString((int)ID));
		}
	}

	public void PageChange(FormMain.PageSubType ID)
	{
		if (PageID == ID)
		{
			return;
		}
		if ((object)Instance != null && ModMinecraft.McInstanceLock.IsLocked(Instance))
		{
			ModMain.Hint("该版本正在被修改，请稍后再切换页面！", ModMain.HintType.Red);
			return;
		}
		checked
		{
			ModAnimation.AniControlEnabled++;
			try
			{
				PageChangeRun((MyPageRight)PageGet(ID));
				PageID = ID;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"切换分页面失败（ID {ID}）", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceLeft.xaml.vb");
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

	public void Refresh_Click(object sender, EventArgs e)
	{
		Refresh((FormMain.PageSubType)checked((int)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null))))));
	}

	public void Refresh()
	{
		Refresh(ModMain.FrmMain.PageCurrentSub);
	}

	void IRefreshable.Refresh()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Refresh
		this.Refresh();
	}

	public void Refresh(FormMain.PageSubType SubType)
	{
		if ((object)Instance != null && ModMinecraft.McInstanceLock.IsLocked(Instance))
		{
			ModMain.Hint("该版本正在被修改，请稍后再刷新！", ModMain.HintType.Red);
			return;
		}
		switch (SubType)
		{
		case FormMain.PageSubType.DownloadPack:
			PageInstanceMod.Refresh();
			ItemMod.Checked = true;
			break;
		case FormMain.PageSubType.DownloadResourcePack:
			ModMain.FrmInstanceExport?.RefreshAll();
			ItemExport.Checked = true;
			break;
		case FormMain.PageSubType.DownloadMod:
			ModMain.FrmDownloadModify?.PageLoaderRestart();
			ItemModify.Checked = true;
			break;
		case FormMain.PageSubType.DownloadDataPack:
			break;
		}
	}

	public void Reset(object sender, EventArgs e)
	{
		if (ModMain.MyMsgBox("是否要初始化该版本的版本独立设置？该操作不可撤销。", "初始化确认", "确定", "取消", "", IsWarn: true) == 1)
		{
			if (Information.IsNothing(ModMain.FrmInstanceSetup))
			{
				ModMain.FrmInstanceSetup = new PageInstanceSetup();
			}
			ModMain.FrmInstanceSetup.Reset();
			ItemSetup.Checked = true;
		}
	}

	public static void ReloadCurrentJava()
	{
		if (ModAnimation.AniControlEnabled == 0 && (object)Instance != null)
		{
			((RedoableWorkerBase<Java>)(object)CurrentJavaWorker).Start(default(CancellationToken), (ProgressProvider)null);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageinstance/pageinstanceleft.xaml", UriKind.Relative);
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
			ItemOverall = (MyListItem)target;
			break;
		case 3:
			ItemSetup = (MyListItem)target;
			break;
		case 4:
			ItemModify = (MyListItem)target;
			break;
		case 5:
			ItemMod = (MyListItem)target;
			break;
		case 6:
			ItemModDisabled = (MyListItem)target;
			break;
		case 7:
			ItemExport = (MyListItem)target;
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
