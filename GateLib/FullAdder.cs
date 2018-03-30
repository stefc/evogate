using System;
using System.Threading.Tasks;

namespace stefc.gatelib
{
    public class FullAdder {


        #region Integration

        internal async Task Add(bool a,bool b, bool c, Func<bool,Task> onSum, Func<bool,Task> onCarry)
        {
            var gate = new Gates();
            await gate.Xor( a, b, 
                xor1 => gate.And( a, b, 
                        and1 => gate.Xor( xor1, c,
                                async s => {
                                    await onSum(s);
                                    await gate.And( xor1, c, 
                                        and2 => gate.Xor( and1, and2, onCarry));
                                })));
        }
        #endregion
    }   
}
