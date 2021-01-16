using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BusinessComponent.Enum;
using TicTacToe.BusinessComponent.Models;

namespace TicTacToe.BusinessComponent.Services
{
    //TODO create external bot
    /// <summary>
    /// Interface of bot service
    /// </summary>
    public interface IBotService
    {
        /// <summary>
        /// Virtual game board
        /// </summary>
        public char[,] Board { get; set; }
        public GameHistory GameHistory { set; }
        /// <summary>
        /// Calculate next move
        /// </summary>
        CheckState MakeNextMove(bool isExternalBot);
        /// <summary>
        /// Save bots move in base
        /// </summary>
        /// <param name="x">X diagonal</param>
        /// <param name="y">Y diagonal</param>
        void SaveBotMove(int x, int y);
    }
}
