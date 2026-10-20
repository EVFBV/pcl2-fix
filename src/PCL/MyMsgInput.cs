using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class MyMsgInput : Grid, IComponentConnector
{
	private readonly ModMain.MyMsgBoxConverter MyConverter;

	private readonly int Uuid;

	[CompilerGenerated]
	[AccessedThroughProperty("PanBorder")]
	private Border _PanBorder;

	[CompilerGenerated]
	[AccessedThroughProperty("LabTitle")]
	private TextBlock _LabTitle;

	[CompilerGenerated]
	[AccessedThroughProperty("TextArea")]
	private MyTextBox _TextArea;

	[CompilerGenerated]
	[AccessedThroughProperty("Btn1")]
	private MyButton _Btn1;

	[CompilerGenerated]
	[AccessedThroughProperty("Btn2")]
	private MyButton _Btn2;

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

	[field: AccessedThroughProperty("PanText")]
	internal virtual MyScrollViewer PanText
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabText")]
	internal virtual TextBlock LabText
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyTextBox TextArea
	{
		[CompilerGenerated]
		get
		{
			return _TextArea;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyTextBox.ValidateChangedEventHandler obj = TextCaption_ValidateChanged;
			if (_TextArea != null)
			{
				MyTextBox.ValidateChanged -= obj;
			}
			_TextArea = value;
			if (_TextArea != null)
			{
				MyTextBox.ValidateChanged += obj;
			}
		}
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

	public virtual MyButton Btn2
	{
		[CompilerGenerated]
		get
		{
			return _Btn2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Btn2_Click();
			};
			MyButton btn = _Btn2;
			if (btn != null)
			{
				btn.Click -= obj;
			}
			_Btn2 = value;
			btn = _Btn2;
			if (btn != null)
			{
				btn.Click += obj;
			}
		}
	}

	public MyMsgInput(ModMain.MyMsgBoxConverter Converter)
	{
		base.Loaded += Load;
		Uuid = ModBase.GetUuid();
		try
		{
			InitializeComponent();
			Btn1.Name += Conversions.ToString(ModBase.GetUuid());
			Btn2.Name += Conversions.ToString(ModBase.GetUuid());
			MyConverter = Converter;
			LabTitle.Text = Converter.Title;
			LabText.Text = Converter.Text;
			PanText.Visibility = ((Operators.CompareString(Converter.Text, "", TextCompare: false) == 0) ? Visibility.Collapsed : Visibility.Visible);
			TextArea.Text = Conversions.ToString(Converter.Content);
			TextArea.HintText = Converter.HintText;
			TextArea.ValidateRules = Converter.ValidateRules;
			Btn1.Text = Converter.Button1;
			if (Converter.IsWarn)
			{
				Btn1.ColorType = MyButton.ColorState.Red;
				LabTitle.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrushRedLight");
			}
			Btn2.Text = Converter.Button2;
			Btn2.Visibility = ((Operators.CompareString(Converter.Button2, "", TextCompare: false) == 0) ? Visibility.Collapsed : Visibility.Visible);
			ShapeLine.StrokeThickness = ModBase.GetWPFSize(1.0);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "输入弹窗初始化失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyMsg\\MyMsgInput.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void Load(object sender, EventArgs e)
	{
		try
		{
			if (Btn2.IsVisible && Btn1.ColorType != MyButton.ColorState.Red)
			{
				Btn1.ColorType = MyButton.ColorState.Highlight;
			}
			TextArea.Focus();
			TextArea.SelectionStart = TextArea.Text.Length;
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
			Logger.Info($"输入弹窗：{LabTitle.Text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyMsg\\MyMsgInput.xaml.vb");
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "输入弹窗加载失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyMsg\\MyMsgInput.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void Close()
	{
		MyConverter.WaitFrame.Continue = false;
		ComponentDispatcher.PopModal();
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
		TextArea.Validate();
		if (!MyConverter.IsExited && TextArea.IsValidated)
		{
			MyConverter.IsExited = true;
			MyConverter.Result = TextArea.Text;
			Close();
		}
	}

	public void Btn2_Click()
	{
		if (!MyConverter.IsExited)
		{
			MyConverter.IsExited = true;
			MyConverter.Result = null;
			Close();
		}
	}

	private void TextCaption_ValidateChanged(object sender, EventArgs e)
	{
		Btn1.IsEnabled = TextArea.IsValidated;
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

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mymsg/mymsginput.xaml", UriKind.Relative);
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
			PanText = (MyScrollViewer)target;
			break;
		case 9:
			LabText = (TextBlock)target;
			break;
		case 10:
			TextArea = (MyTextBox)target;
			break;
		case 11:
			PanBtn = (StackPanel)target;
			break;
		case 12:
			Btn1 = (MyButton)target;
			break;
		case 13:
			Btn2 = (MyButton)target;
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
