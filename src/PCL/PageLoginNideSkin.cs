using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageLoginNideSkin : Grid, IComponentConnector
{
	[CompilerGenerated]
	[AccessedThroughProperty("PanData")]
	private Grid _PanData;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnExit")]
	private MyIconButton _BtnExit;

	[CompilerGenerated]
	[AccessedThroughProperty("Skin")]
	private MySkin _Skin;

	private bool _contentLoaded;

	internal virtual Grid PanData
	{
		[CompilerGenerated]
		get
		{
			return _PanData;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = PageLoginNideSkin_MouseEnter;
			MouseEventHandler value3 = PageLoginNideSkin_MouseLeave;
			Grid panData = _PanData;
			if (panData != null)
			{
				panData.MouseEnter -= value2;
				panData.MouseLeave -= value3;
			}
			_PanData = value;
			panData = _PanData;
			if (panData != null)
			{
				panData.MouseEnter += value2;
				panData.MouseLeave += value3;
			}
		}
	}

	[field: AccessedThroughProperty("TextName")]
	internal virtual TextBlock TextName
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextEmail")]
	internal virtual TextBlock TextEmail
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("BtnEdit")]
	internal virtual MyIconButton BtnEdit
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnExit
	{
		[CompilerGenerated]
		get
		{
			return _BtnExit;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				ExitLogin();
			};
			MyIconButton btnExit = _BtnExit;
			if (btnExit != null)
			{
				btnExit.Click -= obj;
			}
			_BtnExit = value;
			btnExit = _BtnExit;
			if (btnExit != null)
			{
				btnExit.Click += obj;
			}
		}
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
			MySkin.ClickEventHandler obj = Skin_Click;
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

	public PageLoginNideSkin()
	{
		base.Loaded += PageLoginLegacy_Loaded;
		InitializeComponent();
		Skin.Loader = PageLaunchLeft.SkinNide;
	}

	private void PageLoginLegacy_Loaded(object sender, RoutedEventArgs e)
	{
		Skin.Loader.Start();
	}

	public void Reload(bool KeepInput)
	{
		TextName.Text = Settings.Get<string>("CacheNideName");
		TextEmail.Text = Settings.Get<string>("CacheNideUsername");
		TextEmail.Visibility = ModBase.ToVisibility(!Settings.Get<bool>("UiLauncherEmail"));
	}

	public static ModLaunch.McLoginServer GetLoginData()
	{
		string text = (Information.IsNothing(ModMinecraft.McInstanceSelected) ? Settings.Get<string>("CacheNideServer") : Settings.Get<string>("VersionServerNide", ModMinecraft.McInstanceSelected));
		return new ModLaunch.McLoginServer(ModLaunch.McLoginType.Nide)
		{
			Token = "Nide",
			UserName = Settings.Get<string>("CacheNideUsername"),
			Password = Settings.Get<string>("CacheNidePass"),
			Description = "统一通行证",
			Type = ModLaunch.McLoginType.Nide,
			BaseUrl = "https://auth.mc-user.com:233/" + text + "/authserver"
		};
	}

	private void PageLoginNideSkin_MouseEnter(object sender, MouseEventArgs e)
	{
		ModAnimation.AniStart(new ModAnimation.AniData[6]
		{
			ModAnimation.AaOpacity(BtnEdit, 1.0 - BtnEdit.Opacity, 80),
			ModAnimation.AaHeight(BtnEdit, 25.5 - BtnEdit.Height, 140, 0, new ModAnimation.AniEaseOutFluent()),
			ModAnimation.AaHeight(BtnEdit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent()),
			ModAnimation.AaOpacity(BtnExit, 1.0 - BtnExit.Opacity, 80),
			ModAnimation.AaHeight(BtnExit, 25.5 - BtnExit.Height, 140, 0, new ModAnimation.AniEaseOutFluent()),
			ModAnimation.AaHeight(BtnExit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent())
		}, "PageLoginNideSkin Button");
	}

	private void PageLoginNideSkin_MouseLeave(object sender, MouseEventArgs e)
	{
		ModAnimation.AniStart(new ModAnimation.AniData[4]
		{
			ModAnimation.AaOpacity(BtnEdit, 0.0 - BtnEdit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent()),
			ModAnimation.AaHeight(BtnEdit, 14.0 - BtnEdit.Height, 120, 0, new ModAnimation.AniEaseInFluent()),
			ModAnimation.AaOpacity(BtnExit, 0.0 - BtnExit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent()),
			ModAnimation.AaHeight(BtnExit, 14.0 - BtnExit.Height, 120, 0, new ModAnimation.AniEaseInFluent())
		}, "PageLoginNideSkin Button");
	}

	public static void ExitLogin()
	{
		Settings.Set("CacheNideAccess", "");
		ModLaunch.McLoginNideLoader.Input = null;
		ModMain.FrmLaunchLeft.RefreshPage(KeepInput: false, Anim: true);
	}

	private void Skin_Click(object sender, MouseButtonEventArgs e)
	{
		ModBase.OpenWebsite("https://login.mc-user.com:233/" + (Information.IsNothing(ModMinecraft.McInstanceSelected) ? Settings.Get<string>("CacheNideServer") : Settings.Get<string>("VersionServerNide", ModMinecraft.McInstanceSelected)) + "/skin");
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginnideskin.xaml", UriKind.Relative);
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
			PanData = (Grid)target;
			break;
		case 2:
			TextName = (TextBlock)target;
			break;
		case 3:
			TextEmail = (TextBlock)target;
			break;
		case 4:
			BtnEdit = (MyIconButton)target;
			break;
		case 5:
			BtnExit = (MyIconButton)target;
			break;
		case 6:
			Skin = (MySkin)target;
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
