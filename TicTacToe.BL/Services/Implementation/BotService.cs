using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.BusinessComponent.Enum;
using TicTacToe.DataComponent.Models;
using TicTacToe.DataComponent.Services;

namespace TicTacToe.BusinessComponent.Services.Implementation
{
    public class BotService : IBotService
    {
        private readonly IFieldChecker _fieldChecker;
        private readonly DataComponent.Services.IGameService _gameService;
        private readonly IStatisticService _statisticService;
        public char[,] Board { get; set; }
        private Models.GameHistory _gameHistory;

        public BotService(IFieldChecker fieldChecker, DataComponent.Services.IGameService gameService)
        {
            this._fieldChecker = fieldChecker;
            this._gameService = gameService;
        }

        public Models.GameHistory GameHistory
        {
            set => _gameHistory = value;
        }

        public CheckState MakeNextMove(bool isExternalBot)
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
                return CheckState.BotWonCheck;
            }

            if (_fieldChecker.DCheck())
            {
                return CheckState.BotWonCheck;
            }

            return CheckState.None;
        }

        public void SaveBotMove(int x, int y)
        {
            _gameService.SavePlayerMoveAsync(new DataComponent.Models.GameHistory
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
