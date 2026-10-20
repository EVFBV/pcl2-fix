using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;
using PCLCS;

namespace PCL;

[ContentProperty("SearchTags")]
[DesignerGenerated]
public class PageResource : MyScrollViewer, IComponentConnector
{
	private string _TypeName;

	private string _TypeNameSpaced;

	private ResourceTypes _Type;

	public static ModMinecraft.McInstance TargetInstance = null;

	public static string TargetName = null;

	public ModLoader.LoaderTask<ResourceSearcher.SearchRequest, int> Loader;

	public ResourceSearcher.SearchResult Storage;

	public const int PAGE_SIZE = 40;

	public int Page;

	[CompilerGenerated]
	[AccessedThroughProperty("TextSearchName")]
	private MyTextBox _TextSearchName;

	[CompilerGenerated]
	[AccessedThroughProperty("TextSearchVersion")]
	private MyComboBox _TextSearchVersion;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnSearchRun")]
	private MyButton _BtnSearchRun;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnSearchReset")]
	private MyButton _BtnSearchReset;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnSearchInstallModPack")]
	private MyButton _BtnSearchInstallModPack;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnPageFirst")]
	private MyIconButton _BtnPageFirst;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnPageLeft")]
	private MyIconButton _BtnPageLeft;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnPageRight")]
	private MyIconButton _BtnPageRight;

	[CompilerGenerated]
	[AccessedThroughProperty("Load")]
	private MyLoading _Load;

	private bool _contentLoaded;

	[SpecialName]
	private bool _0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded_0024Init;

	public ItemCollection SearchTags => ComboSearchTag.Items;

	public string TypeName
	{
		get
		{
			return _TypeName;
		}
		set
		{
			if (Operators.CompareString(_TypeName, value, TextCompare: false) != 0)
			{
				_TypeName = value;
				Loader.Name = $"社区资源获取：{value}";
			}
		}
	}

	public string TypeNameSpaced
	{
		get
		{
			return _TypeNameSpaced;
		}
		set
		{
			if (Operators.CompareString(_TypeNameSpaced, value, TextCompare: false) != 0)
			{
				_TypeNameSpaced = value;
				PanAlways.Title = $"搜索{value}";
				Load.Text = $"正在获取{value}列表";
			}
		}
	}

	public ResourceTypes PageType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _Type;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Invalid comparison between Unknown and I4
			if (_Type != value)
			{
				_Type = value;
				BtnSearchInstallModPack.Visibility = (((int)value != 2) ? Visibility.Collapsed : Visibility.Visible);
			}
		}
	}

	[field: AccessedThroughProperty("PanAlways")]
	internal virtual MyCard PanAlways
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyTextBox TextSearchName
	{
		[CompilerGenerated]
		get
		{
			return _TextSearchName;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = EnterTrigger;
			MyTextBox textSearchName = _TextSearchName;
			if (textSearchName != null)
			{
				textSearchName.KeyDown -= value2;
			}
			_TextSearchName = value;
			textSearchName = _TextSearchName;
			if (textSearchName != null)
			{
				textSearchName.KeyDown += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ComboSearchSource")]
	internal virtual MyComboBox ComboSearchSource
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyComboBox TextSearchVersion
	{
		[CompilerGenerated]
		get
		{
			return _TextSearchVersion;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = EnterTrigger;
			MyComboBox.TextChangedEventHandler obj = [SpecialName] (object a0, TextChangedEventArgs a1) =>
			{
				TextSearchVersion_TextChanged();
			};
			EventHandler value3 = [SpecialName] (object a0, EventArgs a1) =>
			{
				UpdateSearchLoaderVisibility();
			};
			MyComboBox textSearchVersion = _TextSearchVersion;
			if (textSearchVersion != null)
			{
				textSearchVersion.KeyDown -= value2;
				textSearchVersion.TextChanged -= obj;
				textSearchVersion.DropDownClosed -= value3;
			}
			_TextSearchVersion = value;
			textSearchVersion = _TextSearchVersion;
			if (textSearchVersion != null)
			{
				textSearchVersion.KeyDown += value2;
				textSearchVersion.TextChanged += obj;
				textSearchVersion.DropDownClosed += value3;
			}
		}
	}

	[field: AccessedThroughProperty("ComboSearchLoader")]
	internal virtual MyComboBox ComboSearchLoader
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ComboSearchTag")]
	internal virtual MyComboBox ComboSearchTag
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnSearchRun
	{
		[CompilerGenerated]
		get
		{
			return _BtnSearchRun;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				StartNewSearch();
			};
			MyButton btnSearchRun = _BtnSearchRun;
			if (btnSearchRun != null)
			{
				btnSearchRun.Click -= obj;
			}
			_BtnSearchRun = value;
			btnSearchRun = _BtnSearchRun;
			if (btnSearchRun != null)
			{
				btnSearchRun.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnSearchReset
	{
		[CompilerGenerated]
		get
		{
			return _BtnSearchReset;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				ResetFilter();
			};
			MyButton btnSearchReset = _BtnSearchReset;
			if (btnSearchReset != null)
			{
				btnSearchReset.Click -= obj;
			}
			_BtnSearchReset = value;
			btnSearchReset = _BtnSearchReset;
			if (btnSearchReset != null)
			{
				btnSearchReset.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnSearchInstallModPack
	{
		[CompilerGenerated]
		get
		{
			return _BtnSearchInstallModPack;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnSearchInstallModPack_Click;
			MyButton btnSearchInstallModPack = _BtnSearchInstallModPack;
			if (btnSearchInstallModPack != null)
			{
				btnSearchInstallModPack.Click -= obj;
			}
			_BtnSearchInstallModPack = value;
			btnSearchInstallModPack = _BtnSearchInstallModPack;
			if (btnSearchInstallModPack != null)
			{
				btnSearchInstallModPack.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanContent")]
	internal virtual StackPanel PanContent
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("HintError")]
	internal virtual MyHint HintError
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardProjects")]
	internal virtual MyCard CardProjects
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanProjects")]
	internal virtual StackPanel PanProjects
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("CardPages")]
	internal virtual MyCard CardPages
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnPageFirst
	{
		[CompilerGenerated]
		get
		{
			return _BtnPageFirst;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				BtnPageFirst_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton btnPageFirst = _BtnPageFirst;
			if (btnPageFirst != null)
			{
				btnPageFirst.Click -= obj;
			}
			_BtnPageFirst = value;
			btnPageFirst = _BtnPageFirst;
			if (btnPageFirst != null)
			{
				btnPageFirst.Click += obj;
			}
		}
	}

	internal virtual MyIconButton BtnPageLeft
	{
		[CompilerGenerated]
		get
		{
			return _BtnPageLeft;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				BtnPageLeft_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton btnPageLeft = _BtnPageLeft;
			if (btnPageLeft != null)
			{
				btnPageLeft.Click -= obj;
			}
			_BtnPageLeft = value;
			btnPageLeft = _BtnPageLeft;
			if (btnPageLeft != null)
			{
				btnPageLeft.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("LabPage")]
	internal virtual TextBlock LabPage
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnPageRight
	{
		[CompilerGenerated]
		get
		{
			return _BtnPageRight;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				BtnPageRight_Click(RuntimeHelpers.GetObjectValue(a0), (RoutedEventArgs)a1);
			};
			MyIconButton btnPageRight = _BtnPageRight;
			if (btnPageRight != null)
			{
				btnPageRight.Click -= obj;
			}
			_BtnPageRight = value;
			btnPageRight = _BtnPageRight;
			if (btnPageRight != null)
			{
				btnPageRight.Click += obj;
			}
		}
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

	public PageResource()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		base.Loaded += PageResource_Loaded;
		_TypeName = "";
		_TypeNameSpaced = "";
		_Type = (ResourceTypes)(-1);
		Loader = ResourceSearcher.GetLoader(LoaderInput);
		Storage = new ResourceSearcher.SearchResult();
		Page = 0;
		InitializeComponent();
	}

	private void PageResource_Loaded(object sender, EventArgs e)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		if (_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded_0024Init == null)
		{
			Interlocked.CompareExchange(ref _0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded_0024Init, new StaticLocalInitFlag(), null);
		}
		bool lockTaken = false;
		try
		{
			Monitor.Enter(_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded_0024Init, ref lockTaken);
			if (_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded_0024Init.State == 0)
			{
				_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded_0024Init.State = 2;
				_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded = false;
			}
			else if (_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded_0024Init.State == 2)
			{
				throw new IncompleteInitialization();
			}
		}
		finally
		{
			_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded_0024Init.State = 1;
			if (lockTaken)
			{
				Monitor.Exit(_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded_0024Init);
			}
		}
		if ((object)TargetInstance != null)
		{
			ResetFilter();
			TextSearchVersion.Text = TargetInstance.Version.VanillaName;
			VB_0024AnonymousDelegate_12<string, MyComboBoxItem> vB_0024AnonymousDelegate_ = [SpecialName] (string Name) =>
			{
				foreach (MyComboBoxItem item in (IEnumerable)ComboSearchLoader.Items)
				{
					if (Operators.ConditionalCompareObjectEqual(item.Content, Name, TextCompare: false))
					{
						return item;
					}
				}
				return (MyComboBoxItem)ComboSearchLoader.Items[0];
			};
			if (EnumUtils.HasFlagF<ModLoaders>(TargetInstance.Version.ModLoaders, (ModLoaders)1))
			{
				ComboSearchLoader.SelectedItem = vB_0024AnonymousDelegate_("Forge");
			}
			else if (EnumUtils.HasFlagF<ModLoaders>(TargetInstance.Version.ModLoaders, (ModLoaders)4))
			{
				ComboSearchLoader.SelectedItem = vB_0024AnonymousDelegate_("Fabric");
			}
			else if (EnumUtils.HasFlagF<ModLoaders>(TargetInstance.Version.ModLoaders, (ModLoaders)16))
			{
				ComboSearchLoader.SelectedItem = vB_0024AnonymousDelegate_("NeoForge");
			}
			TargetInstance = null;
			if (TargetName != null)
			{
				TextSearchName.Text = TargetName;
				TargetName = null;
			}
			if (_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded)
			{
				StartNewSearch();
			}
			ScrollToHome();
		}
		if (_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded)
		{
			return;
		}
		_0024STATIC_0024PageResource_Loaded_002420211C128119_0024IsFirstLoaded = true;
		((MyPageRight)base.Parent).PageLoaderInit(Load, PanLoad, PanContent, PanAlways, Loader, [SpecialName] (ModLoader.LoaderTask<ResourceSearcher.SearchRequest, int> a0) =>
		{
			Load_OnFinish();
		}, LoaderInput);
		if (ModDownload.AllDrops != null && ModDownload.AllDrops.First() > 250)
		{
			string text = McVersion.DropToVersion(ModDownload.AllDrops.First());
			if (Operators.CompareString(((MyComboBoxItem)TextSearchVersion.Items[1]).Content.ToString(), text, TextCompare: false) != 0)
			{
				TextSearchVersion.Items.Insert(1, new MyComboBoxItem
				{
					Content = text
				});
			}
		}
	}

	private ResourceSearcher.SearchRequest LoaderInput()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Invalid comparison between Unknown and I4
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		checked
		{
			ResourceSearcher.SearchRequest searchRequest = new ResourceSearcher.SearchRequest(PageType, Storage, (Page + 1) * 40);
			string gameVersion = ((Operators.CompareString(TextSearchVersion.Text, "全部 (也可自行输入)", TextCompare: false) == 0) ? null : ((TextSearchVersion.Text.Contains(".") || TextSearchVersion.Text.Contains("w")) ? TextSearchVersion.Text : null));
			searchRequest.SearchText = TextSearchName.Text;
			searchRequest.GameVersion = gameVersion;
			searchRequest.Tag = Conversions.ToString(NewLateBinding.LateGet(ComboSearchTag.SelectedItem, null, "Tag", new object[0], null, null, null));
			searchRequest.ModLoaders = (ModLoaders)(int)Math.Round((unchecked((int)PageType) == 1) ? ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(ComboSearchLoader.SelectedItem, null, "Tag", new object[0], null, null, null))) : 0.0);
			searchRequest.Sources = (ResourcePlatforms)(int)Math.Round(ModBase.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(ComboSearchSource.SelectedItem, null, "Tag", new object[0], null, null, null))));
			_ = null;
			return searchRequest;
		}
	}

	private void Load_OnFinish()
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Invalid comparison between Unknown and I4
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Invalid comparison between Unknown and I4
		checked
		{
			try
			{
				Logger.Info($"开始可视化{TypeNameSpaced}列表，已储藏 {Storage.Results.Count} 个结果，当前在第 {Page + 1} 页", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageResource.xaml.vb");
				PanProjects.Children.Clear();
				int num = Math.Min(Page * 40, Storage.Results.Count - 1);
				foreach (ResourceProject item in Storage.Results.GetRange(num, Math.Min(Storage.Results.Count - num, 40)))
				{
					PanProjects.Children.Add(item.ToResourceItem(Loader.Input.GameVersion == null, unchecked((int)Loader.Input.ModLoaders == 0 && ((int)PageType == 1 || (int)PageType == 2))));
				}
				CardPages.Visibility = ((Storage.Results.Count <= 40 && Storage.CurseForgeOffset >= Storage.CurseForgeTotal && Storage.ModrinthOffset >= Storage.ModrinthTotal) ? Visibility.Collapsed : Visibility.Visible);
				LabPage.Text = Conversions.ToString(Page + 1);
				BtnPageFirst.IsEnabled = Page > 1;
				BtnPageFirst.Opacity = ((Page > 1) ? 1.0 : 0.2);
				BtnPageLeft.IsEnabled = Page > 0;
				BtnPageLeft.Opacity = ((Page > 0) ? 1.0 : 0.2);
				bool flag = Storage.Results.Count > 40 * (Page + 1) || Storage.CurseForgeOffset < Storage.CurseForgeTotal || Storage.ModrinthOffset < Storage.ModrinthTotal;
				BtnPageRight.IsEnabled = flag;
				BtnPageRight.Opacity = (flag ? 1.0 : 0.2);
				if (Storage.ErrorMessage == null)
				{
					HintError.Visibility = Visibility.Collapsed;
				}
				else
				{
					HintError.Visibility = Visibility.Visible;
					HintError.Text = Storage.ErrorMessage;
				}
				ScrollToTop();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"可视化{TypeNameSpaced}列表出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageResource.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
	}

	private void Load_State(object sender, MyLoading.MyLoadingState state, MyLoading.MyLoadingState oldState)
	{
		ModBase.LoadState state2 = Loader.State;
		if (state2 == ModBase.LoadState.Failed)
		{
			string text = "";
			if (Loader.Error != null)
			{
				text = Loader.Error.Message;
			}
			if (text.Contains("不是有效的 json 文件"))
			{
				Logger.Warn($"下载的{TypeNameSpaced}列表 json 文件损坏，已自动重试", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageResource.xaml.vb");
				((MyPageRight)base.Parent).PageLoaderRestart();
			}
		}
	}

	private void BtnPageFirst_Click(object sender, RoutedEventArgs e)
	{
		ChangePage(0);
	}

	private void BtnPageLeft_Click(object sender, RoutedEventArgs e)
	{
		ChangePage(checked(Page - 1));
	}

	private void BtnPageRight_Click(object sender, RoutedEventArgs e)
	{
		ChangePage(checked(Page + 1));
	}

	private void ChangePage(int NewPage)
	{
		CardPages.IsEnabled = false;
		Page = NewPage;
		ModMain.FrmMain.BackToTop();
		Logger.Info($"{TypeName}：切换到第 {checked(Page + 1)} 页", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\PageResource.xaml.vb");
		ModBase.RunInThread([SpecialName] () =>
		{
			Thread.Sleep(100);
			ModBase.RunInUi([SpecialName] () =>
			{
				CardPages.IsEnabled = true;
			});
			Loader.Start();
		});
	}

	private void StartNewSearch()
	{
		Page = 0;
		ModLoader.LoaderTask<ResourceSearcher.SearchRequest, int> loader = Loader;
		ResourceSearcher.SearchRequest Input = LoaderInput();
		if (loader.ShouldStart(ref Input))
		{
			Storage = new ResourceSearcher.SearchResult();
		}
		Loader.Start();
	}

	private void EnterTrigger(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return)
		{
			StartNewSearch();
		}
	}

	private void ResetFilter()
	{
		TextSearchName.Text = "";
		TextSearchVersion.Text = "全部 (也可自行输入)";
		TextSearchVersion.SelectedIndex = 0;
		ComboSearchSource.SelectedIndex = 0;
		ComboSearchTag.SelectedIndex = 0;
		ComboSearchLoader.SelectedIndex = 0;
		Loader.LastFinishedTime = 0L;
	}

	private void TextSearchVersion_TextChanged()
	{
		if (!TextSearchVersion.IsDropDownOpen)
		{
			UpdateSearchLoaderVisibility();
		}
	}

	private void UpdateSearchLoaderVisibility()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		if ((int)PageType == 1 && (TextSearchVersion.Text.Contains(".") || TextSearchVersion.Text.Contains("w")))
		{
			ComboSearchLoader.Visibility = Visibility.Visible;
			Grid.SetColumnSpan(TextSearchVersion, 1);
		}
		else
		{
			ComboSearchLoader.Visibility = Visibility.Collapsed;
			Grid.SetColumnSpan(TextSearchVersion, 2);
			ComboSearchLoader.SelectedIndex = 0;
		}
	}

	private void BtnSearchInstallModPack_Click(object sender, EventArgs e)
	{
		ModModpack.ModpackInstall();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/resource/pageresource.xaml", UriKind.Relative);
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
			PanAlways = (MyCard)target;
			break;
		case 2:
			TextSearchName = (MyTextBox)target;
			break;
		case 3:
			ComboSearchSource = (MyComboBox)target;
			break;
		case 4:
			TextSearchVersion = (MyComboBox)target;
			break;
		case 5:
			ComboSearchLoader = (MyComboBox)target;
			break;
		case 6:
			ComboSearchTag = (MyComboBox)target;
			break;
		case 7:
			BtnSearchRun = (MyButton)target;
			break;
		case 8:
			BtnSearchReset = (MyButton)target;
			break;
		case 9:
			BtnSearchInstallModPack = (MyButton)target;
			break;
		case 10:
			PanContent = (StackPanel)target;
			break;
		case 11:
			HintError = (MyHint)target;
			break;
		case 12:
			CardProjects = (MyCard)target;
			break;
		case 13:
			PanProjects = (StackPanel)target;
			break;
		case 14:
			CardPages = (MyCard)target;
			break;
		case 15:
			BtnPageFirst = (MyIconButton)target;
			break;
		case 16:
			BtnPageLeft = (MyIconButton)target;
			break;
		case 17:
			LabPage = (TextBlock)target;
			break;
		case 18:
			BtnPageRight = (MyIconButton)target;
			break;
		case 19:
			PanLoad = (MyCard)target;
			break;
		case 20:
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
