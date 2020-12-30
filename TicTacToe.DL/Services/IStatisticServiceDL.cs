using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DL.Models;

namespace TicTacToe.DL.Services
{
    public interface IStatisticServiceDL
    {
        Task<IEnumerable<GameResultDL>> GetAllUserGamesAsync(Guid id);
        Task<IEnumerable<GameHistoryDL>> GetGameHistoryAsync(Guid id);
        Task SaveStatisticAsync(GameResultDL gameResult);
    }
}
