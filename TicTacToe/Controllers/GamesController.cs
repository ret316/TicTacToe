using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.WebApi.Extensions;
using TicTacToe.WebApi.Models;
using TicTacToe.WebApi.Services;

namespace TicTacToe.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            this._gameService = gameService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGame([FromBody] GameModel gameModel)
        {
            await _gameService.CreateGameAsync(gameModel);

            return Ok();
        }

        [HttpPost("move")]
        public async Task<IActionResult> PlayerMove([FromBody] GameHistoryModel gameModel)
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

        [HttpGet]
        public async Task<IActionResult> GetAllGamesForUser(Guid userId)
        {
           var games = await _gameService.GetGamesByUserAsync(userId);

           if (games.Any())
           {
               return Ok(games);
           }

           return NoContent();
        }
    }
}
