using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services
{
    public interface IStatisticService
    {
        Task<IEnumerable<GameResultModel>> GetAllUserGamesAsync(Guid id);
        Task<IEnumerable<GameHistoryModel>> GetGameHistoryAsync(Guid id);
    }
}
