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
using System.Windows.Shapes;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[ContentProperty("Inlines")]
[DesignerGenerated]
public class MyExtraTextButton : Grid, IComponentConnector
{
	public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

	public int Uuid;

	private string _Logo;

	private double _LogoScale;

	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyExtraTextButton), new PropertyMetadata([SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
	{
		if (sender != null)
		{
			((MyExtraTextButton)sender).LabText.Text = Conversions.ToString(e.NewValue);
		}
	}));

	private bool _Show;

	private bool IsLeftMouseHeld;

	private const int AnimationColorIn = 120;

	private const int AnimationColorOut = 150;

	[CompilerGenerated]
	[AccessedThroughProperty("PanClick")]
	private Border _PanClick;

	private bool _contentLoaded;

	public string Logo
	{
		get
		{
			return _Logo;
		}
		set
		{
			if (Operators.CompareString(value, _Logo, TextCompare: false) != 0)
			{
				_Logo = value;
				Path.Data = (Geometry)new GeometryConverter().ConvertFromString(value);
			}
		}
	}

	public double LogoScale
	{
		get
		{
			return _LogoScale;
		}
		set
		{
			_LogoScale = value;
			if (Path != null)
			{
				Path.RenderTransform = new ScaleTransform
				{
					ScaleX = LogoScale,
					ScaleY = LogoScale
				};
			}
		}
	}

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

	public bool Show
	{
		get
		{
			return _Show;
		}
		set
		{
			if (_Show == value)
			{
				return;
			}
			_Show = value;
			ModBase.RunInUi([SpecialName] () =>
			{
				if (value)
				{
					base.Opacity = 0.0;
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaOpacity(this, 1.0 - base.Opacity, 80, 50),
						ModAnimation.AaScaleTransform(this, 0.15 - ((ScaleTransform)base.RenderTransform).ScaleX, 400, 50, new ModAnimation.AniEaseOutBack()),
						ModAnimation.AaScaleTransform(this, 0.85, 160, 50, new ModAnimation.AniEaseOutFluent())
					}, "MyExtraTextButton MainScale " + Conversions.ToString(Uuid));
				}
				else
				{
					ModAnimation.AniStart(new ModAnimation.AniData[2]
					{
						ModAnimation.AaOpacity(this, 0.0 - base.Opacity, 50, 50),
						ModAnimation.AaScaleTransform(this, 0.0 - ((ScaleTransform)base.RenderTransform).ScaleX, 100, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak))
					}, "MyExtraTextButton MainScale " + Conversions.ToString(Uuid));
				}
				base.IsHitTestVisible = value;
			});
		}
	}

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyExtraTextButton PanBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Border PanClick
	{
		[CompilerGenerated]
		get
		{
			return _PanClick;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = Button_LeftMouseUp;
			MouseButtonEventHandler value3 = Button_LeftMouseDown;
			MouseButtonEventHandler value4 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Button_LeftMouseUp();
			};
			MouseButtonEventHandler value5 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Button_RightMouseUp();
			};
			MouseEventHandler value6 = [SpecialName] (object a0, MouseEventArgs a1) =>
			{
				Button_MouseLeave();
			};
			MouseEventHandler value7 = [SpecialName] (object a0, MouseEventArgs a1) =>
			{
				RefreshColor();
			};
			MouseEventHandler value8 = [SpecialName] (object a0, MouseEventArgs a1) =>
			{
				RefreshColor();
			};
			Border panClick = _PanClick;
			if (panClick != null)
			{
				panClick.MouseLeftButtonUp -= value2;
				panClick.MouseLeftButtonDown -= value3;
				panClick.MouseLeftButtonUp -= value4;
				panClick.MouseRightButtonUp -= value5;
				panClick.MouseLeave -= value6;
				panClick.MouseEnter -= value7;
				panClick.MouseLeave -= value8;
			}
			_PanClick = value;
			panClick = _PanClick;
			if (panClick != null)
			{
				panClick.MouseLeftButtonUp += value2;
				panClick.MouseLeftButtonDown += value3;
				panClick.MouseLeftButtonUp += value4;
				panClick.MouseRightButtonUp += value5;
				panClick.MouseLeave += value6;
				panClick.MouseEnter += value7;
				panClick.MouseLeave += value8;
			}
		}
	}

	[field: AccessedThroughProperty("PanScale")]
	internal virtual Grid PanScale
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanColor")]
	internal virtual Border PanColor
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Path")]
	internal virtual Path Path
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

	public MyExtraTextButton()
	{
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			RefreshColor();
		};
		Uuid = ModBase.GetUuid();
		_Logo = "";
		_LogoScale = 1.0;
		_Show = false;
		IsLeftMouseHeld = false;
		InitializeComponent();
	}

	private void Button_LeftMouseUp(object sender, MouseButtonEventArgs e)
	{
		if (IsLeftMouseHeld)
		{
			Logger.Info($"按下附加图标按钮：{Text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyExtraTextButton.xaml.vb");
			Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
			e.Handled = true;
			ModMain.RaiseCustomEvent(this);
			Button_LeftMouseUp();
		}
	}

	private void Button_LeftMouseDown(object sender, MouseButtonEventArgs e)
	{
		if (!IsLeftMouseHeld)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[2]
			{
				ModAnimation.AaScaleTransform(PanScale, 0.85 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)),
				ModAnimation.AaScaleTransform(PanScale, -0.05, 60, 0, new ModAnimation.AniEaseOutFluent())
			}, "MyExtraTextButton Scale " + Conversions.ToString(Uuid));
		}
		IsLeftMouseHeld = true;
		Focus();
	}

	private void Button_LeftMouseUp()
	{
		ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 300, 0, new ModAnimation.AniEaseOutBack()) }, "MyExtraTextButton Scale " + Conversions.ToString(Uuid));
		IsLeftMouseHeld = false;
		RefreshColor();
	}

	private void Button_RightMouseUp()
	{
		if (!IsLeftMouseHeld)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 300, 0, new ModAnimation.AniEaseOutBack()) }, "MyExtraTextButton Scale " + Conversions.ToString(Uuid));
		}
		RefreshColor();
	}

	private void Button_MouseLeave()
	{
		IsLeftMouseHeld = false;
		ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 500, 0, new ModAnimation.AniEaseOutFluent()) }, "MyExtraTextButton Scale " + Conversions.ToString(Uuid));
		RefreshColor();
	}

	public void RefreshColor()
	{
		try
		{
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				if (!base.IsEnabled)
				{
					ModAnimation.AniStart(ModAnimation.AaColor(PanColor, Panel.BackgroundProperty, "ColorBrushGray4", 120), "MyExtraTextButton Color " + Conversions.ToString(Uuid));
				}
				else if (base.IsMouseOver)
				{
					ModAnimation.AniStart(ModAnimation.AaColor(PanColor, Panel.BackgroundProperty, "ColorBrush4", 120), "MyExtraTextButton Color " + Conversions.ToString(Uuid));
				}
				else
				{
					ModAnimation.AniStart(ModAnimation.AaColor(PanColor, Panel.BackgroundProperty, "ColorBrush3", 150), "MyExtraTextButton Color " + Conversions.ToString(Uuid));
				}
				return;
			}
			ModAnimation.AniStop("MyExtraTextButton Color " + Conversions.ToString(Uuid));
			if (!base.IsEnabled)
			{
				PanColor.SetResourceReference(Panel.BackgroundProperty, "ColorBrushGray4");
			}
			else if (base.IsMouseOver)
			{
				PanColor.SetResourceReference(Panel.BackgroundProperty, "ColorBrush4");
			}
			else
			{
				PanColor.SetResourceReference(Panel.BackgroundProperty, "ColorBrush3");
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "刷新附加图标按钮颜色出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyExtraTextButton.xaml.vb");
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
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myextratextbutton.xaml", UriKind.Relative);
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
			PanBack = (MyExtraTextButton)target;
			break;
		case 2:
			PanClick = (Border)target;
			break;
		case 3:
			PanScale = (Grid)target;
			break;
		case 4:
			PanColor = (Border)target;
			break;
		case 5:
			Path = (Path)target;
			break;
		case 6:
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
