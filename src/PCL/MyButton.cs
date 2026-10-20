using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[ContentProperty("Inlines")]
[DesignerGenerated]
public class MyButton : Border, IComponentConnector
{
	public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

	public enum ColorState
	{
		Normal,
		Highlight,
		Red
	}

	public int Uuid;

	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyButton), new PropertyMetadata([SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
	{
		if (sender != null)
		{
			((MyButton)sender).LabText.Text = Conversions.ToString(e.NewValue);
		}
	}));

	private ColorState _ColorType;

	public new static readonly DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(MyButton), new PropertyMetadata([SpecialName] (DependencyObject a0, DependencyPropertyChangedEventArgs a1) =>
	{
		((VB_0024AnonymousDelegate_8<MyButton, DependencyPropertyChangedEventArgs>)([SpecialName] (MyButton sender, DependencyPropertyChangedEventArgs e) =>
		{
			if (sender != null)
			{
				Border panFore = sender.PanFore;
				object newValue = e.NewValue;
				panFore.Padding = ((newValue != null) ? ((Thickness)newValue) : default(Thickness));
			}
		}))((MyButton)a0, a1);
	}));

	private const int AnimationColorIn = 100;

	private const int AnimationColorOut = 200;

	private bool IsMouseDown;

	private bool _contentLoaded;

	public InlineCollection Inlines => LabText.Inlines;

	public string Text
	{
		get
		{
			return Conversions.ToString(GetValue(TextProperty));
		}
		set
		{
			SetValue(TextProperty, value);
		}
	}

	public Thickness TextPadding
	{
		get
		{
			return LabText.Padding;
		}
		set
		{
			LabText.Padding = value;
		}
	}

	public ColorState ColorType
	{
		get
		{
			return _ColorType;
		}
		set
		{
			_ColorType = value;
			RefreshColor();
		}
	}

	public new Thickness Padding
	{
		get
		{
			return PanFore.Padding;
		}
		set
		{
			PanFore.Padding = value;
		}
	}

	public Transform RealRenderTransform
	{
		get
		{
			return PanFore.RenderTransform;
		}
		set
		{
			PanFore.RenderTransform = value;
		}
	}

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyButton PanBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanFore")]
	internal virtual Border PanFore
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

	public event ClickEventHandler Click;

	public MyButton()
	{
		base.MouseEnter += RefreshColor;
		base.MouseLeave += RefreshColor;
		base.Loaded += RefreshColor;
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			RefreshColor(RuntimeHelpers.GetObjectValue(a0), a1);
		};
		base.MouseLeftButtonUp += Button_MouseUp;
		base.MouseLeftButtonDown += Button_MouseDown;
		base.MouseEnter += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			Button_MouseEnter();
		};
		base.MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
		{
			Button_MouseUp();
		};
		base.MouseLeave += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			Button_MouseLeave();
		};
		Uuid = ModBase.GetUuid();
		_ColorType = ColorState.Normal;
		IsMouseDown = false;
		InitializeComponent();
	}

	private void RefreshColor(object obj = null, object e = null)
	{
		try
		{
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				if (base.IsEnabled)
				{
					switch (ColorType)
					{
					case ColorState.Normal:
						if (base.IsMouseOver)
						{
							ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(PanFore, Border.BorderBrushProperty, "ColorBrush3", 100) }, "MyButton Color " + Conversions.ToString(Uuid));
						}
						else
						{
							ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(PanFore, Border.BorderBrushProperty, "ColorBrush1", 200) }, "MyButton Color " + Conversions.ToString(Uuid));
						}
						break;
					case ColorState.Highlight:
						if (base.IsMouseOver)
						{
							ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(PanFore, Border.BorderBrushProperty, "ColorBrush3", 100) }, "MyButton Color " + Conversions.ToString(Uuid));
						}
						else
						{
							ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(PanFore, Border.BorderBrushProperty, "ColorBrush2", 200) }, "MyButton Color " + Conversions.ToString(Uuid));
						}
						break;
					case ColorState.Red:
						if (base.IsMouseOver)
						{
							ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(PanFore, Border.BorderBrushProperty, "ColorBrushRedLight", 100) }, "MyButton Color " + Conversions.ToString(Uuid));
						}
						else
						{
							ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(PanFore, Border.BorderBrushProperty, "ColorBrushRedDark", 200) }, "MyButton Color " + Conversions.ToString(Uuid));
						}
						break;
					}
				}
				else
				{
					ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(PanFore, Border.BorderBrushProperty, ModSecret.ColorGray4 - PanFore.BorderBrush, 200) }, "MyButton Color " + Conversions.ToString(Uuid));
				}
				return;
			}
			ModAnimation.AniStop("MyButton Color " + Conversions.ToString(Uuid));
			if (base.IsEnabled)
			{
				switch (ColorType)
				{
				case ColorState.Normal:
					if (base.IsMouseOver)
					{
						PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrush3");
					}
					else
					{
						PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrush1");
					}
					break;
				case ColorState.Highlight:
					if (base.IsMouseOver)
					{
						PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrush3");
					}
					else
					{
						PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrush2");
					}
					break;
				case ColorState.Red:
					if (base.IsMouseOver)
					{
						PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrushRedLight");
					}
					else
					{
						PanFore.SetResourceReference(Border.BorderBrushProperty, "ColorBrushRedDark");
					}
					break;
				}
			}
			else
			{
				PanFore.BorderBrush = ModSecret.ColorGray4;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "刷新按钮颜色出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyButton.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void Button_MouseUp(object sender, MouseButtonEventArgs e)
	{
		if (IsMouseDown)
		{
			Logger.Info($"按下按钮：{Text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyButton.xaml.vb");
			Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
			ModMain.RaiseCustomEvent(this);
		}
	}

	private void Button_MouseDown(object sender, MouseButtonEventArgs e)
	{
		IsMouseDown = true;
		Focus();
		ModAnimation.AniStart(new ModAnimation.AniData[2]
		{
			ModAnimation.AaScaleTransform(PanFore, 0.955 - ((ScaleTransform)PanFore.RenderTransform).ScaleX, 80, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong)),
			ModAnimation.AaScaleTransform(PanFore, -0.01, 700, 0, new ModAnimation.AniEaseOutFluent())
		}, "MyButton Scale " + Conversions.ToString(Uuid));
	}

	private void Button_MouseEnter()
	{
		ModAnimation.AniStart(ModAnimation.AaColor(PanFore, Border.BackgroundProperty, (_ColorType == ColorState.Red) ? "ColorBrushRedBack" : "ColorBrush7", 100), "MyButton Background " + Conversions.ToString(Uuid));
	}

	private void Button_MouseUp()
	{
		if (IsMouseDown)
		{
			IsMouseDown = false;
			ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanFore, 1.0 - ((ScaleTransform)PanFore.RenderTransform).ScaleX, 300, 10, new ModAnimation.AniEaseOutFluent()) }, "MyButton Scale " + Conversions.ToString(Uuid));
		}
	}

	private void Button_MouseLeave()
	{
		ModAnimation.AniStart(ModAnimation.AaColor(PanFore, Border.BackgroundProperty, "ColorBrushHalfWhite", 200), "MyButton Background " + Conversions.ToString(Uuid));
		if (IsMouseDown)
		{
			IsMouseDown = false;
			ModAnimation.AniStart(ModAnimation.AaScaleTransform(PanFore, 1.0 - ((ScaleTransform)PanFore.RenderTransform).ScaleX, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)), "MyButton Scale " + Conversions.ToString(Uuid));
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mybutton.xaml", UriKind.Relative);
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
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			PanBack = (MyButton)target;
			break;
		case 2:
			PanFore = (Border)target;
			break;
		case 3:
			LabText = (TextBlock)target;
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
