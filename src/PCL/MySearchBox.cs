using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class MySearchBox : MyCard, IComponentConnector
{
	public delegate void TextChangedEventHandler(object sender, EventArgs e);

	[CompilerGenerated]
	[AccessedThroughProperty("TextBox")]
	private MyTextBox _TextBox;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnClear")]
	private MyIconButton _BtnClear;

	private bool _contentLoaded;

	public string HintText
	{
		get
		{
			return TextBox.HintText;
		}
		set
		{
			TextBox.HintText = value;
		}
	}

	public string Text
	{
		get
		{
			return TextBox.Text;
		}
		set
		{
			TextBox.Text = value;
		}
	}

	internal virtual MyTextBox TextBox
	{
		[CompilerGenerated]
		get
		{
			return _TextBox;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			System.Windows.Controls.TextChangedEventHandler value2 = Text_TextChanged;
			MyTextBox textBox = _TextBox;
			if (textBox != null)
			{
				textBox.TextChanged -= value2;
			}
			_TextBox = value;
			textBox = _TextBox;
			if (textBox != null)
			{
				textBox.TextChanged += value2;
			}
		}
	}

	internal virtual MyIconButton BtnClear
	{
		[CompilerGenerated]
		get
		{
			return _BtnClear;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = BtnClear_Click;
			MyIconButton btnClear = _BtnClear;
			if (btnClear != null)
			{
				btnClear.Click -= obj;
			}
			_BtnClear = value;
			btnClear = _BtnClear;
			if (btnClear != null)
			{
				btnClear.Click += obj;
			}
		}
	}

	public event TextChangedEventHandler TextChanged;

	public MySearchBox()
	{
		base.Loaded += MySearchBox_Loaded;
		base.KeyUp += MySearchBox_KeyUp;
		InitializeComponent();
	}

	private void MySearchBox_Loaded(object sender, RoutedEventArgs e)
	{
		TextBox.Focus();
	}

	private void Text_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (string.IsNullOrEmpty(TextBox.Text))
		{
			ModAnimation.AniStart(ModAnimation.AaOpacity(BtnClear, 0.0 - BtnClear.Opacity, 90), "MySearchBox ClearBtn " + Conversions.ToString(Uuid));
			BtnClear.IsHitTestVisible = false;
		}
		else
		{
			ModAnimation.AniStart(ModAnimation.AaOpacity(BtnClear, 1.0 - BtnClear.Opacity, 90), "MySearchBox ClearBtn " + Conversions.ToString(Uuid));
			BtnClear.IsHitTestVisible = true;
		}
		TextChanged?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
	}

	private void BtnClear_Click(object sender, EventArgs e)
	{
		TextBox.Text = "";
		TextBox.Focus();
	}

	private void MySearchBox_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return)
		{
			ModMain.RaiseCustomEvent(this);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/controls/mysearchbox.xaml", UriKind.Relative);
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
	internal Delegate _CreateDelegate(Type delegateType, string handler)
	{
		return Delegate.CreateDelegate(delegateType, this, handler);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			TextBox = (MyTextBox)target;
			break;
		case 2:
			BtnClear = (MyIconButton)target;
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
