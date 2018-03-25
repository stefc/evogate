using Xunit;

using System.Collections;
using System.Linq;
using stefc.gatelib;

namespace tests
{
    public class TestBitUtility
    {
        [Fact]

		public void TestBitsToStrings()
		{
			BitArray bits = new BitArray(6*2*4,true);
			
			string exp = string.Join(string.Empty, 
				Enumerable.Repeat("11 11 11\n",6+2).ToArray());
			
			Assert.Equal(exp, BitUtility.BitsToString(bits,6,2));
		}

        [Fact]
		public void IsBit()
		{
			Assert.True(BitUtility.IsBit(0x0F,0));
			Assert.True(BitUtility.IsBit(0x0F,1));
			Assert.True(BitUtility.IsBit(0x0F,2));
			Assert.True(BitUtility.IsBit(0x0F,3));
			
			Assert.True(BitUtility.IsBit(0x01,0));
			Assert.True(BitUtility.IsBit(0x02,1));
			Assert.True(BitUtility.IsBit(0x04,2));
			Assert.True(BitUtility.IsBit(0x08,3));
			
			Assert.False(BitUtility.IsBit(0x00,0));
			Assert.False(BitUtility.IsBit(0x00,1));
			Assert.False(BitUtility.IsBit(0x00,2));
			Assert.False(BitUtility.IsBit(0x00,3));			
		}
		
		[Fact]
		public void TestBitsToByte()
		{
			BitArray bits = BitUtility.ByteToBits(255);
			Assert.Equal(8, bits.Count);
			for(int i=0; i<8; i++)
				Assert.True(bits[i]);
			Assert.Equal(255,BitUtility.BitsToByte(bits));
			
			bits = BitUtility.ByteToBits(0);
			for(int i=0; i<8; i++)
				Assert.False(bits[i]);
			Assert.Equal(0,BitUtility.BitsToByte(bits));
			
			bits = BitUtility.ByteToBits(1);
			Assert.True(bits[0]);
			Assert.False(bits[7]);
			Assert.Equal(1,BitUtility.BitsToByte(bits));
			
			bits = BitUtility.ByteToBits(128);
			Assert.False(bits[0]);
			Assert.True(bits[7]);
			Assert.Equal(128,BitUtility.BitsToByte(bits));
		}
    }
}
