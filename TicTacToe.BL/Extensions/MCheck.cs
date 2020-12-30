using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.BL.Extensions
{
    public static class MCheck
    {
        public static U? Bind<T, U>(this T? t, Func<T, U?> f) where T : struct where U : struct
        {
            return t.HasValue ? f(t.Value) : null;
        }
    }
}
