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
		public override string ToString ()
		{
			return BitUtility.BitsToString(wiring, gates*2, output, input);
		}
	}
}

