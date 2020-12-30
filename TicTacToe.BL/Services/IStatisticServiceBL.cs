using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Services
{
    public interface IStatisticServiceBL
    {
        Task<IEnumerable<GameResultBL>> GetAllUserGamesAsync(Guid id);
        Task<IEnumerable<GameHistoryBL>> GetGameHistoryAsync(Guid id);
        Task SaveStatisticAsync(GameResultBL gameResult);
    }
}
