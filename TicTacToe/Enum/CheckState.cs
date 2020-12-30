using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Enum
{
    public enum CheckState
    {
        [Description("Ok")] None = 0,
        [Description("Game was already finished")] EndGameCheck = 1,
        [Description("Index out of range")] IndexCheck = 2,
        [Description("Cell have already taken")] DoubleCellCheck = 4,
        [Description("Won in line")] LineCheck = 8,
        [Description("Won in diagonal")] DiagonalCheck = 16,
        [Description("Same player again")] PreviousPlayerCheck = 32,
        [Description("Player not from this game")] GamePlayerCheck = 64
    }
}
