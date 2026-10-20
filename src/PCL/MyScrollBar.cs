using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class MyScrollBar : ScrollBar
{
	public int Uuid;

	public MyScrollBar()
	{
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.GotMouseCapture += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			RefreshColor();
		};
		base.LostMouseCapture += [SpecialName] (object a0, MouseEventArgs a1) =>
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
		base.IsVisibleChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			RefreshColor();
		};
		Uuid = ModBase.GetUuid();
	}

	private void RefreshColor()
	{
		try
		{
			double num;
			int time;
			string text;
			if (!base.IsVisible)
			{
				num = 0.0;
				time = 20;
				text = "ColorBrush4";
			}
			else if (base.IsMouseCaptureWithin)
			{
				num = 1.0;
				text = "ColorBrush4";
				time = 50;
			}
			else if (base.IsMouseOver)
			{
				num = 0.9;
				text = "ColorBrush3";
				time = 130;
			}
			else
			{
				num = 0.5;
				text = "ColorBrush4";
				time = 180;
			}
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaColor(this, Control.ForegroundProperty, text, time),
					ModAnimation.AaOpacity(this, num - base.Opacity, time)
				}, "MyScrollBar Color " + Conversions.ToString(Uuid));
			}
			else
			{
				ModAnimation.AniStop("MyScrollBar Color " + Conversions.ToString(Uuid));
				SetResourceReference(Control.ForegroundProperty, text);
				base.Opacity = num;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "滚动条颜色改变出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyScrollBar.vb");
			ProjectData.ClearProjectError();
		}
	}
}
