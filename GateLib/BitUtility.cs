using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace stefc.gatelib
{
	public static class BitUtility
	{
		public static string BitsToString(BitArray bits)
		{
			string result=String.Empty;
			for(int i= 0; i<bits.Count; i++)
				result += (bits[i] ? "1":"0");
			return result;
		}
		public static string BitsToString(BitArray bits, int linebreak, int separator)
		{
			string result=String.Empty;
			for(int i= 0; i<bits.Count; i++)
			{
				result += (bits[i] ? "1":"0");
				
				if(((i+1) % linebreak)==0)
					result += Environment.NewLine;
				else if ((separator>0) && (((i+1) % separator)==0))
					result += " ";
			}
			return result;
		}
		
		public static BitArray StringToBits(string bits)
		{
			bits = Regex.Replace( bits, @"\s", String.Empty );
			
			int count=bits.Length;
			BitArray result=new BitArray(count);
			for(int index=0; index<count; index++)
				result.Set(index,bits[count-index-1]=='1');
			return result;
		} 
		
		public static BitArray ByteToBits(byte value)
		{
			return new BitArray(new byte[]{value});
		}
		
		public static byte BitsToByte(BitArray bits)
		{
			byte[] result=new byte[1];
			bits.CopyTo(result,0);
			return result[0];
		}
		
		public static bool IsBit(int val, int bit)
		{
			
			return (val & (1 << bit)) == (1 << bit);
		}
		
		public static int BitMask(bool val, byte bit)
		{
			return val ? (1 << bit) : 0;
		}
		
	}
}

