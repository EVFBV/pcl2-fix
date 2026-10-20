using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using MeloongCore;
using MeloongCore.Extensions;
using MeloongCore.Wpf;
using Microsoft.VisualBasic.CompilerServices;
using PCLCS;

namespace PCL;

[DesignerGenerated]
public class PageDownloadResourceDetail : MyPageRight, IComponentConnector
{
	private class CardSorter : IComparer<string>
	{
		public string Topmost;

		public int Compare(string x, string y)
		{
			if (Operators.CompareString(x, y, TextCompare: false) == 0)
			{
				return 0;
			}
			if (Operators.CompareString(x, Topmost, TextCompare: false) == 0)
			{
				return -1;
			}
			if (Operators.CompareString(y, Topmost, TextCompare: false) == 0)
			{
				return 1;
			}
			bool flag = !x.Contains(".");
			bool flag2 = !y.Contains(".");
			if (flag && flag2)
			{
				return x.CompareTo(y);
			}
			if (flag)
			{
				return 1;
			}
			if (flag2)
			{
				return -1;
			}
			checked
			{
				int num = -McUtils.CompareVersion(x.Replace(StringExtensions.BeforeFirst(x, " ", false) + " ", ""), y.Replace(StringExtensions.BeforeFirst(y, " ", false) + " ", ""));
				if (num != 0)
				{
					return num;
				}
				return -McUtils.CompareVersion(x, y);
			}
		}

		int IComparer<string>.Compare(string x, string y)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Compare
			return this.Compare(x, y);
		}

		public CardSorter(string Topmost = "")
		{
			this.Topmost = "";
			this.Topmost = Topmost ?? "";
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__16_002D0
	{
		public List<string> _0024VB_0024Local_Filters;

		public List<ResourceVersion> _0024VB_0024Local_Results;

		public PageDownloadResourceDetail _0024VB_0024Me;

		public _Closure_0024__16_002D0(_Closure_0024__16_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Filters = arg0._0024VB_0024Local_Filters;
				_0024VB_0024Local_Results = arg0._0024VB_0024Local_Results;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			_0024VB_0024Local_Filters = (from v in _0024VB_0024Local_Results.SelectMany([SpecialName] (ResourceVersion v) => v.GameVersions)
				select _0024VB_0024Me.GetGroupedVersionName(v, _0024VB_0024Me.GroupedDrop, _0024VB_0024Me.GroupedOld)).Distinct().OrderByDescending([SpecialName] (string s) => s, Comparer<string>.Create([SpecialName] (string x, string y) => McUtils.CompareVersion(x, y))).ToList();
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__22_002D0
	{
		public string _0024VB_0024Local_Target;

		public string _0024VB_0024Local_InstanceName;

		public string _0024VB_0024Local_LogoFileAddress;

		[SpecialName]
		internal void _Lambda_0024__R6(ModLoader.LoaderTask<int, int> a0)
		{
			_Lambda_0024__0();
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			ModModpack.ModpackInstall(_0024VB_0024Local_Target, _0024VB_0024Local_InstanceName, FileUtils.Exists(_0024VB_0024Local_LogoFileAddress) ? _0024VB_0024Local_LogoFileAddress : null);
		}
	}

	private MyResourceItem ResourceItem;

	private ResourceTypes TargetResourceType;

	private ResourceProject Project;

	private string TargetVersion;

	private ModLoaders TargetLoaders;

	private ModLoader.LoaderTask<int, List<ResourceVersion>> ResourceFileLoader;

	private string VersionFilter;

	private bool GroupedDrop;

	private bool GroupedOld;

	private bool IsFirstInit;

	public static Dictionary<ResourceTypes, string> CachedFolder = new Dictionary<ResourceTypes, string>();

	[CompilerGenerated]
	[AccessedThroughProperty("BtnIntroWeb")]
	private MyButton _BtnIntroWeb;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnIntroWiki")]
	private MyButton _BtnIntroWiki;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnIntroCopy")]
	private MyButton _BtnIntroCopy;

	[CompilerGenerated]
	[AccessedThroughProperty("HintAlternative")]
	private MyHint _HintAlternative;

	[CompilerGenerated]
	[AccessedThroughProperty("Load")]
	private MyLoading _Load;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyScrollViewer PanBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardIntro")]
	internal virtual MyCard CardIntro
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanIntro")]
	internal virtual StackPanel PanIntro
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnIntroWeb
	{
		[CompilerGenerated]
		get
		{
			return _BtnIntroWeb;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnIntroWeb_Click;
			MyButton btnIntroWeb = _BtnIntroWeb;
			if (btnIntroWeb != null)
			{
				btnIntroWeb.Click -= obj;
			}
			_BtnIntroWeb = value;
			btnIntroWeb = _BtnIntroWeb;
			if (btnIntroWeb != null)
			{
				btnIntroWeb.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnIntroWiki
	{
		[CompilerGenerated]
		get
		{
			return _BtnIntroWiki;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnIntroWiki_Click;
			MyButton btnIntroWiki = _BtnIntroWiki;
			if (btnIntroWiki != null)
			{
				btnIntroWiki.Click -= obj;
			}
			_BtnIntroWiki = value;
			btnIntroWiki = _BtnIntroWiki;
			if (btnIntroWiki != null)
			{
				btnIntroWiki.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnIntroCopy
	{
		[CompilerGenerated]
		get
		{
			return _BtnIntroCopy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnIntroCopy_Click;
			MyButton btnIntroCopy = _BtnIntroCopy;
			if (btnIntroCopy != null)
			{
				btnIntroCopy.Click -= obj;
			}
			_BtnIntroCopy = value;
			btnIntroCopy = _BtnIntroCopy;
			if (btnIntroCopy != null)
			{
				btnIntroCopy.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanMain")]
	internal virtual StackPanel PanMain
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyHint HintAlternative
	{
		[CompilerGenerated]
		get
		{
			return _HintAlternative;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyHint.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				HintAlternative_Click();
			};
			MyHint hintAlternative = _HintAlternative;
			if (hintAlternative != null)
			{
				hintAlternative.Click -= obj;
			}
			_HintAlternative = value;
			hintAlternative = _HintAlternative;
			if (hintAlternative != null)
			{
				hintAlternative.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("CardFilter")]
	internal virtual MyCard CardFilter
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanFilter")]
	internal virtual StackPanel PanFilter
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanResults")]
	internal virtual StackPanel PanResults
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

	internal virtual MyLoading Load
	{
		[CompilerGenerated]
		get
		{
			return _Load;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyLoading.StateChangedEventHandler obj = Load_State;
			MyLoading load = _Load;
			if (load != null)
			{
				load.StateChanged -= obj;
			}
			_Load = value;
			load = _Load;
			if (load != null)
			{
				load.StateChanged += obj;
			}
		}
	}

	public PageDownloadResourceDetail()
	{
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			LoadPageArguments();
		};
		base.Initialized += PageDownloadResourceDetail_Inited;
		base.PageEnter += Init;
		ResourceItem = null;
		ResourceFileLoader = new ModLoader.LoaderTask<int, List<ResourceVersion>>("Resource File", [SpecialName] (ModLoader.LoaderTask<int, List<ResourceVersion>> Task) =>
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			LoadPageArguments();
			List<ResourceVersion> output = ResourceVersion.FromProjectId(Project.Id, Project.Platform);
			if (!Task.IsCanceled)
			{
				Task.Output = output;
			}
		});
		IsFirstInit = true;
		InitializeComponent();
	}

	public void LoadPageArguments()
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		Project = (ResourceProject)NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 0 }, null);
		TargetVersion = Conversions.ToString(NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 2 }, null));
		TargetLoaders = (ModLoaders)Conversions.ToInteger(NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 3 }, null));
		if ((int)TargetLoaders != 0 && !EnumerableExtensions.IsSingle<ModLoaders>(EnumerableExtensions.Flags<ModLoaders>(TargetLoaders)))
		{
			TargetLoaders = EnumerableExtensions.Flags<ModLoaders>(TargetLoaders).First();
		}
		TargetResourceType = (ResourceTypes)Conversions.ToInteger(NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 4 }, null));
	}

	private void PageDownloadResourceDetail_Inited(object sender, EventArgs e)
	{
		LoadPageArguments();
		PageLoaderInit(Load, PanLoad, PanMain, CardIntro, ResourceFileLoader, [SpecialName] (ModLoader.LoaderTask<int, List<ResourceVersion>> a0) =>
		{
			Load_OnFinish();
		});
	}

	private void Load_State(object sender, MyLoading.MyLoadingState state, MyLoading.MyLoadingState oldState)
	{
		ModBase.LoadState state2 = ResourceFileLoader.State;
		if (state2 == ModBase.LoadState.Failed)
		{
			string text = "";
			if (ResourceFileLoader.Error != null)
			{
				text = ResourceFileLoader.Error.Message;
			}
			if (text.Contains("不是有效的 json 文件"))
			{
				Logger.Warn("下载的文件 json 列表损坏，已自动重试", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
				PageLoaderRestart();
			}
		}
	}

	private List<ResourceVersion> GetResults()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Invalid comparison between Unknown and I4
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Invalid comparison between Unknown and I4
		List<ResourceVersion> list = ResourceFileLoader.Output;
		ResourceTypes targetResourceType = TargetResourceType;
		if ((int)targetResourceType != 4 && (int)targetResourceType != 8)
		{
			list = (((int)targetResourceType != 127) ? list.Where([SpecialName] (ResourceVersion r) => r.ResourceType == TargetResourceType).ToList() : list.Where([SpecialName] (ResourceVersion r) => (int)r.ResourceType != 32).ToList());
		}
		return list;
	}

	private void Load_OnFinish()
	{
		_Closure_0024__16_002D0 arg = default(_Closure_0024__16_002D0);
		_Closure_0024__16_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__16_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Me = this;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Results = GetResults();
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Filters = null;
		VB_0024AnonymousDelegate_10 vB_0024AnonymousDelegate_ = [SpecialName] () =>
		{
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Filters = (from v in CS_0024_003C_003E8__locals0._0024VB_0024Local_Results.SelectMany([SpecialName] (ResourceVersion v) => v.GameVersions)
				select CS_0024_003C_003E8__locals0._0024VB_0024Me.GetGroupedVersionName(v, CS_0024_003C_003E8__locals0._0024VB_0024Me.GroupedDrop, CS_0024_003C_003E8__locals0._0024VB_0024Me.GroupedOld)).Distinct().OrderByDescending([SpecialName] (string s) => s, Comparer<string>.Create([SpecialName] (string x, string y) => McUtils.CompareVersion(x, y))).ToList();
		};
		GroupedDrop = false;
		GroupedOld = false;
		vB_0024AnonymousDelegate_();
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Filters.Count >= 9)
		{
			GroupedDrop = true;
			GroupedOld = false;
			vB_0024AnonymousDelegate_();
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Filters.Count >= 9)
			{
				GroupedDrop = false;
				GroupedOld = true;
				vB_0024AnonymousDelegate_();
				if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Filters.Count >= 9)
				{
					GroupedDrop = true;
					GroupedOld = true;
					vB_0024AnonymousDelegate_();
				}
			}
		}
		PanFilter.Children.Clear();
		if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Filters.Count < 2)
		{
			CardFilter.Visibility = Visibility.Collapsed;
			VersionFilter = null;
		}
		else
		{
			CardFilter.Visibility = Visibility.Visible;
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Filters.Insert(0, "全部");
			foreach (string _0024VB_0024Local_Filter in CS_0024_003C_003E8__locals0._0024VB_0024Local_Filters)
			{
				MyRadioButton myRadioButton = new MyRadioButton
				{
					Text = _0024VB_0024Local_Filter,
					Margin = new Thickness(2.0, 0.0, 2.0, 0.0),
					ColorType = MyRadioButton.ColorState.Highlight
				};
				myRadioButton.LabText.Margin = new Thickness(-2.0, 0.0, 10.0, 0.0);
				myRadioButton.Check += [SpecialName] (object a0, bool a1) =>
				{
					_Lambda_0024__16_002D5((MyRadioButton)a0, a1);
				};
				PanFilter.Children.Add(myRadioButton);
			}
			MyRadioButton myRadioButton2 = null;
			if (Operators.CompareString(TargetVersion, "", TextCompare: false) != 0 && CS_0024_003C_003E8__locals0._0024VB_0024Local_Results.FirstOrDefault([SpecialName] (ResourceVersion v) => v.GameVersions.Contains(TargetVersion)) != null)
			{
				string groupedVersionName = GetGroupedVersionName(TargetVersion, GroupedDrop, GroupedOld);
				foreach (MyRadioButton child in PanFilter.Children)
				{
					if (Operators.CompareString(child.Text, groupedVersionName, TextCompare: false) == 0)
					{
						myRadioButton2 = child;
						break;
					}
				}
			}
			if (myRadioButton2 == null)
			{
				myRadioButton2 = (MyRadioButton)PanFilter.Children[0];
			}
			myRadioButton2.Checked = true;
		}
		UpdateFilterResult();
	}

	private void UpdateFilterResult()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Invalid comparison between Unknown and I4
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Invalid comparison between Unknown and I4
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Invalid comparison between Unknown and I4
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Invalid comparison between Unknown and I4
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Invalid comparison between Unknown and I4
		List<ResourceVersion> results = GetResults();
		string text = ((Operators.CompareString(TargetVersion, "", TextCompare: false) != 0 || (int)TargetLoaders != 0) ? string.Format("{0}{1}（所选版本）", ((int)TargetLoaders != 0) ? (((Enum)(ModLoaders)(ref TargetLoaders)).ToString() + " ") : "", TargetVersion) : "");
		SortedDictionary<string, List<ResourceVersion>> sortedDictionary = new SortedDictionary<string, List<ResourceVersion>>(new CardSorter(text));
		sortedDictionary.Add("其他", new List<ResourceVersion>());
		foreach (ResourceVersion item in results)
		{
			foreach (string gameVersion in item.GameVersions)
			{
				if (VersionFilter != null && Operators.CompareString(GetGroupedVersionName(gameVersion, GroupedDrop, GroupedOld), VersionFilter, TextCompare: false) != 0)
				{
					continue;
				}
				string groupedVersionName = GetGroupedVersionName(gameVersion, GroupedByDrop: false, FoldOld: false);
				List<string> list = new List<string>();
				if (EnumerableExtensions.Flags<ModLoaders>(Project.ModLoaders).Count() > 1 && (int)item.ResourceType == 1 && McVersion.IsStandardFormat(groupedVersionName))
				{
					list.AddRange(from f in EnumerableExtensions.Flags<ModLoaders>(item.ModLoaders)
						select ((Enum)(ModLoaders)(ref f)).ToString() + " ");
				}
				if (!list.Any())
				{
					list.Add("");
				}
				foreach (string item2 in list)
				{
					string key = item2 + groupedVersionName;
					if (!sortedDictionary.ContainsKey(key))
					{
						sortedDictionary.Add(key, new List<ResourceVersion>());
					}
					if (!sortedDictionary[key].Contains(item))
					{
						sortedDictionary[key].Add(item);
					}
				}
			}
		}
		if (Operators.CompareString(text, "", TextCompare: false) != 0 && (VersionFilter == null || StringExtensions.StartsWithF(GetGroupedVersionName(TargetVersion, GroupedDrop, GroupedOld), VersionFilter, false)))
		{
			sortedDictionary.Add(text, new List<ResourceVersion>());
			foreach (ResourceVersion item3 in results)
			{
				if (item3.GameVersions.Contains(TargetVersion) && ((int)TargetLoaders == 0 || EnumerableExtensions.Flags<ModLoaders>(TargetLoaders).Intersect(EnumerableExtensions.Flags<ModLoaders>(item3.ModLoaders)).Any()) && (VersionFilter == null || item3.GameVersions.Any([SpecialName] (string v) => Operators.CompareString(GetGroupedVersionName(v, GroupedDrop, GroupedOld), VersionFilter, TextCompare: false) == 0)) && !sortedDictionary[text].Contains(item3))
				{
					sortedDictionary[text].Add(item3);
				}
			}
		}
		try
		{
			PanResults.Children.Clear();
			foreach (KeyValuePair<string, List<ResourceVersion>> item4 in sortedDictionary)
			{
				if (item4.Value.Any() && Operators.CompareString(item4.Key, text.Replace("（所选版本）", ""), TextCompare: false) != 0)
				{
					MyCard myCard = new MyCard
					{
						Title = item4.Key,
						Margin = new Thickness(0.0, 0.0, 0.0, 15.0)
					};
					StackPanel stackPanel = new StackPanel
					{
						Margin = new Thickness(20.0, 40.0, 18.0, 0.0),
						VerticalAlignment = VerticalAlignment.Top,
						RenderTransform = new TranslateTransform(0.0, 0.0),
						Tag = item4.Value
					};
					myCard.Children.Add(stackPanel);
					myCard.SwapControl = stackPanel;
					myCard.SetContentBuilder([SpecialName] (Panel a0) =>
					{
						InitializeStackResourceVersions((StackPanel)a0);
					});
					PanResults.Children.Add(myCard);
					if (Operators.CompareString(item4.Key, text, TextCompare: false) == 0 || (ModMain.FrmMain.PageCurrent.Additional != null && ((List<string>)NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 1 }, null)).Contains(myCard.Title)))
					{
						myCard.BuildContent();
					}
					else
					{
						myCard.IsSwapped = true;
					}
					if (Operators.CompareString(item4.Key, "其他", TextCompare: false) == 0)
					{
						stackPanel.Children.Add(new MyHint
						{
							Text = "由于版本信息更新缓慢，可能无法识别刚更新的 MC 版本。几天后即可正常识别。",
							Theme = MyHint.Themes.Yellow,
							Margin = new Thickness(5.0, 0.0, 0.0, 8.0)
						});
					}
				}
			}
			if (PanResults.Children.Count == 1)
			{
				((MyCard)PanResults.Children[0]).IsSwapped = false;
			}
			if ((int)Project.Types == 17 && ((int)TargetResourceType == 1 || (int)TargetResourceType == 16))
			{
				HintAlternative.Visibility = Visibility.Visible;
				HintAlternative.Text = (((int)TargetResourceType == 1) ? "以下是该项目的 Mod 版本。点击这里查看其数据包版本。" : "以下是该项目的数据包版本。点击这里查看其 Mod 版本。");
			}
			else
			{
				HintAlternative.Visibility = Visibility.Collapsed;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "可视化工程下载列表出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void InitializeStackResourceVersions(StackPanel Stack)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Invalid comparison between Unknown and I4
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Invalid comparison between Unknown and I4
		List<ResourceVersion> list = ModBase.SortByComparison((List<ResourceVersion>)Stack.Tag, [SpecialName] (ResourceVersion a, ResourceVersion b) => DateTime.Compare(a.ReleaseDate, b.ReleaseDate) > 0);
		bool isBadDisplay = EnumerableExtensions.DistinctBy<ResourceVersion, string>((IEnumerable<ResourceVersion>)list, (Func<ResourceVersion, string>)([SpecialName] (ResourceVersion a) => a.Display)).Count() != list.Count;
		List<string> source = (((int)TargetResourceType != 2) ? EnumerableExtensions.Ordered<string>(list.SelectMany([SpecialName] (ResourceVersion f) => f.Dependencies).Distinct()).ToList() : new List<string>());
		if (source.Any())
		{
			source = source.Where([SpecialName] (string d) =>
			{
				if (!ResourceProject.Cache.ContainsKey(d))
				{
					Logger.Warn($"未找到 ID {d} 的前置信息", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
				}
				return ResourceProject.Cache.ContainsKey(d);
			}).ToList();
			Stack.Children.Add(new TextBlock
			{
				Text = "前置资源",
				FontSize = 14.0,
				HorizontalAlignment = HorizontalAlignment.Left,
				Margin = new Thickness(6.0, 2.0, 0.0, 5.0)
			});
			foreach (string item in source)
			{
				MyVirtualizingElement<MyResourceItem> element = ResourceProject.Cache[item].ToResourceItem(ShowMcVersionDesc: false, ShowLoaderDesc: false);
				Stack.Children.Add(element);
			}
			Stack.Children.Add(new TextBlock
			{
				Text = "版本列表",
				FontSize = 14.0,
				HorizontalAlignment = HorizontalAlignment.Left,
				Margin = new Thickness(6.0, 12.0, 0.0, 5.0)
			});
		}
		foreach (ResourceVersion item2 in list)
		{
			if ((int)TargetResourceType == 2)
			{
				Stack.Children.Add(item2.ToListItem([SpecialName] (object a0, MouseButtonEventArgs a1) =>
				{
					Install_Click((MyListItem)a0, a1);
				}, Save_Click, isBadDisplay));
			}
			else
			{
				Stack.Children.Add(item2.ToListItem(Save_Click, null, isBadDisplay));
			}
		}
		Stack.Children.Add(new FrameworkElement
		{
			Height = 18.0
		});
		Stack.Tag = null;
	}

	private string GetGroupedVersionName(string Name, bool GroupedByDrop, bool FoldOld)
	{
		int num = McVersion.VersionToDrop(Name);
		if (Name == null)
		{
			return "其他";
		}
		if (Name.Contains("w") || num == 209)
		{
			return "快照版";
		}
		if (!McVersion.IsStandardFormat(Name) || (FoldOld && num < 120))
		{
			return "远古版";
		}
		if (GroupedByDrop)
		{
			return McVersion.DropToVersion(num);
		}
		return Name;
	}

	public void Init()
	{
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		checked
		{
			ModAnimation.AniControlEnabled++;
			Project = (ResourceProject)NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 0 }, null);
			PanBack.ScrollToHome();
			if (IsFirstInit)
			{
				IsFirstInit = false;
			}
			else
			{
				PageLoaderRestart();
			}
			if (ResourceItem != null)
			{
				PanIntro.Children.Remove(ResourceItem);
			}
			ResourceItem = Project.ToResourceItem(ShowMcVersionDesc: true, ShowLoaderDesc: true).Init();
			ResourceItem.CanInteraction = false;
			ResourceItem.Margin = new Thickness(-7.0, -7.0, 0.0, 8.0);
			PanIntro.Children.Insert(0, ResourceItem);
			BtnIntroWeb.Text = $"转到 {Project.Platform}";
			BtnIntroWiki.Visibility = ((Project.WikiId == 0) ? Visibility.Collapsed : Visibility.Visible);
			ModAnimation.AniControlEnabled--;
		}
	}

	public void Install_Click(MyListItem sender, EventArgs e)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Invalid comparison between Unknown and I4
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			_Closure_0024__22_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__22_002D0();
			ResourceVersion resourceVersion = (ResourceVersion)sender.Tag;
			string name = $"{Project.Platform} 整合包下载：{Project.TranslatedName} ";
			string text = Project.TranslatedName.Replace(".zip", "").Replace(".rar", "").Replace(".mrpack", "")
				.Replace("\\", "＼")
				.Replace("/", "／")
				.Replace("|", "｜")
				.Replace(":", "：")
				.Replace("<", "＜")
				.Replace(">", "＞")
				.Replace("*", "＊")
				.Replace("?", "？")
				.Replace("\"", "")
				.Replace("： ", "：");
			ValidateFolderName validateFolderName = new ValidateFolderName(ModMinecraft.McFolderSelected + "versions");
			if (Operators.CompareString(validateFolderName.Validate(text), "", TextCompare: false) != 0)
			{
				text = "";
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_InstanceName = ModMain.MyMsgBoxInput("输入版本名称", "", text, new Collection<Validate> { validateFolderName });
			if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals0._0024VB_0024Local_InstanceName))
			{
				return;
			}
			List<ModLoader.LoaderBase> list = new List<ModLoader.LoaderBase>();
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Target = string.Format("{0}versions\\{1}\\原始整合包.{2}", ModMinecraft.McFolderSelected, CS_0024_003C_003E8__locals0._0024VB_0024Local_InstanceName, ((int)Project.Platform == 1) ? "zip" : "mrpack");
			CS_0024_003C_003E8__locals0._0024VB_0024Local_LogoFileAddress = ResourceItem.PathLogo.ActualSource;
			list.Add(new ModNet.LoaderDownload("下载整合包文件", new ModNet.NetFile[1] { resourceVersion.ToNetFile(CS_0024_003C_003E8__locals0._0024VB_0024Local_Target, ResourceVersion.DownloadReason.Standalone, resourceVersion.GameVersions.FirstOrDefault(), resourceVersion.ModLoaders) })
			{
				ProgressWeight = 10.0,
				Block = true
			});
			list.Add(new ModLoader.LoaderTask<int, int>("准备安装整合包", [SpecialName] (ModLoader.LoaderTask<int, int> a0) =>
			{
				CS_0024_003C_003E8__locals0._Lambda_0024__0();
			})
			{
				ProgressWeight = 0.1
			});
			ModLoader.LoaderCombo<string> loaderCombo = new ModLoader.LoaderCombo<string>(name, list);
			loaderCombo.OnStateChanged = [SpecialName] (ModLoader.LoaderBase MyLoader) =>
			{
				switch (MyLoader.State)
				{
				case ModBase.LoadState.Failed:
					ModMain.Hint(MyLoader.Name + "失败：" + ExceptionExtensions.GetDisplay(MyLoader.Error, false), ModMain.HintType.Red);
					break;
				case ModBase.LoadState.Canceled:
					ModMain.Hint(MyLoader.Name + "已取消！");
					break;
				case ModBase.LoadState.Loading:
					return;
				}
				ModDownloadLib.McInstallFailedClearFolder(MyLoader);
			};
			loaderCombo.Start(ModMinecraft.McFolderSelected + "versions\\" + CS_0024_003C_003E8__locals0._0024VB_0024Local_InstanceName + "\\");
			ModLoader.LoaderTaskbarAdd(loaderCombo);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.PageChange(FormMain.PageType.TaskManager);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "下载资源整合包失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	public void Save_Click(object sender, EventArgs e)
	{
		ResourceVersion resourceVersion = (ResourceVersion)NewLateBinding.LateGet((sender is MyListItem) ? sender : NewLateBinding.LateGet(sender, null, "Parent", new object[0], null, null, null), null, "Tag", new object[0], null, null, null);
		ModBase.RunInNewThread([SpecialName] () =>
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected I4, but got Unknown
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Invalid comparison between Unknown and I4
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Invalid comparison between Unknown and I4
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Invalid comparison between Unknown and I4
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Invalid comparison between Unknown and I4
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ca: Invalid comparison between Unknown and I4
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Invalid comparison between Unknown and I4
			//IL_058f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0595: Invalid comparison between Unknown and I4
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Invalid comparison between Unknown and I4
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Invalid comparison between Unknown and I4
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_013e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0127: Unknown result type (might be due to invalid IL or missing references)
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				string text = null;
				ResourceTypes resourceType = resourceVersion.ResourceType;
				switch (resourceType - 1)
				{
				default:
					if ((int)resourceType != 8)
					{
						if ((int)resourceType == 16)
						{
							text = "数据包";
						}
					}
					else
					{
						text = "光影包";
					}
					break;
				case 1:
					text = "整合包";
					break;
				case 0:
					text = "Mod ";
					break;
				case 3:
					text = "资源包";
					break;
				case 2:
					break;
				}
				string text2 = null;
				if ((int)resourceVersion.ResourceType != 2)
				{
					string arg = null;
					ResourceTypes resourceType2 = resourceVersion.ResourceType;
					if ((int)resourceType2 <= 4)
					{
						if ((int)resourceType2 != 1)
						{
							if ((int)resourceType2 == 4)
							{
								arg = "resourcepacks\\";
							}
						}
						else
						{
							arg = "mods\\";
						}
					}
					else if ((int)resourceType2 != 8)
					{
						if ((int)resourceType2 == 16)
						{
							arg = "";
						}
					}
					else
					{
						arg = "shaderpacks\\";
					}
					Func<ModMinecraft.McInstance, bool> func = null;
					ModLoaders val = (ModLoaders)0;
					if ((int)resourceVersion.ModLoaders != 0)
					{
						val = resourceVersion.ModLoaders;
					}
					else if ((int)Project.ModLoaders != 0)
					{
						val = Project.ModLoaders;
					}
					Logger.Info($"{text}要求的加载器种类：{val}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
					func = [SpecialName] (ModMinecraft.McInstance Instance) =>
					{
						//IL_0036: Unknown result type (might be due to invalid IL or missing references)
						//IL_003c: Invalid comparison between Unknown and I4
						//IL_004e: Unknown result type (might be due to invalid IL or missing references)
						//IL_0055: Invalid comparison between Unknown and I4
						//IL_0084: Unknown result type (might be due to invalid IL or missing references)
						//IL_008c: Unknown result type (might be due to invalid IL or missing references)
						//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
						if (!Instance.IsLoaded)
						{
							Instance.Load();
						}
						return (((int)resourceVersion.ResourceType != 1 && (int)resourceVersion.ResourceType != 16) || resourceVersion.GameVersions.Any([SpecialName] (string FileVersion) =>
						{
							bool flag2 = FileVersion.Contains("预览版") || !FileVersion.Contains(".");
							bool flag3 = Instance.Version.VanillaName.Contains("snapshot") || !Instance.Version.VanillaName.Contains(".");
							if (flag2 != flag3)
							{
								return false;
							}
							if (!flag2 && !flag3)
							{
								return Operators.CompareString(FileVersion, Instance.Version.VanillaName, TextCompare: false) == 0;
							}
							bool flag4 = FileVersion.Contains(".") && ModBase.Val(StringExtensions.BeforeFirst(FileVersion, ".", false)) > 1.0;
							bool flag5 = Instance.Version.Drop > 250;
							if (flag4 != flag5)
							{
								return false;
							}
							if (flag4 && flag5)
							{
								return Operators.CompareString(StringExtensions.BeforeFirst(FileVersion, " ", false), StringExtensions.BeforeFirst(Instance.Version.VanillaName, "-", false), TextCompare: false) == 0;
							}
							return !FileVersion.Contains("w") || !Instance.Version.VanillaName.Contains("w") || Operators.CompareString(FileVersion, Instance.Version.VanillaName, TextCompare: false) == 0;
						})) && ((int)val == 0 || EnumerableExtensions.Flags<ModLoaders>(val).Intersect(EnumerableExtensions.Flags<ModLoaders>(Instance.Version.ModLoaders)).Any());
					};
					if (CachedFolder.ContainsKey(resourceVersion.ResourceType))
					{
						text2 = CachedFolder[resourceVersion.ResourceType];
						Logger.Info($"使用上次下载时的文件夹作为默认下载位置：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
					}
					else if ((object)ModMinecraft.McInstanceSelected != null && func(ModMinecraft.McInstanceSelected))
					{
						text2 = $"{ModMinecraft.McInstanceSelected.PathIndie}{arg}";
						DirectoryUtils.Create(text2);
						Logger.Info($"使用当前版本作为默认下载位置：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
					}
					else
					{
						bool flag = ModMinecraft.McInstanceListLoader.State != ModBase.LoadState.Finished;
						if (flag)
						{
							ModMain.Hint("正在查找适合的游戏版本……");
							ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\", WaitForExit: true);
						}
						IEnumerable<DirectoryInfo> source = from v in ModMinecraft.McInstanceList.Values.SelectMany([SpecialName] (List<ModMinecraft.McInstance> l) => l)
							where func(v)
							select DirectoryUtils.GetInfo($"{v.PathIndie}{arg}");
						if (source.Any())
						{
							DirectoryInfo directoryInfo = (from Dir in source
								orderby (!Dir.Exists) ? DateTime.MinValue : Dir.LastWriteTimeUtc descending, (!Dir.Exists) ? (-1) : Dir.GetFiles().Length descending
								select Dir).First();
							text2 = PathUtils.RemoveExtendedPrefix(directoryInfo.FullName);
							DirectoryUtils.Create(text2);
							Logger.Info($"使用适合的游戏版本作为默认下载位置：{text2}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
						}
						else
						{
							text2 = ModMinecraft.McFolderSelected;
							if (flag)
							{
								ModMain.Hint("当前 MC 文件夹中没有找到合适的版本！");
							}
							else
							{
								Logger.Info("由于当前版本不兼容，使用当前的 MC 文件夹作为默认下载位置", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
							}
						}
					}
				}
				string text3;
				if (Operators.CompareString(Project.TranslatedName, Project.RawName, TextCompare: false) == 0)
				{
					text3 = resourceVersion.FileName;
				}
				else
				{
					string text4 = StringExtensions.BeforeFirst(StringExtensions.BeforeFirst(Project.TranslatedName, " (", false), " - ", false).Replace("\\", "＼").Replace("/", "／")
						.Replace("|", "｜")
						.Replace(":", "：")
						.Replace("<", "＜")
						.Replace(">", "＞")
						.Replace("*", "＊")
						.Replace("?", "？")
						.Replace("\"", "")
						.Replace("： ", "：");
					switch (Settings.Get<int>("ToolDownloadTranslateV2"))
					{
					case 0:
						text3 = $"【{text4}】{resourceVersion.FileName}";
						break;
					case 1:
						text3 = $"[{text4}] {resourceVersion.FileName}";
						break;
					case 2:
						text3 = $"{text4}-{resourceVersion.FileName}";
						break;
					case 3:
						text3 = $"{resourceVersion.FileName}-{text4}";
						break;
					default:
						text3 = resourceVersion.FileName;
						break;
					}
				}
				if ((int)resourceVersion.ResourceType == 1)
				{
					text3 = text3.Replace("~", "-");
				}
				ModBase.RunInUi([SpecialName] () =>
				{
					//IL_0018: Unknown result type (might be due to invalid IL or missing references)
					//IL_001e: Invalid comparison between Unknown and I4
					//IL_0157: Unknown result type (might be due to invalid IL or missing references)
					//IL_015e: Invalid comparison between Unknown and I4
					//IL_0112: Unknown result type (might be due to invalid IL or missing references)
					string item = (((int)resourceVersion.ResourceType != 1) ? ((Operators.CompareString(PathUtils.GetExtension(resourceVersion.FileName), "mrpack", TextCompare: false) == 0) ? "mrpack" : "zip") : ((Operators.CompareString(PathUtils.GetExtension(resourceVersion.FileName), "litemod", TextCompare: false) == 0) ? "litemod" : "jar"));
					string text5 = Dialogs.SaveFile("选择保存位置", text3, text2, (IEnumerable<ValueTuple<string, string>>)new(string, string)[1] { (item, text + "文件") });
					if (text5 != null)
					{
						string name = text + "下载：" + PathUtils.GetFileNameWithoutExtension(text5) + " ";
						if (!PathUtils.IsSame(PathUtils.RemoveLastPart(text5), text2))
						{
							CachedFolder[resourceVersion.ResourceType] = PathUtils.RemoveLastPart(text5);
						}
						ModLoader.LoaderCombo<int> loaderCombo = new ModLoader.LoaderCombo<int>(name, new List<ModLoader.LoaderBase>
						{
							new ModNet.LoaderDownload("下载文件", new ModNet.NetFile[1] { resourceVersion.ToNetFile(text5, ((int)TargetResourceType == 127) ? ResourceVersion.DownloadReason.Dependency : ResourceVersion.DownloadReason.Standalone, null, (ModLoaders)0) })
							{
								ProgressWeight = 6.0,
								Block = true
							}
						});
						loaderCombo.OnStateChanged = [SpecialName] (ModLoader.LoaderBase MyLoader) =>
						{
							//IL_001f: Unknown result type (might be due to invalid IL or missing references)
							//IL_0025: Invalid comparison between Unknown and I4
							ModDownloadLib.LoaderStateChangedHintOnly(MyLoader);
							if (MyLoader.State == ModBase.LoadState.Finished && (int)resourceVersion.ResourceType == 2)
							{
								ModBase.OpenExplorer(text5);
							}
						};
						loaderCombo.Start(1);
						ModLoader.LoaderTaskbarAdd(loaderCombo);
						ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
						ModMain.FrmMain.BtnExtraDownload.Ribble();
					}
				});
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "保存资源文件失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageDownloadCompDetail.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}, "Download Resource Detail Save");
	}

	private void BtnIntroWeb_Click(object sender, EventArgs e)
	{
		ModBase.OpenWebsite(Project.Website);
	}

	private void BtnIntroWiki_Click(object sender, EventArgs e)
	{
		ModBase.OpenWebsite("https://www.mcmod.cn/class/" + Conversions.ToString(Project.WikiId) + ".html");
	}

	private void BtnIntroCopy_Click(object sender, EventArgs e)
	{
		ModBase.ClipboardSet(ResourceItem.LabTitle.Text + ResourceItem.LabTitleRaw.Text);
	}

	private void HintAlternative_Click()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		TargetResourceType = (ResourceTypes)(((int)TargetResourceType != 1) ? 1 : 16);
		NewLateBinding.LateIndexSet(ModMain.FrmMain.PageCurrent.Additional, new object[2] { 4, TargetResourceType }, null);
		PageLoaderRestart();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/resource/pagedownloadcompdetail.xaml", UriKind.Relative);
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
			CardIntro = (MyCard)target;
			break;
		case 3:
			PanIntro = (StackPanel)target;
			break;
		case 4:
			BtnIntroWeb = (MyButton)target;
			break;
		case 5:
			BtnIntroWiki = (MyButton)target;
			break;
		case 6:
			BtnIntroCopy = (MyButton)target;
			break;
		case 7:
			PanMain = (StackPanel)target;
			break;
		case 8:
			HintAlternative = (MyHint)target;
			break;
		case 9:
			CardFilter = (MyCard)target;
			break;
		case 10:
			PanFilter = (StackPanel)target;
			break;
		case 11:
			PanResults = (StackPanel)target;
			break;
		case 12:
			PanLoad = (MyCard)target;
			break;
		case 13:
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

	[SpecialName]
	[CompilerGenerated]
	private void _Lambda_0024__16_002D5(MyRadioButton sender, bool raiseByMouse)
	{
		VersionFilter = ((Operators.CompareString(sender.Text, "全部", TextCompare: false) == 0) ? null : sender.Text);
		UpdateFilterResult();
	}
}
