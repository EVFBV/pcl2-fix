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
using System.Windows.Threading;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[DesignerGenerated]
public class PageSpeedLeft : MyPageLeft, IComponentConnector
{
	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D0
	{
		public MyCard _0024VB_0024Local_Card;

		public _Closure_0024__6_002D1 _0024VB_0024NonLocal__0024VB_0024Closure_2;

		public _Closure_0024__6_002D0(_Closure_0024__6_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Card = arg0._0024VB_0024Local_Card;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__R4(object a0, EventArgs a1)
		{
			_Lambda_0024__1((MyIconButton)a0, a1);
		}

		[SpecialName]
		internal void _Lambda_0024__1(MyIconButton sender, EventArgs e)
		{
			ModAnimation.AniDispose(sender, RemoveFromChildren: false);
			ModAnimation.AniDispose(_0024VB_0024Local_Card, RemoveFromChildren: true, [SpecialName] (object a0) =>
			{
				((VB_0024AnonymousDelegate_10)([SpecialName] () =>
				{
					if (ModMain.FrmSpeedRight.PanMain.Children.Count == 0 && ModMain.FrmMain.PageCurrent == FormMain.PageType.TaskManager)
					{
						ModMain.FrmMain.PageBack();
					}
				}))();
			});
			_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.RightCards.Remove(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader.Name);
			ModLoader.LoaderTaskbar.Remove(_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader);
			Logger.Info($"关闭任务管理卡片：{_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader.Name}，且移出任务列表", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
			ModBase.RunInThread([SpecialName] () =>
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader.Cancel();
			});
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__6_002D1
	{
		public ModLoader.LoaderBase _0024VB_0024Local_Loader;

		public PageSpeedLeft _0024VB_0024Me;

		public Action _0024I3;

		public _Closure_0024__6_002D1(_Closure_0024__6_002D1 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_Loader = arg0._0024VB_0024Local_Loader;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__3()
		{
			_0024VB_0024Local_Loader.Cancel();
		}
	}

	private const int WatcherInterval = 300;

	private bool IsLoad;

	private readonly Dictionary<string, MyCard> RightCards;

	private bool _contentLoaded;

	[field: AccessedThroughProperty("LabProgress")]
	internal virtual TextBlock LabProgress
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabSpeed")]
	internal virtual TextBlock LabSpeed
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabFile")]
	internal virtual TextBlock LabFile
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabThread")]
	internal virtual TextBlock LabThread
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public PageSpeedLeft()
	{
		base.Loaded += Page_Loaded;
		IsLoad = false;
		RightCards = new Dictionary<string, MyCard>();
		InitializeComponent();
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		Watcher();
		TryReturnToHome();
		if (!IsLoad)
		{
			IsLoad = true;
			DispatcherTimer dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
			dispatcherTimer.Tick += [SpecialName] (object a0, EventArgs a1) =>
			{
				Watcher();
			};
			dispatcherTimer.Start();
			if (!ModBase.ModeDebug)
			{
				base.RowDefinitions[12].Height = new GridLength(0.0);
				base.RowDefinitions[13].Height = new GridLength(0.0);
				base.RowDefinitions[14].Height = new GridLength(0.0);
				base.RowDefinitions[15].Height = new GridLength(0.0);
			}
		}
	}

	private void Watcher()
	{
		if (!(ModMain.FrmMain.PageCurrent == FormMain.PageType.TaskManager))
		{
			return;
		}
		try
		{
			if (!((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).Any())
			{
				LabProgress.Text = "100 %";
				LabSpeed.Text = "0 B/s";
				LabFile.Text = "0";
				LabThread.Text = "0 / " + Conversions.ToString(ModNet.NetTaskThreadLimit);
			}
			else
			{
				List<ModLoader.LoaderBase> source = ((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).Where([SpecialName] (ModLoader.LoaderBase l) => l.Show).ToList();
				double num = (source.Any() ? MathUtils.Clamp<double>(source.Select([SpecialName] (ModLoader.LoaderBase l) => l.Progress).Average(), 0.0, 1.0) : 1.0);
				string text = Conversions.ToString(Math.Floor(num * 100.0)) + "." + StringExtensions.EnsureLength(Math.Floor((num * 100.0 - Math.Floor(num * 100.0)) * 100.0).ToString(), '0', 2) + " %";
				LabProgress.Text = ((num > 0.999999) ? "100 %" : text);
				LabSpeed.Text = StringUtils.FormatByteSize(ModNet.NetManager.Speed) + "/s";
				LabFile.Text = Conversions.ToString((ModNet.NetManager.FileRemain < 0) ? "0*" : ((object)ModNet.NetManager.FileRemain));
				LabThread.Text = Conversions.ToString(ModNet.NetTaskThreadCount) + " / " + Conversions.ToString(ModNet.NetTaskThreadLimit);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Error(ex2, "任务管理左栏监视出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
			ProjectData.ClearProjectError();
		}
		if (ModMain.FrmSpeedRight == null || ModMain.FrmSpeedRight.PanMain == null)
		{
			return;
		}
		try
		{
			foreach (ModLoader.LoaderBase item in ((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).ToList())
			{
				TaskRefresh(item);
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Error(ex4, "任务管理右栏监视出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	public void TaskRefresh(ModLoader.LoaderBase Loader)
	{
		_Closure_0024__6_002D1 arg = new _Closure_0024__6_002D1(arg);
		arg._0024VB_0024Me = this;
		arg._0024VB_0024Local_Loader = Loader;
		if (arg._0024VB_0024Local_Loader == null || !arg._0024VB_0024Local_Loader.Show)
		{
			return;
		}
		checked
		{
			try
			{
				List<ModLoader.LoaderBase> list = (List<ModLoader.LoaderBase>)NewLateBinding.LateGet(arg._0024VB_0024Local_Loader, null, "GetLoaderList", new object[0], null, null, null);
				if (RightCards.ContainsKey(arg._0024VB_0024Local_Loader.Name))
				{
					Grid grid = RightCards[arg._0024VB_0024Local_Loader.Name];
					double num = arg._0024VB_0024Local_Loader.Progress + (double)arg._0024VB_0024Local_Loader.State;
					if (ModBase.Val(RuntimeHelpers.GetObjectValue(grid.Tag)) == num)
					{
						return;
					}
					grid.Tag = num;
					if (grid.Children.Count > 3)
					{
						grid = (Grid)grid.Children[3];
						try
						{
							switch (arg._0024VB_0024Local_Loader.State)
							{
							case ModBase.LoadState.Failed:
							{
								grid.RowDefinitions.Clear();
								grid.Children.Clear();
								grid.Children.Add((UIElement)ModBase.GetObjectFromXML("<Path xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Stretch=\"Uniform\" Tag=\"Failed\" Data=\"F1 M2.5,0 L0,2.5 7.5,10 0,17.5 2.5,20 10,12.5 17.5,20 20,17.5 12.5,10 20,2.5 17.5,0 10,7.5 2.5,0Z\" Height=\"15\" Width=\"15\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"0\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,1,0,0\" VerticalAlignment=\"Top\"/>"));
								TextBlock textBlock = (TextBlock)ModBase.GetObjectFromXML("<TextBlock xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" TextWrapping=\"Wrap\" HorizontalAlignment=\"Left\" ToolTip=\"单击复制错误详情\" Grid.Column=\"1\" Grid.Row=\"0\" Margin=\"0,0,0,5\" />");
								textBlock.Text = ExceptionExtensions.GetDisplay(arg._0024VB_0024Local_Loader.Error, true);
								textBlock.MouseLeftButtonDown += [SpecialName] (object a0, MouseButtonEventArgs a1) =>
								{
									((VB_0024AnonymousDelegate_8<TextBlock, EventArgs>)([SpecialName] (TextBlock sender, EventArgs e) =>
									{
										ModBase.ClipboardSet(sender.Text, ShowSuccessHint: false);
										ModMain.Hint("已复制错误详情！", ModMain.HintType.Green);
									}))((TextBlock)a0, a1);
								};
								grid.Children.Add(textBlock);
								break;
							}
							case ModBase.LoadState.Finished:
							case ModBase.LoadState.Canceled:
								ModAnimation.AniDispose((MyCard)grid.Parent, RemoveFromChildren: true, [SpecialName] (object a0) =>
								{
									TryReturnToHome();
								});
								break;
							case ModBase.LoadState.Waiting:
							case ModBase.LoadState.Loading:
								try
								{
									if (grid.Children.Count < list.Count * 2)
									{
										Logger.Warn($"刷新任务管理卡片 {arg._0024VB_0024Local_Loader.Name} 失败：卡片中仅有 {grid.Children.Count} 个子项，要求至少有 {list.Count * 2} 个子项", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
										break;
									}
									int num2 = 0;
									foreach (ModLoader.LoaderBase item in list)
									{
										switch (item.State)
										{
										case ModBase.LoadState.Waiting:
											if (Operators.ConditionalCompareObjectNotEqual(((FrameworkElement)grid.Children[num2 * 2]).Tag, "Waiting", TextCompare: false))
											{
												grid.Children.RemoveAt(num2 * 2);
												grid.Children.Insert(num2 * 2, (UIElement)ModBase.GetObjectFromXML("<Path xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\" Stretch=\"Uniform\" Tag=\"Waiting\" Data=\"F1 M5,0 a5,5 360 1 0 0,0.0001 m15,0 a5,5 360 1 0 0,0.0001 m15,0 a5,5 360 1 0 0,0.0001 Z\" Width=\"18\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num2) + "\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,7,0,0\" VerticalAlignment=\"Top\" Height=\"6\"/>"));
											}
											break;
										case ModBase.LoadState.Loading:
											if (Operators.ConditionalCompareObjectNotEqual(((FrameworkElement)grid.Children[num2 * 2]).Tag, "Loading", TextCompare: false))
											{
												grid.Children.RemoveAt(num2 * 2);
												grid.Children.Insert(num2 * 2, (UIElement)ModBase.GetObjectFromXML("<TextBlock xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\" Text=\"" + Conversions.ToString(Math.Floor(item.Progress * 100.0)) + "%\" Tag=\"Loading\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num2) + "\" Foreground=\"{DynamicResource ColorBrush3}\"/>"));
											}
											else
											{
												((TextBlock)grid.Children[num2 * 2]).Text = Conversions.ToString(Math.Floor(item.Progress * 100.0)) + "%";
											}
											break;
										case ModBase.LoadState.Finished:
											if (Operators.ConditionalCompareObjectNotEqual(((FrameworkElement)grid.Children[num2 * 2]).Tag, "Finished", TextCompare: false))
											{
												grid.Children.RemoveAt(num2 * 2);
												grid.Children.Insert(num2 * 2, (UIElement)ModBase.GetObjectFromXML("<Path xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\" Stretch=\"Uniform\" Tag=\"Finished\" Data=\"F1 M 23.7501,33.25L 34.8334,44.3333L 52.2499,22.1668L 56.9999,26.9168L 34.8334,53.8333L 19.0001,38L 23.7501,33.25 Z\" Height=\"16\" Width=\"15\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num2) + "\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,3,0,0\" VerticalAlignment=\"Top\"/>"));
											}
											break;
										}
										num2++;
									}
									break;
								}
								catch (Exception ex)
								{
									ProjectData.SetProjectError(ex);
									Exception ex2 = ex;
									Logger.Error(ex2, $"刷新任务管理卡片 {arg._0024VB_0024Local_Loader.Name} 失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
									ProjectData.ClearProjectError();
									break;
								}
							}
							return;
						}
						catch (Exception ex3)
						{
							ProjectData.SetProjectError(ex3);
							Exception ex4 = ex3;
							Logger.Error(ex4, $"更新任务管理显示失败（{arg._0024VB_0024Local_Loader.State}）", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
							ProjectData.ClearProjectError();
							return;
						}
					}
					Logger.Warn($"元素不足的卡片：{arg._0024VB_0024Local_Loader.Name}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
				}
				else
				{
					if (arg._0024VB_0024Local_Loader.State == ModBase.LoadState.Canceled || arg._0024VB_0024Local_Loader.State == ModBase.LoadState.Finished)
					{
						return;
					}
					try
					{
						_Closure_0024__6_002D0 arg2 = default(_Closure_0024__6_002D0);
						_Closure_0024__6_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__6_002D0(arg2);
						CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2 = arg;
						string text = "\r\n                        <local:MyCard xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:local=\"clr-namespace:PCL;assembly=Plain Craft Launcher 2\"\r\n                            Tag=\"" + Conversions.ToString(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader.Progress + (double)CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader.State) + "\" Title=\"" + StringUtils.XmlEscape(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader.Name) + "\" Margin=\"0,0,0,15\">\r\n                            <Grid Margin=\"14,40,15,10\">\r\n                                <Grid.ColumnDefinitions>\r\n                                    <ColumnDefinition Width=\"50\"/>\r\n                                    <ColumnDefinition/>\r\n                                </Grid.ColumnDefinitions>\r\n                                <Grid.RowDefinitions>";
						foreach (ModLoader.LoaderBase item2 in list)
						{
							_ = item2;
							text += "<RowDefinition Height=\"26\"/>";
						}
						text += "</Grid.RowDefinitions>";
						int num3 = 0;
						foreach (ModLoader.LoaderBase item3 in list)
						{
							text = item3.State switch
							{
								ModBase.LoadState.Waiting => text + "<Path Stretch=\"Uniform\" Tag=\"Waiting\" Data=\"F1 M5,0 a5,5 360 1 0 0,0.0001 m15,0 a5,5 360 1 0 0,0.0001 m15,0 a5,5 360 1 0 0,0.0001 Z\" Width=\"18\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num3) + "\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,7,0,0\" VerticalAlignment=\"Top\" Height=\"6\"/>", 
								ModBase.LoadState.Loading => text + "<TextBlock Text=\"" + Conversions.ToString(Math.Floor(item3.Progress * 100.0)) + "%\" Tag=\"Loading\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num3) + "\" Foreground=\"{DynamicResource ColorBrush3}\" />", 
								ModBase.LoadState.Finished => text + "<Path Stretch=\"Uniform\" Tag=\"Finished\" Data=\"F1 M 23.7501,33.25L 34.8334,44.3333L 52.2499,22.1668L 56.9999,26.9168L 34.8334,53.8333L 19.0001,38L 23.7501,33.25 Z\" Height=\"16\" Width=\"15\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num3) + "\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,3,0,0\" VerticalAlignment=\"Top\"/>", 
								_ => text + "<Path Stretch=\"Uniform\" Tag=\"Failed\" Data=\"F1 M2.5,0 L0,2.5 7.5,10 0,17.5 2.5,20 10,12.5 17.5,20 20,17.5 12.5,10 20,2.5 17.5,0 10,7.5 2.5,0Z\" Height=\"15\" Width=\"15\" HorizontalAlignment=\"Center\" Grid.Column=\"0\" Grid.Row=\"" + Conversions.ToString(num3) + "\" Fill=\"{DynamicResource ColorBrush3}\" Margin=\"0,1,0,0\" VerticalAlignment=\"Top\"/>", 
							};
							text = text + "<TextBlock Text=\"" + StringUtils.XmlEscape(item3.Name) + "\" HorizontalAlignment=\"Left\" Grid.Column=\"1\" Grid.Row=\"" + Conversions.ToString(num3) + "\"/>";
							num3++;
						}
						text += "</Grid></local:MyCard>";
						try
						{
							CS_0024_003C_003E8__locals0._0024VB_0024Local_Card = (MyCard)ModBase.GetObjectFromXML(text);
						}
						catch (Exception ex5)
						{
							ProjectData.SetProjectError(ex5);
							Exception ex6 = ex5;
							Logger.Warn(ex6, "新建任务管理卡片失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
							Logger.Info(string.Format("出错的卡片内容：{0}{1}", "\r\n", text), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
							throw;
						}
						ModMain.FrmSpeedRight.PanMain.Children.Insert(0, CS_0024_003C_003E8__locals0._0024VB_0024Local_Card);
						RightCards.Add(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader.Name, CS_0024_003C_003E8__locals0._0024VB_0024Local_Card);
						Logger.Info($"新建任务管理卡片：{CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader.Name}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
						MyIconButton myIconButton = new MyIconButton
						{
							Name = "BtnCancel",
							Logo = "F1 M2,0 L0,2 8,10 0,18 2,20 10,12 18,20 20,18 12,10 20,2 18,0 10,8 2,0Z",
							Height = 20.0,
							Margin = new Thickness(0.0, 10.0, 10.0, 0.0),
							LogoScale = 1.1,
							HorizontalAlignment = HorizontalAlignment.Right,
							VerticalAlignment = VerticalAlignment.Top
						};
						CS_0024_003C_003E8__locals0._0024VB_0024Local_Card.Children.Add(myIconButton);
						myIconButton.Click += [SpecialName] (object a0, EventArgs a1) =>
						{
							CS_0024_003C_003E8__locals0._Lambda_0024__1((MyIconButton)a0, a1);
						};
						if (CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader.State == ModBase.LoadState.Failed)
						{
							CS_0024_003C_003E8__locals0._0024VB_0024Local_Card.Tag = null;
							TaskRefresh(CS_0024_003C_003E8__locals0._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Loader);
						}
						return;
					}
					catch (Exception ex7)
					{
						ProjectData.SetProjectError(ex7);
						Exception ex8 = ex7;
						Logger.Error(ex8, "添加任务管理卡片失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
						ProjectData.ClearProjectError();
						return;
					}
				}
			}
			catch (Exception ex9)
			{
				ProjectData.SetProjectError(ex9);
				Exception ex10 = ex9;
				Logger.Error(ex10, "刷新任务管理显示失败", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
	}

	public void TaskRemove(object Loader)
	{
		if (RightCards.ContainsKey(Conversions.ToString(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null))))
		{
			ModBase.RunInUiWait([SpecialName] () =>
			{
				Grid element = RightCards[Conversions.ToString(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null))];
				ModMain.FrmSpeedRight.PanMain.Children.Remove(element);
				RightCards.Remove(Conversions.ToString(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null)));
				Logger.Info(string.Format("移除任务管理卡片：{0}", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Loader, null, "Name", new object[0], null, null, null))), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageSpeedLeft.xaml.vb");
			});
		}
	}

	private void TryReturnToHome()
	{
		if (ModMain.FrmSpeedRight.PanMain.Children.Count == 0 && ModMain.FrmMain.PageCurrent == FormMain.PageType.TaskManager)
		{
			ModMain.FrmMain.PageBack();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagespeedleft.xaml", UriKind.Relative);
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
			LabProgress = (TextBlock)target;
			break;
		case 2:
			LabSpeed = (TextBlock)target;
			break;
		case 3:
			LabFile = (TextBlock)target;
			break;
		case 4:
			LabThread = (TextBlock)target;
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
