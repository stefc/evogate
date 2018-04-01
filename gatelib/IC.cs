using System;
using System.Collections;
using System.Text;

namespace stefc.gatelib
{

    public class IC {

        private readonly string name; 

        private readonly int input; 

        private readonly int output; 

        private readonly int gates;

        public string Name => name;
        public int Input => input; 
		
		public int Output => output;
		public int Gates => gates; 
		
    }
}