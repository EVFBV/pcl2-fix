using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL;

[DesignerGenerated]
public class MyMsgLogin : Grid, IComponentConnector
{
	private JObject Data;

	private string UserCode;

	private string DeviceCode;

	private string Website;

	private readonly ModMain.MyMsgBoxConverter MyConverter;

	private readonly int Uuid;

	[CompilerGenerated]
	[AccessedThroughProperty("PanBorder")]
	private Border _PanBorder;

	[CompilerGenerated]
	[AccessedThroughProperty("LabTitle")]
	private TextBlock _LabTitle;

	[CompilerGenerated]
	[AccessedThroughProperty("Btn1")]
	private MyButton _Btn1;

	[CompilerGenerated]
	[AccessedThroughProperty("Btn3")]
	private MyButton _Btn3;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("TransformRotate")]
	internal virtual RotateTransform TransformRotate
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TransformPos")]
	internal virtual TranslateTransform TransformPos
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Border PanBorder
	{
		[CompilerGenerated]
		get
		{
			return _PanBorder;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = Drag;
			Border panBorder = _PanBorder;
			if (panBorder != null)
			{
				panBorder.MouseLeftButtonDown -= value2;
			}
			_PanBorder = value;
			panBorder = _PanBorder;
			if (panBorder != null)
			{
				panBorder.MouseLeftButtonDown += value2;
			}
		}
	}

	[field: AccessedThroughProperty("EffectShadow")]
	internal virtual DropShadowEffect EffectShadow
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanMain")]
	internal virtual Grid PanMain
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual TextBlock LabTitle
	{
		[CompilerGenerated]
		get
		{
			return _LabTitle;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = Drag;
			TextBlock labTitle = _LabTitle;
			if (labTitle != null)
			{
				labTitle.MouseLeftButtonDown -= value2;
			}
			_LabTitle = value;
			labTitle = _LabTitle;
			if (labTitle != null)
			{
				labTitle.MouseLeftButtonDown += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ShapeLine")]
	internal virtual Rectangle ShapeLine
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanCaption")]
	internal virtual MyScrollViewer PanCaption
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabCaption")]
	internal virtual TextBlock LabCaption
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanBtn")]
	internal virtual StackPanel PanBtn
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual MyButton Btn1
	{
		[CompilerGenerated]
		get
		{
			return _Btn1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Btn1_Click();
			};
			MyButton btn = _Btn1;
			if (btn != null)
			{
				btn.Click -= obj;
			}
			_Btn1 = value;
			btn = _Btn1;
			if (btn != null)
			{
				btn.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("Btn2")]
	public virtual MyButton Btn2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public virtual MyButton Btn3
	{
		[CompilerGenerated]
		get
		{
			return _Btn3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Btn3_Click();
			};
			MyButton btn = _Btn3;
			if (btn != null)
			{
				btn.Click -= obj;
			}
			_Btn3 = value;
			btn = _Btn3;
			if (btn != null)
			{
				btn.Click += obj;
			}
		}
	}

	public MyMsgLogin(ModMain.MyMsgBoxConverter Converter)
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		base.Loaded += Load;
		Uuid = ModBase.GetUuid();
		try
		{
			InitializeComponent();
			Btn1.Name += Conversions.ToString(ModBase.GetUuid());
			Btn2.Name += Conversions.ToString(ModBase.GetUuid());
			Btn3.Name += Conversions.ToString(ModBase.GetUuid());
			MyConverter = Converter;
			ShapeLine.StrokeThickness = ModBase.GetWPFSize(1.0);
			Data = (JObject)Converter.Content;
			Init();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "登录弹窗初始化失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\MyMsgLogin.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void Load(object sender, EventArgs e)
	{
		try
		{
			base.Opacity = 0.0;
			ModAnimation.AniStart(ModAnimation.AaColor(ModMain.FrmMain.PanMsg, Panel.BackgroundProperty, (MyConverter.IsWarn ? new ModBase.MyColor(140.0, 80.0, 0.0, 0.0) : new ModBase.MyColor(90.0, 0.0, 0.0, 0.0)) - ModMain.FrmMain.PanMsg.Background, 200), "PanMsg Background");
			ModAnimation.AniStart(new ModAnimation.AniData[3]
			{
				ModAnimation.AaOpacity(this, 1.0, 120, 60),
				ModAnimation.AaDouble([SpecialName] (object i) =>
				{
					TranslateTransform transformPos;
					(transformPos = TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
				}, 0.0 - TransformPos.Y, 300, 60, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaDouble([SpecialName] (object i) =>
				{
					RotateTransform transformRotate;
					(transformRotate = TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
				}, 0.0 - TransformRotate.Angle, 300, 60, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak))
			}, "MyMsgBox " + Conversions.ToString(Uuid));
			Logger.Info(string.Format("登录弹窗：{0}{1}{2}", LabTitle.Text, "\r\n", LabCaption.Text), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\MyMsgLogin.xaml.vb");
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "登录弹窗加载失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\MyMsgLogin.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void Close()
	{
		ModAnimation.AniStart(new ModAnimation.AniData[5]
		{
			ModAnimation.AaCode([SpecialName] () =>
			{
				if (!ModMain.WaitingMyMsgBox.Any())
				{
					ModAnimation.AniStart(ModAnimation.AaColor(ModMain.FrmMain.PanMsg, Panel.BackgroundProperty, new ModBase.MyColor(0.0, 0.0, 0.0, 0.0) - ModMain.FrmMain.PanMsg.Background, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)));
				}
			}, 30),
			ModAnimation.AaOpacity(this, 0.0 - base.Opacity, 80, 20),
			ModAnimation.AaDouble([SpecialName] (object i) =>
			{
				TranslateTransform transformPos;
				(transformPos = TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
			}, 20.0 - TransformPos.Y, 150, 0, new ModAnimation.AniEaseOutFluent()),
			ModAnimation.AaDouble([SpecialName] (object i) =>
			{
				RotateTransform transformRotate;
				(transformRotate = TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
			}, 6.0 - TransformRotate.Angle, 150, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak)),
			ModAnimation.AaCode([SpecialName] () =>
			{
				((Grid)base.Parent).Children.Remove(this);
			}, 0, After: true)
		}, "MyMsgBox " + Conversions.ToString(Uuid));
	}

	public void Btn1_Click()
	{
	}

	public void Btn3_Click()
	{
		Finished(new OperationCanceledException());
	}

	private void Drag(object sender, MouseButtonEventArgs e)
	{
		int try0000_dispatch = -1;
		int num3 = default(int);
		int num2 = default(int);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0000_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = 1;
					goto IL_0007;
				case 86:
					{
						num2 = num;
						switch (num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0000;
						}
						int num4 = num2 + 1;
						num2 = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_0007;
						case 3:
							goto end_IL_0000_2;
						default:
							goto end_IL_0000;
						case 4:
							goto end_IL_0000_3;
						}
						goto default;
					}
					IL_0007:
					num = 2;
					if (!(e.GetPosition(ShapeLine).Y <= 2.0))
					{
						goto end_IL_0000_3;
					}
					break;
					end_IL_0000_2:
					break;
				}
				num = 3;
				ModMain.FrmMain.DragMove();
				break;
				end_IL_0000:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num2 == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0000_dispatch = 86;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0000_3:
			break;
		}
		if (num2 != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void Finished(object Result)
	{
		if (!MyConverter.IsExited)
		{
			MyConverter.IsExited = true;
			MyConverter.Result = RuntimeHelpers.GetObjectValue(Result);
			ModBase.RunInUi(Close);
			Thread.Sleep(200);
			ModMain.FrmMain.ShowWindowToTop();
		}
	}

	private void Init()
	{
		UserCode = (string)Data["user_code"];
		DeviceCode = (string)Data["device_code"];
		Website = (string)Data["verification_uri"];
		LabTitle.Text = "登录 Minecraft";
		LabCaption.Text = $"登录网页将自动开启，请在网页中输入 {UserCode}（已自动复制）。" + "\r\n\r\n如果网络环境不佳，网页可能一直加载不出来，届时请使用加速器或 VPN 改善网络环境。\r\n" + $"你也可以用其他设备打开 {Website} 并输入上述代码。";
		CustomEventService.SetEventData(Btn1, Website);
		CustomEventService.SetEventData(Btn2, UserCode);
		ModBase.RunInNewThread(WorkThread, "MyMsgLogin");
	}

	private void WorkThread()
	{
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		Thread.Sleep(2000);
		if (MyConverter.IsExited)
		{
			return;
		}
		ModBase.OpenWebsite(Website);
		ModBase.ClipboardSet(UserCode);
		checked
		{
			Thread.Sleep((Data["interval"].ToObject<int>() - 1) * 1000);
			int num = 0;
			while (!MyConverter.IsExited)
			{
				try
				{
					JObject val = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClient("https://login.microsoftonline.com/consumers/oauth2/v2.0/token", HttpMethod.Post, "grant_type=urn:ietf:params:oauth:grant-type:device_code&client_id=fe72edc2-3a6f-4280-90e8-e2beb64ce7e1&device_code=" + DeviceCode + "&scope=XboxLive.signin%20offline_access", "application/x-www-form-urlencoded", "*/*", 5000 + num * 5000, null, RequireJson: true, null, SimulateBrowserHeaders: false, MakeLog: false));
					ModLaunch.McLaunchLog(string.Format("令牌过期时间：{0} 秒", val["expires_in"]), (LogLevel)1);
					ModMain.Hint("网页登录成功！", ModMain.HintType.Green);
					Finished(new string[2]
					{
						val["access_token"].ToString(),
						val["refresh_token"].ToString()
					});
					break;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (!(ex2 is HttpRequestCodeException))
					{
						goto IL_01fb;
					}
					string response = ((HttpRequestCodeException)ex2).Response;
					if (response.Contains("authorization_declined"))
					{
						Finished(new Exception("$你拒绝了 PCL 申请的权限……"));
						ProjectData.ClearProjectError();
						break;
					}
					if (response.Contains("expired_token"))
					{
						Finished(new Exception("$登录用时太长啦，重新试试吧！"));
						ProjectData.ClearProjectError();
						break;
					}
					if (response.Contains("Account security interrupt"))
					{
						Finished(new Exception("$该账号由于安全问题无法登陆，请前往微软账户页获取更多信息。"));
						ProjectData.ClearProjectError();
						break;
					}
					if (response.Contains("service abuse"))
					{
						Finished(new Exception("$非常抱歉，该账号已被微软封禁，无法登录。"));
						ProjectData.ClearProjectError();
						break;
					}
					if (response.Contains("AADSTS70000"))
					{
						Finished(new ModBase.RestartException());
						ProjectData.ClearProjectError();
						break;
					}
					if (!response.Contains("authorization_pending"))
					{
						goto IL_01fb;
					}
					Thread.Sleep(2000);
					ProjectData.ClearProjectError();
					goto end_IL_0104;
					IL_01fb:
					if (num <= 2)
					{
						num++;
						Logger.Warn(ex2, $"登录轮询第 {num} 次失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\MyMsgLogin.xaml.vb");
						Thread.Sleep(2000);
						ProjectData.ClearProjectError();
						continue;
					}
					Finished(new Exception("登录轮询失败", ex2));
					ProjectData.ClearProjectError();
					break;
					end_IL_0104:;
				}
			}
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/mymsglogin.xaml", UriKind.Relative);
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
			TransformRotate = (RotateTransform)target;
			break;
		case 2:
			TransformPos = (TranslateTransform)target;
			break;
		case 3:
			PanBorder = (Border)target;
			break;
		case 4:
			EffectShadow = (DropShadowEffect)target;
			break;
		case 5:
			PanMain = (Grid)target;
			break;
		case 6:
			LabTitle = (TextBlock)target;
			break;
		case 7:
			ShapeLine = (Rectangle)target;
			break;
		case 8:
			PanCaption = (MyScrollViewer)target;
			break;
		case 9:
			LabCaption = (TextBlock)target;
			break;
		case 10:
			PanBtn = (StackPanel)target;
			break;
		case 11:
			Btn1 = (MyButton)target;
			break;
		case 12:
			Btn2 = (MyButton)target;
			break;
		case 13:
			Btn3 = (MyButton)target;
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
