using System;
using NUnit.Framework;
using stefc.gatelib;
using stefc.gatelib.contract;

namespace Tests
{
	[TestFixture]
	public class TestGates
	{
		
		[Test]
		public void TestStandardGates()
		{
			this.CheckGate(new AndGate(), new bool[]{false, false, false, true});
			this.CheckGate(new OrGate(), new bool[]{false, true, true, true});
			this.CheckGate(new NandGate(), new bool[]{true, true, true, false});
			this.CheckGate(new NorGate(), new bool[]{true, false, false, false});
			this.CheckGate(new XorGate(), new bool[]{false, true, true, false});
			this.CheckGate(new XnorGate(), new bool[]{true, false, false, true});
		}
		
		private void CheckGate(IGate gate, bool[] results)
		{
			this.CheckGate(gate, false, false, results[0]); 			
			this.CheckGate(gate, false, true,  results[1]);	
			this.CheckGate(gate, true,  false, results[2]);
			this.CheckGate(gate, true,  true,  results[3]);
		}
		
		private void CheckGate(IGate gate, bool x,bool y, bool q)
		{
			Assert.AreEqual(q, gate.Output(new Tuple<bool,bool>(x,y)));
		}
		
		[Test]
		public void TestFullAdder()
		{
			IFullAdder adder = new FullAdder();
			//          	x		y		cIn		cOut	s			
			CheckGate(adder,	false,	false,	false,	false,	false);
			CheckGate(adder,	false,	false,	true,	false,	true);
			CheckGate(adder,	false,	true,	false,	false,	true);
			CheckGate(adder,	false,	true,	true,	true,	false);
			CheckGate(adder,	true,	false,	false,	false,	true);
			CheckGate(adder,	true,	false,	true,	true,	false);
			CheckGate(adder,	true,	true,	false,	true,	false);
			CheckGate(adder, 	true, 	true, 	true, 	true,	true);
		}
		
		private void CheckGate(IFullAdder gate, bool x,bool y, bool cIn, bool cOut, bool s)
		{
			Assert.AreEqual(new Tuple<bool,bool>(cOut,s), gate.Output(new Tuple<bool,bool,bool>(x,y,cIn)));
		}
		
	}
}

