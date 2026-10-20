using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class ValidateFileName : Validate
{
	[CompilerGenerated]
	private object _RequireParentFolderExists;

	public string Name { get; set; }

	public bool UseMinecraftCharCheck { get; set; }

	public bool IgnoreCase { get; set; }

	public string ParentFolder { get; set; }

	public object RequireParentFolderExists
	{
		[CompilerGenerated]
		get
		{
			return _RequireParentFolderExists;
		}
		[CompilerGenerated]
		set
		{
			_RequireParentFolderExists = RuntimeHelpers.GetObjectValue(value);
		}
	}

	public ValidateFileName()
	{
		UseMinecraftCharCheck = true;
		IgnoreCase = true;
		ParentFolder = null;
		RequireParentFolderExists = true;
	}

	public ValidateFileName(string Name, bool UseMinecraftCharCheck = true, bool IgnoreCase = true)
	{
		this.UseMinecraftCharCheck = true;
		this.IgnoreCase = true;
		ParentFolder = null;
		RequireParentFolderExists = true;
		this.Name = Name;
		this.IgnoreCase = IgnoreCase;
		this.UseMinecraftCharCheck = UseMinecraftCharCheck;
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
				result = "文件名不能以空格开头！";
			}
			else if (StringExtensions.EndsWithF(Str, " ", false))
			{
				result = "文件名不能以空格结尾！";
			}
			else
			{
				text = new ValidateLength(1, 253).Validate(Str + ParentFolder);
				if (Operators.CompareString(text, "", TextCompare: false) != 0)
				{
					result = text;
				}
				else if (StringExtensions.EndsWithF(Str, ".", false))
				{
					result = "文件名不能以小数点结尾！";
				}
				else
				{
					string text2 = new ValidateExcept(new string(Path.GetInvalidFileNameChars()) + (UseMinecraftCharCheck ? "!;" : ""), "文件名不可包含 % 字符！").Validate(Str);
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
						}, "文件名不可为 %！", IgnoreCase: true).Validate(Str);
						if (Operators.CompareString(text3, "", TextCompare: false) != 0)
						{
							result = text3;
						}
						else if (StringExtensions.RegexCheck(Str, ".{2,}~\\d", RegexOptions.None))
						{
							result = "文件名不能包含这一特殊格式！";
						}
						else
						{
							if (ParentFolder == null)
							{
								goto IL_02a8;
							}
							DirectoryInfo info = DirectoryUtils.GetInfo(ParentFolder);
							if (info.Exists)
							{
								string text4 = new ValidateExceptSame(from f in info.EnumerateFiles("*")
									select f.Name, "不可与现有文件重名！", IgnoreCase).Validate(Str);
								if (Operators.CompareString(text4, "", TextCompare: false) == 0)
								{
									goto IL_02a8;
								}
								result = text4;
							}
							else
							{
								if (!Conversions.ToBoolean(RequireParentFolderExists))
								{
									goto IL_02a8;
								}
								result = $"父文件夹不存在：{ParentFolder}";
							}
						}
					}
				}
			}
			goto end_IL_0000;
			IL_02a8:
			result = "";
			end_IL_0000:;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Logger.Warn(ex2, "检查文件名出错", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModValidate.vb");
			result = "错误：" + ex2.Message;
			ProjectData.ClearProjectError();
		}
		return result;
	}
}
