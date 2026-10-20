using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CacheCow.Client;
using CacheCow.Client.FileCacheStore;
using CacheCow.Common;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using ThrottleDebounce;

namespace PCL;

[StandardModule]
public sealed class ModNet
{
	public class NetSource
	{
		public int Id;

		public string Url;

		public int FailCount;

		public Exception Ex;

		public NetThread SingleThread;

		public bool IsFailed;

		public override string ToString()
		{
			return Url;
		}
	}

	public enum NetState
	{
		WaitingToCheck = -1,
		WaitingToDownload,
		Connecting,
		Reading,
		Downloading,
		Merging,
		Finished,
		Canceled
	}

	public enum NetPreDownloadBehaviour
	{
		HintWhileExists,
		ExitWhileExistsOrDownloading,
		IgnoreCheck
	}

	public class NetThread : IEnumerable<NetThread>, IEquatable<NetThread>
	{
		[CompilerGenerated]
		private sealed class VB_0024StateMachine_5_get_Next : IEnumerable<NetThread>, IEnumerable, IEnumerator<NetThread>, IDisposable, IEnumerator
		{
			public int _0024State;

			public NetThread _0024Current;

			public int _0024InitialThreadId;

			internal NetThread _0024VB_0024Me;

			internal NetThread _0024VB_0024ResumableLocal_CurrentChain_00240;

			private NetThread Current => _0024Current;

			object IEnumerator.Current => _0024Current;

			public VB_0024StateMachine_5_get_Next(int _0024State)
			{
				this._0024State = _0024State;
				_0024InitialThreadId = Environment.CurrentManagedThreadId;
			}

			private void Dispose()
			{
				_0024State = -2;
			}

			void IDisposable.Dispose()
			{
				//ILSpy generated this explicit interface implementation from .override directive in Dispose
				this.Dispose();
			}

			[CompilerGenerated]
			private bool MoveNext()
			{
				switch (_0024State)
				{
				default:
					return false;
				case 0:
				{
					int num = (_0024State = -1);
					_0024VB_0024ResumableLocal_CurrentChain_00240 = _0024VB_0024Me;
					break;
				}
				case 1:
				{
					int num = (_0024State = -1);
					_0024VB_0024ResumableLocal_CurrentChain_00240 = _0024VB_0024ResumableLocal_CurrentChain_00240.NextThread;
					break;
				}
				}
				if ((object)_0024VB_0024ResumableLocal_CurrentChain_00240 != null)
				{
					_0024Current = _0024VB_0024ResumableLocal_CurrentChain_00240;
					int num = (_0024State = 1);
					return true;
				}
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			private IEnumerator<NetThread> GetEnumerator()
			{
				VB_0024StateMachine_5_get_Next result;
				if (_0024State == -2 && _0024InitialThreadId == Environment.CurrentManagedThreadId)
				{
					_0024State = 0;
					result = this;
				}
				else
				{
					result = new VB_0024StateMachine_5_get_Next(0)
					{
						_0024VB_0024Me = _0024VB_0024Me
					};
				}
				return result;
			}

			IEnumerator<NetThread> IEnumerable<NetThread>.GetEnumerator()
			{
				//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
				return this.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			private void Reset()
			{
				throw new NotSupportedException();
			}

			void IEnumerator.Reset()
			{
				//ILSpy generated this explicit interface implementation from .override directive in Reset
				this.Reset();
			}
		}

		public NetFile Task;

		public Thread Thread;

		public NetThread NextThread;

		public int Uuid;

		public string Temp;

		public long DownloadStart;

		public long DownloadDone;

		private long SpeedLastTime;

		private long SpeedLastDone;

		private long _Speed;

		public long InitTime;

		public long LastReceiveTime;

		public NetState State;

		public NetSource Source;

		private IEnumerable<NetThread> Next => new VB_0024StateMachine_5_get_Next(-2)
		{
			_0024VB_0024Me = this
		};

		public bool IsFirstThread
		{
			get
			{
				if (DownloadStart == 0L)
				{
					return Task.FileSize == -2;
				}
				return false;
			}
		}

		public long DownloadEnd
		{
			get
			{
				object lockChain = Task.LockChain;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockChain);
				bool lockTaken = false;
				checked
				{
					try
					{
						Monitor.Enter(lockChain, ref lockTaken);
						if ((object)NextThread == null)
						{
							if (Task.IsUnknownSize)
							{
								return 1000000000000L;
							}
							return Task.FileSize - 1;
						}
						return NextThread.DownloadStart - 1;
					}
					finally
					{
						if (lockTaken)
						{
							Monitor.Exit(lockChain);
						}
					}
				}
			}
		}

		public long DownloadUndone => checked(DownloadEnd - (DownloadStart + DownloadDone) + 1);

		public long Speed
		{
			get
			{
				checked
				{
					if (ModBase.GetTimeMs() - SpeedLastTime > 200)
					{
						long num = ModBase.GetTimeMs() - SpeedLastTime;
						_Speed = (long)Math.Round((double)(DownloadDone - SpeedLastDone) / ((double)num / 1000.0));
						SpeedLastDone = DownloadDone;
						SpeedLastTime += num;
					}
					return _Speed;
				}
			}
		}

		public bool IsEnded
		{
			get
			{
				if (State != NetState.Finished)
				{
					return State == NetState.Canceled;
				}
				return true;
			}
		}

		public NetThread()
		{
			DownloadDone = 0L;
			SpeedLastTime = ModBase.GetTimeMs();
			SpeedLastDone = 0L;
			_Speed = 0L;
			InitTime = ModBase.GetTimeMs();
			LastReceiveTime = -1L;
			State = NetState.WaitingToDownload;
		}

		public IEnumerator<NetThread> GetEnumerator()
		{
			return Next.GetEnumerator();
		}

		IEnumerator<NetThread> IEnumerable<NetThread>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
			return this.GetEnumerator();
		}

		private IEnumerator IEnumerable_GetEnumerator()
		{
			return Next.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in IEnumerable_GetEnumerator
			return this.IEnumerable_GetEnumerator();
		}

		public bool Equals(NetThread other)
		{
			if ((object)other != null)
			{
				return Uuid == other.Uuid;
			}
			return false;
		}

		bool IEquatable<NetThread>.Equals(NetThread other)
		{
			//ILSpy generated this explicit interface implementation from .override directive in Equals
			return this.Equals(other);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as NetThread);
		}

		public static bool operator ==(NetThread left, NetThread right)
		{
			return EqualityComparer<NetThread>.Default.Equals(left, right);
		}

		public static bool operator !=(NetThread left, NetThread right)
		{
			return !(left == right);
		}
	}

	public class NetFile
	{
		[CompilerGenerated]
		internal sealed class _Closure_0024__40_002D0
		{
			public HttpResponseMessage _0024VB_0024Local_Response;

			public NetThread _0024VB_0024Local_Th;

			public NetFile _0024VB_0024Me;

			public _Closure_0024__40_002D0(_Closure_0024__40_002D0 arg0)
			{
				if (arg0 != null)
				{
					_0024VB_0024Local_Response = arg0._0024VB_0024Local_Response;
					_0024VB_0024Local_Th = arg0._0024VB_0024Local_Th;
				}
			}

			[SpecialName]
			internal string _Lambda_0024__0()
			{
				return $"{_0024VB_0024Me.LocalName}：重定向至 {_0024VB_0024Local_Response.RequestMessage.RequestUri}";
			}

			[SpecialName]
			internal string _Lambda_0024__1()
			{
				return $"{_0024VB_0024Me.LocalName}：完成，已下载 {_0024VB_0024Local_Th.DownloadDone}（{_0024VB_0024Local_Th.DownloadStart}~{_0024VB_0024Local_Th.DownloadEnd}）";
			}
		}

		public ConcurrentList<LoaderDownload> Tasks;

		public List<NetSource> Sources;

		private int FirstThreadSource;

		public ConcurrentList<NetSource> SourcesOnce;

		private bool Retried;

		public string LocalPath;

		public string LocalName;

		public NetState State;

		public List<Exception> Ex;

		public NetThread Threads;

		public long FileSize;

		public bool IsUnknownSize;

		private MemoryStream Cache;

		public long DownloadDone;

		public FileChecker Check;

		public bool SimulateBrowserHeaders;

		private long SpeedLastTime;

		private long SpeedLastDone;

		private long _Speed;

		public bool IsCopy;

		private int ConnectCount;

		private long ConnectTime;

		private const long FilePieceLimit = 262144L;

		public readonly object LockState;

		public readonly object LockChain;

		public readonly object LockSource;

		public readonly int Uuid;

		public bool IsNoSplit
		{
			get
			{
				if (!IsUnknownSize)
				{
					return FileSize < 1048576;
				}
				return true;
			}
		}

		public long Speed
		{
			get
			{
				checked
				{
					if (ModBase.GetTimeMs() - SpeedLastTime > 200)
					{
						long num = ModBase.GetTimeMs() - SpeedLastTime;
						_Speed = (long)Math.Round((double)(DownloadDone - SpeedLastDone) / ((double)num / 1000.0));
						SpeedLastDone = DownloadDone;
						SpeedLastTime += num;
					}
					return _Speed;
				}
			}
		}

		public double Progress
		{
			get
			{
				switch (State)
				{
				case NetState.WaitingToCheck:
					return 0.0;
				case NetState.WaitingToDownload:
					return 0.01;
				case NetState.Connecting:
					return 0.02;
				case NetState.Reading:
					return 0.04;
				case NetState.Downloading:
				{
					double num = (IsUnknownSize ? 0.5 : ((double)DownloadDone / (double)Math.Max(FileSize, 1L)));
					num = 1.0 - Math.Pow(1.0 - num, 0.9);
					return num * 0.93 + 0.05;
				}
				case NetState.Merging:
					return 0.99;
				case NetState.Finished:
				case NetState.Canceled:
					return 1.0;
				default:
					throw new ArgumentOutOfRangeException("文件状态未知：" + Conversions.ToString((int)State));
				}
			}
		}

		private int ConnectAverage
		{
			get
			{
				int connectCount = ConnectCount;
				return checked((int)Math.Round((connectCount == 0) ? (-1.0) : ((double)ConnectTime / (double)connectCount)));
			}
		}

		private NetSource GetSource(int Id = 0)
		{
			if (Id >= Sources.Count || Id < 0)
			{
				Id = 0;
			}
			object lockSource = LockSource;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockSource);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockSource, ref lockTaken);
				if (HasAvailableSource(AllowOnceSource: false))
				{
					NetSource netSource = Sources[Id];
					while (netSource.IsFailed)
					{
						Id = checked(Id + 1);
						if (Id >= Sources.Count)
						{
							Id = 0;
						}
						netSource = Sources[Id];
					}
					return netSource;
				}
				if (((IEnumerable<NetSource>)SourcesOnce).Any())
				{
					return ((IEnumerable<NetSource>)SourcesOnce).First();
				}
				return null;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockSource);
				}
			}
		}

		public bool HasAvailableSource(bool AllowOnceSource = true)
		{
			object lockSource = LockSource;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockSource);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockSource, ref lockTaken);
				if (Sources.Any([SpecialName] (NetSource s) => !s.IsFailed))
				{
					return true;
				}
				if (AllowOnceSource && ((IEnumerable<NetSource>)SourcesOnce).Any())
				{
					return true;
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockSource);
				}
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is NetFile netFile)
			{
				return Uuid == netFile.Uuid;
			}
			return false;
		}

		public NetFile(IEnumerable<string> Urls, string LocalPath, FileChecker Checker = null, bool SimulateBrowserHeaders = false)
		{
			Tasks = new ConcurrentList<LoaderDownload>();
			FirstThreadSource = 0;
			SourcesOnce = new ConcurrentList<NetSource>();
			Retried = false;
			this.LocalPath = null;
			LocalName = null;
			State = NetState.WaitingToCheck;
			Ex = new List<Exception>();
			FileSize = -2L;
			IsUnknownSize = false;
			DownloadDone = 0L;
			SpeedLastTime = ModBase.GetTimeMs();
			SpeedLastDone = 0L;
			_Speed = 0L;
			IsCopy = false;
			ConnectCount = 0;
			ConnectTime = 0L;
			LockState = RuntimeHelpers.GetObjectValue(new object());
			LockChain = RuntimeHelpers.GetObjectValue(new object());
			LockSource = RuntimeHelpers.GetObjectValue(new object());
			Uuid = ModBase.GetUuid();
			List<NetSource> list = new List<NetSource>();
			int num = 0;
			Urls = Urls.Distinct().ToArray();
			foreach (string Url in Urls)
			{
				list.Add(new NetSource
				{
					FailCount = 0,
					Url = ModSecret.SecretCdnSign(StringExtensions.ReplaceLineEndings(Url, "", false).Trim()),
					Id = num,
					IsFailed = false,
					Ex = null
				});
				num = checked(num + 1);
			}
			Sources = list;
			this.LocalPath = LocalPath;
			Check = Checker;
			this.SimulateBrowserHeaders = SimulateBrowserHeaders;
			LocalName = PathUtils.GetLastPart(LocalPath);
		}

		public NetThread TryBeginThread()
		{
			checked
			{
				NetThread result;
				try
				{
					NetSource netSource = null;
					object lockSource = LockSource;
					ObjectFlowControl.CheckForSyncLockOnValueType(lockSource);
					bool lockTaken = false;
					try
					{
						Monitor.Enter(lockSource, ref lockTaken);
						if (NetTaskThreadCount >= NetTaskThreadLimit || !HasAvailableSource() || (IsNoSplit && (object)Threads != null && Threads.State != NetState.Canceled))
						{
							result = null;
						}
						else if (State >= NetState.Merging || State == NetState.WaitingToCheck)
						{
							result = null;
						}
						else
						{
							object lockState = LockState;
							ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
							bool lockTaken2 = false;
							try
							{
								Monitor.Enter(lockState, ref lockTaken2);
								if (State < NetState.Connecting)
								{
									State = NetState.Connecting;
								}
							}
							finally
							{
								if (lockTaken2)
								{
									Monitor.Exit(lockState);
								}
							}
							if (IsNoSplit)
							{
								goto IL_00eb;
							}
							if (HasAvailableSource(AllowOnceSource: false))
							{
								goto end_IL_0014;
							}
							if ((object)((IEnumerable<NetSource>)SourcesOnce).First().SingleThread == null || ((IEnumerable<NetSource>)SourcesOnce).First().SingleThread.State == NetState.Canceled)
							{
								goto IL_00eb;
							}
							result = null;
						}
						goto end_IL_0000;
						IL_00eb:
						Reset();
						State = NetState.Reading;
						end_IL_0014:;
					}
					finally
					{
						if (lockTaken)
						{
							Monitor.Exit(lockSource);
						}
					}
					object lockChain = LockChain;
					ObjectFlowControl.CheckForSyncLockOnValueType(lockChain);
					bool lockTaken3 = false;
					Thread thread;
					NetThread netThread2;
					try
					{
						Monitor.Enter(lockChain, ref lockTaken3);
						long num;
						if ((object)Threads == null)
						{
							num = 0L;
							netSource = GetSource(FirstThreadSource);
							FirstThreadSource = netSource.Id + 1;
							goto IL_02c2;
						}
						foreach (NetThread thread2 in Threads)
						{
							if (thread2.State != NetState.Canceled || thread2.DownloadUndone <= 0)
							{
								continue;
							}
							num = thread2.DownloadStart + thread2.DownloadDone;
							netSource = GetSource(thread2.Source.Id + 1);
							goto IL_02c2;
						}
						NetSource source = GetSource();
						if (source == null)
						{
							result = null;
						}
						else
						{
							string url = source.Url;
							if (url.Contains("pcl2-server") || url.Contains("meloong.com") || url.Contains("bmclapi") || url.Contains("github.com") || url.Contains("optifine.net") || url.Contains("momot.rs"))
							{
								result = null;
							}
							else
							{
								NetThread netThread = Threads;
								foreach (NetThread thread3 in Threads)
								{
									if (thread3.DownloadUndone > netThread.DownloadUndone)
									{
										netThread = thread3;
									}
								}
								if ((object)netThread != null && netThread.DownloadUndone >= 262144)
								{
									num = (long)Math.Round((double)netThread.DownloadEnd - (double)netThread.DownloadUndone * 0.4);
									netSource = GetSource();
									goto IL_02c2;
								}
								result = null;
							}
						}
						goto end_IL_0118;
						IL_02c2:
						if ((num > FileSize && FileSize >= 0 && !IsUnknownSize) || num < 0 || Information.IsNothing(netSource))
						{
							result = null;
						}
						else
						{
							int uuid = ModBase.GetUuid();
							if (((IEnumerable<LoaderDownload>)Tasks).Any())
							{
								thread = new Thread([SpecialName] (object a0) =>
								{
									Thread((NetThread)a0);
								})
								{
									Name = $"下载 {Tasks[0].Uuid}/{Uuid}/{uuid}#",
									Priority = ThreadPriority.BelowNormal
								};
								netThread2 = new NetThread
								{
									Uuid = uuid,
									DownloadStart = num,
									Thread = thread,
									Source = netSource,
									Task = this,
									State = NetState.WaitingToDownload
								};
								if (netThread2.IsFirstThread || (object)Threads == null)
								{
									Threads = netThread2;
								}
								else
								{
									NetThread netThread3 = Threads;
									while (netThread3.DownloadEnd <= num)
									{
										netThread3 = netThread3.NextThread;
									}
									netThread2.NextThread = netThread3.NextThread;
									netThread3.NextThread = netThread2;
								}
								goto IL_03ec;
							}
							result = null;
						}
						end_IL_0118:;
					}
					finally
					{
						if (lockTaken3)
						{
							Monitor.Exit(lockChain);
						}
					}
					goto end_IL_0000;
					IL_03ec:
					Interlocked.Increment(ref NetTaskThreadCount);
					object lockSource2 = LockSource;
					ObjectFlowControl.CheckForSyncLockOnValueType(lockSource2);
					bool lockTaken4 = false;
					try
					{
						Monitor.Enter(lockSource2, ref lockTaken4);
						if (!HasAvailableSource(AllowOnceSource: false))
						{
							((IEnumerable<NetSource>)SourcesOnce).First().SingleThread = netThread2;
						}
					}
					finally
					{
						if (lockTaken4)
						{
							Monitor.Exit(lockSource2);
						}
					}
					thread.Start(netThread2);
					result = netThread2;
					end_IL_0000:;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					Fail(new Exception($"尝试开始下载线程失败（{LocalName}）", innerException));
					result = null;
					ProjectData.ClearProjectError();
				}
				return result;
			}
		}

		private void Thread(NetThread Th)
		{
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Expected O, but got Unknown
			//IL_0155: Unknown result type (might be due to invalid IL or missing references)
			//IL_015f: Expected O, but got Unknown
			//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
			_Closure_0024__40_002D0 arg = default(_Closure_0024__40_002D0);
			_Closure_0024__40_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__40_002D0(arg);
			CS_0024_003C_003E8__locals0._0024VB_0024Me = this;
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Th = Th;
			Logger.Log($"{LocalName}：开始，起始点 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart}，{CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.Url}", (LogLevel)(CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart == 0L), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
			Stream stream = null;
			HttpRequestMessage Req = null;
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Response = null;
			Stream stream2 = null;
			CancellationTokenSource cancellationTokenSource = null;
			checked
			{
				int num = Math.Min(Math.Max(ConnectAverage, 15000) * (1 + CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.FailCount), 30000);
				CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.State = NetState.Connecting;
				try
				{
					int num2 = 0;
					long num3 = default(long);
					if (!SourcesOnce.Contains(CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source) || !(CS_0024_003C_003E8__locals0._0024VB_0024Local_Th != CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.SingleThread))
					{
						Req = new HttpRequestMessage(HttpMethod.Get, CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.Url);
						ModSecret.SecretHeadersSign(CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.Url, ref Req, SimulateBrowserHeaders);
						cancellationTokenSource = new CancellationTokenSource(num);
						unchecked
						{
							if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.IsFirstThread)
							{
								Req.Headers.Range = new RangeHeaderValue((long?)CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart, (long?)null);
							}
							num3 = 0L;
							CS_0024_003C_003E8__locals0._0024VB_0024Local_Response = ModBase.GetResultWithTimeout(ThreadClient.SendAsync(Req, (HttpCompletionOption)1, cancellationTokenSource.Token), cancellationTokenSource, num);
							if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.IsSuccessStatusCode)
							{
								throw new HttpRequestCodeException($"错误码 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.StatusCode} ({(int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.StatusCode})，{CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.Url}", CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.StatusCode, (string)null);
							}
						}
						if (State != NetState.Canceled)
						{
							if (Operators.CompareString(CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.RequestMessage.RequestUri.ToString(), CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.Url, TextCompare: false) != 0)
							{
								Logger.Trace((Func<string>)([SpecialName] () => $"{CS_0024_003C_003E8__locals0._0024VB_0024Me.LocalName}：重定向至 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.RequestMessage.RequestUri}"), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
							}
							num3 = CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.Content.Headers.ContentLength.GetValueOrDefault(-1L);
							if (num3 < 0)
							{
								if (FileSize > 1)
								{
									if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart != 0L)
									{
										Logger.Info($"{LocalName}：ContentLength 返回了 {num3}，无法确定是否支持分段下载，视作不支持", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
										goto IL_0513;
									}
									Logger.Info($"{LocalName}：文件大小未知，但已从其他下载源获取，不作处理", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
								}
								else
								{
									FileSize = -1L;
									IsUnknownSize = true;
									Logger.Info($"{LocalName}：文件大小未知", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
								}
							}
							else if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.IsFirstThread)
							{
								if (Check != null)
								{
									if (num3 < Check.MinSize && Check.MinSize > 0)
									{
										throw new Exception($"文件大小不足，获取结果为 {num3}，要求至少为 {Check.MinSize}。");
									}
									if (num3 != Check.ActualSize && Check.ActualSize > 0)
									{
										throw new Exception($"文件大小不一致，获取结果为 {num3}，要求必须为 {Check.ActualSize}。");
									}
								}
								FileSize = num3;
								IsUnknownSize = false;
								Logger.Info($"{LocalName}：文件大小 {num3}（{StringUtils.FormatByteSize(num3)}）", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
								if (num3 > 52428800)
								{
									DriveInfo[] drives = DriveInfo.GetDrives();
									foreach (DriveInfo driveInfo in drives)
									{
										string text = driveInfo.Name.First().ToString();
										double num4 = (StringExtensions.StartsWithF(ModBase.PathTemp, text, false) ? ((double)num3 * 1.1) : 0.0) + (double)(StringExtensions.StartsWithF(LocalPath, text, false) ? (num3 + 5242880) : 0);
										if (num4 > 0.0 && (double)driveInfo.TotalFreeSpace < num4)
										{
											throw new Exception(text + " 盘空间不足，无法进行下载。\r\n需要至少 " + StringUtils.FormatByteSize((long)Math.Round(num4)) + " 空间，但当前仅剩余 " + StringUtils.FormatByteSize(driveInfo.TotalFreeSpace) + "。" + (StringExtensions.StartsWithF(ModBase.PathTemp, text, false) ? "\r\n\r\n下载时需要与文件同等大小的空间存放缓存，你可以在设置中调整缓存文件夹的位置。" : ""));
										}
									}
								}
							}
							else
							{
								if (FileSize < 0)
								{
									throw new Exception("非首线程运行时，尚未获取文件大小");
								}
								if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart > 0 && num3 >= FileSize)
								{
									goto IL_0513;
								}
								if (FileSize - CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart != num3)
								{
									throw new RangeNotSupportedException($"获取到的分段大小不一致：Range 起始于 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart}，预期 ContentLength 为 {FileSize - CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart}，返回 ContentLength 为 {num3}，总文件大小 {FileSize}");
								}
							}
							CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.State = NetState.Reading;
							object lockState = LockState;
							ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
							bool lockTaken = false;
							try
							{
								Monitor.Enter(lockState, ref lockTaken);
								if (State < NetState.Reading)
								{
									State = NetState.Reading;
								}
							}
							finally
							{
								if (lockTaken)
								{
									Monitor.Exit(lockState);
								}
							}
							if (IsNoSplit)
							{
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Temp = null;
								Cache = new MemoryStream();
								stream = Cache;
							}
							else
							{
								CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Temp = $"{ModBase.PathTemp}Download\\{Uuid}_{CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Uuid}_{ModBase.RandomInteger(0, 999999)}.tmp";
								stream = FileUtils.CreateAsStream(CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Temp);
							}
							cancellationTokenSource.Dispose();
							cancellationTokenSource = new CancellationTokenSource();
							stream2 = CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
							if (Settings.Get<bool>("SystemDebugDelay"))
							{
								System.Threading.Thread.Sleep(ModBase.RandomInteger(50, 3000));
							}
							byte[] buffer = new byte[16385];
							num2 = ModBase.GetResultWithTimeout(stream2.ReadAsync(buffer, 0, 16384, cancellationTokenSource.Token), cancellationTokenSource, num);
							while ((IsUnknownSize || CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadUndone > 0) && num2 > 0 && !ModBase.IsProgramEnding && State < NetState.Merging && (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.IsFailed || CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.SingleThread == CS_0024_003C_003E8__locals0._0024VB_0024Local_Th))
							{
								while (NetTaskSpeedLimitHigh > 0 && NetTaskSpeedLimitLeft <= 0)
								{
									System.Threading.Thread.Sleep(16);
								}
								int num5 = (int)(IsUnknownSize ? num2 : Math.Min(num2, CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadUndone));
								if (NetTaskSpeedLimitHigh > 0)
								{
									Interlocked.Add(ref NetTaskSpeedLimitLeft, -num5);
								}
								long num6 = ModBase.GetTimeMs() - CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.LastReceiveTime;
								if (num6 > 100000000)
								{
									num6 = 1L;
								}
								if (num5 > 0)
								{
									if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadDone == 0L)
									{
										CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.State = NetState.Downloading;
										object lockState2 = LockState;
										ObjectFlowControl.CheckForSyncLockOnValueType(lockState2);
										bool lockTaken2 = false;
										try
										{
											Monitor.Enter(lockState2, ref lockTaken2);
											if (State < NetState.Downloading)
											{
												State = NetState.Downloading;
											}
										}
										finally
										{
											if (lockTaken2)
											{
												Monitor.Exit(lockState2);
											}
										}
										Interlocked.Increment(ref ConnectCount);
										Interlocked.Add(ref ConnectTime, ModBase.GetTimeMs() - CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.InitTime);
									}
									CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.FailCount = 0;
									foreach (LoaderDownload task in Tasks)
									{
										task.FailCount = 0;
									}
									Interlocked.Add(ref NetManager.DownloadDone, num5);
									Interlocked.Add(ref DownloadDone, num5);
									CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadDone += num5;
									stream.Write(buffer, 0, num5);
									if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadUndone == 0L && !IsUnknownSize)
									{
										break;
									}
									if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.LastReceiveTime > 0 && num6 > 5000 && num6 > num5 && (object)CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.SingleThread == null)
									{
										throw new TimeoutException("由于速度过慢断开链接，下载 " + Conversions.ToString(num5) + " B，消耗 " + Conversions.ToString(num6) + " ms。");
									}
									CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.LastReceiveTime = ModBase.GetTimeMs();
								}
								else if (CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.LastReceiveTime > 0 && num6 > num)
								{
									throw new TimeoutException("操作超时，无数据。");
								}
								num2 = ModBase.GetResultWithTimeout(stream2.ReadAsync(buffer, 0, 16384, cancellationTokenSource.Token), cancellationTokenSource, num);
							}
						}
					}
					goto IL_0a31;
					IL_0a31:
					if (State == NetState.Canceled || (CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.IsFailed && CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source.SingleThread != CS_0024_003C_003E8__locals0._0024VB_0024Local_Th) || (CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadUndone > 0 && !IsUnknownSize))
					{
						CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.State = NetState.Canceled;
						Logger.Info($"{LocalName}：中断", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
						return;
					}
					if (num2 == 0 && CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadUndone > 0 && !IsUnknownSize)
					{
						throw new Exception($"返回的 ContentLength 过多：ContentLength 为 {num3}，但获取到的总数据量仅为 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadDone}（全文件总数据量 {DownloadDone}）");
					}
					CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.State = NetState.Finished;
					Logger.Trace((Func<string>)([SpecialName] () => $"{CS_0024_003C_003E8__locals0._0024VB_0024Me.LocalName}：完成，已下载 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadDone}（{CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart}~{CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadEnd}）"), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
					return;
					IL_0513:
					object lockSource = LockSource;
					ObjectFlowControl.CheckForSyncLockOnValueType(lockSource);
					bool lockTaken3 = false;
					try
					{
						Monitor.Enter(lockSource, ref lockTaken3);
						if (SourcesOnce.Contains(CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.Source))
						{
							goto IL_0a31;
						}
					}
					finally
					{
						if (lockTaken3)
						{
							Monitor.Exit(lockSource);
						}
					}
					throw new RangeNotSupportedException($"该下载源不支持分段下载：Range 起始于 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart}，预期 ContentLength 为 {FileSize - CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.DownloadStart}，返回 ContentLength 为 {num3}，总文件大小 {FileSize}");
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					if (ExceptionExtensions.IsBadNetwork(ex2) || ex2 is TaskCanceledException)
					{
						Logger.Info((Exception)new TimeoutException($"{LocalName}：超时（{num}ms），{ex2.Message}"), (string)null, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
					}
					else
					{
						Logger.Info(ex2, $"{LocalName}：出错", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
					}
					SourceFail(CS_0024_003C_003E8__locals0._0024VB_0024Local_Th, ex2, IsMergeFailure: false);
					ProjectData.ClearProjectError();
				}
				finally
				{
					if (Req != null)
					{
						Req.Dispose();
					}
					HttpResponseMessage _0024VB_0024Local_Response = CS_0024_003C_003E8__locals0._0024VB_0024Local_Response;
					if (_0024VB_0024Local_Response != null)
					{
						_0024VB_0024Local_Response.Dispose();
					}
					stream2?.Dispose();
					cancellationTokenSource?.Dispose();
					if (!IsNoSplit)
					{
						stream?.Dispose();
					}
					Interlocked.Decrement(ref NetTaskThreadCount);
					if (((FileSize >= 0 && DownloadDone >= FileSize) || (FileSize == -1 && DownloadDone > 0)) && State < NetState.Merging && CS_0024_003C_003E8__locals0._0024VB_0024Local_Th.State != NetState.Canceled)
					{
						Merge(CS_0024_003C_003E8__locals0._0024VB_0024Local_Th);
					}
				}
			}
		}

		private void SourceFail(NetThread Th, Exception ex, bool IsMergeFailure)
		{
			Interlocked.Increment(ref Th.Source.FailCount);
			foreach (LoaderDownload task in Tasks)
			{
				int location = task.FailCount;
				Interlocked.Increment(ref location);
				task.FailCount = location;
			}
			Th.State = NetState.Canceled;
			Th.Source.Ex = ex;
			bool flag = ex is RangeNotSupportedException || ex.Message.Contains("(416)");
			if (IsMergeFailure || flag || ex.Message.Contains("(502)") || ex.Message.Contains("(404)") || ex.Message.Contains("未能解析") || ex.Message.Contains("无返回数据") || ex.Message.Contains("空间不足") || ((ex.Message.Contains("(403)") || ex.Message.Contains("(429)")) && !Th.Source.Url.Contains("bmclapi")) || (Th.Source.FailCount >= MathUtils.Clamp<int>(NetTaskThreadLimit, 5, 30) && DownloadDone < 1) || Th.Source.FailCount > checked(NetTaskThreadLimit + 2))
			{
				bool flag2 = false;
				object lockSource = LockSource;
				ObjectFlowControl.CheckForSyncLockOnValueType(lockSource);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(lockSource, ref lockTaken);
					if (!Th.Source.IsFailed || Th.Source.SingleThread == Th)
					{
						flag2 = true;
						Th.Source.IsFailed = true;
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(lockSource);
					}
				}
				if (flag2)
				{
					Logger.Info($"{LocalName}：下载源被禁用（{Th.Source.Id}，Range 问题：{flag}）：{Th.Source.Url}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
					Logger.Warn(ex, string.Format("{0}下载源 {1} 已被禁用", ((object)((IEnumerable<NetSource>)SourcesOnce).FirstOrDefault()?.SingleThread == null) ? "" : "单线程", Th.Source.Id), (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
					object lockSource2 = LockSource;
					ObjectFlowControl.CheckForSyncLockOnValueType(lockSource2);
					bool lockTaken2 = false;
					try
					{
						Monitor.Enter(lockSource2, ref lockTaken2);
						SourcesOnce.Remove(Th.Source);
					}
					finally
					{
						if (lockTaken2)
						{
							Monitor.Exit(lockSource2);
						}
					}
					if (ex.Message.Contains("空间不足"))
					{
						Fail(ex);
					}
					else if (!HasAvailableSource() || IsMergeFailure)
					{
						if (!Retried)
						{
							if (!flag)
							{
								Logger.Warn($"{LocalName}：文件下载失败，正在自动重试……", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
							}
							Retried = true;
							object lockSource3 = LockSource;
							ObjectFlowControl.CheckForSyncLockOnValueType(lockSource3);
							bool lockTaken3 = false;
							try
							{
								Monitor.Enter(lockSource3, ref lockTaken3);
								SourcesOnce.Clear();
								foreach (NetSource source in Sources)
								{
									SourcesOnce.Add(source);
									source.IsFailed = true;
								}
							}
							finally
							{
								if (lockTaken3)
								{
									Monitor.Exit(lockSource3);
								}
							}
							Reset();
							object lockState = LockState;
							ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
							bool lockTaken4 = false;
							try
							{
								Monitor.Enter(lockState, ref lockTaken4);
								State = NetState.WaitingToDownload;
							}
							finally
							{
								if (lockTaken4)
								{
									Monitor.Exit(lockState);
								}
							}
						}
						else if (HasAvailableSource() && IsMergeFailure)
						{
							Reset();
							object lockState2 = LockState;
							ObjectFlowControl.CheckForSyncLockOnValueType(lockState2);
							bool lockTaken5 = false;
							try
							{
								Monitor.Enter(lockState2, ref lockTaken5);
								State = NetState.WaitingToDownload;
							}
							finally
							{
								if (lockTaken5)
								{
									Monitor.Exit(lockState2);
								}
							}
						}
						else
						{
							Logger.Info($"{LocalName}：已无可用下载源，下载失败", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
							Exception raiseEx = null;
							object lockSource4 = LockSource;
							ObjectFlowControl.CheckForSyncLockOnValueType(lockSource4);
							bool lockTaken6 = false;
							try
							{
								Monitor.Enter(lockSource4, ref lockTaken6);
								foreach (NetSource source2 in Sources)
								{
									Logger.Info($"已禁用的下载源：{source2.Url}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
									if (source2.Ex != null)
									{
										raiseEx = source2.Ex;
										Logger.Warn(source2.Ex, "下载源禁用原因", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
									}
								}
							}
							finally
							{
								if (lockTaken6)
								{
									Monitor.Exit(lockSource4);
								}
							}
							Fail(raiseEx);
						}
					}
				}
			}
			if (FileSize == -2)
			{
				Reset();
			}
		}

		private void Reset()
		{
			FileSize = -2L;
			Cache?.Dispose();
			Cache = null;
			object lockChain = LockChain;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockChain);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockChain, ref lockTaken);
				Threads = null;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockChain);
				}
			}
			Interlocked.Add(ref NetManager.DownloadDone, checked(-DownloadDone));
			DownloadDone = 0L;
			SpeedLastDone = 0L;
		}

		private void Merge(NetThread Th)
		{
			//IL_0207: Unknown result type (might be due to invalid IL or missing references)
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			object lockState = LockState;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockState, ref lockTaken);
				if (State >= NetState.Merging)
				{
					return;
				}
				State = NetState.Merging;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockState);
				}
			}
			int num = 0;
			checked
			{
				try
				{
					while (true)
					{
						try
						{
							DirectoryUtils.Create(PathUtils.RemoveLastPart(LocalPath));
							FileUtils.Delete(LocalPath, false);
							object lockChain = LockChain;
							ObjectFlowControl.CheckForSyncLockOnValueType(lockChain);
							bool lockTaken2 = false;
							try
							{
								Monitor.Enter(lockChain, ref lockTaken2);
								if (IsNoSplit)
								{
									Logger.Trace((Func<string>)([SpecialName] () => $"{LocalName}：下载结束，从内存输出文件"), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
									FileUtils.Write(LocalPath, (Stream)Cache);
								}
								else if (Threads.DownloadDone == DownloadDone && Threads.Temp != null)
								{
									Logger.Trace((Func<string>)([SpecialName] () => $"{LocalName}：下载结束，仅有一个文件，无需合并"), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
									FileUtils.Copy(Threads.Temp, LocalPath);
								}
								else
								{
									Logger.Trace((Func<string>)([SpecialName] () => $"{LocalName}：下载结束，开始合并文件"), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
									using FileStream output = FileUtils.CreateAsStream(LocalPath);
									using BinaryWriter binaryWriter = new BinaryWriter(output);
									foreach (NetThread thread in Threads)
									{
										if (thread.DownloadDone == 0L || thread.Temp == null)
										{
											continue;
										}
										using Stream input = FileUtils.ReadAsStream(thread.Temp, (Type)null);
										using BinaryReader binaryReader = new BinaryReader(input);
										binaryWriter.Write(binaryReader.ReadBytes((int)thread.DownloadDone));
									}
								}
								FileChecker check = Check;
								string text = ((check != null) ? check.Check(LocalPath) : null);
								if (text == null && !IsUnknownSize && Check != null)
								{
									if (Check.ActualSize == -1)
									{
										text = new FileChecker
										{
											ActualSize = FileSize
										}.Check(LocalPath);
									}
									else if (Check.ActualSize != FileSize)
									{
										text = $"文件大小不一致：任务校验要求为 {Check.ActualSize}，请求结果为 {FileSize}";
									}
								}
								if (text != null)
								{
									Logger.Info($"{LocalName} 文件校验失败，下载线程细节：", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
									foreach (NetThread thread2 in Threads)
									{
										Logger.Info($"- {thread2.Uuid}#，状态 {thread2.State}，范围 {thread2.DownloadStart}~{thread2.DownloadEnd}，已下载 {thread2.DownloadDone}，未下载 {thread2.DownloadUndone}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
									}
									throw new Exception(text);
								}
								if (!IsNoSplit)
								{
									foreach (NetThread thread3 in Threads)
									{
										if (thread3.Temp != null)
										{
											FileUtils.Delete(thread3.Temp, false);
										}
									}
								}
								Finish();
								break;
							}
							finally
							{
								if (lockTaken2)
								{
									Monitor.Exit(lockChain);
								}
							}
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception ex2 = ex;
							num++;
							if (State > NetState.Merging)
							{
								ProjectData.ClearProjectError();
								break;
							}
							Logger.Warn(ex2, $"合并文件出错，第 {num} 次尝试（{LocalName}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
							if (num < 3)
							{
								System.Threading.Thread.Sleep(500 * num);
								ProjectData.ClearProjectError();
								continue;
							}
							try
							{
								FileUtils.Delete(LocalPath, false);
							}
							catch (Exception ex3)
							{
								ProjectData.SetProjectError(ex3);
								Exception ex4 = ex3;
								Logger.Warn(ex2, $"删除合并失败的文件出错（{LocalPath}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
								ProjectData.ClearProjectError();
							}
							SourceFail(Th, ex2, IsMergeFailure: true);
							ProjectData.ClearProjectError();
							break;
						}
					}
				}
				finally
				{
					Cache?.Dispose();
				}
			}
		}

		private void Fail(Exception RaiseEx = null)
		{
			object lockState = LockState;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockState, ref lockTaken);
				if (State >= NetState.Finished)
				{
					return;
				}
				if (RaiseEx != null)
				{
					Ex.Add(RaiseEx);
				}
				Logger.Info($"{LocalName}：已失败，当前状态 {State}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
				State = NetState.Canceled;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockState);
				}
			}
			CancelInternal();
			foreach (LoaderDownload task in Tasks)
			{
				task.OnFileFail(this);
			}
		}

		public void Cancel(LoaderDownload CanceledTask)
		{
			Tasks.Remove(CanceledTask);
			if (((IEnumerable<LoaderDownload>)Tasks).Any())
			{
				return;
			}
			object lockState = LockState;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockState, ref lockTaken);
				if (State >= NetState.Finished)
				{
					return;
				}
				Logger.Info($"{LocalName}：已中断，当前状态 {State}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
				State = NetState.Canceled;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockState);
				}
			}
			CancelInternal();
		}

		private void CancelInternal()
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
					case 67:
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
								goto end_IL_0000_2;
							default:
								goto end_IL_0000;
							case 4:
								goto end_IL_0000_3;
							}
							goto default;
						}
						IL_0007:
						num = 2;
						Reset();
						break;
						end_IL_0000_2:
						break;
					}
					num = 3;
					Interlocked.Decrement(ref NetManager.FileRemain);
					break;
					end_IL_0000:;
				}
				catch (object obj) when (obj is Exception && num3 != 0 && num2 == 0)
				{
					ProjectData.SetProjectError((Exception)obj);
					try0000_dispatch = 67;
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

		public void Finish(bool PrintLog = true)
		{
			object lockState = LockState;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockState, ref lockTaken);
				if (State >= NetState.Finished)
				{
					return;
				}
				State = NetState.Finished;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockState);
				}
			}
			Interlocked.Decrement(ref NetManager.FileRemain);
			if (PrintLog)
			{
				Logger.Info($"{LocalName}：已完成", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
			}
			foreach (LoaderDownload task in Tasks)
			{
				task.OnFileFinish(this);
			}
		}
	}

	private class RangeNotSupportedException : HttpRequestException
	{
		public RangeNotSupportedException(string Message)
			: base(Message)
		{
		}
	}

	public class LoaderDownload : ModLoader.LoaderBase
	{
		[CompilerGenerated]
		internal sealed class _Closure_0024__13_002D0
		{
			public List<NetFile> _0024VB_0024Local_ThreadFiles;

			public _Closure_0024__13_002D1 _0024VB_0024NonLocal__0024VB_0024Closure_2;

			public _Closure_0024__13_002D0(_Closure_0024__13_002D0 arg0)
			{
				if (arg0 != null)
				{
					_0024VB_0024Local_ThreadFiles = arg0._0024VB_0024Local_ThreadFiles;
				}
			}

			[SpecialName]
			internal void _Lambda_0024__5()
			{
				_0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Me.CheckExistingFiles(_0024VB_0024Local_ThreadFiles, _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_Folders);
			}
		}

		[CompilerGenerated]
		internal sealed class _Closure_0024__13_002D1
		{
			public List<string> _0024VB_0024Local_Folders;

			public LoaderDownload _0024VB_0024Me;

			public _Closure_0024__13_002D1(_Closure_0024__13_002D1 arg0)
			{
				if (arg0 != null)
				{
					_0024VB_0024Local_Folders = arg0._0024VB_0024Local_Folders;
				}
			}
		}

		[CompilerGenerated]
		internal sealed class _Closure_0024__14_002D0
		{
			public List<NetFile> _0024VB_0024Local_Files;

			public _Closure_0024__14_002D0(_Closure_0024__14_002D0 arg0)
			{
				if (arg0 != null)
				{
					_0024VB_0024Local_Files = arg0._0024VB_0024Local_Files;
				}
			}

			[SpecialName]
			internal string _Lambda_0024__0()
			{
				return $"文件检查开始，本线程负责 {_0024VB_0024Local_Files.Count} 个文件，首个文件为 {_0024VB_0024Local_Files.FirstOrDefault()?.LocalName}";
			}
		}

		[CompilerGenerated]
		internal sealed class _Closure_0024__14_002D1
		{
			public NetFile _0024VB_0024Local_File;

			public _Closure_0024__14_002D1(_Closure_0024__14_002D1 arg0)
			{
				if (arg0 != null)
				{
					_0024VB_0024Local_File = arg0._0024VB_0024Local_File;
				}
			}
		}

		[CompilerGenerated]
		internal sealed class _Closure_0024__14_002D2
		{
			public string _0024VB_0024Local_Target;

			public _Closure_0024__14_002D1 _0024VB_0024NonLocal__0024VB_0024Closure_2;

			public _Closure_0024__14_002D2(_Closure_0024__14_002D2 arg0)
			{
				if (arg0 != null)
				{
					_0024VB_0024Local_Target = arg0._0024VB_0024Local_Target;
				}
			}

			[SpecialName]
			internal string _Lambda_0024__1()
			{
				return string.Format("本地文件匹配：{0} → {1}，当前状态 {2}", _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_File.LocalName, _0024VB_0024Local_Target ?? "无匹配", _0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_File.State);
			}
		}

		[CompilerGenerated]
		internal sealed class _Closure_0024__15_002D0
		{
			public NetFile _0024VB_0024Local_File;

			public _Closure_0024__15_002D0(_Closure_0024__15_002D0 arg0)
			{
				if (arg0 != null)
				{
					_0024VB_0024Local_File = arg0._0024VB_0024Local_File;
				}
			}

			[SpecialName]
			internal (string, int) _Lambda_0024__0(string FolderName)
			{
				return (FolderName, StringExtensions.IndexOfF(_0024VB_0024Local_File.LocalPath, FolderName, true));
			}
		}

		public ConcurrentQueue<NetFile> Files;

		private int FileRemain;

		private readonly object FileRemainLock;

		private double _Progress;

		private int _FailCount;

		[SpecialName]
		private ConcurrentDictionary<string, long> _0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes;

		[SpecialName]
		private StaticLocalInitFlag _0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes_0024Init;

		[SpecialName]
		private Func<string, FileChecker, bool> _0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate;

		[SpecialName]
		private StaticLocalInitFlag _0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate_0024Init;

		public override double Progress
		{
			get
			{
				if (base.State >= ModBase.LoadState.Finished)
				{
					return 1.0;
				}
				if (!Files.Any())
				{
					return 0.0;
				}
				return _Progress;
			}
			set
			{
				throw new Exception("文件下载不允许指定进度");
			}
		}

		public int FailCount
		{
			get
			{
				return _FailCount;
			}
			set
			{
				int try0000_dispatch = -1;
				int num = default(int);
				int num2 = default(int);
				int num3 = default(int);
				NetSource current = default(NetSource);
				List<Exception> list = default(List<Exception>);
				IEnumerator<NetFile> enumerator = default(IEnumerator<NetFile>);
				NetFile current2 = default(NetFile);
				List<NetSource>.Enumerator enumerator2 = default(List<NetSource>.Enumerator);
				while (true)
				{
					try
					{
						/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
						switch (try0000_dispatch)
						{
						default:
							num = 1;
							_FailCount = value;
							goto IL_0009;
						case 351:
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
									goto IL_0009;
								case 3:
									goto IL_003d;
								case 4:
									goto IL_005a;
								case 5:
									goto IL_0061;
								case 6:
									goto IL_0069;
								case 7:
									goto IL_0083;
								case 8:
									goto IL_009e;
								case 9:
									goto IL_00a9;
								case 10:
									goto IL_00b9;
								case 12:
									goto IL_00c6;
								case 13:
									goto IL_00d2;
								case 14:
									goto IL_00e2;
								case 15:
									goto IL_00ee;
								case 11:
								case 16:
									goto end_IL_0000_2;
								default:
									goto end_IL_0000;
								case 17:
									goto end_IL_0000_3;
								}
								goto default;
							}
							IL_00c6:
							num = 12;
							goto IL_00c9;
							IL_0009:
							num = 2;
							if (base.State != ModBase.LoadState.Loading || value < Math.Min(10000, checked(Math.Max(FileRemain * 8, NetTaskThreadLimit * 8 + 3))))
							{
								goto end_IL_0000_3;
							}
							goto IL_003d;
							IL_009e:
							num = 8;
							if (current.Ex != null)
							{
								goto IL_00a9;
							}
							goto IL_00c6;
							IL_003d:
							num = 3;
							Logger.Error($"由于同加载器中失败次数过多引发强制失败：连续失败了 {value} 次", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
							goto IL_005a;
							IL_005a:
							ProjectData.ClearProjectError();
							num3 = 1;
							goto IL_0061;
							IL_0061:
							num = 5;
							list = new List<Exception>();
							goto IL_0069;
							IL_0069:
							num = 6;
							enumerator = Files.GetEnumerator();
							goto IL_00e5;
							IL_00e5:
							if (enumerator.MoveNext())
							{
								current2 = enumerator.Current;
								goto IL_0083;
							}
							goto IL_00ee;
							IL_00ee:
							num = 15;
							enumerator?.Dispose();
							break;
							IL_00b9:
							num = 10;
							if (list.Count > 10)
							{
								break;
							}
							goto IL_00c6;
							IL_00a9:
							num = 9;
							list.Add(current.Ex);
							goto IL_00b9;
							IL_0083:
							num = 7;
							enumerator2 = current2.Sources.GetEnumerator();
							goto IL_00c9;
							IL_00c9:
							if (enumerator2.MoveNext())
							{
								current = enumerator2.Current;
								goto IL_009e;
							}
							goto IL_00d2;
							IL_00d2:
							num = 13;
							((IDisposable)enumerator2).Dispose();
							goto IL_00e2;
							IL_00e2:
							num = 14;
							goto IL_00e5;
							end_IL_0000_2:
							break;
						}
						num = 16;
						OnFail(list);
						break;
						end_IL_0000:;
					}
					catch (object obj) when (obj is Exception && num3 != 0 && num2 == 0)
					{
						ProjectData.SetProjectError((Exception)obj);
						try0000_dispatch = 351;
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
		}

		public void RefreshStat()
		{
			double num = 0.0;
			double num2 = 0.0;
			foreach (NetFile file in Files)
			{
				if (file.IsCopy)
				{
					num += file.Progress * 0.2;
					num2 += 0.2;
				}
				else
				{
					num += file.Progress;
					num2 += 1.0;
				}
			}
			if (num2 > 0.0 && !double.IsNaN(num2))
			{
				num /= num2;
			}
			_Progress = num;
		}

		public LoaderDownload(string Name, IEnumerable<NetFile> FileTasks)
		{
			FileRemainLock = RuntimeHelpers.GetObjectValue(new object());
			_Progress = 0.0;
			_FailCount = 0;
			base.Name = Name;
			Files = new ConcurrentQueue<NetFile>(FileTasks);
		}

		public override void Start(object Input = null, bool IsForceRestart = false)
		{
			if (Input != null)
			{
				Files = new ConcurrentQueue<NetFile>((IEnumerable<NetFile>)Input);
			}
			Files = new ConcurrentQueue<NetFile>(EnumerableExtensions.DistinctBy<NetFile, string>((IEnumerable<NetFile>)Files, (Func<NetFile, string>)([SpecialName] (NetFile a) => StringExtensions.Lower(PathUtils.ForCompare(a.LocalPath)))));
			object fileRemainLock = FileRemainLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(fileRemainLock);
			bool lockTaken = false;
			checked
			{
				try
				{
					Monitor.Enter(fileRemainLock, ref lockTaken);
					FileRemain += Files.Where([SpecialName] (NetFile f) => f.State != NetState.Finished).Count();
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(fileRemainLock);
					}
				}
				base.State = ModBase.LoadState.Loading;
			}
			ModBase.RunInNewThread([SpecialName] () =>
			{
				try
				{
					_Closure_0024__13_002D1 arg = new _Closure_0024__13_002D1(arg);
					arg._0024VB_0024Me = this;
					if (!Files.Any())
					{
						OnFinish();
					}
					else
					{
						foreach (NetFile file in Files)
						{
							if (file == null)
							{
								throw new ArgumentException("存在空文件请求！");
							}
							foreach (NetSource source in file.Sources)
							{
								if (!StringExtensions.StartsWithF(source.Url, "https://", true) && !StringExtensions.StartsWithF(source.Url, "http://", true))
								{
									source.Ex = new ArgumentException("输入的下载链接不正确：" + source.Url);
									source.IsFailed = true;
								}
							}
							if (!file.HasAvailableSource())
							{
								throw new ArgumentException("输入的下载链接不正确！");
							}
							file.LocalPath = file.LocalPath.Replace("/", "\\");
							if (!file.LocalPath.Contains(":\\"))
							{
								throw new ArgumentException("输入的本地文件地址不正确：" + file.LocalPath);
							}
							if (StringExtensions.EndsWithF(file.LocalPath, "\\", false))
							{
								throw new ArgumentException("请输入含文件名的完整文件路径：" + file.LocalPath);
							}
							DirectoryUtils.Create(PathUtils.RemoveLastPart(file.LocalPath));
						}
						List<NetFile> list = NetManager.Start(this);
						List<NetFile> list2 = new List<NetFile>();
						foreach (NetFile item in list)
						{
							if (item.Check != null)
							{
								list2.Add(item);
							}
							else
							{
								object lockState = LockState;
								ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
								bool lockTaken2 = false;
								try
								{
									Monitor.Enter(lockState, ref lockTaken2);
									item.State = NetState.WaitingToDownload;
									item.IsCopy = false;
								}
								finally
								{
									if (lockTaken2)
									{
										Monitor.Exit(lockState);
									}
								}
							}
						}
						if (list2.Any())
						{
							arg._0024VB_0024Local_Folders = new List<string>();
							arg._0024VB_0024Local_Folders.Add(Paths.AppData + ".minecraft\\");
							arg._0024VB_0024Local_Folders.AddRange(ModMinecraft.McFolderList.Select([SpecialName] (ModMinecraft.McFolder f) => f.Location));
							arg._0024VB_0024Local_Folders = (from f in arg._0024VB_0024Local_Folders.Distinct()
								where DirectoryUtils.Exists(f)
								select f).ToList();
							int num = MathUtils.Clamp<int>(list2.Count / 40, 1, 8);
							if (num == 1)
							{
								CheckExistingFiles(list2, arg._0024VB_0024Local_Folders);
							}
							else
							{
								int num2 = list2.Count / num;
								int num3 = list2.Count % num;
								int num4 = 0;
								checked
								{
									int num5 = num - 1;
									_Closure_0024__13_002D0 closure_0024__13_002D = default(_Closure_0024__13_002D0);
									for (int i = 0; i <= num5; i++)
									{
										closure_0024__13_002D = new _Closure_0024__13_002D0(closure_0024__13_002D);
										closure_0024__13_002D._0024VB_0024NonLocal__0024VB_0024Closure_2 = arg;
										int num6 = num2 + ((i < num3) ? 1 : 0);
										closure_0024__13_002D._0024VB_0024Local_ThreadFiles = list2.GetRange(num4, num6);
										num4 += num6;
										ModBase.RunInNewThread(closure_0024__13_002D._Lambda_0024__5, $"下载 文件复制 {Uuid}/{ModBase.GetUuid()}");
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception innerException = ex;
					OnFail(new List<Exception>
					{
						new Exception("下载初始化失败", innerException)
					});
					ProjectData.ClearProjectError();
				}
			}, "L/下载 " + Conversions.ToString(Uuid));
		}

		private void CheckExistingFiles(List<NetFile> Files, List<string> FolderList)
		{
			_Closure_0024__14_002D0 arg = default(_Closure_0024__14_002D0);
			_Closure_0024__14_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__14_002D0(arg);
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Files = Files;
			try
			{
				Logger.Trace((Func<string>)([SpecialName] () => $"文件检查开始，本线程负责 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Files.Count} 个文件，首个文件为 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Files.FirstOrDefault()?.LocalName}"), (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
				List<string> list = new List<string>();
				foreach (string Folder in FolderList)
				{
					foreach (string item in DirectoryUtils.EnumerateDirectories(Folder + "versions\\", false, "*"))
					{
						list.Add(PathUtils.AddSlashSuffix(item));
					}
				}
				using List<NetFile>.Enumerator enumerator3 = CS_0024_003C_003E8__locals0._0024VB_0024Local_Files.GetEnumerator();
				_Closure_0024__14_002D1 closure_0024__14_002D = default(_Closure_0024__14_002D1);
				_Closure_0024__14_002D2 closure_0024__14_002D2 = default(_Closure_0024__14_002D2);
				while (enumerator3.MoveNext())
				{
					closure_0024__14_002D = new _Closure_0024__14_002D1(closure_0024__14_002D);
					closure_0024__14_002D._0024VB_0024Local_File = enumerator3.Current;
					closure_0024__14_002D2 = new _Closure_0024__14_002D2(closure_0024__14_002D2);
					closure_0024__14_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2 = closure_0024__14_002D;
					closure_0024__14_002D2._0024VB_0024Local_Target = CheckExistingFile(FolderList, list, closure_0024__14_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_File);
					Logger.Trace((Func<string>)closure_0024__14_002D2._Lambda_0024__1, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
					if (closure_0024__14_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_File.State >= NetState.WaitingToDownload)
					{
						continue;
					}
					if (closure_0024__14_002D2._0024VB_0024Local_Target != null)
					{
						closure_0024__14_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_File.IsCopy = true;
						try
						{
							FileUtils.Copy(closure_0024__14_002D2._0024VB_0024Local_Target, closure_0024__14_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_File.LocalPath);
							closure_0024__14_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_File.Finish(PrintLog: false);
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							ProjectData.ClearProjectError();
							goto IL_017f;
						}
						continue;
					}
					goto IL_017f;
					IL_017f:
					object lockState = LockState;
					ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
					bool lockTaken = false;
					try
					{
						Monitor.Enter(lockState, ref lockTaken);
						closure_0024__14_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_File.State = NetState.WaitingToDownload;
						closure_0024__14_002D2._0024VB_0024NonLocal__0024VB_0024Closure_2._0024VB_0024Local_File.IsCopy = false;
					}
					finally
					{
						if (lockTaken)
						{
							Monitor.Exit(lockState);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception innerException = ex;
				OnFail(new List<Exception>
				{
					new Exception("下载已存在文件查找失败", innerException)
				});
				ProjectData.ClearProjectError();
			}
		}

		private string CheckExistingFile(List<string> FolderList, List<string> VersionFolders, NetFile File)
		{
			_Closure_0024__15_002D0 arg = default(_Closure_0024__15_002D0);
			_Closure_0024__15_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__15_002D0(arg);
			CS_0024_003C_003E8__locals0._0024VB_0024Local_File = File;
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_File.Check.Check(CS_0024_003C_003E8__locals0._0024VB_0024Local_File.LocalPath) == null)
			{
				return CS_0024_003C_003E8__locals0._0024VB_0024Local_File.LocalPath;
			}
			if (Settings.Get<bool>("SystemDebugSkipCopy"))
			{
				return null;
			}
			if (CS_0024_003C_003E8__locals0._0024VB_0024Local_File.Check.Hash == null && CS_0024_003C_003E8__locals0._0024VB_0024Local_File.Check.ActualSize < 0)
			{
				return null;
			}
			List<(string, int)> list = (from FolderName in new string[9] { "\\assets\\", "\\libraries\\", "\\versions\\", "\\mods\\", "\\coremods\\", "\\lib\\", "\\resourcepacks\\", "\\texturepacks\\", "\\shaderpacks\\" }
				select (FolderName, StringExtensions.IndexOfF(CS_0024_003C_003E8__locals0._0024VB_0024Local_File.LocalPath, FolderName, true)) into kv
				where kv.Item2 >= 0
				select kv).ToList();
			if (!list.Any())
			{
				if (Operators.CompareString(PathUtils.GetExtension(CS_0024_003C_003E8__locals0._0024VB_0024Local_File.LocalName), "jar", TextCompare: false) != 0)
				{
					return null;
				}
				list.Add(("\\versions\\", 1));
			}
			string text = EnumerableExtensions.MaxBy<(string, int), int>((IEnumerable<(string, int)>)list, (Func<(string, int), int>)([SpecialName] ((string FolderName, int) kv) => kv.Item2)).Item1.TrimStart('\\');
			if (_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes_0024Init == null)
			{
				Interlocked.CompareExchange(ref _0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes_0024Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes_0024Init, ref lockTaken);
				if (_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes_0024Init.State == 0)
				{
					_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes_0024Init.State = 2;
					_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes = new ConcurrentDictionary<string, long>();
				}
				else if (_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes_0024Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes_0024Init);
				}
			}
			if (_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate_0024Init == null)
			{
				Interlocked.CompareExchange(ref _0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate_0024Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken2 = false;
			try
			{
				Monitor.Enter(_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate_0024Init, ref lockTaken2);
				if (_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate_0024Init.State == 0)
				{
					_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate_0024Init.State = 2;
					_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate = [SpecialName] (string Candidate, FileChecker Checker) =>
					{
						long orAdd = _0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024Sizes.GetOrAdd(Candidate, [SpecialName] (string c) => (!FileUtils.Exists(c)) ? (-1) : FileUtils.GetInfo(c).Length);
						return Checker.ActualSize == orAdd && Checker.Check(Candidate) == null;
					};
				}
				else if (_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate_0024Init.State = 1;
				if (lockTaken2)
				{
					Monitor.Exit(_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate_0024Init);
				}
			}
			switch (text)
			{
			case "assets\\":
			case "libraries\\":
				foreach (string Folder in FolderList)
				{
					string text2 = Folder + text + StringExtensions.AfterFirst(CS_0024_003C_003E8__locals0._0024VB_0024Local_File.LocalPath, text, false);
					if (_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate(text2, CS_0024_003C_003E8__locals0._0024VB_0024Local_File.Check))
					{
						return text2;
					}
				}
				return null;
			case "versions\\":
				foreach (string VersionFolder in VersionFolders)
				{
					foreach (string item in DirectoryUtils.EnumerateFiles(VersionFolder, false, "*." + PathUtils.GetExtension(CS_0024_003C_003E8__locals0._0024VB_0024Local_File.LocalPath)))
					{
						if (_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate(item, CS_0024_003C_003E8__locals0._0024VB_0024Local_File.Check))
						{
							return item;
						}
					}
				}
				break;
			default:
				if (CS_0024_003C_003E8__locals0._0024VB_0024Local_File.Check.ActualSize < 0 || CS_0024_003C_003E8__locals0._0024VB_0024Local_File.Check.Hash == null)
				{
					return null;
				}
				foreach (string item2 in FolderList.Concat(VersionFolders))
				{
					foreach (string item3 in DirectoryUtils.EnumerateFiles(item2 + text, false, "*"))
					{
						if (_0024STATIC_0024CheckExistingFile_0024203E151281691E151281691E128724_0024CheckCandidate(item3, CS_0024_003C_003E8__locals0._0024VB_0024Local_File.Check))
						{
							return item3;
						}
					}
				}
				break;
			}
			return null;
		}

		public void OnFileFinish(NetFile File)
		{
			object fileRemainLock = FileRemainLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(fileRemainLock);
			bool lockTaken = false;
			checked
			{
				try
				{
					Monitor.Enter(fileRemainLock, ref lockTaken);
					FileRemain--;
					if (FileRemain > 0)
					{
						return;
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(fileRemainLock);
					}
				}
				OnFinish();
			}
		}

		public void OnFinish()
		{
			RaisePreviewFinish();
			object lockState = LockState;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockState, ref lockTaken);
				if (base.State <= ModBase.LoadState.Loading)
				{
					base.State = ModBase.LoadState.Finished;
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockState);
				}
			}
		}

		public void OnFileFail(NetFile File)
		{
			foreach (NetSource source in File.Sources)
			{
				if (!Information.IsNothing(source.Ex))
				{
					File.Ex.Add(source.Ex);
				}
			}
			OnFail(File.Ex);
		}

		public override void Failed(Exception Ex)
		{
			OnFail(new List<Exception> { Ex });
		}

		public void OnFail(List<Exception> ExList)
		{
			object lockState = LockState;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockState, ref lockTaken);
				if (base.State >= ModBase.LoadState.Finished)
				{
					return;
				}
				if (ExList == null || !ExList.Any())
				{
					ExList = new List<Exception>
					{
						new Exception("未知错误！")
					};
				}
				List<Exception> source = ExList.Where([SpecialName] (Exception e) => !ExceptionExtensions.IsCanceled(e) && !(e is TimeoutException)).ToList();
				base.Error = source.FirstOrDefault() ?? ExList.FirstOrDefault();
				foreach (NetFile file in Files)
				{
					if (file.State == NetState.Canceled && !file.Sources.All([SpecialName] (NetSource s) => ExceptionExtensions.IsCanceled(s.Ex) || s.Ex is TimeoutException))
					{
						string text = EnumerableExtensions.Join((IEnumerable)file.Sources.Select([SpecialName] (NetSource s) => string.Format("{0}（{1}）", (s.Ex == null) ? "无错误信息。" : ExceptionExtensions.GetDisplay(s.Ex, false), s.Url)), "\r\n");
						base.Error = new Exception("文件下载失败：" + file.LocalPath + "\r\n各下载源的错误如下：\r\n" + text, base.Error);
						ModMain.Telemetry("文件下载失败", "FileName", file.LocalName, "Exception", text);
						break;
					}
				}
				base.State = ModBase.LoadState.Failed;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockState);
				}
			}
			foreach (NetFile file2 in Files)
			{
				file2.Cancel(this);
			}
			Logger.Error((Exception)new AggregateException(ExList), "下载加载器失败", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
		}

		public override void Cancel()
		{
			object lockState = LockState;
			ObjectFlowControl.CheckForSyncLockOnValueType(lockState);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(lockState, ref lockTaken);
				if (base.State >= ModBase.LoadState.Finished)
				{
					return;
				}
				base.State = ModBase.LoadState.Canceled;
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(lockState);
				}
			}
			Logger.Info($"{Name} 已取消！", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
			foreach (NetFile file in Files)
			{
				file.Cancel(this);
			}
		}
	}

	public class NetManagerClass
	{
		[CompilerGenerated]
		internal sealed class _Closure_0024__12_002D0
		{
			public NetFile _0024VB_0024Local_File;

			public _Closure_0024__12_002D0(_Closure_0024__12_002D0 arg0)
			{
				if (arg0 != null)
				{
					_0024VB_0024Local_File = arg0._0024VB_0024Local_File;
				}
			}

			[SpecialName]
			internal string _Lambda_0024__0()
			{
				return $"{_0024VB_0024Local_File.LocalName}：无需开始，该文件已在下载中，目标 {_0024VB_0024Local_File.LocalPath}";
			}

			[SpecialName]
			internal string _Lambda_0024__1()
			{
				return $"{_0024VB_0024Local_File.LocalName}：开始，目标 {_0024VB_0024Local_File.LocalPath}";
			}
		}

		[CompilerGenerated]
		internal sealed class _Closure_0024__9_002D0
		{
			public VB_0024AnonymousDelegate_17<int> _0024VB_0024Local_ThreadStarter;

			public _Closure_0024__9_002D0(_Closure_0024__9_002D0 arg0)
			{
				if (arg0 != null)
				{
					_0024VB_0024Local_ThreadStarter = arg0._0024VB_0024Local_ThreadStarter;
				}
			}

			[SpecialName]
			internal void _Lambda_0024__1()
			{
				_0024VB_0024Local_ThreadStarter(0);
			}

			[SpecialName]
			internal void _Lambda_0024__2()
			{
				_0024VB_0024Local_ThreadStarter(1);
			}
		}

		public ConcurrentDictionary<string, NetFile> AllFiles;

		public ConcurrentBag<LoaderDownload> Tasks;

		public long DownloadDone;

		public int FileRemain;

		public long Speed;

		public readonly int Uuid;

		private long RefreshStatLast;

		private object DownloadCacheLock;

		private bool IsDownloadCacheCleared;

		[SpecialName]
		private List<long> _0024STATIC_0024RefreshStat_00242001_0024SpeedLast;

		[SpecialName]
		private StaticLocalInitFlag _0024STATIC_0024RefreshStat_00242001_0024SpeedLast_0024Init;

		[SpecialName]
		private long _0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone;

		[SpecialName]
		private StaticLocalInitFlag _0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone_0024Init;

		[SpecialName]
		private bool _0024STATIC_0024StartManager_00242001_0024IsStarted;

		[SpecialName]
		private StaticLocalInitFlag _0024STATIC_0024StartManager_00242001_0024IsStarted_0024Init;

		public NetManagerClass()
		{
			AllFiles = new ConcurrentDictionary<string, NetFile>();
			Tasks = new ConcurrentBag<LoaderDownload>();
			DownloadDone = 0L;
			FileRemain = 0;
			Speed = 0L;
			Uuid = ModBase.GetUuid();
			DownloadCacheLock = RuntimeHelpers.GetObjectValue(new object());
			IsDownloadCacheCleared = false;
		}

		private void RefreshStat()
		{
			checked
			{
				try
				{
					long num = ModBase.GetTimeMs() - RefreshStatLast;
					if (num == 0L)
					{
						return;
					}
					RefreshStatLast += num;
					if (_0024STATIC_0024RefreshStat_00242001_0024SpeedLast_0024Init == null)
					{
						Interlocked.CompareExchange(ref _0024STATIC_0024RefreshStat_00242001_0024SpeedLast_0024Init, new StaticLocalInitFlag(), null);
					}
					bool lockTaken = false;
					try
					{
						Monitor.Enter(_0024STATIC_0024RefreshStat_00242001_0024SpeedLast_0024Init, ref lockTaken);
						if (_0024STATIC_0024RefreshStat_00242001_0024SpeedLast_0024Init.State == 0)
						{
							_0024STATIC_0024RefreshStat_00242001_0024SpeedLast_0024Init.State = 2;
							_0024STATIC_0024RefreshStat_00242001_0024SpeedLast = new List<long>();
						}
						else if (_0024STATIC_0024RefreshStat_00242001_0024SpeedLast_0024Init.State == 2)
						{
							throw new IncompleteInitialization();
						}
					}
					finally
					{
						_0024STATIC_0024RefreshStat_00242001_0024SpeedLast_0024Init.State = 1;
						if (lockTaken)
						{
							Monitor.Exit(_0024STATIC_0024RefreshStat_00242001_0024SpeedLast_0024Init);
						}
					}
					if (_0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone_0024Init == null)
					{
						Interlocked.CompareExchange(ref _0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone_0024Init, new StaticLocalInitFlag(), null);
					}
					bool lockTaken2 = false;
					try
					{
						Monitor.Enter(_0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone_0024Init, ref lockTaken2);
						if (_0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone_0024Init.State == 0)
						{
							_0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone_0024Init.State = 2;
							_0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone = 0L;
						}
						else if (_0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone_0024Init.State == 2)
						{
							throw new IncompleteInitialization();
						}
					}
					finally
					{
						_0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone_0024Init.State = 1;
						if (lockTaken2)
						{
							Monitor.Exit(_0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone_0024Init);
						}
					}
					double a = Math.Max(0.0, (double)(DownloadDone - _0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone) / ((double)num / 1000.0));
					_0024STATIC_0024RefreshStat_00242001_0024SpeedLast.Insert(0, (long)Math.Round(a));
					if (_0024STATIC_0024RefreshStat_00242001_0024SpeedLast.Count >= 31)
					{
						_0024STATIC_0024RefreshStat_00242001_0024SpeedLast.RemoveAt(30);
					}
					_0024STATIC_0024RefreshStat_00242001_0024SpeedLastDone = DownloadDone;
					long num2 = 0L;
					long num3 = 0L;
					int num4 = _0024STATIC_0024RefreshStat_00242001_0024SpeedLast.Count;
					foreach (long item in _0024STATIC_0024RefreshStat_00242001_0024SpeedLast)
					{
						num2 += item * num4;
						num3 += num4;
						num4--;
					}
					Speed = (long)Math.Round((num3 > 0) ? ((double)num2 / (double)num3) : 0.0);
					long num5 = 0L;
					if (_0024STATIC_0024RefreshStat_00242001_0024SpeedLast.Count >= 10)
					{
						num5 = (long)Math.Round(_0024STATIC_0024RefreshStat_00242001_0024SpeedLast.Take(10).Average() * 0.85);
					}
					if (num5 > NetTaskSpeedLimitLow)
					{
						NetTaskSpeedLimitLow = num5;
						Logger.Info($"速度下限已提升到 {StringUtils.FormatByteSize(num5)}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
					}
					foreach (LoaderDownload task in Tasks)
					{
						task.RefreshStat();
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "刷新下载公开属性失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
					ProjectData.ClearProjectError();
				}
			}
		}

		private void StartManager()
		{
			_Closure_0024__9_002D0 arg = default(_Closure_0024__9_002D0);
			_Closure_0024__9_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__9_002D0(arg);
			if (_0024STATIC_0024StartManager_00242001_0024IsStarted_0024Init == null)
			{
				Interlocked.CompareExchange(ref _0024STATIC_0024StartManager_00242001_0024IsStarted_0024Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_0024STATIC_0024StartManager_00242001_0024IsStarted_0024Init, ref lockTaken);
				if (_0024STATIC_0024StartManager_00242001_0024IsStarted_0024Init.State == 0)
				{
					_0024STATIC_0024StartManager_00242001_0024IsStarted_0024Init.State = 2;
					_0024STATIC_0024StartManager_00242001_0024IsStarted = false;
				}
				else if (_0024STATIC_0024StartManager_00242001_0024IsStarted_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024StartManager_00242001_0024IsStarted_0024Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_0024STATIC_0024StartManager_00242001_0024IsStarted_0024Init);
				}
			}
			if (_0024STATIC_0024StartManager_00242001_0024IsStarted)
			{
				return;
			}
			_0024STATIC_0024StartManager_00242001_0024IsStarted = true;
			checked
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_ThreadStarter = [SpecialName] (int Id) =>
				{
					try
					{
						while (true)
						{
							IL_0000:
							Thread.Sleep(20);
							List<NetFile> list = new List<NetFile>();
							List<NetFile> list2 = new List<NetFile>();
							if (Id == 0 && FileRemain == 0)
							{
								AllFiles.Clear();
							}
							foreach (NetFile value in AllFiles.Values)
							{
								if (unchecked(value.Uuid % 2) != Id)
								{
									if (value.State == NetState.WaitingToDownload)
									{
										list.Add(value);
									}
									else if (value.State < NetState.Merging && value.State >= NetState.WaitingToDownload)
									{
										list2.Add(value);
									}
								}
							}
							foreach (NetFile item in list)
							{
								if (NetTaskThreadCount >= NetTaskThreadLimit)
								{
									goto IL_0000;
								}
								NetThread netThread = item.TryBeginThread();
								if ((object)netThread != null && netThread.Source.Url.Contains("bmclapi"))
								{
									Thread.Sleep(100);
								}
							}
							if (Speed < NetTaskSpeedLimitLow)
							{
								foreach (NetFile item2 in list2)
								{
									if (NetTaskThreadCount >= NetTaskThreadLimit)
									{
										break;
									}
									int num3 = 0;
									int num4 = 0;
									if ((object)item2.Threads != null)
									{
										foreach (NetThread item3 in item2.Threads.ToList())
										{
											if (item3.State < NetState.Downloading)
											{
												num3++;
											}
											else if (item3.State == NetState.Downloading)
											{
												num4++;
											}
										}
									}
									if (num3 <= num4)
									{
										NetThread netThread2 = item2.TryBeginThread();
										if ((object)netThread2 != null && netThread2.Source.Url.Contains("bmclapi"))
										{
											Thread.Sleep(100);
										}
									}
								}
							}
						}
					}
					catch (Exception ex3)
					{
						ProjectData.SetProjectError(ex3);
						Exception ex4 = ex3;
						Logger.Error(ex4, $"下载管理启动线程 {Id} 出错", (LogBehavior)5, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
						ProjectData.ClearProjectError();
					}
				};
				ModBase.RunInNewThread([SpecialName] () =>
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ThreadStarter(0);
				}, "NetManager ThreadStarter 0");
				ModBase.RunInNewThread([SpecialName] () =>
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_ThreadStarter(1);
				}, "NetManager ThreadStarter 1");
				ModBase.RunInNewThread([SpecialName] () =>
				{
					try
					{
						long num = ModBase.GetTimeMs();
						while (true)
						{
							if (NetTaskSpeedLimitHigh > 0)
							{
								Interlocked.Add(ref NetTaskSpeedLimitLeft, (long)Math.Round((double)NetTaskSpeedLimitHigh / 10.0));
							}
							RefreshStat();
							num += 100;
							long num2 = num - ModBase.GetTimeMs();
							if (num2 > 0)
							{
								Thread.Sleep((int)num2);
							}
							else
							{
								num = ModBase.GetTimeMs();
							}
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Error(ex2, "下载管理刷新线程出错", (LogBehavior)5, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
						ProjectData.ClearProjectError();
					}
				}, "NetManager StatRefresher");
			}
		}

		public List<NetFile> Start(LoaderDownload Task)
		{
			StartManager();
			object downloadCacheLock = DownloadCacheLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(downloadCacheLock);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(downloadCacheLock, ref lockTaken);
				if (!IsDownloadCacheCleared)
				{
					try
					{
						Logger.Info("开始清理下载缓存", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
						DirectoryUtils.Delete(ModBase.PathTemp + "Download", false);
						Logger.Info("下载缓存已清理", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						Logger.Warn(ex2, "清理下载缓存失败", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
						ProjectData.ClearProjectError();
					}
					IsDownloadCacheCleared = true;
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(downloadCacheLock);
				}
			}
			List<NetFile> list = new List<NetFile>();
			List<NetFile> list2 = new List<NetFile>();
			using (IEnumerator<NetFile> enumerator = Task.Files.GetEnumerator())
			{
				_Closure_0024__12_002D0 closure_0024__12_002D = default(_Closure_0024__12_002D0);
				while (enumerator.MoveNext())
				{
					closure_0024__12_002D = new _Closure_0024__12_002D0(closure_0024__12_002D);
					closure_0024__12_002D._0024VB_0024Local_File = enumerator.Current;
					NetFile value = null;
					if (AllFiles.TryGetValue(closure_0024__12_002D._0024VB_0024Local_File.LocalPath, out value) && value.State < NetState.Finished)
					{
						Logger.Trace((Func<string>)closure_0024__12_002D._Lambda_0024__0, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
						closure_0024__12_002D._0024VB_0024Local_File = value;
					}
					else
					{
						Logger.Trace((Func<string>)closure_0024__12_002D._Lambda_0024__1, (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
						AllFiles[closure_0024__12_002D._0024VB_0024Local_File.LocalPath] = closure_0024__12_002D._0024VB_0024Local_File;
						list.Add(closure_0024__12_002D._0024VB_0024Local_File);
						Interlocked.Increment(ref FileRemain);
					}
					list2.Add(closure_0024__12_002D._0024VB_0024Local_File);
					closure_0024__12_002D._0024VB_0024Local_File.Tasks.Add(Task);
				}
			}
			Task.Files = new ConcurrentQueue<NetFile>(list2);
			Tasks.Add(Task);
			return list;
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__3_002D0
	{
		public object _0024VB_0024Local_RequestResult;

		public string _0024VB_0024Local_Url;

		public HttpMethod _0024VB_0024Local_Method;

		public object _0024VB_0024Local_Content;

		public string _0024VB_0024Local_ContentType;

		public string _0024VB_0024Local_Accept;

		public int _0024VB_0024Local_Timeout;

		public string[,] _0024VB_0024Local_Headers;

		public bool _0024VB_0024Local_RequireJson;

		public Encoding _0024VB_0024Local_Encoding;

		public bool _0024VB_0024Local_SimulateBrowserHeaders;

		public int _0024VB_0024Local_FailCount;

		public Exception _0024VB_0024Local_RequestEx;

		public ThreadStart _0024I0;

		public _Closure_0024__3_002D0(_Closure_0024__3_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_RequestResult = arg0._0024VB_0024Local_RequestResult;
				_0024VB_0024Local_Url = arg0._0024VB_0024Local_Url;
				_0024VB_0024Local_Method = arg0._0024VB_0024Local_Method;
				_0024VB_0024Local_Content = arg0._0024VB_0024Local_Content;
				_0024VB_0024Local_ContentType = arg0._0024VB_0024Local_ContentType;
				_0024VB_0024Local_Accept = arg0._0024VB_0024Local_Accept;
				_0024VB_0024Local_Timeout = arg0._0024VB_0024Local_Timeout;
				_0024VB_0024Local_Headers = arg0._0024VB_0024Local_Headers;
				_0024VB_0024Local_RequireJson = arg0._0024VB_0024Local_RequireJson;
				_0024VB_0024Local_Encoding = arg0._0024VB_0024Local_Encoding;
				_0024VB_0024Local_SimulateBrowserHeaders = arg0._0024VB_0024Local_SimulateBrowserHeaders;
				_0024VB_0024Local_FailCount = arg0._0024VB_0024Local_FailCount;
				_0024VB_0024Local_RequestEx = arg0._0024VB_0024Local_RequestEx;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0()
		{
			checked
			{
				try
				{
					_0024VB_0024Local_RequestResult = NetRequestByClient(_0024VB_0024Local_Url, _0024VB_0024Local_Method, RuntimeHelpers.GetObjectValue(_0024VB_0024Local_Content), _0024VB_0024Local_ContentType, _0024VB_0024Local_Accept, _0024VB_0024Local_Timeout, _0024VB_0024Local_Headers, _0024VB_0024Local_RequireJson, _0024VB_0024Local_Encoding, _0024VB_0024Local_SimulateBrowserHeaders);
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					_0024VB_0024Local_FailCount++;
					_0024VB_0024Local_RequestEx = ex2;
					ProjectData.ClearProjectError();
				}
			}
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__34_002D0
	{
		public IEnumerable<int> _0024VB_0024Local_UsedPorts;

		public Func<int, bool> _0024I0;

		public _Closure_0024__34_002D0(_Closure_0024__34_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_UsedPorts = arg0._0024VB_0024Local_UsedPorts;
			}
		}

		[SpecialName]
		internal bool _Lambda_0024__0(int p)
		{
			return _0024VB_0024Local_UsedPorts.Contains(p);
		}
	}

	[CompilerGenerated]
	internal sealed class _Closure_0024__9_002D0
	{
		public CancellationTokenSource _0024VB_0024Local_CancelToken;

		public int _0024VB_0024Local_Timeout;

		public HttpRequestMessage _0024VB_0024Local_Request;

		public HttpResponseMessage _0024VB_0024Local_Response;

		public _Closure_0024__9_002D0(_Closure_0024__9_002D0 arg0)
		{
			if (arg0 != null)
			{
				_0024VB_0024Local_CancelToken = arg0._0024VB_0024Local_CancelToken;
				_0024VB_0024Local_Timeout = arg0._0024VB_0024Local_Timeout;
				_0024VB_0024Local_Request = arg0._0024VB_0024Local_Request;
				_0024VB_0024Local_Response = arg0._0024VB_0024Local_Response;
			}
		}

		[SpecialName]
		internal void _Lambda_0024__0(int Attempt)
		{
			_0024VB_0024Local_CancelToken?.Dispose();
			_0024VB_0024Local_CancelToken = new CancellationTokenSource(_0024VB_0024Local_Timeout);
			HttpRequestMessage val = WebExtensions.Clone(_0024VB_0024Local_Request);
			try
			{
				_0024VB_0024Local_Response = ModBase.GetResultWithTimeout(RequestClient.SendAsync(val, (HttpCompletionOption)1, _0024VB_0024Local_CancelToken.Token), _0024VB_0024Local_CancelToken, _0024VB_0024Local_Timeout);
			}
			finally
			{
				val.Dispose();
			}
		}
	}

	private static HttpClient RequestClient = null;

	private static object RequestClientLock = RuntimeHelpers.GetObjectValue(new object());

	private static HttpClient ThreadClient = new HttpClient((HttpMessageHandler)new HttpClientHandler
	{
		AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate)
	});

	public static long NetTaskSpeedLimitLow = 262144L;

	private static long? _NetTaskSpeedLimitHigh = null;

	public static long NetTaskSpeedLimitLeft = -1L;

	public static int NetTaskThreadCount = 0;

	public static NetManagerClass NetManager = new NetManagerClass();

	public static int NetTaskThreadLimit => checked(Settings.Get<int>("ToolDownloadThread") + 1);

	public static long NetTaskSpeedLimitHigh
	{
		get
		{
			checked
			{
				if (!_NetTaskSpeedLimitHigh.HasValue)
				{
					int num = Settings.Get<int>("ToolDownloadSpeed");
					if (num <= 14)
					{
						_NetTaskSpeedLimitHigh = (long)Math.Round((double)(num + 1) * 0.1 * 1024.0 * 1024.0);
					}
					else if (num <= 31)
					{
						_NetTaskSpeedLimitHigh = (long)Math.Round((double)(num - 11) * 0.5 * 1024.0 * 1024.0);
					}
					else if (num <= 41)
					{
						_NetTaskSpeedLimitHigh = unchecked((long)checked((num - 21) * 1024)) * 1024L;
					}
					else
					{
						_NetTaskSpeedLimitHigh = -1L;
					}
				}
				return _NetTaskSpeedLimitHigh.Value;
			}
		}
	}

	public static string NetRequestByClient(string Url, HttpMethod Method = null, object Content = null, string ContentType = null, string Accept = "*/*", int Timeout = 25000, string[,] Headers = null, bool RequireJson = false, Encoding Encoding = null, bool SimulateBrowserHeaders = false, bool MakeLog = true)
	{
		if (Method == null)
		{
			Method = HttpMethod.Get;
		}
		if (MakeLog)
		{
			Logger.Info($"发起网络请求（{Method.Method}，{Url}），最大超时 {Timeout}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
		}
		try
		{
			Dictionary<string, string> dictionary = EnumerableExtensions.ToDictionary<string>(Headers ?? new string[0, 0]);
			if (ContentType != null)
			{
				dictionary["Content-Type"] = ContentType;
			}
			if (Accept != null)
			{
				dictionary["Accept"] = Accept;
			}
			string text = StringExtensions.GetString(SendRequest(Url, Method, RuntimeHelpers.GetObjectValue(Content), dictionary, SimulateBrowserHeaders, Timeout, Encoding), Encoding);
			if (RequireJson)
			{
				text = text.Trim("\r\n ".ToCharArray());
				if ((!StringExtensions.StartsWithF(text, "{", false) || !StringExtensions.EndsWithF(text, "}", false)) && (!StringExtensions.StartsWithF(text, "[", false) || !StringExtensions.EndsWithF(text, "]", false)))
				{
					throw new FormatException("返回结果并非 JSON 格式：" + ((text.Length > 2000) ? (text.Substring(0, 1000) + "..." + text.Substring(checked(text.Length - 1000))) : text));
				}
			}
			return text;
		}
		catch (FormatException ex)
		{
			ProjectData.SetProjectError(ex);
			FormatException ex2 = ex;
			throw;
		}
		catch (Exception ex3)
		{
			ProjectData.SetProjectError(ex3);
			Exception ex4 = ex3;
			if (MakeLog)
			{
				Logger.Warn(ex4, $"网络请求失败（{Method.Method}，{Url}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
			}
			throw;
		}
	}

	public static string NetRequestByClientRetry(string Url, HttpMethod Method = null, string BackupUrl = null, object Content = null, string ContentType = null, string Accept = "*/*", string[,] Headers = null, bool RequireJson = false, Encoding Encoding = null, bool SimulateBrowserHeaders = false)
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		Exception ex = null;
		long timeMs = ModBase.GetTimeMs();
		checked
		{
			while (true)
			{
				try
				{
					switch (num)
					{
					case 0:
						return NetRequestByClient(Url, Method, RuntimeHelpers.GetObjectValue(Content), ContentType, Accept, 10000, Headers, RequireJson, Encoding, SimulateBrowserHeaders);
					case 1:
						Thread.Sleep(500);
						return NetRequestByClient(Url, Method, RuntimeHelpers.GetObjectValue(Content), ContentType, Accept, 30000, Headers, RequireJson, Encoding, SimulateBrowserHeaders);
					default:
						if (ModBase.GetTimeMs() - timeMs > 5500)
						{
							Thread.Sleep(500);
							return NetRequestByClient(Url, Method, RuntimeHelpers.GetObjectValue(Content), ContentType, Accept, 4000, Headers, RequireJson, Encoding, SimulateBrowserHeaders);
						}
						throw ex;
					}
				}
				catch (Exception ex2)
				{
					ProjectData.SetProjectError(ex2);
					Exception ex3 = ex2;
					ExceptionExtensions.ThrowIfCanceled(ex3);
					if (ex3 is HttpRequestCodeException)
					{
						if (((HttpRequestCodeException)ex3).StatusCode == HttpStatusCode.Forbidden)
						{
							throw;
						}
						if (((HttpRequestCodeException)ex3).StatusCode == HttpStatusCode.NotFound)
						{
							throw;
						}
						if (((HttpRequestCodeException)ex3).StatusCode == (HttpStatusCode)429)
						{
							Thread.Sleep(10000);
						}
					}
					switch (num)
					{
					case 0:
						ex = ex3;
						num++;
						ProjectData.ClearProjectError();
						break;
					case 1:
						num++;
						ProjectData.ClearProjectError();
						break;
					default:
						throw;
					}
				}
			}
		}
	}

	public static string NetRequestByClientMultiple(string Url, HttpMethod Method = null, int ThreadCount = 3, object Content = null, string ContentType = null, string Accept = "*/*", string[,] Headers = null, bool RequireJson = false, int Timeout = 25000, Encoding Encoding = null, bool SimulateBrowserHeaders = false)
	{
		_Closure_0024__3_002D0 arg = default(_Closure_0024__3_002D0);
		_Closure_0024__3_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__3_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Url = Url;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Method = Method;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Content = Content;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_ContentType = ContentType;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Accept = Accept;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Headers = Headers;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_RequireJson = RequireJson;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Timeout = Timeout;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Encoding = Encoding;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_SimulateBrowserHeaders = SimulateBrowserHeaders;
		List<Thread> list = new List<Thread>();
		CS_0024_003C_003E8__locals0._0024VB_0024Local_RequestResult = null;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_RequestEx = null;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_FailCount = 0;
		int num = 1;
		checked
		{
			while (true)
			{
				if (num <= ThreadCount)
				{
					Thread thread = new Thread([SpecialName] () =>
					{
						try
						{
							CS_0024_003C_003E8__locals0._0024VB_0024Local_RequestResult = NetRequestByClient(CS_0024_003C_003E8__locals0._0024VB_0024Local_Url, CS_0024_003C_003E8__locals0._0024VB_0024Local_Method, RuntimeHelpers.GetObjectValue(CS_0024_003C_003E8__locals0._0024VB_0024Local_Content), CS_0024_003C_003E8__locals0._0024VB_0024Local_ContentType, CS_0024_003C_003E8__locals0._0024VB_0024Local_Accept, CS_0024_003C_003E8__locals0._0024VB_0024Local_Timeout, CS_0024_003C_003E8__locals0._0024VB_0024Local_Headers, CS_0024_003C_003E8__locals0._0024VB_0024Local_RequireJson, CS_0024_003C_003E8__locals0._0024VB_0024Local_Encoding, CS_0024_003C_003E8__locals0._0024VB_0024Local_SimulateBrowserHeaders);
						}
						catch (Exception ex)
						{
							ProjectData.SetProjectError(ex);
							Exception _0024VB_0024Local_RequestEx = ex;
							CS_0024_003C_003E8__locals0._0024VB_0024Local_FailCount++;
							CS_0024_003C_003E8__locals0._0024VB_0024Local_RequestEx = _0024VB_0024Local_RequestEx;
							ProjectData.ClearProjectError();
						}
					});
					thread.Start();
					list.Add(thread);
					Thread.Sleep(num * 250);
					if (CS_0024_003C_003E8__locals0._0024VB_0024Local_RequestResult != null)
					{
						break;
					}
					num++;
					continue;
				}
				while (CS_0024_003C_003E8__locals0._0024VB_0024Local_RequestResult == null)
				{
					if (CS_0024_003C_003E8__locals0._0024VB_0024Local_FailCount == ThreadCount)
					{
						try
						{
							foreach (Thread item in list)
							{
								if (item.IsAlive)
								{
									item.Interrupt();
								}
							}
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							ProjectData.ClearProjectError();
						}
						throw CS_0024_003C_003E8__locals0._0024VB_0024Local_RequestEx;
					}
					Thread.Sleep(20);
				}
				break;
			}
			try
			{
				foreach (Thread item2 in list)
				{
					if (item2.IsAlive)
					{
						item2.Interrupt();
					}
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
			return Conversions.ToString(CS_0024_003C_003E8__locals0._0024VB_0024Local_RequestResult);
		}
	}

	public static string NetRequestByLoader(string Url, int Timeout = 45000, bool IsJson = false, bool SimulateBrowserHeaders = false)
	{
		return NetRequestByLoader(new string[1] { Url }, Timeout, IsJson, SimulateBrowserHeaders);
	}

	public static string NetRequestByLoader(IEnumerable<string> Urls, int Timeout = 45000, bool IsJson = false, bool SimulateBrowserHeaders = false)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		string text = ModMain.RequestTaskTempFolder() + "download.txt";
		LoaderDownload loaderDownload = new LoaderDownload("源码获取 " + Conversions.ToString(ModBase.GetUuid()) + "#", new List<NetFile>
		{
			new NetFile(Urls, text, new FileChecker
			{
				IsJson = IsJson
			}, SimulateBrowserHeaders)
		});
		try
		{
			loaderDownload.WaitForExitTime(Timeout, null, "连接服务器超时（第一下载源：" + Urls.First() + "）");
			string result = FileUtils.ReadAsString(text, (Encoding)null, (Type)null);
			FileUtils.Delete(text, false);
			return result;
		}
		finally
		{
			loaderDownload.Cancel();
		}
	}

	public static void NetDownloadByClient(string Url, string LocalPath, bool SimulateBrowserHeaders = false)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Logger.Info($"通过网络请求直接下载小文件：{Url} → {LocalPath}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
		int num = 0;
		while (true)
		{
			try
			{
				FileUtils.Write(LocalPath, SendRequest(Url, HttpMethod.Get, null, null, SimulateBrowserHeaders));
				break;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				FileUtils.Delete(LocalPath, false);
				ExceptionExtensions.ThrowIfCanceled(ex2);
				if (num < 2)
				{
					num = checked(num + 1);
					ProjectData.ClearProjectError();
					continue;
				}
				throw new HttpRequestException($"通过网络请求直接下载小文件失败（{LocalPath}）", ex2);
			}
		}
	}

	public static void NetDownloadByLoader(string Url, string LocalPath, ModLoader.LoaderBase LoaderToSyncProgress = null, FileChecker Check = null, bool SimulateBrowserHeaders = false)
	{
		NetDownloadByLoader(new string[1] { Url }, LocalPath, LoaderToSyncProgress, Check, SimulateBrowserHeaders);
	}

	public static void NetDownloadByLoader(IEnumerable<string> Urls, string LocalPath, ModLoader.LoaderBase LoaderToSyncProgress = null, FileChecker Check = null, bool SimulateBrowserHeaders = false)
	{
		LoaderDownload loaderDownload = new LoaderDownload($"文件下载 {ModBase.GetUuid()}#", new List<NetFile>
		{
			new NetFile(Urls, LocalPath, Check, SimulateBrowserHeaders)
		});
		try
		{
			loaderDownload.WaitForExit(null, LoaderToSyncProgress);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception innerException = ex;
			throw new WebException("多线程直接下载文件失败（第一下载源：" + Urls.First() + "）", innerException);
		}
		finally
		{
			loaderDownload.Cancel();
		}
	}

	private static byte[] SendRequest(string Url, HttpMethod Method, object Content = null, Dictionary<string, string> Headers = null, bool SimulateBrowserHeaders = false, int Timeout = 25000, Encoding Encoding = null)
	{
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Expected O, but got Unknown
		//IL_03a7: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00a2: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		_Closure_0024__9_002D0 arg = default(_Closure_0024__9_002D0);
		_Closure_0024__9_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__9_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Timeout = Timeout;
		if (ModBase.RunInUi() && !Url.Contains("//127."))
		{
			throw new Exception("在 UI 线程执行了网络请求");
		}
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Request = null;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken = null;
		CS_0024_003C_003E8__locals0._0024VB_0024Local_Response = null;
		try
		{
			Directory.CreateDirectory(ModBase.PathTemp + "Cache\\Http\\");
			object requestClientLock = RequestClientLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(requestClientLock);
			bool lockTaken = false;
			try
			{
				Monitor.Enter(requestClientLock, ref lockTaken);
				if (RequestClient == null)
				{
					RequestClient = ClientExtensions.CreateClient((ICacheStore)new FileStore(ModBase.PathTemp + "Cache/Http/"), (HttpMessageHandler)new HttpClientHandler
					{
						AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate),
						UseCookies = false
					});
				}
			}
			finally
			{
				if (lockTaken)
				{
					Monitor.Exit(requestClientLock);
				}
			}
			Url = ModSecret.SecretCdnSign(Url);
			CS_0024_003C_003E8__locals0._0024VB_0024Local_Request = new HttpRequestMessage(Method, Url);
			if (Content != null && CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Method != HttpMethod.Get && CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Method != HttpMethod.Head)
			{
				if (Content is HttpContent)
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Content = WebExtensions.Clone((HttpContent)Content);
				}
				else if (Content is byte[])
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Content = (HttpContent)new ByteArrayContent((byte[])Content);
				}
				else if (Content != null)
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Content = (HttpContent)new ByteArrayContent((Encoding ?? Encoding.UTF8).GetBytes(Content.ToString()));
				}
			}
			foreach (KeyValuePair<string, string> item in Headers ?? new Dictionary<string, string>())
			{
				if (Operators.CompareString(StringExtensions.Lower(item.Key), "content-type", TextCompare: false) == 0)
				{
					HttpContent content = CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Content;
					if (content != null)
					{
						((HttpHeaders)content.Headers).TryAddWithoutValidation(item.Key, item.Value);
					}
				}
				else
				{
					((HttpHeaders)CS_0024_003C_003E8__locals0._0024VB_0024Local_Request.Headers).TryAddWithoutValidation(item.Key, item.Value);
				}
			}
			ModSecret.SecretHeadersSign(Url, ref CS_0024_003C_003E8__locals0._0024VB_0024Local_Request, SimulateBrowserHeaders);
			Retrier.Attempt((Action<int>)([SpecialName] (int Attempt) =>
			{
				CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken?.Dispose();
				CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken = new CancellationTokenSource(CS_0024_003C_003E8__locals0._0024VB_0024Local_Timeout);
				HttpRequestMessage val3 = WebExtensions.Clone(CS_0024_003C_003E8__locals0._0024VB_0024Local_Request);
				try
				{
					CS_0024_003C_003E8__locals0._0024VB_0024Local_Response = ModBase.GetResultWithTimeout(RequestClient.SendAsync(val3, (HttpCompletionOption)1, CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken.Token), CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken, CS_0024_003C_003E8__locals0._0024VB_0024Local_Timeout);
				}
				finally
				{
					val3.Dispose();
				}
			}), (uint?)2u, (Func<int, TimeSpan>)([SpecialName] (int a0) => ((VB_0024AnonymousDelegate_0<TimeSpan>)([SpecialName] () => TimeSpan.FromMilliseconds(200.0)))()), (Func<Exception, bool>)([SpecialName] (Exception ex) => ex is FileNotFoundException || ex is IOException), (Action)null, default(CancellationToken));
			CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken.Dispose();
			CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken = new CancellationTokenSource(CS_0024_003C_003E8__locals0._0024VB_0024Local_Timeout);
			byte[] array;
			using (Stream stream = ModBase.GetResultWithTimeout(CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.Content.ReadAsStreamAsync(), CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken, CS_0024_003C_003E8__locals0._0024VB_0024Local_Timeout))
			{
				using MemoryStream memoryStream = new MemoryStream();
				ModBase.GetResultWithTimeout(stream.CopyToAsync(memoryStream, 81920, CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken.Token), CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken, CS_0024_003C_003E8__locals0._0024VB_0024Local_Timeout);
				array = memoryStream.ToArray();
			}
			if (!CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.IsSuccessStatusCode)
			{
				string @string = StringExtensions.GetString(array, Encoding);
				throw new HttpRequestCodeException($"错误码 {CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.StatusCode} ({(int)CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.StatusCode})，{Method}，{Url}" + (string.IsNullOrEmpty(@string) ? "" : ("\r\n" + @string)), CS_0024_003C_003E8__locals0._0024VB_0024Local_Response.StatusCode, @string);
			}
			return array;
		}
		catch (HttpRequestException val)
		{
			ProjectData.SetProjectError((Exception)val);
			HttpRequestException val2 = val;
			throw;
		}
		catch (TimeoutException ex2)
		{
			ProjectData.SetProjectError(ex2);
			TimeoutException ex3 = ex2;
			throw new WebException($"连接服务器超时，请稍后再试，或使用 VPN 改善网络环境（{Method}, {Url}）", WebExceptionStatus.Timeout);
		}
		catch (Exception ex4)
		{
			ProjectData.SetProjectError(ex4);
			Exception ex5 = ex4;
			if (!(ex5 is TaskCanceledException))
			{
				ExceptionExtensions.ThrowIfCanceled(ex5);
			}
			if (ExceptionExtensions.IsBadNetwork(ex5))
			{
				throw new WebException($"网络请求失败，请稍后再试，或使用 VPN 改善网络环境（{Method}, {Url}）", WebExceptionStatus.Timeout);
			}
			throw new Exception($"网络请求出现意外异常（{Method}, {Url}）", ex5);
		}
		finally
		{
			HttpRequestMessage _0024VB_0024Local_Request = CS_0024_003C_003E8__locals0._0024VB_0024Local_Request;
			if (_0024VB_0024Local_Request != null)
			{
				_0024VB_0024Local_Request.Dispose();
			}
			CS_0024_003C_003E8__locals0._0024VB_0024Local_CancelToken?.Dispose();
			HttpResponseMessage _0024VB_0024Local_Response = CS_0024_003C_003E8__locals0._0024VB_0024Local_Response;
			if (_0024VB_0024Local_Response != null)
			{
				_0024VB_0024Local_Response.Dispose();
			}
		}
	}

	public static void UpdateNetTaskSpeedLimitHigh(int Value)
	{
		_NetTaskSpeedLimitHigh = null;
	}

	public static bool HasDownloadingTask(bool IgnoreCustomDownload = false)
	{
		return ((IEnumerable<ModLoader.LoaderBase>)ModLoader.LoaderTaskbar).Any([SpecialName] (ModLoader.LoaderBase Task) => Task.Show && Task.State == ModBase.LoadState.Loading && (!IgnoreCustomDownload || !Task.Name.ToString().Contains("自定义下载")));
	}

	public static int FindFreePort()
	{
		TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 0);
		tcpListener.Start();
		int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
		tcpListener.Stop();
		return port;
	}

	public static List<int> GetUsedPorts()
	{
		IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
		List<int> list = new List<int>();
		list.AddRange(from ep in iPGlobalProperties.GetActiveTcpListeners()
			select ep.Port);
		list.AddRange(from ep in iPGlobalProperties.GetActiveUdpListeners()
			select ep.Port);
		list.AddRange(from conn in iPGlobalProperties.GetActiveTcpConnections()
			select conn.LocalEndPoint.Port);
		return list.Distinct().ToList();
	}

	public static List<int> FindFreePorts(int ConsecutiveCount, params int[] ExtraBlackLists)
	{
		_Closure_0024__34_002D0 arg = default(_Closure_0024__34_002D0);
		_Closure_0024__34_002D0 CS_0024_003C_003E8__locals0 = new _Closure_0024__34_002D0(arg);
		CS_0024_003C_003E8__locals0._0024VB_0024Local_UsedPorts = GetUsedPorts().Concat(ExtraBlackLists);
		checked
		{
			int num = 12000 + ModBase.RandomInteger(0, 1000);
			int num2 = 65000 - ConsecutiveCount;
			for (int i = num; i <= num2; i++)
			{
				IEnumerable<int> source = Enumerable.Range(i, ConsecutiveCount);
				if (!source.Any([SpecialName] (int p) => CS_0024_003C_003E8__locals0._0024VB_0024Local_UsedPorts.Contains(p)))
				{
					return source.ToList();
				}
			}
			throw new Exception("未能找到可用的端口！");
		}
	}

	public static int Ping(string Ip, int Timeout = 10000, bool MakeLog = true)
	{
		PingReply pingReply;
		int result;
		try
		{
			pingReply = new Ping().Send(Ip);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			if (MakeLog)
			{
				Logger.Info($"Ping {Ip} 失败：{ex2.Message}", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
			}
			result = -1;
			ProjectData.ClearProjectError();
			goto IL_008e;
		}
		if (pingReply.Status == IPStatus.Success)
		{
			if (MakeLog)
			{
				Logger.Info($"Ping {Ip} 结束：{pingReply.RoundtripTime}ms", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
			}
			result = checked((int)pingReply.RoundtripTime);
		}
		else
		{
			if (MakeLog)
			{
				Logger.Info($"Ping {Ip} 失败", (LogBehavior)0, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
			}
			result = -1;
		}
		goto IL_008e;
		IL_008e:
		return result;
	}

	public static void ShouldValidateSslCertificateOnLogin(bool Enabled)
	{
		ServicePointManager.ServerCertificateValidationCallback = [SpecialName] (object Sender, X509Certificate Certificate, X509Chain Chain, SslPolicyErrors Failure) =>
		{
			HttpWebRequest httpWebRequest = Sender as HttpWebRequest;
			if (Failure == SslPolicyErrors.None)
			{
				return true;
			}
			Logger.Warn($"未通过 SSL 证书验证（{Failure}），提供的证书为 {Certificate?.Subject}，URL：{httpWebRequest?.Address}", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModNet.vb");
			if (httpWebRequest == null)
			{
				return !Enabled;
			}
			return (httpWebRequest.Address.Host.Contains("xboxlive") || httpWebRequest.Address.Host.Contains("minecraftservices")) && !Enabled;
		};
	}
}
