using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Enum;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Services
{
    /// <summary>
    /// Interface of bot service
    /// </summary>
    public interface IBotService
    {
        /// <summary>
        /// Virtual game board
        /// </summary>
        public char[,] Board { get; set; }
        public GameHistoryBL GameHistoryBl { set; }
        /// <summary>
        /// Calculate next move
        /// </summary>
        CheckStateBL MakeNextMove();
        /// <summary>
        /// Save bots move in base
        /// </summary>
        /// <param name="x">X diagonal</param>
        /// <param name="y">Y diagonal</param>
        void SaveBotMove(int x, int y);
    }
}
