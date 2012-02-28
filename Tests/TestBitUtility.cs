using System;
using System.Collections;
using System.Linq;
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
			
			string exp = string.Join(string.Empty, 
				Enumerable.Repeat("11 11 11\n",6+2).ToArray());
			
			Assert.AreEqual(exp,BitUtility.BitsToString(bits,6,2));
		}
		
		[Test]
		public void IsBit()
		{
			Assert.IsTrue(BitUtility.IsBit(0x0F,0));
			Assert.IsTrue(BitUtility.IsBit(0x0F,1));
			Assert.IsTrue(BitUtility.IsBit(0x0F,2));
			Assert.IsTrue(BitUtility.IsBit(0x0F,3));
			
			Assert.IsTrue(BitUtility.IsBit(0x01,0));
			Assert.IsTrue(BitUtility.IsBit(0x02,1));
			Assert.IsTrue(BitUtility.IsBit(0x04,2));
			Assert.IsTrue(BitUtility.IsBit(0x08,3));
			
			Assert.IsFalse(BitUtility.IsBit(0x00,0));
			Assert.IsFalse(BitUtility.IsBit(0x00,1));
			Assert.IsFalse(BitUtility.IsBit(0x00,2));
			Assert.IsFalse(BitUtility.IsBit(0x00,3));			
		}
	}
}

