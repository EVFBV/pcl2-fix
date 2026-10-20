using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using MeloongCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class MyCard : Grid
{
	public delegate void PreviewSwapEventHandler(object sender, ModBase.RouteEventArgs e);

	public delegate void SwapEventHandler(object sender, ModBase.RouteEventArgs e);

	[CompilerGenerated]
	internal sealed class _Closure_0024__32_002D0<_0024CLS0>
	{
		public IEnumerable<_0024CLS0> _0024VB_0024Local_Items;

		public Func<_0024CLS0, UIElement> _0024VB_0024Local_ItemBuilder;

		public _Closure_0024__32_002D0(_Closure_0024__32_002D0<_0024CLS0> arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Items = arg0._0024VB_0024Local_Items;
				_0024VB_0024Local_ItemBuilder = arg0._0024VB_0024Local_ItemBuilder;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(Panel Stack)
		{
			foreach (_0024CLS0 _0024VB_0024Local_Item in _0024VB_0024Local_Items)
			{
				Stack.Children.Add(_0024VB_0024Local_ItemBuilder(_0024VB_0024Local_Item));
			}
			Stack.Children.Add(new FrameworkElement
			{
				Height = 18.0
			});
		}
	}

	private readonly Grid MainGrid;

	private readonly Border MainBorder;

	private TextBlock _MainTextBlock;

	private Path _MainSwap;

	public int Uuid;

	public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MyCard), new PropertyMetadata(""));

	private bool IsLoad;

	private Action<Panel> ContentBuilder;

	private const double DropShadowIdleOpacity = 0.07;

	private const double DropShadowHoverOpacity = 0.4;

	private bool IsHeightAnimating;

	private double ActualUsedHeight;

	public FrameworkElement SwapControl;

	private bool _IsSwapped;

	public static readonly DependencyProperty IsSwapedProperty = DependencyProperty.Register("IsSwaped", typeof(bool), typeof(MyCard), new PropertyMetadata(false, [SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
	{
		if (sender != null && sender is MyCard)
		{
			((MyCard)sender).IsSwapped = Conversions.ToBoolean(e.NewValue);
		}
	}));

	private bool IsSwapMouseDown;

	private bool IsCustomMouseDown;

	public const int SwapedHeight = 40;

	public MyDropShadow MainChrome { get; }

	public UIElement BorderChild
	{
		get
		{
			return MainBorder.Child;
		}
		set
		{
			MainBorder.Child = value;
		}
	}

	public TextBlock MainTextBlock
	{
		get
		{
			Init();
			return _MainTextBlock;
		}
		set
		{
			_MainTextBlock = value;
		}
	}

	public Path MainSwap
	{
		get
		{
			Init();
			return _MainSwap;
		}
		set
		{
			_MainSwap = value;
		}
	}

	public InlineCollection Inlines => MainTextBlock.Inlines;

	public CornerRadius CornerRadius
	{
		get
		{
			return MainChrome.CornerRadius;
		}
		set
		{
			MainChrome.CornerRadius = value;
			MainBorder.CornerRadius = value;
		}
	}

	public string Title
	{
		get
		{
			return Conversions.ToString(GetValue(TitleProperty));
		}
		set
		{
			SetValue(TitleProperty, value);
			if (_MainTextBlock != null)
			{
				MainTextBlock.Text = value;
			}
		}
	}

	public bool HasMouseAnimation { get; set; }

	public bool UseAnimation { get; set; }

	public bool CanSwap { get; set; }

	public bool IsSwapped
	{
		get
		{
			return _IsSwapped;
		}
		set
		{
			if (_IsSwapped == value)
			{
				return;
			}
			_IsSwapped = value;
			if (SwapControl != null)
			{
				if (!IsSwapped)
				{
					BuildContent();
				}
				if (base.IsLoaded)
				{
					SwapControl.Visibility = Visibility.Visible;
					TriggerForceResize();
					UpdateCursorStyle();
					ModAnimation.AniStart(ModAnimation.AaRotateTransform(MainSwap, (double)((!_IsSwapped) ? 180 : (SwapLogoRight ? 270 : 0)) - ((RotateTransform)MainSwap.RenderTransform).Angle, 250, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong)), "MyCard Swap " + Conversions.ToString(Uuid), RefreshTime: true);
				}
			}
		}
	}

	[Obsolete("IsSwaped 存在拼写错误，请换用 IsSwapped 属性。")]
	public bool IsSwaped
	{
		get
		{
			return Conversions.ToBoolean(GetValue(IsSwapedProperty));
		}
		set
		{
			SetValue(IsSwapedProperty, value);
		}
	}

	public bool SwapLogoRight { get; set; }

	public event PreviewSwapEventHandler PreviewSwap;

	public event SwapEventHandler Swap;

	public MyCard()
	{
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			Init();
		};
		base.MouseEnter += MyCard_MouseEnter;
		base.MouseLeave += MyCard_MouseLeave;
		base.SizeChanged += MySizeChanged;
		base.MouseLeftButtonDown += MyCard_MouseLeftButtonDown;
		base.MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
		{
			MyCard_MouseLeftButtonUp();
		};
		base.MouseLeave += MyCard_MouseLeave_Swap;
		base.MouseMove += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			UpdateCursorStyle();
		};
		Uuid = ModBase.GetUuid();
		IsLoad = false;
		HasMouseAnimation = true;
		UseAnimation = true;
		IsHeightAnimating = false;
		CanSwap = false;
		_IsSwapped = false;
		SwapLogoRight = false;
		IsSwapMouseDown = false;
		IsCustomMouseDown = false;
		MainChrome = new MyDropShadow
		{
			Margin = new Thickness(-3.0, -3.0, -3.0, -3.0 - ModBase.GetWPFSize(1.0)),
			ShadowRadius = 3.0,
			Opacity = 0.07,
			CornerRadius = new CornerRadius(5.0)
		};
		MainChrome.SetResourceReference(MyDropShadow.ColorProperty, "ColorObject1");
		base.Children.Insert(0, MainChrome);
		MainBorder = new Border
		{
			Background = new SolidColorBrush(Color.FromArgb(245, byte.MaxValue, byte.MaxValue, byte.MaxValue)),
			CornerRadius = new CornerRadius(5.0),
			IsHitTestVisible = false
		};
		base.Children.Insert(1, MainBorder);
		MainGrid = new Grid();
		base.Children.Add(MainGrid);
	}

	private void Init()
	{
		if (IsLoad)
		{
			return;
		}
		IsLoad = true;
		if (MainTextBlock == null)
		{
			MainTextBlock = new TextBlock
			{
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top,
				Margin = new Thickness(15.0, 12.0, 0.0, 0.0),
				FontWeight = FontWeights.Bold,
				FontSize = 13.0,
				IsHitTestVisible = false
			};
			MainTextBlock.SetResourceReference(TextBlock.ForegroundProperty, "ColorBrush1");
			MainTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Title")
			{
				Source = this,
				Mode = BindingMode.OneWay
			});
			MainGrid.Children.Add(MainTextBlock);
		}
		if (CanSwap || SwapControl != null)
		{
			if (SwapControl == null && base.Children.Count > 3)
			{
				SwapControl = (FrameworkElement)base.Children[3];
			}
			MainSwap = new Path
			{
				HorizontalAlignment = HorizontalAlignment.Right,
				Stretch = Stretch.Uniform,
				Height = 6.0,
				Width = 10.0,
				VerticalAlignment = VerticalAlignment.Top,
				Margin = new Thickness(0.0, 17.0, 16.0, 0.0),
				Data = (Geometry)new GeometryConverter().ConvertFromString("M2,4 l-2,2 10,10 10,-10 -2,-2 -8,8 -8,-8 z"),
				RenderTransform = new RotateTransform(180.0),
				RenderTransformOrigin = new Point(0.5, 0.5)
			};
			MainSwap.SetResourceReference(Shape.FillProperty, "ColorBrush1");
			MainGrid.Children.Add(MainSwap);
		}
		if (IsSwapped && SwapControl != null)
		{
			MainSwap.RenderTransform = new RotateTransform(SwapLogoRight ? 270 : 0);
			SwapControl.Visibility = Visibility.Collapsed;
			bool useAnimation = UseAnimation;
			UseAnimation = false;
			base.Height = 40.0;
			ModAnimation.AniStop("MyCard Height " + Conversions.ToString(Uuid));
			IsHeightAnimating = false;
			ModBase.RunInUi([SpecialName] () =>
			{
				UseAnimation = useAnimation;
			}, ForceWaitUntilLoaded: true);
		}
	}

	public void SetContentBuilder(Action<Panel> Builder)
	{
		ContentBuilder = Builder;
	}

	public void SetContentBuilder<T>(IEnumerable<T> Items, Func<T, UIElement> ItemBuilder)
	{
		_Closure_0024__32_002D0<T> arg = default(_Closure_0024__32_002D0<T>);
		_Closure_0024__32_002D0<T> CS_0024_003C_003E8__locals0 = new _Closure_0024__32_002D0<T>(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Items = Items;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ItemBuilder = ItemBuilder;
		SetContentBuilder([SpecialName] (Panel Stack) =>
		{
			foreach (T _0024VB_0024Local_Item in CS_0024_003C_003E8__locals0._0024VB_0024Local_Items)
			{
				Stack.Children.Add(CS_0024_003C_003E8__locals0._0024VB_0024Local_ItemBuilder(_0024VB_0024Local_Item));
			}
			Stack.Children.Add(new FrameworkElement
			{
				Height = 18.0
			});
		});
	}

	public void BuildContent()
	{
		if (ContentBuilder != null && SwapControl is Panel panel)
		{
			try
			{
				ContentBuilder(panel);
				ContentBuilder = null;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				panel.Children.Clear();
				throw;
			}
			TriggerForceResize();
		}
	}

	private void MyCard_MouseEnter(object sender, MouseEventArgs e)
	{
		if (HasMouseAnimation)
		{
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			if (!Information.IsNothing(MainTextBlock))
			{
				list.Add(ModAnimation.AaColor(MainTextBlock, TextBlock.ForegroundProperty, "ColorBrush2", 90));
			}
			if (!Information.IsNothing(MainSwap))
			{
				list.Add(ModAnimation.AaColor(MainSwap, Shape.FillProperty, "ColorBrush2", 90));
			}
			list.AddRange(new ModAnimation.AniData[2]
			{
				ModAnimation.AaColor(MainChrome, MyDropShadow.ColorProperty, "ColorObject4", 90),
				ModAnimation.AaOpacity(MainChrome, 0.4 - MainChrome.Opacity, 90)
			});
			ModAnimation.AniStart(list, "MyCard Mouse " + Conversions.ToString(Uuid));
		}
	}

	private void MyCard_MouseLeave(object sender, MouseEventArgs e)
	{
		if (HasMouseAnimation)
		{
			List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
			if (!Information.IsNothing(MainTextBlock))
			{
				list.Add(ModAnimation.AaColor(MainTextBlock, TextBlock.ForegroundProperty, "ColorBrush1", 90));
			}
			if (!Information.IsNothing(MainSwap))
			{
				list.Add(ModAnimation.AaColor(MainSwap, Shape.FillProperty, "ColorBrush1", 90));
			}
			list.AddRange(new ModAnimation.AniData[2]
			{
				ModAnimation.AaColor(MainChrome, MyDropShadow.ColorProperty, "ColorObject1", 90),
				ModAnimation.AaOpacity(MainChrome, 0.07 - MainChrome.Opacity, 90)
			});
			ModAnimation.AniStart(list, "MyCard Mouse " + Conversions.ToString(Uuid));
		}
	}

	private void MySizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (UseAnimation)
		{
			double num = (IsSwapped ? 40.0 : e.NewSize.Height) - e.PreviousSize.Height;
			if (e.PreviousSize.Height != 0.0 && !IsHeightAnimating && !(Math.Abs(num) < 1.0) && base.ActualHeight != 0.0)
			{
				StartHeightAnimation(num, e.PreviousSize.Height, IsLoadAnimation: false);
			}
		}
	}

	private void StartHeightAnimation(double Delta, double PreviousHeight, bool IsLoadAnimation)
	{
		if (IsHeightAnimating || ModMain.FrmMain == null)
		{
			return;
		}
		List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
		double num = Math.Abs(Delta);
		checked
		{
			if (num <= 800.0)
			{
				list.Add(ModAnimation.AaHeight(this, Delta, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong)));
			}
			else
			{
				int num2 = default(int);
				int num3;
				int num4;
				if (Delta < 0.0 && num - (double)num2 > 500.0)
				{
					num2 = 200;
					num3 = 150;
					num4 = (int)Math.Round((num - (double)num2) / 0.1);
				}
				else if (Delta > 0.0 && num - (double)num2 > 3000.0)
				{
					num4 = 5000;
					num2 = (int)Math.Round(num - (double)num4 * 0.3);
					num3 = 400;
				}
				else
				{
					num2 = 150;
					num3 = 200;
					num4 = 4000;
				}
				list.Add(ModAnimation.AaHeight(this, (num - (double)num2) * (double)Math.Sign(Delta), (int)Math.Round((num - (double)num2) / (double)num4 * 1000.0)));
				list.Add(ModAnimation.AaHeight(this, num2 * Math.Sign(Delta), num3, 0, new ModAnimation.AniEaseOutFluentWithInitial(num4, (double)num3 / 1000.0, num2), After: true));
			}
			list.Add(ModAnimation.AaCode([SpecialName] () =>
			{
				IsHeightAnimating = false;
				base.Height = ActualUsedHeight;
				if (IsSwapped && SwapControl != null)
				{
					SwapControl.Visibility = Visibility.Collapsed;
				}
			}, 0, After: true));
			ModAnimation.AniStart(list, "MyCard Height " + Conversions.ToString(Uuid));
			IsHeightAnimating = true;
			ActualUsedHeight = (IsSwapped ? 40.0 : base.Height);
			base.Height = PreviousHeight;
		}
	}

	public void TriggerForceResize()
	{
		base.Height = (IsSwapped ? 40.0 : double.NaN);
		ModAnimation.AniStop("MyCard Height " + Conversions.ToString(Uuid));
		IsHeightAnimating = false;
	}

	private void MyCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		double y = Mouse.GetPosition(this).Y;
		if (IsSwapped || (!(y > (double)(IsSwapped ? 40 : 34)) && (y != 0.0 || base.IsMouseDirectlyOver)))
		{
			IsCustomMouseDown = true;
			if (IsSwapped || (SwapControl != null && !(y > (double)(IsSwapped ? 40 : 34)) && (y != 0.0 || base.IsMouseDirectlyOver)))
			{
				IsSwapMouseDown = true;
			}
		}
	}

	private void MyCard_MouseLeftButtonUp()
	{
		if (!IsCustomMouseDown)
		{
			return;
		}
		IsCustomMouseDown = false;
		ModMain.RaiseCustomEvent(this);
		if (!IsSwapMouseDown)
		{
			return;
		}
		IsSwapMouseDown = false;
		double y = Mouse.GetPosition(this).Y;
		if (IsSwapped || (SwapControl != null && !(y > (double)(IsSwapped ? 40 : 34)) && (y != 0.0 || base.IsMouseDirectlyOver)))
		{
			ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(RaiseByMouse: true);
			PreviewSwap?.Invoke(this, routeEventArgs);
			if (routeEventArgs.Handled)
			{
				IsSwapMouseDown = false;
				return;
			}
			IsSwapped = !IsSwapped;
			Logger.Info(string.Format("{0}{1}", IsSwapped ? "折叠卡片" : "展开卡片", (Title == null) ? "" : ("：" + Title)), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyCard.vb");
			Swap?.Invoke(this, routeEventArgs);
		}
	}

	private void MyCard_MouseLeave_Swap(object sender, MouseEventArgs e)
	{
		IsSwapMouseDown = false;
	}

	private void UpdateCursorStyle()
	{
		if (_MainSwap == null)
		{
			base.Cursor = null;
			return;
		}
		double y = Mouse.GetPosition(this).Y;
		base.Cursor = ((MainSwap.Visibility == Visibility.Visible && (IsSwapped || (y <= 34.0 && (y != 0.0 || base.IsMouseDirectlyOver)))) ? Cursors.Hand : null);
	}
}
