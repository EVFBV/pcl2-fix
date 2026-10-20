using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class MyTextButton : Label
{
	public delegate void ClickEventHandler(object sender, EventArgs e);

	public int Uuid;

	public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyTextButton), new PropertyMetadata("", [SpecialName] (DependencyObject a0, DependencyPropertyChangedEventArgs a1) =>
	{
		((VB_0024AnonymousDelegate_8<MyTextButton, DependencyPropertyChangedEventArgs>)([SpecialName] (MyTextButton sender, DependencyPropertyChangedEventArgs e) =>
		{
			if (!Operators.ConditionalCompareObjectEqual(e.OldValue, e.NewValue, TextCompare: false))
			{
				ModAnimation.AniStart(new ModAnimation.AniData[3]
				{
					ModAnimation.AaOpacity(sender, 0.0 - sender.Opacity, 50),
					ModAnimation.AaCode([SpecialName] () =>
					{
						sender.Content = RuntimeHelpers.GetObjectValue(e.NewValue);
					}, 0, After: true),
					ModAnimation.AaOpacity(sender, 1.0, 170)
				}, "MyTextButton Text " + Conversions.ToString(sender.Uuid));
			}
		}))((MyTextButton)a0, a1);
	}));

	public bool IsMouseDown;

	private const int AnimationTimeIn = 100;

	private const int AnimationTimeOut = 200;

	private string ColorName;

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

	public event ClickEventHandler Click;

	public MyTextButton()
	{
		base.PreviewMouseLeftButtonDown += MyTextButton_MouseLeftButtonDown;
		base.MouseLeave += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			MyTextButton_MouseLeave();
		};
		base.PreviewMouseLeftButtonUp += MyTextButton_MouseLeftButtonUp;
		base.MouseEnter += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			RefreshColor();
		};
		base.MouseLeave += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			RefreshColor();
		};
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.MouseLeftButtonDown += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
		{
			RefreshColor();
		};
		base.MouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
		{
			RefreshColor();
		};
		Uuid = ModBase.GetUuid();
		IsMouseDown = false;
		SetResourceReference(Control.ForegroundProperty, "ColorBrush1");
		base.Background = ModSecret.ColorSemiTransparent;
	}

	private void MyTextButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		IsMouseDown = true;
		e.Handled = true;
	}

	private void MyTextButton_MouseLeave()
	{
		IsMouseDown = false;
	}

	private void MyTextButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		if (IsMouseDown)
		{
			IsMouseDown = false;
			Logger.Info($"按下文本按钮：{Text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyTextButton.vb");
			Click?.Invoke(this, null);
			ModMain.RaiseCustomEvent(this);
			e.Handled = true;
		}
	}

	private void RefreshColor()
	{
		string text;
		int time;
		if (IsMouseDown)
		{
			text = "ColorBrush4";
			time = 30;
		}
		else if (base.IsMouseOver)
		{
			text = "ColorBrush3";
			time = 100;
		}
		else
		{
			text = "ColorBrush1";
			time = 200;
		}
		if (Operators.CompareString(ColorName, text, TextCompare: false) != 0)
		{
			ColorName = text;
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				ModAnimation.AniStart(ModAnimation.AaColor(this, Control.ForegroundProperty, text, time), "MyTextButton Color " + Conversions.ToString(Uuid));
				return;
			}
			ModAnimation.AniStop("MyTextButton Color " + Conversions.ToString(Uuid));
			SetResourceReference(Control.ForegroundProperty, text);
		}
	}
}
