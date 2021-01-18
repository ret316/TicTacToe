using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Bot.Models;
using TicTacToe.Bot.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Bot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IBotService _botService;
        public BotController(IBotService botService)
        {
            this._botService = botService;
        }

        // POST api/<BotController>
        [HttpPost]
        public void Post([FromBody] GameState value)
        {
            _botService.CalculateMove(value);
        }
    }
}
