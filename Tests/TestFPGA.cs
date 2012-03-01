using System;
using NUnit.Framework;
using stefc.gatelib;

namespace Tests
{
	[TestFixture()]
	public class TestFPGA
	{
		const string ONE_BIT_ADDER = 
			@"3-2-14
00 00 00 10 00 11 00 00 00 00 10 00 00 00
11 00 10 00 00 00 00 10 00 00 00 00 00 00
00 11 01 00 00 00 01 00 00 00 00 00 00 00
00 00 00 10 00 00 10 00 00 00 00 00 00 00
00 00 00 01 00 00 00 01 10 00 00 00 00 00
00 00 00 00 00 00 00 00 01 00 00 00 10 00
00 00 00 00 01 00 00 00 00 00 00 00 00 00
00 00 00 00 00 00 00 00 00 00 00 00 10 00
00 00 00 00 00 00 00 00 00 00 00 10 00 00
00 00 00 00 00 00 00 00 00 10 00 00 00 00
00 00 00 00 00 00 00 00 00 01 00 00 00 00
00 00 00 00 00 00 00 00 00 00 01 00 00 00
00 00 00 00 00 00 00 00 00 00 00 01 00 00
00 00 00 00 00 00 00 00 00 00 00 00 00 01
00 00 00 00 00 00 00 00 00 00 00 00 00 10
";
		
		[Test]
		public void TestSimpleNand()
		{
			
			// 2 Input's = 1 Output = 1 Gate NAND
			// muss identisch zu einzelnen NAND Gate sein 
		
			Wiring singleNand = Create1BitAdder();
			
			Assert.AreEqual(ONE_BIT_ADDER, singleNand.ToString());
			
			Console.WriteLine(singleNand);
			
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
			
			result.InWire(IN_A,  GATE_4, PinWire.A);
			result.Wire(GATE_1,  GATE_4, PinWire.A);
			result.Wire(GATE_2,  GATE_4, PinWire.B);
			
			result.Wire(GATE_4,  GATE_5, PinWire.B);
			
			result.InWire(IN_A,  GATE_6, PinWire.Both);
			
			result.InWire(IN_C,  GATE_7, PinWire.B);
			result.Wire(GATE_1,  GATE_7, PinWire.A);
			
			result.InWire(IN_B,  GATE_8, PinWire.A);
			result.Wire(GATE_2,  GATE_8, PinWire.B);

			result.Wire(GATE_3,  GATE_9, PinWire.B);
			result.Wire(GATE_2,  GATE_9, PinWire.A);
		
			result.Wire(GATE_7, GATE_10, PinWire.A);
			result.Wire(GATE_8, GATE_10, PinWire.B);
						
			result.InWire(IN_A, GATE_11, PinWire.A);
			result.Wire(GATE_9, GATE_11, PinWire.B);
			
			result.Wire(GATE_6, GATE_12, PinWire.A);
			result.Wire(GATE_10, GATE_12, PinWire.B);
			
			result.Wire(GATE_5, GATE_13, PinWire.A);
			result.Wire(GATE_3, GATE_13, PinWire.A);

			result.Wire(GATE_12, GATE_14, PinWire.A); 
			result.Wire(GATE_11, GATE_14, PinWire.B);
			
			return result;
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

