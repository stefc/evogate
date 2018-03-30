using System;

namespace stefc.gatelib.contract
{
	public interface IQuadAdder
	{
		Tuple<byte,bool> Output(Tuple<byte,byte,bool> input);
	}
}

