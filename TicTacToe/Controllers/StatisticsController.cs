﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.WebApi.Models;
using TicTacToe.WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }
        // GET api/<StatisticsController>/5
        [HttpGet("games/{id}")]
        public async Task<IActionResult> GetGames(Guid id)
        {
            var games = await _statisticService.GetAllUserGamesAsync(id);

            if (games.Any())
            {
                return NotFound("No Games");
            }

            return Ok(games);
        }

        [HttpGet("history/{id}")]
        public async Task<IActionResult> GetGameHistory(Guid id)
        {
            var history = await _statisticService.GetGameHistoryAsync(id);
            if (history.Any())
            {
                return NotFound("No game history");
            }

            return Ok(history);
        }
    }
}
