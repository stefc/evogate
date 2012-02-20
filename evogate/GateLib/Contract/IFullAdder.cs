using System;

namespace stefc.gatelib
{
	public interface IFullAdder
	{
		Tuple<bool,bool> Output(Tuple<bool,bool,bool> input);
	}
}

