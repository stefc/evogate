using System;
using stefc.gatelib.contract;
using System.Collections;
using System.Collections.Generic;

namespace stefc.gatelib
{
	public class FPGA : IFieldProgrammableGateArray
	{
		private readonly Wiring wiring;
		private readonly List<IFlowGate> network;
		
		public FPGA (Wiring wiring)
		{	
			this.wiring = wiring;
			
			this.network = new List<IFlowGate>(wiring.Gates);
			for(int i=0; i<wiring.Gates; i++)
				this.network.Add(new NandGate());
			
		}
		
		public BitArray Output(BitArray input)
		{
			BitArray result = new BitArray(this.wiring.Output);
			for(int i=0; i<this.wiring.Output; i++)
			{
				this.network[this.wiring.Gates-i-1].Out += CreatePin(result,i);
			}
			
			
			
			for(int i=0; i<this.wiring.Output; i++)
			{
				this.network[this.wiring.Gates-i-1].Out -= CreatePin(result,i);
			}
			
			return result;
		}
		
		private Action<bool> CreatePin(BitArray output, int port)
		{
			return signal => output.Set(port,signal);
		}
	}
}

