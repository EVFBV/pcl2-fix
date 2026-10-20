using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class MyComboBoxItem : ComboBoxItem
{
	public int Uuid;

	private const int AnimationTimeIn = 100;

	private const int AnimationTimeOut = 300;

	private string BackColorName;

	private double FontOpacity;

	public MyComboBoxItem()
	{
		base.Unselected += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.MouseMove += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			RefreshColor();
		};
		base.MouseLeave += [SpecialName] (object a0, MouseEventArgs a1) =>
		{
			RefreshColor();
		};
		base.Selected += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.MouseLeftButtonUp += MyComboBoxItem_MouseLeftButtonUp;
		Uuid = ModBase.GetUuid();
		base.Style = (Style)FindResource("MyComboBoxItem");
	}

	private void RefreshColor()
	{
		string text;
		double num;
		int time;
		if (base.IsSelected)
		{
			text = "ColorBrush6";
			num = 1.0;
			time = 100;
		}
		else if (base.IsMouseOver)
		{
			text = "ColorBrush8";
			num = 1.0;
			time = 100;
		}
		else if (base.IsEnabled)
		{
			text = "ColorBrushTransparent";
			num = 1.0;
			time = 300;
		}
		else
		{
			text = "ColorBrushTransparent";
			num = 0.4;
			time = 300;
		}
		if (Operators.CompareString(BackColorName, text, TextCompare: false) != 0 || FontOpacity != num)
		{
			BackColorName = text;
			FontOpacity = num;
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaColor(this, Control.BackgroundProperty, BackColorName, time),
					ModAnimation.AaOpacity(this, FontOpacity - base.Opacity, time)
				}, "ComboBoxItem Color " + Conversions.ToString(Uuid));
			}
			else
			{
				ModAnimation.AniStop("ComboBoxItem Color " + Conversions.ToString(Uuid));
				SetResourceReference(Control.BackgroundProperty, BackColorName);
				base.Opacity = FontOpacity;
			}
		}
	}

	public override string ToString()
	{
		return base.Content.ToString();
	}

	public static implicit operator string(MyComboBoxItem Value)
	{
		return Value.Content.ToString();
	}

	private void MyComboBoxItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		Logger.Info($"选择下拉列表项：{ToString()}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyComboBoxItem.vb");
	}
}
