using System;
using System.Collections;
using NUnit.Framework;
using stefc.gatelib;

namespace Tests
{
	[TestFixture()]
	public class TestFPGA
	{
		const string ONE_BIT_ADDER = 
			@"3-2-14
00 00 00 00 10 11 00 00 00 00 10 00 00 00
11 00 10 00 00 00 00 10 00 00 00 00 00 00
00 11 01 00 00 00 01 00 00 00 00 00 00 00
00 00 00 10 00 00 10 00 00 00 00 00 00 00
00 00 00 01 00 00 00 01 00 00 00 00 00 00
00 00 00 00 00 00 00 00 01 00 00 00 10 00
00 00 00 00 01 00 00 00 10 00 00 00 00 00
00 00 00 00 00 00 00 00 00 00 00 00 01 00
00 00 00 00 00 00 00 00 00 00 00 10 00 00
00 00 00 00 00 00 00 00 00 10 00 00 00 00
00 00 00 00 00 00 00 00 00 01 00 00 00 00
00 00 00 00 00 00 00 00 00 00 01 00 00 00
00 00 00 00 00 00 00 00 00 00 00 01 00 00
00 00 00 00 00 00 00 00 00 00 00 00 00 01
00 00 00 00 00 00 00 00 00 00 00 00 00 10
";
		
		/*
		 * 
		 * 3,3 => A
3,6 => A
4,3 => B
4,7 => B
5,8 => B
5,12 => A
6,4 => B
6,8 => A
7,12 => B
8,11 => A
9,9 => A
10,9 => B
11,10 => B
3,3 => A
3,6 => A
4,3 => B
4,7 => B
5,8 => B
5,12 => A
6,4 => B
6,8 => A
7,12 => B
8,11 => A
9,9 => A
10,9 => B
11,10 => B

*/
		[Test,Ignore]
		public void TestSimple1BitAdder()
		{
			
			// 2 Input's = 1 Output = 1 Gate NAND
			// muss identisch zu einzelnen NAND Gate sein 
		
			Wiring wiring = Create1BitAdder();
			
			
			Assert.AreEqual(PinWire.A, wiring.GetWire(0,4));
			Assert.AreEqual(PinWire.Both, wiring.GetWire(0,5));
			Assert.AreEqual(PinWire.A, wiring.GetWire(0,10));
			Assert.AreEqual(PinWire.None, wiring.GetWire(0,11));
			//	Console.WriteLine(singleNand);
			
			Assert.AreEqual(ONE_BIT_ADDER, wiring.ToString());		
			
			FPGA oneBitAdder = new FPGA(wiring);
			
			BitArray result;
			
			// 0 + 0 + (0) = 0 (0)
			result = oneBitAdder.Output(CreateInput(false,false,false));
			Assert.AreEqual(2,result.Length);
			Assert.IsFalse(result[0]);
			Assert.IsFalse(result[1]);
			
			// 1 + 0 + (0) = 1 (0)
			result = oneBitAdder.Output(CreateInput(true,false,false));
			Assert.AreEqual(2,result.Length);
			Assert.IsTrue(result[0]);
			Assert.IsFalse(result[1]);
		}
		
		private Wiring Create1BitAdder()
		{
			Wiring result = new Wiring(3,2,14);
			
			const int IN_A = 0;
			const int IN_B = 1;
			const int IN_C = 2;
			
			const int GATE_1 = 0;
			const int GATE_2 = 1;
			const int GATE_3 = 2;
			const int GATE_4 = 3;
			const int GATE_5 = 4;
			const int GATE_6 = 5;
			const int GATE_7 = 6;
			const int GATE_8 = 7;
			const int GATE_9 = 8;
			const int GATE_10= 9;
			const int GATE_11 = 10;
			const int GATE_12 = 11;
			const int GATE_13 = 12;
			const int GATE_14 = 13;
			
			// 0 mit 56 nand verbinden
			
			result.InWire(IN_B,  GATE_1, PinWire.Both);
			
			result.InWire(IN_C,  GATE_2, PinWire.Both);
			
			result.InWire(IN_B,  GATE_3, PinWire.A);
			result.InWire(IN_C,  GATE_3, PinWire.B);
			
			result.Wire(GATE_1,  GATE_4, PinWire.A);
			result.Wire(GATE_2,  GATE_4, PinWire.B);
			
			result.InWire(IN_A,  GATE_5, PinWire.A);
			result.Wire(GATE_4,  GATE_5, PinWire.B);
			
			result.InWire(IN_A,  GATE_6, PinWire.Both);
			
			result.Wire(GATE_1,  GATE_7, PinWire.A);
			result.InWire(IN_C,  GATE_7, PinWire.B);
			
			result.InWire(IN_B,  GATE_8, PinWire.A);
			result.Wire(GATE_2,  GATE_8, PinWire.B);

			result.Wire(GATE_4,  GATE_9, PinWire.A);
			result.Wire(GATE_3,  GATE_9, PinWire.B);
			
			result.Wire(GATE_7, GATE_10, PinWire.A);
			result.Wire(GATE_8, GATE_10, PinWire.B);
			
			result.InWire(IN_A, GATE_11, PinWire.A);
			result.Wire(GATE_9, GATE_11, PinWire.B);
			
			result.Wire(GATE_6, GATE_12, PinWire.A);
			result.Wire(GATE_10, GATE_12, PinWire.B);

			result.Wire(GATE_3, GATE_13, PinWire.A);
			result.Wire(GATE_5, GATE_13, PinWire.B);

			result.Wire(GATE_12, GATE_14, PinWire.A); 
			result.Wire(GATE_11, GATE_14, PinWire.B);
			
			return result;
		}
		
		private BitArray CreateInput(bool A, bool B, bool C)
		{
			return new BitArray(new bool[]{A,B,C});
		}
		
		
		
		/*
3-2-14

0. 1. 2. -  4. 5. 6. 7. -  -  10.-  -  - 		
00 00 00 00 10 11 00 00 00 00 10 00 00 00	#0	
11 00 10 00 00 00 00 10 00 00 00 00 00 00	#1
00 11 01 00 00 00 01 00 00 00 00 00 00 00	#2

00 00 00 10 00 00 10 00 00 00 00 00 00 00
00 00 00 01 00 00 00 01 00 00 00 00 00 00
00 00 00 00 00 00 00 00 01 00 00 00 10 00
00 00 00 00 01 00 00 00 10 00 00 00 00 00
00 00 00 00 00 00 00 00 00 00 00 00 10 00
00 00 00 00 00 00 00 00 00 00 00 10 00 00
00 00 00 00 00 00 00 00 00 10 00 00 00 00
00 00 00 00 00 00 00 00 00 01 00 00 00 00
00 00 00 00 00 00 00 00 00 00 01 00 00 00
00 00 00 00 00 00 00 00 00 00 00 01 00 00
00 00 00 00 00 00 00 00 00 00 00 00 00 01
00 00 00 00 00 00 00 00 00 00 00 00 00 10		
*/

	}
}

