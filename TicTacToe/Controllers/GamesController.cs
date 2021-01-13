using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TicTacToe.WebApi.Extensions;
using TicTacToe.WebApi.Models;
using TicTacToe.WebApi.Services;

namespace TicTacToe.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")] [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            this._gameService = gameService;
        }

        /// <summary>
        /// Method for game creating
        /// <para>POST api/games/create</para>
        /// </summary>
        /// <param name="gameModel">Game model for creation</param>
        /// <returns></returns>
        [HttpPost("create")] public async Task<IActionResult> CreateGame([FromBody] GameModel gameModel)
        {
            var result = await _gameService.CreateGameAsync(gameModel);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Method for adding players move to database
        /// <para>POST api/games/move</para>
        /// </summary>
        /// <param name="gameModel">Game position</param>
        /// <returns></returns>
        [HttpPost("move")] public async Task<IActionResult> PlayerMove([FromBody] GameHistoryModel gameModel)
        {
            var result = await _gameService.SavePlayerMoveAsync(gameModel);

            switch (result)
            {
                case Enum.CheckState.LineCheck:
                case Enum.CheckState.DiagonalCheck:
                case Enum.CheckState.None:
                    return Ok(result.GetDescription());
            }

            return BadRequest(result.GetDescription());
        }

        /// <summary>
        /// Method for getting all games that user played
        /// <para>GET api/games</para>
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        [HttpGet("{userId}")] public async Task<IActionResult> GetAllGamesForUser(Guid userId)
        {
            var games = await _gameService.GetGamesByUserAsync(userId);

            if (games.Any())
            {
                return Ok(games);
            }

            return NoContent();
        }

        /// <summary>
        /// Method for getting all games
        /// </summary>
        /// <returns>Collection of games</returns>
        [HttpGet] public async Task<IActionResult> GetAllGamesAsync()
        {
            var games = await _gameService.GetAllGamesAsync();
            if (games.Any())
            {
                return Ok(games);
            }

            return NoContent();
        }
    }
}
