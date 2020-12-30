using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;
using TicTacToe.DL.Services;
using ResultStatus = TicTacToe.BL.Models.ResultStatus;

namespace TicTacToe.BL.Services.Implementation
{
    public class StatisticServiceBL : IStatisticServiceBL
    {
        private readonly IStatisticServiceDL _statisticServiceDL;
        private readonly IMapper _mapper;
        public StatisticServiceBL(IStatisticServiceDL statisticService, IMapper mapper)
        {
            this._statisticServiceDL = statisticService;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<GameResultBL>> GetAllUserGamesAsync(Guid id)
        {
            var results = await _statisticServiceDL.GetAllUserGamesAsync(id);
            return results.Select(r => _mapper.Map<GameResultBL>(r));
        }

        public async Task<IEnumerable<GameHistoryBL>> GetGameHistoryAsync(Guid id)
        {
            var results = await _statisticServiceDL.GetGameHistoryAsync(id);
            return results.Select(h => _mapper.Map<GameHistoryBL>(h));
        }

        public async Task SaveStatisticAsync(GameResultBL gameResult)
        {
            await _statisticServiceDL.SaveStatisticAsync(_mapper.Map<GameResultDL>(gameResult));
        }
    }
}
