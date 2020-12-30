using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.WebApi.Enum;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameModel>> GetGamesByUserAsync(Guid id);
        Task<bool> CreateGameAsync(GameModel game);
        Task<CheckState> SavePlayerMoveAsync(GameHistoryModel history);
    }
}
