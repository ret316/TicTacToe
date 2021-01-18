using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BusinessComponent.Enum;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.BusinessComponent.Services;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.BusinessComponent.Helpers
{
    public class WinHelper
    {
        private Models.Game _game;
        private char[,] _board;
        private readonly IStatisticService _statisticService;

        public Models.Game Game
        {
            set => _game = value;
        }

        public char[,] Board
        {
            set => _board = value;
        }

        public WinHelper(IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }

        private bool IsBot(Guid id)
        {
            var idStr = id.ToString().Replace("-", "");
            for (int i = 1; i < idStr.Length; i++)
                if (idStr[i] != idStr[0])
                    return false;

            return true;
        }

        public async Task FindWinnerAndLoser()
        {
            var res = GetWinner();

            if (!IsBot(_game.Player2Id))
            {
                await SaveGameResult(new Models.GameResult
                {
                    Id = Guid.NewGuid(),
                    GameId = _game.GameId.Value,
                    PlayerId = res.winner,
                    Result = ResultStatus.Won
                });

                await SaveGameResult(new Models.GameResult
                {
                    Id = Guid.NewGuid(),
                    GameId = _game.GameId.Value,
                    PlayerId = res.loser,
                    Result = ResultStatus.Lost
                });
            }
            else
            {
                await SaveGameResult(new Models.GameResult
                {
                    Id = Guid.NewGuid(),
                    GameId = _game.GameId.Value,
                    PlayerId = _game.Player1Id,
                    Result = res.winner == _game.Player1Id ? ResultStatus.Won : ResultStatus.Lost
                });
            }
        }

        private (Guid winner, Guid loser) GetWinner()
        {
            var list = _board.Cast<char>();
            var x = list.Count(x => x == 'X');
            var y = list.Count(x => x == 'O');
                return x > y ? (_game.Player1Id, _game.Player2Id) : (_game.Player2Id, _game.Player1Id);
        }

        public async Task SaveGameResult(Models.GameResult gameResult)
        {
            await _statisticService.SaveStatisticAsync(gameResult);
        }
    }
}
