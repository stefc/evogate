using System;

namespace stefc.gatelib.contract
{
	public interface IGate
	{
		bool Output(Tuple<bool,bool> input);
	}
}

