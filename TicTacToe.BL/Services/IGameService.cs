using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BusinessComponent.Enum;
using TicTacToe.BusinessComponent.Models;

namespace TicTacToe.BusinessComponent.Services
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
        Task<bool> CreateGameAsync(Game game);
        /// <summary>
        /// Method for saving players last move
        /// </summary>
        /// <param name="newMove">Game history model</param>
        /// <returns>Check state</returns>
        Task<CheckState> SavePlayerMoveAsync(GameHistory newMove);
        /// <summary>
        /// Method for updating game status in base
        /// </summary>
        /// <param name="game">Game id</param>
        /// <returns></returns>
        Task<bool> SetGameAsFinished(Guid game);
        //Task SaveGameResult(GameResultBL gameResult);
    }
}
