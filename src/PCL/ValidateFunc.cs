using System;

namespace PCL;

public class ValidateFunc : Validate
{
	private readonly Func<string, string> _func;

	public ValidateFunc(Func<string, string> F)
	{
		_func = F;
	}

	public override string Validate(string Str)
	{
		return _func(Str);
	}
}
