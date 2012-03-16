using System;
using System.Collections;
using NUnit.Framework;
using stefc.gatelib;
using stefc.gatelib.contract;

namespace Tests
{
	[TestFixture()]
	public class TestFPGA
	{
		const string XOR_GATE = 
			@"2-1-4
A A - -
B - B -
# B A -
# # - A
# # # B";
		
		
/*		
set 0,A
set 8,B
set 2,A
set 12,B

set 18,B
set 20,A
set 30,A
set 38,B
		 */
		
		const string ONE_BIT_ADDER = 
			@"3-2-14
- - - - A * - - - - A - - -
* - A - - - - A - - - - - -
- * B - - - B - - - - - - -
# - - A - - A - - - - - - -
# # - B - - - B - - - - - -
# # # - - - - - B - - - A -
# # # # B - - - A - - - - -
# # # # # - - - - - - - B -
# # # # # # - - - - - A - -
# # # # # # # - - A - - - -
# # # # # # # # - B - - - -
# # # # # # # # # - B - - -
# # # # # # # # # # - B - -
# # # # # # # # # # # - - B
# # # # # # # # # # # # - A";
		
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
		[Test]
		public void TestSimple1BitAdder()
		{
			
			// 2 Input's = 1 Output = 1 Gate NAND
			// muss identisch zu einzelnen NAND Gate sein 
		
			Wiring wiring = Create1BitAdder();
			
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
			
			Check(oneBitAdder, "000", "00");
			Check(oneBitAdder, "001", "01");
			Check(oneBitAdder, "010", "01");
			Check(oneBitAdder, "011", "10");
			Check(oneBitAdder, "100", "01");
			Check(oneBitAdder, "101", "10");
			Check(oneBitAdder, "110", "10");
			Check(oneBitAdder, "111", "11");
		/*	
			Check(oneBitAdder, 0, 0);
			Check(oneBitAdder, 1, 1);
			Check(oneBitAdder, 2, 1);
			Check(oneBitAdder, 3, 2);
			Check(oneBitAdder, 4, 1);
			Check(oneBitAdder, 5, 2);
			Check(oneBitAdder, 6, 2);
			Check(oneBitAdder, 7, 3);
	*/
	
		}
		
		[Test]
		public void TestXorGate()
		{
			Wiring wiring = CreateXorGate();
			Assert.AreEqual(XOR_GATE, wiring.ToString());
			
			FPGA xorGate = new FPGA(wiring);
				
			Func<bool,bool,bool> calc = (x,y) =>
			{
				BitArray result = xorGate.Output(CreateInput(x,y));
				Assert.AreEqual(1,result.Count);
				return result[0];
			};
			
			Assert.IsFalse(calc(false,false)," 0 xor 0 = 0");
			Assert.IsTrue(calc(false,true)," 0 xor 1 = 1");
			Assert.IsTrue(calc(true,false)," 1 xor 0 = 1");
			Assert.IsFalse(calc(true,true)," 1 xor 1 = 0");
		}
		
		private Wiring CreateXorGate()
		{
			Wiring result = new Wiring(2,1,4);
			
			const int IN_A = 0;
			const int IN_B = 1;
			
			const int GATE_1 = 0;
			const int GATE_2 = 1;
			const int GATE_3 = 2;
			const int GATE_4 = 3;
			
			result[true,IN_A, GATE_1] = PinWire.A;
			result[true,IN_B, GATE_1] = PinWire.B;
			result[true,IN_A, GATE_2] = PinWire.A;
			result[true,IN_B, GATE_3] = PinWire.B;
			
			result[false,GATE_1,  GATE_2] = PinWire.B;
			result[false,GATE_1,  GATE_3] = PinWire.A;
			
			result[false,GATE_2,  GATE_4] = PinWire.A;
			result[false,GATE_3,  GATE_4] = PinWire.B;
			
			return result;
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
			
			result[true, IN_B,   GATE_1] = PinWire.Both;
			
			result[true, IN_C,   GATE_2] = PinWire.Both;
			
			result[true, IN_B,   GATE_3] = PinWire.A;
			result[true, IN_C,   GATE_3] = PinWire.B;
			
			result[false,GATE_1, GATE_4] = PinWire.A;
			result[false,GATE_2, GATE_4] = PinWire.B;
			
			result[true, IN_A,  GATE_5] = PinWire.A;
			result[false,GATE_4, GATE_5] = PinWire.B;
			
			result[true, IN_A,  GATE_6] = PinWire.Both;
			
			result[false,GATE_1,  GATE_7] = PinWire.A;
			result[true, IN_C,  GATE_7 ] = PinWire.B;
			
			result[true, IN_B,  GATE_8] = PinWire.A;
			result[false,GATE_2,  GATE_8] = PinWire.B;

			result[false,GATE_4,  GATE_9] = PinWire.A;
			result[false,GATE_3,  GATE_9] = PinWire.B;
			
			result[false,GATE_7, GATE_10] = PinWire.A;
			result[false,GATE_8, GATE_10] = PinWire.B;
			
			result[true, IN_A, GATE_11] = PinWire.A;
			result[false,GATE_9, GATE_11] = PinWire.B;
			
			result[false,GATE_6, GATE_12] = PinWire.A;
			result[false,GATE_10, GATE_12] = PinWire.B;

			result[false,GATE_3, GATE_13] = PinWire.A;
			result[false,GATE_5, GATE_13] = PinWire.B;

			result[false,GATE_12, GATE_14] = PinWire.A; 
			result[false,GATE_11, GATE_14] = PinWire.B;
			
			return result;
		}
		
		private BitArray CreateInput(bool A, bool B, bool C)
		{
			return new BitArray(new bool[]{A,B,C});
		}
		
		private BitArray CreateInput(bool A, bool B)
		{
			return new BitArray(new bool[]{A,B});
		}
		
		
		
		private void Check(IFieldProgrammableGateArray fpga, BitArray input, BitArray output)
		{
			BitArray result = fpga.Output(input);
			
			Assert.AreEqual(output, result, 
				String.Format("{0} input => {1} expected, {2} result",
			    	BitUtility.BitsToString(input),BitUtility.BitsToString(output),BitUtility.BitsToString(result)) );
		}
		
		
		
		private void Check(IFieldProgrammableGateArray fpga, string input, string output)
		{
			Check(fpga, BitUtility.StringToBits(input), BitUtility.StringToBits(output));
		}
				
		
	
	}
}

