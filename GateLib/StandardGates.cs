using System;
using stefc.gatelib.contract;

namespace stefc.gatelib
{
	public abstract class Gate : IGate,IFlowGate
	{	
		private bool? a;
        private bool? b;
        
		public Gate()
		{
			a=b=null;
		}
		
		public abstract bool Output(Tuple<bool,bool> input);	
		
		public event Action<bool> Out;

		#region Input-Layer / Dendriten
        public void A(bool input) {
            a = input;
			FireEvent();	
        }

        public void B(bool input) {
            b = input;
			FireEvent();
        }
		#endregion
		
		#region Output-Layer / Synapse 
		private void FireEvent()
		{
			if (a.HasValue && b.HasValue) 
			{
				if(this.Out!=null)
				{
                	Out(Output(new Tuple<bool,bool>(a.Value,b.Value)));
				}
				a=b=null;
			}
		}
		#endregion	
	}
	
	public class AndGate : Gate
	{	
		public override bool Output(Tuple<bool,bool> input)
		{
			return input.Item1 & input.Item2;
		}
	}
			
	public class OrGate : Gate
	{
		public override bool Output(Tuple<bool,bool> input)
		{
			return input.Item1 | input.Item2;
		}
	}
	
	public class NandGate : Gate
	{
		public override bool Output(Tuple<bool,bool> input)
		{
			return !(input.Item1 & input.Item2);
		}
	}
			
	public class NorGate : Gate
	{	
		public override bool Output(Tuple<bool,bool> input)
		{
			return !(input.Item1 | input.Item2);
		}
	}
	
	public class XnorGate : Gate
	{	
		public override bool Output(Tuple<bool,bool> input)
		{
			return !(input.Item1 ^ input.Item2);
		}
	}
	
	public class XorGate : Gate
	{	
		public override bool Output(Tuple<bool,bool> input)
		{
			return (input.Item1 ^ input.Item2);
		}
	}
}	
