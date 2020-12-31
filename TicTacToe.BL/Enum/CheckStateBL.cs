using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TicTacToe.BL.Enum
{
    /// <summary>
    /// Player move status
    /// </summary>
    [Flags] public enum CheckStateBL
    {
        /// <summary>
        /// Everything ok
        /// </summary>
        None = 0,
        /// <summary>
        /// Game was already finished
        /// </summary>
        EndGameCheck = 1,
        /// <summary>
        /// Player tried to make a move outside of game board
        /// </summary>
        IndexCheck = 2,
        /// <summary>
        /// Player tried to make a move cell that already taken
        /// </summary>
        DoubleCellCheck = 4,
        /// <summary>
        /// Player won in line
        /// </summary>
        LineCheck = 8,
        /// <summary>
        /// Player won in diagonal
        /// </summary>
        DiagonalCheck = 16,
        /// <summary>
        /// Player trying to make a move twice
        /// </summary>
        PreviousPlayerCheck = 32,
        /// <summary>
        /// Player not from that game
        /// </summary>
        GamePlayerCheck = 64
    }
}
