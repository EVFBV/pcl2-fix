using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageDownloadLeft : MyPageLeft, IRefreshable, IComponentConnector
{
	public FormMain.PageSubType PageID;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemInstall")]
	private MyListItem _ItemInstall;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemMod")]
	private MyListItem _ItemMod;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemPack")]
	private MyListItem _ItemPack;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemDataPack")]
	private MyListItem _ItemDataPack;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemResourcePack")]
	private MyListItem _ItemResourcePack;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemShader")]
	private MyListItem _ItemShader;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("PanItem")]
	internal virtual StackPanel PanItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyListItem ItemInstall
	{
		[CompilerGenerated]
		get
		{
			return _ItemInstall;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem.ClickEventHandler obj2 = ItemInstall_Click;
			MyListItem itemInstall = _ItemInstall;
			if (itemInstall != null)
			{
				itemInstall.Check -= obj;
				itemInstall.Click -= obj2;
			}
			_ItemInstall = value;
			itemInstall = _ItemInstall;
			if (itemInstall != null)
			{
				itemInstall.Check += obj;
				itemInstall.Click += obj2;
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

	internal virtual MyListItem ItemPack
	{
		[CompilerGenerated]
		get
		{
			return _ItemPack;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemPack = _ItemPack;
			if (itemPack != null)
			{
				itemPack.Check -= obj;
			}
			_ItemPack = value;
			itemPack = _ItemPack;
			if (itemPack != null)
			{
				itemPack.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemDataPack
	{
		[CompilerGenerated]
		get
		{
			return _ItemDataPack;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemDataPack = _ItemDataPack;
			if (itemDataPack != null)
			{
				itemDataPack.Check -= obj;
			}
			_ItemDataPack = value;
			itemDataPack = _ItemDataPack;
			if (itemDataPack != null)
			{
				itemDataPack.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemResourcePack
	{
		[CompilerGenerated]
		get
		{
			return _ItemResourcePack;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemResourcePack = _ItemResourcePack;
			if (itemResourcePack != null)
			{
				itemResourcePack.Check -= obj;
			}
			_ItemResourcePack = value;
			itemResourcePack = _ItemResourcePack;
			if (itemResourcePack != null)
			{
				itemResourcePack.Check += obj;
			}
		}
	}

	internal virtual MyListItem ItemShader
	{
		[CompilerGenerated]
		get
		{
			return _ItemShader;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			IMyRadio.CheckEventHandler obj = [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
			{
				PageCheck((FrameworkElement)a0, a1);
			};
			MyListItem itemShader = _ItemShader;
			if (itemShader != null)
			{
				itemShader.Check -= obj;
			}
			_ItemShader = value;
			itemShader = _ItemShader;
			if (itemShader != null)
			{
				itemShader.Check += obj;
			}
		}
	}

	public PageDownloadLeft()
	{
		PageID = FormMain.PageSubType.Default;
		InitializeComponent();
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
			if (ModMain.FrmDownloadInstall == null)
			{
				ModMain.FrmDownloadInstall = new PageDownloadInstall();
			}
			return ModMain.FrmDownloadInstall;
		case FormMain.PageSubType.DownloadMod:
			if (ModMain.FrmDownloadMod == null)
			{
				ModMain.FrmDownloadMod = new PageDownloadMod();
			}
			return ModMain.FrmDownloadMod;
		case FormMain.PageSubType.DownloadPack:
			if (ModMain.FrmDownloadPack == null)
			{
				ModMain.FrmDownloadPack = new PageDownloadPack();
			}
			return ModMain.FrmDownloadPack;
		case FormMain.PageSubType.DownloadResourcePack:
			if (ModMain.FrmDownloadResourcePack == null)
			{
				ModMain.FrmDownloadResourcePack = new PageDownloadResourcePack();
			}
			return ModMain.FrmDownloadResourcePack;
		case FormMain.PageSubType.DownloadShader:
			if (ModMain.FrmDownloadShader == null)
			{
				ModMain.FrmDownloadShader = new PageDownloadShader();
			}
			return ModMain.FrmDownloadShader;
		case FormMain.PageSubType.DownloadDataPack:
			if (ModMain.FrmDownloadDataPack == null)
			{
				ModMain.FrmDownloadDataPack = new PageDownloadDataPack();
			}
			return ModMain.FrmDownloadDataPack;
		default:
			throw new Exception("未知的下载子页面种类：" + Conversions.ToString((int)ID));
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
			try
			{
				PageChangeRun((MyPageRight)PageGet(ID));
				PageID = ID;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"切换分页面失败（ID {ID}）", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\PageDownloadLeft.xaml.vb");
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
		ResourceProject.Cache.Clear();
		ResourceVersion.ProjectFilesCache.Clear();
		switch (SubType)
		{
		case FormMain.PageSubType.Default:
			ModDownload.DlClientListLoader.Start(null, IsForceRestart: true);
			ModDownload.DlOptiFineListLoader.Start(null, IsForceRestart: true);
			ModDownload.DlForgeListLoader.Start(null, IsForceRestart: true);
			ModDownload.DlNeoForgeListLoader.Start(null, IsForceRestart: true);
			ModDownload.DlLiteLoaderListLoader.Start(null, IsForceRestart: true);
			ModDownload.DlFabricListLoader.Start(null, IsForceRestart: true);
			ModDownload.DlFabricApiLoader.Start(null, IsForceRestart: true);
			ModDownload.DlOptiFabricLoader.Start(null, IsForceRestart: true);
			ItemInstall.Checked = true;
			break;
		case FormMain.PageSubType.DownloadMod:
			if (ModMain.FrmDownloadMod != null)
			{
				ModMain.FrmDownloadMod.Content.Storage = new ResourceSearcher.SearchResult();
				ModMain.FrmDownloadMod.Content.Page = 0;
				ModMain.FrmDownloadMod.PageLoaderRestart();
			}
			ItemMod.Checked = true;
			break;
		case FormMain.PageSubType.DownloadPack:
			if (ModMain.FrmDownloadPack != null)
			{
				ModMain.FrmDownloadPack.Content.Storage = new ResourceSearcher.SearchResult();
				ModMain.FrmDownloadPack.Content.Page = 0;
				ModMain.FrmDownloadPack.PageLoaderRestart();
			}
			ItemPack.Checked = true;
			break;
		case FormMain.PageSubType.DownloadResourcePack:
			if (ModMain.FrmDownloadResourcePack != null)
			{
				ModMain.FrmDownloadResourcePack.Content.Storage = new ResourceSearcher.SearchResult();
				ModMain.FrmDownloadResourcePack.Content.Page = 0;
				ModMain.FrmDownloadResourcePack.PageLoaderRestart();
			}
			ItemResourcePack.Checked = true;
			break;
		case FormMain.PageSubType.DownloadShader:
			if (ModMain.FrmDownloadShader != null)
			{
				ModMain.FrmDownloadShader.Content.Storage = new ResourceSearcher.SearchResult();
				ModMain.FrmDownloadShader.Content.Page = 0;
				ModMain.FrmDownloadShader.PageLoaderRestart();
			}
			ItemShader.Checked = true;
			break;
		case FormMain.PageSubType.DownloadDataPack:
			if (ModMain.FrmDownloadDataPack != null)
			{
				ModMain.FrmDownloadDataPack.Content.Storage = new ResourceSearcher.SearchResult();
				ModMain.FrmDownloadDataPack.Content.Page = 0;
				ModMain.FrmDownloadDataPack.PageLoaderRestart();
			}
			ItemDataPack.Checked = true;
			break;
		}
		ModMain.Hint("正在刷新……", ModMain.HintType.Blue, Log: false);
	}

	private void ItemInstall_Click(object sender, MouseButtonEventArgs e)
	{
		if (ItemInstall.Checked)
		{
			ModMain.FrmDownloadInstall.ExitSelectPage();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/pagedownloadleft.xaml", UriKind.Relative);
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
			ItemInstall = (MyListItem)target;
			break;
		case 3:
			ItemMod = (MyListItem)target;
			break;
		case 4:
			ItemPack = (MyListItem)target;
			break;
		case 5:
			ItemDataPack = (MyListItem)target;
			break;
		case 6:
			ItemResourcePack = (MyListItem)target;
			break;
		case 7:
			ItemShader = (MyListItem)target;
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
