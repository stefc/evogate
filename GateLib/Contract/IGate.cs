using System;

namespace stefc.gatelib.contract
{
	public interface IGate
	{
		bool Output(Tuple<bool,bool> input);
	}
	
	public interface IFlowGate
	{
		void Reset();
		event Action<bool> Out;
		void A(bool input);
        void B(bool input);
	}
}

