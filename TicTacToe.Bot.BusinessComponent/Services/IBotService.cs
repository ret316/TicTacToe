using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Bot.BusinessComponent.Models;

namespace TicTacToe.Bot.BusinessComponent.Services
{
    public interface IBotService
    {
        /// <summary>
        /// Method for getting new move coordinates
        /// </summary>
        /// <param name="gameState">Actual game state</param>
        void CalculateMove(GameState gameState);
    }
}
