using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCL.My;
using PCLCS;

namespace PCL;

[StandardModule]
public sealed class ModMain
{
	public enum HintType
	{
		Blue,
		Green,
		Red
	}

	private struct HintMessage
	{
		public string Text;

		public HintType Type;

		public bool Log;
	}

	public class MyMsgBoxConverter
	{
		public MyMsgBoxType Type;

		public string Title;

		public string Text;

		public object Content;

		public Collection<Validate> ValidateRules;

		public string HintText;

		public bool HighLight;

		public string Button1;

		public string Button2;

		public string Button3;

		public Action Button1Action;

		public Action Button2Action;

		public Action Button3Action;

		public bool IsWarn;

		public bool ForceWait;

		public DispatcherFrame WaitFrame;

		public bool IsExited;

		public object Result;

		public MyMsgBoxConverter()
		{
			HintText = "";
			Button1 = "确定";
			Button2 = "";
			Button3 = "";
			Button1Action = null;
			Button2Action = null;
			Button3Action = null;
			IsWarn = false;
			ForceWait = false;
			WaitFrame = new DispatcherFrame(exitWhenRequested: true);
			IsExited = false;
		}
	}

	public enum MyMsgBoxType
	{
		Text,
		Select,
		Input,
		Login
	}

	public class HelpEntry
	{
		public string RawPath;

		public string Title;

		public string Desc;

		public string Search;

		public List<string> Types;

		public string Logo;

		public bool ShowInSearch;

		public bool ShowInPublic;

		public bool ShowInSnapshot;

		public bool IsEvent;

		public CustomEvent.EventType EventType;

		public string EventData;

		public string XamlContent;

		public HelpEntry(string FilePath)
		{
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			//IL_017f: Unknown result type (might be due to invalid IL or missing references)
			Logo = null;
			ShowInSearch = true;
			ShowInPublic = true;
			ShowInSnapshot = true;
			RawPath = FilePath;
			if (!FileUtils.Exists(FilePath))
			{
				throw new FileNotFoundException("未找到帮助文件：" + FilePath, FilePath);
			}
			JObject val = (JObject)StringExtensions.DeserializeJson(ArgumentReplace(FileUtils.ReadAsString(FilePath, (Encoding)null, (Type)null), (Func<string, string>)StringUtils.XmlEscape, ReplaceTime: true));
			if (val["Title"] != null)
			{
				Title = (string)val["Title"];
				Desc = (string)(val["Description"] ?? JToken.op_Implicit(""));
				Search = (string)(val["Keywords"] ?? JToken.op_Implicit(""));
				Logo = (string)val["Logo"];
				ShowInSearch = (bool)(val["ShowInSearch"] ?? JToken.op_Implicit(ShowInSearch));
				ShowInPublic = (bool)(val["ShowInPublic"] ?? JToken.op_Implicit(ShowInPublic));
				ShowInSnapshot = (bool)(val["ShowInSnapshot"] ?? JToken.op_Implicit(ShowInSnapshot));
				Types = new List<string>();
				foreach (JToken item in (IEnumerable<JToken>)(((object)val["Types"]) ?? ((object)new JArray())))
				{
					Types.Add((string)item);
				}
				if (((bool?)(val["IsEvent"] ?? JToken.op_Implicit(false))).GetValueOrDefault())
				{
					EventType = val["EventType"].ToObject<CustomEvent.EventType>();
					EventData = (string)(val["EventData"] ?? JToken.op_Implicit(""));
					IsEvent = true;
					return;
				}
				string text = Path.ChangeExtension(FilePath, ".xaml");
				if (FileUtils.Exists(text))
				{
					XamlContent = FileUtils.ReadAsString(text, (Encoding)null, (Type)null);
					IsEvent = false;
					return;
				}
				throw new FileNotFoundException("未找到帮助条目 .json 对应的 .xaml 文件（" + text + "）");
			}
			throw new ArgumentException("未找到 Title 项");
		}

		public MyListItem ToListItem()
		{
			return SetToListItem(new MyListItem());
		}

		public MyListItem SetToListItem(MyListItem Item)
		{
			string text = ((!IsEvent) ? (ModBase.PathImage + "Blocks/Grass.png") : ((EventType != CustomEvent.EventType.弹出窗口) ? (ModBase.PathImage + "Blocks/CommandBlock.png") : (ModBase.PathImage + "Blocks/GrassPath.png")));
			Item.SnapsToDevicePixels = true;
			Item.Title = Title;
			Item.Info = Desc;
			Item.Logo = Logo ?? text;
			Item.Height = 42.0;
			Item.Type = MyListItem.CheckType.Clickable;
			Item.Tag = this;
			_ = null;
			CustomEventService.SetEventType(Item, CustomEvent.EventType.None);
			CustomEventService.SetEventData(Item, null);
			Item.Click += [SpecialName] (object sender, MouseButtonEventArgs e) =>
			{
				PageOtherHelp.OnItemClick((HelpEntry)NewLateBinding.LateGet(sender, null, "Tag", new object[0], null, null, null));
			};
			return Item;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__1_002D0
	{
		public List<CustomEvent> _0024VB_0024Local_Events;

		public _Closure_0024__1_002D0(_Closure_0024__1_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Events = arg0._0024VB_0024Local_Events;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			foreach (CustomEvent _0024VB_0024Local_Event in _0024VB_0024Local_Events)
			{
				_0024VB_0024Local_Event.Raise();
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D0
	{
		public double _0024VB_0024Local_Percent;

		public Border _0024VB_0024Local_DoubleStack;

		public ModBase.MyColor _0024VB_0024Local_TargetColor0;

		public ModBase.MyColor _0024VB_0024Local_TargetColor1;

		public _Closure_0024__6_002D0(_Closure_0024__6_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Percent = arg0._0024VB_0024Local_Percent;
				_0024VB_0024Local_DoubleStack = arg0._0024VB_0024Local_DoubleStack;
				_0024VB_0024Local_TargetColor0 = arg0._0024VB_0024Local_TargetColor0;
				_0024VB_0024Local_TargetColor1 = arg0._0024VB_0024Local_TargetColor1;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(object i)
		{
			_0024VB_0024Local_Percent = Conversions.ToDouble(Operators.AddObject(_0024VB_0024Local_Percent, i));
			LinearGradientBrush obj = (LinearGradientBrush)_0024VB_0024Local_DoubleStack.Background;
			obj.GradientStops[0].Color = _0024VB_0024Local_TargetColor0 * _0024VB_0024Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - _0024VB_0024Local_Percent);
			obj.GradientStops[1].Color = _0024VB_0024Local_TargetColor1 * _0024VB_0024Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - _0024VB_0024Local_Percent);
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			NewLateBinding.LateIndexSetComplex(_0024VB_0024Local_DoubleStack.Tag, new object[2] { 0, false }, null, OptimisticSet: false, RValueBase: true);
		}

		[SpecialName]
		internal void _Lambda_0024__2()
		{
			FrmMain.PanHint.Children.Remove(_0024VB_0024Local_DoubleStack);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D1
	{
		public Border _0024VB_0024Local_NewHintControl;

		public _Closure_0024__6_002D0 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__6_002D1(_Closure_0024__6_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_NewHintControl = arg0._0024VB_0024Local_NewHintControl;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__3(object i)
		{
			_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Percent = Conversions.ToDouble(Operators.AddObject(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Percent, i));
			LinearGradientBrush obj = (LinearGradientBrush)_0024VB_0024Local_NewHintControl.Background;
			obj.GradientStops[0].Color = _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_TargetColor0 * _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Percent);
			obj.GradientStops[1].Color = _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_TargetColor1 * _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Percent);
		}

		[SpecialName]
		internal void _Lambda_0024__4()
		{
			NewLateBinding.LateIndexSetComplex(_0024VB_0024Local_NewHintControl.Tag, new object[2] { 0, false }, null, OptimisticSet: false, RValueBase: true);
		}

		[SpecialName]
		internal void _Lambda_0024__5()
		{
			FrmMain.PanHint.Children.Remove(_0024VB_0024Local_NewHintControl);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__7_002D0
	{
		public Border _0024VB_0024Local_Control;

		public _Closure_0024__7_002D0(_Closure_0024__7_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Control = arg0._0024VB_0024Local_Control;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			NewLateBinding.LateIndexSetComplex(_0024VB_0024Local_Control.Tag, new object[2] { 0, false }, null, OptimisticSet: false, RValueBase: true);
		}

		[SpecialName]
		internal void _Lambda_0024__1()
		{
			FrmMain.PanHint.Children.Remove(_0024VB_0024Local_Control);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__78_002D0
	{
		public string _0024VB_0024Local_Event;

		public string[] _0024VB_0024Local_Datas;

		public _Closure_0024__78_002D0(_Closure_0024__78_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Event = arg0._0024VB_0024Local_Event;
				_0024VB_0024Local_Datas = arg0._0024VB_0024Local_Datas;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			checked
			{
				try
				{
					Logger.Info($"匿名数据上报：{_0024VB_0024Local_Event}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
					string text = string.Format("{0}&Event={1}", "https://ap-shanghai.cls.tencentcs.com/track?topic_id=pcl-remote-1253424809", StringUtils.UrlEscape(_0024VB_0024Local_Event));
					int num = _0024VB_0024Local_Datas.Length - 1;
					for (int i = 0; i <= num; i += 2)
					{
						text = text + "&" + StringUtils.UrlEscape(_0024VB_0024Local_Datas[i]) + "=" + StringUtils.UrlEscape(StringExtensions.ReplaceLineEndings(_0024VB_0024Local_Datas[i + 1], "\n", true));
					}
					ModNet.NetRequestByClient(text, null, null, null, "*/*", 25000, null, RequireJson: false, null, SimulateBrowserHeaders: false, MakeLog: false);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "匿名数据上报失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
					ProjectData.ClearProjectError();
				}
			}
		}
	}

	private static ConcurrentQueue<HintMessage> HintWaiting = new ConcurrentQueue<HintMessage>();

	public static ConcurrentQueue<MyMsgBoxConverter> WaitingMyMsgBox = new ConcurrentQueue<MyMsgBoxConverter>();

	public static FormMain FrmMain;

	public static SplashScreen FrmStart;

	public static PageLaunchLeft FrmLaunchLeft;

	public static PageLaunchRight FrmLaunchRight;

	public static PageSelectLeft FrmSelectLeft;

	public static PageSelectRight FrmSelectRight;

	public static PageSpeedLeft FrmSpeedLeft;

	public static PageSpeedRight FrmSpeedRight;

	public static PageLinkMain FrmLinkMain;

	public static PageDownloadLeft FrmDownloadLeft;

	public static PageDownloadInstall FrmDownloadInstall;

	public static PageDownloadMod FrmDownloadMod;

	public static PageDownloadPack FrmDownloadPack;

	public static PageDownloadDataPack FrmDownloadDataPack;

	public static PageDownloadShader FrmDownloadShader;

	public static PageDownloadResourcePack FrmDownloadResourcePack;

	public static PageSetupLeft FrmSetupLeft;

	public static PageSetupLaunch FrmSetupLaunch;

	public static PageSetupUI FrmSetupUI;

	public static PageSetupSystem FrmSetupSystem;

	public static PageSetupLink FrmSetupLink;

	public static PageOtherLeft FrmOtherLeft;

	public static PageOtherHelp FrmOtherHelp;

	public static PageOtherAbout FrmOtherAbout;

	public static PageOtherTest FrmOtherTest;

	public static PageLoginLegacy FrmLoginLegacy;

	public static PageLoginNide FrmLoginNide;

	public static PageLoginNideSkin FrmLoginNideSkin;

	public static PageLoginAuth FrmLoginAuth;

	public static PageLoginAuthSkin FrmLoginAuthSkin;

	public static PageLoginMs FrmLoginMs;

	public static PageLoginMsSkin FrmLoginMsSkin;

	public static PageInstanceLeft FrmInstanceLeft;

	public static PageInstanceOverall FrmInstanceOverall;

	public static PageInstanceMod FrmInstanceMod;

	public static PageInstanceModDisabled FrmInstanceModDisabled;

	public static PageInstanceSetup FrmInstanceSetup;

	public static PageInstanceExport FrmInstanceExport;

	public static PageDownloadModify FrmDownloadModify;

	public static PageDownloadResourceDetail FrmDownloadResourceDetail;

	public static ModLoader.LoaderTask<int, List<HelpEntry>> HelpLoader = new ModLoader.LoaderTask<int, List<HelpEntry>>("Help Page", HelpLoad, null, ThreadPriority.BelowNormal);

	private static readonly object HelpLoadLock = RuntimeHelpers.GetObjectValue(new object());

	private static bool? _IsAprilEnabled = null;

	public static bool IsAprilGiveup = false;

	private static int AprilDifficultiedDistance = 0;

	private static bool IsTaskTempCleared = false;

	private static bool IsTaskTempClearing = false;

	public static object DragControl = null;

	private static int Timer4Count = 0;

	private static int Timer150Count = 0;

	[SpecialName]
	private static int _0024STATIC_0024TimerFool_0024001_0024IdieCount;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024TimerFool_0024001_0024IdieCount_0024Init;

	[SpecialName]
	private static Point _0024STATIC_0024TimerFool_0024001_0024MousePosLast;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024TimerFool_0024001_0024MousePosLast_0024Init;

	[SpecialName]
	private static Vector _0024STATIC_0024TimerFool_0024001_0024Speed;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024TimerFool_0024001_0024Speed_0024Init;

	[SpecialName]
	private static double _0024STATIC_0024TimerFool_0024001_0024GiveUpDistance;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024TimerFool_0024001_0024GiveUpDistance_0024Init;

	[SpecialName]
	private static Func<string, string> _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer_0024Init;

	public static bool IsAprilEnabled
	{
		get
		{
			if (!_IsAprilEnabled.HasValue)
			{
				_IsAprilEnabled = DateTime.Now.Month == 4 && DateTime.Now.Day == 1 && Settings.Get<int>("AprilYear") != DateTime.Now.Year;
			}
			return _IsAprilEnabled.Value;
		}
	}

	public static void RaiseCustomEvent(this DependencyObject Control)
	{
		_Closure_0024__1_002D0 arg = default(_Closure_0024__1_002D0);
		_Closure_0024__1_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__1_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Events = CustomEventService.GetEvents(Control).ToList();
		CustomEvent.EventType eventType = CustomEventService.GetEventType(Control);
		if (eventType != 0)
		{
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Events.Add(new CustomEvent(eventType, CustomEventService.GetEventData(Control)));
		}
		if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Events.Any())
		{
			return;
		}
		ModBase.RunInNewThread([SpecialName] () =>
		{
			foreach (CustomEvent _0024VB_0024Local_Event in CS_0024_003C_003E8__locals0._0024VB_0024Local_Events)
			{
				_0024VB_0024Local_Event.Raise();
			}
		}, "执行自定义事件 " + Conversions.ToString(ModBase.GetUuid()));
	}

	public static void Hint(string Text, HintType Type = HintType.Blue, bool Log = true)
	{
		HintWaiting.Enqueue(new HintMessage
		{
			Text = (Text ?? ""),
			Type = Type,
			Log = Log
		});
	}

	private static void HintTick()
	{
		checked
		{
			try
			{
				HintMessage result = default(HintMessage);
				_Closure_0024__6_002D0 closure_0024__6_002D = default(_Closure_0024__6_002D0);
				_Closure_0024__6_002D1 closure_0024__6_002D2 = default(_Closure_0024__6_002D1);
				while (HintWaiting.TryDequeue(out result))
				{
					closure_0024__6_002D = new _Closure_0024__6_002D0(closure_0024__6_002D);
					result.Text = StringExtensions.ReplaceLineEndings(result.Text, " ", true);
					if (FrmMain.PanHint.Children.Count < 20)
					{
						closure_0024__6_002D._0024VB_0024Local_DoubleStack = null;
						foreach (Border child in FrmMain.PanHint.Children)
						{
							if (Conversions.ToBoolean(Conversions.ToBoolean(NewLateBinding.LateIndexGet(child.Tag, new object[1] { 0 }, null)) && Operators.CompareString(((TextBlock)child.Child).Text, result.Text, TextCompare: false) == 0))
							{
								closure_0024__6_002D._0024VB_0024Local_DoubleStack = child;
							}
						}
						closure_0024__6_002D._0024VB_0024Local_Percent = 0.3;
						switch (result.Type)
						{
						case HintType.Blue:
							closure_0024__6_002D._0024VB_0024Local_TargetColor0 = new ModBase.MyColor(215.0, 37.0, 155.0, 252.0);
							closure_0024__6_002D._0024VB_0024Local_TargetColor1 = new ModBase.MyColor(215.0, 10.0, 142.0, 252.0);
							break;
						case HintType.Green:
							closure_0024__6_002D._0024VB_0024Local_TargetColor0 = new ModBase.MyColor(215.0, 33.0, 177.0, 33.0);
							closure_0024__6_002D._0024VB_0024Local_TargetColor1 = new ModBase.MyColor(215.0, 29.0, 160.0, 29.0);
							break;
						default:
							closure_0024__6_002D._0024VB_0024Local_TargetColor0 = new ModBase.MyColor(215.0, 255.0, 53.0, 11.0);
							closure_0024__6_002D._0024VB_0024Local_TargetColor1 = new ModBase.MyColor(215.0, 255.0, 43.0, 0.0);
							break;
						}
						if (!Information.IsNothing(closure_0024__6_002D._0024VB_0024Local_DoubleStack))
						{
							if (!ModAnimation.AniIsRun(Conversions.ToString(Operators.ConcatenateObject("Hint Show ", NewLateBinding.LateIndexGet(closure_0024__6_002D._0024VB_0024Local_DoubleStack.Tag, new object[1] { 1 }, null)))))
							{
								ModAnimation.AniStop(Conversions.ToString(Operators.ConcatenateObject("Hint Hide ", NewLateBinding.LateIndexGet(closure_0024__6_002D._0024VB_0024Local_DoubleStack.Tag, new object[1] { 1 }, null))));
								double a = (double)(800 + MathUtils.Clamp<int>(result.Text.Length, 5, 23) * 180) * ModAnimation.AniSpeed;
								ModAnimation.AniStart(new ModAnimation.AniData[10]
								{
									ModAnimation.AaX(closure_0024__6_002D._0024VB_0024Local_DoubleStack, -12.0 - closure_0024__6_002D._0024VB_0024Local_DoubleStack.Margin.Left, 50, 0, new ModAnimation.AniEaseOutFluent()),
									ModAnimation.AaX(closure_0024__6_002D._0024VB_0024Local_DoubleStack, -8.0, 50, 50, new ModAnimation.AniEaseInFluent()),
									ModAnimation.AaX(closure_0024__6_002D._0024VB_0024Local_DoubleStack, 8.0, 50, 100, new ModAnimation.AniEaseOutFluent()),
									ModAnimation.AaX(closure_0024__6_002D._0024VB_0024Local_DoubleStack, -8.0, 50, 150, new ModAnimation.AniEaseInFluent()),
									ModAnimation.AaDouble(closure_0024__6_002D._Lambda_0024__0, 0.7, 250),
									ModAnimation.AaX(closure_0024__6_002D._0024VB_0024Local_DoubleStack, -50.0, 200, (int)Math.Round(a), new ModAnimation.AniEaseInFluent()),
									ModAnimation.AaOpacity(closure_0024__6_002D._0024VB_0024Local_DoubleStack, -1.0, 150, (int)Math.Round(a)),
									ModAnimation.AaCode(closure_0024__6_002D._Lambda_0024__1, (int)Math.Round(a)),
									ModAnimation.AaHeight(closure_0024__6_002D._0024VB_0024Local_DoubleStack, -26.0, 100, 0, new ModAnimation.AniEaseOutFluent(), After: true),
									ModAnimation.AaCode(closure_0024__6_002D._Lambda_0024__2, 0, After: true)
								}, Conversions.ToString(Operators.ConcatenateObject("Hint Hide ", NewLateBinding.LateIndexGet(closure_0024__6_002D._0024VB_0024Local_DoubleStack.Tag, new object[1] { 1 }, null))));
							}
						}
						else
						{
							closure_0024__6_002D2 = new _Closure_0024__6_002D1(closure_0024__6_002D2);
							closure_0024__6_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2 = closure_0024__6_002D;
							closure_0024__6_002D2._0024VB_0024Local_NewHintControl = new Border
							{
								Tag = new object[2]
								{
									true,
									ModBase.GetUuid()
								},
								Margin = new Thickness(-70.0, 0.0, 20.0, 0.0),
								Opacity = 0.0,
								Height = 0.0,
								HorizontalAlignment = HorizontalAlignment.Left,
								CornerRadius = new CornerRadius(0.0, 6.0, 6.0, 0.0)
							};
							closure_0024__6_002D2._0024VB_0024Local_NewHintControl.Background = new LinearGradientBrush(new GradientStopCollection(new List<GradientStop>
							{
								new GradientStop(closure_0024__6_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_TargetColor0 * closure_0024__6_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - closure_0024__6_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Percent), 0.0),
								new GradientStop(closure_0024__6_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_TargetColor1 * closure_0024__6_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Percent + new ModBase.MyColor(255.0, 255.0, 255.0) * (1.0 - closure_0024__6_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Percent), 1.0)
							}), 90.0);
							closure_0024__6_002D2._0024VB_0024Local_NewHintControl.Child = new TextBlock
							{
								TextTrimming = TextTrimming.CharacterEllipsis,
								FontSize = 13.0,
								Text = result.Text,
								Foreground = new ModBase.MyColor(255.0, 255.0, 255.0),
								Margin = new Thickness(33.0, 5.0, 8.0, 5.0)
							};
							FrmMain.PanHint.Children.Add(closure_0024__6_002D2._0024VB_0024Local_NewHintControl);
							List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
							if (FrmMain.PanHint.Children.Count > 1)
							{
								list.Add(ModAnimation.AaHeight(closure_0024__6_002D2._0024VB_0024Local_NewHintControl, 26.0, 150, 0, new ModAnimation.AniEaseOutFluent()));
							}
							else
							{
								closure_0024__6_002D2._0024VB_0024Local_NewHintControl.Height = 26.0;
							}
							list.AddRange(new ModAnimation.AniData[4]
							{
								ModAnimation.AaX(closure_0024__6_002D2._0024VB_0024Local_NewHintControl, 30.0, 400, 0, new ModAnimation.AniEaseOutElastic(ModAnimation.AniEasePower.Weak)),
								ModAnimation.AaX(closure_0024__6_002D2._0024VB_0024Local_NewHintControl, 20.0, 200, 0, new ModAnimation.AniEaseOutFluent()),
								ModAnimation.AaOpacity(closure_0024__6_002D2._0024VB_0024Local_NewHintControl, 1.0, 100),
								ModAnimation.AaDouble(closure_0024__6_002D2._Lambda_0024__3, 0.7, 250, 100)
							});
							ModAnimation.AniStart(list, Conversions.ToString(Operators.ConcatenateObject("Hint Show ", NewLateBinding.LateIndexGet(closure_0024__6_002D2._0024VB_0024Local_NewHintControl.Tag, new object[1] { 1 }, null))));
							double a2 = (double)(800 + MathUtils.Clamp<int>(result.Text.Length, 5, 23) * 180) * ModAnimation.AniSpeed;
							ModAnimation.AniStart(new ModAnimation.AniData[5]
							{
								ModAnimation.AaX(closure_0024__6_002D2._0024VB_0024Local_NewHintControl, -50.0, 200, (int)Math.Round(a2), new ModAnimation.AniEaseInFluent()),
								ModAnimation.AaOpacity(closure_0024__6_002D2._0024VB_0024Local_NewHintControl, -1.0, 150, (int)Math.Round(a2)),
								ModAnimation.AaCode(closure_0024__6_002D2._Lambda_0024__4, (int)Math.Round(a2)),
								ModAnimation.AaHeight(closure_0024__6_002D2._0024VB_0024Local_NewHintControl, -26.0, 100, 0, new ModAnimation.AniEaseOutFluent(), After: true),
								ModAnimation.AaCode(closure_0024__6_002D2._Lambda_0024__5, 0, After: true)
							}, Conversions.ToString(Operators.ConcatenateObject("Hint Hide ", NewLateBinding.LateIndexGet(closure_0024__6_002D2._0024VB_0024Local_NewHintControl.Tag, new object[1] { 1 }, null))));
						}
					}
					if (result.Log)
					{
						Logger.Info($"弹出提示：{result.Text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Info(ex2, "显示弹出提示失败", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				ProjectData.ClearProjectError();
			}
		}
	}

	private static void HideAllHint()
	{
		IEnumerator enumerator = FrmMain.PanHint.Children.GetEnumerator();
		try
		{
			_Closure_0024__7_002D0 closure_0024__7_002D = default(_Closure_0024__7_002D0);
			while (enumerator.MoveNext())
			{
				closure_0024__7_002D = new _Closure_0024__7_002D0(closure_0024__7_002D);
				closure_0024__7_002D._0024VB_0024Local_Control = (Border)enumerator.Current;
				closure_0024__7_002D._0024VB_0024Local_Control.IsHitTestVisible = false;
				ModAnimation.AniStart(new ModAnimation.AniData[5]
				{
					ModAnimation.AaX(closure_0024__7_002D._0024VB_0024Local_Control, -50.0, 200, 0, new ModAnimation.AniEaseInFluent()),
					ModAnimation.AaOpacity(closure_0024__7_002D._0024VB_0024Local_Control, -1.0, 150, 0, new ModAnimation.AniEaseInFluent()),
					ModAnimation.AaCode(closure_0024__7_002D._Lambda_0024__0),
					ModAnimation.AaHeight(closure_0024__7_002D._0024VB_0024Local_Control, -26.0, 100, 0, new ModAnimation.AniEaseOutFluent(), After: true),
					ModAnimation.AaCode(closure_0024__7_002D._Lambda_0024__1, 0, After: true)
				}, Conversions.ToString(Operators.ConcatenateObject("Hint Hide ", NewLateBinding.LateIndexGet(closure_0024__7_002D._0024VB_0024Local_Control.Tag, new object[1] { 1 }, null))));
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static int MyMsgBox(string Caption, string Title = "提示", string Button1 = "确定", string Button2 = "", string Button3 = "", bool IsWarn = false, bool HighLight = true, bool ForceWait = false, Action Button1Action = null, Action Button2Action = null, Action Button3Action = null)
	{
		MyMsgBoxConverter myMsgBoxConverter = new MyMsgBoxConverter
		{
			Type = MyMsgBoxType.Text,
			Button1 = Button1,
			Button2 = Button2,
			Button3 = Button3,
			Text = Caption,
			IsWarn = IsWarn,
			Title = Title,
			HighLight = HighLight,
			ForceWait = true,
			Button1Action = Button1Action,
			Button2Action = Button2Action,
			Button3Action = Button3Action
		};
		if (Button2.Length > 0 || ForceWait)
		{
			if (FrmMain == null || (FrmMain.PanMsg == null && ModBase.RunInUi()))
			{
				if (Button2.Length > 0)
				{
					switch (Interaction.MsgBox(Caption, (MsgBoxStyle)checked(((Button3.Length > 0) ? 3 : 4) + (IsWarn ? 16 : 32)), Title))
					{
					case MsgBoxResult.Yes:
						myMsgBoxConverter.Result = 1;
						break;
					case MsgBoxResult.No:
						myMsgBoxConverter.Result = 2;
						break;
					case MsgBoxResult.Cancel:
						myMsgBoxConverter.Result = 3;
						break;
					}
				}
				else
				{
					Interaction.MsgBox(Caption, (MsgBoxStyle)checked(0 + (IsWarn ? 16 : 32)), Title);
					myMsgBoxConverter.Result = 1;
				}
				Logger.Warn($"主窗体加载完成前出现意料外的等待弹窗：{Button1},{Button2},{Button3}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
			}
			else
			{
				WaitingMyMsgBox.Enqueue(myMsgBoxConverter);
				try
				{
					FrmMain.DragStop();
					if (ModBase.RunInUi())
					{
						MyMsgBoxTick();
					}
					ComponentDispatcher.PushModal();
					Dispatcher.PushFrame(myMsgBoxConverter.WaitFrame);
				}
				finally
				{
					ComponentDispatcher.PopModal();
				}
			}
			Logger.Info(string.Format("普通弹框返回：{0}", RuntimeHelpers.GetObjectValue(myMsgBoxConverter.Result ?? "null")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
			return Conversions.ToInteger(myMsgBoxConverter.Result);
		}
		WaitingMyMsgBox.Enqueue(myMsgBoxConverter);
		return 1;
	}

	public static string MyMsgBoxInput(string Title, string Text = null, string DefaultInput = null, Collection<Validate> ValidateRules = null, string HintText = "", string Button1 = "确定", string Button2 = "取消", bool IsWarn = false)
	{
		MyMsgBoxConverter myMsgBoxConverter = new MyMsgBoxConverter
		{
			Text = (Text ?? ""),
			HintText = (HintText ?? ""),
			Type = MyMsgBoxType.Input,
			ValidateRules = (ValidateRules ?? new Collection<Validate>()),
			Button1 = Button1,
			Button2 = Button2,
			Content = DefaultInput,
			IsWarn = IsWarn,
			Title = Title
		};
		WaitingMyMsgBox.Enqueue(myMsgBoxConverter);
		try
		{
			if (FrmMain != null)
			{
				FrmMain.DragStop();
			}
			if (ModBase.RunInUi())
			{
				MyMsgBoxTick();
			}
			ComponentDispatcher.PushModal();
			Dispatcher.PushFrame(myMsgBoxConverter.WaitFrame);
		}
		finally
		{
			ComponentDispatcher.PopModal();
		}
		Logger.Info(string.Format("输入弹框返回：{0}", RuntimeHelpers.GetObjectValue(myMsgBoxConverter.Result ?? "null")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
		return Conversions.ToString(myMsgBoxConverter.Result);
	}

	public static int? MyMsgBoxSelect(IEnumerable<IMyRadio> Selections, string Title = "提示", string Button1 = "确定", string Button2 = "", bool IsWarn = false)
	{
		MyMsgBoxConverter myMsgBoxConverter = new MyMsgBoxConverter
		{
			Type = MyMsgBoxType.Select,
			Button1 = Button1,
			Button2 = Button2,
			Content = Selections,
			IsWarn = IsWarn,
			Title = Title
		};
		WaitingMyMsgBox.Enqueue(myMsgBoxConverter);
		try
		{
			if (FrmMain != null)
			{
				FrmMain.DragStop();
			}
			if (ModBase.RunInUi())
			{
				MyMsgBoxTick();
			}
			ComponentDispatcher.PushModal();
			Dispatcher.PushFrame(myMsgBoxConverter.WaitFrame);
		}
		finally
		{
			ComponentDispatcher.PopModal();
		}
		Logger.Info(string.Format("选择弹框返回：{0}", RuntimeHelpers.GetObjectValue(myMsgBoxConverter.Result ?? "null")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
		return (int?)myMsgBoxConverter.Result;
	}

	public static void MyMsgBoxTick()
	{
		try
		{
			if (FrmMain?.PanMsg == null || FrmMain.WindowState == WindowState.Minimized)
			{
				return;
			}
			MyMsgBoxConverter result = null;
			if (FrmMain.PanMsg.Children.Count > 0)
			{
				FrmMain.PanMsg.Visibility = Visibility.Visible;
			}
			else if (WaitingMyMsgBox.TryDequeue(out result))
			{
				FrmMain.PanMsg.Visibility = Visibility.Visible;
				switch (result.Type)
				{
				case MyMsgBoxType.Input:
					FrmMain.PanMsg.Children.Add(new MyMsgInput(result));
					break;
				case MyMsgBoxType.Select:
					FrmMain.PanMsg.Children.Add(new MyMsgSelect(result));
					break;
				case MyMsgBoxType.Text:
					FrmMain.PanMsg.Children.Add(new MyMsgText(result));
					break;
				case MyMsgBoxType.Login:
					FrmMain.PanMsg.Children.Add(new MyMsgLogin(result));
					break;
				}
			}
			else if (FrmMain.PanMsg.Visibility != Visibility.Collapsed)
			{
				FrmMain.PanMsg.Visibility = Visibility.Collapsed;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "处理等待中的弹窗失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
			ProjectData.ClearProjectError();
		}
	}

	private static void HelpLoad(ModLoader.LoaderTask<int, List<HelpEntry>> Loader)
	{
		object helpLoadLock = HelpLoadLock;
		ObjectFlowControl.CheckForSyncLockOnValueType(helpLoadLock);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(helpLoadLock, ref lockTaken);
			try
			{
				HelpTryExtract();
				List<string> list = new List<string>();
				try
				{
					List<string> list2 = new List<string>();
					if (DirectoryUtils.Exists(Paths.Base + "PCL\\Help\\"))
					{
						foreach (string item in DirectoryUtils.EnumerateFiles(Paths.Base + "PCL\\Help\\", true, "*"))
						{
							string extension = PathUtils.GetExtension(item);
							if (Operators.CompareString(extension, "helpignore", TextCompare: false) != 0)
							{
								if (Operators.CompareString(extension, "json", TextCompare: false) == 0)
								{
									list.Add(item);
								}
								continue;
							}
							Logger.Info($"发现 .helpignore 文件：{item}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
							string[] array = FileUtils.ReadAsLines(item, true, (Encoding)null, (Type)null);
							for (int i = 0; i < array.Length; i = checked(i + 1))
							{
								string text = StringExtensions.BeforeFirst(array[i], "#", false).Trim();
								if (!string.IsNullOrWhiteSpace(text))
								{
									list2.Add(text);
									Logger.Trace($"> {text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
								}
							}
						}
					}
					Logger.Info($"已扫描 PCL 文件夹下的帮助文件，目前总计 {list.Count} 条", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
					foreach (string item2 in DirectoryUtils.EnumerateFiles(ModBase.PathTemp + "Help", true, "*"))
					{
						if (PathUtils.RemoveLastPart(item2).Substring((ModBase.PathTemp + "Help").Length).Contains("\\.") || Operators.CompareString(PathUtils.GetExtension(item2), "json", TextCompare: false) != 0)
						{
							continue;
						}
						string text2 = item2.Substring((ModBase.PathTemp + "Help\\").Length);
						foreach (string item3 in list2)
						{
							if (!StringExtensions.RegexCheck(text2, item3, RegexOptions.None))
							{
								continue;
							}
							Logger.Trace($"已忽略 {text2}：{item3}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
							goto IL_0241;
						}
						list.Add(item2);
						IL_0241:;
					}
					Logger.Info($"已扫描缓存文件夹下的帮助文件，目前总计 {list.Count} 条", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "检查帮助文件夹失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
					ProjectData.ClearProjectError();
				}
				if (Loader.IsCanceled)
				{
					return;
				}
				List<HelpEntry> list3 = new List<HelpEntry>();
				foreach (string item4 in list)
				{
					try
					{
						HelpEntry helpEntry = new HelpEntry(item4);
						list3.Add(helpEntry);
						Logger.Trace($"已加载的帮助条目：{helpEntry.Title} ← {item4}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						Logger.Error(ex4, $"初始化帮助条目失败（{item4}）", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
						ProjectData.ClearProjectError();
					}
				}
				if (!list3.Any())
				{
					throw new Exception("未找到可用的帮助；若不需要帮助页面，可以在 设置 → 个性化 → 功能隐藏 中将其隐藏");
				}
				if (!Loader.IsCanceled)
				{
					Loader.Output = list3;
				}
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Logger.Warn(ex6, "帮助列表初始化失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				throw;
			}
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(helpLoadLock);
			}
		}
	}

	public static void HelpTryExtract()
	{
		if (Settings.Get<int>("SystemHelpVersion") != 412 || !FileUtils.Exists(ModBase.PathTemp + "Help\\启动器\\备份设置.xaml"))
		{
			DirectoryUtils.Delete(ModBase.PathTemp + "Help", false);
			ModBase.ExtractResources(ModBase.PathTemp + "Cache\\Help.zip", "Help");
			FileUtils.ExtractToDirectory(ModBase.PathTemp + "Cache\\Help.zip", ModBase.PathTemp + "Help", default(CancellationToken), (ProgressProvider)null);
			Settings.Set("SystemHelpVersion", 412);
			Logger.Info(string.Format("已解压内置帮助文件，目前状态：{0}", FileUtils.Exists(ModBase.PathTemp + "Help\\启动器\\备份设置.xaml")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
		}
	}

	private static void TimerFool()
	{
		checked
		{
			try
			{
				if (FrmLaunchLeft == null || FrmLaunchLeft.AprilPosTrans == null || FrmMain.lastMouseArg == null || IsAprilGiveup || FrmMain.PageCurrent != FormMain.PageType.Launch || ModAnimation.AniControlEnabled != 0 || !FrmLaunchLeft.BtnLaunch.IsLoaded)
				{
					return;
				}
				Point position = FrmMain.lastMouseArg.GetPosition(FrmMain);
				if (_0024STATIC_0024TimerFool_0024001_0024IdieCount_0024Init == null)
				{
					Interlocked.CompareExchange(ref _0024STATIC_0024TimerFool_0024001_0024IdieCount_0024Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken = false;
				try
				{
					Monitor.Enter(_0024STATIC_0024TimerFool_0024001_0024IdieCount_0024Init, ref lockTaken);
					if (_0024STATIC_0024TimerFool_0024001_0024IdieCount_0024Init.State == 0)
					{
						_0024STATIC_0024TimerFool_0024001_0024IdieCount_0024Init.State = 2;
						_0024STATIC_0024TimerFool_0024001_0024IdieCount = 0;
					}
					else if (_0024STATIC_0024TimerFool_0024001_0024IdieCount_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024TimerFool_0024001_0024IdieCount_0024Init.State = 1;
					if (lockTaken)
					{
						Monitor.Exit(_0024STATIC_0024TimerFool_0024001_0024IdieCount_0024Init);
					}
				}
				if (_0024STATIC_0024TimerFool_0024001_0024MousePosLast_0024Init == null)
				{
					Interlocked.CompareExchange(ref _0024STATIC_0024TimerFool_0024001_0024MousePosLast_0024Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken2 = false;
				try
				{
					Monitor.Enter(_0024STATIC_0024TimerFool_0024001_0024MousePosLast_0024Init, ref lockTaken2);
					if (_0024STATIC_0024TimerFool_0024001_0024MousePosLast_0024Init.State == 0)
					{
						_0024STATIC_0024TimerFool_0024001_0024MousePosLast_0024Init.State = 2;
						_0024STATIC_0024TimerFool_0024001_0024MousePosLast = new Point(0.0, 0.0);
					}
					else if (_0024STATIC_0024TimerFool_0024001_0024MousePosLast_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024TimerFool_0024001_0024MousePosLast_0024Init.State = 1;
					if (lockTaken2)
					{
						Monitor.Exit(_0024STATIC_0024TimerFool_0024001_0024MousePosLast_0024Init);
					}
				}
				if (position == _0024STATIC_0024TimerFool_0024001_0024MousePosLast)
				{
					_0024STATIC_0024TimerFool_0024001_0024IdieCount++;
				}
				else
				{
					_0024STATIC_0024TimerFool_0024001_0024MousePosLast = position;
					_0024STATIC_0024TimerFool_0024001_0024IdieCount = 0;
				}
				double num = FrmLaunchLeft.BtnLaunch.ActualWidth / 2.0;
				double num2 = FrmLaunchLeft.BtnLaunch.ActualHeight / 2.0;
				Vector vector = (Vector)(FrmMain.lastMouseArg.GetPosition(FrmLaunchLeft.BtnLaunch) - new Vector(num, num2));
				Vector vector2 = new Vector(vector.X, vector.Y);
				vector2.Normalize();
				Vector vector3 = -vector2;
				double length = new Vector(Math.Max(0.0, Math.Abs(vector.X) - num), Math.Max(0.0, Math.Abs(vector.Y) - num2)).Length;
				Math.Sin((double)Timer150Count / 37.5 * Math.PI);
				double num3 = 1.0 / (1.0 + Math.Pow((double)AprilDifficultiedDistance / 6000.0, 2.0)) + 0.25;
				Vector vector4 = Math.Max(0.0, -0.65 - Math.Log((length / num3 + 0.4) / 200.0)) * vector3 * num3;
				if (_0024STATIC_0024TimerFool_0024001_0024IdieCount >= 320)
				{
					Vector vector5 = (Vector)(FrmMain.lastMouseArg.GetPosition(FrmMain.PanMain) - new Vector(num, FrmMain.PanMain.ActualHeight - num2 * 3.0));
					Vector vector6 = new Vector(FrmLaunchLeft.AprilPosTrans.X, FrmLaunchLeft.AprilPosTrans.Y);
					if (vector5.Length > 250.0 && vector6.Length > 0.4)
					{
						vector4 -= vector6 * 0.0005;
						vector6.Normalize();
						vector4 -= vector6 * 0.15;
					}
				}
				if (_0024STATIC_0024TimerFool_0024001_0024Speed_0024Init == null)
				{
					Interlocked.CompareExchange(ref _0024STATIC_0024TimerFool_0024001_0024Speed_0024Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken3 = false;
				try
				{
					Monitor.Enter(_0024STATIC_0024TimerFool_0024001_0024Speed_0024Init, ref lockTaken3);
					if (_0024STATIC_0024TimerFool_0024001_0024Speed_0024Init.State == 0)
					{
						_0024STATIC_0024TimerFool_0024001_0024Speed_0024Init.State = 2;
						_0024STATIC_0024TimerFool_0024001_0024Speed = new Vector(0.0, 0.0);
					}
					else if (_0024STATIC_0024TimerFool_0024001_0024Speed_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024TimerFool_0024001_0024Speed_0024Init.State = 1;
					if (lockTaken3)
					{
						Monitor.Exit(_0024STATIC_0024TimerFool_0024001_0024Speed_0024Init);
					}
				}
				Point point = FrmLaunchLeft.BtnLaunch.TranslatePoint(new Point(0.0, 0.0), FrmMain.PanForm);
				if (point.X < (0.0 - num) * 2.0)
				{
					FrmLaunchLeft.AprilPosTrans.X += FrmMain.PanForm.ActualWidth + num * 2.0;
					_0024STATIC_0024TimerFool_0024001_0024Speed.X -= 80.0;
					if (point.Y < 0.0)
					{
						FrmLaunchLeft.AprilPosTrans.Y += num2 * 2.5;
					}
					else if (point.Y > FrmMain.PanForm.ActualHeight - num2 * 2.0)
					{
						FrmLaunchLeft.AprilPosTrans.Y -= num2 * 2.5;
					}
				}
				else if (point.X > FrmMain.PanForm.ActualWidth)
				{
					FrmLaunchLeft.AprilPosTrans.X -= FrmMain.PanForm.ActualWidth + num * 2.0;
					_0024STATIC_0024TimerFool_0024001_0024Speed.X += 80.0;
					if (point.Y < 0.0)
					{
						FrmLaunchLeft.AprilPosTrans.Y += num2 * 2.5;
					}
					else if (point.Y > FrmMain.PanForm.ActualHeight - num2 * 2.0)
					{
						FrmLaunchLeft.AprilPosTrans.Y -= num2 * 2.5;
					}
				}
				else if (point.Y < (0.0 - num2) * 2.0)
				{
					FrmLaunchLeft.AprilPosTrans.Y += FrmMain.PanForm.ActualHeight + num2 * 2.0;
					_0024STATIC_0024TimerFool_0024001_0024Speed.Y -= 25.0;
					if (point.X < 0.0)
					{
						FrmLaunchLeft.AprilPosTrans.X += num * 2.0;
					}
					else if (point.X > FrmMain.PanForm.ActualWidth - num * 2.0)
					{
						FrmLaunchLeft.AprilPosTrans.X -= num * 2.0;
					}
				}
				else if (point.Y > FrmMain.PanForm.ActualHeight)
				{
					FrmLaunchLeft.AprilPosTrans.Y -= FrmMain.PanForm.ActualHeight + num2 * 2.0;
					_0024STATIC_0024TimerFool_0024001_0024Speed.Y += 25.0;
					if (point.X < 0.0)
					{
						FrmLaunchLeft.AprilPosTrans.X += num * 2.0;
					}
					else if (point.X > FrmMain.PanForm.ActualWidth - num * 2.0)
					{
						FrmLaunchLeft.AprilPosTrans.X -= num * 2.0;
					}
				}
				_0024STATIC_0024TimerFool_0024001_0024Speed = _0024STATIC_0024TimerFool_0024001_0024Speed * 0.8 + vector4;
				double num4 = Math.Min(60.0, _0024STATIC_0024TimerFool_0024001_0024Speed.Length);
				if (num4 < 0.01)
				{
					return;
				}
				_0024STATIC_0024TimerFool_0024001_0024Speed.Normalize();
				_0024STATIC_0024TimerFool_0024001_0024Speed *= num4;
				AprilDifficultiedDistance = (int)Math.Round((double)AprilDifficultiedDistance + num4);
				FrmLaunchLeft.AprilPosTrans.X += _0024STATIC_0024TimerFool_0024001_0024Speed.X;
				FrmLaunchLeft.AprilPosTrans.Y += _0024STATIC_0024TimerFool_0024001_0024Speed.Y;
				FrmLaunchLeft.AprilScaleTrans.ScaleX = MathUtils.Clamp<double>(1.0 - (Math.Abs(vector3.X) - Math.Abs(vector3.Y)) * (num4 / 160.0), 0.2, 1.8);
				FrmLaunchLeft.AprilScaleTrans.ScaleY = MathUtils.Clamp<double>(1.0 - (Math.Abs(vector3.Y) - Math.Abs(vector3.X)) * (num4 / 100.0), 0.2, 1.8);
				if (_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance_0024Init == null)
				{
					Interlocked.CompareExchange(ref _0024STATIC_0024TimerFool_0024001_0024GiveUpDistance_0024Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken4 = false;
				try
				{
					Monitor.Enter(_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance_0024Init, ref lockTaken4);
					if (_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance_0024Init.State == 0)
					{
						_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance_0024Init.State = 2;
						_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance = -1500.0;
					}
					else if (_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance_0024Init.State = 1;
					if (lockTaken4)
					{
						Monitor.Exit(_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance_0024Init);
					}
				}
				_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance += num4;
				if (_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance > 2500.0)
				{
					_0024STATIC_0024TimerFool_0024001_0024GiveUpDistance = 0.0;
					switch (ModBase.RandomInteger(0, 3))
					{
					case 0:
						Hint("放弃吧！只需要点一下右下角的小白旗……", HintType.Red);
						break;
					case 1:
						Hint("看到右下角的那面小白旗了吗？", HintType.Red);
						break;
					case 2:
						Hint("这里建议点一下右下角的小白旗投降呢.jpg", HintType.Red);
						break;
					case 3:
						Hint("右下角的小白旗永远等着你……", HintType.Red);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "愚人节移动出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void ShowWindowToTop(IntPtr Handle)
	{
		try
		{
			PostMessage(Handle, 6402u, 0L, 0L);
			SetForegroundWindow(Handle);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "设置窗口置顶失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
			ProjectData.ClearProjectError();
		}
	}

	[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "FindWindowA", ExactSpelling = true, SetLastError = true)]
	public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.VBByRefStr)] ref string ClassName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string WindowName);

	[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern int SetForegroundWindow(IntPtr hWnd);

	[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "PostMessageA", ExactSpelling = true, SetLastError = true)]
	private static extern bool PostMessage(IntPtr hWnd, uint msg, long wParam, long lParam);

	public static void SetGPUPreference(string Executeable)
	{
		if (Operators.CompareString(RegistryUtils.Read("HKEY_CURRENT_USER\\Software\\Microsoft\\DirectX\\UserGpuPreferences", Executeable).Item1?.ToString(), "GpuPreference=2;", TextCompare: false) == 0)
		{
			Logger.Info($"无需调整显卡设置：{Executeable}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
			return;
		}
		RegistryUtils.Write("HKEY_CURRENT_USER\\Software\\Microsoft\\DirectX\\UserGpuPreferences", Executeable, (object)"GpuPreference=2;");
		Logger.Info($"已调整显卡设置：{Executeable}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
	}

	public static string ArgumentReplace(string Text, Func<string, string> EscapeHandler = null, bool ReplaceTime = true)
	{
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		if (Text == null || !Text.Contains("{"))
		{
			return Text;
		}
		if (_0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer_0024Init == null)
		{
			Interlocked.CompareExchange(ref _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer_0024Init, new StaticLocalInitFlag(), null);
		}
		bool lockTaken = false;
		try
		{
			Monitor.Enter(_0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer_0024Init, ref lockTaken);
			if (_0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer_0024Init.State == 0)
			{
				_0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer_0024Init.State = 2;
				_0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer = [SpecialName] (string s) =>
				{
					if (s == null)
					{
						return "";
					}
					if (EscapeHandler == null)
					{
						return s;
					}
					if (s.Contains(":\\"))
					{
						bool num = StringExtensions.EndsWithF(s, "\\", false) || StringExtensions.EndsWithF(s, "/", false);
						s = PathUtils.ToShortPath(s);
						if (num)
						{
							s = PathUtils.AddSlashSuffix(s);
						}
					}
					return EscapeHandler(s);
				};
			}
			else if (_0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer_0024Init.State == 2)
			{
				throw new IncompleteInitialization();
			}
		}
		finally
		{
			_0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer_0024Init.State = 1;
			if (lockTaken)
			{
				Monitor.Exit(_0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer_0024Init);
			}
		}
		Text = Text.Replace("{pcl_version}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer("2.14.0.0"));
		Text = Text.Replace("{pcl_version_code}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(Conversions.ToString(412)));
		Text = Text.Replace("{pcl_version_branch}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer("快照版"));
		Text = Text.Replace("{pcl_build_type}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModBase.BuildTypes.Snapshot.ToString()));
		Text = Text.Replace("{pcl_branch}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer("Official"));
		Text = Text.Replace("{identify}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModBase.Identify));
		Text = Text.Replace("{path}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(Paths.Base));
		Text = Text.Replace("{path_with_name}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModBase.PathExe));
		Text = Text.Replace("{path_temp}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModBase.PathTemp));
		Text = StringExtensions.Replace(Text, "{pcl_md5}", (Func<string>)([SpecialName] () => _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(CryptographyUtils.ComputeFileHash(ModBase.PathExe, (HashMethod)0))));
		Text = StringExtensions.Replace(Text, "{pcl_sha1}", (Func<string>)([SpecialName] () => _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(CryptographyUtils.ComputeFileHash(ModBase.PathExe, (HashMethod)1))));
		if (ReplaceTime)
		{
			Text = Text.Replace("{date}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(DateTime.Now.ToString("yyyy'/'M'/'d")));
			Text = Text.Replace("{time}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(DateTime.Now.ToString("HH':'mm':'ss")));
		}
		Text = Text.Replace("{java}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModLaunch.McLaunchJavaSelected?.Folder));
		Text = Text.Replace("{minecraft}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModMinecraft.McFolderSelected));
		ModMinecraft.McInstance mcInstanceSelected = ModMinecraft.McInstanceSelected;
		if ((object)mcInstanceSelected != null && mcInstanceSelected.IsLoaded)
		{
			Text = Text.Replace("{version_path}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModMinecraft.McInstanceSelected.PathVersion));
			Text = Text.Replace("{verpath}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModMinecraft.McInstanceSelected.PathVersion));
			Text = Text.Replace("{version_indie}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModMinecraft.McInstanceSelected.PathIndie));
			Text = Text.Replace("{verindie}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModMinecraft.McInstanceSelected.PathIndie));
			Text = Text.Replace("{name}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModMinecraft.McInstanceSelected.Name));
			Text = ((!new string[3] { "unknown", "old", "pending" }.Contains(StringExtensions.Lower(ModMinecraft.McInstanceSelected.Version.VanillaName))) ? Text.Replace("{version}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModMinecraft.McInstanceSelected.Version.VanillaName)) : Text.Replace("{version}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModMinecraft.McInstanceSelected.Name)));
			Text = Text.Replace("{forge}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(EnumUtils.HasFlagF<ModLoaders>(ModMinecraft.McInstanceSelected.Version.ModLoaders, (ModLoaders)1) ? ModMinecraft.McInstanceSelected.Version.ModLoaderVersions[(ModLoaders)1] : ""));
			Text = Text.Replace("{fabric}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(EnumUtils.HasFlagF<ModLoaders>(ModMinecraft.McInstanceSelected.Version.ModLoaders, (ModLoaders)4) ? ModMinecraft.McInstanceSelected.Version.ModLoaderVersions[(ModLoaders)4] : ""));
			Text = Text.Replace("{neoforge}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(EnumUtils.HasFlagF<ModLoaders>(ModMinecraft.McInstanceSelected.Version.ModLoaders, (ModLoaders)16) ? ModMinecraft.McInstanceSelected.Version.ModLoaderVersions[(ModLoaders)16] : ""));
			Text = Text.Replace("{liteloader}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(EnumUtils.HasFlagF<ModLoaders>(ModMinecraft.McInstanceSelected.Version.ModLoaders, (ModLoaders)2) ? "true" : ""));
		}
		else
		{
			Text = Text.Replace("{version_path}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{verpath}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{version_indie}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{verindie}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{name}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{version}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{forge}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{fabric}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{neoforge}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{liteloader}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
		}
		if (ModLaunch.McLoginLoader.State == ModBase.LoadState.Finished)
		{
			Text = Text.Replace("{user}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(ModLaunch.McLoginLoader.Output.Name));
			string text = Text;
			Func<string, string> func = _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer;
			string uuid = ModLaunch.McLoginLoader.Output.Uuid;
			Text = text.Replace("{uuid}", func((uuid != null) ? StringExtensions.Lower(uuid) : null));
			switch (ModLaunch.McLoginLoader.Input.Type)
			{
			case ModLaunch.McLoginType.Legacy:
				Text = Text.Replace("{login}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer("离线"));
				break;
			case ModLaunch.McLoginType.Ms:
				Text = Text.Replace("{login}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer("正版"));
				break;
			case ModLaunch.McLoginType.Nide:
				Text = Text.Replace("{login}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer("统一通行证"));
				break;
			case ModLaunch.McLoginType.Auth:
				Text = Text.Replace("{login}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer("Authlib-Injector"));
				break;
			}
		}
		else
		{
			Text = Text.Replace("{user}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{uuid}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
			Text = Text.Replace("{login}", _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(null));
		}
		Text = StringExtensions.RegexReplace(Text, "\\{hint\\}", (MatchEvaluator)([SpecialName] (Match a0) => ((VB_0024AnonymousDelegate_0<string>)([SpecialName] () => _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(PageOtherTest.GetRandomHint())))()), RegexOptions.None);
		Text = StringExtensions.RegexReplace(Text, "\\{cave\\}", (MatchEvaluator)([SpecialName] (Match a0) => ((VB_0024AnonymousDelegate_0<string>)([SpecialName] () => _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(PageOtherTest.GetRandomCave())))()), RegexOptions.None);
		Text = StringExtensions.RegexReplace(Text, "\\{setup:([a-zA-Z0-9]+)\\}", (MatchEvaluator)([SpecialName] (Match m) => _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(Conversions.ToString(Settings.GetSafe(m.Groups[1].Value, ModMinecraft.McInstanceSelected)))), RegexOptions.None);
		Text = StringExtensions.RegexReplace(Text, "\\{varible:([^:\\}]+)(?::([^\\}]+))?\\}", (MatchEvaluator)([SpecialName] (Match m) => _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(Conversions.ToString(RegistryUtils.TryRead(string.Format("HKEY_CURRENT_USER\\Software\\{0}", "PCL"), $"CustomEvent{m.Groups[1].Value}", (object)m.Groups[2].Value)))), RegexOptions.None);
		Text = StringExtensions.RegexReplace(Text, "\\{variable:([^:\\}]+)(?::([^\\}]+))?\\}", (MatchEvaluator)([SpecialName] (Match m) => _0024STATIC_0024ArgumentReplace_002403EE151280F92EE2_0024Replacer(Conversions.ToString(RegistryUtils.TryRead(string.Format("HKEY_CURRENT_USER\\Software\\{0}", "PCL"), $"CustomEvent{m.Groups[1].Value}", (object)m.Groups[2].Value)))), RegexOptions.None);
		return Text;
	}

	public static void TryClearTaskTemp()
	{
		if (!IsTaskTempCleared)
		{
			IsTaskTempCleared = true;
			IsTaskTempClearing = true;
			try
			{
				Logger.Info("开始清理任务缓存文件夹", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				DirectoryUtils.Delete($"{ModBase.OsDrive}ProgramData\\PCL\\TaskTemp\\", false);
				DirectoryUtils.Delete($"{ModBase.PathTemp}TaskTemp\\", false);
				Logger.Info("已清理任务缓存文件夹", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				return;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "清理任务缓存文件夹失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				ProjectData.ClearProjectError();
				return;
			}
			finally
			{
				IsTaskTempClearing = false;
			}
		}
		if (IsTaskTempClearing)
		{
			while (IsTaskTempClearing)
			{
				Thread.Sleep(1);
			}
		}
	}

	public static string RequestTaskTempFolder(bool RequireNonSpace = false)
	{
		TryClearTaskTemp();
		string text;
		try
		{
			text = $"{ModBase.PathTemp}TaskTemp\\{ModBase.GetUuid()}-{ModBase.RandomInteger(0, 1000000)}\\";
			if (!RequireNonSpace || !text.Contains(" "))
			{
				DirectoryUtils.Create(text);
				ModBase.CheckPermissionWithException(text);
				return text;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		text = $"{ModBase.OsDrive}ProgramData\\PCL\\TaskTemp\\{ModBase.GetUuid()}-{ModBase.RandomInteger(0, 1000000)}\\";
		DirectoryUtils.Create(text);
		ModBase.CheckPermission(text);
		return text;
	}

	public static void Feedback(bool ShowMsgbox = true, bool ForceOpenLog = false)
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
				case 137:
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
							goto IL_000e;
						case 4:
							goto IL_003d;
						case 5:
							goto end_IL_0000_2;
						default:
							goto end_IL_0000;
						case 6:
							goto end_IL_0000_3;
						}
						goto default;
					}
					IL_003d:
					num = 4;
					ModBase.OpenExplorer(Paths.Base + "PCL\\Log1.txt");
					break;
					IL_0007:
					num = 2;
					FeedbackInfo();
					goto IL_000e;
					IL_000e:
					num = 3;
					if (!ForceOpenLog && (!ShowMsgbox || MyMsgBox("若你在汇报一个 Bug，请点击 打开文件夹 按钮，并上传 Log(1~5).txt 中包含错误信息的文件。\r\n游戏崩溃一般与启动器无关，请不要因为游戏崩溃而提交反馈。", "反馈提交提醒", "打开文件夹", "不需要") != 1))
					{
						break;
					}
					goto IL_003d;
					end_IL_0000_2:
					break;
				}
				num = 5;
				ModBase.OpenWebsite("https://github.com/Meloong-Git/PCL/issues/");
				break;
				end_IL_0000:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num2 == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0000_dispatch = 137;
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

	public static void FeedbackInfo()
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
					break;
				case 338:
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
							goto end_IL_0000_2;
						default:
							goto end_IL_0000;
						case 3:
							goto end_IL_0000_3;
						}
						goto default;
					}
					end_IL_0000_2:
					break;
				}
				num = 2;
				Logger.Warn(string.Format("诊断信息：{0}", "\r\n") + "操作系统：" + MyWpfExtension.Computer.Info.OSFullName + "\r\n剩余内存：" + Conversions.ToString(Conversion.Int((double)MyWpfExtension.Computer.Info.AvailablePhysicalMemory / 1024.0 / 1024.0)) + " M / " + Conversions.ToString(Conversion.Int((double)MyWpfExtension.Computer.Info.TotalPhysicalMemory / 1024.0 / 1024.0)) + " M\r\nDPI：" + Conversions.ToString(ModBase.DPI) + "（" + Conversions.ToString(Math.Round((double)ModBase.DPI / 96.0, 2) * 100.0) + "%）\r\nMC 文件夹：" + (ModMinecraft.McFolderSelected ?? "Nothing") + "\r\n文件位置：" + Paths.Base, (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				break;
				end_IL_0000:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num2 == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try0000_dispatch = 338;
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

	public static void Telemetry(string Event, params string[] Datas)
	{
		_Closure_0024__78_002D0 arg = default(_Closure_0024__78_002D0);
		_Closure_0024__78_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__78_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Event = Event;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Datas = Datas;
		if (!Settings.Get<bool>("SystemSystemTelemetry") || !StringExtensions.StartsWithF("https://ap-shanghai.cls.tencentcs.com/track?topic_id=pcl-remote-1253424809", "http", false))
		{
			return;
		}
		ModBase.RunInNewThread(checked([SpecialName] () =>
		{
			try
			{
				Logger.Info($"匿名数据上报：{CS_0024_003C_003E8__locals0._0024VB_0024Local_Event}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				string text = string.Format("{0}&Event={1}", "https://ap-shanghai.cls.tencentcs.com/track?topic_id=pcl-remote-1253424809", StringUtils.UrlEscape(CS_0024_003C_003E8__locals0._0024VB_0024Local_Event));
				int num = CS_0024_003C_003E8__locals0._0024VB_0024Local_Datas.Length - 1;
				for (int i = 0; i <= num; i += 2)
				{
					text = text + "&" + StringUtils.UrlEscape(CS_0024_003C_003E8__locals0._0024VB_0024Local_Datas[i]) + "=" + StringUtils.UrlEscape(StringExtensions.ReplaceLineEndings(CS_0024_003C_003E8__locals0._0024VB_0024Local_Datas[i + 1], "\n", true));
				}
				ModNet.NetRequestByClient(text, null, null, null, "*/*", 25000, null, RequireJson: false, null, SimulateBrowserHeaders: false, MakeLog: false);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "匿名数据上报失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				ProjectData.ClearProjectError();
			}
		}), "Telemetry", ThreadPriority.Lowest);
	}

	private static void TimerMain()
	{
		try
		{
			HintTick();
			MyMsgBoxTick();
			FrmMain.DragTick();
			ModLoader.LoaderTaskbarProgressRefresh();
			if (ModSecret.ThemeDontClick == 2)
			{
				ModSecret.ThemeRefresh();
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "短程主时钟执行异常", (LogBehavior)5, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
			ProjectData.ClearProjectError();
		}
		checked
		{
			Timer4Count++;
			if (Timer4Count == 4)
			{
				Timer4Count = 0;
				try
				{
					if (ModSecret.ThemeNow == 12)
					{
						ModSecret.ThemeRefresh();
					}
				}
				catch (Exception ex3)
				{
					ProjectData.SetProjectError(ex3);
					Exception ex4 = ex3;
					Logger.Warn(ex4, "中程主时钟执行异常", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
					ProjectData.ClearProjectError();
				}
			}
			Timer150Count++;
			if (Timer150Count != 150)
			{
				return;
			}
			Timer150Count = 0;
			try
			{
				if (FrmMain.BtnExtraApril_ShowCheck() && AprilDifficultiedDistance != 0)
				{
					FrmMain.BtnExtraApril.Ribble();
				}
				ModBase.RunInUi([SpecialName] () =>
				{
					if (!FrmMain.Hidden)
					{
						if (FrmMain.Top < -9000.0)
						{
							FrmMain.Top = 100.0;
						}
						if (FrmMain.Left < -9000.0)
						{
							FrmMain.Left = 100.0;
						}
					}
				});
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				Logger.Error(ex6, "长程主时钟执行异常", (LogBehavior)5, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void TimerMainStart()
	{
		ModBase.RunInNewThread([SpecialName] () =>
		{
			try
			{
				while (true)
				{
					ModBase.RunInUiWait(TimerMain);
					Thread.Sleep(49);
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Error(ex4, "程序主时钟出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				ProjectData.ClearProjectError();
			}
		}, "Timer Main");
		if (!IsAprilEnabled)
		{
			return;
		}
		ModBase.RunInNewThread([SpecialName] () =>
		{
			try
			{
				int tickCount = MyWpfExtension.Computer.Clock.TickCount;
				while (true)
				{
					if (tickCount != MyWpfExtension.Computer.Clock.TickCount)
					{
						tickCount = MyWpfExtension.Computer.Clock.TickCount;
						ModBase.RunInUiWait(TimerFool);
					}
					Thread.Sleep(1);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "愚人节主时钟出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModMain.vb");
				ProjectData.ClearProjectError();
			}
		}, "Timer Main Fool");
	}
}
