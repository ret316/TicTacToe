using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DL.Models;

namespace TicTacToe.DL.Services
{
    /// <summary>
    /// Interface of statistic service
    /// </summary>
    public interface IStatisticServiceDL
    {
        /// <summary>
        /// Method for getting user games results
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Collection of game results</returns>
        Task<IEnumerable<GameResultDL>> GetAllUserGamesAsync(Guid id);
        /// <summary>
        /// Method for getting game chronology of game
        /// </summary>
        /// <param name="id">Game id</param>
        /// <returns>Collection of game moves</returns>
        Task<IEnumerable<GameHistoryDL>> GetGameHistoryAsync(Guid id);
        /// <summary>
        /// Method for saving game results in base
        /// </summary>
        /// <param name="gameResult">Model of game results</param>
        /// <returns></returns>
        Task SaveStatisticAsync(GameResultDL gameResult);
        Task<IEnumerable<UserGamesStatisticDL>> GetTop10PlayersAsync();
    }
}
