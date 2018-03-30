using System;

namespace stefc.gatelib.contract
{
	public interface IFullAdder
	{
		Tuple<bool,bool> Output(Tuple<bool,bool,bool> input);
	}
}

