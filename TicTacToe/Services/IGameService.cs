﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.WebApi.Enum;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services
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
        /// Method for getting games that user had played
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Collection of games</returns>
        Task<IEnumerable<Game>> GetGamesByUserAsync(Guid id);
        /// <summary>
        /// Method for game creation
        /// </summary>
        /// <param name="game">Game model</param>
        /// <returns></returns>
        Task<bool> CreateGameAsync(Game game);
        /// <summary>
        /// Method for saving players last move
        /// </summary>
        /// <param name="history">Game history model</param>
        /// <returns>Move status</returns>
        Task<CheckState> SavePlayerMoveAsync(GameHistory history);
        /// <summary>
        /// Method for updating game as finished
        /// </summary>
        /// <param name="id">Game id</param>
        /// <returns></returns>
        Task<bool> SetGameAsFinished(Guid id);
    }
}
