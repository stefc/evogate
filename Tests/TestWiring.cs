using System;
using NUnit.Framework;
using stefc.gatelib;

namespace Tests
{
	[TestFixture]
	public class TestWiring
	{
		[Test]
		public void TestToString()
		{
			// rows = input-output+gate 
			string expected_2_1_3 =   // 4 rows 
				"2-1-3\n"+
				"00 00 00\n"+  
				"00 00 00\n"+  
				"00 00 00\n"+
				"00 00 00\n";
			
			Wiring wiring = new Wiring(2,1,3);
		
			Assert.AreEqual(expected_2_1_3,wiring.ToString(), "2-1 (3)");
				
			string expected_3_2_4 =  // 5 rows
				"3-2-4\n"+
				"00 00 00 00\n"+  
				"00 00 00 00\n"+  
				"00 00 00 00\n"+
				"00 00 00 00\n"+
				"00 00 00 00\n";
			
			wiring = new Wiring(3,2,4);
			Assert.AreEqual(expected_3_2_4,wiring.ToString(), "3-2 (4)");
			
			string expected_4_3_7 =  // 6 rows 
				"4-3-7\n"+
				"00 00 00 00 00 00 00\n"+  
				"00 00 00 00 00 00 00\n"+  
				"00 00 00 00 00 00 00\n"+  
				"00 00 00 00 00 00 00\n"+
				"00 00 00 00 00 00 00\n"+
				"00 00 00 00 00 00 00\n"+
				"00 00 00 00 00 00 00\n"+
				"00 00 00 00 00 00 00\n";
			
			wiring = new Wiring(4,3,7);
			Assert.AreEqual(expected_4_3_7,wiring.ToString(), "4-3 (7)");
		}
		
		[Test]
		public void TestWireing()
		{
			// rows = input-output+gate 
			Wiring wiring = new Wiring(2,1,3);
			string expected =   // 4 rows 
				"2-1-3\n"+
				"10 01 00\n"+  
				"00 00 11\n"+  
				"11 00 00\n"+
				"00 00 10\n";
			
			wiring[true,0,0]=PinWire.A;
			wiring[true,0,1]=PinWire.B;
			wiring[true,1,2]=PinWire.Both;
			
			wiring.Wire(0,0,PinWire.Both);
			wiring.Wire(1,2,PinWire.A);
			
			Assert.AreEqual(expected,wiring.ToString());
		}
		
		[Test]
		public void TestExceptions()
		{
			Assert.Throws<ArgumentException>( () => new Wiring(0,1,1));
			Assert.Throws<ArgumentException>( () => new Wiring(1,0,1));
			Assert.Throws<ArgumentException>( () => new Wiring(1,1,0));		
		}
	}
}

