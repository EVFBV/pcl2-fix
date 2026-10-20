using System;
using System.CodeDom.Compiler;
using System.Collections;
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
using MeloongCore;
using MeloongCore.Extensions;
using MeloongCore.Wpf;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using PCL.My;

namespace PCL;

[DesignerGenerated]
public class PageInstanceOverall : MyPageRight, IComponentConnector
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__16_002D0
	{
		public ModLoader.LoaderCombo<string> _0024VB_0024Local_Loader;

		public _Closure_0024__16_002D0(_Closure_0024__16_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Loader = arg0._0024VB_0024Local_Loader;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R1(ModLoader.LoaderBase a0)
		{
			_Lambda_0024__0();
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			switch (_0024VB_0024Local_Loader.State)
			{
			case ModBase.LoadState.Finished:
				ModMain.Hint(_0024VB_0024Local_Loader.Name + "成功！", ModMain.HintType.Green);
				break;
			case ModBase.LoadState.Failed:
				ModMain.Hint(_0024VB_0024Local_Loader.Name + "失败：" + ExceptionExtensions.GetDisplay(_0024VB_0024Local_Loader.Error, false), ModMain.HintType.Red);
				break;
			case ModBase.LoadState.Canceled:
				ModMain.Hint(_0024VB_0024Local_Loader.Name + "已取消！");
				break;
			}
		}
	}

	private bool IsLoad;

	public MyListItem InstanceHeaderItem;

	[CompilerGenerated]
	[AccessedThroughProperty("ComboDisplayLogo")]
	private MyComboBox _ComboDisplayLogo;

	[CompilerGenerated]
	[AccessedThroughProperty("ItemDisplayLogoCustom")]
	private MyComboBoxItem _ItemDisplayLogoCustom;

	[CompilerGenerated]
	[AccessedThroughProperty("ComboDisplayType")]
	private MyComboBox _ComboDisplayType;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnDisplayRename")]
	private MyButton _BtnDisplayRename;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnDisplayDesc")]
	private MyButton _BtnDisplayDesc;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnDisplayStar")]
	private MyButton _BtnDisplayStar;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnFolderVersion")]
	private MyButton _BtnFolderVersion;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnFolderSaves")]
	private MyButton _BtnFolderSaves;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnFolderMods")]
	private MyButton _BtnFolderMods;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnFolderScreen")]
	private MyButton _BtnFolderScreen;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnManageScript")]
	private MyButton _BtnManageScript;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnManageCheck")]
	private MyButton _BtnManageCheck;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnManageDelete")]
	private MyButton _BtnManageDelete;

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

	[field: AccessedThroughProperty("PanDisplayItem")]
	internal virtual Grid PanDisplayItem
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanDisplay")]
	internal virtual MyCard PanDisplay
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanDisplayIcon")]
	internal virtual Grid PanDisplayIcon
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyComboBox ComboDisplayLogo
	{
		[CompilerGenerated]
		get
		{
			return _ComboDisplayLogo;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SelectionChangedEventHandler value2 = [SpecialName] (object a0, SelectionChangedEventArgs a1) =>
			{
				ComboDisplayLogo_SelectionChanged();
			};
			MyComboBox comboDisplayLogo = _ComboDisplayLogo;
			if (comboDisplayLogo != null)
			{
				comboDisplayLogo.SelectionChanged -= value2;
			}
			_ComboDisplayLogo = value;
			comboDisplayLogo = _ComboDisplayLogo;
			if (comboDisplayLogo != null)
			{
				comboDisplayLogo.SelectionChanged += value2;
			}
		}
	}

	internal virtual MyComboBoxItem ItemDisplayLogoCustom
	{
		[CompilerGenerated]
		get
		{
			return _ItemDisplayLogoCustom;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				ComboDisplayLogo_SelectionChanged();
			};
			MyComboBoxItem itemDisplayLogoCustom = _ItemDisplayLogoCustom;
			if (itemDisplayLogoCustom != null)
			{
				itemDisplayLogoCustom.PreviewMouseLeftButtonUp -= value2;
			}
			_ItemDisplayLogoCustom = value;
			itemDisplayLogoCustom = _ItemDisplayLogoCustom;
			if (itemDisplayLogoCustom != null)
			{
				itemDisplayLogoCustom.PreviewMouseLeftButtonUp += value2;
			}
		}
	}

	internal virtual MyComboBox ComboDisplayType
	{
		[CompilerGenerated]
		get
		{
			return _ComboDisplayType;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			SelectionChangedEventHandler value2 = ComboDisplayType_SelectionChanged;
			MyComboBox comboDisplayType = _ComboDisplayType;
			if (comboDisplayType != null)
			{
				comboDisplayType.SelectionChanged -= value2;
			}
			_ComboDisplayType = value;
			comboDisplayType = _ComboDisplayType;
			if (comboDisplayType != null)
			{
				comboDisplayType.SelectionChanged += value2;
			}
		}
	}

	internal virtual MyButton BtnDisplayRename
	{
		[CompilerGenerated]
		get
		{
			return _BtnDisplayRename;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnDisplayRename_Click;
			MyButton btnDisplayRename = _BtnDisplayRename;
			if (btnDisplayRename != null)
			{
				btnDisplayRename.Click -= obj;
			}
			_BtnDisplayRename = value;
			btnDisplayRename = _BtnDisplayRename;
			if (btnDisplayRename != null)
			{
				btnDisplayRename.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnDisplayDesc
	{
		[CompilerGenerated]
		get
		{
			return _BtnDisplayDesc;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnDisplayDesc_Click;
			MyButton btnDisplayDesc = _BtnDisplayDesc;
			if (btnDisplayDesc != null)
			{
				btnDisplayDesc.Click -= obj;
			}
			_BtnDisplayDesc = value;
			btnDisplayDesc = _BtnDisplayDesc;
			if (btnDisplayDesc != null)
			{
				btnDisplayDesc.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnDisplayStar
	{
		[CompilerGenerated]
		get
		{
			return _BtnDisplayStar;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnDisplayStar_Click;
			MyButton btnDisplayStar = _BtnDisplayStar;
			if (btnDisplayStar != null)
			{
				btnDisplayStar.Click -= obj;
			}
			_BtnDisplayStar = value;
			btnDisplayStar = _BtnDisplayStar;
			if (btnDisplayStar != null)
			{
				btnDisplayStar.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanFolder")]
	internal virtual MyCard PanFolder
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnFolderVersion
	{
		[CompilerGenerated]
		get
		{
			return _BtnFolderVersion;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				BtnFolderVersion_Click();
			};
			MyButton btnFolderVersion = _BtnFolderVersion;
			if (btnFolderVersion != null)
			{
				btnFolderVersion.Click -= obj;
			}
			_BtnFolderVersion = value;
			btnFolderVersion = _BtnFolderVersion;
			if (btnFolderVersion != null)
			{
				btnFolderVersion.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnFolderSaves
	{
		[CompilerGenerated]
		get
		{
			return _BtnFolderSaves;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				BtnFolderSaves_Click();
			};
			MyButton btnFolderSaves = _BtnFolderSaves;
			if (btnFolderSaves != null)
			{
				btnFolderSaves.Click -= obj;
			}
			_BtnFolderSaves = value;
			btnFolderSaves = _BtnFolderSaves;
			if (btnFolderSaves != null)
			{
				btnFolderSaves.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnFolderMods
	{
		[CompilerGenerated]
		get
		{
			return _BtnFolderMods;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				BtnFolderMods_Click();
			};
			MyButton btnFolderMods = _BtnFolderMods;
			if (btnFolderMods != null)
			{
				btnFolderMods.Click -= obj;
			}
			_BtnFolderMods = value;
			btnFolderMods = _BtnFolderMods;
			if (btnFolderMods != null)
			{
				btnFolderMods.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnFolderScreen
	{
		[CompilerGenerated]
		get
		{
			return _BtnFolderScreen;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnFolderScreen_Click;
			MyButton btnFolderScreen = _BtnFolderScreen;
			if (btnFolderScreen != null)
			{
				btnFolderScreen.Click -= obj;
			}
			_BtnFolderScreen = value;
			btnFolderScreen = _BtnFolderScreen;
			if (btnFolderScreen != null)
			{
				btnFolderScreen.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("PanManage")]
	internal virtual MyCard PanManage
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnManageScript
	{
		[CompilerGenerated]
		get
		{
			return _BtnManageScript;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				BtnManageScript_Click();
			};
			MyButton btnManageScript = _BtnManageScript;
			if (btnManageScript != null)
			{
				btnManageScript.Click -= obj;
			}
			_BtnManageScript = value;
			btnManageScript = _BtnManageScript;
			if (btnManageScript != null)
			{
				btnManageScript.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnManageCheck
	{
		[CompilerGenerated]
		get
		{
			return _BtnManageCheck;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnManageCheck_Click;
			MyButton btnManageCheck = _BtnManageCheck;
			if (btnManageCheck != null)
			{
				btnManageCheck.Click -= obj;
			}
			_BtnManageCheck = value;
			btnManageCheck = _BtnManageCheck;
			if (btnManageCheck != null)
			{
				btnManageCheck.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnManageDelete
	{
		[CompilerGenerated]
		get
		{
			return _BtnManageDelete;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = BtnManageDelete_Click;
			MyButton btnManageDelete = _BtnManageDelete;
			if (btnManageDelete != null)
			{
				btnManageDelete.Click -= obj;
			}
			_BtnManageDelete = value;
			btnManageDelete = _BtnManageDelete;
			if (btnManageDelete != null)
			{
				btnManageDelete.Click += obj;
			}
		}
	}

	public PageInstanceOverall()
	{
		base.Loaded += PageSetupLaunch_Loaded;
		IsLoad = false;
		InitializeComponent();
	}

	private void PageSetupLaunch_Loaded(object sender, RoutedEventArgs e)
	{
		PanBack.ScrollToHome();
		Reload();
		if (!IsLoad)
		{
			IsLoad = true;
			PanDisplay.TriggerForceResize();
		}
	}

	private void Reload()
	{
		checked
		{
			ModAnimation.AniControlEnabled++;
			ComboDisplayType.SelectedIndex = Conversions.ToInteger(ModBase.ReadIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
			BtnDisplayStar.Text = (PageInstanceLeft.Instance.IsStar ? "从收藏夹中移除" : "加入收藏夹");
			BtnFolderMods.Visibility = ((!PageInstanceLeft.Instance.Modable) ? Visibility.Collapsed : Visibility.Visible);
			PanDisplayItem.Children.Clear();
			InstanceHeaderItem = PageInstanceLeft.Instance.ToListItem().Init();
			InstanceHeaderItem.IsHitTestVisible = false;
			PanDisplayItem.Children.Add(InstanceHeaderItem);
			ModMain.FrmMain.PageNameRefresh();
			ComboDisplayLogo.SelectedIndex = 0;
			string text = ModBase.ReadIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "Logo");
			if (Conversions.ToBoolean(ModBase.ReadIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "LogoCustom", "False")))
			{
				foreach (MyComboBoxItem item in (IEnumerable)ComboDisplayLogo.Items)
				{
					if (Operators.ConditionalCompareObjectEqual(item.Tag, text, TextCompare: false) || (Operators.ConditionalCompareObjectEqual(item.Tag, "PCL\\Logo.png", TextCompare: false) && StringExtensions.EndsWithF(text, "PCL\\Logo.png", false)))
					{
						ComboDisplayLogo.SelectedItem = item;
						break;
					}
				}
			}
			ModAnimation.AniControlEnabled--;
		}
	}

	private void ComboDisplayType_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (!IsLoad || ModAnimation.AniControlEnabled != 0)
		{
			return;
		}
		if (ComboDisplayType.SelectedIndex != 1)
		{
			try
			{
				ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(ComboDisplayType.SelectedIndex));
				PageInstanceLeft.Instance.DisplayType = (ModMinecraft.McInstanceCardType)Conversions.ToInteger(ModBase.ReadIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(0)));
				ModMain.FrmInstanceLeft.RefreshModDisabled();
				ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"修改版本分类失败（{PageInstanceLeft.Instance.Name}）", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
				ProjectData.ClearProjectError();
			}
			Reload();
			return;
		}
		try
		{
			if (!Settings.Get<bool>("HintHide"))
			{
				if (ModMain.MyMsgBox("确认要从版本列表中隐藏该版本吗？隐藏该版本后，它将不再出现于 PCL 显示的版本列表中。\r\n此后，在版本列表页面按下 F11 才可以查看被隐藏的版本。", "隐藏版本提示", "确定", "取消") != 1)
				{
					ComboDisplayType.SelectedIndex = 0;
					return;
				}
				Settings.Set("HintHide", true);
			}
			ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "DisplayType", Conversions.ToString(1));
			ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
			ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Error(ex4, $"隐藏版本 {PageInstanceLeft.Instance.Name} 失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void BtnDisplayDesc_Click(object sender, EventArgs e)
	{
		try
		{
			string text = ModBase.ReadIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "CustomInfo");
			string text2 = ModMain.MyMsgBoxInput("更改描述", "修改版本的描述文本，留空则使用 PCL 的默认描述。", text, new Collection<Validate>(), "默认描述");
			if (text2 != null && Operators.CompareString(text, text2, TextCompare: false) != 0)
			{
				ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "CustomInfo", text2);
			}
			PageInstanceLeft.Instance = new ModMinecraft.McInstance(PageInstanceLeft.Instance.Name).Load();
			Reload();
			ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, $"版本 {PageInstanceLeft.Instance.Name} 描述更改失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void BtnDisplayRename_Click(object sender, EventArgs e)
	{
		try
		{
			if (ModMinecraft.McInstanceLock.IsLocked(PageInstanceLeft.Instance))
			{
				ModMain.Hint("该版本正在被修改，请稍后再重命名！", ModMain.HintType.Red);
				return;
			}
			string name = PageInstanceLeft.Instance.Name;
			string text = ModMain.MyMsgBoxInput("重命名版本", "", name, new Collection<Validate>
			{
				new ValidateFolderName(ModMinecraft.McFolderSelected + "versions", UseMinecraftCharCheck: true, new List<string> { name })
			});
			if (!string.IsNullOrWhiteSpace(text))
			{
				ModMinecraft.McInstance instance = PageInstanceLeft.Instance.Rename(text);
				ModMain.Hint("重命名成功！", ModMain.HintType.Green);
				PageInstanceLeft.Instance = instance;
				if (!Information.IsNothing(ModMinecraft.McInstanceSelected) && ModMinecraft.McInstanceSelected.Equals(PageInstanceLeft.Instance))
				{
					ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "Version", text);
				}
				Reload();
				ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "重命名版本失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void ComboDisplayLogo_SelectionChanged()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		if (!IsLoad || ModAnimation.AniControlEnabled != 0)
		{
			return;
		}
		try
		{
			if (ComboDisplayLogo.SelectedItem == ItemDisplayLogoCustom)
			{
				string text = Dialogs.SelectFile("选择图片", false, (string)null, (OneOf<IEnumerable<ValueTuple<string[], string>>, string>?)OneOf<IEnumerable<(string[], string)>, string>.op_Implicit((IEnumerable<(string[], string)>)new(string[], string)[1] { (new string[5] { "png", "jpeg", "jpg", "gif", "webp" }, "常用图片文件") })).FirstOrDefault();
				if (string.IsNullOrEmpty(text))
				{
					Reload();
					return;
				}
				FileUtils.Copy(text, PageInstanceLeft.Instance.PathVersion + "PCL\\Logo.png");
			}
			else
			{
				FileUtils.Delete(PageInstanceLeft.Instance.PathVersion + "PCL\\Logo.png", false);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, $"更改自定义版本图标失败（{PageInstanceLeft.Instance.Name}）", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
			ProjectData.ClearProjectError();
		}
		try
		{
			string text2 = Conversions.ToString(NewLateBinding.LateGet(ComboDisplayLogo.SelectedItem, null, "Tag", new object[0], null, null, null));
			ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "Logo", text2);
			ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "LogoCustom", Conversions.ToString(Operators.CompareString(text2, "", TextCompare: false) != 0));
			ModBase.WriteIni(ModMinecraft.McFolderSelected + "PCL.ini", "InstanceCache", "");
			PageInstanceLeft.Instance = new ModMinecraft.McInstance(PageInstanceLeft.Instance.Name).Load();
			Reload();
			ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Error(ex4, $"更改版本图标失败（{PageInstanceLeft.Instance.Name}）", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void BtnDisplayStar_Click(object sender, EventArgs e)
	{
		try
		{
			ModBase.WriteIni(PageInstanceLeft.Instance.PathVersion + "PCL\\Setup.ini", "IsStar", Conversions.ToString(!PageInstanceLeft.Instance.IsStar));
			PageInstanceLeft.Instance = new ModMinecraft.McInstance(PageInstanceLeft.Instance.Name).Load();
			Reload();
			ModMinecraft.McInstanceListForceRefresh = true;
			ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, $"版本 {PageInstanceLeft.Instance.Name} 收藏状态更改失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void BtnFolderVersion_Click()
	{
		OpenInstanceFolder(PageInstanceLeft.Instance);
	}

	public static void OpenInstanceFolder(ModMinecraft.McInstance Instance)
	{
		ModBase.OpenExplorer(Instance.PathVersion);
	}

	private void BtnFolderSaves_Click()
	{
		string text = PageInstanceLeft.Instance.PathIndie + "saves\\";
		DirectoryUtils.Create(text);
		ModBase.OpenExplorer(text);
	}

	private void BtnFolderMods_Click()
	{
		string text = PageInstanceLeft.Instance.PathIndie + "mods\\";
		DirectoryUtils.Create(text);
		ModBase.OpenExplorer(text);
	}

	private void BtnFolderScreen_Click(object sender, MouseButtonEventArgs e)
	{
		string text = PageInstanceLeft.Instance.PathIndie + "screenshots\\";
		DirectoryUtils.Create(text);
		ModBase.OpenExplorer(text);
	}

	private void BtnManageScript_Click()
	{
		try
		{
			string text = Dialogs.SaveFile("选择脚本保存位置", "启动 " + PageInstanceLeft.Instance.Name + ".bat", (string)null, (IEnumerable<ValueTuple<string, string>>)new(string, string)[1] { ("bat", "批处理文件") });
			if (text == null)
			{
				return;
			}
			if (ModLaunch.McLaunchLoader.State == ModBase.LoadState.Loading)
			{
				ModMain.Hint("请在当前启动任务结束后再试！", ModMain.HintType.Red);
			}
			else if (ModLaunch.McLaunchStart(new ModLaunch.McLaunchOptions
			{
				SaveBatch = text,
				Instance = PageInstanceLeft.Instance
			}))
			{
				if (Settings.Get<ModLaunch.McLoginType>("LoginType") == ModLaunch.McLoginType.Legacy)
				{
					ModMain.Hint("正在导出启动脚本……");
				}
				else
				{
					ModMain.Hint("正在导出启动脚本……（注意，使用脚本启动可能会导致登录失效！）");
				}
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, $"导出启动脚本失败（{PageInstanceLeft.Instance.Name}）", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void BtnManageCheck_Click(object sender, EventArgs e)
	{
		try
		{
			_Closure_0024__16_002D0 arg = default(_Closure_0024__16_002D0);
			_Closure_0024__16_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__16_002D0(arg);
			if (ModMinecraft.McInstanceLock.IsLocked(PageInstanceLeft.Instance))
			{
				ModMain.Hint("该版本正在被修改，请稍后再补全文件！", ModMain.HintType.Red);
				return;
			}
			if (Conversions.ToBoolean(ModMinecraft.ShouldIgnoreFileCheck(PageInstanceLeft.Instance)))
			{
				ModMain.Hint("请先关闭 [版本设置 → 设置 → 高级启动选项 → 关闭文件校验]，然后再尝试补全文件！");
				return;
			}
			foreach (ModLoader.LoaderBase item in ModLoader.LoaderTaskbar)
			{
				if (Operators.CompareString(item.Name, PageInstanceLeft.Instance.Name + " 文件补全", TextCompare: false) == 0)
				{
					ModMain.Hint("正在处理中，请稍候！", ModMain.HintType.Red);
					return;
				}
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader = new ModLoader.LoaderCombo<string>(PageInstanceLeft.Instance.Name + " 文件补全", ModDownload.DlClientFix(PageInstanceLeft.Instance, CheckAssetsHash: true, DownloadAssetIndexInBackground: false));
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.OnStateChanged = [SpecialName] (ModLoader.LoaderBase a0) =>
			{
				CS_0024_003C_003E8__locals0._Lambda_0024__0();
			};
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader.Start(PageInstanceLeft.Instance.Name);
			ModLoader.LoaderTaskbarAdd(CS_0024_003C_003E8__locals0._0024VB_0024Local_Loader);
			ModMain.FrmMain.BtnExtraDownload.ShowRefresh();
			ModMain.FrmMain.BtnExtraDownload.Ribble();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, $"尝试补全文件失败（{PageInstanceLeft.Instance.Name}）", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void BtnManageDelete_Click(object sender, EventArgs e)
	{
		try
		{
			if (ModMinecraft.McInstanceLock.IsLocked(PageInstanceLeft.Instance))
			{
				ModMain.Hint("该版本正在被修改，请稍后再删除！", ModMain.HintType.Red);
				return;
			}
			bool shiftKeyDown = MyWpfExtension.Computer.Keyboard.ShiftKeyDown;
			bool flag = PageInstanceLeft.Instance.State != 0 && Operators.CompareString(PageInstanceLeft.Instance.PathIndie, ModMinecraft.McFolderSelected, TextCompare: false) != 0;
			DirectoryInfo info = DirectoryUtils.GetInfo(PageInstanceLeft.Instance.PathIndie + "saves\\");
			List<string> list = ((flag && info.Exists) ? (from Save in info.EnumerateDirectories()
				orderby Save.LastWriteTime descending
				select $"{Save.Name}（上次修改：{StringUtils.FormatTimeSpan(Save.LastWriteTime - DateTime.Now, false)}）").ToList() : new List<string>());
			switch (ModMain.MyMsgBox(string.Format("你确定要{0}删除版本 {1} 吗？", shiftKeyDown ? "永久" : "", PageInstanceLeft.Instance.Name) + (flag ? ("\r\n该版本对应的存档、资源包、Mod 等文件也将被一并删除！" + (list.Any() ? ("\r\n\r\n这会删除以下存档：\r\n· " + EnumerableExtensions.Join((IEnumerable)list, "\r\n· ")) : "")) : ""), "版本删除确认", "确定", "取消", "", flag || shiftKeyDown))
			{
			case 1:
				PageInstanceLeft.Instance.ResetSettingsCache();
				DirectoryUtils.Delete(PageInstanceLeft.Instance.PathVersion, !shiftKeyDown);
				ModMain.Hint("已删除版本 " + PageInstanceLeft.Instance.Name + "！", ModMain.HintType.Green);
				break;
			case 2:
				return;
			}
			ModLoader.LoaderFolderRun(ModMinecraft.McInstanceListLoader, ModMinecraft.McFolderSelected, ModLoader.LoaderFolderRunType.ForceRun, 1, "versions\\");
			ModMain.FrmMain.PageBack();
		}
		catch (OperationCanceledException ex)
		{
			ProjectData.SetProjectError(ex);
			OperationCanceledException ex2 = ex;
			Logger.Warn((Exception)ex2, $"删除版本 {PageInstanceLeft.Instance.Name} 被主动取消", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
			ProjectData.ClearProjectError();
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Error(ex4, $"删除版本 {PageInstanceLeft.Instance.Name} 失败", (LogBehavior)3, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageInstance\\PageInstanceOverall.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pageinstance/pageinstanceoverall.xaml", UriKind.Relative);
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
			PanDisplayItem = (Grid)target;
			break;
		case 4:
			PanDisplay = (MyCard)target;
			break;
		case 5:
			PanDisplayIcon = (Grid)target;
			break;
		case 6:
			ComboDisplayLogo = (MyComboBox)target;
			break;
		case 7:
			ItemDisplayLogoCustom = (MyComboBoxItem)target;
			break;
		case 8:
			ComboDisplayType = (MyComboBox)target;
			break;
		case 9:
			BtnDisplayRename = (MyButton)target;
			break;
		case 10:
			BtnDisplayDesc = (MyButton)target;
			break;
		case 11:
			BtnDisplayStar = (MyButton)target;
			break;
		case 12:
			PanFolder = (MyCard)target;
			break;
		case 13:
			BtnFolderVersion = (MyButton)target;
			break;
		case 14:
			BtnFolderSaves = (MyButton)target;
			break;
		case 15:
			BtnFolderMods = (MyButton)target;
			break;
		case 16:
			BtnFolderScreen = (MyButton)target;
			break;
		case 17:
			PanManage = (MyCard)target;
			break;
		case 18:
			BtnManageScript = (MyButton)target;
			break;
		case 19:
			BtnManageCheck = (MyButton)target;
			break;
		case 20:
			BtnManageDelete = (MyButton)target;
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
