using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL;

[DesignerGenerated]
public class PageLoginMs : StackPanel, IComponentConnector
{
	[CompilerGenerated]
	[AccessedThroughProperty("ComboAccounts")]
	private MyComboBox _ComboAccounts;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnLogin")]
	private MyButton _BtnLogin;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("PanEmpty")]
	internal virtual Grid PanEmpty
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyComboBox ComboAccounts
	{
		[CompilerGenerated]
		get
		{
			return _ComboAccounts;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SelectionChangedEventHandler value2 = ComboAccounts_SelectionChanged;
			MyComboBox comboAccounts = _ComboAccounts;
			if (comboAccounts != null)
			{
				comboAccounts.SelectionChanged -= value2;
			}
			_ComboAccounts = value;
			comboAccounts = _ComboAccounts;
			if (comboAccounts != null)
			{
				comboAccounts.SelectionChanged += value2;
			}
		}
	}

	internal virtual MyButton BtnLogin
	{
		[CompilerGenerated]
		get
		{
			return _BtnLogin;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnLogin_Click;
			MyButton btnLogin = _BtnLogin;
			if (btnLogin != null)
			{
				btnLogin.Click -= obj;
			}
			_BtnLogin = value;
			btnLogin = _BtnLogin;
			if (btnLogin != null)
			{
				btnLogin.Click += obj;
			}
		}
	}

	public PageLoginMs()
	{
		InitializeComponent();
	}

	public void Reload(bool KeepInput)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		int selectedIndex = ComboAccounts.SelectedIndex;
		ComboAccounts.Items.Clear();
		ComboAccounts.Items.Add(new MyComboBoxItem
		{
			Content = "添加新账号"
		});
		try
		{
			JObject val = (JObject)StringExtensions.DeserializeJson(Settings.Get<string>("LoginMsJson"));
			foreach (KeyValuePair<string, JToken> item in val)
			{
				MyListItem myListItem = (MyListItem)((DataTemplate)FindResource("ComboBoxItemTemplateWithDelete")).LoadContent();
				myListItem.Tag = item.Value.ToString();
				myListItem.Title = item.Key;
				myListItem.Buttons.ElementAtOrDefault(0).Tag = item.Key;
				ComboAccounts.Items.Add(myListItem);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "微软登录信息出错，登录信息已被重置", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMs.xaml.vb");
			Settings.Set("LoginMsJson", "{}");
			ProjectData.ClearProjectError();
		}
		ComboAccounts.SelectedIndex = (KeepInput ? Math.Max(0, selectedIndex) : 0);
	}

	private void ComboAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (ModAnimation.AniControlEnabled == 0 && ComboAccounts.SelectedItem != null && ComboAccounts.ContentPresenter != null)
		{
			if (ComboAccounts.SelectedItem is MyListItem)
			{
				ComboAccounts.ContentPresenter.Content = ((MyListItem)ComboAccounts.SelectedItem).Title;
			}
			else if (ComboAccounts.SelectedItem is MyComboBoxItem)
			{
				ComboAccounts.ContentPresenter.Content = RuntimeHelpers.GetObjectValue(((MyComboBoxItem)ComboAccounts.SelectedItem).Content);
			}
		}
	}

	public static ModLaunch.McLoginMs GetLoginData()
	{
		if (ModMain.FrmLoginMs == null)
		{
			return new ModLaunch.McLoginMs
			{
				OAuthRefreshToken = Settings.Get<string>("CacheMsV2OAuthRefresh"),
				UserName = Settings.Get<string>("CacheMsV2Name")
			};
		}
		ModLaunch.McLoginMs result = null;
		ModBase.RunInUiWait([SpecialName] () =>
		{
			if (ModMain.FrmLoginMs.ComboAccounts.SelectedIndex == 0)
			{
				result = new ModLaunch.McLoginMs();
			}
			else
			{
				MyListItem myListItem = (MyListItem)ModMain.FrmLoginMs.ComboAccounts.SelectedItem;
				result = new ModLaunch.McLoginMs
				{
					OAuthRefreshToken = Conversions.ToString(myListItem.Tag),
					UserName = myListItem.Title
				};
			}
		});
		return result;
	}

	public static string IsVaild(ModLaunch.McLoginMs LoginData)
	{
		if (Operators.CompareString(LoginData.OAuthRefreshToken, "", TextCompare: false) == 0)
		{
			return "请在登录账号后再启动游戏！";
		}
		return "";
	}

	public string IsVaild()
	{
		return IsVaild(GetLoginData());
	}

	private void BtnLogin_Click(object sender, EventArgs e)
	{
		ComboAccounts.IsEnabled = false;
		BtnLogin.IsEnabled = false;
		BtnLogin.Text = "0%";
		ModBase.RunInNewThread([SpecialName] () =>
		{
			try
			{
				ModLaunch.McLoginMsLoader.Start(GetLoginData(), IsForceRestart: true);
				while (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Loading)
				{
					ModBase.RunInUi([SpecialName] () =>
					{
						BtnLogin.Text = Conversions.ToString(Math.Round(ModLaunch.McLoginMsLoader.Progress * 100.0)) + "%";
					});
					Thread.Sleep(50);
				}
				if (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Finished)
				{
					ModBase.RunInUi([SpecialName] () =>
					{
						ModMain.FrmLaunchLeft.RefreshPage(KeepInput: false, Anim: true);
					});
					return;
				}
				if (ModLaunch.McLoginMsLoader.State == ModBase.LoadState.Canceled)
				{
					throw new OperationCanceledException();
				}
				if (ModLaunch.McLoginMsLoader.Error == null)
				{
					throw new Exception("未知错误！");
				}
				throw new Exception(ModLaunch.McLoginMsLoader.Error.Message, ModLaunch.McLoginMsLoader.Error);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				if (!ExceptionExtensions.IsCanceled(ex2))
				{
					if (StringExtensions.StartsWithF(ex2.Message, "$", false))
					{
						ModMain.Hint(ex2.Message.TrimStart('$'), ModMain.HintType.Red);
					}
					else if (ex2 is AuthenticationException && ex2.Message.Contains("SSL/TLS"))
					{
						Logger.Error(ex2, string.Format("正版登录验证失败，请考虑在 [设置 → 其他] 中关闭 [在正版登录时验证 SSL 证书]，然后再试。{0}{1}原始错误信息：", "\r\n", "\r\n"), (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMs.xaml.vb");
					}
					else
					{
						Logger.Error(ex2, "正版登录尝试失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLoginMs.xaml.vb");
					}
				}
				ProjectData.ClearProjectError();
			}
			finally
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					ComboAccounts.IsEnabled = true;
					BtnLogin.IsEnabled = true;
					BtnLogin.Text = "登录";
				});
			}
		}, "Ms Login");
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginms.xaml", UriKind.Relative);
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
			PanEmpty = (Grid)target;
			break;
		case 2:
			ComboAccounts = (MyComboBox)target;
			break;
		case 3:
			BtnLogin = (MyButton)target;
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
