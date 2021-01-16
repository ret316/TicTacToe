using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.DataComponent.Services
{
    /// <summary>
    /// Interface of game service
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Method for getting all games
        /// </summary>
        /// <returns>Collection of games</returns>
        Task<IEnumerable<Game>> GetAllGamesAsync();
        /// <summary>
        /// Method for getting user games 
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Collection of games</returns>
        Task<IEnumerable<Game>> GetGamesByUserAsync(Guid id);
        /// <summary>
        /// Method for getting game info
        /// </summary>
        /// <param name="id">Game id</param>
        /// <returns>Game model</returns>
        Task<Game> GetGameByGameIdAsync(Guid id);
        /// <summary>
        /// Method for game creation
        /// </summary>
        /// <param name="game">Game info model </param>
        /// <returns></returns>
        Task CreateGameAsync(Game game);
        /// <summary>
        /// Method for saving players last move
        /// </summary>
        /// <param name="history">Game history model</param>
        /// <returns>Check state</returns>
        Task SavePlayerMoveAsync(GameHistory history);
        /// <summary>
        /// Method for getting game history
        /// </summary>
        /// <param name="id">Game id</param>
        /// <returns>Collection of game histories</returns>
        Task<IEnumerable<GameHistory>> GetGameHistoriesAsync(Guid id);
        /// <summary>
        /// Method for updating game status in base
        /// </summary>
        /// <param name="game">Game id</param>
        /// <returns></returns>
        Task SetGameAsFinishedAsync(Game game);
    }
}
