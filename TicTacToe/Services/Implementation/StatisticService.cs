using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.BL.Services;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services.Implementation
{
    public class StatisticService : IStatisticService
    {
        private readonly IStatisticServiceBL _statisticServiceBL;
        private readonly IMapper _mapper;
        public StatisticService(IStatisticServiceBL statisticServiceBL, IMapper mapper)
        {
            this._statisticServiceBL = statisticServiceBL;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<GameResultModel>> GetAllUserGamesAsync(Guid id)
        {
            var results = await _statisticServiceBL.GetAllUserGamesAsync(id);
            return results.Select(r => _mapper.Map<GameResultModel>(r));
        }

        public async Task<IEnumerable<GameHistoryModel>> GetGameHistoryAsync(Guid id)
        {
            var results = await _statisticServiceBL.GetGameHistoryAsync(id);
            return results.Select(r => _mapper.Map<GameHistoryModel>(r));
        }

        public async Task<IEnumerable<UserGamesStatisticModel>> GetTop10PlayersAsync()
        {
            var results = await _statisticServiceBL.GetTop10PlayersAsync();
            return results.Select(x => _mapper.Map<UserGamesStatisticModel>(x));
        }
    }
}
