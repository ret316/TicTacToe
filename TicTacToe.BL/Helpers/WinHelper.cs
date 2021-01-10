using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.DL.Models;

namespace TicTacToe.BL.Helpers
{
    public class WinHelper
    {
        private GameBL game;
        private char[,] board;
        private readonly IStatisticServiceBL _statisticServiceBL;

        public GameBL Game
        {
            set => game = value;
        }

        public char[,] Board
        {
            set => board = value;
        }

        public WinHelper(IStatisticServiceBL statisticServiceBL)
        {
            this._statisticServiceBL = statisticServiceBL;
        }

        public async Task FindWinnerAndLoser()
        {
            var res = GetWinner();

            if (!game.IsPlayer2Bot)
            {
                await SaveGameResult(new GameResultBL
                {
                    Id = Guid.NewGuid(),
                    GameId = game.GameId.Value,
                    PlayerId = res.winner,
                    Result = Models.ResultStatus.Won
                });

                await SaveGameResult(new GameResultBL
                {
                    Id = Guid.NewGuid(),
                    GameId = game.GameId.Value,
                    PlayerId = res.loser,
                    Result = Models.ResultStatus.Lost
                });
            }
            else
            {
                await SaveGameResult(new GameResultBL
                {
                    Id = Guid.NewGuid(),
                    GameId = game.GameId.Value,
                    PlayerId = game.Player1Id,
                    Result = res.winner == game.Player1Id ? Models.ResultStatus.Won : Models.ResultStatus.Lost
                });
            }
        }

        private (Guid winner, Guid loser) GetWinner()
        {
            var list = board.Cast<char>();
            var x = list.Count(x => x == 'X');
            var y = list.Count(x => x == 'O');
            if (!game.IsPlayer2Bot)
                return x > y ? (game.Player1Id, game.Player2Id.Value) : (game.Player2Id.Value, game.Player1Id);
            else
                return x > y ? (game.Player1Id, Guid.Empty) : (Guid.Empty, game.Player1Id);
        }

        public async Task SaveGameResult(GameResultBL gameResult)
        {
            await _statisticServiceBL.SaveStatisticAsync(gameResult);
        }
    }
}
