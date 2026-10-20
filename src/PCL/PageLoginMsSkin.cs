using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL;

[DesignerGenerated]
public class PageLoginMsSkin : Grid, IComponentConnector
{
	private bool IsChanging;

	[CompilerGenerated]
	[AccessedThroughProperty("PanData")]
	private Grid _PanData;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnSkin")]
	private MyIconButton _BtnSkin;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnEdit")]
	private MyIconButton _BtnEdit;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnExit")]
	private MyIconButton _BtnExit;

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
			MouseEventHandler value2 = ShowPanel;
			MouseEventHandler value3 = HidePanel;
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

	[field: AccessedThroughProperty("PanButtons")]
	internal virtual Border PanButtons
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnSkin
	{
		[CompilerGenerated]
		get
		{
			return _BtnSkin;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				BtnSkin_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton btnSkin = _BtnSkin;
			if (btnSkin != null)
			{
				btnSkin.Click -= obj;
			}
			_BtnSkin = value;
			btnSkin = _BtnSkin;
			if (btnSkin != null)
			{
				btnSkin.Click += obj;
			}
		}
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
				BtnExit_Click();
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

	[field: AccessedThroughProperty("Skin")]
	internal virtual MySkin Skin
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public PageLoginMsSkin()
	{
		base.Loaded += PageLoginLegacy_Loaded;
		IsChanging = false;
		InitializeComponent();
		Skin.Loader = PageLaunchLeft.SkinMs;
	}

	private void PageLoginLegacy_Loaded(object sender, RoutedEventArgs e)
	{
		Skin.Loader.Start();
	}

	public void Reload(bool KeepInput)
	{
		TextName.Text = Settings.Get<string>("CacheMsV2Name");
	}

	public static ModLaunch.McLoginMs GetLoginData()
	{
		if (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Finished)
		{
			return new ModLaunch.McLoginMs
			{
				OAuthRefreshToken = Settings.Get<string>("CacheMsV2OAuthRefresh"),
				UserName = Settings.Get<string>("CacheMsV2Name"),
				AccessToken = Settings.Get<string>("CacheMsV2Access"),
				Uuid = Settings.Get<string>("CacheMsV2Uuid"),
				ProfileJson = Settings.Get<string>("CacheMsV2ProfileJson")
			};
		}
		return new ModLaunch.McLoginMs
		{
			OAuthRefreshToken = Settings.Get<string>("CacheMsV2OAuthRefresh"),
			UserName = Settings.Get<string>("CacheMsV2Name")
		};
	}

	private void ShowPanel(object sender, MouseEventArgs e)
	{
		ModAnimation.AniStart(ModAnimation.AaOpacity(PanButtons, 1.0 - PanButtons.Opacity, 120), "PageLoginMsSkin Button");
	}

	public void HidePanel(object sender, EventArgs e)
	{
		if (!BtnEdit.ContextMenu.IsOpen && !BtnSkin.ContextMenu.IsOpen && !PanData.IsMouseOver)
		{
			ModAnimation.AniStart(ModAnimation.AaOpacity(PanButtons, 0.0 - PanButtons.Opacity, 120), "PageLoginMsSkin Button");
		}
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		BtnEdit.ContextMenu.IsOpen = true;
	}

	private void BtnExit_Click()
	{
		Settings.Set("CacheMsV2OAuthRefresh", "");
		Settings.Set("CacheMsV2Access", "");
		Settings.Set("CacheMsV2ProfileJson", "");
		Settings.Set("CacheMsV2Uuid", "");
		Settings.Set("CacheMsV2Name", "");
		Settings.Set("CacheMsV2Expires", 0L);
		ModLaunch.McLoginMsLoader.Cancel();
		ModMain.FrmLaunchLeft.RefreshPage(KeepInput: false, Anim: true);
	}

	private void BtnSkin_Click(object sender, RoutedEventArgs e)
	{
		BtnSkin.ContextMenu.IsOpen = true;
	}

	public void BtnSkinEdit_Click(object sender, RoutedEventArgs e)
	{
		if (IsChanging)
		{
			ModMain.Hint("正在更改皮肤中，请稍候！");
			return;
		}
		if (ModLaunch.McLoginLoader.State == ModBase.LoadState.Failed)
		{
			ModMain.Hint("登录失败，无法更改皮肤！", ModMain.HintType.Red);
			return;
		}
		ModMinecraft.McSkinInfo skinInfo = ModMinecraft.McSkinSelect();
		if (skinInfo.IsVaild)
		{
			ModMain.Hint("正在更改皮肤……");
			IsChanging = true;
			ModBase.RunInNewThread([SpecialName] () =>
			{
				EditSkin(skinInfo);
			}, "Ms Skin Upload");
		}
	}

	private void EditSkin(ModMinecraft.McSkinInfo SkinInfo)
	{
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_0115: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected O, but got Unknown
		while (true)
		{
			try
			{
				while (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Loading)
				{
					Thread.Sleep(10);
				}
				if (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Failed)
				{
					throw new Exception("登录失败", ModLaunch.McLoginMsLoader.Error);
				}
				string text = Settings.Get<string>("CacheMsV2Access");
				Settings.Get<string>("CacheMsV2Uuid");
				HttpMethod post = HttpMethod.Post;
				MultipartFormDataContent val = new MultipartFormDataContent();
				val.Add((HttpContent)new StringContent(SkinInfo.IsSlim ? "slim" : "classic"), "variant");
				val.Add((HttpContent)new ByteArrayContent(FileUtils.ReadAsBytes(SkinInfo.LocalFile, (Type)null)), "file", PathUtils.GetLastPart(SkinInfo.LocalFile));
				string text2 = ModNet.NetRequestByClientRetry("https://api.minecraftservices.com/minecraft/profile/skins", post, null, (object)val, null, "*/*", new string[3, 2]
				{
					{
						"Authorization",
						"Bearer " + text
					},
					{ "Accept", "*/*" },
					{ "User-Agent", "MojangSharp/0.1" }
				});
				if (text2.Contains("request requires user authentication"))
				{
					ModMain.Hint("正在重新登录，将在登录后自动更改皮肤……");
					ModLaunch.McLoginMsLoader.Start(GetLoginData(), IsForceRestart: true);
					continue;
				}
				Logger.Info(string.Format("皮肤修改返回值：{0}{1}", "\r\n", text2), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMsSkin.xaml.vb");
				JObject val2 = (JObject)StringExtensions.DeserializeJson(text2);
				if (val2.ContainsKey("errorMessage"))
				{
					throw new Exception(val2["errorMessage"].ToString());
				}
				foreach (JObject item in (IEnumerable<JToken>)val2["skins"])
				{
					JObject val3 = item;
					if (Operators.CompareString(val3["state"].ToString(), "ACTIVE", TextCompare: false) == 0)
					{
						MySkin.ReloadCache((string)val3["url"]);
						return;
					}
				}
				throw new Exception("未知错误（" + text2 + "）");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				if (ex2 is HttpRequestCodeException)
				{
					HttpRequestCodeException val4 = (HttpRequestCodeException)ex2;
					switch (val4.StatusCode)
					{
					case HttpStatusCode.BadRequest:
					{
						Logger.Warn(ex2, "更改皮肤时遭遇 400 错误", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMsSkin.xaml.vb");
						string response = val4.Response;
						if (response != null && response.Contains("\"error\""))
						{
							ModMain.Hint("更改皮肤失败：" + StringExtensions.DeserializeJson(val4.Response)[(object)"error"].ToString(), ModMain.HintType.Red);
							ProjectData.ClearProjectError();
							return;
						}
						string response2 = val4.Response;
						if (response2 != null && response2.Contains("\"errorMessage\""))
						{
							ModMain.Hint("更改皮肤失败：" + StringExtensions.DeserializeJson(val4.Response)[(object)"errorMessage"].ToString(), ModMain.HintType.Red);
							ProjectData.ClearProjectError();
							return;
						}
						break;
					}
					case HttpStatusCode.Unauthorized:
						Logger.Warn(ex2, "更改皮肤时遭遇 401 错误", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMsSkin.xaml.vb");
						ModMain.Hint("正在重新登录，将在登录后自动更改皮肤……");
						ModLaunch.McLoginMsLoader.Start(GetLoginData(), IsForceRestart: true);
						ProjectData.ClearProjectError();
						goto end_IL_0212;
					}
				}
				else if (ExceptionExtensions.IsBadNetwork(ex2))
				{
					ModMain.Hint("更改皮肤失败：连接 Mojang 服务器超时，请稍后再试，或使用 VPN 改善网络环境", ModMain.HintType.Red);
				}
				else
				{
					Logger.Error(ex2, "更改皮肤失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMsSkin.xaml.vb");
				}
				ProjectData.ClearProjectError();
				break;
				end_IL_0212:;
			}
			finally
			{
				IsChanging = false;
			}
		}
	}

	public void BtnSkinSave_Click(object sender, RoutedEventArgs e)
	{
		Skin.BtnSkinSave_Click();
	}

	public void BtnSkinRefresh_Click(object sender, RoutedEventArgs e)
	{
		Skin.RefreshClick();
	}

	public void BtnSkinCape_Click(object sender, RoutedEventArgs e)
	{
		Skin.BtnSkinCape_Click();
	}

	public void BtnSkinCapeRefresh_Click(object sender, RoutedEventArgs e)
	{
		ModBase.RunInThread([SpecialName] () =>
		{
			try
			{
				ModMain.Hint("正在刷新披风列表……");
				if (ModLaunch.McLaunchLoader.State == ModBase.LoadState.Loading)
				{
					ModLaunch.McLoginMsLoader.WaitForExit();
				}
				else
				{
					ModLaunch.McLoginMsLoader.WaitForExit(GetLoginData(), null, IsForceRestart: true);
				}
				ModMain.Hint("已刷新披风列表！", ModMain.HintType.Green);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "刷新披风列表失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMsSkin.xaml.vb");
				ProjectData.ClearProjectError();
			}
		});
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginmsskin.xaml", UriKind.Relative);
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
			PanButtons = (Border)target;
			break;
		case 4:
			BtnSkin = (MyIconButton)target;
			break;
		case 5:
			((ContextMenu)target).Closed += HidePanel;
			break;
		case 6:
			BtnEdit = (MyIconButton)target;
			break;
		case 7:
			((ContextMenu)target).Closed += HidePanel;
			break;
		case 8:
			BtnExit = (MyIconButton)target;
			break;
		case 9:
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
