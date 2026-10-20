using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[ContentProperty("Inlines")]
[DesignerGenerated]
public class MyListItem : Grid, IMyRadio, IComponentConnector
{
	public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

	public delegate void LogoClickEventHandler(object sender, MouseButtonEventArgs e);

	public enum CheckType
	{
		None,
		Clickable,
		RadioBox,
		CheckBox
	}

	private Border _RectBack;

	public FrameworkElement ButtonStack;

	public FrameworkElement PathLogo;

	public Border RectCheck;

	private TextBlock _LabInfo;

	public int Uuid;

	private bool _IsScaleAnimationEnabled;

	private IEnumerable<MyIconButton> _Buttons;

	public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MyListItem));

	public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(MyListItem), new PropertyMetadata(14.0));

	private string _Info;

	private string _Logo;

	private double _LogoScale;

	private bool IsLogoDown;

	private CheckType _Type;

	private bool _Checked;

	public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(MyListItem), new PropertyMetadata((SolidColorBrush)ModSecret.Color1));

	public Action<MyListItem, EventArgs> ContentHandler;

	private bool IsMouseDown;

	private string StateLast;

	public bool IsMouseOverAnimationEnabled;

	private bool _contentLoaded;

	public Border RectBack
	{
		get
		{
			if (_RectBack == null)
			{
				Border border = new Border
				{
					Name = "RectBack",
					CornerRadius = new CornerRadius((IsScaleAnimationEnabled || base.Height > 40.0) ? 6 : 0),
					RenderTransform = (IsScaleAnimationEnabled ? new ScaleTransform(0.8, 0.8) : null),
					RenderTransformOrigin = new Point(0.5, 0.5),
					BorderThickness = new Thickness(ModBase.GetWPFSize(1.0)),
					SnapsToDevicePixels = true,
					IsHitTestVisible = false,
					Opacity = 0.0
				};
				border.SetResourceReference(Border.BackgroundProperty, "ColorBrush7");
				border.SetResourceReference(Border.BorderBrushProperty, "ColorBrush6");
				Grid.SetColumnSpan(border, 999);
				Grid.SetRowSpan(border, 999);
				base.Children.Insert(0, border);
				_RectBack = border;
			}
			return _RectBack;
		}
	}

	public TextBlock LabInfo
	{
		get
		{
			if (_LabInfo == null)
			{
				TextBlock textBlock = new TextBlock
				{
					Name = "LabInfo",
					SnapsToDevicePixels = false,
					UseLayoutRounding = false,
					HorizontalAlignment = HorizontalAlignment.Left,
					IsHitTestVisible = false,
					TextTrimming = TextTrimming.CharacterEllipsis,
					Visibility = Visibility.Collapsed,
					FontSize = 12.0,
					Margin = new Thickness(4.0, 0.0, 0.0, 0.0),
					Opacity = 0.6
				};
				Grid.SetColumn(textBlock, 3);
				Grid.SetRow(textBlock, 2);
				base.Children.Add(textBlock);
				_LabInfo = textBlock;
			}
			return _LabInfo;
		}
	}

	public bool IsScaleAnimationEnabled
	{
		get
		{
			return _IsScaleAnimationEnabled;
		}
		set
		{
			_IsScaleAnimationEnabled = value;
			if (_RectBack != null)
			{
				RectBack.CornerRadius = new CornerRadius(value ? 6 : 0);
			}
		}
	}

	public int PaddingLeft
	{
		get
		{
			return checked((int)Math.Round(ColumnPaddingLeft.Width.Value));
		}
		set
		{
			ColumnPaddingLeft.Width = new GridLength(value);
		}
	}

	public int MinPaddingRight { get; set; }

	public IEnumerable<MyIconButton> Buttons
	{
		get
		{
			return _Buttons;
		}
		set
		{
			_Buttons = value;
			if (ButtonStack != null)
			{
				base.Children.Remove(ButtonStack);
				ButtonStack = null;
			}
			switch (value.Count())
			{
			case 1:
			{
				foreach (MyIconButton item in value)
				{
					if (item.Height.Equals(double.NaN))
					{
						item.Height = 25.0;
					}
					if (item.Width.Equals(double.NaN))
					{
						item.Width = 25.0;
					}
					item.Opacity = 0.0;
					item.Margin = new Thickness(0.0, 0.0, 5.0, 0.0);
					item.SnapsToDevicePixels = false;
					item.HorizontalAlignment = HorizontalAlignment.Right;
					item.VerticalAlignment = VerticalAlignment.Center;
					item.SnapsToDevicePixels = false;
					item.UseLayoutRounding = false;
					_ = null;
					Grid.SetColumnSpan(item, 10);
					Grid.SetRowSpan(item, 10);
					base.Children.Add(item);
					ButtonStack = item;
				}
				return;
			}
			case 0:
				return;
			}
			ButtonStack = new StackPanel
			{
				Opacity = 0.0,
				Margin = new Thickness(0.0, 0.0, 5.0, 0.0),
				SnapsToDevicePixels = false,
				Orientation = Orientation.Horizontal,
				HorizontalAlignment = HorizontalAlignment.Right,
				VerticalAlignment = VerticalAlignment.Center,
				UseLayoutRounding = false
			};
			Grid.SetColumnSpan(ButtonStack, 10);
			Grid.SetRowSpan(ButtonStack, 10);
			foreach (MyIconButton item2 in value)
			{
				if (item2.Height.Equals(double.NaN))
				{
					item2.Height = 25.0;
				}
				if (item2.Width.Equals(double.NaN))
				{
					item2.Width = 25.0;
				}
				((StackPanel)ButtonStack).Children.Add(item2);
			}
			base.Children.Add(ButtonStack);
		}
	}

	public InlineCollection Inlines => LabTitle.Inlines;

	public string Title
	{
		get
		{
			return Conversions.ToString(GetValue(TitleProperty));
		}
		set
		{
			SetValue(TitleProperty, StringExtensions.ReplaceLineEndings(value, "", false));
		}
	}

	public double FontSize
	{
		get
		{
			return Conversions.ToDouble(GetValue(FontSizeProperty));
		}
		set
		{
			SetValue(FontSizeProperty, value);
		}
	}

	public string Info
	{
		get
		{
			return _Info;
		}
		set
		{
			if (Operators.CompareString(_Info, value, TextCompare: false) != 0)
			{
				value = StringExtensions.ReplaceLineEndings(value, "", false);
				_Info = value;
				LabInfo.Text = value;
				LabInfo.Visibility = ((Operators.CompareString(value, "", TextCompare: false) == 0) ? Visibility.Collapsed : Visibility.Visible);
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
			if (Operators.CompareString(_Logo, value, TextCompare: false) == 0)
			{
				return;
			}
			_Logo = value;
			if (!Information.IsNothing(PathLogo))
			{
				base.Children.Remove(PathLogo);
			}
			if (Operators.CompareString(_Logo, "", TextCompare: false) != 0)
			{
				if (StringExtensions.StartsWithF(_Logo, "http", true))
				{
					PathLogo = new MyImage
					{
						Tag = this,
						IsHitTestVisible = LogoClickable,
						Source = _Logo,
						RenderTransformOrigin = new Point(0.5, 0.5),
						RenderTransform = new ScaleTransform
						{
							ScaleX = LogoScale,
							ScaleY = LogoScale
						},
						SnapsToDevicePixels = true,
						UseLayoutRounding = false
					};
					RenderOptions.SetBitmapScalingMode(PathLogo, BitmapScalingMode.LowQuality);
				}
				else if (StringExtensions.EndsWithF(_Logo, ".png", true) || StringExtensions.EndsWithF(_Logo, ".jpg", true) || StringExtensions.EndsWithF(_Logo, ".webp", true))
				{
					PathLogo = new Canvas
					{
						Tag = this,
						IsHitTestVisible = LogoClickable,
						Background = (ImageBrush)new MyBitmap(_Logo),
						RenderTransformOrigin = new Point(0.5, 0.5),
						RenderTransform = new ScaleTransform
						{
							ScaleX = LogoScale,
							ScaleY = LogoScale
						},
						SnapsToDevicePixels = true,
						UseLayoutRounding = false,
						HorizontalAlignment = HorizontalAlignment.Stretch,
						VerticalAlignment = VerticalAlignment.Stretch
					};
					RenderOptions.SetBitmapScalingMode(PathLogo, BitmapScalingMode.LowQuality);
				}
				else
				{
					PathLogo = new Path
					{
						Tag = this,
						IsHitTestVisible = LogoClickable,
						HorizontalAlignment = HorizontalAlignment.Center,
						VerticalAlignment = VerticalAlignment.Center,
						Stretch = Stretch.Uniform,
						Data = (Geometry)new GeometryConverter().ConvertFromString(_Logo),
						RenderTransformOrigin = new Point(0.5, 0.5),
						RenderTransform = new ScaleTransform
						{
							ScaleX = LogoScale,
							ScaleY = LogoScale
						},
						SnapsToDevicePixels = false,
						UseLayoutRounding = false
					};
					PathLogo.SetBinding(Shape.FillProperty, new Binding("Foreground")
					{
						Source = this
					});
				}
				Grid.SetColumn(PathLogo, 2);
				Grid.SetRowSpan(PathLogo, 4);
				OnSizeChanged();
				base.Children.Add(PathLogo);
				if (LogoClickable)
				{
					PathLogo.MouseLeave += [SpecialName] (object sender, MouseEventArgs e) =>
					{
						IsLogoDown = false;
					};
					PathLogo.MouseLeftButtonDown += [SpecialName] (object sender, MouseButtonEventArgs e) =>
					{
						IsLogoDown = true;
					};
					PathLogo.MouseLeftButtonUp += [SpecialName] (object sender, MouseButtonEventArgs e) =>
					{
						if (IsLogoDown)
						{
							IsLogoDown = false;
							LogoClick?.Invoke(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null)), e);
						}
					};
				}
			}
			ColumnLogo.Width = new GridLength(checked(((Operators.CompareString(_Logo, "", TextCompare: false) != 0) ? 34 : 0) + ((!(base.Height < 40.0)) ? 4 : 0)));
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
			if (!Information.IsNothing(PathLogo))
			{
				PathLogo.RenderTransform = new ScaleTransform
				{
					ScaleX = LogoScale,
					ScaleY = LogoScale
				};
			}
		}
	}

	public bool LogoClickable { get; set; }

	public CheckType Type
	{
		get
		{
			return _Type;
		}
		set
		{
			if (_Type == value)
			{
				return;
			}
			_Type = value;
			ColumnCheck.Width = new GridLength((_Type != 0 && _Type != CheckType.Clickable) ? 6 : ((base.Height < 40.0) ? 4 : 2));
			if (_Type == CheckType.None || _Type == CheckType.Clickable)
			{
				if (!Information.IsNothing(RectCheck))
				{
					base.Children.Remove(RectCheck);
					RectCheck = null;
				}
				SetChecked(value: false, user: false, anime: false);
			}
			else if (Information.IsNothing(RectCheck))
			{
				RectCheck = new Border
				{
					Width = 5.0,
					Height = (Checked ? double.NaN : 0.0),
					CornerRadius = new CornerRadius(2.0, 2.0, 2.0, 2.0),
					VerticalAlignment = ((!Checked) ? VerticalAlignment.Center : VerticalAlignment.Stretch),
					HorizontalAlignment = HorizontalAlignment.Left,
					UseLayoutRounding = false,
					SnapsToDevicePixels = false,
					Margin = (Checked ? new Thickness(-1.0, 6.0, 0.0, 6.0) : new Thickness(-1.0, 0.0, 0.0, 0.0))
				};
				RectCheck.SetResourceReference(Border.BackgroundProperty, "ColorBrush3");
				Grid.SetRowSpan(RectCheck, 4);
				base.Children.Add(RectCheck);
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
			SetChecked(value, user: false, value != _Checked);
		}
	}

	public Brush Foreground
	{
		get
		{
			return (Brush)GetValue(ForegroundProperty);
		}
		set
		{
			SetValue(ForegroundProperty, value);
		}
	}

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyListItem PanBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnCheck")]
	internal virtual ColumnDefinition ColumnCheck
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnPaddingLeft")]
	internal virtual ColumnDefinition ColumnPaddingLeft
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnLogo")]
	internal virtual ColumnDefinition ColumnLogo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnPaddingRight")]
	internal virtual ColumnDefinition ColumnPaddingRight
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabTitle")]
	internal virtual TextBlock LabTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public event ClickEventHandler Click;

	public event LogoClickEventHandler LogoClick;

	public event IMyRadio.CheckEventHandler Check;

	public event IMyRadio.ChangedEventHandler Changed;

	public MyListItem()
	{
		base.SizeChanged += [SpecialName] (object a0, SizeChangedEventArgs a1) =>
		{
			OnSizeChanged();
		};
		base.PreviewMouseLeftButtonUp += Button_MouseUp;
		base.PreviewMouseLeftButtonDown += Button_MouseDown;
		base.MouseLeave += Button_MouseLeave;
		base.PreviewMouseLeftButtonUp += Button_MouseLeave;
		base.MouseEnter += RefreshColor;
		base.MouseLeave += RefreshColor;
		base.PreviewMouseLeftButtonDown += RefreshColor;
		base.PreviewMouseLeftButtonUp += RefreshColor;
		base.Loaded += MyListItem_Loaded;
		_RectBack = null;
		_LabInfo = null;
		Uuid = ModBase.GetUuid();
		_IsScaleAnimationEnabled = true;
		MinPaddingRight = 4;
		_Info = "";
		_Logo = "";
		_LogoScale = 1.0;
		LogoClickable = false;
		IsLogoDown = false;
		_Type = CheckType.None;
		_Checked = false;
		IsMouseDown = false;
		IsMouseOverAnimationEnabled = true;
		InitializeComponent();
	}

	private void OnSizeChanged()
	{
		ColumnCheck.Width = new GridLength((_Type != 0 && _Type != CheckType.Clickable) ? 6 : ((base.Height < 40.0) ? 4 : 2));
		ColumnLogo.Width = new GridLength(checked(((Operators.CompareString(_Logo, "", TextCompare: false) != 0) ? 34 : 0) + ((!(base.Height < 40.0)) ? 4 : 0)));
		if (PathLogo != null)
		{
			if (StringExtensions.EndsWithF(_Logo, ".png", true) || StringExtensions.EndsWithF(_Logo, ".jpg", true) || StringExtensions.EndsWithF(_Logo, ".webp", true))
			{
				PathLogo.Margin = new Thickness(4.0, 5.0, 3.0, 5.0);
			}
			else
			{
				PathLogo.Margin = new Thickness((base.Height < 40.0) ? 6 : 8, 8.0, (base.Height < 40.0) ? 4 : 6, 8.0);
			}
		}
		LabTitle.Margin = new Thickness(4.0, 0.0, 0.0, (!(base.Height < 40.0)) ? 2 : 0);
	}

	public void SetChecked(bool value, bool user, bool anime)
	{
		try
		{
			ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(user);
			bool @checked = _Checked;
			if (Type == CheckType.RadioBox)
			{
				if (base.IsInitialized && value != _Checked)
				{
					_Checked = value;
					Changed?.Invoke(this, routeEventArgs);
					if (routeEventArgs.Handled)
					{
						_Checked = @checked;
						return;
					}
				}
				_Checked = value;
			}
			else
			{
				if (value == _Checked)
				{
					return;
				}
				_Checked = value;
				if (base.IsInitialized)
				{
					Changed?.Invoke(this, routeEventArgs);
					if (routeEventArgs.Handled)
					{
						_Checked = @checked;
						return;
					}
				}
			}
			if (value)
			{
				ModBase.RouteEventArgs routeEventArgs2 = new ModBase.RouteEventArgs(user);
				Check?.Invoke(this, routeEventArgs2);
				if (routeEventArgs2.Handled)
				{
					return;
				}
			}
			if (Type == CheckType.RadioBox)
			{
				if (Information.IsNothing(base.Parent))
				{
					return;
				}
				List<MyListItem> list = new List<MyListItem>();
				int num = 0;
				foreach (object item in (IEnumerable)NewLateBinding.LateGet(base.Parent, null, "Children", new object[0], null, null, null))
				{
					object objectValue = RuntimeHelpers.GetObjectValue(item);
					objectValue = MyVirtualizingElement.TryInit((FrameworkElement)objectValue);
					if (objectValue is MyListItem && ((MyListItem)objectValue).Type == CheckType.RadioBox)
					{
						list.Add((MyListItem)objectValue);
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
						foreach (MyListItem item2 in list)
						{
							if (item2.Checked && !item2.Equals(this))
							{
								item2.Checked = false;
							}
						}
					}
					else
					{
						bool flag = false;
						foreach (MyListItem item3 in list)
						{
							if (item3.Checked)
							{
								if (flag)
								{
									item3.Checked = false;
								}
								else
								{
									flag = true;
								}
							}
						}
					}
				}
			}
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0 && anime)
			{
				List<ModAnimation.AniData> list2 = new List<ModAnimation.AniData>();
				if (Checked)
				{
					if (!Information.IsNothing(RectCheck))
					{
						double num3 = base.ActualHeight - RectCheck.ActualHeight - 12.0;
						list2.Add(ModAnimation.AaHeight(RectCheck, num3 * 0.4, 200, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)));
						list2.Add(ModAnimation.AaHeight(RectCheck, num3 * 0.6, 300, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)));
						list2.Add(ModAnimation.AaOpacity(RectCheck, 1.0 - RectCheck.Opacity, 30));
						RectCheck.VerticalAlignment = VerticalAlignment.Center;
						RectCheck.Margin = new Thickness(-1.0, 0.0, 0.0, 0.0);
					}
					list2.Add(ModAnimation.AaColor(this, ForegroundProperty, (base.Height < 40.0) ? "ColorBrush3" : "ColorBrush2", 200));
				}
				else
				{
					if (!Information.IsNothing(RectCheck))
					{
						list2.Add(ModAnimation.AaHeight(RectCheck, 0.0 - RectCheck.ActualHeight, 120, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak)));
						list2.Add(ModAnimation.AaOpacity(RectCheck, 0.0 - RectCheck.Opacity, 70, 40));
						RectCheck.VerticalAlignment = VerticalAlignment.Center;
					}
					list2.Add(ModAnimation.AaColor(this, ForegroundProperty, "ColorBrush1", 120));
				}
				ModAnimation.AniStart(list2, "MyListItem Checked " + Conversions.ToString(Uuid));
				return;
			}
			ModAnimation.AniStop("MyListItem Checked " + Conversions.ToString(Uuid));
			if (Checked)
			{
				if (!Information.IsNothing(RectCheck))
				{
					RectCheck.Height = double.NaN;
					RectCheck.Margin = new Thickness(-1.0, 6.0, 0.0, 6.0);
					RectCheck.Opacity = 1.0;
					RectCheck.VerticalAlignment = VerticalAlignment.Stretch;
				}
				SetResourceReference(ForegroundProperty, (base.Height < 40.0) ? "ColorBrush3" : "ColorBrush2");
			}
			else
			{
				if (!Information.IsNothing(RectCheck))
				{
					RectCheck.Height = 0.0;
					RectCheck.Margin = new Thickness(-1.0, 0.0, 0.0, 0.0);
					RectCheck.Opacity = 0.0;
					RectCheck.VerticalAlignment = VerticalAlignment.Center;
				}
				SetResourceReference(ForegroundProperty, "ColorBrush1");
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "设置 Checked 失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyListItem.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void Button_MouseUp(object sender, MouseButtonEventArgs e)
	{
		if (!IsMouseDown)
		{
			return;
		}
		Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
		if (e.Handled)
		{
			return;
		}
		if (CustomEventService.GetEventType((DependencyObject)sender) != 0 || CustomEventService.GetEvents((DependencyObject)sender).Events.Count > 0)
		{
			ModMain.RaiseCustomEvent(this);
			e.Handled = true;
		}
		if (e.Handled)
		{
			return;
		}
		switch (Type)
		{
		case CheckType.Clickable:
			Logger.Info($"按下单击列表项：{Title}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyListItem.xaml.vb");
			break;
		case CheckType.RadioBox:
			Logger.Info($"按下单选列表项：{Title}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyListItem.xaml.vb");
			if (!Checked)
			{
				SetChecked(value: true, user: true, anime: true);
			}
			break;
		case CheckType.CheckBox:
			Logger.Info($"按下复选列表项（{!Checked}）：{Title}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyListItem.xaml.vb");
			SetChecked(!Checked, user: true, anime: true);
			break;
		}
	}

	private void Button_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (base.IsMouseDirectlyOver && Type != 0)
		{
			IsMouseDown = true;
			if (ButtonStack != null)
			{
				ButtonStack.IsHitTestVisible = false;
			}
		}
	}

	private void Button_MouseLeave(object sender, object e)
	{
		IsMouseDown = false;
		if (ButtonStack != null)
		{
			ButtonStack.IsHitTestVisible = true;
		}
	}

	public void RefreshColor(object sender, EventArgs e)
	{
		if (ContentHandler != null)
		{
			ContentHandler(this, e);
			ContentHandler = null;
		}
		string text;
		int num;
		if (IsMouseDown && (Type != CheckType.RadioBox || !Checked))
		{
			text = "MouseDown";
			num = 120;
		}
		else if (base.IsMouseOver && IsMouseOverAnimationEnabled)
		{
			text = "MouseOver";
			num = 120;
		}
		else
		{
			text = "Idle";
			num = 180;
		}
		if (Operators.CompareString(StateLast, text, TextCompare: false) == 0)
		{
			return;
		}
		StateLast = text;
		checked
		{
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
				if (base.IsMouseOver && IsMouseOverAnimationEnabled)
				{
					if (ButtonStack != null)
					{
						list.Add(ModAnimation.AaOpacity(ButtonStack, 1.0 - ButtonStack.Opacity, (int)Math.Round((double)num * 0.7), (int)Math.Round((double)num * 0.3)));
						list.Add(ModAnimation.AaDouble([SpecialName] (object i) =>
						{
							ColumnPaddingRight.Width = new GridLength(Conversions.ToDouble(NewLateBinding.LateGet(null, typeof(Math), "Max", new object[2]
							{
								0,
								Operators.AddObject(ColumnPaddingRight.Width.Value, i)
							}, null, null, null)));
						}, (double)Math.Max(MinPaddingRight, 5 + Buttons.Count() * 25) - ColumnPaddingRight.Width.Value, (int)Math.Round((double)num * 0.3), (int)Math.Round((double)num * 0.7)));
					}
					list.AddRange(new ModAnimation.AniData[2]
					{
						ModAnimation.AaColor(RectBack, Border.BackgroundProperty, IsMouseDown ? "ColorBrush6" : "ColorBrushBg1", num),
						ModAnimation.AaOpacity(RectBack, 1.0 - RectBack.Opacity, num, 0, new ModAnimation.AniEaseOutFluent())
					});
					if (IsScaleAnimationEnabled)
					{
						list.Add(ModAnimation.AaScaleTransform(RectBack, 1.0 - ((ScaleTransform)RectBack.RenderTransform).ScaleX, (int)Math.Round((double)num * 1.6), 0, new ModAnimation.AniEaseOutFluent()));
						if (IsMouseDown)
						{
							list.Add(ModAnimation.AaScaleTransform(this, 0.98 - ((ScaleTransform)base.RenderTransform).ScaleX, (int)Math.Round((double)num * 0.9), 0, new ModAnimation.AniEaseOutFluent()));
						}
						else
						{
							list.Add(ModAnimation.AaScaleTransform(this, 1.0 - ((ScaleTransform)base.RenderTransform).ScaleX, (int)Math.Round((double)num * 1.2), 0, new ModAnimation.AniEaseOutFluent()));
						}
					}
				}
				else
				{
					if (ButtonStack != null)
					{
						list.Add(ModAnimation.AaOpacity(ButtonStack, 0.0 - ButtonStack.Opacity, (int)Math.Round((double)num * 0.4)));
						list.Add(ModAnimation.AaDouble([SpecialName] (object i) =>
						{
							ColumnPaddingRight.Width = new GridLength(Conversions.ToDouble(NewLateBinding.LateGet(null, typeof(Math), "Max", new object[2]
							{
								0,
								Operators.AddObject(ColumnPaddingRight.Width.Value, i)
							}, null, null, null)));
						}, (double)MinPaddingRight - ColumnPaddingRight.Width.Value, (int)Math.Round((double)num * 0.4)));
					}
					list.Add(ModAnimation.AaOpacity(RectBack, 0.0 - RectBack.Opacity, num));
					if (IsScaleAnimationEnabled)
					{
						list.AddRange(new ModAnimation.AniData[4]
						{
							ModAnimation.AaColor(RectBack, Border.BackgroundProperty, IsMouseDown ? "ColorBrush6" : "ColorBrush7", num),
							ModAnimation.AaScaleTransform(this, 1.0 - ((ScaleTransform)base.RenderTransform).ScaleX, num * 3, 0, new ModAnimation.AniEaseOutFluent()),
							ModAnimation.AaScaleTransform(RectBack, 0.996 - ((ScaleTransform)RectBack.RenderTransform).ScaleX, num, 0, new ModAnimation.AniEaseOutFluent()),
							ModAnimation.AaScaleTransform(RectBack, -0.246, 1, 0, null, After: true)
						});
					}
				}
				ModAnimation.AniStart(list, "ListItem Color " + Conversions.ToString(Uuid));
				return;
			}
			if (base.IsMouseOver && IsMouseOverAnimationEnabled)
			{
				if (ButtonStack != null)
				{
					ButtonStack.Opacity = 1.0;
					ColumnPaddingRight.Width = new GridLength(Math.Max(MinPaddingRight, 5 + Buttons.Count() * 25));
				}
				RectBack.Background = ModSecret.ColorBg1;
				RectBack.Opacity = 1.0;
				RectBack.RenderTransform = new ScaleTransform(1.0, 1.0);
				base.RenderTransform = new ScaleTransform(1.0, 1.0);
			}
			else
			{
				if (ButtonStack != null)
				{
					ButtonStack.Opacity = 0.0;
					ColumnPaddingRight.Width = new GridLength(MinPaddingRight);
				}
				base.RenderTransform = new ScaleTransform(1.0, 1.0);
				if (_RectBack != null)
				{
					if (IsScaleAnimationEnabled)
					{
						RectBack.RenderTransform = new ScaleTransform(0.75, 0.75);
					}
					RectBack.Background = ModSecret.Color7;
					RectBack.Opacity = 0.0;
				}
			}
			ModAnimation.AniStop("ListItem Color " + Conversions.ToString(Uuid));
		}
	}

	private void MyListItem_Loaded(object sender, RoutedEventArgs e)
	{
		if (Checked)
		{
			SetResourceReference(ForegroundProperty, (base.Height < 40.0) ? "ColorBrush3" : "ColorBrush2");
		}
		else
		{
			SetResourceReference(ForegroundProperty, "ColorBrush1");
		}
		ColumnPaddingRight.Width = new GridLength(MinPaddingRight);
		if (CustomEventService.GetEventType(this) == CustomEvent.EventType.打开帮助 && (Operators.CompareString(Title, "", TextCompare: false) == 0 || Operators.CompareString(Info, "", TextCompare: false) == 0))
		{
			try
			{
				new ModMain.HelpEntry(CustomEvent.GetAbsoluteUrls(CustomEventService.GetEventData(this), CustomEventService.GetEventType(this))[0]).SetToListItem(this);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "设置帮助 MyListItem 失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyListItem.xaml.vb");
				CustomEventService.SetEventType(this, CustomEvent.EventType.None);
				CustomEventService.SetEventData(this, "");
				ProjectData.ClearProjectError();
			}
		}
	}

	public override string ToString()
	{
		return Title;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mylistitem.xaml", UriKind.Relative);
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
			PanBack = (MyListItem)target;
			break;
		case 2:
			ColumnCheck = (ColumnDefinition)target;
			break;
		case 3:
			ColumnPaddingLeft = (ColumnDefinition)target;
			break;
		case 4:
			ColumnLogo = (ColumnDefinition)target;
			break;
		case 5:
			ColumnPaddingRight = (ColumnDefinition)target;
			break;
		case 6:
			LabTitle = (TextBlock)target;
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
