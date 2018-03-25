using System;


namespace stefc.gatelib
{
    public class Gates {

        #region Operationen

        internal void And(bool a,bool b, Action<bool> onResult)
        {
            onResult(a & b);
        }

        internal void Or(bool a,bool b, Action<bool> onResult)
        {
            onResult(a | b);
        }

        internal void Nand(bool a,bool b, Action<bool> onResult)
        {
            onResult(!(a & b));
        }

        internal void Nor(bool a,bool b, Action<bool> onResult)
        {
            onResult(!(a | b));
        }


        internal void Xor(bool a,bool b, Action<bool> onResult)
        {
            onResult(a ^ b);
        }

        internal void Xnor(bool a,bool b, Action<bool> onResult)
        {
            onResult(!(a ^ b));
        }

        #endregion
    }   
}
