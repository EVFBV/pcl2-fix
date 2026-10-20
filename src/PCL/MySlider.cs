using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using MeloongCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class MySlider : Border, ISettingControl, IComponentConnector
{
	public delegate void ChangeEventHandler(object sender, bool user);

	public delegate void PreviewChangeEventHandler(object sender, ModBase.RouteEventArgs e);

	public int Uuid;

	private int _MaxValue;

	private bool ChangeByKey;

	private int _Value;

	public Delegate GetHintText;

	private bool _contentLoaded;

	public int MaxValue
	{
		get
		{
			return _MaxValue;
		}
		set
		{
			if (value != _MaxValue)
			{
				_MaxValue = value;
				RefreshWidth(null, null);
			}
		}
	}

	public int Value
	{
		get
		{
			return _Value;
		}
		set
		{
			try
			{
				value = MathUtils.Clamp<int>(value, 0, MaxValue);
				if (_Value == value)
				{
					return;
				}
				int value2 = _Value;
				_Value = value;
				if (ModAnimation.AniControlEnabled == 0)
				{
					ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs();
					PreviewChange?.Invoke(this, routeEventArgs);
					if (routeEventArgs.Handled)
					{
						_Value = value2;
						DragStop();
						return;
					}
				}
				if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
				{
					if (base.ActualWidth < ShapeDot.Width)
					{
						return;
					}
					double num = (double)_Value / (double)MaxValue * (base.ActualWidth - ShapeDot.Width);
					double num2 = Math.Abs(LineFore.Width / (base.ActualWidth - ShapeDot.Width) - (double)_Value / (double)MaxValue);
					double num3 = (1.0 - Math.Pow(1.0 - num2, 3.0)) * 300.0 + (double)(ChangeByKey ? 100 : 0);
					ModAnimation.AniStart(checked(new ModAnimation.AniData[3]
					{
						ModAnimation.AaWidth(LineFore, Math.Max(0.0, num + ((num < 0.5) ? 0.0 : 0.5)) - LineFore.Width, (int)Math.Round(num3), 0, (num3 > 50.0) ? ((ModAnimation.AniEase)new ModAnimation.AniEaseOutFluent()) : ((ModAnimation.AniEase)new ModAnimation.AniEaseLinear())),
						ModAnimation.AaWidth(LineBack, Math.Max(0.0, base.ActualWidth - ShapeDot.Width - num + ((base.ActualWidth - ShapeDot.Width - num < 0.5) ? 0.0 : 0.5)) - LineBack.Width, (int)Math.Round(num3), 0, (num3 > 50.0) ? ((ModAnimation.AniEase)new ModAnimation.AniEaseOutFluent()) : ((ModAnimation.AniEase)new ModAnimation.AniEaseLinear())),
						ModAnimation.AaX(ShapeDot, num - ShapeDot.Margin.Left, (int)Math.Round(num3), 0, (num3 > 50.0) ? ((ModAnimation.AniEase)new ModAnimation.AniEaseOutFluent()) : ((ModAnimation.AniEase)new ModAnimation.AniEaseLinear()))
					}), "MySlider Progress " + Conversions.ToString(Uuid));
				}
				else
				{
					RefreshWidth(null, null);
				}
				if (ModAnimation.AniControlEnabled == 0)
				{
					Change?.Invoke(this, user: false);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "滑动条进度改变出错", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MySlider.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
	}

	public uint ValueByKey { get; set; }

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MySlider PanBack
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

	[field: AccessedThroughProperty("LineBack")]
	internal virtual Line LineBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LineFore")]
	internal virtual Line LineFore
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ShapeDot")]
	internal virtual Ellipse ShapeDot
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Popup")]
	internal virtual Popup Popup
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TextHint")]
	internal virtual TextBlock TextHint
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public event ChangeEventHandler Change;

	public event PreviewChangeEventHandler PreviewChange;

	public MySlider()
	{
		base.SizeChanged += RefreshWidth;
		base.MouseLeftButtonDown += DragStart;
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.MouseEnter += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			RefreshColor();
		};
		base.MouseLeave += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			RefreshColor();
		};
		base.MouseEnter += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			MySlider_MouseEnter();
		};
		base.KeyDown += MySlider_KeyDown;
		Change += [SpecialName] (object a0, bool a1) =>
		{
			SaveSetting();
		};
		Uuid = ModBase.GetUuid();
		_MaxValue = 100;
		ChangeByKey = false;
		_Value = 0;
		ValueByKey = 1u;
		InitializeComponent();
	}

	private void RefreshWidth(object sender, SizeChangedEventArgs e)
	{
		if (!Information.IsNothing(e))
		{
			PanMain.Width = e.NewSize.Width;
		}
		ModAnimation.AniStop("MySlider Progress " + Conversions.ToString(Uuid));
		double num = (double)_Value / (double)MaxValue * (base.ActualWidth - ShapeDot.Width);
		LineFore.Width = Math.Max(0.0, num + ((num < 0.5) ? 0.0 : 0.5));
		LineBack.Width = Math.Max(0.0, base.ActualWidth - ShapeDot.Width - num + ((base.ActualWidth - ShapeDot.Width - num < 0.5) ? 0.0 : 0.5));
		ShapeDot.Margin = new Thickness(num, ShapeDot.Margin.Top, ShapeDot.Margin.Right, ShapeDot.Margin.Bottom);
	}

	private void DragStart(object sender, MouseButtonEventArgs e)
	{
		e.Handled = true;
		ModMain.DragControl = this;
		RefreshColor();
		ModMain.FrmMain.DragDoing();
		ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(ShapeDot, 1.3 - ((ScaleTransform)ShapeDot.RenderTransform).ScaleX, 40, 0, new ModAnimation.AniEaseOutFluent()) }, "MySlider Scale " + Conversions.ToString(Uuid));
		RefreshPopup();
		ModAnimation.AniStop("MySlider KeyPopup " + Conversions.ToString(Uuid));
	}

	public void DragDoing()
	{
		int num = checked((int)Math.Round(MathUtils.Clamp<double>((Mouse.GetPosition(PanMain).X - ShapeDot.Width / 2.0) / (base.ActualWidth - ShapeDot.Width), 0.0, 1.0) * (double)MaxValue));
		if (num != Value)
		{
			Value = num;
		}
		RefreshPopup();
	}

	public void DragStop()
	{
		RefreshColor();
		ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScaleTransform(ShapeDot, 1.0 - ((ScaleTransform)ShapeDot.RenderTransform).ScaleX, 200, 0, new ModAnimation.AniEaseOutFluent()) }, "MySlider Scale " + Conversions.ToString(Uuid));
		Popup.IsOpen = false;
	}

	public void RefreshPopup()
	{
		if ((object)GetHintText != null)
		{
			Popup.IsOpen = true;
			TextHint.Text = Conversions.ToString(GetHintText.DynamicInvoke(Value));
			Typeface typeface = new Typeface(TextHint.FontFamily, TextHint.FontStyle, TextHint.FontWeight, TextHint.FontStretch);
			FormattedText formattedText = new FormattedText(TextHint.Text, Thread.CurrentThread.CurrentCulture, TextHint.FlowDirection, typeface, TextHint.FontSize, TextHint.Foreground, ModBase.DPI);
			TextHint.Width = formattedText.Width;
		}
	}

	private void RefreshColor()
	{
		try
		{
			string text;
			string text2;
			int time;
			if (base.IsEnabled)
			{
				if (!Information.IsNothing(RuntimeHelpers.GetObjectValue(ModMain.DragControl)) && ModMain.DragControl.Equals(this))
				{
					text = "ColorBrush3";
					text2 = "ColorBrush3";
					time = 40;
				}
				else if (base.IsMouseOver)
				{
					text = "ColorBrush3";
					text2 = "ColorBrush3";
					time = 40;
				}
				else
				{
					text = "ColorBrushBg0";
					text2 = "ColorBrushBg0";
					time = 100;
				}
			}
			else
			{
				text = "ColorBrushGray5";
				text2 = "ColorBrushGray5";
				time = 200;
			}
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaColor(this, Border.BorderBrushProperty, text, time),
					ModAnimation.AaColor(ShapeDot, Shape.FillProperty, text2, time)
				}, "MySlider Color " + Conversions.ToString(Uuid));
			}
			else
			{
				ModAnimation.AniStop("MySlider Color " + Conversions.ToString(Uuid));
				SetResourceReference(Border.BorderBrushProperty, text);
				ShapeDot.SetResourceReference(Shape.FillProperty, text2);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "滑动条颜色改变出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MySlider.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void MySlider_MouseEnter()
	{
		Focus();
	}

	private void MySlider_KeyDown(object sender, KeyEventArgs e)
	{
		if (object.ReferenceEquals(this, RuntimeHelpers.GetObjectValue(ModMain.DragControl)))
		{
			return;
		}
		checked
		{
			if (e.Key == Key.Left)
			{
				ChangeByKey = true;
				Value = (int)(Value - ValueByKey);
				ChangeByKey = false;
				e.Handled = true;
			}
			else
			{
				if (e.Key != Key.Right)
				{
					return;
				}
				ChangeByKey = true;
				Value = (int)(Value + ValueByKey);
				ChangeByKey = false;
				e.Handled = true;
			}
			if ((object)GetHintText != null)
			{
				RefreshPopup();
				ModAnimation.AniStop("MySlider KeyPopup " + Conversions.ToString(Uuid));
				ModAnimation.AniStart(ModAnimation.AaCode([SpecialName] () =>
				{
					Popup.IsOpen = false;
				}, (int)Math.Round(700.0 * ModAnimation.AniSpeed)), "MySlider KeyPopup " + Conversions.ToString(Uuid));
			}
		}
	}

	private void RefreshSetting(string NewValue)
	{
		Value = checked((int)Math.Round(ModBase.Val(NewValue)));
	}

	void ISettingControl.RefreshSetting(string NewValue)
	{
		//ILSpy generated this explicit interface implementation from .override directive in RefreshSetting
		this.RefreshSetting(NewValue);
	}

	private string GetCurrentSetting()
	{
		return Conversions.ToString(Value);
	}

	string ISettingControl.GetCurrentSetting()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetCurrentSetting
		return this.GetCurrentSetting();
	}

	private void SaveSetting()
	{
		SettingService.SaveSetting(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myslider.xaml", UriKind.Relative);
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
			PanBack = (MySlider)target;
			break;
		case 2:
			PanMain = (Grid)target;
			break;
		case 3:
			LineBack = (Line)target;
			break;
		case 4:
			LineFore = (Line)target;
			break;
		case 5:
			ShapeDot = (Ellipse)target;
			break;
		case 6:
			Popup = (Popup)target;
			break;
		case 7:
			TextHint = (TextBlock)target;
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
