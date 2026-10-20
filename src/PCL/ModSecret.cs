using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using PCL.My;
using PCLCS;

namespace PCL;

[StandardModule]
internal sealed class ModSecret
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__36_002D0
	{
		public bool _0024VB_0024Local_EffectSetup;

		public _Closure_0024__36_002D0(_Closure_0024__36_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_EffectSetup = arg0._0024VB_0024Local_EffectSetup;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			checked
			{
				try
				{
					int num = Settings.Get<int>("UiLauncherTheme");
					if (!ThemeCheckOne(num))
					{
						Logger.Warn($"检测到尚未解锁的主题：{num}，已重置为默认主题", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						Settings.Set("UiLauncherTheme", 0);
					}
					if (!_0024VB_0024Local_EffectSetup || ModMain.FrmSetupUI == null)
					{
						return;
					}
					List<int> list = new List<int>();
					int num2 = 0;
					do
					{
						if (ThemeCheckOne(num2))
						{
							list.Add(num2);
						}
						num2++;
					}
					while (num2 <= 14);
					list.Add(42);
					ModAnimation.AniControlEnabled++;
					if (list.Contains(5))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme5.Opacity = 1.0;
						ModMain.FrmSetupUI.LabLauncherTheme5Unlock.Visibility = Visibility.Collapsed;
						ModMain.FrmSetupUI.RadioLauncherTheme5Gray.Visibility = Visibility.Collapsed;
					}
					if (list.Contains(6))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme6.IsEnabled = true;
					}
					if (list.Contains(7))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme7.IsEnabled = true;
					}
					if (list.Contains(8))
					{
						ModMain.FrmSetupUI.LabLauncherTheme8Copy.Visibility = Visibility.Collapsed;
						ModMain.FrmSetupUI.RadioLauncherTheme8.IsEnabled = true;
					}
					if (list.Contains(9))
					{
						ModMain.FrmSetupUI.LabLauncherTheme9Copy.Visibility = Visibility.Collapsed;
						ModMain.FrmSetupUI.RadioLauncherTheme9.IsEnabled = true;
					}
					if (list.Contains(10))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme10.IsEnabled = true;
					}
					if (list.Contains(11))
					{
						ModMain.FrmSetupUI.LabLauncherTheme11Click.Visibility = Visibility.Collapsed;
						ModMain.FrmSetupUI.RadioLauncherTheme11.IsEnabled = true;
					}
					else if (!Information.IsNothing(ModMain.FrmSetupUI) && !Information.IsNothing(ModMain.FrmSetupUI.RadioLauncherTheme11) && ModMain.FrmSetupUI.RadioLauncherTheme11.IsLoaded)
					{
						int num3 = 0;
						foreach (int item in list)
						{
							if (item >= 5 && item <= 14)
							{
								num3++;
							}
						}
						if (num3 < 3)
						{
							ModMain.FrmSetupUI.LabLauncherTheme11Click.ToolTip = "需要解锁三个隐藏主题";
						}
						else
						{
							ModMain.FrmSetupUI.LabLauncherTheme11Click.ToolTip = "点击打开解密游戏入口";
						}
					}
					if (list.Contains(12))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme12.IsEnabled = true;
					}
					if (list.Contains(13))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme13.IsEnabled = true;
					}
					if (list.Contains(14))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme14.IsEnabled = true;
					}
					ModAnimation.AniControlEnabled--;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if ((ex2.InnerException ?? ex2) is FormatException)
					{
						Logger.Error(ex2, "解锁的主题列表存档已损坏，主题解锁已被重置", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						Settings.Set("UiLauncherThemeHide2", "");
					}
					else
					{
						Logger.Error(ex2, "检查主题失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					}
					ProjectData.ClearProjectError();
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__45_002D0
	{
		public string _0024VB_0024Local_NewFilePath;

		public _Closure_0024__45_002D0(_Closure_0024__45_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_NewFilePath = arg0._0024VB_0024Local_NewFilePath;
			}
		}

		[SpecialName]
		internal string _Lambda_0024__0(string Reason)
		{
			string[] obj = new string[5]
			{
				string.Format("由于{0}，PCL 无法完成更新。{1}请依次尝试：{2}", Reason, "\r\n", "\r\n"),
				(PathUtils.IsParentOf(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Paths.Base) || PathUtils.IsParentOf(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Paths.Base)) ? " - 将 PCL 文件移动到桌面、文档、C 盘以外的文件夹（这或许可以一劳永逸地解决权限问题）\r\n" : "",
				null,
				null,
				null
			};
			char? diskName = PathUtils.GetDiskName(Paths.Base);
			obj[2] = ((diskName.HasValue ? new bool?(diskName.GetValueOrDefault() == 'C') : null).GetValueOrDefault() ? " - 将 PCL 文件移动到 C 盘以外的文件夹（这或许可以一劳永逸地解决权限问题）\r\n" : "");
			obj[3] = ((!WindowsUtils.HasAdminRole()) ? " - 右键 PCL，选择以管理员身份运行\r\n" : "");
			obj[4] = string.Format(" - 手动剪切 PCL 文件夹中的新版本程序，覆盖原程序：{0}   将位于 {1}{2}   的程序{3}   剪切到 {4} 并覆盖即可。", "\r\n", ModBase.PathExe, "\r\n", "\r\n", _0024VB_0024Local_NewFilePath);
			return string.Concat(obj);
		}
	}

	public const string VersionBranchMain = "Official";

	public const string RegFolder = "PCL";

	public const string OAuthClientId = "fe72edc2-3a6f-4280-90e8-e2beb64ce7e1";

	public const string ClsBaseUrl = "https://ap-shanghai.cls.tencentcs.com/track?topic_id=pcl-remote-1253424809";

	public static ModBase.MyColor Color1 = new ModBase.MyColor(52.0, 61.0, 74.0);

	public static ModBase.MyColor Color2 = new ModBase.MyColor(11.0, 91.0, 203.0);

	public static ModBase.MyColor Color3 = new ModBase.MyColor(19.0, 112.0, 243.0);

	public static ModBase.MyColor Color4 = new ModBase.MyColor(72.0, 144.0, 245.0);

	public static ModBase.MyColor Color5 = new ModBase.MyColor(150.0, 192.0, 249.0);

	public static ModBase.MyColor Color6 = new ModBase.MyColor(213.0, 230.0, 253.0);

	public static ModBase.MyColor Color7 = new ModBase.MyColor(222.0, 236.0, 253.0);

	public static ModBase.MyColor Color8 = new ModBase.MyColor(234.0, 242.0, 254.0);

	public static ModBase.MyColor ColorBg0 = new ModBase.MyColor(150.0, 192.0, 249.0);

	public static ModBase.MyColor ColorBg1 = new ModBase.MyColor(190.0, Color7);

	public static ModBase.MyColor ColorGray1 = new ModBase.MyColor(64.0, 64.0, 64.0);

	public static ModBase.MyColor ColorGray2 = new ModBase.MyColor(115.0, 115.0, 115.0);

	public static ModBase.MyColor ColorGray3 = new ModBase.MyColor(140.0, 140.0, 140.0);

	public static ModBase.MyColor ColorGray4 = new ModBase.MyColor(166.0, 166.0, 166.0);

	public static ModBase.MyColor ColorGray5 = new ModBase.MyColor(204.0, 204.0, 204.0);

	public static ModBase.MyColor ColorGray6 = new ModBase.MyColor(235.0, 235.0, 235.0);

	public static ModBase.MyColor ColorGray7 = new ModBase.MyColor(240.0, 240.0, 240.0);

	public static ModBase.MyColor ColorGray8 = new ModBase.MyColor(245.0, 245.0, 245.0);

	public static ModBase.MyColor ColorSemiTransparent = new ModBase.MyColor(1.0, Color8);

	public static int ThemeNow = -1;

	public static int ColorHue = 210;

	public static int ColorSat = 85;

	public static int ColorLightAdjust = 0;

	public static OneOf<int, int[]> ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);

	public static int ThemeDontClick = 0;

	private static bool IsThemeColorfulLoaded = false;

	private static int ColorHueDelta = 1;

	private static bool IsUpdating = false;

	internal static bool IsUpdateWaitingRestart = false;

	private static bool IsUpdateRestartWaitingTasks = false;

	private static string UpdateMark = "EmptyMark********************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************";

	public static JObject ServerConfig;

	public static ModLoader.LoaderTask<int, int> ServerLoader = new ModLoader.LoaderTask<int, int>("PCL 配置更新", ServerSub, null, ThreadPriority.BelowNormal)
	{
		ReloadTimeout = 3600000
	};

	private static List<string> _PotatoFeatures = null;

	private const string EcdsaPublicKey = "RUNTMSAAAAC4QTUNAewh23Q4Q6koHkyIrDIIZUSbua23sf2DiZmIRwSzadISDRyTVTbuWniH3KR7rKj8XBsabms1be6i3c+S";

	[SpecialName]
	private static bool _0024STATIC_0024UpdateCheckByButton_0024001_0024IsUiCheckingUpdate;

	[SpecialName]
	private static StaticLocalInitFlag _0024STATIC_0024UpdateCheckByButton_0024001_0024IsUiCheckingUpdate_0024Init;

	[SpecialName]
	private static bool _0024STATIC_0024UpdateStart_00240212151280E91E_0024IsSilentUpdate;

	public static DonationRank CurrentRank
	{
		get
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			DonationRank[] array = new DonationRank[5];
			RuntimeHelpers.InitializeArray(array, (RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/);
			DonationRank[] array2 = (DonationRank[])(object)array;
			for (int i = 0; i < array2.Length; i = checked(i + 1))
			{
				DonationRank result = array2[i];
				if (PotatoFeatures.Contains(((Enum)(DonationRank)(ref result)).ToString()))
				{
					return result;
				}
			}
			return (DonationRank)0;
		}
	}

	private static bool HasUpdatePermission
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Invalid comparison between Unknown and I4
			if ((int)CurrentRank < 12)
			{
				return PotatoFeatures.Contains("Update");
			}
			return true;
		}
	}

	private static List<string> PotatoFeatures
	{
		get
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			if (_PotatoFeatures == null)
			{
				_PotatoFeatures = new List<string>();
				string[] array = StringExtensions.Split(Settings.Get<string>("Potatoes").ToString(), '|', true);
				for (int i = 0; i < array.Length; i = checked(i + 1))
				{
					OneOf<List<string>, string> val = CheckPotatoCode(array[i]);
					if (val.Is<List<string>>())
					{
						_PotatoFeatures.AddRange(val.As<List<string>>());
					}
				}
				_PotatoFeatures = _PotatoFeatures.Distinct().ToList();
			}
			return _PotatoFeatures;
		}
	}

	internal static string SecretCdnSign(string UrlWithMark)
	{
		if (ModBase.ApplicationStartTick < 1)
		{
			return UrlWithMark;
		}
		if (!StringExtensions.EndsWithF(UrlWithMark, "{CDN}", false))
		{
			return UrlWithMark;
		}
		string text = UrlWithMark.Replace("{CDN}", "").Replace(" ", "%20");
		string text2 = StringExtensions.EnsureLength(ModBase.RandomInteger(0, 2147483645).ToString("x"), '0', 8);
		string text3 = Conversions.ToString(ModBase.GetUnixTimestampUtc());
		string text4 = text.Substring(checked(StringExtensions.IndexOfF(text, "://", false) + 3));
		text4 = text4.Substring(StringExtensions.IndexOfF(text4, "/", false));
		string text5 = CryptographyUtils.ComputeHash(EnumerableExtensions.Join((IEnumerable)new string[5] { text4, text3, text2, "0", "PCL2Server" }, "-"), (HashMethod)0);
		return text + (text.Contains("?") ? "&" : "?") + "sign=" + EnumerableExtensions.Join((IEnumerable)new string[4] { text3, text2, "0", text5 }, "-");
	}

	internal static void SecretHeadersSign(string Url, ref HttpRequestMessage Req, bool SimulateBrowserHeaders = false)
	{
		if (ModBase.ApplicationStartTick < 1)
		{
			return;
		}
		if (!((IEnumerable<ProductInfoHeaderValue>)Req.Headers.UserAgent).Any())
		{
			if (Url.Contains("baidupcs.com") || Url.Contains("baidu.com"))
			{
				((HttpHeaders)Req.Headers).Add("User-Agent", "LogStatistic");
			}
			else if (SimulateBrowserHeaders)
			{
				((HttpHeaders)Req.Headers).Add("User-Agent", string.Format("PCL2/{0}.{1} Mozilla/5.0 AppleWebKit/537.36 Chrome/63.0.3239.132 Safari/537.36", "2.14.0.0", 0));
			}
			else
			{
				((HttpHeaders)Req.Headers).Add("User-Agent", string.Format("PCL2/{0}.{1}", "2.14.0.0", 0));
			}
		}
		if (!SimulateBrowserHeaders)
		{
			Req.Headers.Referrer = new Uri($"http://{412}.pcl2.server/");
		}
		if (Url.Contains("api.curseforge.com") || Url.Contains("forgecdn.net"))
		{
			((HttpHeaders)Req.Headers).Add("x-api-key", Encoding.Default.GetString(Convert.FromBase64String("JDJhJDEwJDdXV1YxV0VMY3dYOFhwN2Q2OE1Icy41Z3JUeVpWYTZTeW55ZVN5TWgxcnNFODluSWEwOXpP")));
		}
	}

	public static void ThemeRefresh(int NewTheme = -1)
	{
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		checked
		{
			try
			{
				if ((ThemeNow == NewTheme && NewTheme >= 0) || !ThemeCheckOne((NewTheme >= 0) ? NewTheme : ThemeNow))
				{
					return;
				}
				if (NewTheme >= 0)
				{
					ThemeNow = NewTheme;
				}
				switch (ThemeNow)
				{
				case 10:
					ColorHue = 5;
					ColorSat = 70;
					ColorLightAdjust = -5;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 9:
					ColorHue = 25;
					ColorSat = 75;
					ColorLightAdjust = 5;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 4:
					ColorHue = 35;
					ColorSat = 30;
					ColorLightAdjust = 0;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 8:
					ColorHue = 50;
					ColorSat = 65;
					ColorLightAdjust = 10;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 3:
					ColorHue = 75;
					ColorSat = 50;
					ColorLightAdjust = 0;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 2:
					ColorHue = 130;
					ColorSat = 40;
					ColorLightAdjust = 0;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(new int[3] { 5, -5, 5 });
					break;
				case 42:
					ColorHue = 225;
					ColorSat = 60;
					ColorLightAdjust = -15;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 1:
					ColorHue = 180;
					ColorSat = 50;
					ColorLightAdjust = 0;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 0:
					ColorHue = 210;
					ColorSat = 85;
					ColorLightAdjust = 0;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 5:
					ColorHue = 225;
					ColorSat = 10;
					ColorLightAdjust = -20;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 11:
					ColorHue = 230;
					ColorSat = 60;
					ColorLightAdjust = -5;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 7:
					ColorHue = 285;
					ColorSat = 50;
					ColorLightAdjust = 0;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 13:
					ColorHue = 290;
					ColorSat = 50;
					ColorLightAdjust = 0;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(new int[3] { -30, 10, 50 });
					break;
				case 6:
					ColorHue = 345;
					ColorSat = 70;
					ColorLightAdjust = 15;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(0);
					break;
				case 12:
					if (!IsThemeColorfulLoaded)
					{
						IsThemeColorfulLoaded = true;
						ColorHue = ModBase.RandomInteger(0, 359);
						ColorHueDelta = (ModBase.RandomInteger(0, 1) * 2 - 1) * 4;
					}
					unchecked
					{
						ColorHue = checked(ColorHue + ColorHueDelta) % 360;
						ColorSat = 45;
						ColorLightAdjust = 0;
						break;
					}
				case 14:
					ColorHue = Settings.Get<int>("UiLauncherHue");
					ColorSat = Settings.Get<int>("UiLauncherSat");
					ColorLightAdjust = Settings.Get<int>("UiLauncherLight") - 20;
					ColorHueTopbarDelta = OneOf<int, int[]>.op_Implicit(Settings.Get<int>("UiLauncherDelta") - 90);
					break;
				default:
					ColorHue = 210;
					ColorSat = 85;
					ColorLightAdjust = 0;
					break;
				}
				switch (ThemeDontClick)
				{
				case 1:
					ColorLightAdjust = 999;
					break;
				case 2:
					ColorHue = ModBase.RandomInteger(0, 359);
					ColorSat = ModBase.RandomInteger(40, 70);
					ColorLightAdjust = ModBase.RandomInteger(-20, 20);
					break;
				}
				Color1 = new ModBase.MyColor().FromHSL2(ColorHue, (double)ColorSat * 0.2, 25.0 + (double)ColorLightAdjust * 0.3);
				Color2 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 45 + ColorLightAdjust);
				Color3 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 55 + ColorLightAdjust);
				Color4 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 65 + ColorLightAdjust);
				Color5 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 80.0 + (double)ColorLightAdjust * 0.4);
				Color6 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 91.0 + (double)ColorLightAdjust * 0.1);
				Color7 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 95.0);
				Color8 = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 97.0);
				ColorBg0 = Color4 * 0.4 + Color5 * 0.4 + ColorGray4 * 0.2;
				ColorBg1 = new ModBase.MyColor(190.0, Color7);
				ColorSemiTransparent = new ModBase.MyColor(1.0, Color8);
				System.Windows.Application.Current.Resources["ColorBrush1"] = new SolidColorBrush(Color1);
				System.Windows.Application.Current.Resources["ColorBrush2"] = new SolidColorBrush(Color2);
				System.Windows.Application.Current.Resources["ColorBrush3"] = new SolidColorBrush(Color3);
				System.Windows.Application.Current.Resources["ColorBrush4"] = new SolidColorBrush(Color4);
				System.Windows.Application.Current.Resources["ColorBrush5"] = new SolidColorBrush(Color5);
				System.Windows.Application.Current.Resources["ColorBrush6"] = new SolidColorBrush(Color6);
				System.Windows.Application.Current.Resources["ColorBrush7"] = new SolidColorBrush(Color7);
				System.Windows.Application.Current.Resources["ColorBrush8"] = new SolidColorBrush(Color8);
				System.Windows.Application.Current.Resources["ColorBrushBg0"] = new SolidColorBrush(ColorBg0);
				System.Windows.Application.Current.Resources["ColorBrushBg1"] = new SolidColorBrush(ColorBg1);
				System.Windows.Application.Current.Resources["ColorObject1"] = (Color)Color1;
				System.Windows.Application.Current.Resources["ColorObject2"] = (Color)Color2;
				System.Windows.Application.Current.Resources["ColorObject3"] = (Color)Color3;
				System.Windows.Application.Current.Resources["ColorObject4"] = (Color)Color4;
				System.Windows.Application.Current.Resources["ColorObject5"] = (Color)Color5;
				System.Windows.Application.Current.Resources["ColorObject6"] = (Color)Color6;
				System.Windows.Application.Current.Resources["ColorObject7"] = (Color)Color7;
				System.Windows.Application.Current.Resources["ColorObject8"] = (Color)Color8;
				System.Windows.Application.Current.Resources["ColorObjectBg0"] = (Color)ColorBg0;
				System.Windows.Application.Current.Resources["ColorObjectBg1"] = (Color)ColorBg1;
				ThemeRefreshMain();
				if (ThemeNow != 12 && ThemeNow != 14 && ThemeDontClick != 2)
				{
					Logger.Info($"刷新主题：{ThemeNow}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "刷新主题颜色失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void ThemeRefreshMain()
	{
		ModBase.RunInUi(checked([SpecialName] () =>
		{
			if (ModMain.FrmMain.IsLoaded)
			{
				LinearGradientBrush linearGradientBrush = new LinearGradientBrush
				{
					EndPoint = new Point(1.0, 0.0),
					StartPoint = new Point(0.0, 0.0)
				};
				if (ThemeNow == 5)
				{
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 0.0,
						Color = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 25.0)
					});
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 0.5,
						Color = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 15.0)
					});
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 1.0,
						Color = new ModBase.MyColor().FromHSL2(ColorHue, ColorSat, 25.0)
					});
					ModMain.FrmMain.PanTitle.Background = linearGradientBrush;
					ModMain.FrmMain.PanTitle.Background.Freeze();
				}
				else if (ThemeNow != 12 && ThemeDontClick != 2)
				{
					int[] array = ColorHueTopbarDelta.Switch<int[]>((Func<int, int[]>)([SpecialName] (int d) => new int[3]
					{
						-d,
						0,
						d
					}), (Func<int[], int[]>)([SpecialName] (int[] d) => d));
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 0.0,
						Color = new ModBase.MyColor().FromHSL2(ColorHue + array[0], ColorSat, 48 + ColorLightAdjust)
					});
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 0.5,
						Color = new ModBase.MyColor().FromHSL2(ColorHue + array[1], ColorSat, 54 + ColorLightAdjust)
					});
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 1.0,
						Color = new ModBase.MyColor().FromHSL2(ColorHue + array[2], ColorSat, 48 + ColorLightAdjust)
					});
					ModMain.FrmMain.PanTitle.Background = linearGradientBrush;
					ModMain.FrmMain.PanTitle.Background.Freeze();
				}
				else
				{
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 0.0,
						Color = new ModBase.MyColor().FromHSL2(ColorHue - 21, ColorSat, 53 + ColorLightAdjust)
					});
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 0.33,
						Color = new ModBase.MyColor().FromHSL2(ColorHue - 7, ColorSat, 47 + ColorLightAdjust)
					});
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 0.67,
						Color = new ModBase.MyColor().FromHSL2(ColorHue + 7, ColorSat, 47 + ColorLightAdjust)
					});
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 1.0,
						Color = new ModBase.MyColor().FromHSL2(ColorHue + 21, ColorSat, 53 + ColorLightAdjust)
					});
					ModMain.FrmMain.PanTitle.Background = linearGradientBrush;
				}
				if (ThemeNow >= 5 && ThemeNow != 14)
				{
					ModMain.FrmMain.ImgTitle.Source = $"{ModBase.PathImage}Themes/{ThemeNow}.png";
				}
				else
				{
					ModMain.FrmMain.ImgTitle.Source = null;
				}
				ModMain.FrmMain.ImgTitle.Opacity = ((ThemeNow == 13) ? 0.25 : 0.5);
				if (Settings.Get<bool>("UiBackgroundColorful"))
				{
					linearGradientBrush = new LinearGradientBrush
					{
						EndPoint = new Point(0.1, 1.0),
						StartPoint = new Point(0.9, 0.0)
					};
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = -0.1,
						Color = new ModBase.MyColor().FromHSL2(ColorHue - 15, (double)ColorSat * 0.8, 91.0)
					});
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 0.4,
						Color = new ModBase.MyColor().FromHSL2(ColorHue, (double)ColorSat * 0.8, 91.0)
					});
					linearGradientBrush.GradientStops.Add(new GradientStop
					{
						Offset = 1.1,
						Color = new ModBase.MyColor().FromHSL2(ColorHue + 15, (double)ColorSat * 0.8, 91.0)
					});
					ModMain.FrmMain.PanForm.Background = linearGradientBrush;
				}
				else
				{
					ModMain.FrmMain.PanForm.Background = new ModBase.MyColor(245.0, 245.0, 245.0);
				}
				ModMain.FrmMain.PanForm.Background.Freeze();
			}
		}));
	}

	internal static void ThemeCheckAll(bool EffectSetup)
	{
		_Closure_0024__36_002D0 arg = default(_Closure_0024__36_002D0);
		_Closure_0024__36_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__36_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_EffectSetup = EffectSetup;
		ModBase.RunInUi(checked([SpecialName] () =>
		{
			try
			{
				int num = Settings.Get<int>("UiLauncherTheme");
				if (!ThemeCheckOne(num))
				{
					Logger.Warn($"检测到尚未解锁的主题：{num}，已重置为默认主题", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					Settings.Set("UiLauncherTheme", 0);
				}
				if (CS_0024_003C_003E8__locals0._0024VB_0024Local_EffectSetup && ModMain.FrmSetupUI != null)
				{
					List<int> list = new List<int>();
					int num2 = 0;
					do
					{
						if (ThemeCheckOne(num2))
						{
							list.Add(num2);
						}
						num2++;
					}
					while (num2 <= 14);
					list.Add(42);
					ModAnimation.AniControlEnabled++;
					if (list.Contains(5))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme5.Opacity = 1.0;
						ModMain.FrmSetupUI.LabLauncherTheme5Unlock.Visibility = Visibility.Collapsed;
						ModMain.FrmSetupUI.RadioLauncherTheme5Gray.Visibility = Visibility.Collapsed;
					}
					if (list.Contains(6))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme6.IsEnabled = true;
					}
					if (list.Contains(7))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme7.IsEnabled = true;
					}
					if (list.Contains(8))
					{
						ModMain.FrmSetupUI.LabLauncherTheme8Copy.Visibility = Visibility.Collapsed;
						ModMain.FrmSetupUI.RadioLauncherTheme8.IsEnabled = true;
					}
					if (list.Contains(9))
					{
						ModMain.FrmSetupUI.LabLauncherTheme9Copy.Visibility = Visibility.Collapsed;
						ModMain.FrmSetupUI.RadioLauncherTheme9.IsEnabled = true;
					}
					if (list.Contains(10))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme10.IsEnabled = true;
					}
					if (list.Contains(11))
					{
						ModMain.FrmSetupUI.LabLauncherTheme11Click.Visibility = Visibility.Collapsed;
						ModMain.FrmSetupUI.RadioLauncherTheme11.IsEnabled = true;
					}
					else if (!Information.IsNothing(ModMain.FrmSetupUI) && !Information.IsNothing(ModMain.FrmSetupUI.RadioLauncherTheme11) && ModMain.FrmSetupUI.RadioLauncherTheme11.IsLoaded)
					{
						int num3 = 0;
						foreach (int item in list)
						{
							if (item >= 5 && item <= 14)
							{
								num3++;
							}
						}
						if (num3 < 3)
						{
							ModMain.FrmSetupUI.LabLauncherTheme11Click.ToolTip = "需要解锁三个隐藏主题";
						}
						else
						{
							ModMain.FrmSetupUI.LabLauncherTheme11Click.ToolTip = "点击打开解密游戏入口";
						}
					}
					if (list.Contains(12))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme12.IsEnabled = true;
					}
					if (list.Contains(13))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme13.IsEnabled = true;
					}
					if (list.Contains(14))
					{
						ModMain.FrmSetupUI.RadioLauncherTheme14.IsEnabled = true;
					}
					ModAnimation.AniControlEnabled--;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				if ((ex2.InnerException ?? ex2) is FormatException)
				{
					Logger.Error(ex2, "解锁的主题列表存档已损坏，主题解锁已被重置", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					Settings.Set("UiLauncherThemeHide2", "");
				}
				else
				{
					Logger.Error(ex2, "检查主题失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				}
				ProjectData.ClearProjectError();
			}
		}));
	}

	internal static bool ThemeCheckOne(int Id)
	{
		return true;
	}

	internal static bool ThemeUnlock(int Id, bool ShowDoubleHint = true, string UnlockHint = null)
	{
		if (ModMain.FrmMain == null)
		{
			return false;
		}
		if (Id == 8)
		{
			throw new Exception("不可使用此方式解锁秋仪金！");
		}
		List<string> list = new List<string>(StringExtensions.Split(Settings.Get<string>("UiLauncherThemeHide2").ToString(), "|", false));
		if (list.Contains(Id.ToString()))
		{
			if (ShowDoubleHint)
			{
				ModMain.Hint("该隐藏主题已被解锁！");
			}
			return false;
		}
		Logger.Info($"解锁隐藏主题：{Id}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
		if (UnlockHint != null)
		{
			ModMain.Hint(UnlockHint, ModMain.HintType.Green, Log: false);
		}
		list.Add(Id.ToString());
		Settings.Set("UiLauncherThemeHide2", EnumerableExtensions.Join((IEnumerable)list, "|"));
		ThemeCheckAll(EffectSetup: true);
		return true;
	}

	internal static void UpdateCheckByButton()
	{
		ModMain.MyMsgBox("在2026年10月20日因为奇异搞笑地球online新手玩家往某不知名方块人伺服器中投入大量金圆券导致地球online老玩家急眼报告了这个伺服器，同时连着波及了这个二进制产物导致其暂时下架了国内的所有方块人副本下载渠道，如果你点击了更新则会无法下载最新最热方块人副本进行玩耍，所以我帮你删掉了。");
	}

	private static void UpdateStart(bool Silent, IEnumerable<string> UrlsOverride = null)
	{
	}

	public static void UpdateRestart(bool TriggerRestartAndByEnd)
	{
		try
		{
			IsUpdateWaitingRestart = false;
			string fileName = Paths.Base + "PCL\\Plain Craft Launcher 2.exe";
			string text = $"--update {Process.GetCurrentProcess().Id} \"{AppDomain.CurrentDomain.SetupInformation.ApplicationName}\" \"{AppDomain.CurrentDomain.SetupInformation.ApplicationName}\" {TriggerRestartAndByEnd}";
			Logger.Info($"更新程序启动，参数：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			ModBase.StartProcess(new ProcessStartInfo(fileName)
			{
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				Arguments = text
			});
			if (TriggerRestartAndByEnd)
			{
				ModMain.FrmMain.EndProgram(SendWarning: false);
				Logger.Info("已由于更新强制结束程序", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			}
		}
		catch (Win32Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Win32Exception ex2 = ex;
			Logger.Warn((Exception)ex2, "自动更新时触发 Win32 错误，疑似被拦截", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			if (ModMain.MyMsgBox(string.Format("由于被 Windows 安全中心拦截，或者存在权限问题，导致 PCL 无法更新。{0}请将 PCL 所在文件夹加入白名单，或者手动用 {1}PCL\\Plain Craft Launcher 2.exe 替换当前文件！", "\r\n", Paths.Base), "更新失败", "查看帮助", "确定", "", IsWarn: true) == 1)
			{
				CustomEvent.Raise(CustomEvent.EventType.打开帮助, "启动器/Microsoft Defender 添加排除项.json");
			}
			ProjectData.ClearProjectError();
		}
	}

	public static void UpdateReplace(int ProcessId, string OldFileName, string NewFileName, bool TriggerRestart)
	{
		_Closure_0024__45_002D0 arg = default(_Closure_0024__45_002D0);
		_Closure_0024__45_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__45_002D0(arg);
		try
		{
			Process.GetProcessById(ProcessId).Kill();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		string text = Path.Combine(PathUtils.AddSlashSuffix(PathUtils.RemoveLastPart(Paths.Base)), PathUtils.GetLastPart(OldFileName));
		CS_0024_003C_003E8__locals0._0024VB_0024Local_NewFilePath = Path.Combine(PathUtils.AddSlashSuffix(PathUtils.RemoveLastPart(Paths.Base)), PathUtils.GetLastPart(NewFileName));
		Exception ex = null;
		int num = 0;
		checked
		{
			do
			{
				try
				{
					FileUtils.Delete(text, false);
					FileUtils.Delete(CS_0024_003C_003E8__locals0._0024VB_0024Local_NewFilePath, false);
					if (!FileUtils.Exists(text) && !FileUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_NewFilePath))
					{
						break;
					}
				}
				catch (Exception ex2)
				{
					ProjectData.SetProjectError(ex2);
					Exception ex3 = ex2;
					ex = ex3;
					ProjectData.ClearProjectError();
				}
				Thread.Sleep(num * 500 + 500);
				num++;
			}
			while (num <= 4);
			VB_0024AnonymousDelegate_11<string, string> vB_0024AnonymousDelegate_ = [SpecialName] (string Reason) =>
			{
				string[] obj = new string[5]
				{
					string.Format("由于{0}，PCL 无法完成更新。{1}请依次尝试：{2}", Reason, "\r\n", "\r\n"),
					(PathUtils.IsParentOf(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Paths.Base) || PathUtils.IsParentOf(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Paths.Base)) ? " - 将 PCL 文件移动到桌面、文档、C 盘以外的文件夹（这或许可以一劳永逸地解决权限问题）\r\n" : "",
					null,
					null,
					null
				};
				char? diskName = PathUtils.GetDiskName(Paths.Base);
				obj[2] = ((diskName.HasValue ? new bool?(diskName.GetValueOrDefault() == 'C') : null).GetValueOrDefault() ? " - 将 PCL 文件移动到 C 盘以外的文件夹（这或许可以一劳永逸地解决权限问题）\r\n" : "");
				obj[3] = ((!WindowsUtils.HasAdminRole()) ? " - 右键 PCL，选择以管理员身份运行\r\n" : "");
				obj[4] = string.Format(" - 手动剪切 PCL 文件夹中的新版本程序，覆盖原程序：{0}   将位于 {1}{2}   的程序{3}   剪切到 {4} 并覆盖即可。", "\r\n", ModBase.PathExe, "\r\n", "\r\n", CS_0024_003C_003E8__locals0._0024VB_0024Local_NewFilePath);
				return string.Concat(obj);
			};
			if (FileUtils.Exists(text) || FileUtils.Exists(CS_0024_003C_003E8__locals0._0024VB_0024Local_NewFilePath))
			{
				if (ex is UnauthorizedAccessException)
				{
					Interaction.MsgBox(string.Format("{0}{1}{2}详细错误信息：{3}{4}", vB_0024AnonymousDelegate_("删除老版本文件时权限不足"), "\r\n", "\r\n", "\r\n", ExceptionExtensions.GetDisplay(ex, true)), MsgBoxStyle.Critical, "更新失败");
				}
				else
				{
					Interaction.MsgBox(string.Format("{0}{1}{2}详细错误信息：{3}{4}", vB_0024AnonymousDelegate_("删除老版本文件时出错"), "\r\n", "\r\n", "\r\n", ExceptionExtensions.GetDisplay(ex, true)), MsgBoxStyle.Critical, "更新失败");
				}
				return;
			}
			try
			{
				FileUtils.Copy(ModBase.PathExe, CS_0024_003C_003E8__locals0._0024VB_0024Local_NewFilePath);
			}
			catch (UnauthorizedAccessException ex4)
			{
				ProjectData.SetProjectError(ex4);
				UnauthorizedAccessException ex5 = ex4;
				Interaction.MsgBox(string.Format("{0}{1}{2}详细错误信息：{3}{4}", vB_0024AnonymousDelegate_("复制新版本文件时权限不足"), "\r\n", "\r\n", "\r\n", ExceptionExtensions.GetDisplay((Exception)ex5, true)), MsgBoxStyle.Critical, "更新失败");
				ProjectData.ClearProjectError();
			}
			catch (Exception ex6)
			{
				ProjectData.SetProjectError(ex6);
				Exception ex7 = ex6;
				Interaction.MsgBox(string.Format("{0}{1}{2}详细错误信息：{3}{4}", vB_0024AnonymousDelegate_("复制新版本文件时出错"), "\r\n", "\r\n", "\r\n", ExceptionExtensions.GetDisplay(ex7, true)), MsgBoxStyle.Critical, "更新失败");
				ProjectData.ClearProjectError();
				return;
			}
			if (!TriggerRestart)
			{
				return;
			}
			try
			{
				ModBase.StartProcess(CS_0024_003C_003E8__locals0._0024VB_0024Local_NewFilePath);
			}
			catch (Exception ex8)
			{
				ProjectData.SetProjectError(ex8);
				Exception ex9 = ex8;
				Interaction.MsgBox(string.Format("PCL 更新已完成，但在自动重启 PCL 时遇到了意外，请手动启动 PCL。{0}{1}详细错误信息：{2}{3}", "\r\n", "\r\n", "\r\n", ExceptionExtensions.GetDisplay(ex9, true)), MsgBoxStyle.Information, "更新完成");
				ProjectData.ClearProjectError();
			}
		}
	}

	internal static void DownloadLatestPCL(ModLoader.LoaderBase LoaderToSyncProgress = null)
	{
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		int num = (FileUtils.Exists(ModBase.PathTemp + "Latest.exe") ? Settings.Get<int>("SystemHighestSavedBetaVersionReg") : 0);
		int num2 = 0;
		string text = FileUtils.TryReadAsString(ModBase.PathTemp + "Cache\\Notice.cfg", (Encoding)null, (Type)null) ?? "";
		if (StringExtensions.Split(text, "|", false).Count() >= 3)
		{
			num2 = Conversions.ToInteger(StringExtensions.Split(text, "|", false)[2]);
		}
		if (num < num2)
		{
			Logger.Info($"需要下载最新正式版：{num} -> {num2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			if (ServerConfig == null)
			{
				ServerLoader.WaitForExit(null, null, ServerLoader.State == ModBase.LoadState.Finished);
			}
			if (ServerConfig == null)
			{
				throw new Exception("无法从服务器获取正式版 PCL 下载链接！");
			}
			ModNet.NetDownloadByLoader(((IEnumerable<JToken>)ServerConfig["Update"][(object)"Release"][(object)"DownloadUrls"]).Select([SpecialName] (JToken t) => t.ToString()).ToList(), ModBase.PathTemp + "Latest.zip", LoaderToSyncProgress, new FileChecker
			{
				MinSize = 1048576L
			});
			FileUtils.ExtractToDirectory(ModBase.PathTemp + "Latest.zip", ModBase.PathTemp, default(CancellationToken), (ProgressProvider)null);
			FileUtils.Delete(ModBase.PathTemp + "Latest.zip", false);
			FileUtils.Move(ModBase.PathTemp + "Plain Craft Launcher 2.exe", ModBase.PathTemp + "Latest.exe", false);
			Settings.Set("SystemHighestSavedBetaVersionReg", num2);
		}
	}

	private static void ServerSub(ModLoader.LoaderTask<int, int> Loader)
	{
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Expected O, but got Unknown
		//IL_0816: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Expected O, but got Unknown
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Expected O, but got Unknown
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Expected O, but got Unknown
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Expected O, but got Unknown
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (FileUtils.Exists(Paths.Base + "PCL\\update.exe"))
			{
				FileUtils.Delete(Paths.Base + "PCL\\update.exe", false);
				Logger.Info("已清理更新缓存", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "清理更新缓存失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			ProjectData.ClearProjectError();
		}
		int num = 0;
		string text = ModBase.PathTemp + "Cache\\ServerConfig.json";
		checked
		{
			try
			{
				if (FileUtils.Exists(text))
				{
					ServerConfig = (JObject)FileUtils.ReadAsJson(text, (Encoding)null, (Type)null);
					num = (int)Math.Round(ModBase.Val(ServerConfig["Version"]));
					Logger.Info($"已读取本地缓存的配置：{num}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				}
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "读取配置信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				ServerConfig = null;
				FileUtils.Delete(text, false);
				ProjectData.ClearProjectError();
			}
			Loader.Progress = 0.2;
			int num2 = Settings.Get<int>("HintNotice");
			string text2 = ModBase.PathTemp + "Cache\\Notice.cfg";
			string text3;
			int num3;
			try
			{
				text3 = ModNet.NetRequestByClientRetry("https://pcl2-server-1253424809.file.myqcloud.com/notice.cfg{CDN}");
				num3 = (int)Math.Round(ModBase.Val(StringExtensions.Split(text3, "|", false)[0]));
				if (num3 == 0)
				{
					throw new Exception("获取到的内容有误！（" + text3 + "）");
				}
				if (num3 > num2)
				{
					Logger.Info($"本地公告编号：{num2}，服务器公告：{text3}，需更新", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				}
				else
				{
					Logger.Info($"本地公告编号：{num2}，服务器公告：{text3}，无需更新", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				}
				FileUtils.Write(text2, text3, (Encoding)null);
			}
			catch (Exception ex5)
			{
				ProjectData.SetProjectError(ex5);
				Exception ex6 = ex5;
				if (ex6 is InvalidOperationException && ex6.Message.Contains("FIPS"))
				{
					if (ModMain.MyMsgBox("由于系统未启用 FIPS 兼容算法，PCL 可能无法正常运行。\r\n请按照教程启用该功能，然后重启 PCL。", "兼容性警告", "打开教程", "取消", "", IsWarn: true) == 1)
					{
						ModBase.OpenWebsite("https://blog.csdn.net/qq_37608398/article/details/81209922");
					}
				}
				else
				{
					Logger.Warn(ex6, "获取 PCL 服务器状态失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					FileUtils.Delete(text2, false);
				}
				ProjectData.ClearProjectError();
				return;
			}
			Loader.Progress = 0.4;
			try
			{
				double num4 = ModBase.Val(StringExtensions.Split(text3, "|", false)[4]);
				if ((double)num < num4)
				{
					Logger.Info($"本地配置版本号：{num}，服务器配置版本号：{num4}，需要更新", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					ModNet.NetDownloadByClient("https://pcl2-server-1253424809.file.myqcloud.com/ServerConfig.json{CDN}", text);
					ServerConfig = (JObject)FileUtils.ReadAsJson(text, (Encoding)null, (Type)null);
				}
			}
			catch (Exception ex7)
			{
				ProjectData.SetProjectError(ex7);
				Exception ex8 = ex7;
				Logger.Warn(ex8, "下载配置信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				ProjectData.ClearProjectError();
			}
			Loader.Progress = 0.6;
			string text4;
			try
			{
				if (num3 <= num2 && FileUtils.Exists(ModBase.PathTemp + "Cache\\Notice.json"))
				{
					text4 = FileUtils.ReadAsString(ModBase.PathTemp + "Cache\\Notice.json", (Encoding)null, (Type)null);
				}
				else
				{
					text4 = ModNet.NetRequestByClientRetry("https://pcl2-server-1253424809.file.myqcloud.com/notice.json{CDN}", null, null, null, null, "*/*", null, RequireJson: true);
					FileUtils.Write(ModBase.PathTemp + "Cache\\Notice.json", text4, (Encoding)null);
				}
				Settings.Set("HintNotice", num3);
			}
			catch (Exception ex9)
			{
				ProjectData.SetProjectError(ex9);
				Exception ex10 = ex9;
				Logger.Warn(ex10, "下载 PCL 服务器公告失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				FileUtils.Delete(ModBase.PathTemp + "Cache\\Notice.json", false);
				ProjectData.ClearProjectError();
				return;
			}
			Loader.Progress = 0.9;
			try
			{
				foreach (JObject item in (IEnumerable<JToken>)StringExtensions.DeserializeJson(text4.Replace("{UNIQUE}", ModBase.Identify)))
				{
					JObject val = item;
					int num5 = (int)Math.Round(ModBase.Val(val["id"] ?? JToken.op_Implicit(0)));
					if (num5 <= num2)
					{
						continue;
					}
					bool flag = true;
					foreach (JProperty item2 in (IEnumerable<JToken>)(val["requirements"] ?? JToken.op_Implicit(new byte[0])))
					{
						JProperty val2 = item2;
						if (!ServerRequirement(val2.Name, val2.Value))
						{
							flag = false;
						}
					}
					if (!flag)
					{
						continue;
					}
					int num6 = (int)(val["importantLevel"] ?? JToken.op_Implicit(2));
					bool flag2 = (bool)(val["isUpdate"] ?? JToken.op_Implicit(false));
					Logger.Info($"重要等级 {num6}，更新公告 {flag2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					if (Settings.Get<int>(flag2 ? "SystemSystemUpdate" : "SystemSystemActivity") > num6 || (flag2 && IsUpdating))
					{
						continue;
					}
					string title = (val["title"] ?? JToken.op_Implicit("公告")).ToString();
					string text5 = (val["description"] ?? JToken.op_Implicit("")).ToString().Replace("\\n", "\r\n");
					JArray val3 = (JArray)(((object)val["buttons"]) ?? ((object)new JArray()));
					int count = ((JContainer)val3).Count;
					object obj = new object[0];
					if (count > 0)
					{
						Logger.Info($"显示公告 {num5}：{text5}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
						int num7;
						switch (count)
						{
						case 1:
							num7 = ModMain.MyMsgBox(text5, title, (val3[0][(object)"text"] ?? JToken.op_Implicit("确定")).ToString(), "", "", IsWarn: false, HighLight: true, ForceWait: true);
							break;
						case 2:
							num7 = ModMain.MyMsgBox(text5, title, (val3[0][(object)"text"] ?? JToken.op_Implicit("确定")).ToString(), (val3[1][(object)"text"] ?? JToken.op_Implicit("确定")).ToString());
							break;
						case 3:
							num7 = ModMain.MyMsgBox(text5, title, (val3[0][(object)"text"] ?? JToken.op_Implicit("确定")).ToString(), (val3[1][(object)"text"] ?? JToken.op_Implicit("确定")).ToString(), (val3[2][(object)"text"] ?? JToken.op_Implicit("确定")).ToString());
							break;
						default:
							Logger.Warn($"公告 {num5} 的弹窗有 {count} 个按钮，无法显示", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
							continue;
						}
						num7 = MathUtils.Clamp<int>(num7, 1, count);
						obj = val3[num7 - 1][(object)"actions"] ?? JToken.op_Implicit(new byte[0]);
					}
					else
					{
						obj = val["actions"] ?? JToken.op_Implicit(new byte[0]);
					}
					foreach (JProperty item3 in (IEnumerable)obj)
					{
						JProperty val4 = item3;
						try
						{
							switch (val4.Name)
							{
							case "copy":
								ModBase.ClipboardSet(val4.Value.ToString());
								break;
							case "website":
								ModBase.OpenWebsite(val4.Value.ToString());
								break;
							case "stop":
								ModMain.FrmMain.EndProgram(SendWarning: false);
								break;
							case "setup":
							{
								JObject val5 = (JObject)val4.Value;
								foreach (KeyValuePair<string, JToken> item4 in val5)
								{
									Settings.Set(item4.Key, item4.Value.ToString());
								}
								break;
							}
							case "update":
								break;
							case "slientupdate":
								break;
							default:
								throw new Exception("未知的行动支：" + val4.Name + ", " + val4.Value.ToString());
							}
						}
						catch (Exception ex11)
						{
							ProjectData.SetProjectError(ex11);
							Exception ex12 = ex11;
							Logger.Error(ex12, $"执行 PCL 服务器公告动作失败（{val4.Name}, {val4.Value}）", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
							ProjectData.ClearProjectError();
						}
					}
				}
			}
			catch (Exception ex13)
			{
				ProjectData.SetProjectError(ex13);
				Exception ex14 = ex13;
				Logger.Warn(text4, (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				Logger.Error(ex14, $"读取 PCL 服务器公告失败（{text4.Length}）", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				FileUtils.Delete(ModBase.PathTemp + "Cache\\Notice.json", false);
				ProjectData.ClearProjectError();
			}
		}
	}

	private static bool ServerRequirement(string Name, JToken Value)
	{
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		checked
		{
			bool result;
			try
			{
				switch (Name)
				{
				case "d10000 <=":
					result = (double)ModBase.RandomInteger(1, 10000) <= ModBase.Val(Value);
					break;
				case "opencount <=":
					result = (double)Settings.Get<int>("SystemCount") <= ModBase.Val(Value);
					break;
				case "opencount >=":
					result = (double)Settings.Get<int>("SystemCount") >= ModBase.Val(Value);
					break;
				case "versioncode <=":
					result = 412.0 <= ModBase.Val(Value);
					break;
				case "versioncode >=":
					result = 412.0 >= ModBase.Val(Value);
					break;
				case "versionbranch <=":
					result = ModBase.Val(0) <= ModBase.Val(Value);
					break;
				case "versionbranch >=":
					result = ModBase.Val(0) >= ModBase.Val(Value);
					break;
				case "uniqueaddress =":
					result = Operators.CompareString(ModBase.Identify, Value.ToString(), TextCompare: false) == 0;
					break;
				case "unlockedtheme =":
					result = ThemeCheckOne((int)Math.Round(ModBase.Val(Value)));
					break;
				case "unlockedtheme !=":
					result = !ThemeCheckOne((int)Math.Round(ModBase.Val(Value)));
					break;
				case "setupinteger >=":
					result = (double)Settings.Get<int>(((JProperty)Value).Name) >= ModBase.Val(((JProperty)Value).Value);
					break;
				case "setupinteger <=":
					result = (double)Settings.Get<int>(((JProperty)Value).Name) <= ModBase.Val(((JProperty)Value).Value);
					break;
				case "setupboolean =":
					result = Settings.Get<bool>(((JProperty)Value).Name) == ((JProperty)Value).Value.ToObject<bool>();
					break;
				case "debug =":
					result = Operators.CompareString(StringExtensions.Lower(ModBase.ModeDebug.ToString()), StringExtensions.Lower(Value.ToString()), TextCompare: false) == 0;
					break;
				default:
					Logger.Warn($"未知的条件支：{Name}, {Value}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					result = false;
					break;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn($"判断分支条件失败：{Name}, {Value}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
	}

	private static OneOf<List<string>, string> CheckPotatoCode(string Key)
	{
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		OneOf<List<string>, string> result;
		if (string.IsNullOrEmpty(Key))
		{
			result = OneOf<List<string>, string>.op_Implicit("输入的土豆码为空。");
		}
		else if (Key.Length < 30 && StringExtensions.ContainsIgnoreCase(Key, ModBase.Identify))
		{
			result = OneOf<List<string>, string>.op_Implicit(string.Format("你输入的是识别码，不是土豆码。{0}要获取土豆码，请在爱发电私信发送【土豆 {1}】。", "\r\n", ModBase.Identify));
		}
		else if (Key.Length > 85 && Key.Length < 90 && StringExtensions.EndsWithF(Key, "=", false))
		{
			result = OneOf<List<string>, string>.op_Implicit(string.Format("你输入的是解锁码，不是土豆码。{0}要获取土豆码，请在爱发电私信发送【土豆】。", "\r\n"));
		}
		else if (Key.Length > 150 && Key.Length < 165 && StringExtensions.EndsWithF(Key, "11", false))
		{
			result = OneOf<List<string>, string>.op_Implicit(string.Format("你输入的是更新密钥，不是土豆码。{0}要获取土豆码，请在爱发电私信发送【土豆】。", "\r\n"));
		}
		else if (!StringExtensions.StartsWithF(Key, "CD", true) || !Key.Contains("#"))
		{
			result = OneOf<List<string>, string>.op_Implicit(string.Format("你输入的不是土豆码……有一说一，这是啥玩意儿？{0}要获取土豆码，请在爱发电私信发送【土豆 {1}】。", "\r\n", ModBase.Identify));
		}
		else if (Key[2] > '1')
		{
			result = OneOf<List<string>, string>.op_Implicit(string.Format("这个土豆码得在新版 PCL 上才能使用。{0}请更新 PCL，或是在爱发电私信发送【下载】，然后在新下载的 PCL 上输入土豆码。", "\r\n"));
		}
		else if ('1' > Key[3])
		{
			result = OneOf<List<string>, string>.op_Implicit(string.Format("这个土豆码不太新鲜，已经过期了。{0}请在爱发电私信重新发送【土豆 {1}】，获取更新鲜的土豆码。", "\r\n", ModBase.Identify));
		}
		else if (!StringExtensions.StartsWithF(Key, "CD", false))
		{
			result = OneOf<List<string>, string>.op_Implicit(string.Format("土豆码区分大小写，但你把一些大写的东西输成小写的了。{0}你可以选中文本，然后右键复制粘贴，不必手动输入。", "\r\n"));
		}
		else
		{
			try
			{
				string[] array = Key.Substring(4).Replace("p", "=").Replace("%", "p")
					.Split('#');
				string @string = Encoding.UTF8.GetString(Convert.FromBase64String(array[1]));
				CryptographyUtils.EcdsaVerify(ModBase.Identify + Conversions.ToString(Key[2]) + Conversions.ToString(Key[3]) + @string, array[0], "RUNTMSAAAAC4QTUNAewh23Q4Q6koHkyIrDIIZUSbua23sf2DiZmIRwSzadISDRyTVTbuWniH3KR7rKj8XBsabms1be6i3c+S");
				result = OneOf<List<string>, string>.op_Implicit(StringExtensions.Split(@string, ',', true).ToList());
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"土豆码校验失败（{Key}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
				result = OneOf<List<string>, string>.op_Implicit(string.Format("土豆码有误，请检查：{0}- 私信显示的识别码应当为 {1}，如果不是，请在私信发送【土豆 {2}】{3}- 你输入的以下土豆码是否与私信中的一致：{4}{5}{6}{7}错误原因：{8}", "\r\n", ModBase.Identify, ModBase.Identify, "\r\n", "\r\n", Key, "\r\n", "\r\n", ex2.Message));
				ProjectData.ClearProjectError();
			}
		}
		return result;
	}

	public static void InputPotatoCode(bool IsUpdating)
	{
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		string text;
		if (IsUpdating)
		{
			if (ModMain.MyMsgBox(string.Format("请点击【复制命令】，然后将复制的命令粘贴到爱发电私信，获取土豆码。{0}输入土豆码后，就能继续更新啦！", "\r\n"), "验证爱发电赞助", "输入土豆码", "复制命令", "取消", IsWarn: false, HighLight: true, ForceWait: false, null, [SpecialName] () =>
			{
				ModBase.ClipboardSet("土豆 " + ModBase.Identify, ShowSuccessHint: false);
				ModBase.OpenWebsite("https://meloong.com/afd/message/df2dfad2960911e89c5252540025c377?potato");
			}) == 3)
			{
				return;
			}
			text = ModMain.MyMsgBoxInput("输入土豆码", string.Format("请在【爱发电】向龙腾猫跃发送【土豆 {0}】，然后输入自动回复的土豆码以继续更新。{1}自动回复可能比较慢，发送后等一会儿刷新一下网页应该就有了。", ModBase.Identify, "\r\n"), null, new Collection<Validate>
			{
				new ValidateNullOrWhiteSpace()
			});
		}
		else
		{
			text = ModMain.MyMsgBoxInput("输入土豆码", string.Format("请输入你获得的土豆码。{0}你的识别码：{1}", "\r\n", ModBase.Identify), null, new Collection<Validate>
			{
				new ValidateNullOrWhiteSpace()
			});
		}
		if (string.IsNullOrWhiteSpace(text))
		{
			return;
		}
		text = StringExtensions.Between(StringExtensions.ReplaceLineEndings(text, "", false), "：", "在", false).Trim(' ', '【', '】');
		if (string.IsNullOrWhiteSpace(text))
		{
			return;
		}
		Logger.Info($"输入的土豆码：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
		if (!IsUpdating && checked(Operators.CompareString(text, "PuzzleUnlock" + ModBase.Identify.Substring(ModBase.Identify.Count() - 4), TextCompare: false) == 0 || Operators.CompareString(text, "PuzzleUnlock" + ModBase.Identify.Substring(ModBase.Identify.Count() - 5), TextCompare: false) == 0 || Operators.CompareString(text, "PuzzleUnlock" + ModBase.Identify.Substring(ModBase.Identify.Count() - 6), TextCompare: false) == 0))
		{
			ModMain.MyMsgBox(string.Format("隐藏主题 极客蓝 已解锁！{0}难道，这就是终点了？还是说……？", "\r\n"), "解锁新隐藏主题！");
			ThemeUnlock(11);
			return;
		}
		try
		{
			OneOf<List<string>, string> val = CheckPotatoCode(text);
			if (val.Is<string>())
			{
				throw new Exception(val.As<string>());
			}
			List<string> list = val.As<List<string>>();
			Logger.Info(string.Format("土豆码包含的特性：{0}", EnumerableExtensions.Join((IEnumerable)list, ", ")), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			if (list.Any([SpecialName] (string f) => !PotatoFeatures.Contains(f)))
			{
				List<string> list2 = StringExtensions.Split(Settings.Get<string>("Potatoes").ToString(), '|', true).ToList();
				list2.Add(text);
				Settings.Set("Potatoes", EnumerableExtensions.Join((IEnumerable)list2, "|"));
				_PotatoFeatures = null;
			}
			if (IsUpdating)
			{
				if (!HasUpdatePermission)
				{
					throw new Exception("这个土豆码里不包含更新权限……");
				}
			}
			else
			{
				ModMain.Hint("已成功解锁！", ModMain.HintType.Green);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Info($"土豆码无效：{ex2.Message}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			if (ModMain.MyMsgBox(ex2.Message, "错误", "重新输入", "取消", "", IsWarn: true) == 1)
			{
				InputPotatoCode(IsUpdating);
			}
			ProjectData.ClearProjectError();
		}
	}

	internal static void GeneratePotatoCode()
	{
	}

	internal static string GetIdentify()
	{
		string result;
		try
		{
			if (ModBase.ApplicationStartTick < 1)
			{
				result = "0000-0000-0000-0000";
			}
			else
			{
				string text;
				try
				{
					text = StringExtensions.Upper(RegistryUtils.TryRead(string.Format("HKEY_LOCAL_MACHINE\\SYSTEM\\Har{0}wareConfig", StringExtensions.Lower("D")), "LastConfig", (object)"Unknown").ToString()).Trim('{', '}');
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "获取主板标识码失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
					text = "Unknown";
					ProjectData.ClearProjectError();
				}
				string text2;
				try
				{
					text2 = Settings.Get<string>("Identify");
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					text2 = "";
					ProjectData.ClearProjectError();
				}
				if (text2.Length < 3)
				{
					text2 = Conversions.ToString(ModBase.GetTimeMs()) + Conversions.ToString(MyWpfExtension.Computer.Info.AvailablePhysicalMemory);
					Settings.Set("Identify", text2);
				}
				string text3 = StringExtensions.EnsureLength(StringExtensions.GetStableHashCode(text + text2).ToString("X"), '7', 16);
				result = $"{text3.Substring(4, 4)}-{text3.Substring(12, 4)}-{text3.Substring(0, 4)}-{text3.Substring(8, 4)}";
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Error(ex4, string.Format("PCL 无法获取设备识别码，这可能会导致部分设置无法正常存储。{0}{1}详细的错误信息", "\r\n", "\r\n"), (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\ModSecret.vb");
			result = "0000-0000-0000-0000";
			ProjectData.ClearProjectError();
		}
		return result;
	}
}
