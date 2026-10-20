using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class MyScrollViewer : ScrollViewer
{
	private double RealOffset;

	public MyScrollBar ScrollBar;

	public double DeltaMult { get; set; }

	public MyScrollViewer()
	{
		base.PreviewMouseWheel += MyScrollViewer_PreviewMouseWheel;
		base.ScrollChanged += MyScrollViewer_ScrollChanged;
		base.IsVisibleChanged += MyScrollViewer_IsVisibleChanged;
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			Load();
		};
		base.PreviewGotKeyboardFocus += MyScrollViewer_PreviewGotKeyboardFocus;
		DeltaMult = 1.0;
	}

	private void MyScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
	{
		if (e.Delta != 0 && base.ActualHeight != 0.0 && base.ScrollableHeight != 0.0)
		{
			Type type = e.Source.GetType();
			if (NewLateBinding.LateGet(base.Content, null, "TemplatedParent", new object[0], null, null, null) != null || ((!typeof(ComboBox).IsAssignableFrom(type) || !((ComboBox)e.Source).IsDropDownOpen) && (!typeof(TextBox).IsAssignableFrom(type) || !((TextBox)e.Source).AcceptsReturn) && !typeof(ComboBoxItem).IsAssignableFrom(type) && (!(e.Source is Visual) || Operators.CompareString(PresentationSource.FromVisual((Visual)e.Source)?.RootVisual?.GetType().FullName, "System.Windows.Controls.Primitives.PopupRoot", TextCompare: false) != 0)))
			{
				e.Handled = true;
				PerformVerticalOffsetDelta(checked(-e.Delta));
			}
		}
	}

	public void PerformVerticalOffsetDelta(double Delta)
	{
		ModAnimation.AniStart(ModAnimation.AaDouble([SpecialName] (object a0) =>
		{
			_Lambda_0024__7_002D0(Conversions.ToDouble(a0));
		}, Delta * DeltaMult, 300, 0, new ModAnimation.AniEaseOutFluent((ModAnimation.AniEasePower)6)));
	}

	private void MyScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
	{
		RealOffset = base.VerticalOffset;
		if (ModMain.FrmMain != null && (e.VerticalChange != 0.0 || e.ViewportHeightChange != 0.0))
		{
			ModMain.FrmMain.BtnExtraBack.ShowRefresh();
		}
	}

	private void MyScrollViewer_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		ModMain.FrmMain.BtnExtraBack.ShowRefresh();
	}

	private void Load()
	{
		ScrollBar = (MyScrollBar)GetTemplateChild("PART_VerticalScrollBar");
	}

	private void MyScrollViewer_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (e.NewFocus != null && e.NewFocus is MySlider)
		{
			e.Handled = true;
		}
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__7_002D0(double AnimDelta)
	{
		RealOffset = MathUtils.Clamp<double>(RealOffset + AnimDelta, 0.0, base.ExtentHeight - base.ActualHeight);
		ScrollToVerticalOffset(RealOffset);
	}
}
