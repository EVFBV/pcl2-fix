using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageOtherAbout : MyPageRight, IComponentConnector
{
	private new bool IsLoaded;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnAboutUpdate")]
	private MyButton _BtnAboutUpdate;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnDonateOutput")]
	private MyButton _BtnDonateOutput;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnDonateCopy")]
	private MyButton _BtnDonateCopy;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnDonateInput")]
	private MyButton _BtnDonateInput;

	private bool _contentLoaded;

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

	[field: AccessedThroughProperty("ItemAboutPcl")]
	internal virtual MyListItem ItemAboutPcl
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnAboutUpdate
	{
		[CompilerGenerated]
		get
		{
			return _BtnAboutUpdate;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnAboutUpdate_Click;
			MyButton btnAboutUpdate = _BtnAboutUpdate;
			if (btnAboutUpdate != null)
			{
				btnAboutUpdate.Click -= obj;
			}
			_BtnAboutUpdate = value;
			btnAboutUpdate = _BtnAboutUpdate;
			if (btnAboutUpdate != null)
			{
				btnAboutUpdate.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnDonateOutput
	{
		[CompilerGenerated]
		get
		{
			return _BtnDonateOutput;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnDonateOutput_Click;
			MyButton btnDonateOutput = _BtnDonateOutput;
			if (btnDonateOutput != null)
			{
				btnDonateOutput.Click -= obj;
			}
			_BtnDonateOutput = value;
			btnDonateOutput = _BtnDonateOutput;
			if (btnDonateOutput != null)
			{
				btnDonateOutput.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("BtnDonateDonate")]
	internal virtual MyButton BtnDonateDonate
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnDonateCopy
	{
		[CompilerGenerated]
		get
		{
			return _BtnDonateCopy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				CopyIdentify();
			};
			MyButton btnDonateCopy = _BtnDonateCopy;
			if (btnDonateCopy != null)
			{
				btnDonateCopy.Click -= obj;
			}
			_BtnDonateCopy = value;
			btnDonateCopy = _BtnDonateCopy;
			if (btnDonateCopy != null)
			{
				btnDonateCopy.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnDonateInput
	{
		[CompilerGenerated]
		get
		{
			return _BtnDonateInput;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				BtnDonateCodeInput_Click();
			};
			MyButton btnDonateInput = _BtnDonateInput;
			if (btnDonateInput != null)
			{
				btnDonateInput.Click -= obj;
			}
			_BtnDonateInput = value;
			btnDonateInput = _BtnDonateInput;
			if (btnDonateInput != null)
			{
				btnDonateInput.Click += obj;
			}
		}
	}

	public PageOtherAbout()
	{
		base.Loaded += PageOtherAbout_Loaded;
		IsLoaded = false;
		InitializeComponent();
	}

	private void PageOtherAbout_Loaded(object sender, RoutedEventArgs e)
	{
		PanBack.ScrollToHome();
		if (!IsLoaded)
		{
			IsLoaded = true;
			ItemAboutPcl.Info = ItemAboutPcl.Info.Replace("%VERSION%", "快照版 2.14.0.0").Replace("%VERSIONCODE%", Conversions.ToString(412)).Replace("%BRANCH%", Conversions.ToString(0));
		}
	}

	public static void CopyIdentify()
	{
		ModBase.ClipboardSet(ModBase.Identify);
	}

	private void BtnDonateCodeInput_Click()
	{
		ModSecret.InputPotatoCode(IsUpdating: false);
	}

	private void BtnDonateOutput_Click(object sender, EventArgs e)
	{
		ModSecret.GeneratePotatoCode();
	}

	private void BtnAboutUpdate_Click(object sender, MouseButtonEventArgs e)
	{
		ModSecret.UpdateCheckByButton();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageother/pageotherabout.xaml", UriKind.Relative);
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
			ItemAboutPcl = (MyListItem)target;
			break;
		case 4:
			BtnAboutUpdate = (MyButton)target;
			break;
		case 5:
			BtnDonateOutput = (MyButton)target;
			break;
		case 6:
			BtnDonateDonate = (MyButton)target;
			break;
		case 7:
			BtnDonateCopy = (MyButton)target;
			break;
		case 8:
			BtnDonateInput = (MyButton)target;
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
