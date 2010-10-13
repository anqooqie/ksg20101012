using System;
using System.Collections.Generic;
using System.Linq;

namespace ksg20101012.Library {
    /// <summary>
    /// System.Func‚Ì’l‚ð•Ô‚³‚È‚¢”Å
    /// </summary>
    public delegate void Sub();
    public delegate void Sub<T>(T arg);
    public delegate void Sub<T1, T2>(T1 arg1, T2 arg2);
    public delegate void Sub<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
    public delegate void Sub<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}