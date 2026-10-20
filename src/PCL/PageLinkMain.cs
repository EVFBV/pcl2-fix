using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;

namespace PCL;

[DesignerGenerated]
public class PageLinkMain : MyPageRight, IComponentConnector
{
	public enum LinkStates
	{
		Waiting,
		Loading,
		Failed,
		Finished
	}

	private class Peer
	{
		public enum Types
		{
			Client,
			Server,
			Misc,
			Self
		}

		public readonly Types Type;

		public readonly double Ping;

		public readonly string Name;

		public readonly bool Relay;

		public readonly NATTypes NATType;

		public Peer(JObject Info)
		{
			string text = Info["hostname"].ToString();
			if (Operators.CompareString(Info["cost"].ToString(), "Local", TextCompare: false) == 0)
			{
				Type = Types.Self;
			}
			else if (StringExtensions.StartsWithF(text, "Client", false))
			{
				Type = Types.Client;
			}
			else if ((string)Info["ipv4"] == "10.114.114.114")
			{
				Type = Types.Server;
			}
			else
			{
				Type = Types.Misc;
			}
			double.TryParse((string)Info["lat_ms"], NumberStyles.Any, CultureInfo.InvariantCulture, out Ping);
			Name = text;
			Relay = StringExtensions.ContainsIgnoreCase(Info["cost"].ToString(), "relay");
			NATType = EnumUtils.FromString<NATTypes>((string)Info["nat_type"]);
		}

		public override string ToString()
		{
			return $"{Type} - {Name} - Ping {Ping:0.0}ms [中继? {Relay}] - NAT {NATType}";
		}
	}

	private enum NATTypes
	{
		Unknown,
		OpenInternet,
		NoPAT,
		FullCone,
		Restricted,
		PortRestricted,
		Symmetric,
		SymUdpFirewall,
		SymmetricEasyInc,
		SymmetricEasyDec,
		Pending
	}

	private const int INVITE_CODE_VERSION = 2;

	public static LinkStates LinkState = LinkStates.Waiting;

	private static string LastCode = null;

	private static string AutoJoinCode = null;

	private string FailReason;

	private string PathEasyTier;

	private bool IsServerSide;

	private int ServerPort;

	private int ClientPort;

	private int RPCPort;

	private string ClientAddress;

	private int DiscoverNodeID;

	private string NetworkName;

	private string NetworkSecret;

	private int ServerVersion;

	private static Process ProcessCore;

	private static AutoResetEvent ProcessOutputHandle;

	private static AutoResetEvent ProcessErrorHandle;

	private Queue<string> LogHistory;

	private List<Peer> Peers;

	private NATTypes NATType;

	private ModLoader.LoaderTask<int, List<Peer>> RefreshPeerLoader;

	private int FailCount;

	[CompilerGenerated]
	[AccessedThroughProperty("CardLoad")]
	private MyCard _CardLoad;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnLoadCancel")]
	private MyIconButton _BtnLoadCancel;

	[CompilerGenerated]
	[AccessedThroughProperty("PanSelectCreate")]
	private MyCard _PanSelectCreate;

	[CompilerGenerated]
	[AccessedThroughProperty("PanSelectJoin")]
	private MyCard _PanSelectJoin;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnFinishCopy")]
	private MyButton _BtnFinishCopy;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnFinishExit")]
	private MyButton _BtnFinishExit;

	[CompilerGenerated]
	[AccessedThroughProperty("BtnFinishIp")]
	private StackPanel _BtnFinishIp;

	private bool _contentLoaded;

	[SpecialName]
	private bool _0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted_0024Init;

	[SpecialName]
	private Socket _0024STATIC_0024Update_00242001_0024BroadcastSocket;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024Update_00242001_0024BroadcastSocket_0024Init;

	[field: AccessedThroughProperty("LinkLoader")]
	private virtual ModLoader.LoaderCombo<int> LinkLoader
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	private int PeopleCount
	{
		get
		{
			if (Peers == null)
			{
				return 0;
			}
			return checked(Peers.Where([SpecialName] (Peer p) => p.Type != Peer.Types.Misc).Count() + 1);
		}
	}

	[field: AccessedThroughProperty("PanLoad")]
	internal virtual Grid PanLoad
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCard CardLoad
	{
		[CompilerGenerated]
		get
		{
			return _CardLoad;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				CardLoad_MouseLeftButtonUp();
			};
			SizeChangedEventHandler value3 = [SpecialName] (object a0, SizeChangedEventArgs a1) =>
			{
				CardLoad_SizeChanged();
			};
			MyCard cardLoad = _CardLoad;
			if (cardLoad != null)
			{
				cardLoad.MouseLeftButtonUp -= value2;
				cardLoad.SizeChanged -= value3;
			}
			_CardLoad = value;
			cardLoad = _CardLoad;
			if (cardLoad != null)
			{
				cardLoad.MouseLeftButtonUp += value2;
				cardLoad.SizeChanged += value3;
			}
		}
	}

	[field: AccessedThroughProperty("Load")]
	internal virtual MyLoading Load
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabLoadTitle")]
	internal virtual TextBlock LabLoadTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabLoadDesc")]
	internal virtual TextBlock LabLoadDesc
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyIconButton BtnLoadCancel
	{
		[CompilerGenerated]
		get
		{
			return _BtnLoadCancel;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyIconButton.ClickEventHandler obj = [SpecialName] (object a0, EventArgs a1) =>
			{
				BtnLoadCancel_Click();
			};
			MyIconButton btnLoadCancel = _BtnLoadCancel;
			if (btnLoadCancel != null)
			{
				btnLoadCancel.Click -= obj;
			}
			_BtnLoadCancel = value;
			btnLoadCancel = _BtnLoadCancel;
			if (btnLoadCancel != null)
			{
				btnLoadCancel.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("ColumnProgressA")]
	internal virtual ColumnDefinition ColumnProgressA
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnProgressB")]
	internal virtual ColumnDefinition ColumnProgressB
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RectProgressClip")]
	internal virtual RectangleGeometry RectProgressClip
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanContent")]
	internal virtual Grid PanContent
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanSelect")]
	internal virtual Grid PanSelect
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyCard PanSelectCreate
	{
		[CompilerGenerated]
		get
		{
			return _PanSelectCreate;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Create_MouseLeftButtonUp();
			};
			MyCard panSelectCreate = _PanSelectCreate;
			if (panSelectCreate != null)
			{
				panSelectCreate.MouseLeftButtonUp -= value2;
			}
			_PanSelectCreate = value;
			panSelectCreate = _PanSelectCreate;
			if (panSelectCreate != null)
			{
				panSelectCreate.MouseLeftButtonUp += value2;
			}
		}
	}

	internal virtual MyCard PanSelectJoin
	{
		[CompilerGenerated]
		get
		{
			return _PanSelectJoin;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Join();
			};
			RoutedEventHandler value3 = [SpecialName] (object a0, RoutedEventArgs a1) =>
			{
				PanSelectJoin_Loaded();
			};
			MyCard panSelectJoin = _PanSelectJoin;
			if (panSelectJoin != null)
			{
				panSelectJoin.MouseLeftButtonUp -= value2;
				panSelectJoin.Loaded -= value3;
			}
			_PanSelectJoin = value;
			panSelectJoin = _PanSelectJoin;
			if (panSelectJoin != null)
			{
				panSelectJoin.MouseLeftButtonUp += value2;
				panSelectJoin.Loaded += value3;
			}
		}
	}

	[field: AccessedThroughProperty("PanFinish")]
	internal virtual Grid PanFinish
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("HintFinish")]
	internal virtual MyHint HintFinish
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ImgFinishLogo")]
	internal virtual Path ImgFinishLogo
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabFinishTitle")]
	internal virtual TextBlock LabFinishTitle
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabFinishDesc")]
	internal virtual TextBlock LabFinishDesc
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual MyButton BtnFinishCopy
	{
		[CompilerGenerated]
		get
		{
			return _BtnFinishCopy;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				Copy();
			};
			MyButton btnFinishCopy = _BtnFinishCopy;
			if (btnFinishCopy != null)
			{
				btnFinishCopy.Click -= obj;
			}
			_BtnFinishCopy = value;
			btnFinishCopy = _BtnFinishCopy;
			if (btnFinishCopy != null)
			{
				btnFinishCopy.Click += obj;
			}
		}
	}

	internal virtual MyButton BtnFinishExit
	{
		[CompilerGenerated]
		get
		{
			return _BtnFinishExit;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MyButton.ClickEventHandler obj = [SpecialName] (object a0, MouseButtonEventArgs a1) =>
			{
				BtnFinishExit_Click();
			};
			MyButton btnFinishExit = _BtnFinishExit;
			if (btnFinishExit != null)
			{
				btnFinishExit.Click -= obj;
			}
			_BtnFinishExit = value;
			btnFinishExit = _BtnFinishExit;
			if (btnFinishExit != null)
			{
				btnFinishExit.Click += obj;
			}
		}
	}

	[field: AccessedThroughProperty("BtnFinishPing")]
	internal virtual StackPanel BtnFinishPing
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabFinishPing")]
	internal virtual TextBlock LabFinishPing
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("BtnFinishPlayer")]
	internal virtual StackPanel BtnFinishPlayer
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabFinishPlayer")]
	internal virtual TextBlock LabFinishPlayer
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LineFinishPort")]
	internal virtual TextBlock LineFinishPort
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("BtnFinishPort")]
	internal virtual StackPanel BtnFinishPort
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LabFinishPort")]
	internal virtual TextBlock LabFinishPort
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	internal virtual StackPanel BtnFinishIp
	{
		[CompilerGenerated]
		get
		{
			return _BtnFinishIp;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseButtonEventHandler value2 = BtnFinishIp_MouseLeftButtonUp;
			StackPanel btnFinishIp = _BtnFinishIp;
			if (btnFinishIp != null)
			{
				btnFinishIp.MouseLeftButtonUp -= value2;
			}
			_BtnFinishIp = value;
			btnFinishIp = _BtnFinishIp;
			if (btnFinishIp != null)
			{
				btnFinishIp.MouseLeftButtonUp += value2;
			}
		}
	}

	[field: AccessedThroughProperty("LabFinishIp")]
	internal virtual TextBlock LabFinishIp
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PanAlways")]
	internal virtual Grid PanAlways
	{
		get; [MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public PageLinkMain()
	{
		base.Initialized += [SpecialName] (object a0, EventArgs a1) =>
		{
			LoaderInit();
		};
		base.PageEnter += UpdatePanelVisibility;
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			StartTimerThread();
		};
		base.Loaded += [SpecialName] (object a0, RoutedEventArgs a1) =>
		{
			Update();
		};
		FailReason = "准备初始化";
		PathEasyTier = Paths.AppDataThenName + "EasyTier\\";
		ClientAddress = null;
		DiscoverNodeID = -1;
		LinkLoader = new ModLoader.LoaderCombo<int>("联机", new ModLoader.LoaderBase[4]
		{
			new ModLoader.LoaderTask<int, int>("获取配置", InitConfig)
			{
				ProgressWeight = 8.0
			},
			new ModLoader.LoaderTask<int, List<ModNet.NetFile>>("准备下载联机模块", InitPrepareDownload)
			{
				ProgressWeight = 2.0
			},
			new ModNet.LoaderDownload("下载联机模块", new List<ModNet.NetFile>())
			{
				ProgressWeight = 40.0
			},
			new ModLoader.LoaderTask<int, int>("启动联机模块", InitLaunch)
			{
				ProgressWeight = 50.0
			}
		});
		LogHistory = new Queue<string>(10);
		Peers = null;
		NATType = NATTypes.Pending;
		RefreshPeerLoader = new ModLoader.LoaderTask<int, List<Peer>>("EasyTier CLI", [SpecialName] (ModLoader.LoaderTask<int, List<Peer>> a0) =>
		{
			RefreshPeer();
		});
		FailCount = 0;
		InitializeComponent();
	}

	private void ChangeState(LinkStates NewState)
	{
		if (LinkState == NewState)
		{
			return;
		}
		LinkStates linkState = LinkState;
		LinkState = NewState;
		Logger.Info($"主状态由 {linkState} 变更为 {NewState}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		ModBase.RunInUi([SpecialName] () =>
		{
			switch (NewState)
			{
			case LinkStates.Waiting:
				SwitchToWaiting(linkState);
				break;
			case LinkStates.Loading:
				SwitchToLoading(linkState);
				break;
			case LinkStates.Failed:
				SwitchToFailed(linkState);
				break;
			case LinkStates.Finished:
				SwitchToFinished(linkState);
				break;
			}
		});
	}

	private void LoaderInit()
	{
		PageLoaderInit(Load, PanLoad, PanContent, PanAlways, LinkLoader, null, null, AutoRun: false);
		LinkLoader.OnStateChangedUi += [SpecialName] (ModLoader.LoaderBase Loader, ModBase.LoadState NewState, ModBase.LoadState OldState) =>
		{
			switch (NewState)
			{
			case ModBase.LoadState.Finished:
				ChangeState(LinkStates.Finished);
				break;
			case ModBase.LoadState.Failed:
				ModMain.Telemetry("联机失败", "Exception", ModMinecraft.FilterUserName(ExceptionExtensions.GetDisplay(Loader.Error, true), '*'));
				ChangeState(LinkStates.Failed);
				break;
			}
		};
	}

	private void UpdatePanelVisibility()
	{
		ModMain.FrmLinkMain.PanSelect.Visibility = ((LinkState != 0) ? Visibility.Collapsed : Visibility.Visible);
		ModMain.FrmLinkMain.PanFinish.Visibility = ((LinkState != LinkStates.Finished) ? Visibility.Collapsed : Visibility.Visible);
	}

	private void SwitchToWaiting(LinkStates OldState)
	{
		LinkLoader.Cancel();
		object lockState = LinkLoader.LockState;
		ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(lockState, ref lockTaken);
			LinkLoader.State = ModBase.LoadState.Waiting;
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(lockState);
			}
		}
		ProcessStop();
		if (OldState != LinkStates.Finished)
		{
			UpdatePanelVisibility();
		}
		PageOnContentExit();
	}

	private void Create_MouseLeftButtonUp()
	{
		string text = ModMain.MyMsgBoxInput("输入端口", string.Format("在单人游戏的暂停菜单选择 {0}对局域网开放{1}，然后输入端口数字。{2}甚至可以输入其他游戏的端口……嗯……", ModBase.vbLQ, ModBase.vbRQ, "\r\n"), null, new Collection<Validate>
		{
			new ValidateInteger(1024, 65535)
		}, "端口号");
		if (text == null)
		{
			return;
		}
		IsServerSide = true;
		ServerPort = Conversions.ToInteger(text);
		VB_0024AnonymousDelegate_0<string> vB_0024AnonymousDelegate_ = [SpecialName] () =>
		{
			string text2 = "";
			int num = 1;
			do
			{
				text2 += MathUtils.ConvertRadix(ModBase.RandomInteger(0, 35), 36);
				num = checked(num + 1);
			}
			while (num <= 5);
			return text2.Replace("O", "0").Replace("I", "1");
		};
		NetworkName = $"P{MathUtils.ConvertRadix(ServerPort, 16).PadLeft(4, '0')}-{vB_0024AnonymousDelegate_()}";
		NetworkSecret = vB_0024AnonymousDelegate_();
		DiscoverNodeID = -1;
		Logger.Info($"尝试创建房间，网络名 {NetworkName}，网络密码 {NetworkSecret}，端口 {ServerPort}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		ChangeState(LinkStates.Loading);
	}

	public static void Join()
	{
		string text = ModMain.MyMsgBoxInput("输入邀请码", "输入房主发给你的邀请码。", null, null, string.IsNullOrEmpty(LastCode) ? "" : "使用上一次的邀请码");
		if (!string.IsNullOrEmpty(LastCode) && text != null && Operators.CompareString(text, "", TextCompare: false) == 0)
		{
			text = LastCode;
		}
		if (text != null)
		{
			Join(text);
		}
	}

	public void JoinInternal(string Code)
	{
		if (Code == null)
		{
			return;
		}
		string text = ValidateCodeFormat(Code);
		if (text != null)
		{
			ModMain.Hint(text, ModMain.HintType.Red);
			return;
		}
		Code = FixCodeFormat(Code);
		Logger.Info($"实际使用的邀请码：{Code}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		IsServerSide = false;
		ServerPort = Conversions.ToInteger(MathUtils.ConvertRadix(StringExtensions.Upper(Code.Substring(1, 4)), 16, 10));
		NetworkName = Code.Substring(0, 11);
		NetworkSecret = Code.Substring(12, 5);
		if (Operators.CompareString(Code.Substring(20, 3), "000", TextCompare: false) == 0)
		{
			DiscoverNodeID = -2;
		}
		else
		{
			DiscoverNodeID = Conversions.ToInteger(MathUtils.ConvertRadix(StringExtensions.Upper(Code.Substring(20, 3)), 16, 10));
		}
		Logger.Info($"尝试加入房间，网络名 {NetworkName}，网络密码 {NetworkSecret}，端口 {ServerPort}，发现节点 {DiscoverNodeID}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		LastCode = Code;
		ChangeState(LinkStates.Loading);
	}

	public static string ValidateCodeFormat(string Code)
	{
		if (Code == null)
		{
			return "邀请码为空！";
		}
		Code = FixCodeFormat(Code);
		if (Code.Length < 14 || Code[0] != 'P' || Code[5] != '-' || Code[11] != '-')
		{
			if (StringExtensions.StartsWithF(Code, "U/", false))
			{
				return "请让房主使用 PCL 创建房间！";
			}
			if (Code.Length == 10)
			{
				return "请让房主使用非社区版的 PCL 创建房间！";
			}
			return "邀请码有误，请让房主使用 PCL 创建房间！";
		}
		if (Code.Length >= 23 && Code[17] == '-' && ModBase.Val(Code.Substring(18, 2)) > 2.0)
		{
			return "你的 PCL 版本太老了，请在更新 PCL 之后再联机！";
		}
		return null;
	}

	private static string FixCodeFormat(string Code)
	{
		Code = StringExtensions.Between(StringExtensions.Between(Code, "【", "】", false), "[", "]", false);
		Code = StringExtensions.Upper(Code).Replace("O", "0").Replace("I", "1");
		if (Code.Length >= 17 && (Code.Length < 23 || (Code.Length >= 18 && Code[17] != '-')))
		{
			Code = Code.Substring(0, 17) + "-0105E";
		}
		return Code;
	}

	public static void Join(string Code)
	{
		if (LinkState != 0)
		{
			ModMain.Hint("你已经在联机房间中了！", ModMain.HintType.Red);
			return;
		}
		if (ModMain.FrmMain.PageCurrent == FormMain.PageType.Link)
		{
			ModMain.FrmLinkMain.JoinInternal(Code);
			return;
		}
		AutoJoinCode = Code;
		ModMain.FrmMain.PageChange(FormMain.PageType.Link);
	}

	private void PanSelectJoin_Loaded()
	{
		if (AutoJoinCode != null)
		{
			if (LinkState == LinkStates.Waiting)
			{
				JoinInternal(AutoJoinCode);
			}
			AutoJoinCode = null;
		}
	}

	private void SwitchToLoading(LinkStates OldState)
	{
		Peers = null;
		FailCount = 0;
		LinkLoader.Start(null, IsForceRestart: true);
		UpdateProgressBar(0.0);
		LabLoadTitle.Text = (IsServerSide ? "创建房间中" : "加入房间中");
		UpdateLoadingPage("正在初始化……", "准备初始化");
	}

	private void SwitchToFailed(LinkStates OldState)
	{
		LinkLoader.Cancel();
		ProcessStop();
		UpdateProgressBar(1.0);
		LabLoadTitle.Text = FailReason;
		string text = ExceptionExtensions.GetDisplay(LinkLoader.Error, false);
		Exception ex = LinkLoader.Error;
		if (StringExtensions.StartsWithF(ex.Message, "$", false))
		{
			text = ex.Message;
		}
		while (ex.InnerException != null)
		{
			ex = ex.InnerException;
			if (StringExtensions.StartsWithF(ex.Message, "$", false))
			{
				text = ex.Message;
			}
		}
		LabLoadDesc.Text = (StringExtensions.StartsWithF(text, "$", false) ? text.TrimStart('$') : ExceptionExtensions.GetDisplay(LinkLoader.Error, true));
		Logger.Warn(LinkLoader.Error, LabLoadTitle.Text, (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
	}

	private void UpdateLoadingPage(string Title, string FailBrief)
	{
		if (Operators.CompareString(FailReason, FailBrief + "失败", TextCompare: false) == 0)
		{
			return;
		}
		FailReason = FailBrief + "失败";
		Logger.Info($"开始步骤：{Title}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		ModBase.RunInUiWait([SpecialName] () =>
		{
			if (ModMain.FrmLinkMain != null && ModMain.FrmLinkMain.LabLoadDesc.IsLoaded)
			{
				ModMain.FrmLinkMain.LabLoadDesc.Text = Title;
			}
		});
	}

	private void BtnLoadCancel_Click()
	{
		ChangeState(LinkStates.Waiting);
	}

	private void CardLoad_MouseLeftButtonUp()
	{
		ChangeState(LinkStates.Loading);
	}

	private void UpdateProgressBar(double Value = -1.0)
	{
		if (Value == -1.0)
		{
			Value = LinkLoader.Progress;
		}
		double value = ColumnProgressA.Width.Value;
		if (Math.Round(Value - value, 3) != 0.0)
		{
			if (value <= Value)
			{
				double num = ((Value == 1.0) ? 1.0 : (value + Math.Pow(Value - value, 2.0) * 0.5));
				ModAnimation.AniStart(new ModAnimation.AniData[2]
				{
					ModAnimation.AaGridLengthWidth(ColumnProgressA, num - ColumnProgressA.Width.Value, 200),
					ModAnimation.AaGridLengthWidth(ColumnProgressB, 1.0 - num - ColumnProgressB.Width.Value, 200)
				}, "Link Progress");
			}
			else
			{
				ColumnProgressA.Width = new GridLength(Value, GridUnitType.Star);
				ColumnProgressB.Width = new GridLength(1.0 - Value, GridUnitType.Star);
				ModAnimation.AniStop("Link Progress");
			}
		}
	}

	private void CardLoad_SizeChanged()
	{
		RectProgressClip.Rect = new Rect(0.0, 0.0, CardLoad.ActualWidth, 12.0);
	}

	private void SwitchToFinished(LinkStates OldState)
	{
		UpdatePanelVisibility();
		if (IsServerSide)
		{
			LabFinishTitle.Text = "已创建房间";
			LabFinishDesc.Text = string.Format("把邀请码发给朋友，让大家加入房间吧！{0}邀请码：{1}", "\r\n", GetInviteCode());
			BtnFinishExit.Text = "关闭";
			BtnFinishPing.ToolTip = "网络延迟";
			BtnFinishCopy.Visibility = Visibility.Visible;
			Copy();
			BtnFinishPort.Visibility = Visibility.Visible;
			LabFinishPort.Visibility = Visibility.Visible;
			BtnFinishIp.Visibility = Visibility.Collapsed;
			LabFinishIp.Visibility = Visibility.Collapsed;
			LabFinishPort.Text = Conversions.ToString(ServerPort);
		}
		else
		{
			LabFinishTitle.Text = "已加入房间";
			LabFinishDesc.Text = string.Format("在多人游戏页面的最下方就能找到联机房间！{0}注意：使用离线登录时不要手动输入 IP！", "\r\n");
			BtnFinishExit.Text = "离开";
			BtnFinishPing.ToolTip = "与房主的延迟";
			BtnFinishCopy.Visibility = Visibility.Collapsed;
			BtnFinishPort.Visibility = Visibility.Collapsed;
			LabFinishPort.Visibility = Visibility.Collapsed;
			LabFinishIp.Text = ClientAddress;
			BtnFinishIp.Visibility = Visibility.Visible;
			LabFinishIp.Visibility = Visibility.Visible;
		}
		Update();
	}

	private void BtnFinishExit_Click()
	{
		TryExit(Silent: false, Closing: false);
	}

	public bool TryExit(bool Silent, bool Closing)
	{
		if (LinkState == LinkStates.Waiting || LinkState == LinkStates.Failed)
		{
			return false;
		}
		if (!Silent)
		{
			if (IsServerSide && PeopleCount > 1)
			{
				if (ModMain.MyMsgBox("你确定要关闭联机房间吗？\r\n所有玩家都需要重新输入邀请码才可加入游戏！", "退出联机", "确定", "取消", "", IsWarn: true) == 2)
				{
					return true;
				}
			}
			else if (Closing && ModMain.MyMsgBox(IsServerSide ? "你确定要关闭联机房间吗？" : "你确定要离开联机房间吗？", "退出联机", "确定", "取消") == 2)
			{
				return true;
			}
		}
		ChangeState(LinkStates.Waiting);
		return false;
	}

	private void Copy()
	{
		string text = $"在 PCL 启动器中输入邀请码【{GetInviteCode()}】，即可加入联机房间！";
		ModBase.ClipboardSet(text, ShowSuccessHint: false);
		Settings.Set("LinkLastAutoJoinInviteCode", text);
		ModMain.Hint("已复制邀请码！", ModMain.HintType.Green);
	}

	private string GetInviteCode()
	{
		return $"{NetworkName}-{NetworkSecret}-{2.ToString().PadLeft(2, '0')}{MathUtils.ConvertRadix((DiscoverNodeID != -1) ? DiscoverNodeID : 0, 16).PadLeft(3, '0')}";
	}

	private void BtnFinishIp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		ModBase.ClipboardSet(ClientAddress, ShowSuccessHint: false);
		ModMain.Hint("已复制服务器地址！", ModMain.HintType.Green);
	}

	private void InitConfig(ModLoader.LoaderTask<int, int> Task)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		UpdateLoadingPage("正在联网获取配置……", "联网获取配置");
		ModSecret.ServerLoader.WaitForExit(null, Task);
		if (ModSecret.ServerConfig == null)
		{
			throw new Exception("无法从服务器获取配置");
		}
		JToken obj = ModSecret.ServerConfig["Link"];
		object obj2;
		if (obj == null)
		{
			obj2 = null;
		}
		else
		{
			JToken obj3 = obj[(object)"DisableReason2"];
			obj2 = ((obj3 != null) ? obj3.ToString() : null);
		}
		string text = (string)obj2;
		if (!string.IsNullOrEmpty(text))
		{
			throw new Exception("$" + text);
		}
		if (((JObject)ModSecret.ServerConfig["Link"]).ContainsKey("MinVersionCode") && 412 < ModSecret.ServerConfig["Link"][(object)"MinVersionCode"].ToObject<int>())
		{
			throw new Exception("$你的 PCL 版本太老了，请在更新 PCL 之后再联机！");
		}
	}

	private void InitPrepareDownload(ModLoader.LoaderTask<int, List<ModNet.NetFile>> Task)
	{
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Expected O, but got Unknown
		UpdateLoadingPage("正在获取 CPU 架构……", "获取 CPU 架构");
		string text = Conversions.ToString((int)typeof(string).Assembly.GetName().ProcessorArchitecture);
		string left = text;
		if (Operators.CompareString(left, Conversions.ToString(2), TextCompare: false) == 0)
		{
			text = "i686";
		}
		else if (Operators.CompareString(left, Conversions.ToString(4), TextCompare: false) == 0)
		{
			text = "x86_64";
		}
		else if (Operators.CompareString(left, Conversions.ToString(5), TextCompare: false) == 0)
		{
			text = "arm64";
		}
		else
		{
			Logger.Warn($"CPU 是不支持的 {text} 架构，这可能会导致联机模块无法启动！", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			text = "arm64";
		}
		Logger.Info($"CPU 架构：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		ModMain.Telemetry("联机开始");
		UpdateLoadingPage("正在检查联机模块版本……", "检查联机模块版本");
		if (!FileUtils.Exists(PathEasyTier + "联机模块 CLI.exe") || !FileUtils.Exists(PathEasyTier + "联机模块.exe") || !FileUtils.Exists(PathEasyTier + "Packet.dll"))
		{
			Settings.Set("LinkEasyTierVersion", -1);
		}
		int num = Settings.Get<int>("LinkEasyTierVersion");
		ServerVersion = (int)ModSecret.ServerConfig["Link"][(object)"EasyTierVersion"];
		List<ModNet.NetFile> list = new List<ModNet.NetFile>();
		Logger.Info($"EasyTier 本地版本：{num}，需求版本：{ServerVersion}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		if (num < ServerVersion)
		{
			list.Add(new ModNet.NetFile(((IEnumerable<JToken>)ModSecret.ServerConfig["Link"][(object)"Downloads"]).Select([SpecialName] (JToken UrlEntry) => UrlEntry.ToString().Replace("{arch}", text)), PathEasyTier + "EasyTier.zip", new FileChecker
			{
				MinSize = 2097152L
			}));
		}
		UpdateLoadingPage("正在下载联机模块……", "下载联机模块");
		Task.Output = list;
	}

	private void InitLaunch(ModLoader.LoaderTask<int, int> Task)
	{
		UpdateLoadingPage("正在解压联机模块……", "解压联机模块");
		if (FileUtils.Exists(PathEasyTier + "EasyTier.zip"))
		{
			DirectoryUtils.Copy(DirectoryUtils.EnumerateDirectories(ModMain.RequestTaskTempFolder(), true, "*").First(), PathEasyTier);
			FileUtils.Move(PathEasyTier + "easytier-core.exe", PathEasyTier + "联机模块.exe", false);
			FileUtils.Move(PathEasyTier + "easytier-cli.exe", PathEasyTier + "联机模块 CLI.exe", false);
			FileUtils.Delete(PathEasyTier + "EasyTier.zip", false);
			Settings.Set("LinkEasyTierVersion", ServerVersion);
		}
		Task.Progress = 0.07;
		UpdateLoadingPage("正在获取节点列表……", "获取节点列表");
		string text = Settings.Get<string>("LinkCustomPeer");
		List<string> list;
		if (string.IsNullOrWhiteSpace(text))
		{
			if (DiscoverNodeID == -2 && !IsServerSide)
			{
				Panic("未填写自定义节点设置", string.Format("$你必须在 {0}自定义节点{1} 设置中填写与房主相同的内容，{2}才能进入该房间！", ModBase.vbLQ, ModBase.vbRQ, "\r\n"));
				return;
			}
			list = GetTargetPeers();
		}
		else
		{
			if (DiscoverNodeID != -2 && !IsServerSide)
			{
				ModMain.Hint("房主可能没有使用自定义节点设置，请确认你们的自定义节点设置是否一致！");
			}
			list = (from p in text.Split("，,".ToCharArray())
				select p.Trim() into p
				where !string.IsNullOrEmpty(p)
				select p).ToList();
			Logger.Info($"使用自定义节点：{text}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		}
		Task.Progress = 0.13;
		UpdateLoadingPage("正在启动联机模块……", "启动联机模块");
		List<int> list2 = ModNet.FindFreePorts(5, ServerPort);
		ClientPort = list2[0];
		RPCPort = list2[1];
		int num = list2[2];
		string text2 = (string)ModSecret.ServerConfig["Link"][(object)"Argument"];
		text2 += $" --network-name={NetworkName} --network-secret={NetworkSecret} --listeners {num} --rpc-portal {RPCPort} --private-mode true";
		string arg = (IsServerSide ? "Server-" : "Client-") + StringExtensions.EnsureLength(MathUtils.ConvertRadix(StringExtensions.GetStableHashCode(ModBase.Identify), 36).TrimStart('-'), '0', 6);
		if (IsServerSide)
		{
			text2 += $" -i 10.114.114.114 --hostname={arg} --tcp-whitelist={ServerPort} --udp-whitelist={ServerPort}";
		}
		else
		{
			text2 += $" -d --hostname={arg} --tcp-whitelist=0 --udp-whitelist=0";
			ClientAddress = $"localhost:{ClientPort}";
			text2 += $" --port-forward tcp://[{IPAddress.IPv6Loopback}]:{ClientPort}/10.114.114.114:{ServerPort}";
			text2 += $" --port-forward udp://[{IPAddress.IPv6Loopback}]:{ClientPort}/10.114.114.114:{ServerPort}";
			text2 += $" --port-forward tcp://{IPAddress.Loopback}:{ClientPort}/10.114.114.114:{ServerPort}";
			text2 += $" --port-forward udp://{IPAddress.Loopback}:{ClientPort}/10.114.114.114:{ServerPort}";
		}
		foreach (string item in list)
		{
			text2 += $" -p=\"{item}\"";
		}
		if (Settings.Get<int>("LinkLatencyMode") == 1)
		{
			text2 += " --latency-first";
		}
		ProcessStart(text2);
		Task.Progress = 0.15;
		int num2 = 0;
		do
		{
			Thread.Sleep(500);
			CheckCrash();
			RefreshPeerLoader.WaitForExit(null, null, IsForceRestart: true);
			double peerPing = GetPeerPing();
			if (peerPing > 0.0)
			{
				Logger.Info($"已与目标建立连接，当前 Ping 为 {peerPing:0.0}ms", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				ModMain.Telemetry("联机成功", "Server", Conversions.ToString(IsServerSide), "NAT", Conversions.ToString((int)NATType));
				break;
			}
			double progress = Task.Progress;
			int num3 = ((Peers == null) ? (-1) : Peers.Where([SpecialName] (Peer p) => p.Ping > 0.0).Count());
			switch (num3)
			{
			case -1:
				Task.Progress = MathUtils.Clamp<double>(Task.Progress + 0.02, 0.15, 0.25);
				break;
			case 0:
				UpdateLoadingPage("正在连接到节点……", "连接节点");
				Task.Progress = MathUtils.Clamp<double>(Task.Progress + 0.02, IsServerSide ? 0.5 : 0.3, IsServerSide ? 0.95 : 0.5);
				break;
			default:
				UpdateLoadingPage("正在连接到房主……", "连接房主");
				Task.Progress = MathUtils.Clamp<double>(Task.Progress + 0.02, Math.Min(0.45 + (double)num3 * 0.05, 0.65), 0.95);
				break;
			}
			if (progress != Task.Progress)
			{
				num2 = 0;
				continue;
			}
			if (num2 < 60)
			{
				num2 = checked(num2 + 1);
				continue;
			}
			switch (num3)
			{
			case -1:
				Panic("无法启动联机模块", "近期日志：\r\n" + EnumerableExtensions.Join((IEnumerable)LogHistory, "\r\n"));
				break;
			case 0:
				Panic("无法连接到节点", "请检查你的网络环境是否良好。");
				break;
			default:
				Panic("无法连接到房主", string.Format("可能的原因：{0}- 你或者房主的网络环境不佳{1}- 房主已关闭房间{2}- 邀请码输错了", "\r\n", "\r\n", "\r\n"));
				break;
			}
		}
		while (!Task.IsCanceled);
		if (Task.IsCanceled)
		{
			throw new OperationCanceledException();
		}
		if (IsServerSide)
		{
			return;
		}
		UpdateLoadingPage("连接优化中……", "优化连接");
		Task.Progress = 0.999;
		int num4 = 1;
		do
		{
			Peer targetPeer = GetTargetPeer();
			if (targetPeer == null || targetPeer.Relay || !(targetPeer.Ping < 100.0))
			{
				if (Task.IsCanceled)
				{
					throw new OperationCanceledException();
				}
				Thread.Sleep(100);
				num4 = checked(num4 + 1);
				continue;
			}
			break;
		}
		while (num4 <= 50);
	}

	private List<string> GetTargetPeers()
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Expected O, but got Unknown
		List<string> list = new List<string>();
		int num = -1;
		int num2 = ModSecret.ServerConfig["Link"][(object)"DiscoverPeerId"].ToObject<int>();
		string text = ModSecret.ServerConfig["Link"][(object)"DiscoverPeer"].ToString();
		try
		{
			bool flag = false;
			JObject val;
			try
			{
				val = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClient("https://uptime.easytier.cn/api/nodes?page=1&per_page=1000", null, null, null, "*/*", 25000, null, RequireJson: true));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "从源站获取节点列表失败，将使用 CDN 缓存", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				val = (JObject)StringExtensions.DeserializeJson(ModNet.NetRequestByClientRetry("https://easytier.meloong.com/?page=1&per_page=1000", null, null, null, null, "*/*", null, RequireJson: true));
				flag = true;
				ProjectData.ClearProjectError();
			}
			List<JObject> list2 = new List<JObject>();
			List<string> list3 = ((IEnumerable<JToken>)ModSecret.ServerConfig["Link"][(object)"PeersBlackList"]).Select([SpecialName] (JToken p) => p.ToString()).ToList();
			foreach (JObject item in (IEnumerable<JToken>)val["data"][(object)"items"])
			{
				JObject val2 = item;
				int num3 = val2["id"].ToObject<int>();
				if (!val2["is_active"].ToObject<bool>() || !val2["is_approved"].ToObject<bool>())
				{
					continue;
				}
				if (num3 != DiscoverNodeID)
				{
					if (num3 == num2 || list3.Contains(val2["address"].ToString()) || (val2["usage_percentage"].ToObject<double>() == 0.0 && ModBase.RandomInteger(1, 100) != 1))
					{
						continue;
					}
					List<string> list4 = ((IEnumerable<JToken>)val2["tags"]).Select([SpecialName] (JToken t) => t.ToString()).ToList();
					if (!list4.Contains("国内") || !list4.Contains("MC中继"))
					{
						continue;
					}
				}
				double num4 = val2["usage_percentage"].ToObject<double>();
				num4 *= 110.0 - val2["health_percentage_24h"].ToObject<double>();
				val2["load"] = JToken.op_Implicit(num4);
				list2.Add(val2);
			}
			list2 = (flag ? ModBase.Shuffle(list2).ToList() : list2.OrderBy([SpecialName] (JObject n) => n["load"].ToObject<double>()).ToList());
			JObject val3 = null;
			if (DiscoverNodeID == -1)
			{
				val3 = list2.FirstOrDefault([SpecialName] (JObject n) => !n["allow_relay"].ToObject<bool>());
			}
			else if (DiscoverNodeID > 0)
			{
				val3 = list2.FirstOrDefault([SpecialName] (JObject n) => n["id"].ToObject<int>() == DiscoverNodeID);
				if (val3 == null)
				{
					Logger.Warn($"未找到 ID {DiscoverNodeID} 的发现节点", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
					Panic("房间已过期", "请让房主重新创建房间！");
					throw new OperationCanceledException();
				}
			}
			if (val3 == null)
			{
				JObject val4 = new JObject();
				val4.Add("address", JToken.op_Implicit(text));
				val4.Add("id", JToken.op_Implicit(num2));
				val3 = val4;
				Logger.Warn("将使用回退发现节点", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			}
			list.Add(val3["address"].ToString());
			num = val3["id"].ToObject<int>();
			Logger.Info(string.Format("发现节点：{0} (ID: {1})", val3["address"], num), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			if (ModBase.ModeDebug || !IsServerSide)
			{
				int num5 = ModSecret.ServerConfig["Link"][(object)"RandomPeer"].ToObject<int>();
				List<JObject> list5 = list2.Where([SpecialName] (JObject n) => n["allow_relay"].ToObject<bool>()).ToList();
				if (list5.Count < num5)
				{
					throw new Exception($"可用的中继节点数量不足，需要 {num5} 个，实际 {list5.Count} 个");
				}
				list.AddRange(from n in list5.Take(num5)
					select n["address"].ToString());
			}
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			ExceptionExtensions.ThrowIfCanceled(ex4);
			Logger.Error(ex4, "获取节点列表失败，联机质量可能受到影响", (LogBehavior)2, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			list.AddRange(((IEnumerable<JToken>)ModSecret.ServerConfig["Link"][(object)"Peers"]).Select([SpecialName] (JToken p) => p.ToString()));
			if (num <= 0)
			{
				list.Add(text);
				num = num2;
			}
			ProjectData.ClearProjectError();
		}
		if (((JObject)ModSecret.ServerConfig["Link"]).ContainsKey("EnableForcedPeer"))
		{
			list.Add("tcp://mc1.easytier.cn:55558");
			if (num <= 0)
			{
				num = 94;
			}
		}
		list.AddRange(((IEnumerable<JToken>)ModSecret.ServerConfig["Link"][(object)"MandatoryPeers"]).Select([SpecialName] (JToken p) => p.ToString()));
		DiscoverNodeID = num;
		return list.Distinct().ToList();
	}

	public static void ProcessStop()
	{
		try
		{
			Process[] processes = Process.GetProcesses();
			foreach (Process process in processes)
			{
				int id = process.Id;
				int? num = ProcessCore?.Id;
				if (!(num.HasValue ? new bool?(id == num.GetValueOrDefault()) : null).GetValueOrDefault() && (Operators.CompareString(process.ProcessName, "联机模块", TextCompare: false) == 0 || Operators.CompareString(process.ProcessName, "联机模块 CLI", TextCompare: false) == 0))
				{
					try
					{
						Logger.Info($"停止残留的联机模块，PID：{process.Id}，进程名：{process.ProcessName}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
						process.Kill();
						process.Close();
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, $"结束进程失败（{process.ProcessName}，PID {process.Id}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
						ProjectData.ClearProjectError();
					}
				}
			}
			if (ProcessCore != null && !ProcessCore.HasExited)
			{
				Logger.Info($"停止所启动的联机模块，PID：{ProcessCore.Id}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				ProcessCore.Kill();
				ProcessCore.Close();
				ProcessCore = null;
			}
			ProcessOutputHandle?.Dispose();
			ProcessErrorHandle?.Dispose();
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Warn(ex4, "停止联机模块失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			ProjectData.ClearProjectError();
		}
	}

	private void Panic(string Brief, string Detail)
	{
		if (Detail.Contains("failed to listen on "))
		{
			Detail = string.Format("监听端口失败。{0}请点击重试，如果还是出现此错误，可以重启电脑解决。{1}{2}{3}", "\r\n", "\r\n", "\r\n", Detail);
		}
		if (LinkState == LinkStates.Loading)
		{
			FailReason = Brief;
			LinkLoader.Failed(new Exception(Detail));
			return;
		}
		ModMain.MyMsgBox(Detail, "联机出错：" + Brief, "确定", "", "", IsWarn: true);
		ChangeState(LinkStates.Waiting);
		ModMain.Telemetry("联机失败", "Exception", ModMinecraft.FilterUserName(Brief + "：" + Detail, '*'));
	}

	private void ProcessStart(string Arguments)
	{
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			FileName = PathEasyTier + "联机模块.exe",
			Arguments = Arguments,
			UseShellExecute = false,
			CreateNoWindow = true,
			RedirectStandardError = true,
			RedirectStandardOutput = true,
			StandardOutputEncoding = Encoding.UTF8,
			StandardErrorEncoding = Encoding.UTF8
		};
		Logger.Info($"正在启动 EasyTier：{Arguments}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		ProcessCore = new Process
		{
			StartInfo = startInfo
		};
		VB_0024AnonymousDelegate_13<object, DataReceivedEventArgs, AutoResetEvent> vB_0024AnonymousDelegate_ = [SpecialName] (object sender, DataReceivedEventArgs e, AutoResetEvent Handle) =>
		{
			try
			{
				if (e.Data == null)
				{
					Handle.Set();
				}
				else
				{
					ProcessLogLine(e.Data);
				}
			}
			catch (ObjectDisposedException ex)
			{
				ProjectData.SetProjectError(ex);
				ObjectDisposedException ex2 = ex;
				ProjectData.ClearProjectError();
			}
			catch (Exception ex3)
			{
				ProjectData.SetProjectError(ex3);
				Exception ex4 = ex3;
				Logger.Warn(ex4, "读取 EasyTier 信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				ProjectData.ClearProjectError();
			}
		};
		ProcessOutputHandle = new AutoResetEvent(initialState: false);
		ProcessCore.OutputDataReceived += [SpecialName] (object sender, DataReceivedEventArgs e) =>
		{
			vB_0024AnonymousDelegate_(RuntimeHelpers.GetObjectValue(sender), e, ProcessOutputHandle);
		};
		ProcessErrorHandle = new AutoResetEvent(initialState: false);
		ProcessCore.ErrorDataReceived += [SpecialName] (object sender, DataReceivedEventArgs e) =>
		{
			vB_0024AnonymousDelegate_(RuntimeHelpers.GetObjectValue(sender), e, ProcessErrorHandle);
		};
		ProcessCore.Start();
		ProcessCore.BeginOutputReadLine();
		ProcessCore.BeginErrorReadLine();
	}

	private void ProcessLogLine(string Line)
	{
		Logger.Info(Line, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
		LogHistory.Enqueue(Line);
		if (LogHistory.Count >= 10)
		{
			LogHistory.Dequeue();
		}
		if (StringExtensions.ContainsIgnoreCase(Line, "new peer connection added"))
		{
			Logger.Warn(string.Format("已建立连接：{0}", StringExtensions.RegexSeek(Line, "(?<=remote_addr.+?\")[^\"}]{3,}", RegexOptions.None) ?? Line), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			Update();
		}
		else if (StringExtensions.ContainsIgnoreCase(Line, "peer connection removed"))
		{
			Logger.Warn(string.Format("已断开连接：{0}", StringExtensions.RegexSeek(Line, "(?<=remote_addr.+?\")[^\"}]{3,}", RegexOptions.None) ?? Line), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			Update();
		}
	}

	private void RefreshPeer()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			string text = "";
			if (!text.Contains("lat_ms"))
			{
				throw new Exception("CLI 调用失败：\r\n" + text);
			}
			if (ModBase.GetUuid() % (ModBase.ModeDebug ? 23 : 103) == 0)
			{
				Logger.Info(string.Format("CLI 输出抽样：{0}{1}", "\r\n", text), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			}
			List<Peer> list = new List<Peer>();
			foreach (JObject item in (JArray)StringExtensions.DeserializeJson(text))
			{
				JObject val = item;
				try
				{
					Peer peer = new Peer(val);
					if (peer.Type == Peer.Types.Self)
					{
						NATType = peer.NATType;
					}
					else
					{
						list.Add(peer);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, $"错误的信息（{val}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
					ProjectData.ClearProjectError();
				}
			}
			Peers = list;
			FailCount = 0;
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			Logger.Warn(ex4, "获取节点信息失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
			checked
			{
				if (LinkState == LinkStates.Finished)
				{
					FailCount++;
					if (FailCount >= 4)
					{
						Panic("获取节点信息失败", ex4.Message);
					}
				}
				ProjectData.ClearProjectError();
			}
		}
	}

	private double GetPeerPing()
	{
		return GetTargetPeer()?.Ping ?? 0.0;
	}

	private Peer GetTargetPeer()
	{
		if (Peers == null)
		{
			return null;
		}
		IEnumerable<Peer> source = ((!IsServerSide) ? Peers.Where([SpecialName] (Peer p) => p.Type == Peer.Types.Server && p.Ping > 0.0) : Peers.Where([SpecialName] (Peer p) => p.Ping > 0.0));
		if (!source.Any())
		{
			return null;
		}
		double num = source.Min([SpecialName] (Peer p) => p.Ping);
		return source.First([SpecialName] (Peer p) => p.Ping == num);
	}

	private void StartTimerThread()
	{
		if (_0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted_0024Init == null)
		{
			Interlocked.CompareExchange(ref _0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted_0024Init, new StaticLocalInitFlag(), null);
		}
		bool lockTaken = false;
		try
		{
			Monitor.Enter(_0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted_0024Init, ref lockTaken);
			if (_0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted_0024Init.State == 0)
			{
				_0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted_0024Init.State = 2;
				_0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted = false;
			}
			else if (_0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted_0024Init.State == 2)
			{
				throw new IncompleteInitialization();
			}
		}
		finally
		{
			_0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted_0024Init.State = 1;
			if (lockTaken)
			{
				Monitor.Exit(_0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted_0024Init);
			}
		}
		if (_0024STATIC_0024StartTimerThread_00242001_0024IsTimerStarted)
		{
			return;
		}
		ModBase.RunInNewThread([SpecialName] () =>
		{
			int num = 0;
			while (true)
			{
				try
				{
					Thread.Sleep(200);
					num = checked(num + 200);
					if (num >= 1000)
					{
						num = 0;
						Update();
					}
					if (LinkState == LinkStates.Loading)
					{
						ModBase.RunInUi([SpecialName] () =>
						{
							UpdateProgressBar();
						});
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "联机模块主时钟出错", (LogBehavior)4, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
					Thread.Sleep(10000);
					ProjectData.ClearProjectError();
				}
			}
		}, "Link Timer");
	}

	private void Update()
	{
		if (LinkState != LinkStates.Finished)
		{
			return;
		}
		object lockState = RefreshPeerLoader.LockState;
		ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
		bool lockTaken = false;
		try
		{
			Monitor.Enter(lockState, ref lockTaken);
			if (RefreshPeerLoader.State != ModBase.LoadState.Loading)
			{
				RefreshPeerLoader.Start(null, IsForceRestart: true);
			}
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(lockState);
			}
		}
		if (ModMain.FrmMain.PageCurrent == FormMain.PageType.Link)
		{
			ModBase.RunInUi([SpecialName] () =>
			{
				ModAnimation.AniStart(ModAnimation.AaRotateTransform(ImgFinishLogo, 500.0, 5000), "Link Logo Rotation");
				double peerPing = GetPeerPing();
				bool flag = peerPing % 500.0 == 0.0 || Operators.CompareString($"{peerPing:0.0}", "1.0", TextCompare: false) == 0 || FailCount > 0;
				if (flag)
				{
					LabFinishPing.Text = "连接优化中";
				}
				else
				{
					LabFinishPing.Text = ((peerPing >= 10.0) ? $"{peerPing:0}\u200ams" : $"{peerPing:0.0}\u200ams");
				}
				LabFinishPlayer.Text = Conversions.ToString(PeopleCount) + "\u200a\u200a人";
				HintFinish.Visibility = Visibility.Collapsed;
				if (!IsServerSide && !flag)
				{
					Peer targetPeer = GetTargetPeer();
					if (targetPeer != null)
					{
						if (targetPeer.Relay)
						{
							if (NATType >= NATTypes.Symmetric && targetPeer.NATType >= NATTypes.Symmetric)
							{
								HintFinish.Text = "你和房主的网络环境都不太好，";
							}
							else if (NATType >= NATTypes.Symmetric)
							{
								HintFinish.Text = "你的网络环境不太好，";
							}
							else if (targetPeer.NATType >= NATTypes.Symmetric)
							{
								HintFinish.Text = "房主的网络环境不太好，";
							}
							else
							{
								HintFinish.Text = "你或者房主的网络环境不太好，";
							}
							if (string.IsNullOrWhiteSpace(Settings.Get<string>("LinkCustomPeer")))
							{
								HintFinish.Text += "正使用社区节点进行中继。";
							}
							else
							{
								HintFinish.Text += "正通过自定义节点进行中继。";
							}
							HintFinish.Visibility = Visibility.Visible;
							HintFinish.Theme = MyHint.Themes.Yellow;
						}
						else if (NATType < NATTypes.PortRestricted && targetPeer.NATType >= NATTypes.PortRestricted)
						{
							List<Peer> source = Peers.Where([SpecialName] (Peer p) => p.Type == Peer.Types.Client).ToList();
							if (source.Any() && !source.Any([SpecialName] (Peer p) => p.Relay) && !source.All([SpecialName] (Peer p) => p.NATType < NATTypes.PortRestricted))
							{
								HintFinish.Visibility = Visibility.Visible;
								HintFinish.Theme = MyHint.Themes.Blue;
								HintFinish.Text = "你的网络环境比房主更好！如果你来当房主，其他玩家或许能更加流畅！";
							}
						}
					}
				}
			});
		}
		CheckCrash();
		if (!Peers.Any())
		{
			Panic("网络连接已断开", "请检查你的网络环境是否正常。");
		}
		else if (!IsServerSide && !Peers.Any([SpecialName] (Peer p) => p.Type == Peer.Types.Server))
		{
			ModMain.MyMsgBox("房主已离开房间！", "联机结束");
			ChangeState(LinkStates.Waiting);
		}
		else
		{
			if (IsServerSide)
			{
				return;
			}
			try
			{
				if (_0024STATIC_0024Update_00242001_0024BroadcastSocket_0024Init == null)
				{
					Interlocked.CompareExchange(ref _0024STATIC_0024Update_00242001_0024BroadcastSocket_0024Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken2 = false;
				try
				{
					Monitor.Enter(_0024STATIC_0024Update_00242001_0024BroadcastSocket_0024Init, ref lockTaken2);
					if (_0024STATIC_0024Update_00242001_0024BroadcastSocket_0024Init.State == 0)
					{
						_0024STATIC_0024Update_00242001_0024BroadcastSocket_0024Init.State = 2;
						_0024STATIC_0024Update_00242001_0024BroadcastSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
					}
					else if (_0024STATIC_0024Update_00242001_0024BroadcastSocket_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024Update_00242001_0024BroadcastSocket_0024Init.State = 1;
					if (lockTaken2)
					{
						Monitor.Exit(_0024STATIC_0024Update_00242001_0024BroadcastSocket_0024Init);
					}
				}
				_0024STATIC_0024Update_00242001_0024BroadcastSocket.SendTo(Encoding.UTF8.GetBytes($"[MOTD]PCL 联机房间[/MOTD][AD]{ClientPort}[/AD]"), SocketFlags.None, new IPEndPoint(IPAddress.Loopback, 4445));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, "广播联机房间端口失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Pages\\PageLink\\PageLinkMain.xaml.vb");
				ProjectData.ClearProjectError();
			}
		}
	}

	private void CheckCrash()
	{
		if (ProcessCore == null || ProcessCore.HasExited)
		{
			Panic("联机模块已崩溃", "近期日志：\r\n" + EnumerableExtensions.Join((IEnumerable)LogHistory, "\r\n"));
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Plain Craft Launcher 2;component/pages/pagelink/pagelinkmain.xaml", UriKind.Relative);
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
			PanLoad = (Grid)target;
			break;
		case 2:
			CardLoad = (MyCard)target;
			break;
		case 3:
			Load = (MyLoading)target;
			break;
		case 4:
			LabLoadTitle = (TextBlock)target;
			break;
		case 5:
			LabLoadDesc = (TextBlock)target;
			break;
		case 6:
			BtnLoadCancel = (MyIconButton)target;
			break;
		case 7:
			ColumnProgressA = (ColumnDefinition)target;
			break;
		case 8:
			ColumnProgressB = (ColumnDefinition)target;
			break;
		case 9:
			RectProgressClip = (RectangleGeometry)target;
			break;
		case 10:
			PanContent = (Grid)target;
			break;
		case 11:
			PanSelect = (Grid)target;
			break;
		case 12:
			PanSelectCreate = (MyCard)target;
			break;
		case 13:
			PanSelectJoin = (MyCard)target;
			break;
		case 14:
			PanFinish = (Grid)target;
			break;
		case 15:
			HintFinish = (MyHint)target;
			break;
		case 16:
			ImgFinishLogo = (Path)target;
			break;
		case 17:
			LabFinishTitle = (TextBlock)target;
			break;
		case 18:
			LabFinishDesc = (TextBlock)target;
			break;
		case 19:
			BtnFinishCopy = (MyButton)target;
			break;
		case 20:
			BtnFinishExit = (MyButton)target;
			break;
		case 21:
			BtnFinishPing = (StackPanel)target;
			break;
		case 22:
			LabFinishPing = (TextBlock)target;
			break;
		case 23:
			BtnFinishPlayer = (StackPanel)target;
			break;
		case 24:
			LabFinishPlayer = (TextBlock)target;
			break;
		case 25:
			LineFinishPort = (TextBlock)target;
			break;
		case 26:
			BtnFinishPort = (StackPanel)target;
			break;
		case 27:
			LabFinishPort = (TextBlock)target;
			break;
		case 28:
			BtnFinishIp = (StackPanel)target;
			break;
		case 29:
			LabFinishIp = (TextBlock)target;
			break;
		case 30:
			PanAlways = (Grid)target;
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
