using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DL.Models;

namespace TicTacToe.DL.Services
{
    public interface IGameServiceDL
    {
        Task<IEnumerable<GameDL>> GetGamesByUserAsync(Guid id);
        Task<GameDL> GetGameByGameIdAsync(Guid id);
        Task CreateGameAsync(GameDL game);
        Task SavePlayerMoveAsync(GameHistoryDL historyDL);
        Task<IEnumerable<GameHistoryDL>> GetGameHistoriesAsync(Guid Id);
        Task SetGameAsFinisheed(GameDL game);
    }
}
