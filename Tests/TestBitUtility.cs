using System;
using NUnit.Framework;
using stefc.gatelib;

namespace Tests
{
	[TestFixture]
	public class TestBitUtility
	{
		[Test]
		public void TestBitsToStrings()
		{
			BitArray bits = new BitArray(6*2*4,true);
			
			Console.WriteLine(BitUtility.BitsToString(bits,6,2,4));
		}
	}
}

