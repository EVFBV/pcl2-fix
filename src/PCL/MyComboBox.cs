using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeloongCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class MyComboBox : ComboBox, ISettingControl
{
	public delegate void TextChangedEventHandler(object sender, TextChangedEventArgs e);

	public int Uuid;

	private MyTextBox TextBox;

	private string _Text;

	private bool IsMouseDown;

	private double RealWidth;

	private bool IsTextChanging;

	public string HintText { get; set; }

	public new string Text
	{
		get
		{
			if (base.IsEditable)
			{
				if (TextBox == null)
				{
					return _Text ?? "";
				}
				return TextBox.Text ?? "";
			}
			return (base.SelectedItem ?? "").ToString();
		}
		set
		{
			if (base.IsEditable)
			{
				if (Information.IsNothing(TextBox))
				{
					_Text = value;
				}
				else
				{
					TextBox.Text = value;
				}
				return;
			}
			throw new NotSupportedException("该 ComboBox 不支持修改文本。");
		}
	}

	public ContentPresenter ContentPresenter => (ContentPresenter)base.Template.FindName("PART_Content", this);

	public event TextChangedEventHandler TextChanged;

	public MyComboBox()
	{
		base.PreviewMouseLeftButtonDown += MyComboBox_PreviewMouseLeftButtonDown;
		base.PreviewMouseLeftButtonUp += MyComboBox_PreviewMouseLeftButtonUp;
		base.MouseLeave += MyComboBox_PreviewMouseLeftButtonUp;
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
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
		base.PreviewMouseLeftButtonDown += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
		{
			RefreshColor();
		};
		base.PreviewMouseLeftButtonUp += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
		{
			RefreshColor();
		};
		base.GotKeyboardFocus += [SpecialName] (object a0, KeyboardFocusChangedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.DropDownOpened += MyComboBox_DropDownOpened;
		base.DropDownClosed += MyComboBox_DropDownClosed;
		TextChanged += MyComboBox_TextChanged;
		base.SelectionChanged += MyComboBox_SelectionChanged;
		base.SelectionChanged += [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
		{
			SaveSetting();
		};
		Uuid = ModBase.GetUuid();
		HintText = "";
		_Text = Conversions.ToString(base.SelectedItem);
		IsMouseDown = false;
		IsTextChanging = false;
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		if (!base.IsEditable)
		{
			return;
		}
		try
		{
			TextBox = (MyTextBox)base.Template.FindName("PART_EditableTextBox", this);
			TextBox.AddHandler(UIElement.LostFocus, (RoutedEventHandler)([SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				RefreshColor();
			}));
			TextBox.ChangedEventList.Add([SpecialName] (object sender, RoutedEventArgs e) =>
			{
				TextChanged?.Invoke(RuntimeHelpers.GetObjectValue(sender), (TextChangedEventArgs)e);
			});
			TextBox.Tag = RuntimeHelpers.GetObjectValue(base.Tag);
			if (Operators.CompareString(Text, "", TextCompare: false) == 0)
			{
				TextBox.Text = _Text;
			}
			else
			{
				TextChanged?.Invoke(this, null);
			}
			if (HintText.Length > 0)
			{
				TextBox.HintText = HintText;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, string.Format("初始化可编辑文本框失败（{0}）", base.Name ?? ""), (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyComboBox.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void MyComboBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		IsMouseDown = true;
	}

	private void MyComboBox_PreviewMouseLeftButtonUp(object sender, EventArgs e)
	{
		IsMouseDown = false;
	}

	public void RefreshColor()
	{
		string text;
		string text2;
		int time;
		if (base.IsEnabled)
		{
			if (Conversions.ToBoolean(IsMouseDown || base.IsDropDownOpen || (base.IsEditable && Conversions.ToBoolean(NewLateBinding.LateGet(base.Template.FindName("PART_EditableTextBox", this), null, "IsFocused", new object[0], null, null, null)))))
			{
				text = "ColorBrush3";
				text2 = "ColorBrush7";
				time = 10;
			}
			else if (base.IsMouseOver)
			{
				text = "ColorBrush4";
				text2 = "ColorBrush7";
				time = 100;
			}
			else
			{
				text = "ColorBrushBg0";
				text2 = "ColorBrushHalfWhite";
				time = 100;
			}
		}
		else
		{
			text = "ColorBrushGray5";
			text2 = "ColorBrushGray6";
			time = 200;
		}
		if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
		{
			ModAnimation.AniStart(new ModAnimation.AniData[2]
			{
				ModAnimation.AaColor(this, Control.ForegroundProperty, text, time),
				ModAnimation.AaColor(this, Control.BackgroundProperty, text2, time)
			}, "MyComboBox Color " + Conversions.ToString(Uuid));
		}
		else
		{
			ModAnimation.AniStop("MyComboBox Color " + Conversions.ToString(Uuid));
			SetResourceReference(Control.ForegroundProperty, text);
			SetResourceReference(Control.BackgroundProperty, text2);
		}
	}

	private void MyComboBox_DropDownOpened(object sender, EventArgs e)
	{
		RealWidth = base.Width;
		base.Width = base.ActualWidth;
		try
		{
			((Grid)base.Template.FindName("PanPopup", this)).Opacity = ModMain.FrmMain.Opacity;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "设置下拉框透明度失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyComboBox.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void MyComboBox_DropDownClosed(object sender, EventArgs e)
	{
		base.Width = RealWidth;
	}

	private void MyComboBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (!IsTextChanging && base.IsEditable && base.SelectedItem != null && Operators.CompareString(Text, base.SelectedItem.ToString(), TextCompare: false) != 0)
		{
			string text = Text;
			int selectionStart = TextBox.SelectionStart;
			IsTextChanging = true;
			base.SelectedItem = null;
			Text = text;
			TextBox.SelectionStart = selectionStart;
			IsTextChanging = false;
		}
	}

	private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
		{
			ModMain.RaiseCustomEvent(this);
		}
	}

	private void RefreshSetting(string NewValue)
	{
		DependencyObject dependencyObject = base.Items.OfType<DependencyObject>().FirstOrDefault([SpecialName] (DependencyObject i) => ModBase.Val(RuntimeHelpers.GetObjectValue(SettingService.GetValue(i) ?? ((object)(-1)))) == Conversions.ToDouble(NewValue));
		if (dependencyObject != null)
		{
			base.SelectedItem = dependencyObject;
			return;
		}
		if ((double)base.Items.Count > Conversions.ToDouble(NewValue))
		{
			base.SelectedIndex = Conversions.ToInteger(NewValue);
			return;
		}
		Logger.Warn($"尝试显示设置 {SettingService.GetKey(this)}，但没有找到设置值 {NewValue} 的对应项", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyComboBox.vb");
		base.SelectedIndex = -1;
		base.SelectedItem = null;
	}

	void ISettingControl.RefreshSetting(string NewValue)
	{
		//ILSpy generated this explicit interface implementation from .override directive in RefreshSetting
		this.RefreshSetting(NewValue);
	}

	private string GetCurrentSetting()
	{
		if (base.SelectedItem == null)
		{
			return null;
		}
		if (base.SelectedItem is DependencyObject)
		{
			return Conversions.ToString(SettingService.GetValue((DependencyObject)base.SelectedItem) ?? ((object)base.SelectedIndex));
		}
		return Conversions.ToString(base.SelectedIndex);
	}

	string ISettingControl.GetCurrentSetting()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetCurrentSetting
		return this.GetCurrentSetting();
	}

	private void SaveSetting()
	{
		if (base.SelectedItem != null)
		{
			SettingService.SaveSetting(this);
		}
	}
}
