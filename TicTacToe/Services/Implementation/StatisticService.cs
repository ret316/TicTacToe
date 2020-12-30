using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.BL.Services;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services.Implementation
{
    public class StatisticService : IStatisticService
    {
        private readonly IStatisticServiceBL _statisticServiceBL;

        public StatisticService(IStatisticServiceBL statisticServiceBL)
        {
            this._statisticServiceBL = statisticServiceBL;
        }

        public async Task<IEnumerable<GameResultModel>> GetAllUserGamesAsync(Guid id)
        {
            var results = await _statisticServiceBL.GetAllUserGamesAsync(id);
            return results.Select(r => new GameResultModel
            {
                GameId = r.GameId,
                PlayerId = r.PlayerId,
                Result = (ResultStatus) (int) r.Result
            });
        }

        public async Task<IEnumerable<GameHistoryModel>> GetGameHistoryAsync(Guid id)
        {
            var results = await _statisticServiceBL.GetGameHistoryAsync(id);
            return results.Select(r => new GameHistoryModel
            {
                GameId = r.GameId,
                PlayerId = r.PlayerId,
                IsBot = r.IsBot,
                XAxis = r.XAxis,
                YAxis = r.YAxis,
                MoveDate = r.MoveDate
            });
        }
    }
}
