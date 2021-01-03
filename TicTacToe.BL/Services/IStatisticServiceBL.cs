using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Services
{
    /// <summary>
    /// Interface of statistic service
    /// </summary>
    public interface IStatisticServiceBL
    {
        /// <summary>
        /// Method for getting user games results
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Collection of game results</returns>
        Task<IEnumerable<GameResultBL>> GetAllUserGamesAsync(Guid id);
        /// <summary>
        /// Method for getting game chronology of game
        /// </summary>
        /// <param name="id">Game id</param>
        /// <returns>Collection of game moves</returns>
        Task<IEnumerable<GameHistoryBL>> GetGameHistoryAsync(Guid id);
        /// <summary>
        /// Method for saving game results in base
        /// </summary>
        /// <param name="gameResult">Model of game results</param>
        /// <returns></returns>
        Task<bool> SaveStatisticAsync(GameResultBL gameResult);
        Task<IEnumerable<UserGamesStatisticBL>> GetTop10PlayersAsync();
    }
}
