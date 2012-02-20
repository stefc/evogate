using System;
using stefc.gatelib.contract;

namespace stefc.gatelib
{
	
	public class AndGate : IGate
	{	
		public bool Output(Tuple<bool,bool> input)
		{
			return input.Item1 & input.Item2;
		}
	}
			
	public class OrGate : IGate
	{
		public bool Output(Tuple<bool,bool> input)
		{
			return input.Item1 | input.Item2;
		}
	}
	
	public class NandGate : IGate
	{
		public bool Output(Tuple<bool,bool> input)
		{
			return !(input.Item1 & input.Item2);
		}
	}
			
	public class NorGate : IGate
	{	
		public bool Output(Tuple<bool,bool> input)
		{
			return !(input.Item1 | input.Item2);
		}
	}
	
	public class XnorGate : IGate
	{	
		public bool Output(Tuple<bool,bool> input)
		{
			return !(input.Item1 ^ input.Item2);
		}
	}
	
	public class XorGate : IGate
	{	
		public bool Output(Tuple<bool,bool> input)
		{
			return (input.Item1 ^ input.Item2);
		}
	}
}	
