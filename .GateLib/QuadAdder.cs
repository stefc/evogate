using System;
using stefc.gatelib.contract;

namespace stefc.gatelib
{
	public class QuadAdder : IQuadAdder
	{
		private readonly IFullAdder adder;
		
		public QuadAdder ()
		{
			adder = new FullAdder();
		}
			
		public Tuple<byte,bool> Output(Tuple<byte,byte,bool> input)
		{
			bool[] result = new bool[4];
			bool cOut = SingleAdd(input,result,0,input.Item3);
			int s = AggregateOr(result,0);		
			return new Tuple<byte,bool>((byte)s,cOut);
		}
			
		private bool SingleAdd(Tuple<byte,byte,bool> input, bool[] result, int bit, bool cIn)
		{
			if(bit==result.Length) return cIn;
			Tuple<bool,bool> output = adder.Output(
				new Tuple<bool, bool, bool>(
					BitUtility.IsBit(input.Item1,bit),
					BitUtility.IsBit(input.Item2,bit),cIn));
			result[bit]=output.Item1;
			return SingleAdd(input,result,bit+1,output.Item2);
		}
		
		private int AggregateOr(bool[] input, byte bit)
		{
			if(bit==input.Length) return 0;
			return BitUtility.BitMask(input[bit],bit) | AggregateOr(input,(byte)(bit+1));
		}
	}
}