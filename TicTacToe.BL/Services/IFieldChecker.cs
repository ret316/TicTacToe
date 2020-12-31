using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Enum;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

namespace TicTacToe.BL.Services
{
    /// <summary>
    /// Interface of field check service
    /// </summary>
    public interface IFieldChecker
    {
        /// <summary>
        /// Board size
        /// </summary>
        const int BOARD_SIZE = 3;
        /// <summary>
        /// Virtual board
        /// </summary>
        public char[,] Board { get; set; }
        public GameHistoryBL NextMove{ set; }
        /// <summary>
        /// Initialization of game board
        /// </summary>
        /// <param name="gameHistories">Game chronology</param>
        void BoardInit(IEnumerable<GameHistoryBL> gameHistories);

        /// <summary>
        /// Set next move on board
        /// </summary>
        void MakeMove();
        /// <summary>
        /// Check win position in line
        /// </summary>
        /// <returns></returns>
        bool LinesCheck();
        /// <summary>
        /// Check win position in diagonal
        /// </summary>
        /// <returns></returns>
        bool DCheck();
        /// <summary>
        /// Empty cell check
        /// </summary>
        /// <returns></returns>
        bool DoubleCellCheck();
        /// <summary>
        /// Check if game ended
        /// </summary>
        /// <param name="game">Game parameter is game finished</param>
        /// <returns></returns>
        bool EndGameCheck(bool game);
        /// <summary>
        /// Check if index out of range of game board
        /// </summary>
        /// <returns></returns>
        bool IndexCheck();
        /// <summary>
        /// Last game move check. if user already take
        /// </summary>
        /// <returns></returns>
        bool LastPlayerCheck();
        /// <summary>
        /// Сheck if the previous move was also from this user
        /// </summary>
        /// <param name="game">Game details</param>
        /// <returns></returns>
        bool GamePlayerCheck(GameBL game);
    }
}
