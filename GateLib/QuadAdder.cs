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
			
		public Tuple<byte,bool> Output(Tuple<byte,byte,bool> input)
		{
			Tuple<bool,bool>[] result = new Tuple<bool, bool>[4];
			result[0] = adders[0].Output(Join(input,input.Item3,0));
			result[1] = adders[1].Output(Join(input,result[0].Item2,1));
			result[2] = adders[2].Output(Join(input,result[1].Item2,2));
			result[3] = adders[3].Output(Join(input,result[2].Item2,3));
			
			// Result 
			/*
			byte s = (byte)
			(BitMask(result[0].Item1,0) | 
				BitMask(result[1].Item1,1) | 
				BitMask(result[2].Item1,2) | 
				BitMask(result[3].Item1,3)); */
			byte index=0;
			int s = 0;
			foreach(Tuple<bool,bool> val in result)
			{
				s |= BitUtility.BitMask(val.Item1,index++);
			}
			
			bool cOut = result[3].Item2;
			
			return new Tuple<byte,bool>((byte)s,cOut);
		}
		
		private static Tuple<bool,bool,bool> Join(Tuple<byte,byte,bool> input, bool cIn, byte bit)
		{
			return new Tuple<bool, bool, bool>(
				BitUtility.IsBit(input.Item1,bit),
				BitUtility.IsBit(input.Item2,bit),cIn);
		}
	}
}