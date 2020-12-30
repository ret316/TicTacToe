using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TicTacToe.BL.Enum
{
    [Flags]
    public enum CheckStateBL
    {
        None = 0,
        EndGameCheck = 1,
        IndexCheck = 2,
        DoubleCellCheck = 4,
        LineCheck = 8,
        DiagonalCheck = 16,
        PreviousPlayerCheck = 32,
        GamePlayerCheck = 64
    }
}
