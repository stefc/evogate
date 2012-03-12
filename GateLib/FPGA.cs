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
			Console.WriteLine ("input");
			DoInput(input);
				
			DoWire(false);
			
			DoOutput(false,result);
			
			return result;
		}
		
		private void DoWire(bool connecting)
		{
			Console.WriteLine ("DoWire");
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
					PinWire pin = this.wiring.GetInWire(i,j);
					if(pin!=PinWire.None)
						Console.WriteLine("[in]{0},{1} => {2}", i,j, pin);
			
					IFlowGate gate = this.network[j];
						
					if(pin == PinWire.A)
						gate.A(input[i]);
					else if(pin == PinWire.B)
						gate.B(input[i]);
					else if(pin == PinWire.Both)
					{
						gate.A(input[i]);
						gate.B(input[i]);
					}
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
				if(pin == PinWire.A)
					this.network[src].A(s);
				else if(pin == PinWire.B)
					this.network[src].B(s);
				else if(pin == PinWire.Both)
				{
					this.network[src].A(s);
					this.network[src].B(s);
				}
			};
		}
	}
}

