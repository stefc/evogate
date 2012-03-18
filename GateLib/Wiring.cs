using System;
using System.Collections;
using System.Text;

namespace stefc.gatelib
{
	public class Wiring
	{
		private readonly int input;
		private readonly int output;
		private readonly int gates;
		
		private readonly BitArray wiring;
		
		public Wiring (int input, int output, int gates) : this(input,output,gates, CreateEmptyWiring(input,output,gates))
		{
		}
		
	
		public Wiring (int input, int output, int gates, BitArray wiring)
		{
			if(input<=0) throw new ArgumentException("input");
			if(output<=0) throw new ArgumentException("output");
			if(gates<=0) throw new ArgumentException("gates");
			
			this.input=input;
			this.output=output;
			this.gates=gates;
			this.wiring=wiring;
		}
		
		public int Input
		{ get { return input; }}
		
		public int Output
		{ get { return output; }}
		
		public int Gates
		{ get { return gates; }}
		
		private int CalcOfs(bool isInput, int src, int dest)
		{
			return isInput ? CalcInputOfs(src,dest) : CalcHiddenOfs(src,dest); 
		}
		
		private int CalcHiddenOfs(int src, int dest)
		{
			return 
				CalcLineOfs(input) + 
				(gates * 3) +
				(CalcHiddenLineOfs(gates-1,src)*2)+ 
				CalcColOfs(dest-src-1);
		}
		
		private int CalcInputOfs(int src, int dest)
		{
			return CalcLineOfs(src) + CalcColOfs(dest);	
		}
		
		private int CalcLineOfs(int src)
		{
			return src * (gates*2);
		}
		
		private int CalcHiddenLineOfs(int gates, int index)
		{
			if(index==0) return 0;
			return gates + CalcHiddenLineOfs(gates-1,index-1);
		}
		
		private int CalcColOfs(int dest)
		{
			return dest*2;
		}
		
		private static int SumSub(int n)
		{
			if(n==0) return 0;
			return n-1 + SumSub(n-1);
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
			StringBuilder sb = new StringBuilder(String.Format("{0}-{1}-{2}",input,output,gates));
			Action<char> emmit = ch => sb.Append(ch);
			
			emmit('\n');
			GetFormattedInput(0,emmit);
			emmit('\n');
			GetFormattedHidden(0,emmit);
			
			return sb.ToString();
		}
		
		private void GetFormattedInput(int row, Action<char> emmit)
		{
			if(row<input)
			{
				AddChar('\n',row,emmit);
				AddChar(true,row,0,emmit);		
				GetFormattedInput(row+1, emmit);
			}
		}
		
		private void GetFormattedHidden(int row, Action<char> emmit)
		{
			if(row<gates-output)
			{
				AddChar('\n',row,emmit);
				AddChar('#',0,row+1,emmit);
				AddChar(false,row,row+1,emmit);
				GetFormattedHidden(row+1,emmit);
			}
		}

		private void AddChar(bool isInput, int row, int col, Action<char> emmit)
		{
			if(col!=gates) 
			{
				AddChar(' ',col,emmit);
				AddPin(this[isInput,row,col],emmit);
				AddChar(isInput, row, col+1, emmit);
			}
		}

		private void AddChar(char ch, int start, int end, Action<char> emmit)
		{
			if(start!=end)
			{
				AddChar(ch, start+1,end, emmit);
				emmit('#');
				AddChar(' ',start,emmit);
			}
		}

		private void AddPin(PinWire pin, Action<char> emmit)
		{
			if(pin==PinWire.A) emmit('A');
			else if(pin == PinWire.B) emmit('B');
			else if(pin == PinWire.Both) emmit('*');
			else emmit('-');
		}

		private void AddChar(char ch, int index, Action<char> emmit)
		{
			if(index!=0) emmit(ch);
		}
		
		private static BitArray CreateEmptyWiring(int input, int output, int gates)
		{
			return new BitArray(CalcLength(input,output,gates));
		}
		
		public static int CalcLength(int input, int output, int gates)
		{
			return 
				(input*gates)*2 + 
				(gates * 3) +
				SumSub(gates)*2;
		}
		
	}
}