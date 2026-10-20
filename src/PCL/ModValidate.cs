using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace PCL;

[StandardModule]
public sealed class ModValidate
{
	public static string Validate(string Text, IEnumerable<Validate> ValidateRules)
	{
		string text = "";
		foreach (Validate ValidateRule in ValidateRules)
		{
			text = ValidateRule.Validate(Text);
			if (text == null)
			{
				return "";
			}
			if (Operators.CompareString(text, "", TextCompare: false) != 0)
			{
				return text;
			}
		}
		return text;
	}
}
