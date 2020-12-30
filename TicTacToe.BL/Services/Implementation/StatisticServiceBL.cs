using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Models;
using TicTacToe.DL.Services;

namespace TicTacToe.BL.Services.Implementation
{
    public class StatisticServiceBL : IStatisticServiceBL
    {
        private readonly IStatisticServiceDL _statisticServiceDL;

        public StatisticServiceBL(IStatisticServiceDL statisticService)
        {
            this._statisticServiceDL = statisticService;
        }

        public async Task<IEnumerable<GameResultBL>> GetAllUserGamesAsync(Guid id)
        {
            var results = await _statisticServiceDL.GetAllUserGamesAsync(id);
            return results.Select(r => new GameResultBL
            {
                GameId = r.GameId,
                PlayerId = r.PlayerId,
                Result = (ResultStatus) (int) r.Result
            });
        }

        public async Task<IEnumerable<GameHistoryBL>> GetGameHistoryAsync(Guid id)
        {
            var results = await _statisticServiceDL.GetGameHistoryAsync(id);
            return results.Select(h => new GameHistoryBL
            {
                GameId = h.GameId,
                PlayerId = h.PlayerId,
                IsBot = h.IsBot,
                XAxis = h.XAxis,
                YAxis = h.YAxis,
                MoveDate = h.MoveDate
            });
        }

        public async Task SaveStatisticAsync(GameResultBL gameResult)
        {
            await _statisticServiceDL.SaveStatisticAsync(new DL.Models.GameResultDL
            {
                Id = Guid.NewGuid(),
                GameId = gameResult.GameId,
                PlayerId = gameResult.PlayerId,
                Result = (DL.Models.ResultStatus) (int) gameResult.Result
            });
        }
    }
}
