using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacToe.DataComponent.Config;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.DataComponent.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly DataBaseContext _dataBaseContext;
        public GameService(DataBaseContext dataBaseContext)
        {
            this._dataBaseContext = dataBaseContext;
        }

        public async Task CreateGameAsync(Game game)
        {
            await _dataBaseContext.Games.AddAsync(game);
            await _dataBaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameHistory>> GetGameHistoriesAsync(Guid id)
        {
            return await _dataBaseContext.GameHistories.Where(h => h.GameId == id).OrderBy(d => d.MoveDate).ToListAsync();
        }

        public async Task<Game> GetGameByGameIdAsync(Guid id)
        {
            return await _dataBaseContext.Games.FirstOrDefaultAsync(g => g.GameId == id);
        }

        public async Task<IEnumerable<Game>> GetGamesByUserAsync(Guid id)
        {
            return await _dataBaseContext.Games.Where(g => g.Player1Id == id || g.Player2Id == id).ToListAsync();
        }

        public async Task SavePlayerMoveAsync(GameHistory history)
        {
            await _dataBaseContext.GameHistories.AddAsync(history);
            await _dataBaseContext.SaveChangesAsync();
        }

        public async Task SetGameAsFinishedAsync(Game game)
        {
            _dataBaseContext.Entry(game).State = EntityState.Modified;
            await _dataBaseContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _dataBaseContext.Games.ToListAsync();
        }
    }
}
