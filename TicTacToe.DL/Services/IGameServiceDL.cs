using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DL.Models;

namespace TicTacToe.DL.Services
{
    /// <summary>
    /// Interface of game service
    /// </summary>
    public interface IGameServiceDL
    {
        /// <summary>
        /// Method for getting user games 
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Collection of games</returns>
        Task<IEnumerable<GameDL>> GetGamesByUserAsync(Guid id);
        /// <summary>
        /// Method for getting game info
        /// </summary>
        /// <param name="id">Game id</param>
        /// <returns>Game model</returns>
        Task<GameDL> GetGameByGameIdAsync(Guid id);
        /// <summary>
        /// Method for game creation
        /// </summary>
        /// <param name="game">Game info model </param>
        /// <returns></returns>
        Task CreateGameAsync(GameDL game);
        /// <summary>
        /// Method for saving players last move
        /// </summary>
        /// <param name="historyBL">Game history model</param>
        /// <returns>Check state</returns>
        Task SavePlayerMoveAsync(GameHistoryDL historyDL);
        /// <summary>
        /// Method for getting game history
        /// </summary>
        /// <param name="Id">Game id</param>
        /// <returns>Collection of game histories</returns>
        Task<IEnumerable<GameHistoryDL>> GetGameHistoriesAsync(Guid Id);
        /// <summary>
        /// Method for updating game status in base
        /// </summary>
        /// <param name="game">Game id</param>
        /// <returns></returns>
        Task SetGameAsFinished(GameDL game);
    }
}
