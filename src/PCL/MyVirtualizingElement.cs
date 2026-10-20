using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class MyVirtualizingElement<T> : FrameworkElement where T : FrameworkElement
{
	private Func<T> Initializer;

	public MyVirtualizingElement(Func<T> Initializer)
	{
		this.Initializer = Initializer;
		LazyLoadBehavior.OnFirstEnterScrollViewerViewport(this, [SpecialName] () =>
		{
			Init();
		});
	}

	public T Init()
	{
		T val = Initializer();
		if (base.Parent != null)
		{
			if (!(base.Parent is Panel))
			{
				throw new Exception("MyVirtualizingElement 的父级必须是一个 Panel");
			}
			Panel obj = (Panel)base.Parent;
			int index = obj.Children.IndexOf(this);
			obj.Children.RemoveAt(index);
			obj.Children.Insert(index, val);
		}
		return val;
	}

	public static implicit operator T(MyVirtualizingElement<T> Virtualized)
	{
		return Virtualized.Init();
	}
}
public class MyVirtualizingElement : FrameworkElement
{
	private Func<FrameworkElement> Initializer;

	public MyVirtualizingElement(Func<FrameworkElement> Initializer)
	{
		this.Initializer = Initializer;
		LazyLoadBehavior.OnFirstEnterScrollViewerViewport(this, [SpecialName] () =>
		{
			Init();
		});
	}

	public FrameworkElement Init()
	{
		FrameworkElement frameworkElement = Initializer();
		if (base.Parent != null)
		{
			if (!(base.Parent is Panel))
			{
				throw new Exception("MyVirtualizingElement 的父级必须是一个 Panel");
			}
			Panel obj = (Panel)base.Parent;
			int index = obj.Children.IndexOf(this);
			obj.Children.RemoveAt(index);
			obj.Children.Insert(index, frameworkElement);
		}
		return frameworkElement;
	}

	public static FrameworkElement TryInit(FrameworkElement Element)
	{
		if (GeneralUtils.IsGenericInstanceOf((object)Element, typeof(MyVirtualizingElement<>)))
		{
			return (FrameworkElement)NewLateBinding.LateGet(Element, null, "Init", new object[0], null, null, null);
		}
		if (Element is MyVirtualizingElement)
		{
			return ((MyVirtualizingElement)Element).Init();
		}
		return Element;
	}
}
