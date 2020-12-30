using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Enum;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Services
{
    public interface IGameServiceBL
    {
        Task<IEnumerable<GameBL>> GetGamesByUserAsync(Guid id);
        Task<GameBL> GetGameByGameIdAsync(Guid id);
        Task<bool> CreateGameAsync(GameBL game);
        Task<CheckStateBL> SavePlayerMoveAsync(GameHistoryBL historyBL);
        Task SetGameAsFinished(Guid game);
        //Task SaveGameResult(GameResultBL gameResult);
    }
}
