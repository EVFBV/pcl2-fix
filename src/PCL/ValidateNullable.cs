using Microsoft.VisualBasic;

namespace PCL;

public class ValidateNullable : Validate
{
	public override string Validate(string Str)
	{
		if (Information.IsNothing(Str) || string.IsNullOrEmpty(Str))
		{
			return null;
		}
		return "";
	}
}
