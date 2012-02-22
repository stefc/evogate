using System;
using stefc.gatelib.contract;

namespace stefc.gatelib
{
	public class QuadAdder : IQuadAdder
	{
		private readonly IFullAdder[] adders;
		
		public QuadAdder ()
		{
			adders = new IFullAdder[4]{new FullAdder(),new FullAdder(),new FullAdder(),new FullAdder()};	
		}
		
		public static bool IsBit(int val, int bit)
		{
			
			return (val & (1 << bit)) == (1 << bit);
		}
		
		public static byte BitMask(bool val, byte bit)
		{
			return val ? (byte)(1 << bit) : (byte)0;
		}
		
		public Tuple<byte,bool> Output(Tuple<byte,byte,bool> input)
		{
			Tuple<bool,bool> a = 
				adders[0].Output(Join(input,input.Item3,0));
					
			Tuple<bool,bool> b = 
				adders[1].Output(Join(input,a.Item2,1));
			
			Tuple<bool,bool> c = 
				adders[2].Output(Join(input,b.Item2,2));
			
			Tuple<bool,bool> d = 
				adders[3].Output(Join(input,c.Item2,3));
			
			// Result 
			byte s = (byte)(BitMask(a.Item1,0) | BitMask(b.Item1,1) | BitMask(c.Item1,2) | BitMask(d.Item1,3));
			bool cOut = d.Item2;
			
			return new Tuple<byte,bool>(s,cOut);
		}
		
		private static Tuple<bool,bool,bool> Join(Tuple<byte,byte,bool> input, bool cIn, byte bit)
		{
			return new Tuple<bool, bool, bool>(IsBit(input.Item1,bit),IsBit(input.Item2,bit),cIn);
		}
	}
}