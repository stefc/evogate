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
			
			// empty signals on gates
			foreach(IFlowGate gate in this.network)
				gate.Reset();
			
			DoOutput(true, result);
			
			// wiring
			DoWire(true);
			
			// input 
			DoInput(input);
				
			DoWire(false);
			
			DoOutput(false,result);
			
			return result;
		}
		
		private void DoWire(bool connecting)
		{
			for(int i=0; i<this.wiring.Gates-this.wiring.Output; i++)
			{
				for(int j=0; j<this.wiring.Gates; j++)
				{
					PinWire pin = this.wiring.GetWire(i,j);
					if(pin != PinWire.None)
					{
						IFlowGate gate = this.network[i];
						Action<bool> pinAction = Connect(j,pin);
						
						if(connecting)
							gate.Out += pinAction;
						else
							gate.Out -= pinAction;
					}
				}
			}
		}
		
		private void DoOutput(bool connecting, BitArray result)
		{
			for(int i=0; i<this.wiring.Output; i++)
			{
				IFlowGate gate = this.network[this.wiring.Gates-i-1];
				Action<bool> pinAction = CreatePin(result,i);
				if(connecting)
					gate.Out += pinAction;
				else
					gate.Out -= pinAction;
			}
		}	
		
		private void DoInput(BitArray input)
		{
			for(int i=0; i<this.wiring.Input; i++)
			{
				for(int j=0; j<this.wiring.Gates; j++)
				{
					PinWire pin = this.wiring[true,i,j];
					
					IFlowGate dest = this.network[j];
					Signal(dest,pin,input[i]);	
				}
			}
		}
		
		
		private Action<bool> CreatePin(BitArray output, int port)
		{
			return signal => 
			{
				output.Set(port,signal);
			};
		}
		
		private Action<bool> Connect(int src, PinWire pin)
		{
			return (s) => 
			{
				Signal(this.network[src],pin,s);
			};
		}
		
		private void Signal(IFlowGate gate, PinWire pin, bool s)
		{
			if(pin == PinWire.A)
				gate.A(s);
			else if(pin == PinWire.B)
				gate.B(s);
			else if(pin == PinWire.Both)
			{
				gate.A(s);
				gate.B(s);
			}
		}
	}
}

