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
		
		
		private int CalcOfs(bool isInput, int src, int dest)
		{
			return (((isInput ? 0 : input) + src) * (gates*2)) + (dest*2); 
		}
		
		private PinWire this[int ofs]
		{
			get 
			{
				if (wiring[ofs] && wiring[ofs+1])
					return PinWire.Both;
				else if(wiring[ofs])
					return PinWire.A;
				else if (wiring[ofs+1])
					return PinWire.B;
				return PinWire.None;
			}
			set 
			{
				bool a=false;
				bool b=false;
				
				if(value == PinWire.A)
				{
					a=true;
				}
				else if(value== PinWire.B)
				{
					b=true;
				}
				else if(value== PinWire.Both)
				{
					a=true;
					b=true;
				}
				wiring[ofs]=a;
				wiring[ofs+1]=b;
			}
		}
		
		/*
		public void Wire(int src, int dest, PinWire wire)
		{
			int ofs = CalcOfs(false,src,dest);
			Console.WriteLine ("Wire({2},{3}) ofs={0} => {1}",ofs,wire,src,dest);
			this[ofs]=wire;
		}
		
		
		public PinWire GetWire(int src, int dest)
		{
			int ofs = CalcOfs(false,src,dest);
			Console.WriteLine ("GetWire({2},{3}) ofs={0} => {1}",ofs,this[ofs],src,dest);
			return this[ofs];
		}	*/
		
		public PinWire this[bool isInput, int src, int dest]		
		{
			get {
				
				int ofs = CalcOfs(isInput,src,dest);
				return this[ofs];
			}
			set {
				int ofs = CalcOfs(isInput,src,dest);
				this[ofs]=value;	
			}
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

