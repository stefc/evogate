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
		public void TestFlowGates()
		{
			this.CheckGate((IFlowGate)new AndGate(), new bool[]{false, false, false, true});
			this.CheckGate((IFlowGate)new OrGate(), new bool[]{false, true, true, true});
			this.CheckGate((IFlowGate)new NandGate(), new bool[]{true, true, true, false});
			this.CheckGate((IFlowGate)new NorGate(), new bool[]{true, false, false, false});
			this.CheckGate((IFlowGate)new XorGate(), new bool[]{false, true, true, false});
			this.CheckGate((IFlowGate)new XnorGate(), new bool[]{true, false, false, true});
		}
		
		
		
		private void CheckGate(IGate gate, bool[] results)
		{
			this.CheckGate(gate, false, false, results[0]);
			this.CheckGate(gate, false, true,  results[1]);
			this.CheckGate(gate, true,  false, results[2]);
			this.CheckGate(gate, true,  true,  results[3]);
		}
		
		private void CheckGate(IFlowGate gate, bool[] results)
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
		
		private void CheckGate(IFlowGate gate, bool x, bool y, bool q)
		{
			bool wasFired = false;
			Action<bool> action = (signal) => {
				Assert.AreEqual(q, signal);	
				wasFired=true;
			};
			
			gate.Out += action;
			
			gate.A(x);
			gate.B(y);
			
			gate.Out -= action;
			
			Assert.IsTrue(wasFired,"No fired!");
		}	
		
		
	
		
		[Test]
		public void TestQuadAdder()
		{
			IQuadAdder adder = new QuadAdder();
			Assert.IsNotNull(adder);
			
			// ohne carry in (cIn)
			this.DoLoop(adder,false);
			
			// mit carry in (cIn) 
			this.DoLoop(adder,true);			
		}
		
		private void DoLoop(IQuadAdder adder,bool cIn)
		{
			// ohne carry in (cIn)
			for(byte x = 0; x<16; x++)
				for(byte y= 0; y<16; y++)
				{
					byte result = (byte)(x + y + (cIn ? 1:0 ));
					bool cOut = (result & 0x10) > 0;
					result &= 0x0F;
			
					Check(adder, x,y,cIn,cOut,result);
					Check(adder, y,x,cIn,cOut,result);
				}
		}
		
		private void Check(IQuadAdder adder,byte x,byte y, bool cIn, bool cOut, byte s)
		{
			string message = String.Format("(x={0},y={1},cIn={2}) => (cOut={3},s={4})",x,y,cIn,cOut,s);
			
			Tuple<byte,byte,bool> input = new Tuple<byte, byte, bool>(x,y,cIn);
			Tuple<byte,bool> result = adder.Output(input);
			
			Assert.AreEqual(s,result.Item1, message);
			Assert.AreEqual(cOut, result.Item2, message);
			
		}
		
	}
}

