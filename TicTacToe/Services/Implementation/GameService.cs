using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.BusinessComponent.Services;
using TicTacToe.WebApi.Enum;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly BusinessComponent.Services.IGameService _gameServiceBL;
        private readonly IMapper _mapper;
        public GameService(BusinessComponent.Services.IGameService gameServiceBL, IMapper mapper)
        {
            this._gameServiceBL = gameServiceBL;
            this._mapper = mapper;
        }
        public async Task<bool> CreateGameAsync(Models.Game game)
        {

            return await _gameServiceBL.CreateGameAsync(new BusinessComponent.Models.Game
            {
                Player1Id = game.Player1Id,
                Player2Id = game.Player2Id,
            });
        }

        public async Task<IEnumerable<Models.Game>> GetAllGamesAsync()
        {
            var result = await _gameServiceBL.GetAllGamesAsync();
            return result.Select(r => _mapper.Map<Models.Game>(r));
        }

        public async Task<IEnumerable<Models.Game>> GetGamesByUserAsync(Guid id)
        {
            var games = await _gameServiceBL.GetGamesByUserAsync(id);
            return games.Select(g => _mapper.Map<Models.Game>(g));
        }

        public async Task<CheckState> SavePlayerMoveAsync(Models.GameHistory history)
        {
           var playerMoveResult = await _gameServiceBL.SavePlayerMoveAsync(_mapper.Map<BusinessComponent.Models.GameHistory>(history));
           return (CheckState) (int) playerMoveResult;
        }

        public async Task<bool> SetGameAsFinished(Guid id)
        {
            return await _gameServiceBL.SetGameAsFinished(id);
        }
    }
}
