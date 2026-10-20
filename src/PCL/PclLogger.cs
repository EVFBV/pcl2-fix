using System;
using System.Runtime.CompilerServices;
using System.Threading;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class PclLogger : FileLogger
{
	[SpecialName]
	private bool _0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger_0024Init;

	public override string Format(string Text, LogLevel Level, string FilePath, Exception Ex)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Text = ((BaseLogger)this).Format(Text, Level, FilePath, Ex);
		Text = ModMinecraft.FilterUserName(ModMinecraft.FilterAccessToken(Text, '*'), '*');
		return Text;
	}

	public override void HandleBehavior(string RawMessage, string FormattedMessage, LogBehavior Behavior, Exception Ex)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected I4, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Invalid comparison between Unknown and I4
		if (ModBase.IsProgramEnding)
		{
			return;
		}
		((BaseLogger)this).HandleBehavior(RawMessage, FormattedMessage, Behavior, Ex);
		string raw = ((Ex == null) ? RawMessage : (((RawMessage == null) ? "" : $"{RawMessage}：") + ExceptionExtensions.GetDisplay(Ex, false)));
		raw = ModMinecraft.FilterUserName(ModMinecraft.FilterAccessToken(raw, '*'), '*');
		string raw2 = ((Ex == null) ? RawMessage : (((RawMessage == null) ? "" : $"{RawMessage}：") + ExceptionExtensions.GetDisplay(Ex, true)));
		raw2 = ModMinecraft.FilterUserName(ModMinecraft.FilterAccessToken(raw2, '*'), '*');
		switch ((int)Behavior)
		{
		case 1:
			if (ModBase.ModeDebug)
			{
				ModMain.Hint("[调试模式] " + raw, ModMain.HintType.Blue, Log: false);
			}
			break;
		case 2:
			ModMain.Hint(raw, ModMain.HintType.Red, Log: false);
			break;
		case 3:
			ModMain.MyMsgBox(raw2, "错误", "确定", "", "", IsWarn: true);
			break;
		case 4:
			if (PageSetupSystem.IsLauncherNewest().GetValueOrDefault())
			{
				if (ModMain.MyMsgBox(raw2 + "\r\n\r\n是否反馈此问题？如果不反馈，这个问题可能永远无法得到解决！", "错误", "反馈", "取消", "", IsWarn: true) == 1)
				{
					ModMain.Feedback(ShowMsgbox: false, ForceOpenLog: true);
				}
			}
			else
			{
				ModMain.MyMsgBox(raw2 + "\r\n\r\n将 PCL 更新至最新版或许可以解决这个问题……", "错误", "确定", "", "", IsWarn: true);
			}
			break;
		case 5:
		{
			if (_0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger_0024Init == null)
			{
				Interlocked.CompareExchange(ref _0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger_0024Init, new StaticLocalInitFlag(), null);
			}
			bool lockTaken = false;
			try
			{
				Monitor.Enter(_0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger_0024Init, ref lockTaken);
				if (_0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger_0024Init.State == 0)
				{
					_0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger_0024Init.State = 2;
					_0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger = true;
				}
				else if (_0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger_0024Init.State == 2)
				{
					throw new IncompleteInitialization();
				}
			}
			finally
			{
				_0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger_0024Init.State = 1;
				if (lockTaken)
				{
					Monitor.Exit(_0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger_0024Init);
				}
			}
			if (_0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger)
			{
				_0024STATIC_0024HandleBehavior_00242041EE1181E91280CD_0024FirstTrigger = false;
				if (PageSetupSystem.IsLauncherNewest().GetValueOrDefault())
				{
					if (Interaction.MsgBox(raw2 + "\r\n\r\n是否反馈此问题？如果不反馈，这个问题可能永远无法得到解决！", MsgBoxStyle.YesNo | MsgBoxStyle.Critical, "错误") == MsgBoxResult.Yes)
					{
						ModMain.Feedback(ShowMsgbox: false, ForceOpenLog: true);
					}
				}
				else
				{
					Interaction.MsgBox(raw2 + "\r\n\r\n将 PCL 更新至最新版或许可以解决这个问题……", MsgBoxStyle.Critical, "错误");
				}
			}
			else
			{
				Thread.Sleep(2000);
			}
			FormMain.EndProgramForce(ModBase.ProcessReturnValues.Exception);
			break;
		}
		}
		if ((int)Behavior >= 2)
		{
			ModMain.Telemetry("错误日志", "Exception", raw2);
		}
	}
}
