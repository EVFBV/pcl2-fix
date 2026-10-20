using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using MeloongCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class MyExtraButton : Grid, IComponentConnector
{
	public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

	public delegate void RightClickEventHandler(object sender, MouseButtonEventArgs e);

	public delegate bool ShowCheckDelegate();

	private double _Progress;

	public int Uuid;

	private string _Logo;

	private double _LogoScale;

	private bool _Show;

	public ShowCheckDelegate ShowCheck;

	private bool _CanRightClick;

	private bool IsLeftMouseHeld;

	private bool IsRightMouseHeld;

	private const int AnimationColorIn = 120;

	private const int AnimationColorOut = 150;

	[CompilerGenerated]
	[AccessedThroughProperty("PanClick")]
	private Border _PanClick;

	private bool _contentLoaded;

	public double Progress
	{
		get
		{
			return _Progress;
		}
		set
		{
			if (_Progress != value)
			{
				_Progress = value;
				if (value < 0.0001)
				{
					PanProgress.Visibility = Visibility.Collapsed;
					return;
				}
				PanProgress.Visibility = Visibility.Visible;
				RectProgress.Rect = new Rect(0.0, 40.0 * (1.0 - value), 40.0, 40.0 * value);
			}
		}
	}

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
			if (!Information.IsNothing(Path))
			{
				Path.RenderTransform = new ScaleTransform
				{
					ScaleX = LogoScale,
					ScaleY = LogoScale
				};
			}
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
					base.Visibility = Visibility.Visible;
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaScaleTransform(this, 0.3 - ((ScaleTransform)base.RenderTransform).ScaleX, 500, 60, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaScaleTransform(this, 0.7, 500, 60, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaHeight(this, 50.0 - base.Height, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak))
					}, "MyExtraButton MainScale " + Conversions.ToString(Uuid));
				}
				else
				{
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaScaleTransform(this, 0.0 - ((ScaleTransform)base.RenderTransform).ScaleX, 100, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaHeight(this, 0.0 - base.Height, 400, 100, new ModAnimation.AniEaseOutFluent()),
						ModAnimation.AaCode([SpecialName] () =>
						{
							base.Visibility = Visibility.Collapsed;
						}, 0, After: true)
					}, "MyExtraButton MainScale " + Conversions.ToString(Uuid));
				}
				base.IsHitTestVisible = value;
			});
		}
	}

	public bool CanRightClick
	{
		get
		{
			return _CanRightClick;
		}
		set
		{
			_CanRightClick = value;
		}
	}

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyExtraButton PanBack
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
			MouseButtonEventHandler value3 = Button_RightMouseUp;
			MouseButtonEventHandler value4 = Button_LeftMouseDown;
			MouseButtonEventHandler value5 = Button_RightMouseDown;
			MouseButtonEventHandler value6 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Button_LeftMouseUp();
			};
			MouseButtonEventHandler value7 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Button_RightMouseUp();
			};
			MouseEventHandler value8 = [SpecialName] (object a0, MouseEventArgs a1) =>
			{
				Button_MouseLeave();
			};
			MouseEventHandler value9 = [SpecialName] (object a0, MouseEventArgs a1) =>
			{
				RefreshColor();
			};
			MouseEventHandler value10 = [SpecialName] (object a0, MouseEventArgs a1) =>
			{
				RefreshColor();
			};
			Border panClick = _PanClick;
			if (panClick != null)
			{
				panClick.MouseLeftButtonUp -= value2;
				panClick.MouseRightButtonUp -= value3;
				panClick.MouseLeftButtonDown -= value4;
				panClick.MouseRightButtonDown -= value5;
				panClick.MouseLeftButtonUp -= value6;
				panClick.MouseRightButtonUp -= value7;
				panClick.MouseLeave -= value8;
				panClick.MouseEnter -= value9;
				panClick.MouseLeave -= value10;
			}
			_PanClick = value;
			panClick = _PanClick;
			if (panClick != null)
			{
				panClick.MouseLeftButtonUp += value2;
				panClick.MouseRightButtonUp += value3;
				panClick.MouseLeftButtonDown += value4;
				panClick.MouseRightButtonDown += value5;
				panClick.MouseLeftButtonUp += value6;
				panClick.MouseRightButtonUp += value7;
				panClick.MouseLeave += value8;
				panClick.MouseEnter += value9;
				panClick.MouseLeave += value10;
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

	[field: AccessedThroughProperty("PanProgress")]
	internal virtual Border PanProgress
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RectProgress")]
	internal virtual RectangleGeometry RectProgress
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

	public event ClickEventHandler Click;

	public event RightClickEventHandler RightClick;

	public MyExtraButton()
	{
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			RefreshColor();
		};
		_Progress = 0.0;
		Uuid = ModBase.GetUuid();
		_Logo = "";
		_LogoScale = 1.0;
		_Show = false;
		ShowCheck = null;
		_CanRightClick = false;
		IsLeftMouseHeld = false;
		IsRightMouseHeld = false;
		InitializeComponent();
	}

	public void ShowRefresh()
	{
		if (ShowCheck != null)
		{
			Show = ShowCheck();
		}
	}

	private void Button_LeftMouseUp(object sender, MouseButtonEventArgs e)
	{
		if (IsLeftMouseHeld)
		{
			Logger.Info(string.Format("按下附加按钮{0}", RuntimeHelpers.GetObjectValue(Operators.ConditionalCompareObjectEqual(base.ToolTip, "", TextCompare: false) ? "" : Operators.ConcatenateObject("：", base.ToolTip))), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyExtraButton.xaml.vb");
			Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
			e.Handled = true;
			Button_LeftMouseUp();
		}
	}

	private void Button_RightMouseUp(object sender, MouseButtonEventArgs e)
	{
		if (IsRightMouseHeld)
		{
			Logger.Info(string.Format("右键按下附加按钮{0}", RuntimeHelpers.GetObjectValue(Operators.ConditionalCompareObjectEqual(base.ToolTip, "", TextCompare: false) ? "" : Operators.ConcatenateObject("：", base.ToolTip))), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyExtraButton.xaml.vb");
			RightClick?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
			e.Handled = true;
			Button_RightMouseUp();
		}
	}

	private void Button_LeftMouseDown(object sender, MouseButtonEventArgs e)
	{
		if (!IsLeftMouseHeld && !IsRightMouseHeld)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[2]
			{
				ModAnimation.AaScaleTransform(PanScale, 0.85 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)),
				ModAnimation.AaScaleTransform(PanScale, -0.05, 60, 0, new ModAnimation.AniEaseOutFluent())
			}, "MyExtraButton Scale " + Conversions.ToString(Uuid));
		}
		IsLeftMouseHeld = true;
		Focus();
	}

	private void Button_RightMouseDown(object sender, MouseButtonEventArgs e)
	{
		if (CanRightClick)
		{
			if (!IsLeftMouseHeld && !IsRightMouseHeld)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaScaleTransform(PanScale, 0.85 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 800, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)),
					ModAnimation.AaScaleTransform(PanScale, -0.05, 60, 0, new ModAnimation.AniEaseOutFluent())
				}, "MyExtraButton Scale " + Conversions.ToString(Uuid));
			}
			IsRightMouseHeld = true;
			Focus();
		}
	}

	private void Button_LeftMouseUp()
	{
		if (!IsRightMouseHeld)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 300, 0, new ModAnimation.AniEaseOutBack()) }, "MyExtraButton Scale " + Conversions.ToString(Uuid));
		}
		if (IsLeftMouseHeld)
		{
			ModMain.RaiseCustomEvent(this);
		}
		IsLeftMouseHeld = false;
		RefreshColor();
	}

	private void Button_RightMouseUp()
	{
		if (CanRightClick)
		{
			if (!IsLeftMouseHeld)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 300, 0, new ModAnimation.AniEaseOutBack()) }, "MyExtraButton Scale " + Conversions.ToString(Uuid));
			}
			IsRightMouseHeld = false;
			RefreshColor();
		}
	}

	private void Button_MouseLeave()
	{
		IsLeftMouseHeld = false;
		IsRightMouseHeld = false;
		ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanScale, 1.0 - ((ScaleTransform)PanScale.RenderTransform).ScaleX, 500, 0, new ModAnimation.AniEaseOutFluent()) }, "MyExtraButton Scale " + Conversions.ToString(Uuid));
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
					ModAnimation.AniStart(ModAnimation.AaColor(PanColor, Panel.BackgroundProperty, "ColorBrushGray4", 120), "MyExtraButton Color " + Conversions.ToString(Uuid));
				}
				else if (base.IsMouseOver)
				{
					ModAnimation.AniStart(ModAnimation.AaColor(PanColor, Panel.BackgroundProperty, "ColorBrush4", 120), "MyExtraButton Color " + Conversions.ToString(Uuid));
				}
				else
				{
					ModAnimation.AniStart(ModAnimation.AaColor(PanColor, Panel.BackgroundProperty, "ColorBrush3", 150), "MyExtraButton Color " + Conversions.ToString(Uuid));
				}
				return;
			}
			ModAnimation.AniStop("MyExtraButton Color " + Conversions.ToString(Uuid));
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
			Logger.Warn(ex2, "刷新图标按钮颜色出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyExtraButton.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	public void Ribble()
	{
		ModBase.RunInUi([SpecialName] () =>
		{
			Border border = new Border
			{
				CornerRadius = new CornerRadius(1000.0),
				BorderThickness = new Thickness(0.001),
				Opacity = 0.5,
				RenderTransformOrigin = new Point(0.5, 0.5),
				RenderTransform = new ScaleTransform()
			};
			border.SetResourceReference(Border.BackgroundProperty, "ColorBrush5");
			PanScale.Children.Insert(0, border);
			ModAnimation.AniStart(new ModAnimation.AniData[3]
			{
				ModAnimation.AaScaleTransform(border, 13.0, 1000, 0, new ModAnimation.AniEaseInoutFluent(ModAnimation.AniEasePower.Strong, 0.3)),
				ModAnimation.AaOpacity(border, 0.0 - border.Opacity, 1000),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PanScale.Children.Remove(border);
				}, 0, After: true)
			}, "ExtraButton Ribble " + Conversions.ToString(ModBase.GetUuid()));
		});
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myextrabutton.xaml", UriKind.Relative);
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
			PanBack = (MyExtraButton)target;
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
			PanProgress = (Border)target;
			break;
		case 6:
			RectProgress = (RectangleGeometry)target;
			break;
		case 7:
			Path = (Path)target;
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
