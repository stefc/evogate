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
				result = (bits[i] ? "1":"0") + result;
				if( (i+1) == (linebreak*inputs))
					result = Environment.NewLine + result;
				
				if(((i+1) % linebreak)==0)
					result = Environment.NewLine + result;
				else if ((separator>0) && (((i+1) % separator)==0))
					result = " " + result;
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
	}
}

