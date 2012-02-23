using System;
using stefc.gatelib.contract;
using System.Collections;
using System.Collections.Generic;

namespace stefc.gatelib
{
	public class FPGA : IFieldProgrammableGateArray
	{
		private readonly int input; 
		private readonly int output;
		private readonly int gates;
		
		private readonly List<IFlowGate> network;
		
		public FPGA (int input, int output, int gates)
		{	
			this.input=input;
			this.output=output;
			this.gates=gates;
			
			this.network = new List<IFlowGate>(gates);
			for(int i=0; i<gates; i++)
				this.network.Add(new NandGate());
		}
		
		public BitArray Output(BitArray input)
		{
			BitArray result = new BitArray(this.output);
			for(int i=0; i<this.output; i++)
			{
				this.network[this.gates-i-1].Out += CreatePin(result,i);
			}
			
			
			
			for(int i=0; i<this.output; i++)
			{
				this.network[this.gates-i-1].Out -= CreatePin(result,i);
			}
			
			return result;
		}
		
		private Action<bool> CreatePin(BitArray output, int port)
		{
			return signal => output.Set(port,signal);
		}
	}
}

