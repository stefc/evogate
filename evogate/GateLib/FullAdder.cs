using System;
using stefc.gatelib.contract;

namespace stefc.gatelib
{
			
	public class FullAdder : IFullAdder
	{
		private IGate xor1;
		private IGate xor2;
		private IGate and1;
		private IGate and2;
		private IGate or;
		
		public FullAdder()
		{
			xor1=new XorGate(); 
			xor2=new XorGate();
			and1=new AndGate();
			and2=new AndGate();
			or=new OrGate();
		}
		
		public Tuple<bool,bool> Output(Tuple<bool,bool,bool> input)
		{
			Tuple<bool,bool> ab = new Tuple<bool,bool>(input.Item1,input.Item2);
			bool a_xor_b = xor1.Output(ab);
			bool a_and_b = and2.Output(ab);
			
			
			Tuple<bool,bool> ab_c = new Tuple<bool,bool>(a_xor_b,input.Item3);
			bool s = xor2.Output(ab_c);
			bool ab_and_c = and1.Output(ab_c);
			
	
			bool c = or.Output(new Tuple<bool,bool>(ab_and_c,a_and_b));
			
			return new Tuple<bool, bool>(c,s);
		}
	}
}

