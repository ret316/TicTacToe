using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Enum;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;
using TicTacToe.DL.Services;

namespace TicTacToe.BL.Services.Implementation
{
    public class BotService : IBotService
    {
        private readonly IFieldChecker _fieldChecker;
        private IGameServiceDL _gameServiceDL;
        private readonly IStatisticServiceBL _statisticServiceBL;
        public char[,] Board { get; set; }
        private GameHistoryBL _gameHistory;

        public BotService(IFieldChecker fieldChecker, IGameServiceDL gameServiceDL)
        {
            this._fieldChecker = fieldChecker;
            this._gameServiceDL = gameServiceDL;
        }

        public GameHistoryBL GameHistoryBl
        {
            set => _gameHistory = value;
        }

        public CheckStateBL MakeNextMove(bool isExternalBot)
        {
            Random rnd = new Random();
            int xAxis; int yAxis;

            if (!isExternalBot)
            {
                while (true)
                {
                    xAxis = rnd.Next(0, IFieldChecker.BOARD_SIZE);
                    yAxis = rnd.Next(0, IFieldChecker.BOARD_SIZE);
                    if (Board[yAxis, xAxis] == '\0')
                    {
                        Board[yAxis, xAxis] = 'O';
                        break;
                    }
                }
            }
            else
            {
                xAxis = _gameHistory.XAxis; yAxis = _gameHistory.YAxis;
                Board[_gameHistory.YAxis, _gameHistory.XAxis] = 'O';
            }

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
            _gameServiceDL.SavePlayerMoveAsync(new GameHistoryDL
            {
                GameId = _gameHistory.GameId,
                PlayerId = null,
                IsBot = true,
                XAxis = x,
                YAxis = y,
                MoveDate = DateTime.Now
            });
        }
    }
}
