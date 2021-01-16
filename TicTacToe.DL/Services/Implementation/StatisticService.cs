using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacToe.DataComponent.Config;
using TicTacToe.DataComponent.Enum;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.DataComponent.Services.Implementation
{
    public class StatisticService : IStatisticService
    {
        private readonly DataBaseContext _dataBaseContext;

        public StatisticService(DataBaseContext dataBaseContext)
        {
            this._dataBaseContext = dataBaseContext;
        }

        public async Task<IEnumerable<GameResult>> GetAllUserGamesAsync(Guid id)
        {
            return await _dataBaseContext.GameResults.Where(g => g.PlayerId == id).ToListAsync();
        }

        public async Task<IEnumerable<GameHistory>> GetGameHistoryAsync(Guid id)
        {
            return await _dataBaseContext.GameHistories.Where(g => g.GameId == id).ToListAsync();
        }

        public async Task SaveStatisticAsync(GameResult gameResult)
        {
            await _dataBaseContext.GameResults.AddAsync(gameResult);
            await _dataBaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserGamesStatistic>> GetTop10PlayersAsync()
        {
            return await _dataBaseContext.GameResults.Select(p => p.PlayerId).Distinct().Select(x =>
                new UserGamesStatistic
                {
                    PlayerId = x,
                    GameCount = _dataBaseContext.GameResults.Count(x1 => x == x1.PlayerId),
                    WinCount = _dataBaseContext.GameResults.Count(x2 =>
                        x == x2.PlayerId && x2.Result == ResultStatus.Won),
                    LostCount = _dataBaseContext.GameResults.Count(x3 =>
                        x == x3.PlayerId && x3.Result == ResultStatus.Lost),
                    DrawCount = _dataBaseContext.GameResults.Count(x4 =>
                        x == x4.PlayerId && x4.Result == ResultStatus.Draw),
                }).OrderByDescending(x5 => x5.WinCount).Take(10).ToListAsync();
        }
    }
}
