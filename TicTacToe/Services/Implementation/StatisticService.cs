using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.BusinessComponent.Services;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services.Implementation
{
    public class StatisticService : IStatisticService
    {
        private readonly BusinessComponent.Services.IStatisticService _statisticService;
        private readonly IMapper _mapper;

        public StatisticService(BusinessComponent.Services.IStatisticService statisticService, IMapper mapper)
        {
            this._statisticService = statisticService;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<GameResult>> GetAllUserGamesAsync(Guid id)
        {
            var results = await _statisticService.GetAllUserGamesAsync(id);
            return results.Select(r => _mapper.Map<GameResult>(r));
        }

        public async Task<IEnumerable<GameHistory>> GetGameHistoryAsync(Guid id)
        {
            var results = await _statisticService.GetGameHistoryAsync(id);
            return results.Select(r => _mapper.Map<GameHistory>(r));
        }

        public async Task<IEnumerable<UserGamesStatistic>> GetTop10PlayersAsync()
        {
            var results = await _statisticService.GetTop10PlayersAsync();
            return results.Select(x => _mapper.Map<UserGamesStatistic>(x));
        }
    }
}
