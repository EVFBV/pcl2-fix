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
using System.Windows.Shapes;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[ContentProperty("Inlines")]
[DesignerGenerated]
public class MyRadioBox : Grid, IMyRadio, ISettingControl, IComponentConnector
{
	public delegate void PreviewCheckEventHandler(object sender, ModBase.RouteEventArgs e);

	public delegate void PreviewChangeEventHandler(object sender, ModBase.RouteEventArgs e);

	public int Uuid;

	public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register("Checked", typeof(bool), typeof(MyRadioBox), new PropertyMetadata(false, [SpecialName] (DependencyObject a0, DependencyPropertyChangedEventArgs a1) =>
	{
		((VB_0024AnonymousDelegate_6<MyRadioBox, DependencyPropertyChangedEventArgs>)([SpecialName] (MyRadioBox d, DependencyPropertyChangedEventArgs e) =>
		{
			if (!d.IsLoaded)
			{
				d.SyncUI();
			}
		}))((MyRadioBox)a0, a1);
	}));

	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyRadioBox), new PropertyMetadata([SpecialName] (DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
	{
		if (sender != null)
		{
			((MyRadioBox)sender).LabText.Text = Conversions.ToString(e.NewValue);
		}
	}));

	private bool MouseDowned;

	private bool AllowMouseDown;

	private const int AnimationTimeOfMouseIn = 100;

	private const int AnimationTimeOfMouseOut = 200;

	private const int AnimationTimeOfCheck = 150;

	private bool _contentLoaded;

	public bool Checked
	{
		get
		{
			return Conversions.ToBoolean(GetValue(CheckedProperty));
		}
		set
		{
			SetChecked(value, user: false);
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

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyRadioBox PanBack
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

	[field: AccessedThroughProperty("ShapeBorder")]
	internal virtual Ellipse ShapeBorder
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

	public event PreviewCheckEventHandler PreviewCheck;

	public event PreviewChangeEventHandler PreviewChange;

	public event IMyRadio.CheckEventHandler Check;

	public event IMyRadio.ChangedEventHandler Changed;

	public MyRadioBox()
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
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			Radiobox_IsEnabledChanged();
		};
		base.MouseEnter += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			Radiobox_MouseEnterAnimation();
		};
		base.MouseLeave += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			Radiobox_MouseLeaveAnimation();
		};
		Check += [SpecialName] (object a0, ModBase.RouteEventArgs a1) =>
		{
			SaveSetting();
		};
		Uuid = ModBase.GetUuid();
		MouseDowned = false;
		AllowMouseDown = true;
		InitializeComponent();
	}

	public void SetChecked(bool value, bool user)
	{
		try
		{
			if (value && user)
			{
				ModBase.RouteEventArgs routeEventArgs = new ModBase.RouteEventArgs(user);
				PreviewCheck?.Invoke(this, routeEventArgs);
				if (routeEventArgs.Handled)
				{
					Radiobox_MouseLeave();
					return;
				}
			}
			bool flag = false;
			if (base.IsLoaded && value != Checked)
			{
				PreviewChange?.Invoke(this, new ModBase.RouteEventArgs(user));
			}
			if (value != Checked)
			{
				SetValue(CheckedProperty, value);
				flag = true;
			}
			if (base.Parent == null)
			{
				return;
			}
			List<MyRadioBox> list = new List<MyRadioBox>();
			int num = 0;
			foreach (object item in (IEnumerable)NewLateBinding.LateGet(base.Parent, null, "Children", new object[0], null, null, null))
			{
				object objectValue = RuntimeHelpers.GetObjectValue(item);
				if (objectValue is MyRadioBox)
				{
					list.Add((MyRadioBox)objectValue);
					if (Conversions.ToBoolean(NewLateBinding.LateGet(objectValue, null, "Checked", new object[0], null, null, null)))
					{
						num = checked(num + 1);
					}
				}
			}
			if (num > 1)
			{
				if (Checked)
				{
					foreach (MyRadioBox item2 in list)
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
					foreach (MyRadioBox item3 in list)
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
				if (Checked)
				{
					Check?.Invoke(this, new ModBase.RouteEventArgs(user));
				}
				Changed?.Invoke(this, new ModBase.RouteEventArgs(user));
				ModMain.RaiseCustomEvent(this);
				SyncUI();
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "单选框勾选改变错误", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyRadioBox.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void SyncUI()
	{
		if (ModAnimation.AniControlEnabled == 0 && base.IsLoaded)
		{
			if (Checked)
			{
				if (ShapeDot.Opacity < 0.01)
				{
					ShapeDot.Opacity = 1.0;
				}
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaScale(ShapeBorder, 10.0 - ShapeBorder.Width, 150, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak), After: false, Absolute: true),
					ModAnimation.AaScale(ShapeBorder, 8.0, 300, 90, new ModAnimation.AniEaseOutBack(), After: false, Absolute: true)
				}, "MyRadioBox Border " + Conversions.ToString(Uuid));
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaScale(ShapeDot, 9.0 - ShapeDot.Width, 390, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak), After: false, Absolute: true),
					ModAnimation.AaOpacity(ShapeDot, 1.0 - ShapeDot.Opacity, 75, 90)
				}, "MyRadioBox Dot " + Conversions.ToString(Uuid));
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(ShapeBorder, Shape.StrokeProperty, base.IsMouseOver ? "ColorBrush3" : (base.IsEnabled ? "ColorBrush2" : "ColorBrushGray5"), 150) }, "MyRadioBox BorderColor " + Conversions.ToString(Uuid));
			}
			else
			{
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaScale(ShapeBorder, 18.0 - ShapeBorder.Width, 150, 0, new ModAnimation.AniEaseOutFluent(), After: false, Absolute: true) }, "MyRadioBox Border " + Conversions.ToString(Uuid));
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaScale(ShapeDot, 0.0 - ShapeDot.Width, 150, 0, new ModAnimation.AniEaseInFluent(), After: false, Absolute: true),
					ModAnimation.AaOpacity(ShapeDot, 0.0 - ShapeDot.Opacity, 75, 30)
				}, "MyRadioBox Dot " + Conversions.ToString(Uuid));
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(ShapeBorder, Shape.StrokeProperty, base.IsMouseOver ? "ColorBrush3" : (base.IsEnabled ? "ColorBrush1" : "ColorBrushGray5"), 150) }, "MyRadioBox BorderColor " + Conversions.ToString(Uuid));
			}
		}
		else
		{
			ModAnimation.AniStop("MyRadioBox Border " + Conversions.ToString(Uuid));
			ModAnimation.AniStop("MyRadioBox Dot " + Conversions.ToString(Uuid));
			ModAnimation.AniStop("MyRadioBox BorderColor " + Conversions.ToString(Uuid));
			if (Checked)
			{
				ShapeDot.Width = 9.0;
				ShapeDot.Height = 9.0;
				ShapeDot.Opacity = 1.0;
				ShapeDot.Margin = new Thickness(5.5, 0.0, 0.0, 0.0);
				ShapeBorder.SetResourceReference(Shape.StrokeProperty, base.IsEnabled ? "ColorBrush2" : "ColorBrushGray5");
			}
			else
			{
				ShapeDot.Width = 0.0;
				ShapeDot.Height = 0.0;
				ShapeDot.Opacity = 0.0;
				ShapeDot.Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
				ShapeBorder.SetResourceReference(Shape.StrokeProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
			}
		}
	}

	private void Radiobox_MouseUp()
	{
		if (MouseDowned)
		{
			Logger.Info($"按下单选框：{Text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyRadioBox.xaml.vb");
			SetChecked(value: true, user: true);
			MouseDowned = false;
			ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.FillProperty, "ColorBrushHalfWhite", 100), "MyRadioBox Background " + Conversions.ToString(Uuid));
		}
	}

	private void Radiobox_MouseDown()
	{
		MouseDowned = true;
		Focus();
		ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.FillProperty, "ColorBrushBg1", 100), "MyRadioBox Background " + Conversions.ToString(Uuid));
		if (!Checked)
		{
			ModAnimation.AniStart(ModAnimation.AaScale(ShapeBorder, 16.5 - ShapeBorder.Width, 1000, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), After: false, Absolute: true), "MyRadioBox Border " + Conversions.ToString(Uuid));
		}
	}

	private void Radiobox_MouseLeave()
	{
		if (MouseDowned)
		{
			MouseDowned = false;
			ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.FillProperty, "ColorBrushHalfWhite", 100), "MyRadioBox Background " + Conversions.ToString(Uuid));
			if (!Checked)
			{
				ModAnimation.AniStart(ModAnimation.AaScale(ShapeBorder, 18.0 - ShapeBorder.Width, 400, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Strong), After: false, Absolute: true), "MyRadioBox Border " + Conversions.ToString(Uuid));
			}
		}
	}

	private void Radiobox_IsEnabledChanged()
	{
		if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
		{
			if (base.IsEnabled)
			{
				Radiobox_MouseLeaveAnimation();
				return;
			}
			ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.StrokeProperty, ModSecret.ColorGray5 - ShapeBorder.Stroke, 200), "MyRadioBox BorderColor " + Conversions.ToString(Uuid));
			ModAnimation.AniStart(ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, ModSecret.ColorGray5 - LabText.Foreground, 200), "MyRadioBox TextColor " + Conversions.ToString(Uuid));
		}
		else
		{
			ModAnimation.AniStop("MyRadioBox BorderColor " + Conversions.ToString(Uuid));
			ModAnimation.AniStop("MyRadioBox TextColor " + Conversions.ToString(Uuid));
			LabText.SetResourceReference(TextBlock.ForegroundProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
			ShapeBorder.SetResourceReference(Shape.StrokeProperty, (!base.IsEnabled) ? "ColorBrushGray5" : (Checked ? "ColorBrush2" : "ColorBrush1"));
		}
	}

	private void Radiobox_MouseEnterAnimation()
	{
		ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.StrokeProperty, "ColorBrush3", 100), "MyRadioBox BorderColor " + Conversions.ToString(Uuid));
		ModAnimation.AniStart(ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, "ColorBrush3", 100), "MyRadioBox TextColor " + Conversions.ToString(Uuid));
	}

	private void Radiobox_MouseLeaveAnimation()
	{
		if (base.IsEnabled)
		{
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				ModAnimation.AniStart(ModAnimation.AaColor(ShapeBorder, Shape.StrokeProperty, (!base.IsEnabled) ? "ColorBrushGray5" : (Checked ? "ColorBrush2" : "ColorBrush1"), 200), "MyRadioBox BorderColor " + Conversions.ToString(Uuid));
				ModAnimation.AniStart(ModAnimation.AaColor(LabText, TextBlock.ForegroundProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray5", 200), "MyRadioBox TextColor " + Conversions.ToString(Uuid));
				return;
			}
			ModAnimation.AniStop("MyRadioBox BorderColor " + Conversions.ToString(Uuid));
			ModAnimation.AniStop("MyRadioBox TextColor " + Conversions.ToString(Uuid));
			ShapeBorder.SetResourceReference(Shape.StrokeProperty, (!base.IsEnabled) ? "ColorBrushGray5" : (Checked ? "ColorBrush2" : "ColorBrush1"));
			LabText.SetResourceReference(TextBlock.ForegroundProperty, base.IsEnabled ? "ColorBrush1" : "ColorBrushGray5");
		}
	}

	private void RefreshSetting(string NewValue)
	{
		Checked = Operators.CompareString(NewValue, SettingService.GetValue(this), TextCompare: false) == 0;
	}

	void ISettingControl.RefreshSetting(string NewValue)
	{
		//ILSpy generated this explicit interface implementation from .override directive in RefreshSetting
		this.RefreshSetting(NewValue);
	}

	private string GetCurrentSetting()
	{
		if (!Checked)
		{
			return null;
		}
		return SettingService.GetValue(this);
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
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/myradiobox.xaml", UriKind.Relative);
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
			PanBack = (MyRadioBox)target;
			break;
		case 2:
			LabText = (TextBlock)target;
			break;
		case 3:
			ShapeBorder = (Ellipse)target;
			break;
		case 4:
			ShapeDot = (Ellipse)target;
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
