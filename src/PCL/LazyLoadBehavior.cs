using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PCL;

public class LazyLoadBehavior
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__4_002D0
	{
		public bool _0024VB_0024Local_Handled;

		public FrameworkElement _0024VB_0024Local_Element;

		public DependencyPropertyChangedEventArgs _0024VB_0024Local_e;

		public EventHandler _0024VB_0024Local_Handler;

		[SpecialName]
		internal void _Lambda_0024__R1(object a0, EventArgs a1)
		{
			_Lambda_0024__0();
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			if (!_0024VB_0024Local_Handled && !(_0024VB_0024Local_Element.RenderSize.Width < double.Epsilon) && _0024VB_0024Local_Element.IsVisible)
			{
				ScrollViewer scrollViewer = FindParentScrollViewer(_0024VB_0024Local_Element);
				if (scrollViewer != null && new Rect(0.0, 0.0, scrollViewer.ViewportWidth, scrollViewer.ViewportHeight).IntersectsWith(_0024VB_0024Local_Element.TransformToAncestor(scrollViewer).TransformBounds(new Rect(new Point(0.0, 0.0), _0024VB_0024Local_Element.RenderSize))))
				{
					_0024VB_0024Local_Handled = true;
					((Action)_0024VB_0024Local_e.NewValue)();
					_0024VB_0024Local_Element.LayoutUpdated -= _0024VB_0024Local_Handler;
				}
			}
		}
	}

	private static readonly DependencyProperty IsInViewportProperty = DependencyProperty.RegisterAttached("IsInViewport", typeof(Action), typeof(LazyLoadBehavior), new PropertyMetadata(null, OnIsInViewportChanged));

	public static void OnFirstEnterScrollViewerViewport(DependencyObject obj, Action value)
	{
		obj.SetValue(IsInViewportProperty, value);
	}

	private static void OnIsInViewportChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		_Closure_0024__4_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__4_002D0();
		CS_0024_003C_003E8__locals0._0024VB_0024Local_e = e;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Element = d as FrameworkElement;
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Element != null && CS_0024_003C_003E8__locals0._0024VB_0024Local_e.NewValue != null)
		{
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Handled = false;
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Handler = [SpecialName] (object a0, EventArgs a1) =>
			{
				CS_0024_003C_003E8__locals0._Lambda_0024__0();
			};
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Element.LayoutUpdated += CS_0024_003C_003E8__locals0._0024VB_0024Local_Handler;
		}
	}

	private static ScrollViewer FindParentScrollViewer(DependencyObject d)
	{
		while (d != null)
		{
			if (d is ScrollViewer)
			{
				return (ScrollViewer)d;
			}
			d = VisualTreeHelper.GetParent(d);
		}
		return null;
	}
}
