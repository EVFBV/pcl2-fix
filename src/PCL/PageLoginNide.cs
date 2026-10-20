using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MeloongCore.Extensions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageLoginNide : Grid, IComponentConnector
{
	private bool IsFirstLoad;

	[CompilerGenerated]
	[AccessedThroughProperty("ComboName")]
	private MyComboBox _ComboName;

	[CompilerGenerated]
	[AccessedThroughProperty("TextPass")]
	private PasswordBox _TextPass;

	[CompilerGenerated]
	[AccessedThroughProperty("CheckRemember")]
	private MyCheckBox _CheckRemember;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnLink")]
	private MyTextButton _BtnLink;

	private bool _contentLoaded;

	internal virtual MyComboBox ComboName
	{
		[CompilerGenerated]
		get
		{
			return _ComboName;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyComboBox.TextChangedEventHandler obj = ComboName_TextChanged;
			SelectionChangedEventHandler value2 = [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
			{
				ComboName_SelectionChanged((MyComboBox)a0, a1);
			};
			MyComboBox.TextChangedEventHandler obj2 = [SpecialName] (object a0, TextChangedEventArgs a1) =>
			{
				ComboName_TextChanged();
			};
			MyComboBox comboName = _ComboName;
			if (comboName != null)
			{
				comboName.TextChanged -= obj;
				comboName.SelectionChanged -= value2;
				comboName.TextChanged -= obj2;
			}
			_ComboName = value;
			comboName = _ComboName;
			if (comboName != null)
			{
				comboName.TextChanged += obj;
				comboName.SelectionChanged += value2;
				comboName.TextChanged += obj2;
			}
		}
	}

	internal virtual PasswordBox TextPass
	{
		[CompilerGenerated]
		get
		{
			return _TextPass;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			RoutedEventHandler value2 = TextPass_PasswordChanged;
			PasswordBox textPass = _TextPass;
			if (textPass != null)
			{
				textPass.PasswordChanged -= value2;
			}
			_TextPass = value;
			textPass = _TextPass;
			if (textPass != null)
			{
				textPass.PasswordChanged += value2;
			}
		}
	}

	internal virtual MyCheckBox CheckRemember
	{
		[CompilerGenerated]
		get
		{
			return _CheckRemember;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyCheckBox.ChangeEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				CheckBoxChange((MyCheckBox)a0, a1);
			};
			MyCheckBox checkRemember = _CheckRemember;
			if (checkRemember != null)
			{
				checkRemember.Change -= obj;
			}
			_CheckRemember = value;
			checkRemember = _CheckRemember;
			if (checkRemember != null)
			{
				checkRemember.Change += obj;
			}
		}
	}

	internal virtual MyTextButton BtnLink
	{
		[CompilerGenerated]
		get
		{
			return _BtnLink;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyTextButton.ClickEventHandler obj = Btn_Click;
			MyTextButton btnLink = _BtnLink;
			if (btnLink != null)
			{
				btnLink.Click -= obj;
			}
			_BtnLink = value;
			btnLink = _BtnLink;
			if (btnLink != null)
			{
				btnLink.Click += obj;
			}
		}
	}

	public PageLoginNide()
	{
		IsFirstLoad = true;
		InitializeComponent();
	}

	public void Reload(bool KeepInput)
	{
		CheckRemember.Checked = Settings.Get<bool>("LoginRemember");
		if (KeepInput && !IsFirstLoad)
		{
			string text = ComboName.Text;
			ComboName.ItemsSource = ((Operators.CompareString(Settings.Get<string>("LoginNideEmail"), "", TextCompare: false) == 0) ? null : StringExtensions.Split(Settings.Get<string>("LoginNideEmail").ToString(), "\u00a8", false));
			ComboName.Text = text;
		}
		else if (Operators.CompareString(Settings.Get<string>("LoginNideEmail"), "", TextCompare: false) == 0)
		{
			ComboName.ItemsSource = null;
		}
		else
		{
			ComboName.ItemsSource = StringExtensions.Split(Settings.Get<string>("LoginNideEmail"), "\u00a8", false);
			ComboName.Text = StringExtensions.BeforeFirst(Settings.Get<string>("LoginNideEmail"), "\u00a8", false);
			if (Settings.Get<bool>("LoginRemember"))
			{
				TextPass.Password = StringExtensions.BeforeFirst(Settings.Get<string>("LoginNidePass"), "\u00a8", false).Trim();
			}
		}
		IsFirstLoad = false;
	}

	public static ModLaunch.McLoginServer GetLoginData()
	{
		string text = (Information.IsNothing(ModMinecraft.McInstanceSelected) ? Settings.Get<string>("CacheNideServer") : Settings.Get<string>("VersionServerNide", ModMinecraft.McInstanceSelected));
		if (ModMain.FrmLoginNide == null)
		{
			return new ModLaunch.McLoginServer(ModLaunch.McLoginType.Nide)
			{
				Token = "Nide",
				UserName = "",
				Password = "",
				Description = "统一通行证",
				Type = ModLaunch.McLoginType.Nide,
				BaseUrl = "https://auth.mc-user.com:233/" + text + "/authserver"
			};
		}
		return new ModLaunch.McLoginServer(ModLaunch.McLoginType.Nide)
		{
			Token = "Nide",
			UserName = ModMain.FrmLoginNide.ComboName.Text.Replace("\u00a8", "").Trim(),
			Password = ModMain.FrmLoginNide.TextPass.Password.Replace("\u00a8", "").Trim(),
			Description = "统一通行证",
			Type = ModLaunch.McLoginType.Nide,
			BaseUrl = "https://auth.mc-user.com:233/" + text + "/authserver"
		};
	}

	public static string IsVaild(ModLaunch.McLoginServer LoginData)
	{
		if (Operators.CompareString(LoginData.UserName, "", TextCompare: false) == 0)
		{
			return "账号不能为空！";
		}
		if (Operators.CompareString(LoginData.Password, "", TextCompare: false) == 0)
		{
			return "密码不能为空！";
		}
		return "";
	}

	public string IsVaild()
	{
		return IsVaild(GetLoginData());
	}

	private void ComboName_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(sender, null, "Text", new object[0], null, null, null), "", TextCompare: false))
		{
			TextPass.Password = "";
		}
		if (ModAnimation.AniControlEnabled == 0)
		{
			Settings.Set("CacheNideAccess", "");
		}
	}

	private void TextPass_PasswordChanged(object sender, RoutedEventArgs e)
	{
		if (ModAnimation.AniControlEnabled == 0)
		{
			Settings.Set("CacheNideAccess", "");
		}
	}

	private void ComboName_SelectionChanged(MyComboBox sender, SelectionChangedEventArgs e)
	{
		if (sender.SelectedIndex == -1 || !Settings.Get<bool>("LoginRemember"))
		{
			TextPass.Password = "";
		}
		else
		{
			TextPass.Password = StringExtensions.Split(Settings.Get<string>("LoginNidePass"), "\u00a8", false)[sender.SelectedIndex].Trim();
		}
	}

	private void CheckBoxChange(MyCheckBox sender, object e)
	{
		if (ModAnimation.AniControlEnabled == 0)
		{
			Settings.Set(Conversions.ToString(sender.Tag), sender.Checked);
		}
	}

	private void ComboName_TextChanged()
	{
		BtnLink.Content = ((Operators.CompareString(ComboName.Text, "", TextCompare: false) == 0) ? "注册账号" : "找回密码");
	}

	private void Btn_Click(object sender, EventArgs e)
	{
		if (Operators.ConditionalCompareObjectEqual(BtnLink.Content, "注册账号", TextCompare: false))
		{
			ModBase.OpenWebsite("https://login.mc-user.com:233/" + Settings.Get<string>("VersionServerNide", ModMinecraft.McInstanceSelected) + "/register");
		}
		else
		{
			ModBase.OpenWebsite("https://login.mc-user.com:233/account/login");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pageloginnide.xaml", UriKind.Relative);
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
			ComboName = (MyComboBox)target;
			break;
		case 2:
			TextPass = (PasswordBox)target;
			break;
		case 3:
			CheckRemember = (MyCheckBox)target;
			break;
		case 4:
			BtnLink = (MyTextButton)target;
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
