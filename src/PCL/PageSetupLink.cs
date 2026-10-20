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
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageSetupLink : MyPageRight, IComponentConnector
{
	private bool _contentLoaded;

	[SpecialName]
	private bool _0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded_0024Init;

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

	[field: AccessedThroughProperty("ComboLatencyMode")]
	internal virtual MyComboBox ComboLatencyMode
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextCustomPeer")]
	internal virtual MyTextBox TextCustomPeer
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public PageSetupLink()
	{
		base.Loaded += PageSetupLink_Loaded;
		InitializeComponent();
	}

	private void PageSetupLink_Loaded(object sender, RoutedEventArgs e)
	{
		PanBack.ScrollToHome();
		if (_0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded_0024Init == null)
		{
			Interlocked.CompareExchange(ref _0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded_0024Init, new StaticLocalInitFlag(), null);
		}
		bool lockTaken = false;
		try
		{
			Monitor.Enter(_0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded_0024Init, ref lockTaken);
			if (_0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded_0024Init.State == 0)
			{
				_0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded_0024Init.State = 2;
				_0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded = false;
			}
			else if (_0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded_0024Init.State == 2)
			{
				throw new IncompleteInitialization();
			}
		}
		finally
		{
			_0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded_0024Init.State = 1;
			if (lockTaken)
			{
				Monitor.Exit(_0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded_0024Init);
			}
		}
		checked
		{
			if (!_0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded)
			{
				_0024STATIC_0024PageSetupLink_Loaded_002420211C128141_0024IsLoaded = true;
				ModAnimation.AniControlEnabled++;
				Reload();
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
			Logger.Info("已初始化联机页设置", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupLink.xaml.vb");
			ModMain.Hint("已初始化联机页设置！", ModMain.HintType.Green, Log: false);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "初始化联机页设置失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSetup\\PageSetupLink.xaml.vb");
			ProjectData.ClearProjectError();
		}
		Reload();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagesetup/pagesetuplink.xaml", UriKind.Relative);
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
			ComboLatencyMode = (MyComboBox)target;
			break;
		case 5:
			TextCustomPeer = (MyTextBox)target;
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
