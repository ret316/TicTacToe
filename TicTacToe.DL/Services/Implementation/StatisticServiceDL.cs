using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacToe.DL.Config;
using TicTacToe.DL.Models;

namespace TicTacToe.DL.Services.Implementation
{
    public class StatisticServiceDL : IStatisticServiceDL
    {
        private readonly DataBaseContext _dataBaseContextDL;
        public StatisticServiceDL(DataBaseContext dataBaseContext)
        {
            this._dataBaseContextDL = dataBaseContext;
        }

        public async Task<IEnumerable<GameResultDL>> GetAllUserGamesAsync(Guid id)
        {
            return await _dataBaseContextDL.GameResults.Where(g => g.PlayerId == id).ToListAsync();
        }

        public async Task<IEnumerable<GameHistoryDL>> GetGameHistoryAsync(Guid id)
        {
            return await _dataBaseContextDL.GameHistories.Where(g => g.GameId == id).ToListAsync();
        }

        public async Task SaveStatisticAsync(GameResultDL gameResult)
        {
            await _dataBaseContextDL.GameResults.AddAsync(gameResult);
            await _dataBaseContextDL.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserGamesStatisticDL>> GetTop10PlayersAsync()
        {
            return await _dataBaseContextDL.GameResults.Select(p => p.PlayerId).Distinct().Select(x =>
                new UserGamesStatisticDL
                {
                    PlayerId = x,
                    GameCount = _dataBaseContextDL.GameResults.Count(x1 => x == x1.PlayerId),
                    WinCount = _dataBaseContextDL.GameResults.Count(x2 =>
                        x == x2.PlayerId && x2.Result == ResultStatus.Won),
                    LostCount = _dataBaseContextDL.GameResults.Count(x3 =>
                        x == x3.PlayerId && x3.Result == ResultStatus.Lost),
                    DrawCount = _dataBaseContextDL.GameResults.Count(x4 =>
                        x == x4.PlayerId && x4.Result == ResultStatus.Draw),
                }).OrderByDescending(x5 => x5.WinCount).Take(10).ToListAsync();
        }
    }
}
