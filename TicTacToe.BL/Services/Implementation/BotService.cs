using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Enum;
using TicTacToe.BL.Models;
using TicTacToe.DL.Services;

namespace TicTacToe.BL.Services.Implementation
{
    public class BotService : IBotService
    {
        private readonly IFieldChecker _fieldChecker;
        private readonly IGameServiceDL _gameServiceDL;
        public char[,] Board { get; set; }
        private GameHistoryBL gameHistory;

        public BotService(IFieldChecker fieldChecker, IGameServiceDL gameServiceDL)
        {
            this._fieldChecker = fieldChecker;
            this._gameServiceDL = gameServiceDL;
        }
        
        public GameHistoryBL GameHistoryBl
        {
            set => gameHistory = value;
        }

        public CheckStateBL MakeNextMove()
        {
            Random rnd = new Random();
            int xAxis; int yAxis;
            while (true)
            {
                xAxis = rnd.Next(0, IFieldChecker.BOARD_SIZE);
                yAxis = rnd.Next(0, IFieldChecker.BOARD_SIZE);
                if (Board[yAxis, xAxis] == '\0')
                    break;
            }

            //Board[yAxis, xAxis] = '\0';
            _fieldChecker.Board = Board;

            SaveBotMove(xAxis, yAxis);

            if (_fieldChecker.LinesCheck())
            {
                return CheckStateBL.BotWonCheck;
            }

            if (_fieldChecker.DCheck())
            {
                return CheckStateBL.BotWonCheck;
            }

            return CheckStateBL.None;
        }

        public void SaveBotMove(int x, int y)
        {
            _gameServiceDL.SavePlayerMoveAsync(new DL.Models.GameHistoryDL
            {
                Id = Guid.NewGuid(),
                GameId = gameHistory.GameId,
                PlayerId = null,
                IsBot = true,
                XAxis = x,
                YAxis = y,
                MoveDate = DateTime.Now
            });
        }
    }
}
