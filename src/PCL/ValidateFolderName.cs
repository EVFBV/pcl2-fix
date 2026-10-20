using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class ValidateFolderName : Validate
{
	private readonly IEnumerable<string> PathIgnore;

	private readonly List<string> IgnoreList;

	public string Folder { get; set; }

	public bool UseMinecraftCharCheck { get; set; }

	public ValidateFolderName()
	{
		UseMinecraftCharCheck = true;
	}

	public ValidateFolderName(string Folder, bool UseMinecraftCharCheck = true, List<string> IgnoreList = null)
	{
		this.UseMinecraftCharCheck = true;
		int try000d_dispatch = -1;
		int num = default(int);
		int num2 = default(int);
		int num3 = default(int);
		while (true)
		{
			try
			{
				/*Note: ILSpy has introduced the following switch to emulate a goto from catch-block to try-block*/;
				switch (try000d_dispatch)
				{
				default:
					num = 1;
					this.Folder = Folder;
					goto IL_0016;
				case 109:
					{
						num2 = num;
						switch (num3)
						{
						case 1:
							break;
						default:
							goto end_IL_000d;
						}
						int num4 = num2 + 1;
						num2 = 0;
						switch (num4)
						{
						case 1:
							break;
						case 2:
							goto IL_0016;
						case 3:
							goto IL_001f;
						case 4:
							goto IL_0026;
						case 5:
							goto end_IL_000d_2;
						default:
							goto end_IL_000d;
						case 6:
							goto end_IL_000d_3;
						}
						goto default;
					}
					IL_0026:
					num = 4;
					PathIgnore = DirectoryUtils.EnumerateDirectories(Folder, false, "*");
					break;
					IL_0016:
					num = 2;
					this.UseMinecraftCharCheck = UseMinecraftCharCheck;
					goto IL_001f;
					IL_001f:
					ProjectData.ClearProjectError();
					num3 = 1;
					goto IL_0026;
					end_IL_000d_2:
					break;
				}
				num = 5;
				this.IgnoreList = IgnoreList;
				break;
				end_IL_000d:;
			}
			catch (object obj) when (obj is Exception && num3 != 0 && num2 == 0)
			{
				ProjectData.SetProjectError((Exception)obj);
				try000d_dispatch = 109;
				continue;
			}
			throw ProjectData.CreateProjectError(-2146828237);
			continue;
			end_IL_000d_3:
			break;
		}
		if (num2 != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	public override string Validate(string Str)
	{
		string result;
		try
		{
			string text = new ValidateNullOrWhiteSpace().Validate(Str);
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				result = text;
			}
			else if (StringExtensions.StartsWithF(Str, " ", false))
			{
				result = "文件夹名不能以空格开头！";
			}
			else if (StringExtensions.EndsWithF(Str, " ", false))
			{
				result = "文件夹名不能以空格结尾！";
			}
			else
			{
				text = new ValidateLength(1, 100).Validate(Str);
				if (Operators.CompareString(text, "", TextCompare: false) != 0)
				{
					result = text;
				}
				else if (StringExtensions.EndsWithF(Str, ".", false))
				{
					result = "文件夹名不能以小数点结尾！";
				}
				else
				{
					string text2 = new ValidateExcept(new string(Path.GetInvalidFileNameChars()) + (UseMinecraftCharCheck ? "!;" : ""), "文件夹名不可包含 % 字符！").Validate(Str);
					if (Operators.CompareString(text2, "", TextCompare: false) != 0)
					{
						result = text2;
					}
					else
					{
						string text3 = new ValidateExceptSame(new string[25]
						{
							"CON", "PRN", "AUX", "CLOCK$", "NUL", "COM0", "COM1", "COM2", "COM3", "COM4",
							"COM5", "COM6", "COM7", "COM8", "COM9", "LPT0", "LPT1", "LPT2", "LPT3", "LPT4",
							"LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
						}, "文件夹名不可为 %！", IgnoreCase: true).Validate(Str);
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							result = text3;
						}
						else if (StringExtensions.RegexCheck(Str, ".{2,}~\\d", RegexOptions.None))
						{
							result = "文件夹名不能包含这一特殊格式！";
						}
						else
						{
							List<string> list = new List<string>();
							if (PathIgnore != null)
							{
								foreach (string item in PathIgnore)
								{
									string lastPart = PathUtils.GetLastPart(item);
									if (IgnoreList == null || !IgnoreList.Contains(lastPart, StringComparer.OrdinalIgnoreCase))
									{
										list.Add(lastPart);
									}
								}
							}
							string text4 = new ValidateExceptSame(list, "不可与现有文件夹重名！", IgnoreCase: true).Validate(Str);
							result = ((Operators.CompareString(text4, "", TextCompare: false) == 0) ? "" : text4);
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "检查文件夹名出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModValidate.vb");
			result = "错误：" + ex2.Message;
			ProjectData.ClearProjectError();
		}
		return result;
	}
}
