using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.WebApi.Enum;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly IGameServiceBL _gameServiceBL;
        public GameService(IGameServiceBL gameServiceBL)
        {
            this._gameServiceBL = gameServiceBL;
        }
        public async Task CreateGameAsync(GameModel game)
        {
            await _gameServiceBL.CreateGameAsync(new GameBL
            {
                Player1Id = game.Player1Id,
                Player2Id = game.Player2Id,
                IsPlayer2Bot = game.IsPlayer2Bot
            });
        }

        public async Task<IEnumerable<GameModel>> GetGamesByUserAsync(Guid id)
        {
            var games = await _gameServiceBL.GetGamesByUserAsync(id);
            return games.Select(g => new GameModel
            {
                GameId = g.GameId.Value,
                Player1Id = g.Player1Id,
                Player2Id = g.Player2Id,
                IsPlayer2Bot = g.IsPlayer2Bot,
                IsGameFinished = g.IsGameFinished
            });
        }

        public async Task<CheckState> SavePlayerMoveAsync(GameHistoryModel history)
        {
           var playerMoveResult = await _gameServiceBL.SavePlayerMoveAsync(new GameHistoryBL
            {
                GameId = history.GameId,
                PlayerId = history.PlayerId,
                IsBot = history.IsBot,
                XAxis = history.XAxis,
                YAxis = history.YAxis,
                MoveDate = history.MoveDate ?? DateTime.Now
            });
           return (CheckState) (int) playerMoveResult;
        }
    }
}
