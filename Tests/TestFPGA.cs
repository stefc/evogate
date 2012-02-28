using System;
using NUnit.Framework;
using stefc.gatelib;

namespace Tests
{
	[TestFixture()]
	public class TestFPGA
	{
		[Test]
		public void TestSimpleNand()
		{
			
			// 2 Input's = 1 Output = 1 Gate NAND
			// muss identisch zu einzelnen NAND Gate sein 
		
			Wiring singleNand = new Wiring(3,2,14);
			
//			Console.WriteLine(singleNand);
			
		}
	}
}

