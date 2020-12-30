using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Services
{
    public interface IBotService
    {
        public char[,] Board { get; set; }
        public GameHistoryBL GameHistoryBl { set; }
        void MakeNextMove();
        void SaveBotMove(int x, int y);
    }
}
