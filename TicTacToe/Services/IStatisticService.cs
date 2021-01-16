using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services
{
    public interface IStatisticService
    {
        /// <summary>
        /// Method for getting all user games results
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Collection of game results</returns>
        Task<IEnumerable<GameResult>> GetAllUserGamesAsync(Guid id);
        /// <summary>
        /// Method for getting game history that contains all moves
        /// </summary>
        /// <param name="id">Game id</param>
        /// <returns>Collection of game movements</returns>
        Task<IEnumerable<GameHistory>> GetGameHistoryAsync(Guid id);
        /// <summary>
        /// Method for getting top 10 best players
        /// </summary>
        /// <returns>Collection of players</returns>
        Task<IEnumerable<UserGamesStatistic>> GetTop10PlayersAsync();
    }
}
