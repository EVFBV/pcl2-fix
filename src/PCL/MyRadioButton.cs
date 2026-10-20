using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
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
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[ContentProperty("Inlines")]
[DesignerGenerated]
public class MyRadioButton : Border, IComponentConnector
{
	public delegate void CheckEventHandler(object sender, bool raiseByMouse);

	public enum ColorState
	{
		White,
		Highlight
	}

	public delegate void PreviewClickEventHandler(object sender, ModBase.RouteEventArgs e);

	public int Uuid;

	private double _LogoScale;

	private bool _Checked;

	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyRadioButton), new PropertyMetadata([SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
	{
		if (sender != null)
		{
			((MyRadioButton)sender).LabText.Text = Conversions.ToString(e.NewValue);
		}
	}));

	private ColorState _ColorType;

	private bool IsMouseDown;

	private const int AnimationTimeOfMouseIn = 90;

	private const int AnimationTimeOfMouseOut = 150;

	private const int AnimationTimeOfCheck = 120;

	private bool _contentLoaded;

	public string Logo
	{
		get
		{
			return ShapeLogo.Data.ToString();
		}
		set
		{
			ShapeLogo.Data = (Geometry)new GeometryConverter().ConvertFromString(value);
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
			if (!Information.IsNothing(ShapeLogo))
			{
				ShapeLogo.RenderTransform = new ScaleTransform
				{
					ScaleX = LogoScale,
					ScaleY = LogoScale
				};
			}
		}
	}

	public bool Checked
	{
		get
		{
			return _Checked;
		}
		set
		{
			SetChecked(value, raiseByMouse: false, anime: true);
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

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyRadioButton PanBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ShapeLogo")]
	internal virtual Path ShapeLogo
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

	public event CheckEventHandler Check;

	public event PreviewClickEventHandler PreviewClick;

	public MyRadioButton()
	{
		base.MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
		{
			Radiobox_MouseUp();
		};
		base.MouseLeftButtonDown += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
		{
			Radiobox_MouseDown();
		};
		base.MouseLeave += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			Radiobox_MouseLeave();
		};
		base.MouseEnter += RefreshColor;
		base.MouseLeave += RefreshColor;
		base.Loaded += RefreshColor;
		Uuid = ModBase.GetUuid();
		_LogoScale = 1.0;
		_Checked = false;
		_ColorType = ColorState.White;
		IsMouseDown = false;
		InitializeComponent();
	}

	public void SetChecked(bool value, bool raiseByMouse, bool anime)
	{
		try
		{
			bool flag = false;
			if (_Checked != value)
			{
				_Checked = value;
				flag = true;
			}
			if (base.Parent == null)
			{
				return;
			}
			List<MyRadioButton> list = new List<MyRadioButton>();
			int num = 0;
			foreach (object item in (IEnumerable)NewLateBinding.LateGet(base.Parent, null, "Children", new object[0], null, null, null))
			{
				object objectValue = RuntimeHelpers.GetObjectValue(item);
				if (objectValue is MyRadioButton)
				{
					list.Add((MyRadioButton)objectValue);
					if (Conversions.ToBoolean(NewLateBinding.LateGet(objectValue, null, "Checked", new object[0], null, null, null)))
					{
						num = checked(num + 1);
					}
				}
			}
			int num2 = num;
			if (num2 == 0)
			{
				list[0].Checked = true;
			}
			else if (num2 > 1)
			{
				if (Checked)
				{
					foreach (MyRadioButton item2 in list)
					{
						if (item2.Checked && !item2.Equals(this))
						{
							item2.Checked = false;
						}
					}
				}
				else
				{
					bool flag2 = false;
					foreach (MyRadioButton item3 in list)
					{
						if (item3.Checked)
						{
							if (flag2)
							{
								item3.Checked = false;
							}
							else
							{
								flag2 = true;
							}
						}
					}
				}
			}
			if (flag)
			{
				RefreshColor(null, anime);
				if (Checked)
				{
					Check?.Invoke(this, raiseByMouse);
				}
				ModMain.RaiseCustomEvent(this);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "单选按钮勾选改变错误", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyRadioButton.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void Radiobox_MouseUp()
	{
		if (!Checked && IsMouseDown)
		{
			Logger.Info($"按下单选按钮：{Text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyRadioButton.xaml.vb");
			IsMouseDown = false;
			ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(RaiseByMouse: true);
			PreviewClick?.Invoke(this, routeEventArgs);
			if (!routeEventArgs.Handled)
			{
				SetChecked(value: true, raiseByMouse: true, anime: true);
			}
		}
	}

	private void Radiobox_MouseDown()
	{
		if (!Checked)
		{
			IsMouseDown = true;
			RefreshColor();
		}
	}

	private void Radiobox_MouseLeave()
	{
		IsMouseDown = false;
	}

	private void RefreshColor(object obj = null, object e = null)
	{
		try
		{
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0 && !false.Equals(RuntimeHelpers.GetObjectValue(e)))
			{
				switch (ColorType)
				{
				case ColorState.White:
					if (Checked)
					{
						ModAnimation.AniStart(new ModAnimation.AniData[2]
						{
							ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, "ColorBrush3", 120),
							ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrush3", 120)
						}, "MyRadioButton Checked " + Conversions.ToString(Uuid));
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - base.Background, 120), "MyRadioButton Color " + Conversions.ToString(Uuid));
					}
					else if (IsMouseDown)
					{
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, new ModBase.MyColor(120.0, ModSecret.Color8) - base.Background, 60), "MyRadioButton Color " + Conversions.ToString(Uuid));
					}
					else if (base.IsMouseOver)
					{
						ModAnimation.AniStart(new ModAnimation.AniData[2]
						{
							ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - ShapeLogo.Fill, 90),
							ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - LabText.Foreground, 90)
						}, "MyRadioButton Checked " + Conversions.ToString(Uuid));
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, new ModBase.MyColor(50.0, ModSecret.Color8) - base.Background, 90), "MyRadioButton Color " + Conversions.ToString(Uuid));
					}
					else
					{
						ModAnimation.AniStart(new ModAnimation.AniData[2]
						{
							ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - ShapeLogo.Fill, 150),
							ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - LabText.Foreground, 150)
						}, "MyRadioButton Checked " + Conversions.ToString(Uuid));
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret.ColorSemiTransparent - base.Background, 150), "MyRadioButton Color " + Conversions.ToString(Uuid));
					}
					break;
				case ColorState.Highlight:
					if (Checked)
					{
						ModAnimation.AniStart(new ModAnimation.AniData[2]
						{
							ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - ShapeLogo.Fill, 120),
							ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, new ModBase.MyColor(255.0, 255.0, 255.0) - LabText.Foreground, 120)
						}, "MyRadioButton Checked " + Conversions.ToString(Uuid));
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrush3", 120), "MyRadioButton Color " + Conversions.ToString(Uuid));
					}
					else if (IsMouseDown)
					{
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrush6", 90), "MyRadioButton Color " + Conversions.ToString(Uuid));
					}
					else if (base.IsMouseOver)
					{
						ModAnimation.AniStart(new ModAnimation.AniData[2]
						{
							ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, "ColorBrush3", 90),
							ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrush3", 90)
						}, "MyRadioButton Checked " + Conversions.ToString(Uuid));
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, "ColorBrush7", 90), "MyRadioButton Color " + Conversions.ToString(Uuid));
					}
					else
					{
						ModAnimation.AniStart(new ModAnimation.AniData[2]
						{
							ModAnimation.AaColor(ShapeLogo, Shape.FillProperty, "ColorBrush3", 150),
							ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrush3", 150)
						}, "MyRadioButton Checked " + Conversions.ToString(Uuid));
						ModAnimation.AniStart(ModAnimation.AaColor(this, Border.BackgroundProperty, ModSecret.ColorSemiTransparent - base.Background, 150), "MyRadioButton Color " + Conversions.ToString(Uuid));
					}
					break;
				}
				return;
			}
			ModAnimation.AniStop("MyRadioButton Checked " + Conversions.ToString(Uuid));
			ModAnimation.AniStop("MyRadioButton Color " + Conversions.ToString(Uuid));
			switch (ColorType)
			{
			case ColorState.White:
				if (Checked)
				{
					base.Background = new ModBase.MyColor(255.0, 255.0, 255.0);
					ShapeLogo.SetResourceReference(Shape.FillProperty, "ColorBrush3");
					LabText.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrush3");
				}
				else
				{
					base.Background = ModSecret.ColorSemiTransparent;
					ShapeLogo.Fill = new ModBase.MyColor(255.0, 255.0, 255.0);
					LabText.Foreground = new ModBase.MyColor(255.0, 255.0, 255.0);
				}
				break;
			case ColorState.Highlight:
				if (Checked)
				{
					SetResourceReference(Border.BackgroundProperty, "ColorBrush3");
					ShapeLogo.Fill = new ModBase.MyColor(255.0, 255.0, 255.0);
					LabText.Foreground = new ModBase.MyColor(255.0, 255.0, 255.0);
				}
				else
				{
					base.Background = ModSecret.ColorSemiTransparent;
					ShapeLogo.SetResourceReference(Shape.FillProperty, "ColorBrush3");
					LabText.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrush3");
				}
				break;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "刷新单选按钮颜色出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyRadioButton.xaml.vb");
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
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myradiobutton.xaml", UriKind.Relative);
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
			PanBack = (MyRadioButton)target;
			break;
		case 2:
			ShapeLogo = (Path)target;
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
