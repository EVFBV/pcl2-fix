using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class MyPageLeft : Grid
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__7_002D0
	{
		public FrameworkElement _0024VB_0024Local_Element;

		public MyPageLeft _0024VB_0024Me;

		public _Closure_0024__7_002D0(_Closure_0024__7_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Element = arg0._0024VB_0024Local_Element;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			((MyListItem)_0024VB_0024Local_Element).IsMouseOverAnimationEnabled = true;
			((MyListItem)_0024VB_0024Local_Element).RefreshColor(_0024VB_0024Me, new EventArgs());
		}
	}

	private int Uuid;

	public static readonly DependencyProperty AnimatedControlProperty = DependencyProperty.Register("AnimatedControl", typeof(string), typeof(MyPageLeft), new PropertyMetadata(null));

	public string AnimatedControl
	{
		get
		{
			return Conversions.ToString(GetValue(AnimatedControlProperty));
		}
		set
		{
			SetValue(AnimatedControlProperty, value);
		}
	}

	public MyPageLeft()
	{
		Uuid = ModBase.GetUuid();
	}

	public void TriggerShowAnimation()
	{
		if (AnimatedControl == null)
		{
			if (!(base.RenderTransform is ScaleTransform))
			{
				base.RenderTransform = new ScaleTransform(0.96, 0.96);
				base.RenderTransformOrigin = new Point(0.5, 0.5);
			}
			base.Opacity = 0.0;
			ModAnimation.AniStart(new ModAnimation.AniData[2]
			{
				ModAnimation.AaScaleTransform(this, 1.0 - ((ScaleTransform)base.RenderTransform).ScaleX, 400, 0, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaOpacity(this, 1.0, 100)
			}, "PageLeft PageChange " + Conversions.ToString(Uuid));
			return;
		}
		List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
		int num = 0;
		int num2 = 0;
		checked
		{
			using (List<FrameworkElement>.Enumerator enumerator = GetAllAnimControls(IgnoreInvisibility: true).GetEnumerator())
			{
				_Closure_0024__7_002D0 closure_0024__7_002D = default(_Closure_0024__7_002D0);
				while (enumerator.MoveNext())
				{
					closure_0024__7_002D = new _Closure_0024__7_002D0(closure_0024__7_002D);
					closure_0024__7_002D._0024VB_0024Me = this;
					closure_0024__7_002D._0024VB_0024Local_Element = enumerator.Current;
					closure_0024__7_002D._0024VB_0024Local_Element = MyVirtualizingElement.TryInit(closure_0024__7_002D._0024VB_0024Local_Element);
					if (closure_0024__7_002D._0024VB_0024Local_Element.Visibility == Visibility.Collapsed)
					{
						closure_0024__7_002D._0024VB_0024Local_Element.Opacity = 1.0;
						closure_0024__7_002D._0024VB_0024Local_Element.RenderTransform = new TranslateTransform(0.0, 0.0);
						if (closure_0024__7_002D._0024VB_0024Local_Element is MyListItem)
						{
							((MyListItem)closure_0024__7_002D._0024VB_0024Local_Element).IsMouseOverAnimationEnabled = true;
						}
						continue;
					}
					closure_0024__7_002D._0024VB_0024Local_Element.Opacity = 0.0;
					closure_0024__7_002D._0024VB_0024Local_Element.RenderTransform = new TranslateTransform(-25.0, 0.0);
					if (closure_0024__7_002D._0024VB_0024Local_Element is MyListItem)
					{
						((MyListItem)closure_0024__7_002D._0024VB_0024Local_Element).IsMouseOverAnimationEnabled = false;
					}
					list.Add(ModAnimation.AaOpacity(closure_0024__7_002D._0024VB_0024Local_Element, (closure_0024__7_002D._0024VB_0024Local_Element is TextBlock) ? 0.6 : 1.0, 100, num2, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)));
					list.Add(ModAnimation.AaTranslateX(closure_0024__7_002D._0024VB_0024Local_Element, 5.0, 200, num2, new ModAnimation.AniEaseOutFluent()));
					list.Add(ModAnimation.AaTranslateX(closure_0024__7_002D._0024VB_0024Local_Element, 20.0, 300, num2, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)));
					if (closure_0024__7_002D._0024VB_0024Local_Element is MyListItem)
					{
						list.Add(ModAnimation.AaCode(closure_0024__7_002D._Lambda_0024__0, num2 + 280));
					}
					num2 += Math.Max(15 - num, 7) * 2;
					num++;
				}
			}
			ModAnimation.AniStart(list, "PageLeft PageChange " + Conversions.ToString(Uuid));
		}
	}

	public void TriggerHideAnimation()
	{
		if (AnimatedControl == null)
		{
			if (!(base.RenderTransform is ScaleTransform))
			{
				base.RenderTransform = new ScaleTransform(1.0, 1.0);
				base.RenderTransformOrigin = new Point(0.5, 0.5);
			}
			ModAnimation.AniStart(new ModAnimation.AniData[2]
			{
				ModAnimation.AaScaleTransform(this, 0.95 - ((ScaleTransform)base.RenderTransform).ScaleX, 110, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaOpacity(this, 0.0 - base.Opacity, 80, 30)
			}, "PageLeft PageChange " + Conversions.ToString(Uuid));
			return;
		}
		List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
		int num = 0;
		List<FrameworkElement> allAnimControls = GetAllAnimControls();
		checked
		{
			foreach (FrameworkElement item in allAnimControls)
			{
				list.Add(ModAnimation.AaOpacity(item, 0.0 - item.Opacity, 50, (int)Math.Round(70.0 / (double)allAnimControls.Count * (double)num)));
				list.Add(ModAnimation.AaTranslateX(item, -6.0, 50, (int)Math.Round(70.0 / (double)allAnimControls.Count * (double)num)));
				num++;
			}
			ModAnimation.AniStart(list, "PageLeft PageChange " + Conversions.ToString(Uuid));
		}
	}

	private List<FrameworkElement> GetAllAnimControls(bool IgnoreInvisibility = false)
	{
		List<FrameworkElement> AllControls = new List<FrameworkElement>();
		GetAllAnimControls((FrameworkElement)FindName(AnimatedControl), ref AllControls, IgnoreInvisibility);
		return AllControls;
	}

	private void GetAllAnimControls(FrameworkElement Element, ref List<FrameworkElement> AllControls, bool IgnoreInvisibility)
	{
		if (!IgnoreInvisibility && Element.Visibility == Visibility.Collapsed)
		{
			return;
		}
		if (Element is MyTextButton)
		{
			AllControls.Add(Element);
			return;
		}
		if (Element is MyListItem)
		{
			AllControls.Add(Element);
			return;
		}
		if (Element is ContentControl)
		{
			GetAllAnimControls((FrameworkElement)((ContentControl)Element).Content, ref AllControls, IgnoreInvisibility);
			return;
		}
		if (Element is Panel)
		{
			foreach (FrameworkElement child in ((Panel)Element).Children)
			{
				GetAllAnimControls(child, ref AllControls, IgnoreInvisibility);
			}
			return;
		}
		AllControls.Add(Element);
	}
}
