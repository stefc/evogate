using System;


namespace stefc.gatelib
{
    public class FullAdder {


        #region Integration

        internal void Add(bool a,bool b, bool c, Action<bool> onSum, Action<bool> onCarry)
        {
            var gate = new Gates();

            gate.Xor( a, b, 
                xor1 => {
                    gate.And( a, b, 
                        and1 => {
                            gate.Xor( xor1, c, 
                                s => {
                                    onSum(s);
                                    gate.And( xor1, c, 
                                        and2 => {
                                           gate.Xor( and1, and2, onCarry);
                                        });
                                });
                        });
                });
        }
        #endregion
    }   
}
