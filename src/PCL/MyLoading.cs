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
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class MyLoading : Grid, IComponentConnector
{
	public delegate void IsErrorChangedEventHandler(object sender, bool isError);

	public delegate void StateChangedEventHandler(object sender, MyLoadingState newState, MyLoadingState oldState);

	public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

	public enum MyLoadingState
	{
		Unloaded = -1,
		Run,
		Stop,
		Error
	}

	private int Uuid;

	public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(MyLoading));

	private string _Text;

	private string _TextError;

	[CompilerGenerated]
	[AccessedThroughProperty("_State")]
	private ILoadingTrigger __State;

	private bool IsLooping;

	private bool ErrorAnimationWaiting;

	private bool IsMouseDown;

	private bool _contentLoaded;

	public bool AutoRun { get; set; }

	public SolidColorBrush Foreground
	{
		get
		{
			return (SolidColorBrush)GetValue(ForegroundProperty);
		}
		set
		{
			SetValue(ForegroundProperty, value);
		}
	}

	private bool _ShowProgress { get; set; }

	public bool ShowProgress
	{
		get
		{
			return _ShowProgress;
		}
		set
		{
			if (_ShowProgress != value)
			{
				_ShowProgress = value;
				RefreshText();
			}
		}
	}

	public string Text
	{
		get
		{
			return _Text;
		}
		set
		{
			_Text = value;
			RefreshText();
		}
	}

	public string TextError
	{
		get
		{
			return _TextError;
		}
		set
		{
			_TextError = value;
			RefreshText();
		}
	}

	public bool TextErrorInherit { get; set; }

	private virtual ILoadingTrigger _State
	{
		[CompilerGenerated]
		get
		{
			return __State;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			ILoadingTrigger.ProgressChangedEventHandler obj = [SpecialName] (double a0, double a1) =>
			{
				RefreshText();
			};
			ILoadingTrigger.LoadingStateChangedEventHandler obj2 = [SpecialName] (MyLoadingState a0, MyLoadingState a1) =>
			{
				RefreshState();
			};
			ILoadingTrigger loadingTrigger = __State;
			if (loadingTrigger != null)
			{
				loadingTrigger.ProgressChanged -= obj;
				loadingTrigger.LoadingStateChanged -= obj2;
			}
			__State = value;
			loadingTrigger = __State;
			if (loadingTrigger != null)
			{
				loadingTrigger.ProgressChanged += obj;
				loadingTrigger.LoadingStateChanged += obj2;
			}
		}
	}

	public ILoadingTrigger State
	{
		get
		{
			InitState();
			return _State;
		}
		set
		{
			_State = value;
			RefreshState();
		}
	}

	private MyLoadingState _OuterState { get; set; }

	private MyLoadingState OuterState
	{
		get
		{
			return _OuterState;
		}
		set
		{
			if (_OuterState != value)
			{
				MyLoadingState outerState = _OuterState;
				_OuterState = value;
				StateChanged?.Invoke(this, value, outerState);
				if (outerState == MyLoadingState.Error != (value == MyLoadingState.Error))
				{
					IsErrorChanged?.Invoke(this, value == MyLoadingState.Error);
				}
			}
		}
	}

	private MyLoadingState _InnerState { get; set; }

	private MyLoadingState InnerState
	{
		get
		{
			return _InnerState;
		}
		set
		{
			if (_InnerState != value)
			{
				MyLoadingState innerState = _InnerState;
				_InnerState = value;
				AniLoop();
				if (innerState == MyLoadingState.Error != (value == MyLoadingState.Error))
				{
					ErrorAnimation(this, value == MyLoadingState.Error);
				}
			}
		}
	}

	public bool HasAnimation { get; set; }

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyLoading PanBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PathPickaxe")]
	internal virtual Path PathPickaxe
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PathLeft")]
	internal virtual Path PathLeft
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PathRight")]
	internal virtual Path PathRight
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PathError")]
	internal virtual Path PathError
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

	public event IsErrorChangedEventHandler IsErrorChanged;

	public event StateChangedEventHandler StateChanged;

	public event ClickEventHandler Click;

	public MyLoading()
	{
		IsErrorChanged += [SpecialName] (object a0, bool a1) =>
		{
			RefreshText();
		};
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshText();
		};
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			InitState();
		};
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshState();
		};
		base.Unloaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshState();
		};
		base.MouseLeftButtonUp += Button_MouseUp;
		base.MouseLeftButtonDown += Button_MouseDown;
		base.MouseLeave += Button_MouseLeave;
		base.MouseLeftButtonUp += Button_MouseLeave;
		AutoRun = true;
		Uuid = ModBase.GetUuid();
		_ShowProgress = false;
		_Text = "加载中";
		_TextError = "加载失败";
		TextErrorInherit = true;
		_OuterState = MyLoadingState.Unloaded;
		_InnerState = MyLoadingState.Unloaded;
		HasAnimation = true;
		IsLooping = false;
		ErrorAnimationWaiting = false;
		IsMouseDown = false;
		InitializeComponent();
		SetResourceReference(ForegroundProperty, "ColorBrush3");
	}

	private void RefreshText()
	{
		ModBase.RunInUi([SpecialName] () =>
		{
			if (InnerState == MyLoadingState.Error)
			{
				if (TextErrorInherit && State.IsLoader)
				{
					Exception ex = (Exception)NewLateBinding.LateGet(State, null, "Error", new object[0], null, null, null);
					if (ex == null)
					{
						LabText.Text = "未知错误";
					}
					else
					{
						LabText.Text = ExceptionExtensions.GetDisplay(ex, false);
					}
				}
				else
				{
					LabText.Text = TextError;
				}
			}
			else if (ShowProgress && State.IsLoader)
			{
				LabText.Text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Text + " - ", NewLateBinding.LateGet(null, typeof(Math), "Floor", new object[1] { Operators.MultiplyObject(NewLateBinding.LateGet(State, null, "Progress", new object[0], null, null, null), 100) }, null, null, null)), "%"));
			}
			else
			{
				LabText.Text = Text;
			}
		});
	}

	private void InitState()
	{
		if (_State == null)
		{
			_State = new MyLoadingStateSimulator();
			if (AutoRun)
			{
				_State.LoadingState = MyLoadingState.Run;
			}
		}
	}

	private void RefreshState()
	{
		if (_State.LoadingState == MyLoadingState.Run && !base.IsLoaded)
		{
			InnerState = MyLoadingState.Stop;
		}
		InnerState = _State.LoadingState;
		OuterState = _State.LoadingState;
		AniLoop();
	}

	private void AniLoop()
	{
		if (HasAnimation && !IsLooping && InnerState == MyLoadingState.Run && !(ModAnimation.AniSpeed > 10.0) && base.IsLoaded)
		{
			IsLooping = true;
			ErrorAnimationWaiting = true;
			ModAnimation.AniStart(new ModAnimation.AniData[11]
			{
				ModAnimation.AaRotateTransform(PathPickaxe, -20.0 - ((RotateTransform)PathPickaxe.RenderTransform).Angle, 350, 250, new ModAnimation.AniEaseInBack(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaRotateTransform(PathPickaxe, 50.0, 900, 0, new ModAnimation.AniEaseOutFluent(), After: true),
				ModAnimation.AaRotateTransform(PathPickaxe, 25.0, 900, 0, new ModAnimation.AniEaseOutElastic(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PathLeft.Opacity = 1.0;
					PathLeft.Margin = new Thickness(7.0, 41.0, 0.0, 0.0);
					PathRight.Opacity = 1.0;
					PathRight.Margin = new Thickness(14.0, 41.0, 0.0, 0.0);
					ErrorAnimationWaiting = false;
				}),
				ModAnimation.AaOpacity(PathLeft, -1.0, 100, 50),
				ModAnimation.AaX(PathLeft, -5.0, 180, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaY(PathLeft, -6.0, 180, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaOpacity(PathRight, -1.0, 100, 50),
				ModAnimation.AaX(PathRight, 5.0, 180, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaY(PathRight, -6.0, 180, 0, new ModAnimation.AniEaseOutFluent()),
				ModAnimation.AaCode([SpecialName] () =>
				{
					IsLooping = false;
					AniLoop();
				}, 0, After: true)
			}, "MyLoader Loop " + Conversions.ToString(Uuid) + "/" + Conversions.ToString(ModBase.GetUuid()));
		}
	}

	private void ErrorAnimation(object sender, bool isError)
	{
		if (isError)
		{
			int num = (ErrorAnimationWaiting ? 400 : 0);
			ModAnimation.AniStart(checked(new ModAnimation.AniData[3]
			{
				ModAnimation.AaColor(PanBack, ForegroundProperty, "ColorBrushRedLight", 300),
				ModAnimation.AaOpacity(PathError, 1.0 - PathError.Opacity, 100, 300 + num),
				ModAnimation.AaScaleTransform(PathError, 1.0 - ((ScaleTransform)PathError.RenderTransform).ScaleX, 400, 300 + num, new ModAnimation.AniEaseOutBack())
			}), "MyLoader Error " + Conversions.ToString(Uuid));
		}
		else
		{
			ModAnimation.AniStart(new ModAnimation.AniData[3]
			{
				ModAnimation.AaOpacity(PathError, 0.0 - PathError.Opacity, 100),
				ModAnimation.AaScaleTransform(PathError, 0.5 - ((ScaleTransform)PathError.RenderTransform).ScaleX, 200),
				ModAnimation.AaColor(PanBack, ForegroundProperty, "ColorBrush3", 300)
			}, "MyLoader Error " + Conversions.ToString(Uuid));
		}
	}

	private void Button_MouseUp(object sender, MouseButtonEventArgs e)
	{
		Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
	}

	private void Button_MouseDown(object sender, MouseButtonEventArgs e)
	{
		IsMouseDown = true;
	}

	private void Button_MouseLeave(object sender, object e)
	{
		IsMouseDown = false;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myloading.xaml", UriKind.Relative);
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
			PanBack = (MyLoading)target;
			break;
		case 2:
			PathPickaxe = (Path)target;
			break;
		case 3:
			PathLeft = (Path)target;
			break;
		case 4:
			PathRight = (Path)target;
			break;
		case 5:
			PathError = (Path)target;
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
