using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace stefc.gatelib
{
	public static class BitUtility
	{
		public static string BitsToString(BitArray bits, int linebreak, int separator, int inputs)
		{
			string result=String.Empty;
			for(int i= 0; i<bits.Count; i++)
			{
				result += (bits[i] ? "1":"0");
				if( (i+1) == (linebreak*inputs))
					result += Environment.NewLine;
				
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
		
		public static bool IsBit(int val, int bit)
		{
			
			return (val & (1 << bit)) == (1 << bit);
		}
		
		public static int BitMask(bool val, byte bit)
		{
			return val ? (1 << bit) : 0;
		}
		
		public static BitArray CreateWiring(int input, int gates,int output)
		{
			int rows = input+gates-output;
			int cols = gates*2;   // gates 
			BitArray wiring = new BitArray(rows*cols);
			return wiring;
		}
	}
}

