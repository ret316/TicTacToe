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
            return await _dataBaseContextDL.GameHistories.Where(g => g.GameId == id).ToArrayAsync();
        }

        public async Task SaveStatisticAsync(GameResultDL gameResult)
        {
            await _dataBaseContextDL.GameResults.AddAsync(gameResult);
            await _dataBaseContextDL.SaveChangesAsync();
        }
    }
}
