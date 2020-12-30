using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.WebApi.Enum;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly IGameServiceBL _gameServiceBL;
        private readonly IMapper _mapper;
        public GameService(IGameServiceBL gameServiceBL, IMapper mapper)
        {
            this._gameServiceBL = gameServiceBL;
            this._mapper = mapper;
        }
        public async Task<bool> CreateGameAsync(GameModel game)
        {

            return await _gameServiceBL.CreateGameAsync(new GameBL
            {
                Player1Id = game.Player1Id,
                Player2Id = game.Player2Id,
                IsPlayer2Bot = game.IsPlayer2Bot
            });
        }

        public async Task<IEnumerable<GameModel>> GetGamesByUserAsync(Guid id)
        {
            var games = await _gameServiceBL.GetGamesByUserAsync(id);
            return games.Select(g => _mapper.Map<GameModel>(g));
        }

        public async Task<CheckState> SavePlayerMoveAsync(GameHistoryModel history)
        {
           var playerMoveResult = await _gameServiceBL.SavePlayerMoveAsync(_mapper.Map<GameHistoryBL>(history));
           return (CheckState) (int) playerMoveResult;
        }
    }
}
