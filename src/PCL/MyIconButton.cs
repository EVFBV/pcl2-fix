using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
public class MyIconButton : Border, IComponentConnector
{
	public delegate void ClickEventHandler(object sender, EventArgs e);

	public enum Themes
	{
		Color,
		White,
		Black,
		Red,
		Custom
	}

	public int Uuid;

	private double _LogoScale;

	private SolidColorBrush _Foreground;

	private bool IsMouseDown;

	private const int AnimationColorIn = 120;

	private const int AnimationColorOut = 150;

	private bool _contentLoaded;

	public string Logo
	{
		get
		{
			return Path.Data.ToString();
		}
		set
		{
			Path.Data = (Geometry)new GeometryConverter().ConvertFromString(value);
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

	public Themes Theme { get; set; }

	public SolidColorBrush Foreground
	{
		get
		{
			return _Foreground;
		}
		set
		{
			_Foreground = value;
			checked
			{
				ModAnimation.AniControlEnabled++;
				RefreshAnim();
				ModAnimation.AniControlEnabled--;
			}
		}
	}

	public bool CanClick { get; set; }

	[field: AccessedThroughProperty("PanBack")]
	internal virtual Border PanBack
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

	public MyIconButton()
	{
		base.MouseLeftButtonUp += Button_MouseUp;
		base.MouseLeftButtonDown += Button_MouseDown;
		base.MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
		{
			Button_MouseUp();
		};
		base.MouseLeave += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			Button_MouseLeave();
		};
		base.MouseEnter += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			RefreshAnim();
		};
		base.MouseLeave += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			RefreshAnim();
		};
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshAnim();
		};
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			RefreshAnim();
		};
		Uuid = ModBase.GetUuid();
		_LogoScale = 1.0;
		Theme = Themes.Color;
		_Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
		IsMouseDown = false;
		CanClick = true;
		InitializeComponent();
	}

	private void Button_MouseUp(object sender, MouseButtonEventArgs e)
	{
		if (CanClick && IsMouseDown)
		{
			Logger.Info(string.Format("按下图标按钮{0}", string.IsNullOrEmpty(base.Name) ? "" : ("：" + base.Name)), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyIconButton.xaml.vb");
			Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
			e.Handled = true;
			Button_MouseUp();
			ModMain.RaiseCustomEvent(this);
		}
	}

	private void Button_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (CanClick)
		{
			IsMouseDown = true;
			Focus();
			ModAnimation.AniStart(ModAnimation.AaScaleTransform(PanBack, 0.8 - ((ScaleTransform)PanBack.RenderTransform).ScaleX, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong)), "MyIconButton Scale " + Conversions.ToString(Uuid));
		}
	}

	private void Button_MouseUp()
	{
		if (CanClick)
		{
			if (IsMouseDown)
			{
				IsMouseDown = false;
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaScaleTransform(PanBack, 1.05 - ((ScaleTransform)PanBack.RenderTransform).ScaleX, 250, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaScaleTransform(PanBack, -0.05, 250, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong))
				}, "MyIconButton Scale " + Conversions.ToString(Uuid));
			}
			RefreshAnim();
		}
	}

	private void Button_MouseLeave()
	{
		if (CanClick)
		{
			IsMouseDown = false;
			ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(PanBack, 1.0 - ((ScaleTransform)PanBack.RenderTransform).ScaleX, 250, 0, new ModAnimation.AniEaseOutFluent()) }, "MyIconButton Scale " + Conversions.ToString(Uuid));
			RefreshAnim();
		}
	}

	public void RefreshAnim()
	{
		try
		{
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				if (PanBack.Background == null)
				{
					PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
				}
				if (Path.Fill == null)
				{
					switch (Theme)
					{
					case Themes.Red:
						Path.Fill = new ModBase.MyColor(160.0, 255.0, 76.0, 76.0);
						break;
					case Themes.Black:
						Path.Fill = new ModBase.MyColor(160.0, 0.0, 0.0, 0.0);
						break;
					case Themes.Custom:
						Path.Fill = new ModBase.MyColor(160.0, Foreground);
						break;
					}
				}
				List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
				if (!base.IsEnabled)
				{
					list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, "ColorBrushGray5", 120));
				}
				else if (base.IsMouseOver)
				{
					switch (Theme)
					{
					case Themes.Color:
						list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, "ColorBrush2", 120));
						break;
					case Themes.White:
						list.Add(ModAnimation.AaColor(PanBack, Border.BackgroundProperty, new ModBase.MyColor(50.0, 255.0, 255.0, 255.0) - PanBack.Background, 120));
						break;
					case Themes.Red:
						list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(255.0, 76.0, 76.0) - Path.Fill, 120));
						break;
					case Themes.Black:
						list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(230.0, 0.0, 0.0, 0.0) - Path.Fill, 120));
						break;
					case Themes.Custom:
						list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(255.0, Foreground) - Path.Fill, 120));
						break;
					}
				}
				else
				{
					switch (Theme)
					{
					case Themes.Color:
						list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, "ColorBrush4", 150));
						PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
						break;
					case Themes.White:
						list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, "ColorBrush8", 150));
						list.Add(ModAnimation.AaColor(PanBack, Border.BackgroundProperty, new ModBase.MyColor(0.0, 255.0, 255.0, 255.0) - PanBack.Background, 150));
						break;
					case Themes.Red:
						list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(160.0, 255.0, 76.0, 76.0) - Path.Fill, 150));
						PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
						break;
					case Themes.Black:
						list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(160.0, 0.0, 0.0, 0.0) - Path.Fill, 150));
						PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
						break;
					case Themes.Custom:
						list.Add(ModAnimation.AaColor(Path, Shape.FillProperty, new ModBase.MyColor(160.0, Foreground) - Path.Fill, 150));
						PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
						break;
					}
				}
				ModAnimation.AniStart(list, "MyIconButton Color " + Conversions.ToString(Uuid));
				return;
			}
			ModAnimation.AniStop("MyIconButton Color " + Conversions.ToString(Uuid));
			if (base.IsEnabled)
			{
				switch (Theme)
				{
				case Themes.Color:
					Path.SetResourceReference(Shape.FillProperty, "ColorBrush5");
					break;
				case Themes.White:
					Path.SetResourceReference(Shape.FillProperty, "ColorBrush8");
					break;
				case Themes.Red:
					Path.Fill = new ModBase.MyColor(160.0, 255.0, 76.0, 76.0);
					break;
				case Themes.Black:
					Path.Fill = new ModBase.MyColor(160.0, 0.0, 0.0, 0.0);
					break;
				case Themes.Custom:
					Path.Fill = new ModBase.MyColor(160.0, Foreground);
					break;
				}
			}
			else
			{
				Path.SetResourceReference(Shape.FillProperty, "ColorBrushGray5");
			}
			PanBack.Background = new ModBase.MyColor(0.0, 255.0, 255.0, 255.0);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "刷新图标按钮动画状态出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyIconButton.xaml.vb");
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
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myiconbutton.xaml", UriKind.Relative);
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
			PanBack = (Border)target;
			break;
		case 2:
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
