using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageOtherHelp : MyPageRight, IRefreshable, IComponentConnector
{
	[CompilerGenerated]
	[AccessedThroughProperty("SearchBox")]
	private MySearchBox _SearchBox;

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

	internal virtual MySearchBox SearchBox
	{
		[CompilerGenerated]
		get
		{
			return _SearchBox;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MySearchBox.TextChangedEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				SearchRun();
			};
			MySearchBox searchBox = _SearchBox;
			if (searchBox != null)
			{
				searchBox.TextChanged -= obj;
			}
			_SearchBox = value;
			searchBox = _SearchBox;
			if (searchBox != null)
			{
				searchBox.TextChanged += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanSearch")]
	internal virtual MyCard PanSearch
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanSearchList")]
	internal virtual StackPanel PanSearchList
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanList")]
	internal virtual StackPanel PanList
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanLoad")]
	internal virtual MyCard PanLoad
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Load")]
	internal virtual MyLoading Load
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public PageOtherHelp()
	{
		base.Loaded += PageOther_Loaded;
		base.Initialized += PageOther_Inited;
		InitializeComponent();
	}

	private void PageOther_Loaded(object sender, RoutedEventArgs e)
	{
		PanBack.ScrollToHome();
	}

	private void PageOther_Inited(object sender, EventArgs e)
	{
		PageLoaderInit(Load, PanLoad, PanBack, null, ModMain.HelpLoader, HelpListLoad);
	}

	private void HelpListLoad(ModLoader.LoaderTask<int, List<ModMain.HelpEntry>> Loader)
	{
		try
		{
			PanList.Children.Clear();
			PanBack.ScrollToHome();
			List<ModMain.HelpEntry> output = Loader.Output;
			List<string> list = new List<string>();
			foreach (ModMain.HelpEntry item in output)
			{
				if (!item.ShowInSnapshot)
				{
					continue;
				}
				foreach (string type in item.Types)
				{
					if (!list.Contains(type))
					{
						list.Add(type);
					}
				}
			}
			if (list.Contains("指南"))
			{
				list.Remove("指南");
				list.Insert(0, "指南");
			}
			foreach (string item2 in list)
			{
				List<ModMain.HelpEntry> list2 = new List<ModMain.HelpEntry>();
				foreach (ModMain.HelpEntry item3 in output)
				{
					if (item3.ShowInSnapshot && item3.Types.Contains(item2))
					{
						list2.Add(item3);
					}
				}
				MyCard myCard = new MyCard
				{
					Title = item2,
					Margin = new Thickness(0.0, 0.0, 0.0, 15.0)
				};
				StackPanel stackPanel = new StackPanel
				{
					Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
					VerticalAlignment = VerticalAlignment.Top,
					RenderTransform = new TranslateTransform(0.0, 0.0)
				};
				myCard.Children.Add(stackPanel);
				myCard.SwapControl = stackPanel;
				myCard.SetContentBuilder(list2, [SpecialName] (ModMain.HelpEntry Entry) => Entry.ToListItem());
				if (Operators.CompareString(item2, "指南", TextCompare: false) == 0)
				{
					myCard.BuildContent();
				}
				else
				{
					myCard.IsSwapped = true;
				}
				PanList.Children.Add(myCard);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "加载帮助列表 UI 失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageOther\\PageOtherHelp.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	public static void OnItemClick(ModMain.HelpEntry Entry)
	{
		try
		{
			if (Entry.IsEvent)
			{
				CustomEvent.Raise(Entry.EventType, Entry.EventData);
			}
			else
			{
				EnterHelpPage(Entry);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "处理帮助项目点击时发生意外错误", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageOther\\PageOtherHelp.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	public static void EnterHelpPage(string Location)
	{
		ModBase.RunInThread([SpecialName] () =>
		{
			if (ModMain.HelpLoader.State != ModBase.LoadState.Finished)
			{
				ModMain.HelpLoader.WaitForExit(ModBase.GetUuid());
			}
			ModMain.HelpEntry helpEntry = new ModMain.HelpEntry(Location);
			ModBase.RunInUi([SpecialName] () =>
			{
				PageOtherHelpDetail pageOtherHelpDetail = new PageOtherHelpDetail();
				if (pageOtherHelpDetail.Init(helpEntry))
				{
					ModMain.FrmMain.PageChange(new FormMain.PageStackData
					{
						Page = FormMain.PageType.HelpDetail,
						Additional = new object[2] { helpEntry, pageOtherHelpDetail }
					});
				}
				else
				{
					Logger.Warn("已取消进入帮助项目，这一般是由于 xaml 初始化失败，且用户在弹窗中手动放弃", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageOther\\PageOtherHelp.xaml.vb");
				}
			});
		});
	}

	public static void EnterHelpPage(ModMain.HelpEntry Entry)
	{
		ModBase.RunInThread([SpecialName] () =>
		{
			if (ModMain.HelpLoader.State != ModBase.LoadState.Finished)
			{
				ModMain.HelpLoader.WaitForExit(ModBase.GetUuid());
			}
			ModBase.RunInUi([SpecialName] () =>
			{
				PageOtherHelpDetail pageOtherHelpDetail = new PageOtherHelpDetail();
				if (pageOtherHelpDetail.Init(Entry))
				{
					ModMain.FrmMain.PageChange(new FormMain.PageStackData
					{
						Page = FormMain.PageType.HelpDetail,
						Additional = new object[2] { Entry, pageOtherHelpDetail }
					});
				}
				else
				{
					Logger.Warn("已取消进入帮助项目，这一般是由于 xaml 初始化失败，且用户在弹窗中手动放弃", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageOther\\PageOtherHelp.xaml.vb");
				}
			});
		});
	}

	public void SearchRun()
	{
		if (string.IsNullOrWhiteSpace(SearchBox.Text))
		{
			ModAnimation.AniStart(new ModAnimation.AniData[3]
			{
				ModAnimation.AaOpacity(PanSearch, 0.0 - PanSearch.Opacity, 100),
				ModAnimation.AaCode([SpecialName] () =>
				{
					PanSearch.Height = 0.0;
					PanSearch.Visibility = Visibility.Collapsed;
					PanList.Visibility = Visibility.Visible;
				}, 0, After: true),
				ModAnimation.AaOpacity(PanList, 1.0 - PanList.Opacity, 150, 30)
			}, "FrmOtherHelp Search Switch");
			return;
		}
		List<ModBase.SearchEntry<ModMain.HelpEntry>> list = new List<ModBase.SearchEntry<ModMain.HelpEntry>>();
		foreach (ModMain.HelpEntry item in ModMain.HelpLoader.Output)
		{
			if (item.ShowInSearch && item.ShowInSearch && item.ShowInSnapshot)
			{
				list.Add(new ModBase.SearchEntry<ModMain.HelpEntry>
				{
					Item = item,
					SearchSource = new List<ModBase.SearchSource>
					{
						new ModBase.SearchSource(item.Title),
						new ModBase.SearchSource(item.Desc, 0.5),
						new ModBase.SearchSource(item.Search, 1.5)
					}
				});
			}
		}
		List<ModBase.SearchEntry<ModMain.HelpEntry>> list2 = ModBase.Search(list, SearchBox.Text, 5, 0.08);
		PanSearchList.Children.Clear();
		if (!list2.Any())
		{
			PanSearch.Title = "无搜索结果";
			PanSearchList.Visibility = Visibility.Collapsed;
		}
		else
		{
			PanSearch.Title = "搜索结果";
			foreach (ModBase.SearchEntry<ModMain.HelpEntry> item2 in list2)
			{
				MyListItem myListItem = item2.Item.ToListItem();
				if (ModBase.ModeDebug)
				{
					myListItem.Info = (item2.AbsoluteRight ? "完全匹配，" : "") + "相似度：" + Conversions.ToString(Math.Round(item2.Similarity, 3)) + "，" + myListItem.Info;
				}
				PanSearchList.Children.Add(myListItem);
			}
			PanSearchList.Visibility = Visibility.Visible;
		}
		ModAnimation.AniStart(new ModAnimation.AniData[3]
		{
			ModAnimation.AaOpacity(PanList, 0.0 - PanList.Opacity, 100),
			ModAnimation.AaCode([SpecialName] () =>
			{
				PanList.Visibility = Visibility.Collapsed;
				PanSearch.Visibility = Visibility.Visible;
				PanSearch.TriggerForceResize();
			}, 0, After: true),
			ModAnimation.AaOpacity(PanSearch, 1.0 - PanSearch.Opacity, 150, 30)
		}, "FrmOtherHelp Search Switch");
	}

	public void Refresh()
	{
		PageOtherLeft.RefreshHelp();
	}

	void IRefreshable.Refresh()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Refresh
		this.Refresh();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageother/pageotherhelp.xaml", UriKind.Relative);
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
			SearchBox = (MySearchBox)target;
			break;
		case 4:
			PanSearch = (MyCard)target;
			break;
		case 5:
			PanSearchList = (StackPanel)target;
			break;
		case 6:
			PanList = (StackPanel)target;
			break;
		case 7:
			PanLoad = (MyCard)target;
			break;
		case 8:
			Load = (MyLoading)target;
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
