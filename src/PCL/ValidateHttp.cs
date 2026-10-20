using System.Text.RegularExpressions;
using MeloongCore.Extensions;

namespace PCL;

public class ValidateHttp : Validate
{
	public override string Validate(string Str)
	{
		if (StringExtensions.EndsWithF(Str, "/", false))
		{
			Str = Str.Substring(0, checked(Str.Length - 1));
		}
		if (!StringExtensions.RegexCheck(Str, "^(http[s]?)\\://", RegexOptions.None))
		{
			return "输入的网址无效！";
		}
		return "";
	}
}
