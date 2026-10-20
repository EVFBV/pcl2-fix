using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using MeloongCore;
using MeloongCore.Extensions;
using MeloongCore.Wpf.Extensions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using PCL.My;

namespace PCL;

[DesignerGenerated]
public class FormMain : Window, IComponentConnector
{
	public enum PageType
	{
		Launch,
		Download,
		Link,
		Setup,
		Other,
		InstanceSelect,
		TaskManager,
		InstanceSetup,
		ResourceDetail,
		HelpDetail
	}

	public enum PageSubType
	{
		Default = 0,
		DownloadInstall = 0,
		DownloadMod = 2,
		DownloadPack = 3,
		DownloadDataPack = 4,
		DownloadResourcePack = 5,
		DownloadShader = 6,
		SetupLaunch = 0,
		SetupLink = 1,
		SetupUI = 2,
		SetupSystem = 3,
		LinkMain = 0,
		OtherHelp = 0,
		OtherAbout = 1,
		OtherTest = 2,
		InstanceOverall = 0,
		InstanceSetup = 1,
		InstanceModify = 2,
		InstanceMod = 3,
		InstanceModDisabled = 4,
		InstanceExport = 5
	}

	public class PageStackData
	{
		public PageType Page;

		public object Additional;

		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (other is PageStackData)
			{
				PageStackData pageStackData = (PageStackData)other;
				if (Page != pageStackData.Page)
				{
					return false;
				}
				if (Additional == null)
				{
					return pageStackData.Additional == null;
				}
				return pageStackData.Additional != null && Additional.Equals(RuntimeHelpers.GetObjectValue(pageStackData.Additional));
			}
			if (other is int)
			{
				if (Operators.ConditionalCompareObjectNotEqual(Page, other, TextCompare: false))
				{
					return false;
				}
				return Additional == null;
			}
			return false;
		}

		public static bool operator ==(PageStackData left, PageStackData right)
		{
			return EqualityComparer<PageStackData>.Default.Equals(left, right);
		}

		public static bool operator !=(PageStackData left, PageStackData right)
		{
			return !(left == right);
		}

		public static implicit operator PageStackData(PageType Value)
		{
			return new PageStackData
			{
				Page = Value
			};
		}

		public static implicit operator PageType(PageStackData Value)
		{
			return Value.Page;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__1_002D0
	{
		public string _0024VB_0024Local_Content;

		public _Closure_0024__1_002D0(_Closure_0024__1_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Content = arg0._0024VB_0024Local_Content;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			if (ModMain.MyMsgBox(_0024VB_0024Local_Content, "PCL 已更新至快照版 2.14.0.0", "确定", "完整更新日志") == 2)
			{
				ModBase.OpenWebsite("https://meloong.com/afd/a/LTCat?tab=feed");
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__22_002D0
	{
		public string _0024VB_0024Local_Code;

		public Action _0024I1;

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			if (ModMain.MyMsgBox("嘿，是否使用复制的邀请码加入房间？", "加入联机房间", "加入", "取消") != 2)
			{
				ModBase.RunInUi([SpecialName] () =>
				{
					PageLinkMain.Join(_0024VB_0024Local_Code);
					ModBase.ClipboardSet(null, ShowSuccessHint: false);
				});
			}
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			PageLinkMain.Join(_0024VB_0024Local_Code);
			ModBase.ClipboardSet(null, ShowSuccessHint: false);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__25_002D0
	{
		public IEnumerable<string> _0024VB_0024Local_FilePathList;

		public _Closure_0024__25_002D0(_Closure_0024__25_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_FilePathList = arg0._0024VB_0024Local_FilePathList;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			_Closure_0024__25_002D1 arg = default(_Closure_0024__25_002D1);
			_Closure_0024__25_002D1 CS_0024_003C_003E8__locals0 = new _Closure_0024__25_002D1(arg);
			string text = _0024VB_0024Local_FilePathList.First();
			Logger.Warn(string.Format("接受文件拖拽：{0}{1}", text, _0024VB_0024Local_FilePathList.Any() ? $" 等 {_0024VB_0024Local_FilePathList.Count()} 个文件" : ""), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			if (DirectoryUtils.Exists(_0024VB_0024Local_FilePathList.First()) && !FileUtils.Exists(_0024VB_0024Local_FilePathList.First()))
			{
				ModMain.Hint("请拖入一个文件，而非文件夹！", ModMain.HintType.Red);
				return;
			}
			if (!FileUtils.Exists(_0024VB_0024Local_FilePathList.First()))
			{
				ModMain.Hint("拖入的文件不存在：" + _0024VB_0024Local_FilePathList.First(), ModMain.HintType.Red);
				return;
			}
			List<string> list = _0024VB_0024Local_FilePathList.ToList();
			if (_0024VB_0024Local_FilePathList.Count() > 1)
			{
				foreach (string item in list)
				{
					if (!new string[4] { "jar", "litemod", "disabled", "old" }.Contains(PathUtils.GetExtension(item)))
					{
						ModMain.Hint("一次请只拖入一个文件！", ModMain.HintType.Red);
						return;
					}
				}
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Extension = PathUtils.GetExtension(text);
			if (Operators.CompareString(CS_0024_003C_003E8__locals0._0024VB_0024Local_Extension, "xaml", TextCompare: false) == 0)
			{
				Logger.Info("文件后缀为 XAML，作为主页加载", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				if (!FileUtils.Exists(Paths.Base + "PCL\\Custom.xaml") || ModMain.MyMsgBox("已存在一个主页文件，是否要将它覆盖？", "覆盖确认", "覆盖", "取消") != 2)
				{
					FileUtils.Copy(text, Paths.Base + "PCL\\Custom.xaml");
					ModBase.RunInUi([SpecialName] () =>
					{
						Settings.Set("UiCustomType", 1);
						ModMain.FrmLaunchRight.ForceRefresh();
						ModMain.Hint("已加载主页自定义文件！", ModMain.HintType.Green);
					});
				}
			}
			else
			{
				if (PageInstanceMod.InstallMods(list))
				{
					return;
				}
				if (new string[3] { "zip", "rar", "mrpack" }.Any([SpecialName] (string t) => Operators.CompareString(t, CS_0024_003C_003E8__locals0._0024VB_0024Local_Extension, TextCompare: false) == 0))
				{
					ModMain.Hint("正在读取压缩包……");
					try
					{
						Logger.Info("文件为压缩包，尝试作为整合包安装", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						ModModpack.ModpackInstall(text);
						ModBase.RunInUi([SpecialName] () =>
						{
							ModMain.FrmMain.PageChange(PageType.TaskManager);
						});
						return;
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						if (ExceptionExtensions.IsCanceled(ex2))
						{
							ProjectData.ClearProjectError();
							return;
						}
						ProjectData.ClearProjectError();
					}
				}
				if (Operators.CompareString(CS_0024_003C_003E8__locals0._0024VB_0024Local_Extension, "rar", TextCompare: false) == 0)
				{
					ModMain.Hint("PCL 无法处理 rar 格式的压缩包，请在解压后重新压缩为 zip 格式再试！", ModMain.HintType.Red);
					return;
				}
				try
				{
					Logger.Info("尝试进行错误报告分析", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					CrashAnalyzer crashAnalyzer = new CrashAnalyzer();
					crashAnalyzer.Import(text);
					if (crashAnalyzer.Prepare())
					{
						crashAnalyzer.Analyze();
						crashAnalyzer.Output(IsManualAnalyze: true, new List<string>());
						return;
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Error(ex4, "自主错误报告分析失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					ProjectData.ClearProjectError();
				}
				ModMain.Hint("PCL 无法确定适合的操作……", ModMain.HintType.Red);
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__25_002D1
	{
		public string _0024VB_0024Local_Extension;

		public _Closure_0024__25_002D1(_Closure_0024__25_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Extension = arg0._0024VB_0024Local_Extension;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__2(string t)
		{
			return Operators.CompareString(t, _0024VB_0024Local_Extension, TextCompare: false) == 0;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__50_002D0
	{
		public string _0024VB_0024Local_PageName;

		public FormMain _0024VB_0024Me;

		public _Closure_0024__50_002D0(_Closure_0024__50_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_PageName = arg0._0024VB_0024Local_PageName;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			_0024VB_0024Me.LabTitleInner.Text = _0024VB_0024Local_PageName;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__9_002D0
	{
		public ScaleTransform _0024VB_0024Local_TransformScale;

		public TranslateTransform _0024VB_0024Local_TransformPos;

		public RotateTransform _0024VB_0024Local_TransformRotate;

		public _Closure_0024__9_002D0(_Closure_0024__9_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_TransformScale = arg0._0024VB_0024Local_TransformScale;
				_0024VB_0024Local_TransformPos = arg0._0024VB_0024Local_TransformPos;
				_0024VB_0024Local_TransformRotate = arg0._0024VB_0024Local_TransformRotate;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__2(object i)
		{
			ScaleTransform scaleTransform;
			(scaleTransform = _0024VB_0024Local_TransformScale).ScaleX = Conversions.ToDouble(Operators.AddObject(scaleTransform.ScaleX, i));
			(scaleTransform = _0024VB_0024Local_TransformScale).ScaleY = Conversions.ToDouble(Operators.AddObject(scaleTransform.ScaleY, i));
		}

		[SpecialName]
		internal void _Lambda_0024__3(object i)
		{
			TranslateTransform translateTransform;
			(translateTransform = _0024VB_0024Local_TransformPos).Y = Conversions.ToDouble(Operators.AddObject(translateTransform.Y, i));
		}

		[SpecialName]
		internal void _Lambda_0024__4(object i)
		{
			RotateTransform rotateTransform;
			(rotateTransform = _0024VB_0024Local_TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(rotateTransform.Angle, i));
		}
	}

	private bool IsWindowLoadFinished;

	private static bool IsLogShown = false;

	public bool IsSizeSaveable;

	public bool IsSystemTimeChanged;

	private bool _Hidden;

	public PageStackData PageCurrent;

	public PageStackData PageLast;

	public List<PageStackData> PageStack;

	public MyPageLeft PageLeft;

	public MyPageRight PageRight;

	private bool IsChangingPage;

	public MouseEventArgs lastMouseArg;

	[CompilerGenerated]
	[AccessedThroughProperty("PanBack")]
	private Grid _PanBack;

	[CompilerGenerated]
	[AccessedThroughProperty("PanTitle")]
	private Grid _PanTitle;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnTitleClose")]
	private MyIconButton _BtnTitleClose;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnTitleMin")]
	private MyIconButton _BtnTitleMin;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnTitleSelect0")]
	private MyRadioButton _BtnTitleSelect0;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnTitleSelect1")]
	private MyRadioButton _BtnTitleSelect1;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnTitleSelect2")]
	private MyRadioButton _BtnTitleSelect2;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnTitleSelect3")]
	private MyRadioButton _BtnTitleSelect3;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnTitleSelect4")]
	private MyRadioButton _BtnTitleSelect4;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnTitleInner")]
	private MyIconButton _BtnTitleInner;

	[CompilerGenerated]
	[AccessedThroughProperty("PanMainLeft")]
	private Border _PanMainLeft;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnExtraBack")]
	private MyExtraButton _BtnExtraBack;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnExtraDownload")]
	private MyExtraButton _BtnExtraDownload;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnExtraApril")]
	private MyExtraButton _BtnExtraApril;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnExtraShutdown")]
	private MyExtraButton _BtnExtraShutdown;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnExtraMusic")]
	private MyExtraButton _BtnExtraMusic;

	[CompilerGenerated]
	[AccessedThroughProperty("PanMsg")]
	private Grid _PanMsg;

	private bool _contentLoaded;

	[SpecialName]
	private DragHelper _0024STATIC_0024_002Ector_00242001_0024Helper;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024_002Ector_00242001_0024Helper_0024Init;

	[SpecialName]
	private IDataObject _0024STATIC_0024HandleDrag_002420211C1284DD_0024PrevData;

	[SpecialName]
	private DragDropEffects _0024STATIC_0024HandleDrag_002420211C1284DD_0024PrevEffects;

	public bool Hidden
	{
		get
		{
			return _Hidden;
		}
		set
		{
			if (_Hidden == value)
			{
				return;
			}
			_Hidden = value;
			if (value)
			{
				base.Left -= 10000.0;
				base.ShowInTaskbar = false;
				base.Visibility = Visibility.Hidden;
				Logger.Info($"窗口已隐藏，位置：({base.Left},{base.Top})", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
			else
			{
				if (base.Left < -2000.0)
				{
					base.Left += 10000.0;
				}
				ShowWindowToTop();
			}
		}
	}

	public PageSubType PageCurrentSub
	{
		get
		{
			PageStackData pageCurrent = PageCurrent;
			if (pageCurrent == PageType.Download)
			{
				if (ModMain.FrmDownloadLeft == null)
				{
					ModMain.FrmDownloadLeft = new PageDownloadLeft();
				}
				return ModMain.FrmDownloadLeft.PageID;
			}
			if (pageCurrent == PageType.Setup)
			{
				if (ModMain.FrmSetupLeft == null)
				{
					ModMain.FrmSetupLeft = new PageSetupLeft();
				}
				return ModMain.FrmSetupLeft.PageID;
			}
			if (pageCurrent == PageType.Other)
			{
				if (ModMain.FrmOtherLeft == null)
				{
					ModMain.FrmOtherLeft = new PageOtherLeft();
				}
				return ModMain.FrmOtherLeft.PageID;
			}
			if (pageCurrent == PageType.InstanceSetup)
			{
				if (ModMain.FrmInstanceLeft == null)
				{
					ModMain.FrmInstanceLeft = new PageInstanceLeft();
				}
				return ModMain.FrmInstanceLeft.PageID;
			}
			return PageSubType.Default;
		}
	}

	[field: AccessedThroughProperty("WindMain")]
	internal virtual FormMain WindMain
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Grid PanBack
	{
		[CompilerGenerated]
		get
		{
			return _PanBack;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = [SpecialName] (object a0, MouseEventArgs a1) =>
			{
				DragDoing();
			};
			Grid panBack = _PanBack;
			if (panBack != null)
			{
				panBack.MouseMove -= value2;
			}
			_PanBack = value;
			panBack = _PanBack;
			if (panBack != null)
			{
				panBack.MouseMove += value2;
			}
		}
	}

	[field: AccessedThroughProperty("TransformRotate")]
	internal virtual RotateTransform TransformRotate
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TransformPos")]
	internal virtual TranslateTransform TransformPos
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ResizerT")]
	internal virtual Rectangle ResizerT
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ResizerB")]
	internal virtual Rectangle ResizerB
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ResizerR")]
	internal virtual Rectangle ResizerR
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ResizerL")]
	internal virtual Rectangle ResizerL
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ResizerLT")]
	internal virtual Rectangle ResizerLT
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ResizerLB")]
	internal virtual Rectangle ResizerLB
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ResizerRB")]
	internal virtual Rectangle ResizerRB
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ResizerRT")]
	internal virtual Rectangle ResizerRT
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("BorderForm")]
	internal virtual Border BorderForm
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RectForm")]
	internal virtual RectangleGeometry RectForm
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanForm")]
	internal virtual Grid PanForm
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgBack")]
	internal virtual Canvas ImgBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Grid PanTitle
	{
		[CompilerGenerated]
		get
		{
			return _PanTitle;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = FormDragMove;
			Grid panTitle = _PanTitle;
			if (panTitle != null)
			{
				panTitle.MouseLeftButtonDown -= value2;
			}
			_PanTitle = value;
			panTitle = _PanTitle;
			if (panTitle != null)
			{
				panTitle.MouseLeftButtonDown += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ImgTitle")]
	internal virtual MyImage ImgTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnTitleClose
	{
		[CompilerGenerated]
		get
		{
			return _BtnTitleClose;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				BtnTitleClose_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton btnTitleClose = _BtnTitleClose;
			if (btnTitleClose != null)
			{
				btnTitleClose.Click -= obj;
			}
			_BtnTitleClose = value;
			btnTitleClose = _BtnTitleClose;
			if (btnTitleClose != null)
			{
				btnTitleClose.Click += obj;
			}
		}
	}

	internal virtual MyIconButton BtnTitleMin
	{
		[CompilerGenerated]
		get
		{
			return _BtnTitleMin;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				BtnTitleMin_Click();
			};
			MyIconButton btnTitleMin = _BtnTitleMin;
			if (btnTitleMin != null)
			{
				btnTitleMin.Click -= obj;
			}
			_BtnTitleMin = value;
			btnTitleMin = _BtnTitleMin;
			if (btnTitleMin != null)
			{
				btnTitleMin.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanTitleMain")]
	internal virtual Grid PanTitleMain
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ShapeTitleLogo")]
	internal virtual Path ShapeTitleLogo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabTitleLogo")]
	internal virtual TextBlock LabTitleLogo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImageTitleLogo")]
	internal virtual MyImage ImageTitleLogo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanTitleSelect")]
	internal virtual StackPanel PanTitleSelect
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyRadioButton BtnTitleSelect0
	{
		[CompilerGenerated]
		get
		{
			return _BtnTitleSelect0;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyRadioButton.CheckEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				BtnTitleSelect_Click((MyRadioButton)a0, a1);
			};
			MyRadioButton btnTitleSelect = _BtnTitleSelect0;
			if (btnTitleSelect != null)
			{
				btnTitleSelect.Check -= obj;
			}
			_BtnTitleSelect0 = value;
			btnTitleSelect = _BtnTitleSelect0;
			if (btnTitleSelect != null)
			{
				btnTitleSelect.Check += obj;
			}
		}
	}

	internal virtual MyRadioButton BtnTitleSelect1
	{
		[CompilerGenerated]
		get
		{
			return _BtnTitleSelect1;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyRadioButton.CheckEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				BtnTitleSelect_Click((MyRadioButton)a0, a1);
			};
			MyRadioButton btnTitleSelect = _BtnTitleSelect1;
			if (btnTitleSelect != null)
			{
				btnTitleSelect.Check -= obj;
			}
			_BtnTitleSelect1 = value;
			btnTitleSelect = _BtnTitleSelect1;
			if (btnTitleSelect != null)
			{
				btnTitleSelect.Check += obj;
			}
		}
	}

	internal virtual MyRadioButton BtnTitleSelect2
	{
		[CompilerGenerated]
		get
		{
			return _BtnTitleSelect2;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyRadioButton.CheckEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				BtnTitleSelect_Click((MyRadioButton)a0, a1);
			};
			MyRadioButton btnTitleSelect = _BtnTitleSelect2;
			if (btnTitleSelect != null)
			{
				btnTitleSelect.Check -= obj;
			}
			_BtnTitleSelect2 = value;
			btnTitleSelect = _BtnTitleSelect2;
			if (btnTitleSelect != null)
			{
				btnTitleSelect.Check += obj;
			}
		}
	}

	internal virtual MyRadioButton BtnTitleSelect3
	{
		[CompilerGenerated]
		get
		{
			return _BtnTitleSelect3;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyRadioButton.CheckEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				BtnTitleSelect_Click((MyRadioButton)a0, a1);
			};
			MyRadioButton btnTitleSelect = _BtnTitleSelect3;
			if (btnTitleSelect != null)
			{
				btnTitleSelect.Check -= obj;
			}
			_BtnTitleSelect3 = value;
			btnTitleSelect = _BtnTitleSelect3;
			if (btnTitleSelect != null)
			{
				btnTitleSelect.Check += obj;
			}
		}
	}

	internal virtual MyRadioButton BtnTitleSelect4
	{
		[CompilerGenerated]
		get
		{
			return _BtnTitleSelect4;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyRadioButton.CheckEventHandler obj = [SpecialName] (object a0, bool a1) =>
			{
				BtnTitleSelect_Click((MyRadioButton)a0, a1);
			};
			MyRadioButton btnTitleSelect = _BtnTitleSelect4;
			if (btnTitleSelect != null)
			{
				btnTitleSelect.Check -= obj;
			}
			_BtnTitleSelect4 = value;
			btnTitleSelect = _BtnTitleSelect4;
			if (btnTitleSelect != null)
			{
				btnTitleSelect.Check += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanTitleInner")]
	internal virtual Grid PanTitleInner
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnTitleInner
	{
		[CompilerGenerated]
		get
		{
			return _BtnTitleInner;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				PageBack();
			};
			MyIconButton btnTitleInner = _BtnTitleInner;
			if (btnTitleInner != null)
			{
				btnTitleInner.Click -= obj;
			}
			_BtnTitleInner = value;
			btnTitleInner = _BtnTitleInner;
			if (btnTitleInner != null)
			{
				btnTitleInner.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("LabTitleInner")]
	internal virtual TextBlock LabTitleInner
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanLeft")]
	internal virtual Grid PanLeft
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RectLeftBackground")]
	internal virtual Rectangle RectLeftBackground
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RectLeftShadow")]
	internal virtual Rectangle RectLeftShadow
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanMain")]
	internal virtual Grid PanMain
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanMainRight")]
	internal virtual Border PanMainRight
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual Border PanMainLeft
	{
		[CompilerGenerated]
		get
		{
			return _PanMainLeft;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SizeChangedEventHandler value2 = PanMainLeft_SizeChanged;
			Border panMainLeft = _PanMainLeft;
			if (panMainLeft != null)
			{
				panMainLeft.SizeChanged -= value2;
			}
			_PanMainLeft = value;
			panMainLeft = _PanMainLeft;
			if (panMainLeft != null)
			{
				panMainLeft.SizeChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("PanHint")]
	internal virtual StackPanel PanHint
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyExtraButton BtnExtraBack
	{
		[CompilerGenerated]
		get
		{
			return _BtnExtraBack;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyExtraButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				BackToTop();
			};
			MyExtraButton btnExtraBack = _BtnExtraBack;
			if (btnExtraBack != null)
			{
				btnExtraBack.Click -= obj;
			}
			_BtnExtraBack = value;
			btnExtraBack = _BtnExtraBack;
			if (btnExtraBack != null)
			{
				btnExtraBack.Click += obj;
			}
		}
	}

	internal virtual MyExtraButton BtnExtraDownload
	{
		[CompilerGenerated]
		get
		{
			return _BtnExtraDownload;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyExtraButton.ClickEventHandler obj = BtnExtraDownload_Click;
			MyExtraButton btnExtraDownload = _BtnExtraDownload;
			if (btnExtraDownload != null)
			{
				btnExtraDownload.Click -= obj;
			}
			_BtnExtraDownload = value;
			btnExtraDownload = _BtnExtraDownload;
			if (btnExtraDownload != null)
			{
				btnExtraDownload.Click += obj;
			}
		}
	}

	internal virtual MyExtraButton BtnExtraApril
	{
		[CompilerGenerated]
		get
		{
			return _BtnExtraApril;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyExtraButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				AprilGiveup();
			};
			MyExtraButton btnExtraApril = _BtnExtraApril;
			if (btnExtraApril != null)
			{
				btnExtraApril.Click -= obj;
			}
			_BtnExtraApril = value;
			btnExtraApril = _BtnExtraApril;
			if (btnExtraApril != null)
			{
				btnExtraApril.Click += obj;
			}
		}
	}

	internal virtual MyExtraButton BtnExtraShutdown
	{
		[CompilerGenerated]
		get
		{
			return _BtnExtraShutdown;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyExtraButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				BtnExtraShutdown_Click();
			};
			MyExtraButton btnExtraShutdown = _BtnExtraShutdown;
			if (btnExtraShutdown != null)
			{
				btnExtraShutdown.Click -= obj;
			}
			_BtnExtraShutdown = value;
			btnExtraShutdown = _BtnExtraShutdown;
			if (btnExtraShutdown != null)
			{
				btnExtraShutdown.Click += obj;
			}
		}
	}

	internal virtual MyExtraButton BtnExtraMusic
	{
		[CompilerGenerated]
		get
		{
			return _BtnExtraMusic;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyExtraButton.ClickEventHandler obj = BtnExtraMusic_Click;
			MyExtraButton.RightClickEventHandler obj2 = BtnExtraMusic_RightClick;
			MyExtraButton btnExtraMusic = _BtnExtraMusic;
			if (btnExtraMusic != null)
			{
				btnExtraMusic.Click -= obj;
				btnExtraMusic.RightClick -= obj2;
			}
			_BtnExtraMusic = value;
			btnExtraMusic = _BtnExtraMusic;
			if (btnExtraMusic != null)
			{
				btnExtraMusic.Click += obj;
				btnExtraMusic.RightClick += obj2;
			}
		}
	}

	internal virtual Grid PanMsg
	{
		[CompilerGenerated]
		get
		{
			return _PanMsg;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = FormDragMove;
			Grid panMsg = _PanMsg;
			if (panMsg != null)
			{
				panMsg.MouseLeftButtonDown -= value2;
			}
			_PanMsg = value;
			panMsg = _PanMsg;
			if (panMsg != null)
			{
				panMsg.MouseLeftButtonDown += value2;
			}
		}
	}

	private void ShowUpdateLog(int LastVersion)
	{
		_Closure_0024__1_002D0 arg = default(_Closure_0024__1_002D0);
		_Closure_0024__1_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__1_002D0(arg);
		int num = 0;
		int num2 = 0;
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		checked
		{
			if (LastVersion < 412)
			{
				list.Add(new KeyValuePair<int, string>(5, "新增：允许修改、添加、移除、更新已安装版本的 MC 版本以及各个组件，可从版本设置中进入"));
				list.Add(new KeyValuePair<int, string>(2, "优化：启动游戏时允许按 Esc 取消"));
				list.Add(new KeyValuePair<int, string>(2, "优化：允许复制主页的启动日志中的内容"));
				num += 19;
				num2 += 26;
			}
			if (LastVersion < 410)
			{
				if (LastVersion == 409)
				{
					list.Add(new KeyValuePair<int, string>(1, "修复：更新 Mod 时可能更新成另一个 Mod 加载器的版本"));
				}
				num += 2;
				num2 += 6;
			}
			if (LastVersion < 409)
			{
				list.Add(new KeyValuePair<int, string>(2, "优化：版本设置中，添加打开截图文件夹的快捷方式"));
				list.Add(new KeyValuePair<int, string>(2, "优化：导出整合包时，可以勾选导出光影设置文件"));
				num += 21;
				num2 += 22;
			}
			if (LastVersion < 407)
			{
				if (LastVersion == 405)
				{
					list.Add(new KeyValuePair<int, string>(1, "修复：使用部分主页预设时崩溃"));
					list.Add(new KeyValuePair<int, string>(1, "修复：无法删除部分文件夹"));
					list.Add(new KeyValuePair<int, string>(1, "修复：版本设置中指定的 Java 无法被保存"));
					num++;
					num2 += 3;
				}
				num += 2;
			}
			if (LastVersion < 405)
			{
				list.Add(new KeyValuePair<int, string>(4, "新增：重做 Java 管理与相关设置，允许调整 Java 优先级、指定 Java 版本范围等"));
				list.Add(new KeyValuePair<int, string>(3, "优化：导出整合包时允许自动导出版本文件夹中的 Java"));
				list.Add(new KeyValuePair<int, string>(3, "优化：重做弹出提示的样式以及动画，以更符合现代 UI 审美"));
				list.Add(new KeyValuePair<int, string>(1, "修复：可能无法安装 OptiFine 26.1.2"));
				num += 35;
				num2 += 17;
			}
			if (LastVersion < 403)
			{
				list.Add(new KeyValuePair<int, string>(1, "修复：Windows 7 无法正常联网"));
				num++;
				num2 += 2;
			}
			if (LastVersion < 401)
			{
				list.Add(new KeyValuePair<int, string>(1, "修复：可能无法更换披风"));
				num += 8;
				num2 += 6;
			}
			if (LastVersion < 399)
			{
				list.Add(new KeyValuePair<int, string>(2, "优化：升级框架到 .NET Framework 4.8"));
				num += 28;
				num2 += 19;
			}
			if (LastVersion < 397 && LastVersion >= 393 && LastVersion < 397)
			{
				list.Add(new KeyValuePair<int, string>(1, "修复：无法使用自定义隐藏主题"));
			}
			if (LastVersion < 395 && LastVersion == 393)
			{
				list.Add(new KeyValuePair<int, string>(2, "修复：部分个性化功能失效"));
			}
			if (LastVersion < 393)
			{
				list.Add(new KeyValuePair<int, string>(2, "优化：允许永久关闭启动页面的快照版提示"));
				list.Add(new KeyValuePair<int, string>(2, "优化：下载游戏、整合包的稳定性"));
				num++;
				num2 += 5;
			}
			if (LastVersion < 390)
			{
				list.Add(new KeyValuePair<int, string>(1, "修复：会为部分 Forge / NeoForge 选择不支持的 Java"));
				num2 += 4;
			}
			if (LastVersion < 388)
			{
				if (LastVersion == 386)
				{
					list.Add(new KeyValuePair<int, string>(2, "修复：无法使用 Java 25+ 启动 Forge"));
				}
				num += 4;
				num2 += 3;
			}
			if (LastVersion < 386)
			{
				list.Add(new KeyValuePair<int, string>(2, "修复：Minecraft 使用 Java 25+ 时的一个性能问题"));
				if (LastVersion == 384 || LastVersion == 385)
				{
					list.Add(new KeyValuePair<int, string>(2, "修复：无法正常重命名游戏版本"));
				}
				if (LastVersion == 385)
				{
					list.Add(new KeyValuePair<int, string>(1, "修复：无法显示 Modrinth 整合包的版本列表"));
				}
				num2++;
			}
			if (LastVersion < 385)
			{
				list.Add(new KeyValuePair<int, string>(2, "修复：无法下载 NeoForge 26.1"));
				if (LastVersion == 384)
				{
					list.Add(new KeyValuePair<int, string>(2, "修复：下载可能失败，提示任务管理刷新线程出错"));
					list.Add(new KeyValuePair<int, string>(2, "修复：无法保存选择的 Minecraft 文件夹"));
					list.Add(new KeyValuePair<int, string>(1, "修复：使用中文搜索 Mod 时，部分结果会忽略筛选条件"));
				}
				num += 3;
				num2 += 6;
			}
			if (LastVersion < 384)
			{
				list.Add(new KeyValuePair<int, string>(3, "优化：降低 Minecraft 的内存占用"));
				list.Add(new KeyValuePair<int, string>(2, "修复：部分关键词搜不到资源，特别是中文 Mod 搜索经常没有结果"));
				list.Add(new KeyValuePair<int, string>(1, "修复：老版本 Windows 无法启动游戏"));
				list.Add(new KeyValuePair<int, string>(1, "修复：连接 Mojang 的服务可能失败，提示错误码 421"));
				list.Add(new KeyValuePair<int, string>(1, "修复：无法访问爱发电"));
				num += 21;
				num2 += 14;
			}
			if (LastVersion < 382)
			{
				list.Add(new KeyValuePair<int, string>(2, "修复：下载可能完全卡住，或是下载进度反复回退"));
				list.Add(new KeyValuePair<int, string>(1, "新增：内存管理设置，可以选择 G1GC、ZGC 或分代 ZGC"));
				list.Add(new KeyValuePair<int, string>(1, "优化：来自 Modrinth 的文件下载速度"));
				num += 15;
				num2 += 16;
			}
			if (LastVersion < 380)
			{
				num += 3;
				num2 += 13;
			}
			if (LastVersion < 378 && LastVersion >= 377)
			{
				list.Add(new KeyValuePair<int, string>(1, "修复：在版本列表中存在 OptiFine 时可能无法加载版本列表"));
				num2 += 2;
			}
			if (LastVersion < 377)
			{
				if (LastVersion >= 373)
				{
					list.Add(new KeyValuePair<int, string>(5, "删除：暂时隐藏联机入口……不过只是暂时关闭，它还会回来的！"));
				}
				list.Add(new KeyValuePair<int, string>(3, "新增：适配新的 Minecraft 版本号系统与 Unobfuscated 版本"));
				list.Add(new KeyValuePair<int, string>(2, "优化：巨幅优化各个下载页面和 Mod 管理页面的性能"));
				list.Add(new KeyValuePair<int, string>(1, "优化：更换披风时会显示当前使用的披风"));
				num += 33;
				num2 += 29;
			}
			if (LastVersion < 375)
			{
				num += 16;
				num2 += 4;
			}
			if (LastVersion < 374)
			{
				num += 9;
				num2 += 7;
			}
			if (LastVersion < 373)
			{
				num += 7;
				num2 += 10;
			}
			List<string> list2 = new List<string>();
			List<KeyValuePair<int, string>> list3 = list.OrderByDescending([SpecialName] (KeyValuePair<int, string> f) => f.Key).ToList();
			if (!list3.Any() && num == 0 && num2 == 0)
			{
				list2.Add("没有更新日志……");
			}
			int num3 = Math.Min(9, list3.Count - 1);
			for (int i = 0; i <= num3; i++)
			{
				list2.Add(list3[i].Value);
			}
			if (list3.Count > 10)
			{
				num += list3.Count - 10;
			}
			if (num > 0 || num2 > 0)
			{
				list2.Add(((num > 0) ? (Conversions.ToString(num) + " 项小调整与修改") : "") + ((num > 0 && num2 > 0) ? "，" : "") + ((num2 > 0) ? ("修复了 " + Conversions.ToString(num2) + " 个 Bug") : "") + "，详见完整更新日志");
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Content = "· " + EnumerableExtensions.Join((IEnumerable)list2, "\r\n· ");
			ModBase.RunInNewThread([SpecialName] () =>
			{
				if (ModMain.MyMsgBox(CS_0024_003C_003E8__locals0._0024VB_0024Local_Content, "PCL 已更新至快照版 2.14.0.0", "确定", "完整更新日志") == 2)
				{
					ModBase.OpenWebsite("https://meloong.com/afd/a/LTCat?tab=feed");
				}
			}, "UpdateLog Output");
		}
	}

	public FormMain()
	{
		base.Loaded += FormMain_Loaded;
		base.Closing += FormMain_Closing;
		base.SizeChanged += [SpecialName] (object a0, SizeChangedEventArgs a1) =>
		{
			FormMain_SizeChanged();
		};
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			FormMain_SizeChanged();
		};
		base.KeyDown += FormMain_KeyDown;
		base.MouseDown += FormMain_MouseDown;
		base.Activated += [SpecialName] (object a0, EventArgs a1) =>
		{
			FormMain_Activated();
		};
		base.Drop += FrmMain_Drop;
		base.MouseMove += FormMain_MouseMove;
		IsWindowLoadFinished = false;
		IsSizeSaveable = false;
		IsSystemTimeChanged = false;
		_Hidden = false;
		PageCurrent = PageType.Launch;
		PageLast = PageType.Launch;
		PageStack = new List<PageStackData>();
		IsChangingPage = false;
		lastMouseArg = null;
		ModBase.ApplicationStartTick = ModBase.GetTimeMs();
		ModMain.FrmMain = this;
		ModMain.FrmLaunchLeft = new PageLaunchLeft();
		ModMain.FrmLaunchRight = new PageLaunchRight();
		int num = Settings.Get<int>("SystemLastVersionReg");
		if (num < 412)
		{
			UpgradeSub(num);
		}
		else if (num > 412)
		{
			DowngradeSub(num);
		}
		if (!Settings.HasSaved("LaunchArgumentIndieV2"))
		{
			if (Settings.HasSaved("LaunchArgumentIndie"))
			{
				Logger.Info("从老 PCL 迁移版本隔离", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				Settings.Set("LaunchArgumentIndieV2", Settings.Get<int>("LaunchArgumentIndie"));
			}
			else if (ModBase.HasIniKey("Setup", "LaunchVersionSelect"))
			{
				Logger.Info("从老 PCL 升级，但此前未调整版本隔离，使用老的版本隔离默认值", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				Settings.Set("LaunchArgumentIndieV2", Settings.GetDefault("LaunchArgumentIndie"));
			}
			else
			{
				Logger.Info("全新的 PCL，使用新的版本隔离默认值", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				Settings.Set("LaunchArgumentIndieV2", Settings.GetDefault("LaunchArgumentIndieV2"));
			}
		}
		ModSecret.ThemeCheckAll(EffectSetup: false);
		ModSecret.ThemeRefresh(Settings.Get<int>("UiLauncherTheme"));
		AddHandler(DragDrop.DragEnterEvent, new DragEventHandler(HandleDrag), handledEventsToo: true);
		AddHandler(DragDrop.DragOverEvent, new DragEventHandler(HandleDrag), handledEventsToo: true);
		InitializeComponent();
		base.Opacity = 0.0;
		if (WindowsUtils.HasAdminRole())
		{
			if (_0024STATIC_0024_002Ector_00242001_0024Helper_0024Init == null)
			{
				Interlocked.CompareExchange(ref _0024STATIC_0024_002Ector_00242001_0024Helper_0024Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_0024STATIC_0024_002Ector_00242001_0024Helper_0024Init, ref lockTaken);
				if (_0024STATIC_0024_002Ector_00242001_0024Helper_0024Init.State == 0)
				{
					_0024STATIC_0024_002Ector_00242001_0024Helper_0024Init.State = 2;
					_0024STATIC_0024_002Ector_00242001_0024Helper = new DragHelper();
				}
				else if (_0024STATIC_0024_002Ector_00242001_0024Helper_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024_002Ector_00242001_0024Helper_0024Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_0024STATIC_0024_002Ector_00242001_0024Helper_0024Init);
				}
			}
			base.SourceInitialized += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__3_002D0();
			};
			base.Closing += [SpecialName] (object a0, CancelEventArgs a1) =>
			{
				_Lambda_0024__3_002D1();
			};
			_0024STATIC_0024_002Ector_00242001_0024Helper.DragDrop += [SpecialName] (object a0, EventArgs a1) =>
			{
				_Lambda_0024__3_002D2();
			};
		}
		if (!Information.IsNothing(ModMain.FrmLaunchLeft.Parent))
		{
			ModMain.FrmLaunchLeft.SetValue(ContentPresenter.ContentProperty, null);
		}
		if (!Information.IsNothing(ModMain.FrmLaunchRight.Parent))
		{
			ModMain.FrmLaunchRight.SetValue(ContentPresenter.ContentProperty, null);
		}
		PanMainLeft.Child = ModMain.FrmLaunchLeft;
		PageLeft = ModMain.FrmLaunchLeft;
		PanMainRight.Child = ModMain.FrmLaunchRight;
		PageRight = ModMain.FrmLaunchRight;
		ModMain.FrmLaunchRight.PageState = MyPageRight.PageStates.ContentStay;
		if (ModBase.ModeDebug)
		{
			ModMain.Hint("[调试模式] PCL 正以调试模式运行，这可能会导致性能下降，若无必要请不要开启！");
		}
		ModMinecraft.McFolderListLoader.Start(0);
		Logger.Info($"第二阶段加载用时：{checked(ModBase.GetTimeMs() - ModBase.ApplicationStartTick)} ms", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
	}

	private void FormMain_Loaded(object sender, RoutedEventArgs e)
	{
		ModBase.ApplicationStartTick = ModBase.GetTimeMs();
		ModBase.Handle = new WindowInteropHelper(this).Handle;
		UpdateBackgroundAndTitleBar();
		PageSetupUI.HiddenRefresh();
		PageSetupUI.BackgroundRefresh(IsHint: false, Refresh: true);
		ModMusic.MusicRefreshPlay(ShowHint: false, IsFirstLoad: true);
		BtnExtraDownload.ShowCheck = BtnExtraDownload_ShowCheck;
		BtnExtraBack.ShowCheck = BtnExtraBack_ShowCheck;
		BtnExtraApril.ShowCheck = BtnExtraApril_ShowCheck;
		BtnExtraShutdown.ShowCheck = BtnExtraShutdown_ShowCheck;
		BtnExtraApril.ShowRefresh();
		MyResizer myResizer = new MyResizer(this);
		myResizer.addResizerDown(ResizerB);
		myResizer.addResizerLeft(ResizerL);
		myResizer.addResizerLeftDown(ResizerLB);
		myResizer.addResizerLeftUp(ResizerLT);
		myResizer.addResizerRight(ResizerR);
		myResizer.addResizerRightDown(ResizerRB);
		myResizer.addResizerRightUp(ResizerRT);
		myResizer.addResizerUp(ResizerT);
		if (ModBase.RandomInteger(1, 1000) == 233)
		{
			ShapeTitleLogo.Data = (Geometry)new GeometryConverter().ConvertFromString("M26,29 v-25 h6 a7,7 180 0 1 0,14 h-6 M83,6.5 a10,11.5 180 1 0 0,18 M48,2.5 v24.5 h13.5");
		}
		ModSecret.ThemeRefreshMain();
		try
		{
			base.Height = Settings.Get<int>("WindowHeight");
			base.Width = Settings.Get<int>("WindowWidth");
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "读取窗口默认大小失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			base.Height = base.MinHeight + 100.0;
			base.Width = base.MinWidth + 100.0;
			ProjectData.ClearProjectError();
		}
		base.Topmost = false;
		checked
		{
			if (ModMain.FrmStart != null)
			{
				ModMain.FrmStart.Close(new TimeSpan(0, 0, 0, 0, (int)Math.Round(400.0 / ModAnimation.AniSpeed)));
			}
			base.Top = (ModBase.GetWPFSize(MyWpfExtension.Computer.Screen.WorkingArea.Height) - base.Height) / 2.0;
			base.Left = (ModBase.GetWPFSize(MyWpfExtension.Computer.Screen.WorkingArea.Width) - base.Width) / 2.0;
			IsSizeSaveable = true;
			ShowWindowToTop();
			((HwndSource)PresentationSource.FromVisual(this)).AddHook(WndProc);
			ModAnimation.AniStart(new ModAnimation.AniData[5]
			{
				ModAnimation.AaCode([SpecialName] () =>
				{
					ModAnimation.AniControlEnabled--;
				}, 50),
				ModAnimation.AaOpacity(this, (double)Settings.Get<int>("UiLauncherTransparent") / 1000.0 + 0.4, 250, 100),
				ModAnimation.AaDouble([SpecialName] (object i) =>
				{
					TranslateTransform transformPos;
					(transformPos = TransformPos).Y = Conversions.ToDouble(Operators.AddObject(transformPos.Y, i));
				}, 0.0 - TransformPos.Y, 600, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaDouble([SpecialName] (object i) =>
				{
					RotateTransform transformRotate;
					(transformRotate = TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(transformRotate.Angle, i));
				}, 0.0 - TransformRotate.Angle, 500, 100, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PanBack.RenderTransform = null;
					IsWindowLoadFinished = true;
					Logger.Info($"DPI：{ModBase.DPI}，系统版本：{Environment.OSVersion.VersionString}，PCL 位置：{ModBase.PathExe}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				}, 0, After: true)
			}, "Form Show");
			ModAnimation.AniStart();
			ModMain.TimerMainStart();
			ModBase.RunInNewThread([SpecialName] () =>
			{
				if (Settings.Get<int>("SystemEulaVersion") < 2)
				{
					switch (ModMain.MyMsgBox((Settings.Get<int>("SystemEulaVersion") == 0) ? "在使用 PCL 前，请先阅读用户协议与免责声明。" : string.Format("PCL 的用户协议与免责声明已更新。{0}请阅读更新后的用户协议与免责声明。", "\r\n"), "协议授权", "同意", "拒绝", "查看用户协议与免责声明", IsWarn: false, HighLight: true, ForceWait: false, null, null, [SpecialName] () =>
					{
						ModBase.OpenWebsite("https://shimo.im/docs/rGrd8pY8xWkt6ryW");
					}))
					{
					case 1:
						Settings.Set("SystemEulaVersion", 2);
						break;
					case 2:
						EndProgram(SendWarning: false);
						break;
					}
				}
				try
				{
					ModJava.JavaInit();
					Thread.Sleep(100);
					ModDownload.DlClientListMojangLoader.Start(1);
					RunCountSub();
					ModSecret.ServerLoader.Start();
					ModBase.RunInNewThread(ModMain.TryClearTaskTemp, "TryClearTaskTemp", ThreadPriority.BelowNormal);
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Error(ex4, "初始化加载池运行失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					ProjectData.ClearProjectError();
				}
				try
				{
					FileUtils.Delete(Paths.Base + "PCL\\Plain Craft Launcher 2.exe", false);
				}
				catch (Exception ex5)
				{
					ProjectData.SetProjectError(ex5);
					Exception ex6 = ex5;
					Logger.Warn(ex6, "清理自动更新文件失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					ProjectData.ClearProjectError();
				}
				ModMain.Telemetry("启动");
			}, "初始化", ThreadPriority.Lowest);
			Logger.Info($"第三阶段加载用时：{ModBase.GetTimeMs() - ModBase.ApplicationStartTick} ms", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
	}

	private void RunCountSub()
	{
		Settings.Set("SystemCount", checked(Settings.Get<int>("SystemCount") + 1));
		int num = Settings.Get<int>("SystemCount");
		if (num == 1)
		{
			ModMain.MyMsgBox("欢迎使用 PCL 快照版！\r\n快照版包含尚未在正式版发布的测试性功能，仅用于赞助者本人尝鲜。所以请不要发给其他人或者用于制作整合包哦！\r\n如果你并非通过赞助或赞助者本人邀请进群获得的本程序，那么可能是有人在违规传播，记得提醒他一下啦。", "快照版使用说明");
		}
		if (Settings.Get<int>("SystemCount") >= 99 && ModSecret.ThemeUnlock(6, ShowDoubleHint: false))
		{
			ModMain.MyMsgBox("你已经使用了 99 次 PCL 啦，感谢你长期以来的支持！\r\n隐藏主题 铁杆粉 已解锁！");
		}
	}

	private void UpgradeSub(int LastVersionCode)
	{
		Logger.Info($"版本号从 {LastVersionCode} 升高到 {412}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		Settings.Set("SystemLastVersionReg", 412);
		int num = Settings.Get<int>("SystemHighestAlphaVersionReg");
		if (num < 412)
		{
			Settings.Set("SystemHighestAlphaVersionReg", 412);
			Logger.Info($"最高版本号从 {num} 升高到 {412}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
		if (Settings.Get<int>("LaunchArgumentWindowType") == 5)
		{
			Settings.Set("LaunchArgumentWindowType", 1);
		}
		if (num <= 207)
		{
			List<string> list = new List<string> { "2" };
			list.AddRange(new List<string>(StringExtensions.Split(Settings.Get<string>("UiLauncherThemeHide").ToString(), "|", false)));
			list.AddRange(new List<string>(StringExtensions.Split(Settings.Get<string>("UiLauncherThemeHide2").ToString(), "|", false)));
			Settings.Set("UiLauncherThemeHide2", EnumerableExtensions.Join((IEnumerable)list.Distinct().ToList(), '|'));
		}
		if (LastVersionCode <= 115 && StringExtensions.Split(Settings.Get<string>("UiLauncherThemeHide2").ToString(), "|", false).Contains("13"))
		{
			List<string> list2 = new List<string>(StringExtensions.Split(Settings.Get<string>("UiLauncherThemeHide2").ToString(), "|", false));
			list2.Remove("13");
			Settings.Set("UiLauncherThemeHide2", EnumerableExtensions.Join((IEnumerable)list2, '|'));
			ModMain.MyMsgBox("由于新版 PCL 修改了欧皇彩的解锁方式，你需要重新解锁欧皇彩。\r\n多谢各位的理解啦！", "重新解锁提醒");
		}
		if (LastVersionCode <= 152 && StringExtensions.Split(Settings.Get<string>("UiLauncherThemeHide2").ToString(), "|", false).Contains("12"))
		{
			List<string> list3 = new List<string>(StringExtensions.Split(Settings.Get<string>("UiLauncherThemeHide2").ToString(), "|", false));
			list3.Remove("12");
			Settings.Set("UiLauncherThemeHide2", EnumerableExtensions.Join((IEnumerable)list3, '|'));
			ModMain.MyMsgBox("由于新版 PCL 修改了滑稽彩的解锁方式，你需要重新解锁滑稽彩。\r\n多谢各位的理解啦！", "重新解锁提醒");
		}
		if (LastVersionCode <= 161 && FileUtils.Exists(Paths.Base + "PCL\\CustomSkin.png") && !FileUtils.Exists(Paths.AppDataThenName + "CustomSkin.png"))
		{
			FileUtils.Copy(Paths.Base + "PCL\\CustomSkin.png", Paths.AppDataThenName + "CustomSkin.png");
			Logger.Info("已移动离线自定义皮肤 (162)", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
		if (LastVersionCode <= 263 && FileUtils.Exists(ModBase.PathTemp + "CustomSkin.png") && !FileUtils.Exists(Paths.AppDataThenName + "CustomSkin.png"))
		{
			FileUtils.Copy(ModBase.PathTemp + "CustomSkin.png", Paths.AppDataThenName + "CustomSkin.png");
			Logger.Info("已移动离线自定义皮肤 (264)", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
		if (LastVersionCode <= 205)
		{
			Settings.Set("UiHiddenOtherHelp", false);
			Logger.Info("已解除帮助页面的隐藏", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
		if (!Settings.Get<bool>("CacheMsV2Migrated"))
		{
			Settings.Set("CacheMsV2Migrated", true);
			Settings.Set("CacheMsV2OAuthRefresh", Settings.Get<string>("CacheMsOAuthRefresh"));
			Settings.Set("CacheMsV2Access", Settings.Get<string>("CacheMsAccess"));
			Settings.Set("CacheMsV2ProfileJson", Settings.Get<string>("CacheMsProfileJson"));
			Settings.Set("CacheMsV2Uuid", Settings.Get<string>("CacheMsUuid"));
			Settings.Set("CacheMsV2Name", Settings.Get<string>("CacheMsName"));
			Logger.Info("已从老版本迁移微软登录结果", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
		if (Settings.HasSaved("ToolDownloadTranslate") && !Settings.HasSaved("ToolDownloadTranslateV2"))
		{
			Settings.Set("ToolDownloadTranslateV2", checked(Settings.Get<int>("ToolDownloadTranslate") + 1));
			Logger.Info("已从老版本迁移 Mod 命名设置", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
		if (LastVersionCode <= 381 && Settings.HasSaved("LaunchAdvanceJvm") && Operators.CompareString(Settings.Get<string>("LaunchAdvanceJvm").ToString().Replace("-XX:+UseG1GC ", "")
			.Replace("-XX:-UseAdaptiveSizePolicy ", "")
			.Trim(), Settings.GetDefault("LaunchAdvanceJvm"), TextCompare: false) == 0)
		{
			Settings.Reset("LaunchAdvanceJvm");
			Logger.Info("已重置 JVM 参数设置", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
		if (LastVersionCode > 0 && num < 412)
		{
			ShowUpdateLog(num);
		}
	}

	private void DowngradeSub(int LastVersionCode)
	{
		Logger.Info($"版本号从 {LastVersionCode} 降低到 {412}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		Settings.Set("SystemLastVersionReg", 412);
	}

	private void FormMain_Closing(object sender, CancelEventArgs e)
	{
		EndProgram(SendWarning: true);
		e.Cancel = true;
	}

	public void EndProgram(bool SendWarning)
	{
		if (ModNet.HasDownloadingTask())
		{
			if (SendWarning && ModMain.MyMsgBox("还有下载任务尚未完成，是否确定退出？", "提示", "确定", "取消") == 2)
			{
				return;
			}
			ModBase.RunInNewThread([SpecialName] () =>
			{
				Logger.Info("正在强行停止任务", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				foreach (ModLoader.LoaderBase item in ((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).ToList())
				{
					item.Cancel();
				}
			}, "强行停止下载任务");
		}
		PageLinkMain frmLinkMain = ModMain.FrmLinkMain;
		if (frmLinkMain != null && frmLinkMain.TryExit(!SendWarning, Closing: true))
		{
			return;
		}
		ModBase.RunInUiWait([SpecialName] () =>
		{
			base.IsHitTestVisible = false;
			if (PanBack.RenderTransform == null)
			{
				_Closure_0024__9_002D0 arg = default(_Closure_0024__9_002D0);
				_Closure_0024__9_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__9_002D0(arg);
				CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformPos = new TranslateTransform(0.0, 0.0);
				CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformRotate = new RotateTransform(0.0);
				CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformScale = new ScaleTransform(1.0, 1.0);
				PanBack.RenderTransform = new TransformGroup
				{
					Children = new TransformCollection(new Transform[3] { CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformRotate, CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformPos, CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformScale })
				};
				ModAnimation.AniStart(new ModAnimation.AniData[6]
				{
					ModAnimation.AaOpacity(this, 0.0 - base.Opacity, 140, 40, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaDouble([SpecialName] (object i) =>
					{
						ScaleTransform _0024VB_0024Local_TransformScale;
						(_0024VB_0024Local_TransformScale = CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformScale).ScaleX = Conversions.ToDouble(Operators.AddObject(_0024VB_0024Local_TransformScale.ScaleX, i));
						(_0024VB_0024Local_TransformScale = CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformScale).ScaleY = Conversions.ToDouble(Operators.AddObject(_0024VB_0024Local_TransformScale.ScaleY, i));
					}, 0.88 - CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformScale.ScaleX, 180),
					ModAnimation.AaDouble([SpecialName] (object i) =>
					{
						TranslateTransform _0024VB_0024Local_TransformPos;
						(_0024VB_0024Local_TransformPos = CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformPos).Y = Conversions.ToDouble(Operators.AddObject(_0024VB_0024Local_TransformPos.Y, i));
					}, 20.0 - CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformPos.Y, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaDouble([SpecialName] (object i) =>
					{
						RotateTransform _0024VB_0024Local_TransformRotate;
						(_0024VB_0024Local_TransformRotate = CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformRotate).Angle = Conversions.ToDouble(Operators.AddObject(_0024VB_0024Local_TransformRotate.Angle, i));
					}, 0.6 - CS_0024_003C_003E8__locals0._0024VB_0024Local_TransformRotate.Angle, 180, 0, new ModAnimation.AniEaseInoutFluent(ModAnimation.AniEasePower.Weak)),
					ModAnimation.AaCode([SpecialName] () =>
					{
						base.IsHitTestVisible = false;
						base.Top = -10000.0;
						base.ShowInTaskbar = false;
					}, 210),
					ModAnimation.AaCode([SpecialName] () =>
					{
						EndProgramForce();
					}, 230)
				}, "Form Close");
			}
			else
			{
				EndProgramForce();
			}
			Logger.Info("收到关闭指令", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		});
	}

	public static void EndProgramForce(ModBase.ProcessReturnValues ReturnCode = ModBase.ProcessReturnValues.Success)
	{
		int try0000_dispatch = -1;
		int num3 = default(int);
		int num2 = default(int);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				checked
				{
					switch (try0000_dispatch)
					{
					default:
						ProjectData.ClearProjectError();
						num3 = 1;
						goto IL_0007;
					case 314:
						{
							num2 = num;
							switch (num3)
							{
							case 1:
								break;
							default:
								goto end_IL_0000;
							}
							int num4 = unchecked(num2 + 1);
							num2 = 0;
							switch (num4)
							{
							case 1:
								break;
							case 2:
								goto IL_0007;
							case 3:
								goto IL_000f;
							case 4:
								goto IL_001d;
							case 5:
								goto IL_0026;
							case 6:
								goto IL_002e;
							case 7:
								goto IL_0034;
							case 8:
								goto IL_003d;
							case 9:
								goto IL_0044;
							case 10:
								goto IL_0057;
							case 11:
								goto IL_0060;
							case 12:
								goto IL_007d;
							case 13:
								goto IL_008a;
							case 14:
								goto IL_00a8;
							case 15:
								goto IL_00b0;
							case 16:
								goto IL_00bd;
							case 17:
								goto IL_00c3;
							case 18:
								goto end_IL_0000_2;
							default:
								goto end_IL_0000;
							case 19:
								goto end_IL_0000_3;
							}
							goto default;
						}
						IL_00c3:
						num = 17;
						Environment.Exit(unchecked((int)ReturnCode));
						break;
						IL_0007:
						num = 2;
						ModBase.IsProgramEnding = true;
						goto IL_000f;
						IL_000f:
						num = 3;
						ModAnimation.AniControlEnabled++;
						goto IL_001d;
						IL_001d:
						num = 4;
						if (ModSecret.IsUpdateWaitingRestart)
						{
							goto IL_0026;
						}
						goto IL_002e;
						IL_0026:
						num = 5;
						ModSecret.UpdateRestart(TriggerRestartAndByEnd: false);
						goto IL_002e;
						IL_002e:
						num = 6;
						if (ReturnCode == ModBase.ProcessReturnValues.Exception)
						{
							goto IL_0034;
						}
						goto IL_008a;
						IL_0034:
						num = 7;
						if (!IsLogShown)
						{
							goto IL_003d;
						}
						goto IL_007d;
						IL_003d:
						num = 8;
						ModMain.FeedbackInfo();
						goto IL_0044;
						IL_0044:
						num = 9;
						Logger.Info("请在 https://github.com/Meloong-Git/PCL/issues 提交错误报告，以便于作者解决此问题！", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						goto IL_0057;
						IL_0057:
						num = 10;
						IsLogShown = true;
						goto IL_0060;
						IL_0060:
						num = 11;
						ModBase.StartProcess(Paths.Base + "PCL\\Log1.txt");
						goto IL_007d;
						IL_007d:
						num = 12;
						Thread.Sleep(500);
						goto IL_008a;
						IL_008a:
						num = 13;
						Logger.Info($"程序已退出，返回值：{ReturnCode}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						goto IL_00a8;
						IL_00a8:
						num = 14;
						ConfigUtils.SaveAll();
						goto IL_00b0;
						IL_00b0:
						num = 15;
						Logger.Instance.Flush();
						goto IL_00bd;
						IL_00bd:
						num = 16;
						if (ReturnCode == ModBase.ProcessReturnValues.Success)
						{
							break;
						}
						goto IL_00c3;
						end_IL_0000_2:
						break;
					}
					num = 18;
					Process.GetCurrentProcess().Kill();
					break;
				}
				end_IL_0000:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num2 == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0000_dispatch = 314;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0000_3:
			break;
		}
		if (num2 != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void BtnTitleClose_Click(object sender, RoutedEventArgs e)
	{
		EndProgram(SendWarning: true);
	}

	private void FormDragMove(object sender, MouseButtonEventArgs e)
	{
		int try0000_dispatch = -1;
		int num3 = default(int);
		int num2 = default(int);
		int num = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try0000_dispatch)
				{
				default:
					ProjectData.ClearProjectError();
					num3 = 1;
					goto IL_0007;
				case 79:
					{
						num2 = num;
						switch (num3)
						{
						case 1:
							break;
						default:
							goto end_IL_0000;
						}
						int num4 = num2 + 1;
						num2 = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_0007;
						case 3:
							goto end_IL_0000_2;
						default:
							goto end_IL_0000;
						case 4:
							goto end_IL_0000_3;
						}
						goto default;
					}
					IL_0007:
					num = 2;
					if (!Conversions.ToBoolean(NewLateBinding.LateGet(sender, null, "IsMouseDirectlyOver", new object[0], null, null, null)))
					{
						goto end_IL_0000_3;
					}
					break;
					end_IL_0000_2:
					break;
				}
				num = 3;
				DragMove();
				break;
				end_IL_0000:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num2 == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0000_dispatch = 79;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_0000_3:
			break;
		}
		if (num2 != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private void FormMain_SizeChanged()
	{
		if (IsSizeSaveable)
		{
			Settings.Set("WindowHeight", base.Height);
			Settings.Set("WindowWidth", base.Width);
		}
		RectForm.Rect = new Rect(0.0, 0.0, BorderForm.ActualWidth, BorderForm.ActualHeight);
		PanForm.Width = BorderForm.ActualWidth + 0.001;
		PanForm.Height = BorderForm.ActualHeight + 0.001;
		PanMain.Width = PanForm.Width;
		PanMain.Height = Math.Max(0.0, PanForm.Height - PanTitle.ActualHeight);
		if (base.WindowState == WindowState.Maximized)
		{
			base.WindowState = WindowState.Normal;
		}
	}

	private void BtnTitleMin_Click()
	{
		base.WindowState = WindowState.Minimized;
	}

	public static void UpdateBackgroundAndTitleBar(object Value)
	{
		if (ModMain.FrmMain != null && ModMain.FrmMain.IsLoaded)
		{
			ModMain.FrmMain.UpdateBackgroundAndTitleBar();
		}
	}

	public void UpdateBackgroundAndTitleBar()
	{
		Logger.Info("从设置更新背景图片与标题栏样式", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		ImgBack.Opacity = (double)Settings.Get<int>("UiBackgroundOpacity") / 1000.0;
		double num = checked(Settings.Get<int>("UiBackgroundBlur") + 1);
		if (num == 1.0)
		{
			ImgBack.Effect = null;
		}
		else
		{
			ImgBack.Effect = new BlurEffect
			{
				Radius = num
			};
		}
		ImgBack.Margin = new Thickness((0.0 - num) / 1.8);
		int num2 = Settings.Get<int>("UiBackgroundSuit");
		if (ImgBack.Background != null)
		{
			ImageBrush imageBrush = (ImageBrush)ImgBack.Background;
			if (num2 == 0)
			{
				num2 = ((!(imageBrush.ImageSource.Width < PanMain.ActualWidth / 2.0) || !(imageBrush.ImageSource.Height < PanMain.ActualHeight / 2.0)) ? 2 : 4);
			}
			imageBrush.Stretch = Stretch.UniformToFill;
			imageBrush.TileMode = TileMode.None;
			imageBrush.Viewport = new Rect(0.0, 0.0, 1.0, 1.0);
			imageBrush.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
			imageBrush.AlignmentX = AlignmentX.Center;
			imageBrush.AlignmentY = AlignmentY.Center;
			switch (num2)
			{
			case 1:
				imageBrush.Stretch = Stretch.None;
				break;
			case 3:
				imageBrush.Stretch = Stretch.Fill;
				break;
			case 4:
				imageBrush.Stretch = Stretch.None;
				imageBrush.TileMode = TileMode.Tile;
				imageBrush.Viewport = new Rect(0.0, 0.0, imageBrush.ImageSource.Width, imageBrush.ImageSource.Height);
				imageBrush.ViewportUnits = BrushMappingMode.Absolute;
				break;
			case 5:
				imageBrush.AlignmentX = AlignmentX.Left;
				imageBrush.AlignmentY = AlignmentY.Top;
				break;
			case 6:
				imageBrush.AlignmentX = AlignmentX.Right;
				imageBrush.AlignmentY = AlignmentY.Top;
				break;
			case 7:
				imageBrush.AlignmentX = AlignmentX.Left;
				imageBrush.AlignmentY = AlignmentY.Bottom;
				break;
			case 8:
				imageBrush.AlignmentX = AlignmentX.Right;
				imageBrush.AlignmentY = AlignmentY.Bottom;
				break;
			case 9:
				imageBrush.AlignmentX = AlignmentX.Left;
				imageBrush.AlignmentY = AlignmentY.Center;
				break;
			case 10:
				imageBrush.AlignmentX = AlignmentX.Right;
				imageBrush.AlignmentY = AlignmentY.Center;
				break;
			case 11:
				imageBrush.AlignmentX = AlignmentX.Center;
				imageBrush.AlignmentY = AlignmentY.Top;
				break;
			case 12:
				imageBrush.AlignmentX = AlignmentX.Center;
				imageBrush.AlignmentY = AlignmentY.Bottom;
				break;
			}
		}
		switch (Settings.Get<int>("UiLogoType"))
		{
		case 0:
			ShapeTitleLogo.Visibility = Visibility.Collapsed;
			LabTitleLogo.Visibility = Visibility.Collapsed;
			ImageTitleLogo.Visibility = Visibility.Collapsed;
			if (!Information.IsNothing(ModMain.FrmSetupUI))
			{
				ModMain.FrmSetupUI.CheckLogoLeft.Visibility = Visibility.Visible;
				ModMain.FrmSetupUI.PanLogoText.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanLogoChange.Visibility = Visibility.Collapsed;
			}
			break;
		case 1:
			ShapeTitleLogo.Visibility = Visibility.Visible;
			LabTitleLogo.Visibility = Visibility.Collapsed;
			ImageTitleLogo.Visibility = Visibility.Collapsed;
			if (!Information.IsNothing(ModMain.FrmSetupUI))
			{
				ModMain.FrmSetupUI.CheckLogoLeft.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanLogoText.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanLogoChange.Visibility = Visibility.Collapsed;
			}
			break;
		case 2:
			ShapeTitleLogo.Visibility = Visibility.Collapsed;
			LabTitleLogo.Visibility = Visibility.Visible;
			ImageTitleLogo.Visibility = Visibility.Collapsed;
			if (!Information.IsNothing(ModMain.FrmSetupUI))
			{
				ModMain.FrmSetupUI.CheckLogoLeft.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanLogoText.Visibility = Visibility.Visible;
				ModMain.FrmSetupUI.PanLogoChange.Visibility = Visibility.Collapsed;
			}
			break;
		case 3:
			ShapeTitleLogo.Visibility = Visibility.Collapsed;
			LabTitleLogo.Visibility = Visibility.Collapsed;
			ImageTitleLogo.Visibility = Visibility.Visible;
			if (!Information.IsNothing(ModMain.FrmSetupUI))
			{
				ModMain.FrmSetupUI.CheckLogoLeft.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanLogoText.Visibility = Visibility.Collapsed;
				ModMain.FrmSetupUI.PanLogoChange.Visibility = Visibility.Visible;
			}
			try
			{
				ImageTitleLogo.Source = Paths.Base + "PCL\\Logo.png";
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ImageTitleLogo.Source = null;
				Logger.Error(ex2, "显示标题栏图片失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				ProjectData.ClearProjectError();
			}
			break;
		}
		ModMain.FrmSetupUI?.CardLogo.TriggerForceResize();
		LabTitleLogo.Text = Settings.Get<string>("UiLogoText");
		PanTitleMain.ColumnDefinitions[0].Width = new GridLength((!Settings.Get<bool>("UiLogoLeft") || Settings.Get<int>("UiLogoType") != 0) ? 1 : 0, GridUnitType.Star);
	}

	private void FormMain_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.IsRepeat)
		{
			return;
		}
		if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
		{
			e.Handled = true;
		}
		if (WpfExtensions.Any(PanMsg.Children))
		{
			if (e.Key == Key.Return)
			{
				NewLateBinding.LateCall(PanMsg.Children[0], null, "Btn1_Click", new object[0], null, null, null, IgnoreReturn: true);
			}
			else if (e.Key == Key.Escape)
			{
				object obj = PanMsg.Children[0];
				if (!(obj is MyMsgInput) && !(obj is MyMsgSelect) && Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(obj, null, "Btn3", new object[0], null, null, null), null, "Visibility", new object[0], null, null, null), Visibility.Visible, TextCompare: false))
				{
					NewLateBinding.LateCall(obj, null, "Btn3_Click", new object[0], null, null, null, IgnoreReturn: true);
				}
				else if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(obj, null, "Btn2", new object[0], null, null, null), null, "Visibility", new object[0], null, null, null), Visibility.Visible, TextCompare: false))
				{
					NewLateBinding.LateCall(obj, null, "Btn2_Click", new object[0], null, null, null, IgnoreReturn: true);
				}
				else
				{
					NewLateBinding.LateCall(obj, null, "Btn1_Click", new object[0], null, null, null, IgnoreReturn: true);
				}
			}
			return;
		}
		if (e.Key == Key.Escape)
		{
			if (PageCurrent == PageType.Launch && ModLaunch.McLaunchLoader.State == ModBase.LoadState.Loading)
			{
				ModMain.FrmLaunchLeft.BtnCancel_Click();
				return;
			}
			TriggerPageBack();
		}
		if (e.Key == Key.F11 && PageCurrent == PageType.InstanceSelect)
		{
			ModMain.FrmSelectRight.ShowHidden = !ModMain.FrmSelectRight.ShowHidden;
			ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
		}
		else if (e.Key == Key.F12)
		{
			PageSetupUI.HiddenForceShow = !PageSetupUI.HiddenForceShow;
			if (PageSetupUI.HiddenForceShow)
			{
				ModMain.Hint("功能隐藏设置已暂时关闭！", ModMain.HintType.Green);
			}
			else
			{
				ModMain.Hint("功能隐藏设置已重新开启！", ModMain.HintType.Green);
			}
			PageSetupUI.HiddenRefresh();
		}
		else if (e.Key == Key.F5)
		{
			if (PageLeft is IRefreshable)
			{
				((IRefreshable)PageLeft).Refresh();
			}
			if (PageRight is IRefreshable)
			{
				((IRefreshable)PageRight).Refresh();
			}
		}
		else if (e.Key == Key.Return && PageCurrent == PageType.Launch)
		{
			if (ModMain.IsAprilEnabled && !ModMain.IsAprilGiveup)
			{
				ModMain.Hint("木大！");
			}
			else
			{
				ModMain.FrmLaunchLeft.LaunchButtonClick();
			}
		}
	}

	private void FormMain_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (ModMain.FrmMain.PanMsg.Children.Count <= 0 && !ModMain.WaitingMyMsgBox.Any() && (e.ChangedButton == MouseButton.XButton1 || e.ChangedButton == MouseButton.XButton2))
		{
			TriggerPageBack();
		}
	}

	private void TriggerPageBack()
	{
		if (PageCurrent == PageType.Download && PageCurrentSub == PageSubType.Default && ModMain.FrmDownloadInstall.IsInSelectPage)
		{
			ModMain.FrmDownloadInstall.ExitSelectPage();
		}
		else
		{
			PageBack();
		}
	}

	private void FormMain_Activated()
	{
		_Closure_0024__22_002D0 closure_0024__22_002D = new _Closure_0024__22_002D0();
		try
		{
			if (PageCurrent == PageType.InstanceSetup && PageCurrentSub == PageSubType.DownloadPack)
			{
				ModMain.FrmInstanceMod.ReloadModList();
			}
			else if (PageCurrent == PageType.InstanceSetup && (PageCurrentSub == PageSubType.SetupLink || PageCurrentSub == PageSubType.DownloadResourcePack))
			{
				PageInstanceLeft.ReloadCurrentJava();
			}
			else if (PageCurrent == PageType.InstanceSelect)
			{
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.RunOnUpdated, 1, "versions\\");
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "切回窗口时出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void HandleDrag(object sender, DragEventArgs e)
	{
		try
		{
			if (e.Handled && e.Effects != 0)
			{
				return;
			}
			e.Handled = true;
			if (e.Data == _0024STATIC_0024HandleDrag_002420211C1284DD_0024PrevData)
			{
				e.Effects = _0024STATIC_0024HandleDrag_002420211C1284DD_0024PrevEffects;
				return;
			}
			e.Effects = DragDropEffects.None;
			if (e.Data.GetDataPresent(DataFormats.Text))
			{
				string text = Conversions.ToString(e.Data.GetData(DataFormats.Text));
				if (StringExtensions.StartsWithF(text, "authlib-injector:yggdrasil-server:", false))
				{
					e.Effects = DragDropEffects.Copy;
				}
				else if (StringExtensions.StartsWithF(text, "file:///", false))
				{
					e.Effects = DragDropEffects.Copy;
				}
			}
			else if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
				if (array != null && array.Length > 0)
				{
					e.Effects = DragDropEffects.Link;
				}
			}
			_0024STATIC_0024HandleDrag_002420211C1284DD_0024PrevData = e.Data;
			_0024STATIC_0024HandleDrag_002420211C1284DD_0024PrevEffects = e.Effects;
			Logger.Info($"设置拖放类型：{e.Effects}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "处理拖放时出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void FrmMain_Drop(object sender, DragEventArgs e)
	{
		try
		{
			ShowWindowToTop();
			if (e.Data.GetDataPresent(DataFormats.Text))
			{
				try
				{
					string text = Conversions.ToString(e.Data.GetData(DataFormats.Text));
					Logger.Info($"接受文本拖拽：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					if (StringExtensions.StartsWithF(text, "authlib-injector:yggdrasil-server:", false))
					{
						e.Handled = true;
						e.Effects = DragDropEffects.Copy;
						string text2 = StringUtils.FormUrlUnescape(text.Substring("authlib-injector:yggdrasil-server:".Length));
						Logger.Info($"Authlib 拖拽：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
						if (!string.IsNullOrEmpty(new ValidateHttp().Validate(text2)))
						{
							ModMain.Hint($"输入的 Authlib 验证服务器不符合网址格式（{text2}）！", ModMain.HintType.Red);
						}
						else
						{
							ModMinecraft.McInstance mcInstance = ((PageCurrent == PageType.InstanceSetup) ? PageInstanceLeft.Instance : ModMinecraft.McInstanceSelected);
							if ((object)mcInstance == null)
							{
								ModMain.Hint("请先下载游戏，再设置第三方登录！", ModMain.HintType.Red);
							}
							else
							{
								if (Operators.CompareString(text2, "https://littleskin.cn/api/yggdrasil", TextCompare: false) == 0)
								{
									if (ModMain.MyMsgBox($"是否要在版本 {mcInstance.Name} 中开启 LittleSkin 登录？" + "\r\n你可以在 版本设置 → 设置 → 服务器选项 中修改登录方式。", "第三方登录开启确认", "确定", "取消") == 2)
									{
										return;
									}
									Settings.Set("VersionServerLogin", 4, mcInstance);
									Settings.Set("VersionServerAuthServer", "https://littleskin.cn/api/yggdrasil", mcInstance);
									Settings.Set("VersionServerAuthRegister", "https://littleskin.cn/auth/register", mcInstance);
									Settings.Set("VersionServerAuthName", "LittleSkin 登录", mcInstance);
								}
								else
								{
									if (ModMain.MyMsgBox($"是否要在版本 {mcInstance.Name} 中开启第三方登录？" + "\r\n" + $"登录服务器：{text2}" + "\r\n\r\n你可以在 版本设置 → 设置 → 服务器选项 中修改登录方式。", "第三方登录开启确认", "确定", "取消") == 2)
									{
										return;
									}
									Settings.Set("VersionServerLogin", 4, mcInstance);
									Settings.Set("VersionServerAuthServer", text2, mcInstance);
									Settings.Set("VersionServerAuthRegister", text2.Replace("api/yggdrasil", "auth/register"), mcInstance);
									Settings.Set("VersionServerAuthName", "", mcInstance);
								}
								if (PageCurrent == PageType.InstanceSetup && PageCurrentSub == PageSubType.SetupLink)
								{
									ModMain.FrmInstanceSetup.Reload();
								}
								else if (PageCurrent == PageType.Launch)
								{
									ModMain.FrmLaunchLeft.RefreshPage(KeepInput: true, Anim: false);
								}
							}
						}
					}
					else if (StringExtensions.StartsWithF(text, "file:///", false))
					{
						string item = WebUtility.UrlDecode(text).Substring("file:///".Length).Replace("/", "\\");
						e.Handled = true;
						e.Effects = DragDropEffects.Copy;
						FileDrag(new List<string> { item });
					}
					return;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "无法接取文本拖拽事件", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					ProjectData.ClearProjectError();
					return;
				}
			}
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				object objectValue = RuntimeHelpers.GetObjectValue(e.Data.GetData(DataFormats.FileDrop));
				if (objectValue == null)
				{
					ModMain.Hint("请将文件解压后再拖入！", ModMain.HintType.Red);
					return;
				}
				e.Handled = true;
				e.Effects = DragDropEffects.Link;
				FileDrag((IEnumerable<string>)objectValue);
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Error(ex4, "接取拖拽事件失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void FileDrag(IEnumerable<string> FilePathList)
	{
		_Closure_0024__25_002D0 arg = default(_Closure_0024__25_002D0);
		_Closure_0024__25_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__25_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_FilePathList = FilePathList;
		ModBase.RunInNewThread([SpecialName] () =>
		{
			_Closure_0024__25_002D1 arg2 = default(_Closure_0024__25_002D1);
			_Closure_0024__25_002D1 CS_0024_003C_003E8__locals1 = new _Closure_0024__25_002D1(arg2);
			string text = CS_0024_003C_003E8__locals0._0024VB_0024Local_FilePathList.First();
			Logger.Warn(string.Format("接受文件拖拽：{0}{1}", text, CS_0024_003C_003E8__locals0._0024VB_0024Local_FilePathList.Any() ? $" 等 {CS_0024_003C_003E8__locals0._0024VB_0024Local_FilePathList.Count()} 个文件" : ""), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			if (DirectoryUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_FilePathList.First()) && !FileUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_FilePathList.First()))
			{
				ModMain.Hint("请拖入一个文件，而非文件夹！", ModMain.HintType.Red);
			}
			else if (!FileUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_FilePathList.First()))
			{
				ModMain.Hint("拖入的文件不存在：" + CS_0024_003C_003E8__locals0._0024VB_0024Local_FilePathList.First(), ModMain.HintType.Red);
			}
			else
			{
				List<string> list = CS_0024_003C_003E8__locals0._0024VB_0024Local_FilePathList.ToList();
				if (CS_0024_003C_003E8__locals0._0024VB_0024Local_FilePathList.Count() > 1)
				{
					foreach (string item in list)
					{
						if (!new string[4] { "jar", "litemod", "disabled", "old" }.Contains(PathUtils.GetExtension(item)))
						{
							ModMain.Hint("一次请只拖入一个文件！", ModMain.HintType.Red);
							return;
						}
					}
				}
				CS_0024_003C_003E8__locals1._0024VB_0024Local_Extension = PathUtils.GetExtension(text);
				if (Operators.CompareString(CS_0024_003C_003E8__locals1._0024VB_0024Local_Extension, "xaml", TextCompare: false) == 0)
				{
					Logger.Info("文件后缀为 XAML，作为主页加载", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
					if (!FileUtils.Exists(Paths.Base + "PCL\\Custom.xaml") || ModMain.MyMsgBox("已存在一个主页文件，是否要将它覆盖？", "覆盖确认", "覆盖", "取消") != 2)
					{
						FileUtils.Copy(text, Paths.Base + "PCL\\Custom.xaml");
						ModBase.RunInUi([SpecialName] () =>
						{
							Settings.Set("UiCustomType", 1);
							ModMain.FrmLaunchRight.ForceRefresh();
							ModMain.Hint("已加载主页自定义文件！", ModMain.HintType.Green);
						});
					}
				}
				else if (!PageInstanceMod.InstallMods(list))
				{
					if (new string[3] { "zip", "rar", "mrpack" }.Any([SpecialName] (string t) => Operators.CompareString(t, CS_0024_003C_003E8__locals1._0024VB_0024Local_Extension, TextCompare: false) == 0))
					{
						ModMain.Hint("正在读取压缩包……");
						try
						{
							Logger.Info("文件为压缩包，尝试作为整合包安装", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
							ModModpack.ModpackInstall(text);
							ModBase.RunInUi([SpecialName] () =>
							{
								ModMain.FrmMain.PageChange(PageType.TaskManager);
							});
							return;
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							if (ExceptionExtensions.IsCanceled(ex2))
							{
								ProjectData.ClearProjectError();
								return;
							}
							ProjectData.ClearProjectError();
						}
					}
					if (Operators.CompareString(CS_0024_003C_003E8__locals1._0024VB_0024Local_Extension, "rar", TextCompare: false) == 0)
					{
						ModMain.Hint("PCL 无法处理 rar 格式的压缩包，请在解压后重新压缩为 zip 格式再试！", ModMain.HintType.Red);
					}
					else
					{
						try
						{
							Logger.Info("尝试进行错误报告分析", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
							CrashAnalyzer crashAnalyzer = new CrashAnalyzer();
							crashAnalyzer.Import(text);
							if (crashAnalyzer.Prepare())
							{
								crashAnalyzer.Analyze();
								crashAnalyzer.Output(IsManualAnalyze: true, new List<string>());
								return;
							}
						}
						catch (Exception ex3)
						{
							ProjectData.SetProjectError(ex3);
							Exception ex4 = ex3;
							Logger.Error(ex4, "自主错误报告分析失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
							ProjectData.ClearProjectError();
						}
						ModMain.Hint("PCL 无法确定适合的操作……", ModMain.HintType.Red);
					}
				}
			}
		}, "文件拖拽");
	}

	private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
	{
		switch (msg)
		{
		case 30:
		{
			DateTime now = DateTime.Now;
			if (DateTime.Compare(now.Date, ModBase.ApplicationOpenTime.Date) == 0)
			{
				Logger.Info($"系统时间微调为：{now.ToLongDateString()} {now.ToLongTimeString()}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				IsSystemTimeChanged = false;
			}
			else
			{
				Logger.Info($"系统时间修改为：{now.ToLongDateString()} {now.ToLongTimeString()}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				IsSystemTimeChanged = true;
			}
			break;
		}
		case 6402:
			Logger.Info($"收到置顶信息：{hwnd.ToInt64()}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			if (!IsWindowLoadFinished)
			{
				Logger.Info("窗口尚未加载完成，忽略置顶请求", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				return IntPtr.Zero;
			}
			ShowWindowToTop();
			handled = true;
			break;
		}
		return IntPtr.Zero;
	}

	public void ShowWindowToTop()
	{
		ModBase.RunInUi([SpecialName] () =>
		{
			base.Visibility = Visibility.Visible;
			base.ShowInTaskbar = true;
			base.WindowState = WindowState.Normal;
			Hidden = false;
			base.Topmost = true;
			base.Topmost = false;
			ModMain.SetForegroundWindow(ModBase.Handle);
			Focus();
			Logger.Info($"窗口已置顶，位置：({base.Left}, {base.Top}), {base.Width} x {base.Height}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		});
	}

	private string PageNameGet(PageStackData Stack)
	{
		return Stack.Page switch
		{
			PageType.InstanceSelect => "版本选择", 
			PageType.TaskManager => "任务管理", 
			PageType.InstanceSetup => "版本设置 - " + (((object)PageInstanceLeft.Instance == null) ? "未知版本" : PageInstanceLeft.Instance.Name), 
			PageType.ResourceDetail => ((ResourceProject)NewLateBinding.LateIndexGet(Stack.Additional, new object[1] { 0 }, null)).TranslatedName, 
			PageType.HelpDetail => ((ModMain.HelpEntry)NewLateBinding.LateIndexGet(Stack.Additional, new object[1] { 0 }, null)).Title, 
			_ => "", 
		};
	}

	public void PageNameRefresh(PageStackData Type)
	{
		LabTitleInner.Text = PageNameGet(Type);
	}

	public void PageNameRefresh()
	{
		PageNameRefresh(PageCurrent);
	}

	public void PageChange(PageStackData Stack, PageSubType SubType = PageSubType.Default)
	{
		if (Operators.CompareString(PageNameGet(Stack), "", TextCompare: false) == 0)
		{
			PageChangeExit();
			IsChangingPage = true;
			((MyRadioButton)PanTitleSelect.Children[(int)(PageType)Stack]).SetChecked(value: true, raiseByMouse: true, Operators.CompareString(PageNameGet(PageCurrent), "", TextCompare: false) == 0);
			IsChangingPage = false;
			switch (Stack.Page)
			{
			case PageType.Download:
				if (ModMain.FrmDownloadLeft == null)
				{
					ModMain.FrmDownloadLeft = new PageDownloadLeft();
				}
				((MyListItem)ModMain.FrmDownloadLeft.PanItem.Children[(int)SubType]).SetChecked(value: true, user: true, Stack == PageCurrent);
				break;
			case PageType.Setup:
				if (ModMain.FrmSetupLeft == null)
				{
					ModMain.FrmSetupLeft = new PageSetupLeft();
				}
				((MyListItem)ModMain.FrmSetupLeft.PanItem.Children[(int)SubType]).SetChecked(value: true, user: true, Stack == PageCurrent);
				break;
			case PageType.Other:
				if (ModMain.FrmOtherLeft == null)
				{
					ModMain.FrmOtherLeft = new PageOtherLeft();
				}
				((MyListItem)ModMain.FrmOtherLeft.PanItem.Children[(int)SubType]).SetChecked(value: true, user: true, Stack == PageCurrent);
				break;
			}
			PageChangeActual(Stack, SubType);
			return;
		}
		PageType page = Stack.Page;
		if (page == PageType.InstanceSetup)
		{
			if (ModMain.FrmInstanceLeft == null)
			{
				ModMain.FrmInstanceLeft = new PageInstanceLeft();
			}
			((MyListItem)ModMain.FrmInstanceLeft.PanItem.Children[(int)SubType]).SetChecked(value: true, user: true, Stack == PageCurrent);
		}
		PageChangeActual(Stack, SubType);
	}

	private void BtnTitleSelect_Click(MyRadioButton sender, bool raiseByMouse)
	{
		if (!IsChangingPage)
		{
			PageChangeActual((PageType)checked((int)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(sender.Tag)))));
		}
	}

	public void PageBack()
	{
		if (PageStack.Any())
		{
			PageChangeActual(PageStack[0]);
		}
		else
		{
			PageChange(PageType.Launch);
		}
	}

	private void PageChangeActual(PageStackData Stack, PageSubType SubType = (PageSubType)(-1))
	{
		if (PageCurrent == Stack && (PageCurrentSub == SubType || SubType == (PageSubType)(-1)))
		{
			return;
		}
		if (Stack.Page == PageType.InstanceSetup && (object)PageInstanceLeft.Instance != null && ModMinecraft.McInstanceLock.IsLocked(PageInstanceLeft.Instance))
		{
			ModMain.Hint("该版本正在被修改，暂时无法进入版本设置！", ModMain.HintType.Red);
			return;
		}
		checked
		{
			ModAnimation.AniControlEnabled++;
			try
			{
				_Closure_0024__50_002D0 arg = default(_Closure_0024__50_002D0);
				_Closure_0024__50_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__50_002D0(arg);
				CS_0024_003C_003E8__locals0._0024VB_0024Me = this;
				CS_0024_003C_003E8__locals0._0024VB_0024Local_PageName = PageNameGet(Stack);
				if (Operators.CompareString(CS_0024_003C_003E8__locals0._0024VB_0024Local_PageName, "", TextCompare: false) == 0)
				{
					PageChangeExit();
				}
				else if (PageStack.Any())
				{
					ModAnimation.AniStart(new ModAnimation.AniData[3]
					{
						ModAnimation.AaOpacity(LabTitleInner, 0.0 - LabTitleInner.Opacity, 130),
						ModAnimation.AaCode([SpecialName] () =>
						{
							CS_0024_003C_003E8__locals0._0024VB_0024Me.LabTitleInner.Text = CS_0024_003C_003E8__locals0._0024VB_0024Local_PageName;
						}, 0, After: true),
						ModAnimation.AaOpacity(LabTitleInner, 1.0, 150, 30)
					}, "FrmMain Titlebar SubLayer");
					if (PageStack.Contains(Stack))
					{
						while (PageStack.Contains(Stack))
						{
							PageStack.RemoveAt(0);
						}
					}
					else
					{
						PageStack.Insert(0, PageCurrent);
					}
				}
				else
				{
					PanTitleInner.Visibility = Visibility.Visible;
					PanTitleMain.IsHitTestVisible = false;
					PanTitleInner.IsHitTestVisible = true;
					PageNameRefresh(Stack);
					ModAnimation.AniStart(new ModAnimation.AniData[5]
					{
						ModAnimation.AaOpacity(PanTitleMain, 0.0 - PanTitleMain.Opacity, 150),
						ModAnimation.AaX(PanTitleMain, 12.0 - PanTitleMain.Margin.Left, 150, 0, new ModAnimation.AniEaseInFluent(ModAnimation.AniEasePower.Weak)),
						ModAnimation.AaOpacity(PanTitleInner, 1.0 - PanTitleInner.Opacity, 150, 200),
						ModAnimation.AaX(PanTitleInner, 0.0 - PanTitleInner.Margin.Left, 350, 200, new ModAnimation.AniEaseOutBack()),
						ModAnimation.AaCode([SpecialName] () =>
						{
							PanTitleMain.Visibility = Visibility.Collapsed;
						}, 0, After: true)
					}, "FrmMain Titlebar FirstLayer");
					PageStack.Insert(0, PageCurrent);
				}
				PageLast = PageCurrent;
				PageCurrent = Stack;
				switch (Stack.Page)
				{
				case PageType.Launch:
					PageChangeAnim(ModMain.FrmLaunchLeft, ModMain.FrmLaunchRight);
					break;
				case PageType.Download:
					if (ModMain.FrmDownloadLeft == null)
					{
						ModMain.FrmDownloadLeft = new PageDownloadLeft();
					}
					PageChangeAnim(ModMain.FrmDownloadLeft, (FrameworkElement)ModMain.FrmDownloadLeft.PageGet(SubType));
					break;
				case PageType.Link:
					if (ModMain.FrmLinkMain == null)
					{
						ModMain.FrmLinkMain = new PageLinkMain();
					}
					PageChangeAnim(new MyPageLeft(), ModMain.FrmLinkMain);
					break;
				case PageType.Setup:
					if (ModMain.FrmSetupLeft == null)
					{
						ModMain.FrmSetupLeft = new PageSetupLeft();
					}
					PageChangeAnim(ModMain.FrmSetupLeft, (FrameworkElement)ModMain.FrmSetupLeft.PageGet(SubType));
					break;
				case PageType.Other:
					if (ModMain.FrmOtherLeft == null)
					{
						ModMain.FrmOtherLeft = new PageOtherLeft();
					}
					PageChangeAnim(ModMain.FrmOtherLeft, (FrameworkElement)ModMain.FrmOtherLeft.PageGet(SubType));
					break;
				case PageType.InstanceSelect:
					if (ModMain.FrmSelectLeft == null)
					{
						ModMain.FrmSelectLeft = new PageSelectLeft();
					}
					if (ModMain.FrmSelectRight == null)
					{
						ModMain.FrmSelectRight = new PageSelectRight();
					}
					PageChangeAnim(ModMain.FrmSelectLeft, ModMain.FrmSelectRight);
					break;
				case PageType.TaskManager:
					if (ModMain.FrmSpeedLeft == null)
					{
						ModMain.FrmSpeedLeft = new PageSpeedLeft();
					}
					if (ModMain.FrmSpeedRight == null)
					{
						ModMain.FrmSpeedRight = new PageSpeedRight();
					}
					PageChangeAnim(ModMain.FrmSpeedLeft, ModMain.FrmSpeedRight);
					break;
				case PageType.InstanceSetup:
					if (ModMain.FrmInstanceLeft == null)
					{
						ModMain.FrmInstanceLeft = new PageInstanceLeft();
					}
					PageChangeAnim(ModMain.FrmInstanceLeft, (FrameworkElement)ModMain.FrmInstanceLeft.PageGet(SubType));
					break;
				case PageType.ResourceDetail:
					if (ModMain.FrmDownloadResourceDetail == null)
					{
						ModMain.FrmDownloadResourceDetail = new PageDownloadResourceDetail();
					}
					PageChangeAnim(new MyPageLeft(), ModMain.FrmDownloadResourceDetail);
					break;
				case PageType.HelpDetail:
					PageChangeAnim(new MyPageLeft(), (FrameworkElement)NewLateBinding.LateIndexGet(Stack.Additional, new object[1] { 1 }, null));
					break;
				}
				BtnExtraDownload.ShowRefresh();
				BtnExtraApril.ShowRefresh();
				Logger.Info($"切换主要页面：{Stack.Page}, {SubType}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"切换主要页面失败（ID {PageCurrent.Page}）", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
				ProjectData.ClearProjectError();
			}
			finally
			{
				ModAnimation.AniControlEnabled--;
			}
		}
	}

	private void PageChangeAnim(FrameworkElement TargetLeft, FrameworkElement TargetRight)
	{
		ModAnimation.AniStop("FrmMain LeftChange");
		ModAnimation.AniStop("PageLeft PageChange");
		checked
		{
			ModAnimation.AniControlEnabled++;
			if (TargetLeft.Parent != null)
			{
				TargetLeft.SetValue(ContentPresenter.ContentProperty, null);
			}
			if (TargetRight != null && TargetRight.Parent != null)
			{
				TargetRight.SetValue(ContentPresenter.ContentProperty, null);
			}
			PageLeft = (MyPageLeft)TargetLeft;
			PageRight = (MyPageRight)TargetRight;
			((MyPageLeft)PanMainLeft.Child).TriggerHideAnimation();
			((MyPageRight)PanMainRight.Child).PageOnExit();
			ModAnimation.AniControlEnabled--;
			ModAnimation.AniStart(new ModAnimation.AniData[2]
			{
				ModAnimation.AaCode([SpecialName] () =>
				{
					ModAnimation.AniControlEnabled++;
					PanMainLeft.Child = PageLeft;
					PageLeft.Opacity = 0.0;
					PanMainLeft.Background = null;
					ModAnimation.AniControlEnabled--;
					ModBase.RunInUi([SpecialName] () =>
					{
						PanMainLeft_Resize(PanMainLeft.ActualWidth);
					}, ForceWaitUntilLoaded: true);
				}, 110),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PageLeft.Opacity = 1.0;
					PageLeft.TriggerShowAnimation();
				}, 30, After: true)
			}, "FrmMain PageChangeLeft");
			ModAnimation.AniStart(new ModAnimation.AniData[2]
			{
				ModAnimation.AaCode([SpecialName] () =>
				{
					ModAnimation.AniControlEnabled++;
					((MyPageRight)PanMainRight.Child).PageOnForceExit();
					PanMainRight.Child = PageRight;
					PageRight.Opacity = 0.0;
					PanMainRight.Background = null;
					ModAnimation.AniControlEnabled--;
					ModBase.RunInUi([SpecialName] () =>
					{
						BtnExtraBack.ShowRefresh();
					}, ForceWaitUntilLoaded: true);
				}, 110),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PageRight.Opacity = 1.0;
					PageRight.PageOnEnter();
				}, 30, After: true)
			}, "FrmMain PageChangeRight");
		}
	}

	private void PageChangeExit()
	{
		if (PageStack.Any())
		{
			PanTitleMain.Visibility = Visibility.Visible;
			PanTitleMain.IsHitTestVisible = true;
			PanTitleInner.IsHitTestVisible = false;
			ModAnimation.AniStart(new ModAnimation.AniData[5]
			{
				ModAnimation.AaOpacity(PanTitleInner, 0.0 - PanTitleInner.Opacity, 150),
				ModAnimation.AaX(PanTitleInner, -18.0 - PanTitleInner.Margin.Left, 150, 0, new ModAnimation.AniEaseInFluent()),
				ModAnimation.AaOpacity(PanTitleMain, 1.0 - PanTitleMain.Opacity, 150, 200),
				ModAnimation.AaX(PanTitleMain, 0.0 - PanTitleMain.Margin.Left, 350, 200, new ModAnimation.AniEaseOutBack(ModAnimation.AniEasePower.Weak)),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PanTitleInner.Visibility = Visibility.Collapsed;
				}, 0, After: true)
			}, "FrmMain Titlebar FirstLayer");
			PageStack.Clear();
		}
	}

	private void PanMainLeft_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (e.WidthChanged)
		{
			PanMainLeft_Resize(e.NewSize.Width);
		}
	}

	private void PanMainLeft_Resize(double NewWidth)
	{
		if (Math.Abs(NewWidth - RectLeftBackground.Width) > 0.1 && ModAnimation.AniControlEnabled == 0)
		{
			if (PanMain.Opacity < 0.1)
			{
				PanMainLeft.IsHitTestVisible = false;
			}
			if (NewWidth > 0.0)
			{
				ModAnimation.AniStart(new ModAnimation.AniData[3]
				{
					ModAnimation.AaWidth(RectLeftBackground, NewWidth - RectLeftBackground.Width, 180, 0, new ModAnimation.AniEaseOutFluent(ModAnimation.AniEasePower.ExtraStrong)),
					ModAnimation.AaOpacity(RectLeftShadow, 1.0 - RectLeftShadow.Opacity, 180),
					ModAnimation.AaCode([SpecialName] () =>
					{
						PanMainLeft.IsHitTestVisible = true;
					}, 150)
				}, "FrmMain LeftChange", RefreshTime: true);
			}
			else
			{
				ModAnimation.AniStart(new ModAnimation.AniData[3]
				{
					ModAnimation.AaWidth(RectLeftBackground, 0.0 - RectLeftBackground.Width, 180, 0, new ModAnimation.AniEaseOutFluent()),
					ModAnimation.AaOpacity(RectLeftShadow, 0.0 - RectLeftShadow.Opacity, 180),
					ModAnimation.AaCode([SpecialName] () =>
					{
						PanMainLeft.IsHitTestVisible = true;
					}, 150)
				}, "FrmMain LeftChange", RefreshTime: true);
			}
		}
		else
		{
			RectLeftBackground.Width = NewWidth;
			PanMainLeft.IsHitTestVisible = true;
			ModAnimation.AniStop("FrmMain LeftChange");
		}
	}

	public void DragTick()
	{
		if (ModMain.DragControl != null && Mouse.LeftButton != MouseButtonState.Pressed)
		{
			DragStop();
		}
	}

	public void DragDoing()
	{
		if (ModMain.DragControl != null)
		{
			if (Mouse.LeftButton == MouseButtonState.Pressed)
			{
				NewLateBinding.LateCall(ModMain.DragControl, null, "DragDoing", new object[0], null, null, null, IgnoreReturn: true);
			}
			else
			{
				DragStop();
			}
		}
	}

	public void DragStop()
	{
		ModBase.RunInUi([SpecialName] () =>
		{
			if (ModMain.DragControl != null)
			{
				object objectValue = RuntimeHelpers.GetObjectValue(ModMain.DragControl);
				ModMain.DragControl = null;
				NewLateBinding.LateCall(objectValue, null, "DragStop", new object[0], null, null, null, IgnoreReturn: true);
			}
		});
	}

	private void BtnExtraMusic_Click(object sender, EventArgs e)
	{
		ModMusic.MusicControlPause();
	}

	private void BtnExtraMusic_RightClick(object sender, EventArgs e)
	{
		ModMusic.MusicControlNext();
	}

	private void BtnExtraDownload_Click(object sender, EventArgs e)
	{
		PageChange(PageType.TaskManager);
	}

	private bool BtnExtraDownload_ShowCheck()
	{
		if (ModNet.HasDownloadingTask())
		{
			return !(PageCurrent == PageType.TaskManager);
		}
		return false;
	}

	public void AprilGiveup()
	{
		if (ModMain.IsAprilEnabled && !ModMain.IsAprilGiveup)
		{
			ModMain.Hint("=D", ModMain.HintType.Green);
			ModMain.IsAprilGiveup = true;
			ModMain.FrmLaunchLeft.AprilScaleTrans.ScaleX = 1.0;
			ModMain.FrmLaunchLeft.AprilScaleTrans.ScaleY = 1.0;
			BtnExtraApril.ShowRefresh();
		}
	}

	public bool BtnExtraApril_ShowCheck()
	{
		if (ModMain.IsAprilEnabled && !ModMain.IsAprilGiveup)
		{
			return PageCurrent == PageType.Launch;
		}
		return false;
	}

	public void BtnExtraShutdown_Click()
	{
		try
		{
			if (ModLaunch.McLaunchLoaderReal != null)
			{
				ModLaunch.McLaunchLoaderReal.Cancel();
			}
			foreach (ModWatcher.Watcher mcWatcher in ModWatcher.McWatcherList)
			{
				mcWatcher.Kill();
			}
			ModMain.Hint("已关闭运行中的 Minecraft！", ModMain.HintType.Green);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "强制关闭所有 Minecraft 失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	public bool BtnExtraShutdown_ShowCheck()
	{
		return ModWatcher.HasRunningMinecraft;
	}

	public void BackToTop()
	{
		MyScrollViewer myScrollViewer = BtnExtraBack_GetRealChild();
		if (myScrollViewer != null)
		{
			myScrollViewer.PerformVerticalOffsetDelta(0.0 - myScrollViewer.VerticalOffset);
		}
		else
		{
			Logger.Error("无法返回顶部，未找到合适的 RealScroll", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\FormMain.xaml.vb");
		}
	}

	private bool BtnExtraBack_ShowCheck()
	{
		MyScrollViewer myScrollViewer = BtnExtraBack_GetRealChild();
		if (myScrollViewer != null && myScrollViewer.Visibility == Visibility.Visible)
		{
			return myScrollViewer.VerticalOffset > base.Height + (double)((!BtnExtraBack.Show) ? 700 : 0);
		}
		return false;
	}

	private MyScrollViewer BtnExtraBack_GetRealChild()
	{
		if (PanMainRight.Child == null || !(PanMainRight.Child is MyPageRight))
		{
			return null;
		}
		MyPageRight obj = (MyPageRight)PanMainRight.Child;
		return (MyScrollViewer)obj.FindName(obj.PanScroll);
	}

	private void FormMain_MouseMove(object sender, MouseEventArgs e)
	{
		lastMouseArg = e;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/formmain.xaml", UriKind.Relative);
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
			WindMain = (FormMain)target;
			break;
		case 2:
			PanBack = (Grid)target;
			break;
		case 3:
			TransformRotate = (RotateTransform)target;
			break;
		case 4:
			TransformPos = (TranslateTransform)target;
			break;
		case 5:
			ResizerT = (Rectangle)target;
			break;
		case 6:
			ResizerB = (Rectangle)target;
			break;
		case 7:
			ResizerR = (Rectangle)target;
			break;
		case 8:
			ResizerL = (Rectangle)target;
			break;
		case 9:
			ResizerLT = (Rectangle)target;
			break;
		case 10:
			ResizerLB = (Rectangle)target;
			break;
		case 11:
			ResizerRB = (Rectangle)target;
			break;
		case 12:
			ResizerRT = (Rectangle)target;
			break;
		case 13:
			BorderForm = (Border)target;
			break;
		case 14:
			RectForm = (RectangleGeometry)target;
			break;
		case 15:
			PanForm = (Grid)target;
			break;
		case 16:
			ImgBack = (Canvas)target;
			break;
		case 17:
			PanTitle = (Grid)target;
			break;
		case 18:
			ImgTitle = (MyImage)target;
			break;
		case 19:
			BtnTitleClose = (MyIconButton)target;
			break;
		case 20:
			BtnTitleMin = (MyIconButton)target;
			break;
		case 21:
			PanTitleMain = (Grid)target;
			break;
		case 22:
			ShapeTitleLogo = (Path)target;
			break;
		case 23:
			LabTitleLogo = (TextBlock)target;
			break;
		case 24:
			ImageTitleLogo = (MyImage)target;
			break;
		case 25:
			PanTitleSelect = (StackPanel)target;
			break;
		case 26:
			BtnTitleSelect0 = (MyRadioButton)target;
			break;
		case 27:
			BtnTitleSelect1 = (MyRadioButton)target;
			break;
		case 28:
			BtnTitleSelect2 = (MyRadioButton)target;
			break;
		case 29:
			BtnTitleSelect3 = (MyRadioButton)target;
			break;
		case 30:
			BtnTitleSelect4 = (MyRadioButton)target;
			break;
		case 31:
			PanTitleInner = (Grid)target;
			break;
		case 32:
			BtnTitleInner = (MyIconButton)target;
			break;
		case 33:
			LabTitleInner = (TextBlock)target;
			break;
		case 34:
			PanLeft = (Grid)target;
			break;
		case 35:
			RectLeftBackground = (Rectangle)target;
			break;
		case 36:
			RectLeftShadow = (Rectangle)target;
			break;
		case 37:
			PanMain = (Grid)target;
			break;
		case 38:
			PanMainRight = (Border)target;
			break;
		case 39:
			PanMainLeft = (Border)target;
			break;
		case 40:
			PanHint = (StackPanel)target;
			break;
		case 41:
			BtnExtraBack = (MyExtraButton)target;
			break;
		case 42:
			BtnExtraDownload = (MyExtraButton)target;
			break;
		case 43:
			BtnExtraApril = (MyExtraButton)target;
			break;
		case 44:
			BtnExtraShutdown = (MyExtraButton)target;
			break;
		case 45:
			BtnExtraMusic = (MyExtraButton)target;
			break;
		case 46:
			PanMsg = (Grid)target;
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

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__3_002D0()
	{
		WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this);
		_0024STATIC_0024_002Ector_00242001_0024Helper.HwndIntPtrSource = HwndSource.FromHwnd(windowInteropHelper.Handle);
		_0024STATIC_0024_002Ector_00242001_0024Helper.AddHook();
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__3_002D1()
	{
		_0024STATIC_0024_002Ector_00242001_0024Helper.RemoveDragHook();
	}

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__3_002D2()
	{
		FileDrag(_0024STATIC_0024_002Ector_00242001_0024Helper.DropFilePaths);
	}
}
