using System;
using Xunit;
using FakeItEasy;
using stefc.gatelib;

namespace tests
{
	public class TestWiring
	{
		[Fact]
		public void TestToString()
		{
			// rows = input-output+gate 
			string expected_2_1_3 =   // 4 rows 
				"2-1-3\n"+
				"- - -\n"+  
				"- - -\n"+  
				"# - -\n"+
				"# # -";
			
			Wiring wiring = new Wiring(2,1,3);
		
			Assert.Equal(expected_2_1_3,wiring.ToString());
				
			string expected_3_2_4 =  // 5 rows
				"3-2-4\n"+
				"- - - -\n"+  
				"- - - -\n"+  
				"- - - -\n"+
				"# - - -\n"+
				"# # - -";
			
			wiring = new Wiring(3,2,4);
			Assert.Equal(expected_3_2_4,wiring.ToString());
			
			string expected_4_3_7 =  // 6 rows 
				"4-3-7\n"+
				"- - - - - - -\n"+  
				"- - - - - - -\n"+  
				"- - - - - - -\n"+  
				"- - - - - - -\n"+
				"# - - - - - -\n"+
				"# # - - - - -\n"+
				"# # # - - - -\n"+
				"# # # # - - -";
			
			wiring = new Wiring(4,3,7);
			Assert.Equal(expected_4_3_7,wiring.ToString());
		}
		
		[Fact]
		public void TestWireing()
		{
			// rows = input-output+gate 
			Wiring wiring = new Wiring(2,1,3);
			string expected =   // 4 rows 
				"2-1-3\n"+
				"A B -\n"+  
				"- - *\n"+  
				"# - -\n"+
				"# # A";
			
			wiring[true,0,0]=PinWire.A;
			wiring[true,0,1]=PinWire.B;
			wiring[true,1,2]=PinWire.Both;
			
			wiring[false,0,0]=PinWire.Both;
			wiring[false,1,2]=PinWire.A;
			
			Assert.Equal(expected,wiring.ToString());
		}
		
		[Fact]
		public void TestExceptions()
		{
			Assert.Throws<ArgumentException>( () => new Wiring(0,1,1));
			Assert.Throws<ArgumentException>( () => new Wiring(1,0,1));
			Assert.Throws<ArgumentException>( () => new Wiring(1,1,0));
		}
		
		[Fact]
		public void TestAsymetricMatrix()
		{
			/* 
			4 TestGates , 1 output => 0,3,5   +3, +2
				
			5 Gates , 1 output => 0,4,7,9   +4 +3 + 2
					
			6 Gates, 1 Output => 0,5,9,12,14  +5 + 4 + 3+ 2
					
			5 Gates, 2 output => 0,4,7 
			*/ 		
			Check( 4, new int[]{0,3,5});
			Check( 5, new int[]{0,4,7,9});
			Check( 6, new int[]{0,5,9,12,14});
			Check( 5, new int[]{0,4,7});
			
			Assert.Equal(3+2+1, SumSub(4,1));
			Assert.Equal(4+3+2+1, SumSub(5,1));
			Assert.Equal(5+4+3+2+1, SumSub(6,1));
			Assert.Equal(4+3+2,	SumSub(5,2));
		}
		
		private void Check(int gates, int[] result)
		{
			for(int index=0; index<result.Length; index++)
				Assert.Equal(result[index], CalcOfs(gates-1,index));
		}
		
		private int CalcOfs(int gates, int index)
		{
			if(index==0) return 0;
			return gates + CalcOfs(gates-1,index-1);
		}
		
		private int SumSub(int n, int output)
		{
			if(n==output) return 0;
			return n-1 + SumSub(n-1,output);
		}		
	}
}

