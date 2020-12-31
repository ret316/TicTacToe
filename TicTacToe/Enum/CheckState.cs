using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Enum
{
    /// <summary>
    /// Player move status
    /// </summary>
    public enum CheckState
    {
        /// <summary>
        /// Everything ok
        /// </summary>
        [Description("Ok")] None = 0,
        /// <summary>
        /// Game was already finished
        /// </summary>
        [Description("Game was already finished")] EndGameCheck = 1,
        /// <summary>
        /// Player tried to make a move outside of game board
        /// </summary>
        [Description("Index out of range")] IndexCheck = 2,
        /// <summary>
        /// Player tried to make a move cell that already taken
        /// </summary>
        [Description("Cell have already taken")] DoubleCellCheck = 4,
        /// <summary>
        /// Player won in line
        /// </summary>
        [Description("Won in line")] LineCheck = 8,
        /// <summary>
        /// Player won in diagonal
        /// </summary>
        [Description("Won in diagonal")] DiagonalCheck = 16,
        /// <summary>
        /// Player trying to make a move twice
        /// </summary>
        [Description("Same player again")] PreviousPlayerCheck = 32,
        /// <summary>
        /// Player not from that game
        /// </summary>
        [Description("Player not from this game")] GamePlayerCheck = 64
    }
}
