using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MeloongCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class MyTextBox : TextBox, ISettingControl
{
	public delegate void ValidateChangedEventHandler(object sender, EventArgs e);

	private enum ValidateState
	{
		NotInited,
		Success,
		FailedButTextNotChanged,
		FailedAndShowDetail,
		FailedAndHideDetail,
		NotLoaded
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__46_002D0
	{
		public bool _0024VB_0024Local_IsFinished;

		public MyTextBox _0024VB_0024Me;

		public _Closure_0024__46_002D0(_Closure_0024__46_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_IsFinished = arg0._0024VB_0024Local_IsFinished;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__3()
		{
			if (_0024VB_0024Me.labWrong != null)
			{
				_0024VB_0024Me.labWrong.Text = _0024VB_0024Me.ValidateResult;
				_0024VB_0024Local_IsFinished = true;
			}
			if (!_0024VB_0024Me.IsLoaded)
			{
				_0024VB_0024Local_IsFinished = true;
			}
		}
	}

	public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MyTextBox), new PropertyMetadata(new CornerRadius(3.0)));

	private TextBlock _labWrong;

	private TextBlock _labHint;

	public int Uuid;

	private List<RoutedEventHandler> _ChangedEventList;

	public static readonly DependencyProperty ValidateResultProperty = DependencyProperty.Register("ValidateResult", typeof(string), typeof(MyTextBox), new PropertyMetadata("", [SpecialName] (DependencyObject a0, DependencyPropertyChangedEventArgs a1) =>
	{
		((VB_0024AnonymousDelegate_6<MyTextBox, DependencyPropertyChangedEventArgs>)([SpecialName] (MyTextBox d, DependencyPropertyChangedEventArgs e) =>
		{
			d.SetValue(IsValidatedPropertyKey, string.IsNullOrEmpty(Conversions.ToString(e.NewValue)));
		}))((MyTextBox)a0, a1);
	}));

	private static readonly DependencyPropertyKey IsValidatedPropertyKey = DependencyProperty.RegisterReadOnly("IsValidated", typeof(bool), typeof(MyTextBox), new PropertyMetadata(true));

	public static readonly DependencyProperty IsValidatedProperty = IsValidatedPropertyKey.DependencyProperty;

	private Collection<Validate> _ValidateRules;

	private ValidateState ShownValidateResult;

	private bool IsTextChanged;

	public static readonly DependencyProperty HintTextProperty = DependencyProperty.Register("HintText", typeof(string), typeof(MyTextBox), new PropertyMetadata("", [SpecialName] (DependencyObject a0, DependencyPropertyChangedEventArgs a1) =>
	{
		((VB_0024AnonymousDelegate_7<MyTextBox, DependencyPropertyChangedEventArgs>)([SpecialName] (MyTextBox t, DependencyPropertyChangedEventArgs e) =>
		{
			if (t.labHint != null)
			{
				t.labHint.Text = ((Operators.CompareString(t.Text, "", TextCompare: false) == 0) ? t.HintText : "");
			}
		}))((MyTextBox)a0, a1);
	}));

	public bool HasBackground { get; set; }

	public bool ShowValidateResult { get; set; }

	public CornerRadius CornerRadius
	{
		get
		{
			object value = GetValue(CornerRadiusProperty);
			if (value == null)
			{
				return default(CornerRadius);
			}
			return (CornerRadius)value;
		}
		set
		{
			SetValue(CornerRadiusProperty, value);
		}
	}

	private TextBlock labWrong
	{
		get
		{
			if (base.Template == null)
			{
				return null;
			}
			if (_labWrong == null)
			{
				_labWrong = (TextBlock)base.Template.FindName("labWrong", this);
			}
			return _labWrong;
		}
	}

	private TextBlock labHint
	{
		get
		{
			if (base.Template == null)
			{
				return null;
			}
			if (_labHint == null)
			{
				_labHint = (TextBlock)base.Template.FindName("labHint", this);
			}
			return _labHint;
		}
	}

	public List<RoutedEventHandler> ChangedEventList
	{
		get
		{
			if (_ChangedEventList == null)
			{
				_ChangedEventList = new List<RoutedEventHandler>();
			}
			return _ChangedEventList;
		}
	}

	public string ValidateResult
	{
		get
		{
			return Conversions.ToString(GetValue(ValidateResultProperty));
		}
		set
		{
			SetValue(ValidateResultProperty, value);
		}
	}

	public bool IsValidated => Conversions.ToBoolean(GetValue(IsValidatedProperty));

	public Collection<Validate> ValidateRules
	{
		get
		{
			return _ValidateRules;
		}
		set
		{
			_ValidateRules = value;
			Validate();
		}
	}

	public string HintText
	{
		get
		{
			return Conversions.ToString(GetValue(HintTextProperty));
		}
		set
		{
			SetValue(HintTextProperty, value);
		}
	}

	public static event ValidateChangedEventHandler ValidateChanged;

	public event RoutedEventHandler ValidatedTextChanged
	{
		add
		{
			ChangedEventList.Add(value);
		}
		remove
		{
			ChangedEventList.Remove(value);
		}
	}

	public MyTextBox()
	{
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			Validate();
		};
		base.TextChanged += [SpecialName] (object a0, TextChangedEventArgs a1) =>
		{
			MyTextBox_TextChanged((MyTextBox)a0, a1);
		};
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
		base.GotFocus += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.LostFocus += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			RefreshColor();
		};
		base.IsEnabledChanged += [SpecialName] (object a0, DependencyPropertyChangedEventArgs a1) =>
		{
			RefreshTextColor();
		};
		base.KeyUp += MyTextBox_KeyUp;
		ValidatedTextChanged += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			SaveSetting();
		};
		HasBackground = true;
		ShowValidateResult = true;
		_labWrong = null;
		_labHint = null;
		Uuid = ModBase.GetUuid();
		_ValidateRules = new Collection<Validate>();
		ShownValidateResult = ValidateState.NotInited;
		IsTextChanged = false;
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		if (Operators.CompareString(HintText, "", TextCompare: false) != 0 && Operators.CompareString(labHint.Text, "", TextCompare: false) == 0)
		{
			labHint.Text = ((Operators.CompareString(base.Text, "", TextCompare: false) == 0) ? HintText : "");
		}
	}

	[SpecialName]
	private void raise_ValidatedTextChanged(object sender, TextChangedEventArgs e)
	{
		foreach (RoutedEventHandler changedEvent in ChangedEventList)
		{
			if (!Information.IsNothing(changedEvent))
			{
				changedEvent(RuntimeHelpers.GetObjectValue(sender), e);
			}
		}
	}

	public void Validate()
	{
		ValidateResult = ModValidate.Validate(base.Text, ValidateRules);
		if (ShownValidateResult != (ValidateState)(IsValidated ? 1 : 3))
		{
			if (base.IsLoaded && labWrong != null)
			{
				ChangeValidateResult(IsValidated, IsLoaded: true);
			}
			else
			{
				ModBase.RunInNewThread([SpecialName] () =>
				{
					Thread.Sleep(30);
					ModBase.RunInUi([SpecialName] () =>
					{
						ChangeValidateResult(IsValidated, IsLoaded: false);
					});
				}, "DelayedValidate Change");
			}
		}
		if (!ShowValidateResult || IsValidated)
		{
			return;
		}
		if (base.IsLoaded && labWrong != null)
		{
			labWrong.Text = ValidateResult;
			return;
		}
		ModBase.RunInNewThread([SpecialName] () =>
		{
			_Closure_0024__46_002D0 arg = default(_Closure_0024__46_002D0);
			_Closure_0024__46_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__46_002D0(arg);
			CS_0024_003C_003E8__locals0._0024VB_0024Me = this;
			CS_0024_003C_003E8__locals0._0024VB_0024Local_IsFinished = false;
			while (!CS_0024_003C_003E8__locals0._0024VB_0024Local_IsFinished)
			{
				Thread.Sleep(20);
				ModBase.RunInUiWait([SpecialName] () =>
				{
					if (CS_0024_003C_003E8__locals0._0024VB_0024Me.labWrong != null)
					{
						CS_0024_003C_003E8__locals0._0024VB_0024Me.labWrong.Text = CS_0024_003C_003E8__locals0._0024VB_0024Me.ValidateResult;
						CS_0024_003C_003E8__locals0._0024VB_0024Local_IsFinished = true;
					}
					if (!CS_0024_003C_003E8__locals0._0024VB_0024Me.IsLoaded)
					{
						CS_0024_003C_003E8__locals0._0024VB_0024Local_IsFinished = true;
					}
				});
			}
		}, "DelayedValidate Text");
	}

	public void ForceShowAsSuccess()
	{
		IsTextChanged = false;
		ChangeValidateResult(IsValidated, IsLoaded: true);
	}

	private void ChangeValidateResult(bool IsSuccessful, bool IsLoaded)
	{
		if (IsLoaded && ModAnimation.AniControlEnabled == 0 && labWrong != null)
		{
			if (IsSuccessful || !IsTextChanged)
			{
				ShownValidateResult = (IsSuccessful ? ValidateState.Success : ValidateState.FailedButTextNotChanged);
				ModAnimation.AniStart(new ModAnimation.AniData[3]
				{
					ModAnimation.AaOpacity(labWrong, 0.0 - labWrong.Opacity, 150),
					ModAnimation.AaHeight(labWrong, 0.0 - labWrong.Height, 150, 0, new ModAnimation.AniEaseOutFluent()),
					ModAnimation.AaCode([SpecialName] () =>
					{
						labWrong.Visibility = Visibility.Collapsed;
					}, 0, After: true)
				}, "MyTextBox Validate " + Conversions.ToString(Uuid));
			}
			else if (ShowValidateResult)
			{
				ShownValidateResult = ValidateState.FailedAndShowDetail;
				labWrong.Visibility = Visibility.Visible;
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaOpacity(labWrong, 1.0 - labWrong.Opacity, 150),
					ModAnimation.AaHeight(labWrong, 21.0 - labWrong.Height, 150, 0, new ModAnimation.AniEaseOutFluent())
				}, "MyTextBox Validate " + Conversions.ToString(Uuid));
			}
			else
			{
				ShownValidateResult = ValidateState.FailedAndHideDetail;
			}
		}
		else
		{
			ShownValidateResult = ValidateState.NotLoaded;
		}
		RefreshColor();
		ValidateChanged?.Invoke(this, new EventArgs());
	}

	private void MyTextBox_TextChanged(MyTextBox sender, TextChangedEventArgs e)
	{
		try
		{
			if (labHint != null)
			{
				labHint.Text = ((Operators.CompareString(base.Text, "", TextCompare: false) == 0) ? HintText : "");
			}
			IsTextChanged = base.IsLoaded;
			Validate();
			if (IsValidated)
			{
				raise_ValidatedTextChanged(sender, e);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "进行输入验证时出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyTextBox.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void RefreshColor()
	{
		try
		{
			if (base.TemplatedParent != null && base.TemplatedParent is MyComboBox)
			{
				return;
			}
			string text;
			string text2;
			int time;
			if (base.IsEnabled)
			{
				if (IsValidated || !IsTextChanged)
				{
					if (base.IsFocused)
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
					text = "ColorBrushRedLight";
					text2 = "ColorBrushRedBack";
					time = 200;
				}
			}
			else
			{
				text = "ColorBrushGray5";
				text2 = "ColorBrushGray6";
				time = 200;
			}
			if (!HasBackground)
			{
				text2 = "ColorBrushTransparent";
			}
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaColor(this, Control.BorderBrushProperty, text, time),
					ModAnimation.AaColor(this, Control.BackgroundProperty, text2, time)
				}, "MyTextBox Color " + Conversions.ToString(Uuid));
			}
			else
			{
				ModAnimation.AniStop("MyTextBox Color " + Conversions.ToString(Uuid));
				SetResourceReference(Control.BorderBrushProperty, text);
				SetResourceReference(Control.BackgroundProperty, text2);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "文本框颜色改变出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\MyTextBox.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void RefreshTextColor()
	{
		ModBase.MyColor myColor = (base.IsEnabled ? ModSecret.ColorGray1 : ModSecret.ColorGray4);
		if ((double)(int)((SolidColorBrush)base.Foreground).Color.R != myColor.R)
		{
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0 && Operators.CompareString(base.Text, "", TextCompare: false) != 0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[1] { ModAnimation.AaColor(this, Control.ForegroundProperty, base.IsEnabled ? "ColorBrushGray1" : "ColorBrushGray4", 200) }, "MyTextBox TextColor " + Conversions.ToString(Uuid));
			}
			else
			{
				ModAnimation.AniStop("MyTextBox TextColor " + Conversions.ToString(Uuid));
				base.Foreground = myColor;
			}
		}
	}

	private void MyTextBox_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return)
		{
			ModMain.RaiseCustomEvent(this);
		}
	}

	private void RefreshSetting(string NewValue)
	{
		base.Text = NewValue;
	}

	void ISettingControl.RefreshSetting(string NewValue)
	{
		//ILSpy generated this explicit interface implementation from .override directive in RefreshSetting
		this.RefreshSetting(NewValue);
	}

	private string GetCurrentSetting()
	{
		return base.Text;
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
}
