using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using MeloongCore;
using Microsoft.VisualBasic.CompilerServices;
using PCLCS;

namespace PCL;

[DesignerGenerated]
public class MyResourceItem : Grid, IComponentConnector
{
	public delegate void ClickEventHandler(object sender, MouseButtonEventArgs e);

	public int Uuid;

	private bool IsMouseDown;

	private Border _RectBack;

	private string StateLast;

	[CompilerGenerated]
	[AccessedThroughProperty("LabInfo")]
	private TextBlock _LabInfo;

	private bool _contentLoaded;

	public string Title
	{
		get
		{
			return LabTitle.Text;
		}
		set
		{
			if (Operators.CompareString(LabTitle.Text, value, TextCompare: false) != 0)
			{
				LabTitle.Text = value;
			}
		}
	}

	public string SubTitle
	{
		get
		{
			return LabTitleRaw?.Text ?? "";
		}
		set
		{
			if (Operators.CompareString(LabTitleRaw.Text, value, TextCompare: false) != 0)
			{
				LabTitleRaw.Text = value;
				LabTitleRaw.Visibility = ((Operators.CompareString(value, "", TextCompare: false) == 0) ? Visibility.Collapsed : Visibility.Visible);
			}
		}
	}

	public string Description
	{
		get
		{
			return LabInfo.Text;
		}
		set
		{
			if (Operators.CompareString(LabInfo.Text, value, TextCompare: false) != 0)
			{
				LabInfo.Text = value;
			}
		}
	}

	public List<string> Tags
	{
		set
		{
			PanTags.Children.Clear();
			PanTags.Visibility = ((!value.Any()) ? Visibility.Collapsed : Visibility.Visible);
			foreach (string item in value)
			{
				Border border = new Border
				{
					Background = new ModBase.MyColor("#11000000"),
					Padding = new Thickness(3.0, 1.0, 3.0, 1.0),
					CornerRadius = new CornerRadius(3.0),
					Margin = new Thickness(0.0, 0.0, 3.0, 0.0),
					SnapsToDevicePixels = true,
					UseLayoutRounding = false
				};
				TextBlock child = new TextBlock
				{
					Text = item,
					Foreground = new ModBase.MyColor("#868686"),
					FontSize = 11.0
				};
				border.Child = child;
				PanTags.Children.Add(border);
			}
		}
	}

	public Border RectBack
	{
		get
		{
			if (_RectBack == null)
			{
				Border border = new Border
				{
					Name = "RectBack",
					CornerRadius = new CornerRadius(3.0),
					RenderTransform = new ScaleTransform(0.8, 0.8),
					RenderTransformOrigin = new Point(0.5, 0.5),
					BorderThickness = new Thickness(ModBase.GetWPFSize(1.0)),
					SnapsToDevicePixels = true,
					IsHitTestVisible = false,
					Opacity = 0.0
				};
				border.SetResourceReference(Border.BackgroundProperty, "ColorBrush7");
				border.SetResourceReference(Border.BorderBrushProperty, "ColorBrush6");
				Grid.SetColumnSpan(border, 999);
				Grid.SetRowSpan(border, 999);
				base.Children.Insert(0, border);
				_RectBack = border;
			}
			return _RectBack;
		}
	}

	public bool CanInteraction { get; set; }

	[field: AccessedThroughProperty("PanBack")]
	internal virtual MyResourceItem PanBack
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PathLogo")]
	internal virtual MyImage PathLogo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabTitle")]
	internal virtual TextBlock LabTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabTitleRaw")]
	internal virtual TextBlock LabTitleRaw
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanTags")]
	internal virtual StackPanel PanTags
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual TextBlock LabInfo
	{
		[CompilerGenerated]
		get
		{
			return _LabInfo;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = LabInfo_MouseEnter;
			TextBlock labInfo = _LabInfo;
			if (labInfo != null)
			{
				labInfo.MouseEnter -= value2;
			}
			_LabInfo = value;
			labInfo = _LabInfo;
			if (labInfo != null)
			{
				labInfo.MouseEnter += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolTipInfo")]
	internal virtual ToolTip ToolTipInfo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnVersion1")]
	internal virtual ColumnDefinition ColumnVersion1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnVersion2")]
	internal virtual ColumnDefinition ColumnVersion2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnVersion3")]
	internal virtual ColumnDefinition ColumnVersion3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnTime1")]
	internal virtual ColumnDefinition ColumnTime1
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnTime2")]
	internal virtual ColumnDefinition ColumnTime2
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnTime3")]
	internal virtual ColumnDefinition ColumnTime3
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PathVersion")]
	internal virtual Path PathVersion
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabVersion")]
	internal virtual TextBlock LabVersion
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PathDownload")]
	internal virtual Path PathDownload
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabDownload")]
	internal virtual TextBlock LabDownload
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PathTime")]
	internal virtual Path PathTime
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabTime")]
	internal virtual TextBlock LabTime
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PathSource")]
	internal virtual Path PathSource
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabSource")]
	internal virtual TextBlock LabSource
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public event ClickEventHandler Click;

	public MyResourceItem()
	{
		base.PreviewMouseLeftButtonUp += Button_MouseUp;
		Click += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
		{
			MyResourceItem_Click((MyResourceItem)a0, a1);
		};
		base.PreviewMouseLeftButtonDown += Button_MouseDown;
		base.MouseLeave += Button_MouseLeave;
		base.PreviewMouseLeftButtonUp += Button_MouseLeave;
		base.MouseEnter += RefreshColor;
		base.MouseLeave += RefreshColor;
		base.MouseLeftButtonDown += RefreshColor;
		base.MouseLeftButtonUp += RefreshColor;
		Uuid = ModBase.GetUuid();
		IsMouseDown = false;
		_RectBack = null;
		CanInteraction = true;
		InitializeComponent();
	}

	private void LabInfo_MouseEnter(object sender, MouseEventArgs e)
	{
		ToolTipInfo.Content = LabInfo.Text;
		LabInfo.ToolTip = (ModBase.IsTextTrimmed(LabInfo) ? ToolTipInfo : null);
	}

	private void Button_MouseUp(object sender, MouseButtonEventArgs e)
	{
		if (IsMouseDown)
		{
			Click?.Invoke(RuntimeHelpers.GetObjectValue(sender), e);
			if (!e.Handled)
			{
				Logger.Info($"按下资源工程列表项：{LabTitle.Text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\MyResourceItem.xaml.vb");
			}
		}
	}

	private void MyResourceItem_Click(MyResourceItem sender, EventArgs e)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		List<string> list = new List<string>();
		if (ModMain.FrmMain.PageCurrent.Page == FormMain.PageType.ResourceDetail)
		{
			foreach (MyCard child in ModMain.FrmDownloadResourceDetail.PanResults.Children)
			{
				if (Operators.CompareString(child.Title, "", TextCompare: false) != 0 && !child.IsSwapped)
				{
					list.Add(child.Title);
				}
			}
			Logger.Info(string.Format("记录当前已展开的卡片：{0}", string.Join("、", list)), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageDownload\\Resource\\MyResourceItem.xaml.vb");
		}
		string text = null;
		ModLoaders val = (ModLoaders)0;
		ResourceTypes val2 = default(ResourceTypes);
		if (ModMain.FrmMain.PageCurrent.Page == FormMain.PageType.Download)
		{
			switch (ModMain.FrmMain.PageCurrentSub)
			{
			case FormMain.PageSubType.DownloadMod:
				val2 = (ResourceTypes)1;
				text = ModMain.FrmDownloadMod.Content.Loader.Input.GameVersion;
				val = ModMain.FrmDownloadMod.Content.Loader.Input.ModLoaders;
				break;
			case FormMain.PageSubType.DownloadPack:
				val2 = (ResourceTypes)2;
				text = ModMain.FrmDownloadPack.Content.Loader.Input.GameVersion;
				break;
			case FormMain.PageSubType.DownloadDataPack:
				val2 = (ResourceTypes)16;
				text = ModMain.FrmDownloadDataPack.Content.Loader.Input.GameVersion;
				break;
			case FormMain.PageSubType.DownloadResourcePack:
				val2 = (ResourceTypes)4;
				text = ModMain.FrmDownloadResourcePack.Content.Loader.Input.GameVersion;
				break;
			case FormMain.PageSubType.DownloadShader:
				val2 = (ResourceTypes)8;
				text = ModMain.FrmDownloadShader.Content.Loader.Input.GameVersion;
				break;
			}
		}
		else
		{
			val2 = (ResourceTypes)127;
			text = Conversions.ToString(NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 2 }, null));
			val = (ModLoaders)Conversions.ToInteger(NewLateBinding.LateIndexGet(ModMain.FrmMain.PageCurrent.Additional, new object[1] { 3 }, null));
		}
		ModMain.FrmMain.PageChange(new FormMain.PageStackData
		{
			Page = FormMain.PageType.ResourceDetail,
			Additional = new object[5] { sender.Tag, list, text, val, val2 }
		});
	}

	private void Button_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (base.IsMouseOver && CanInteraction)
		{
			IsMouseDown = true;
		}
	}

	private void Button_MouseLeave(object sender, object e)
	{
		IsMouseDown = false;
	}

	public void RefreshColor(object sender, EventArgs e)
	{
		if (!CanInteraction)
		{
			return;
		}
		string text;
		int num;
		if (base.IsMouseOver)
		{
			if (IsMouseDown)
			{
				text = "MouseDown";
				num = 120;
			}
			else
			{
				text = "MouseOver";
				num = 120;
			}
		}
		else
		{
			text = "Idle";
			num = 180;
		}
		if (Operators.CompareString(StateLast, text, TextCompare: false) == 0)
		{
			return;
		}
		StateLast = text;
		checked
		{
			if (base.IsLoaded && ModAnimation.AniControlEnabled == 0)
			{
				List<ModAnimation.AniData> list = new List<ModAnimation.AniData>();
				if (base.IsMouseOver)
				{
					list.AddRange(new ModAnimation.AniData[2]
					{
						ModAnimation.AaColor(RectBack, Border.BackgroundProperty, IsMouseDown ? "ColorBrush6" : "ColorBrushBg1", num),
						ModAnimation.AaOpacity(RectBack, 1.0 - RectBack.Opacity, num, 0, new ModAnimation.AniEaseOutFluent())
					});
					if (IsMouseDown)
					{
						list.Add(ModAnimation.AaScaleTransform(RectBack, 0.996 - ((ScaleTransform)RectBack.RenderTransform).ScaleX, (int)Math.Round((double)num * 1.2), 0, new ModAnimation.AniEaseOutFluent()));
					}
					else
					{
						list.Add(ModAnimation.AaScaleTransform(RectBack, 1.0 - ((ScaleTransform)RectBack.RenderTransform).ScaleX, (int)Math.Round((double)num * 1.2), 0, new ModAnimation.AniEaseOutFluent()));
					}
				}
				else
				{
					list.AddRange(new ModAnimation.AniData[4]
					{
						ModAnimation.AaOpacity(RectBack, 0.0 - RectBack.Opacity, num),
						ModAnimation.AaColor(RectBack, Border.BackgroundProperty, IsMouseDown ? "ColorBrush6" : "ColorBrush7", num),
						ModAnimation.AaScaleTransform(RectBack, 0.996 - ((ScaleTransform)RectBack.RenderTransform).ScaleX, num, 0, new ModAnimation.AniEaseOutFluent()),
						ModAnimation.AaScaleTransform(RectBack, -0.196, 1, 0, null, After: true)
					});
				}
				ModAnimation.AniStart(list, "MyResourceItem Color " + Conversions.ToString(Uuid));
			}
			else
			{
				ModAnimation.AniStop("MyResourceItem Color " + Conversions.ToString(Uuid));
				if (_RectBack != null)
				{
					RectBack.Opacity = 0.0;
				}
			}
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagedownload/resource/myresourceitem.xaml", UriKind.Relative);
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
			PanBack = (MyResourceItem)target;
			break;
		case 2:
			PathLogo = (MyImage)target;
			break;
		case 3:
			LabTitle = (TextBlock)target;
			break;
		case 4:
			LabTitleRaw = (TextBlock)target;
			break;
		case 5:
			PanTags = (StackPanel)target;
			break;
		case 6:
			LabInfo = (TextBlock)target;
			break;
		case 7:
			ToolTipInfo = (ToolTip)target;
			break;
		case 8:
			ColumnVersion1 = (ColumnDefinition)target;
			break;
		case 9:
			ColumnVersion2 = (ColumnDefinition)target;
			break;
		case 10:
			ColumnVersion3 = (ColumnDefinition)target;
			break;
		case 11:
			ColumnTime1 = (ColumnDefinition)target;
			break;
		case 12:
			ColumnTime2 = (ColumnDefinition)target;
			break;
		case 13:
			ColumnTime3 = (ColumnDefinition)target;
			break;
		case 14:
			PathVersion = (Path)target;
			break;
		case 15:
			LabVersion = (TextBlock)target;
			break;
		case 16:
			PathDownload = (Path)target;
			break;
		case 17:
			LabDownload = (TextBlock)target;
			break;
		case 18:
			PathTime = (Path)target;
			break;
		case 19:
			LabTime = (TextBlock)target;
			break;
		case 20:
			PathSource = (Path)target;
			break;
		case 21:
			LabSource = (TextBlock)target;
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
