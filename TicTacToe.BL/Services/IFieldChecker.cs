using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Enum;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

namespace TicTacToe.BL.Services
{
    public interface IFieldChecker
    {
        const int BOARD_SIZE = 3;
        public char[,] Board { get; set; }
        public GameHistoryBL NextMove{ set; }
        void BoardInit(IEnumerable<GameHistoryBL> gameHistories);

        void MakeMove();
        bool LinesCheck();
        bool DCheck();
        bool DoubleCellCheck();
        bool EndGameCheck(bool game);
        bool IndexCheck();
        bool LastPlayerCheck();
        bool GamePlayerCheck(GameBL game);
    }
}
