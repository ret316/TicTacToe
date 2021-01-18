using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Bot.Models;

namespace TicTacToe.Bot.Services.Implementation
{
    public class BotService : IBotService
    {
        private readonly BusinessComponent.Services.IBotService _botService;
        public BotService(BusinessComponent.Services.IBotService botService)
        {
            this._botService = botService;
        }
        public void CalculateMove(GameState gameState)
        {
            _botService.CalculateMove(new BusinessComponent.Models.GameState
            {
                GameId = gameState.GameId,
                Player1Id = gameState.Player1Id,
                Player2Id = gameState.Player2Id,
                Board = gameState.Board,
                IsGameFinished = gameState.IsGameFinished
            });
        }
    }
}
