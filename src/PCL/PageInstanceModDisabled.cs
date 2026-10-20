using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageInstanceModDisabled : MyPageRight, IComponentConnector
{
	[CompilerGenerated]
	[AccessedThroughProperty("BtnDownload")]
	private MyButton _BtnDownload;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnVersion")]
	private MyButton _BtnVersion;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("PanMain")]
	internal virtual MyCard PanMain
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnDownload
	{
		[CompilerGenerated]
		get
		{
			return _BtnDownload;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnDownload_Click;
			RoutedEventHandler value2 = [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				BtnDownload_Loaded();
			};
			MyButton btnDownload = _BtnDownload;
			if (btnDownload != null)
			{
				btnDownload.Click -= obj;
				btnDownload.Loaded -= value2;
			}
			_BtnDownload = value;
			btnDownload = _BtnDownload;
			if (btnDownload != null)
			{
				btnDownload.Click += obj;
				btnDownload.Loaded += value2;
			}
		}
	}

	internal virtual MyButton BtnVersion
	{
		[CompilerGenerated]
		get
		{
			return _BtnVersion;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnVersion_Click;
			MyButton btnVersion = _BtnVersion;
			if (btnVersion != null)
			{
				btnVersion.Click -= obj;
			}
			_BtnVersion = value;
			btnVersion = _BtnVersion;
			if (btnVersion != null)
			{
				btnVersion.Click += obj;
			}
		}
	}

	public PageInstanceModDisabled()
	{
		InitializeComponent();
	}

	private void BtnDownload_Click(object sender, EventArgs e)
	{
		ModMain.FrmMain.PageChange(FormMain.PageType.Download);
	}

	private void BtnVersion_Click(object sender, EventArgs e)
	{
		ModMain.FrmMain.PageChange(FormMain.PageType.Launch);
		ModMain.FrmMain.PageChange(FormMain.PageType.InstanceSelect);
	}

	public void BtnDownload_Loaded()
	{
		Visibility visibility = (((Settings.Get<bool>("UiHiddenPageDownload") && !PageSetupUI.HiddenForceShow) || (ModMain.FrmSelectRight != null && ModMain.FrmSelectRight.ShowHidden)) ? Visibility.Collapsed : Visibility.Visible);
		if (BtnDownload.Visibility != visibility)
		{
			BtnDownload.Visibility = visibility;
			PanMain.TriggerForceResize();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageinstance/pageinstancemoddisabled.xaml", UriKind.Relative);
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
			PanMain = (MyCard)target;
			break;
		case 2:
			BtnDownload = (MyButton)target;
			break;
		case 3:
			BtnVersion = (MyButton)target;
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
