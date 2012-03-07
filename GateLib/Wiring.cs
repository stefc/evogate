using System;
using System.Collections;

namespace stefc.gatelib
{
	public class Wiring
	{
		private readonly int input;
		private readonly int output;
		private readonly int gates;
		
		private readonly BitArray wiring;
		
		public Wiring (int input, int output, int gates)
		{
			if(input<=0) throw new ArgumentException("input");
			if(output<=0) throw new ArgumentException("output");
			if(gates<=0) throw new ArgumentException("gates");
			
			this.input=input;
			this.output=output;
			this.gates=gates;
			
			int rows = input+gates-output;
			int cols = gates*2;
			
			this.wiring = new BitArray(rows*cols);
		}
		
		public int Input
		{ get { return input; }}
		
		public int Output
		{ get { return output; }}
		
		public int Gates
		{ get { return gates; }}
		
		public PinWire GetWire(int src, int dest)
		{
			int cols = gates * 2;
			int ofs = (src * cols) + (dest*2);
			if (wiring[ofs] && wiring[ofs+1])
				return PinWire.Both;
			else if(wiring[ofs])
				return PinWire.A;
			else if (wiring[ofs+1])
				return PinWire.B;
			return PinWire.None;
		}			
		
		public void Wire(bool isInput, int src, int dest, PinWire wire)
		{
			int cols = gates * 2;
			int ofs = (((isInput ? 0 : input) + src) * cols) + (dest*2); 
			if(wire == PinWire.A)
			{
				wiring[ofs]=true;
			}
			else if(wire== PinWire.B)
			{
				wiring[ofs+1]=true;
			}
			else if(wire== PinWire.Both)
			{
				wiring[ofs]=true;
				wiring[ofs+1]=true;
			}
		}
		
		public void Wire(int src, int dest, PinWire wire)
		{
			this.Wire(false,src,dest,wire);
		}
		
		public void InWire(int src, int dest, PinWire wire)
		{
			this.Wire(true,src,dest,wire);
		}
		
		
		public override string ToString ()
		{
			return 
				String.Format("{0}-{1}-{2}\n{3}",
					input,output,gates,
					BitUtility.BitsToString(wiring,gates*2, 2));
		}
	}
}

