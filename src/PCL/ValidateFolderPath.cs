using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

public class ValidateFolderPath : Validate
{
	public bool UseMinecraftCharCheck { get; set; }

	public ValidateFolderPath()
	{
		UseMinecraftCharCheck = true;
	}

	public ValidateFolderPath(bool UseMinecraftCharCheck)
	{
		this.UseMinecraftCharCheck = true;
		this.UseMinecraftCharCheck = UseMinecraftCharCheck;
	}

	public override string Validate(string Str)
	{
		Str = Str.Replace("/", "\\");
		if (!StringExtensions.EndsWithF(PathUtils.RemoveSlashSuffix(Str), ":", false))
		{
			Str = PathUtils.RemoveSlashSuffix(Str);
		}
		string text = new ValidateNullOrWhiteSpace().Validate(Str);
		if (Operators.CompareString(text, "", TextCompare: false) != 0)
		{
			return text;
		}
		text = new ValidateLength(1, 254).Validate(Str);
		if (Operators.CompareString(text, "", TextCompare: false) != 0)
		{
			return text;
		}
		string text2 = StringExtensions.RegexSeek(Str, ".{2,}~\\d", RegexOptions.None);
		if (text2 != null)
		{
			return $"文件夹名不能包含 {text2} 这一特殊格式！";
		}
		if (!StringExtensions.StartsWithF(Str, "\\\\Mac\\", false))
		{
			foreach (string item in from p in Environment.GetLogicalDrives()
				where DirectoryUtils.Exists(p)
				select p)
			{
				string text3 = Conversions.ToString(StringExtensions.Upper(item).First()) + ":\\";
				if (Operators.CompareString(StringExtensions.Upper(Str), text3, TextCompare: false) == 0)
				{
					return "";
				}
				if (!StringExtensions.StartsWithF(Str, text3, true))
				{
					continue;
				}
				goto IL_0146;
			}
			return "文件夹路径头存在错误！";
		}
		goto IL_0146;
		IL_0146:
		int num = ((!StringExtensions.StartsWithF(Str, "\\\\Mac\\", false)) ? 1 : 2);
		checked
		{
			int num2 = StringExtensions.Split(Str, "\\", false).Count() - 1;
			for (int i = num; i <= num2; i++)
			{
				string text4 = StringExtensions.Split(Str, "\\", false)[i];
				if (Operators.CompareString(new ValidateNullOrWhiteSpace().Validate(text4), "", TextCompare: false) != 0)
				{
					return "文件夹路径存在错误！";
				}
				string text5 = new ValidateExcept(new string(Path.GetInvalidFileNameChars()) + (UseMinecraftCharCheck ? "!;" : ""), "路径中存在无效字符！").Validate(text4);
				if (Operators.CompareString(text5, "", TextCompare: false) != 0)
				{
					return text5;
				}
				if (StringExtensions.StartsWithF(text4, " ", false))
				{
					return "文件夹名不能以空格开头！";
				}
				if (StringExtensions.EndsWithF(text4, " ", false))
				{
					return "文件夹名不能以空格结尾！";
				}
				if (StringExtensions.EndsWithF(text4, ".", false))
				{
					return "文件夹名不能以小数点结尾！";
				}
				string text6 = new ValidateExceptSame(new string[25]
				{
					"CON", "PRN", "AUX", "CLOCK$", "NUL", "COM0", "COM1", "COM2", "COM3", "COM4",
					"COM5", "COM6", "COM7", "COM8", "COM9", "LPT0", "LPT1", "LPT2", "LPT3", "LPT4",
					"LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
				}, "文件夹名不可为 %！").Validate(text4);
				if (Operators.CompareString(text6, "", TextCompare: false) != 0)
				{
					return text6;
				}
			}
			return "";
		}
	}
}
