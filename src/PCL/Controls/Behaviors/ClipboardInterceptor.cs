using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using ThrottleDebounce;

namespace PCL.Controls.Behaviors;

public sealed class ClipboardInterceptor
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _Closure_0024__
	{
		public static readonly _Closure_0024__ _0024I;

		public static Func<int, string> _0024I13_002D0;

		public static VB_0024AnonymousDelegate_0<TimeSpan> _0024I13_002D1;

		public static Func<int, TimeSpan> _0024IR13_002D1;

		public static Func<int, string> _0024I16_002D0;

		public static VB_0024AnonymousDelegate_0<TimeSpan> _0024I16_002D1;

		public static Func<int, TimeSpan> _0024IR16_002D2;

		public static Func<DataGridCellInfo, object> _0024I17_002D0;

		public static Func<DataGridCellInfo, string> _0024I17_002D1;

		static _Closure_0024__()
		{
			_0024I = new _Closure_0024__();
		}

		[SpecialName]
		internal string _Lambda_0024__13_002D0(int Attempt)
		{
			if (!System.Windows.Forms.Clipboard.ContainsText())
			{
				return null;
			}
			return System.Windows.Forms.Clipboard.GetText();
		}

		[SpecialName]
		internal TimeSpan _Lambda_0024__R13_002D1(int a0)
		{
			return ((VB_0024AnonymousDelegate_0<TimeSpan>)([SpecialName] () => TimeSpan.FromMilliseconds(100.0)))();
		}

		[SpecialName]
		internal TimeSpan _Lambda_0024__13_002D1()
		{
			return TimeSpan.FromMilliseconds(100.0);
		}

		[SpecialName]
		internal string _Lambda_0024__16_002D0(int Attempt)
		{
			if (!System.Windows.Forms.Clipboard.ContainsText())
			{
				return null;
			}
			return System.Windows.Forms.Clipboard.GetText();
		}

		[SpecialName]
		internal TimeSpan _Lambda_0024__R16_002D2(int a0)
		{
			return ((VB_0024AnonymousDelegate_0<TimeSpan>)([SpecialName] () => TimeSpan.FromMilliseconds(200.0)))();
		}

		[SpecialName]
		internal TimeSpan _Lambda_0024__16_002D1()
		{
			return TimeSpan.FromMilliseconds(200.0);
		}

		[SpecialName]
		internal object _Lambda_0024__17_002D0(DataGridCellInfo c)
		{
			return c.Item;
		}

		[SpecialName]
		internal string _Lambda_0024__17_002D1(DataGridCellInfo cell)
		{
			if (!(cell.Column.GetCellContent(RuntimeHelpers.GetObjectValue(cell.Item)) is TextBlock textBlock))
			{
				return "";
			}
			return textBlock.Text;
		}
	}

	public static readonly DependencyProperty EnableSafeClipboardProperty = DependencyProperty.RegisterAttached("EnableSafeClipboard", typeof(bool), typeof(ClipboardInterceptor), new PropertyMetadata(false, OnEnableSafeClipboardChanged));

	private ClipboardInterceptor()
	{
	}

	public static void SetEnableSafeClipboard(DependencyObject element, bool value)
	{
		element.SetValue(EnableSafeClipboardProperty, value);
	}

	public static bool GetEnableSafeClipboard(DependencyObject element)
	{
		return Conversions.ToBoolean(element.GetValue(EnableSafeClipboardProperty));
	}

	private static void OnEnableSafeClipboardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is System.Windows.Controls.TextBox && Conversions.ToBoolean(e.NewValue))
		{
			AddCommandBindingsToTextBox((System.Windows.Controls.TextBox)d);
		}
		else if (d is System.Windows.Controls.RichTextBox && Conversions.ToBoolean(e.NewValue))
		{
			AddCommandBindingsToRichTextBox((System.Windows.Controls.RichTextBox)d);
		}
		else if (d is System.Windows.Controls.DataGrid && Conversions.ToBoolean(e.NewValue))
		{
			AddCommandBindingsToDataGrid((System.Windows.Controls.DataGrid)d);
		}
	}

	private static void AddCommandBindingsToTextBox(System.Windows.Controls.TextBox tb)
	{
		tb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, OnCopyTextBox));
		tb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, OnCutTextBox, CanEditTextBox));
		tb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteTextBox, CanEditTextBox));
	}

	private static void AddCommandBindingsToRichTextBox(System.Windows.Controls.RichTextBox rtb)
	{
		rtb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, OnCopyRichTextBox));
		rtb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, OnCutRichTextBox));
		rtb.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteRichTextBox));
	}

	private static void AddCommandBindingsToDataGrid(System.Windows.Controls.DataGrid dg)
	{
		dg.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, OnCopyDataGrid));
	}

	private static void SetClipboardText(string text)
	{
		System.Windows.Forms.Clipboard.SetDataObject(text, copy: false, 10, 100);
		try
		{
			System.Windows.Clipboard.Flush();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, $"剪贴板持久化失败（错误码：0x{ex2.HResult:X8}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
			ProjectData.ClearProjectError();
		}
	}

	private static void OnCopyTextBox(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender is System.Windows.Controls.TextBox { SelectionLength: >0 } textBox)
		{
			try
			{
				SetClipboardText(textBox.SelectedText);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"复制失败，请稍后再试（错误码：0x{ex2.HResult:X8}）", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
				ProjectData.ClearProjectError();
			}
			e.Handled = true;
		}
	}

	private static void CanEditTextBox(object sender, CanExecuteRoutedEventArgs e)
	{
		System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
		e.CanExecute = textBox != null && !textBox.IsReadOnly && (e.Command != ApplicationCommands.Cut || textBox.SelectionLength > 0);
		e.Handled = true;
	}

	private static void OnCutTextBox(object sender, ExecutedRoutedEventArgs e)
	{
		System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
		e.Handled = true;
		if (textBox != null && !textBox.IsReadOnly && textBox.SelectionLength > 0)
		{
			try
			{
				SetClipboardText(textBox.SelectedText);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"剪切失败，请稍后再试（错误码：0x{ex2.HResult:X8}）", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
				ProjectData.ClearProjectError();
				return;
			}
			textBox.SelectedText = string.Empty;
		}
	}

	private static void OnPasteTextBox(object sender, ExecutedRoutedEventArgs e)
	{
		System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
		e.Handled = true;
		if (textBox == null || textBox.IsReadOnly)
		{
			return;
		}
		try
		{
			string text = Retrier.Attempt<string>((Func<int, string>)([SpecialName] (int Attempt) => (!System.Windows.Forms.Clipboard.ContainsText()) ? null : System.Windows.Forms.Clipboard.GetText()), (uint?)10u, (Func<int, TimeSpan>)([SpecialName] (int a0) => ((VB_0024AnonymousDelegate_0<TimeSpan>)([SpecialName] () => TimeSpan.FromMilliseconds(100.0)))()), (Func<Exception, bool>)null, (Action)null, default(CancellationToken));
			if (text != null)
			{
				if (!textBox.AcceptsReturn)
				{
					text = StringExtensions.ReplaceLineEndings(text, " ", true);
				}
				int selectionStart = textBox.SelectionStart;
				textBox.SelectedText = text;
				textBox.CaretIndex = checked(selectionStart + text.Length);
				textBox.SelectionLength = 0;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, $"粘贴失败，请稍后再试（错误码：0x{ex2.HResult:X8}）", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
			ProjectData.ClearProjectError();
		}
	}

	private static void OnCopyRichTextBox(object sender, ExecutedRoutedEventArgs e)
	{
		if (!(sender is System.Windows.Controls.RichTextBox richTextBox))
		{
			return;
		}
		TextRange textRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
		if (!string.IsNullOrEmpty(textRange.Text))
		{
			try
			{
				SetClipboardText(textRange.Text);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"复制失败，请稍后再试（错误码：0x{ex2.HResult:X8}）", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
				ProjectData.ClearProjectError();
			}
			e.Handled = true;
		}
	}

	private static void OnCutRichTextBox(object sender, ExecutedRoutedEventArgs e)
	{
		if (!(sender is System.Windows.Controls.RichTextBox richTextBox))
		{
			return;
		}
		TextRange textRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
		if (!string.IsNullOrEmpty(textRange.Text))
		{
			e.Handled = true;
			try
			{
				SetClipboardText(textRange.Text);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"剪切失败，请稍后再试（错误码：0x{ex2.HResult:X8}）", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
				ProjectData.ClearProjectError();
				return;
			}
			textRange.Text = string.Empty;
		}
	}

	private static void OnPasteRichTextBox(object sender, ExecutedRoutedEventArgs e)
	{
		if (!(sender is System.Windows.Controls.RichTextBox richTextBox))
		{
			return;
		}
		try
		{
			string text = Retrier.Attempt<string>((Func<int, string>)([SpecialName] (int Attempt) => (!System.Windows.Forms.Clipboard.ContainsText()) ? null : System.Windows.Forms.Clipboard.GetText()), (uint?)2u, (Func<int, TimeSpan>)([SpecialName] (int a0) => ((VB_0024AnonymousDelegate_0<TimeSpan>)([SpecialName] () => TimeSpan.FromMilliseconds(200.0)))()), (Func<Exception, bool>)null, (Action)null, default(CancellationToken));
			if (text == null)
			{
				return;
			}
			TextSelection selection = richTextBox.Selection;
			selection.Text = text;
			TextPointer textPointer = (richTextBox.CaretPosition = selection.End);
			richTextBox.Selection.Select(textPointer, textPointer);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, $"粘贴失败，请稍后再试（错误码：0x{ex2.HResult:X8}）", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
			ProjectData.ClearProjectError();
		}
		e.Handled = true;
	}

	private static void OnCopyDataGrid(object sender, ExecutedRoutedEventArgs e)
	{
		if (!(sender is System.Windows.Controls.DataGrid { SelectedCells: not null } dataGrid) || dataGrid.SelectedCells.Count == 0)
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		IEnumerable<IGrouping<object, DataGridCellInfo>> enumerable = from c in dataGrid.SelectedCells
			group c by c.Item;
		foreach (IGrouping<object, DataGridCellInfo> item in enumerable)
		{
			string value = string.Join("\t", item.Select((_Closure_0024__._0024I17_002D1 == null) ? (_Closure_0024__._0024I17_002D1 = [SpecialName] (DataGridCellInfo cell) => (!(cell.Column.GetCellContent(RuntimeHelpers.GetObjectValue(cell.Item)) is TextBlock textBlock)) ? "" : textBlock.Text) : _Closure_0024__._0024I17_002D1));
			stringBuilder.AppendLine(value);
		}
		string clipboardText = stringBuilder.ToString().TrimEnd('\r', '\n');
		try
		{
			SetClipboardText(clipboardText);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, $"复制失败，请稍后再试（错误码：0x{ex2.HResult:X8}）", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Controls\\Behaviors\\ClipboardInterceptor.vb");
			ProjectData.ClearProjectError();
		}
		e.Handled = true;
	}
}
