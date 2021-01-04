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
    public class GameServiceDL : IGameServiceDL
    {
        private readonly DataBaseContext _dataBaseContext;
        public GameServiceDL(DataBaseContext dataBaseContext)
        {
            this._dataBaseContext = dataBaseContext;
        }

        public async Task CreateGameAsync(GameDL game)
        {
            await _dataBaseContext.Games.AddAsync(game);
            await _dataBaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameHistoryDL>> GetGameHistoriesAsync(Guid Id)
        {
            return await _dataBaseContext.GameHistories.Where(h => h.GameId == Id).OrderBy(d => d.MoveDate).ToListAsync();
        }

        public async Task<GameDL> GetGameByGameIdAsync(Guid id)
        {
            return await _dataBaseContext.Games.FirstOrDefaultAsync(g => g.GameId == id);
        }

        public async Task<IEnumerable<GameDL>> GetGamesByUserAsync(Guid id)
        {
            return await _dataBaseContext.Games.Where(g => g.Player1Id == id || g.Player2Id == id).ToListAsync();
        }

        public async Task SavePlayerMoveAsync(GameHistoryDL historyDL)
        {
            await _dataBaseContext.GameHistories.AddAsync(historyDL);
            await _dataBaseContext.SaveChangesAsync();
        }

        public async Task SetGameAsFinished(GameDL game)
        {
            _dataBaseContext.Entry(game).State = EntityState.Modified;
            await _dataBaseContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<GameDL>> GetAllGamesAsync()
        {
            return await _dataBaseContext.Games.ToListAsync();
        }
    }
}
