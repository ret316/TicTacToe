using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.DataComponent.Models;
using TicTacToe.DataComponent.Services;

namespace TicTacToe.BusinessComponent.Services.Implementation
{
    public class StatisticService : IStatisticService
    {
        private readonly DataComponent.Services.IStatisticService _statisticService;
        private readonly IMapper _mapper;
        public StatisticService(DataComponent.Services.IStatisticService statisticService, IMapper mapper)
        {
            this._statisticService = statisticService;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<Models.GameResult>> GetAllUserGamesAsync(Guid id)
        {
            var results = await _statisticService.GetAllUserGamesAsync(id);
            return results.Select(r => _mapper.Map<Models.GameResult>(r));
        }

        public async Task<IEnumerable<Models.GameHistory>> GetGameHistoryAsync(Guid id)
        {
            var results = await _statisticService.GetGameHistoryAsync(id);
            return results.Select(h => _mapper.Map<Models.GameHistory>(h));
        }

        public async Task<bool> SaveStatisticAsync(Models.GameResult gameResult)
        {
            try
            {
                await _statisticService.SaveStatisticAsync(_mapper.Map<DataComponent.Models.GameResult>(gameResult));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<IEnumerable<Models.UserGamesStatistic>> GetTop10PlayersAsync()
        {
            var results = await _statisticService.GetTop10PlayersAsync();
            return results.Select(x => _mapper.Map<Models.UserGamesStatistic>(x));
        }
    }
}
