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
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageLoginAuthSkin : Grid, IComponentConnector
{
	[CompilerGenerated]
	[AccessedThroughProperty("PanData")]
	private Grid _PanData;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnEdit")]
	private MyIconButton _BtnEdit;

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
			MouseEventHandler value2 = PageLoginAuthSkin_MouseEnter;
			MouseEventHandler value3 = PageLoginAuthSkin_MouseLeave;
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

	internal virtual MyIconButton BtnEdit
	{
		[CompilerGenerated]
		get
		{
			return _BtnEdit;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = BtnEdit_Click;
			MyIconButton btnEdit = _BtnEdit;
			if (btnEdit != null)
			{
				btnEdit.Click -= obj;
			}
			_BtnEdit = value;
			btnEdit = _BtnEdit;
			if (btnEdit != null)
			{
				btnEdit.Click += obj;
			}
		}
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

	public PageLoginAuthSkin()
	{
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			PageLoginLegacy_Loaded();
		};
		InitializeComponent();
		Skin.Loader = PageLaunchLeft.SkinAuth;
	}

	private void PageLoginLegacy_Loaded()
	{
		Skin.Loader.Start();
	}

	public void Reload(bool KeepInput)
	{
		TextName.Text = Settings.Get<string>("CacheAuthName");
		TextEmail.Text = Settings.Get<string>("CacheAuthUsername");
		TextEmail.Visibility = (Settings.Get<bool>("UiLauncherEmail") ? Visibility.Collapsed : Visibility.Visible);
		PageLoginLegacy_Loaded();
	}

	public static ModLaunch.McLoginServer GetLoginData()
	{
		string baseUrl = (Information.IsNothing(ModMinecraft.McInstanceSelected) ? Settings.Get<string>("CacheAuthServerServer") : Settings.Get<string>("VersionServerAuthServer", ModMinecraft.McInstanceSelected)) + "/authserver";
		return new ModLaunch.McLoginServer(ModLaunch.McLoginType.Auth)
		{
			Token = "Auth",
			BaseUrl = baseUrl,
			UserName = Settings.Get<string>("CacheAuthUsername"),
			Password = Settings.Get<string>("CacheAuthPass"),
			Description = "Authlib-Injector",
			Type = ModLaunch.McLoginType.Auth
		};
	}

	private void PageLoginAuthSkin_MouseEnter(object sender, MouseEventArgs e)
	{
		ModAnimation.AniStart(new ModAnimation.AniData[6]
		{
			ModAnimation.AaOpacity(BtnEdit, 1.0 - BtnEdit.Opacity, 80),
			ModAnimation.AaHeight(BtnEdit, 25.5 - BtnEdit.Height, 140, 0, new ModAnimation.AniEaseOutFluent()),
			ModAnimation.AaHeight(BtnEdit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent()),
			ModAnimation.AaOpacity(BtnExit, 1.0 - BtnExit.Opacity, 80),
			ModAnimation.AaHeight(BtnExit, 25.5 - BtnExit.Height, 140, 0, new ModAnimation.AniEaseOutFluent()),
			ModAnimation.AaHeight(BtnExit, -1.5, 50, 140, new ModAnimation.AniEaseInFluent())
		}, "PageLoginAuthSkin Button");
	}

	private void PageLoginAuthSkin_MouseLeave(object sender, MouseEventArgs e)
	{
		ModAnimation.AniStart(new ModAnimation.AniData[4]
		{
			ModAnimation.AaOpacity(BtnEdit, 0.0 - BtnEdit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent()),
			ModAnimation.AaHeight(BtnEdit, 14.0 - BtnEdit.Height, 120, 0, new ModAnimation.AniEaseInFluent()),
			ModAnimation.AaOpacity(BtnExit, 0.0 - BtnExit.Opacity, 120, 0, new ModAnimation.AniEaseOutFluent()),
			ModAnimation.AaHeight(BtnExit, 14.0 - BtnExit.Height, 120, 0, new ModAnimation.AniEaseInFluent())
		}, "PageLoginAuthSkin Button");
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		if (ModLaunch.McLoginLoader.State == ModBase.LoadState.Loading)
		{
			Logger.Warn("要求更换角色，但登录加载器繁忙", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginAuthSkin.xaml.vb");
			if (ModLaunch.McLoginLoader.Input is ModLaunch.McLoginServer && ((ModLaunch.McLoginServer)ModLaunch.McLoginLoader.Input).ForceReselectProfile)
			{
				ModMain.Hint("正在尝试更换，请稍候！");
			}
			else
			{
				ModMain.Hint("正在登录中，请稍后再更换角色！", ModMain.HintType.Red);
			}
			return;
		}
		ModMain.Hint("正在尝试更换，请稍候！");
		Settings.Set("CacheAuthUuid", "");
		Settings.Set("CacheAuthName", "");
		ModBase.RunInThread([SpecialName] () =>
		{
			try
			{
				ModLaunch.McLoginServer loginData = GetLoginData();
				loginData.ForceReselectProfile = true;
				ModLaunch.McLoginLoader.WaitForExit(loginData, null, IsForceRestart: true);
				ModBase.RunInUi([SpecialName] () =>
				{
					Reload(KeepInput: true);
				});
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "更换角色失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginAuthSkin.xaml.vb");
				ProjectData.ClearProjectError();
			}
		});
	}

	public static void ExitLogin()
	{
		Settings.Set("CacheAuthAccess", "");
		Settings.Set("CacheAuthUuid", "");
		Settings.Set("CacheAuthName", "");
		ModLaunch.McLoginAuthLoader.Input = null;
		ModMain.FrmLaunchLeft.RefreshPage(KeepInput: false, Anim: true);
	}

	private void Skin_Click(object sender, MouseButtonEventArgs e)
	{
		string text = (((object)ModMinecraft.McInstanceSelected != null) ? Settings.Get<string>("VersionServerAuthRegister", ModMinecraft.McInstanceSelected) : Settings.Get<string>("CacheAuthServerRegister"));
		if (string.IsNullOrEmpty(new ValidateHttp().Validate(text)))
		{
			ModBase.OpenWebsite(text.Replace("/auth/register", "/user/closet"));
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginauthskin.xaml", UriKind.Relative);
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
