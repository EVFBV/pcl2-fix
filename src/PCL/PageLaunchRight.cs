using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageLaunchRight : MyPageRight, IRefreshable, IDispatcherUnhandledException, IComponentConnector
{
	private object RefreshLock;

	private ModLoader.LoaderTask<(string Address, bool ShouldRefresh), int> OnlineLoader;

	private ulong? LoadedContentHash;

	private object LoadContentLock;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyScrollViewer PanBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanMain")]
	internal virtual StackPanel PanMain
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanHint")]
	internal virtual MyHint PanHint
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanCustom")]
	internal virtual StackPanel PanCustom
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanLog")]
	internal virtual MyCard PanLog
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabLog")]
	internal virtual MyTextBox LabLog
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public PageLaunchRight()
	{
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			Init();
		};
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			Refresh();
		};
		RefreshLock = RuntimeHelpers.GetObjectValue(new object());
		OnlineLoader = new ModLoader.LoaderTask<(string, bool), int>("下载主页", OnlineLoaderSub)
		{
			ReloadTimeout = 600000
		};
		LoadedContentHash = null;
		LoadContentLock = RuntimeHelpers.GetObjectValue(new object());
		InitializeComponent();
	}

	private void Init()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Invalid comparison between Unknown and I4
		PanBack.ScrollToHome();
		PanLog.Visibility = ModBase.ToVisibility(ModBase.ModeDebug);
		PanHint.Visibility = ModBase.ToVisibility((int)ModSecret.CurrentRank < 23 && !Settings.Get<bool>("HintSnapshot"));
	}

	private void Refresh()
	{
		ModBase.RunInThread([SpecialName] () =>
		{
			try
			{
				object refreshLock = RefreshLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(refreshLock);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(refreshLock, ref lockTaken);
					RefreshReal();
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(refreshLock);
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "加载 PCL 主页自定义信息失败", (LogBehavior)(ModBase.ModeDebug ? 3 : 2), "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				ProjectData.ClearProjectError();
			}
		});
	}

	private void RefreshReal()
	{
		string content = null;
		string text = null;
		switch (Settings.Get<int>("UiCustomType"))
		{
		case 1:
			Logger.Info("主页自定义数据来源：本地文件", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
			content = FileUtils.TryReadAsString(Paths.Base + "PCL\\Custom.xaml", (Encoding)null, (Type)null);
			break;
		case 2:
			text = Settings.Get<string>("UiCustomNet");
			break;
		case 3:
			switch (Settings.Get<int>("UiCustomPreset"))
			{
			case 0:
				Logger.Info("主页预设：你知道吗", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				content = "\r\n                            <local:MyCard Title=\"你知道吗？\" Margin=\"0,0,0,15\">\r\n                                <TextBlock Margin=\"25,38,23,15\" FontSize=\"13.5\" IsHitTestVisible=\"False\" Text=\"{hint}\" TextWrapping=\"Wrap\" Foreground=\"{DynamicResource ColorBrush1}\" />\r\n                                <local:MyIconButton Height=\"22\" Width=\"22\" Margin=\"9\" VerticalAlignment=\"Top\" HorizontalAlignment=\"Right\" \r\n                                    EventType=\"刷新主页\" EventData=\"/\"\r\n                                    Logo=\"M512.0 838.3c-80.2 0-153.4-29.3-210.2-77.4l75.5-75.5c11.5-11.5 25.8-22.0 25.8-37.0a27.2 27.2 0 0 0-27.1-27.1H104.0c-27.1 0-27.1 23.9-27.1 27.1v271.9a27.1 27.1 0 0 0 27.1 27.1c15.0 0 27.8-16.6 42.5-31.2l77.9-77.9c76.6 67.7 177.1 108.9 287.4 108.9 221.7 0 404.5-166.0 431.2-380.6h-109.8c-25.9 154.2-159.7 271.9-321.3 271.9zM919.9 76.6c-15.0 0-27.8 16.6-42.5 31.3L799.5 185.8c-76.5-67.7-177.1-108.9-287.4-108.9-221.8 0-404.5 166.1-431.3 380.6H190.6c25.9-154.2 159.7-271.9 321.4-271.9 80.2 0 153.4 29.3 210.1 77.4l-75.5 75.5c-11.6 11.5-25.8 22.0-25.8 37.1a27.2 27.2 0 0 0 27.1 27.1h271.9c27.1 0 27.1-23.9 27.1-27.1V103.8a27.1 27.1 0 0 0-27.1-27.1z\" />\r\n                            </local:MyCard>";
				break;
			case 1:
				Logger.Info("主页预设：回声洞", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				content = "\r\n                            <local:MyCard Title=\"回声洞\" Margin=\"0,0,0,15\">\r\n                                <TextBlock Margin=\"25,38,23,15\" FontSize=\"13.5\" IsHitTestVisible=\"False\" Text=\"{cave}\" TextWrapping=\"Wrap\" Foreground=\"{DynamicResource ColorBrush1}\" />\r\n                                <local:MyIconButton Height=\"22\" Width=\"22\" Margin=\"9\" VerticalAlignment=\"Top\" HorizontalAlignment=\"Right\" \r\n                                    EventType=\"刷新主页\" EventData=\"/\"\r\n                                    Logo=\"M512.0 838.3c-80.2 0-153.4-29.3-210.2-77.4l75.5-75.5c11.5-11.5 25.8-22.0 25.8-37.0a27.2 27.2 0 0 0-27.1-27.1H104.0c-27.1 0-27.1 23.9-27.1 27.1v271.9a27.1 27.1 0 0 0 27.1 27.1c15.0 0 27.8-16.6 42.5-31.2l77.9-77.9c76.6 67.7 177.1 108.9 287.4 108.9 221.7 0 404.5-166.0 431.2-380.6h-109.8c-25.9 154.2-159.7 271.9-321.3 271.9zM919.9 76.6c-15.0 0-27.8 16.6-42.5 31.3L799.5 185.8c-76.5-67.7-177.1-108.9-287.4-108.9-221.8 0-404.5 166.1-431.3 380.6H190.6c25.9-154.2 159.7-271.9 321.4-271.9 80.2 0 153.4 29.3 210.1 77.4l-75.5 75.5c-11.6 11.5-25.8 22.0-25.8 37.1a27.2 27.2 0 0 0 27.1 27.1h271.9c27.1 0 27.1-23.9 27.1-27.1V103.8a27.1 27.1 0 0 0-27.1-27.1z\" />\r\n                            </local:MyCard>";
				break;
			case 2:
				Logger.Info("主页预设：Minecraft 新闻", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://mcnews.meloong.com";
				break;
			case 3:
				Logger.Info("主页预设：简单主页", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://pclhomeplazaoss.lingyunawa.top:26994/d/Homepages/MFn233/Custom.xaml";
				break;
			case 4:
				Logger.Info("主页预设：每日整合包推荐", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://pclsub.sodamc.com/";
				break;
			case 5:
				Logger.Info("主页预设：Minecraft 皮肤推荐", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://forgepixel.com/pcl_sub_file";
				break;
			case 6:
				Logger.Info("主页预设：OpenBMCLAPI 仪表盘 Lite", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://pcl-bmcl.milu.ink/";
				break;
			case 7:
				Logger.Info("主页预设：主页市场", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://pclhomeplazaoss.lingyunawa.top:26994/p/Homepages/Homepage.Market/Custom.xaml";
				break;
			case 8:
				Logger.Info("主页预设：更新日志", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://pclhomeplazaoss.lingyunawa.top:26994/d/Homepages/Joker2184/UpdateHomepage.xaml";
				break;
			case 9:
				Logger.Info("主页预设：PCL 新功能说明书", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://raw.gitcode.com/WForst-Breeze/WhatsNewPCL/raw/main/Custom.xaml";
				break;
			case 10:
				Logger.Info("主页预设：OpenMCIM Dashboard", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://files.mcimirror.top/PCL";
				break;
			case 11:
				Logger.Info("主页预设：杂志主页", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://gh-proxy.com/https://github.com/Neclyon/Magazine-Homepage-PCL/raw/main/output/Custom.xaml";
				break;
			case 12:
				Logger.Info("主页预设：PCL GitHub 仪表盘", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://ddf.pcl-community.top/Custom.xaml";
				break;
			case 13:
				Logger.Info("主页预设：PCL 更新摘要", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://raw.gitcode.com/ENC_Euphony/PCL-AI-Summary-HomePage/raw/master/Custom.xaml";
				break;
			case 14:
				Logger.Info("主页预设：今日新闻热点", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://pcl.wyc-w.top/index.xaml";
				break;
			case 15:
				Logger.Info("主页预设：Minecraft 芝士站", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://mkss-services.awaeric.xyz";
				break;
			case 16:
				Logger.Info("主页预设：整合包推荐引擎", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://qawsedrftgyhujiko.fun/pcl2/Custom.xaml";
				break;
			case 17:
				Logger.Info("主页预设：Bangumi 番剧主页", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://bangumi.p.kaphia.top";
				break;
			case 18:
				Logger.Info("主页预设：Bilibili 热门", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://bilibili.p.kaphia.top";
				break;
			case 19:
				Logger.Info("主页预设：Music 云音乐热门", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://cloudmusic.p.kaphia.top";
				break;
			case 20:
				Logger.Info("主页预设：模组推荐", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://g-fish.dpdns.org/download/ModPage.xaml";
				break;
			case 21:
				Logger.Info("主页预设：每日游戏推荐", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://pcl.xn--9iq209asnop94b.top/download/GamePage.xaml";
				break;
			case 22:
				Logger.Info("主页预设：Wallpaper 每日壁纸", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://wallpaper.p.kaphia.top";
				break;
			case 23:
				Logger.Info("主页预设：Weather 天气预报", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://weather-p.kaphia.top/{variable:PCLDailyWeatherPage:choose}.xaml";
				break;
			case 24:
				Logger.Info("主页预设：新一代狗屁王大杂烩主页", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				text = "https://end.g-fish.dpdns.org/Custom.xaml";
				break;
			}
			break;
		}
		if (!string.IsNullOrWhiteSpace(text))
		{
			text = ModMain.ArgumentReplace(text, WebUtility.HtmlEncode);
			if (Operators.CompareString(text, Settings.Get<string>("CacheSavedPageUrl"), TextCompare: false) != 0 || !FileUtils.Exists(ModBase.PathTemp + "Cache\\Custom.xaml"))
			{
				Logger.Info("主页自定义数据来源：联网全新下载", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				ModMain.Hint("正在加载主页……");
				ModBase.RunInUiWait([SpecialName] () =>
				{
					LoadContent(null);
				});
				Settings.Set("CacheSavedPageVersion", "");
				OnlineLoader.Start((text, true));
				return;
			}
			Logger.Info("主页自定义数据来源：联网缓存文件", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
			content = FileUtils.TryReadAsString(ModBase.PathTemp + "Cache\\Custom.xaml", (Encoding)null, (Type)null);
			OnlineLoader.Start((text, false));
		}
		ModBase.RunInUi([SpecialName] () =>
		{
			LoadContent(content);
		});
	}

	private void OnlineLoaderSub(ModLoader.LoaderTask<(string Address, bool ShouldRefresh), int> Task)
	{
		string item = Task.Input.Address;
		bool item2 = Task.Input.ShouldRefresh;
		try
		{
			string text;
			if (item.Contains(".xaml"))
			{
				text = item.Replace(".xaml", ".xaml.ini");
			}
			else
			{
				text = StringExtensions.BeforeFirst(item, "?", false);
				if (!StringExtensions.EndsWithF(text, "/", false))
				{
					text += "/";
				}
				text += "version";
				if (item.Contains("?"))
				{
					text = text + "?" + StringExtensions.AfterFirst(item, "?", false);
				}
			}
			string text2 = "";
			try
			{
				text2 = ModNet.NetRequestByClientRetry(text);
				if (text2.Length > 1000)
				{
					throw new Exception($"获取的主页版本过长（{text2.Length} 字符）");
				}
				string text3 = Settings.Get<string>("CacheSavedPageVersion");
				if (Operators.CompareString(text2, "", TextCompare: false) != 0 && Operators.CompareString(text3, "", TextCompare: false) != 0 && Operators.CompareString(text2, text3, TextCompare: false) == 0)
				{
					Logger.Info($"当前缓存的主页已为最新，当前版本：{text2}，检查源：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
					return;
				}
				Logger.Info($"需要下载联网主页，当前版本：{text2}，检查源：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "联网获取主页版本失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				Logger.Info($"无法检查联网主页版本，将直接下载，检查源：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				ProjectData.ClearProjectError();
			}
			string text4 = ModNet.NetRequestByClientRetry(item);
			Logger.Info($"已联网下载主页，内容长度：{text4.Length}，来源：{item}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
			Settings.Set("CacheSavedPageUrl", item);
			Settings.Set("CacheSavedPageVersion", text2);
			FileUtils.Write(ModBase.PathTemp + "Cache\\Custom.xaml", text4, (Encoding)null);
			ulong? loadedContentHash = LoadedContentHash;
			ulong stableHashCode = StringExtensions.GetStableHashCode(text4);
			bool? flag = (loadedContentHash.HasValue ? new bool?(loadedContentHash.GetValueOrDefault() != stableHashCode) : null);
			if ((flag ?? true) && item2 && flag.HasValue)
			{
				Refresh();
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Error(ex4, $"下载主页失败（{item}）", (LogBehavior)(ModBase.ModeDebug ? 3 : 2), "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	public void ForceRefresh()
	{
		Logger.Info("要求强制刷新主页", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
		ClearCache();
		if (ModMain.FrmMain.PageCurrent.Page == FormMain.PageType.Launch)
		{
			PanBack.ScrollToHome();
			Refresh();
		}
		else
		{
			ModMain.FrmMain.PageChange(FormMain.PageType.Launch);
		}
	}

	void IRefreshable.Refresh()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ForceRefresh
		this.ForceRefresh();
	}

	private void ClearCache()
	{
		LoadedContentHash = null;
		OnlineLoader.Input = (Address: "", ShouldRefresh: true);
		Settings.Set("CacheSavedPageUrl", "");
		Settings.Set("CacheSavedPageVersion", "");
		Logger.Info("已清空主页缓存", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
	}

	private void LoadContent(string Content)
	{
		try
		{
			object loadContentLock = LoadContentLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(loadContentLock);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(loadContentLock, ref lockTaken);
				ulong stableHashCode = StringExtensions.GetStableHashCode(Content ?? "");
				ulong num = stableHashCode;
				ulong? loadedContentHash = LoadedContentHash;
				if ((loadedContentHash.HasValue ? new bool?(num == loadedContentHash.GetValueOrDefault()) : null).GetValueOrDefault())
				{
					return;
				}
				LoadedContentHash = stableHashCode;
				PanCustom.Children.Clear();
				if (string.IsNullOrWhiteSpace(Content))
				{
					Logger.Info("实例化：清空主页 UI，来源为空", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
					return;
				}
				DateTime now = DateTime.Now;
				Content = ModMain.ArgumentReplace(Content, (Func<string, string>)StringUtils.XmlEscape, ReplaceTime: true);
				while (Content.Contains("xmlns"))
				{
					Content = StringExtensions.RegexReplace(Content, "xmlns[^\"']*(\"|')[^\"']*(\"|')", "", RegexOptions.None).Replace("xmlns", "");
				}
				Content = "<StackPanel xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:sys=\"clr-namespace:System;assembly=mscorlib\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\" xmlns:core=\"clr-namespace:MeloongCore;assembly=MeloongCore\" xmlns:corewpf=\"clr-namespace:MeloongCore.Wpf;assembly=MeloongCore.Wpf\">" + Content + "</StackPanel>";
				Logger.Info($"实例化：加载主页 UI 开始，最终内容长度：{Content.Count()}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				PanCustom.Children.Add((UIElement)ModBase.GetObjectFromXML(Content));
				int milliseconds = (DateTime.Now - now).Milliseconds;
				Logger.Info($"实例化：加载主页 UI 完成，耗时 {milliseconds}ms", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
				if (milliseconds > 3000)
				{
					ModMain.Hint($"主页加载过于缓慢（花费了 {Math.Round((double)milliseconds / 1000.0, 1)} 秒），请向主页作者反馈此问题，或暂时停止使用该主页");
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(loadContentLock);
				}
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, string.Format("加载失败的主页内容：{0}{1}", "\r\n", Content), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
			OnLoadContentFailed(ex2);
			ProjectData.ClearProjectError();
		}
	}

	private void OnLoadContentFailed(Exception ex)
	{
		if (ModBase.ModeDebug || Settings.Get<int>("UiCustomType") == 1)
		{
			Logger.Warn(ex, "加载主页失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
			if (ModMain.MyMsgBox((ex is UnauthorizedAccessException) ? ex.Message : string.Format("主页内容编写有误，请根据下列错误信息进行检查：{0}{1}", "\r\n", ExceptionExtensions.GetDisplay(ex, false)), "加载主页失败", "重试", "取消") == 1)
			{
				ForceRefresh();
			}
		}
		else
		{
			Logger.Error(ex, "加载主页失败", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLaunch\\PageLaunchRight.xaml.vb");
		}
	}

	private void DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
	{
		if (e.Exception is XamlParseException)
		{
			e.Handled = true;
			LoadContent(null);
			OnLoadContentFailed(e.Exception);
		}
	}

	void IDispatcherUnhandledException.DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
	{
		//ILSpy generated this explicit interface implementation from .override directive in DispatcherUnhandledException
		this.DispatcherUnhandledException(sender, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelaunch/pagelaunchright.xaml", UriKind.Relative);
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
			PanBack = (MyScrollViewer)target;
			break;
		case 2:
			PanMain = (StackPanel)target;
			break;
		case 3:
			PanHint = (MyHint)target;
			break;
		case 4:
			PanCustom = (StackPanel)target;
			break;
		case 5:
			PanLog = (MyCard)target;
			break;
		case 6:
			LabLog = (MyTextBox)target;
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
