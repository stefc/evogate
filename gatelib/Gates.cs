using System;
using System.Threading.Tasks;

namespace stefc.gatelib;

public class Gates {

    #region Operationen

    internal async Task Not(bool a, Func<bool, Task> onResult) => await onResult(!a);

    internal async Task Identity(bool a, Func<bool, Task> onResult) => await onResult(a);

    internal async Task And(bool a, bool b, Func<bool, Task> onResult) => await onResult(a & b);

    internal async Task Or(bool a, bool b, Func<bool, Task> onResult) => await onResult(a | b);

    internal async Task Nand(bool a, bool b, Func<bool, Task> onResult) => await onResult(!(a & b));

    internal async Task Nor(bool a, bool b, Func<bool, Task> onResult) => await onResult(!(a | b));


    internal async Task Xor(bool a, bool b, Func<bool, Task> onResult) => await onResult(a ^ b);

    internal async Task Xnor(bool a, bool b, Func<bool, Task> onResult) => await onResult(!(a ^ b));

    #endregion
}