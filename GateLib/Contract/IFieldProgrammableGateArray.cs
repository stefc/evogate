using System;
using System.Collections;

namespace stefc.gatelib.contract
{
	public interface IFieldProgrammableGateArray
	{
		BitArray Output(BitArray input);
	}
}

