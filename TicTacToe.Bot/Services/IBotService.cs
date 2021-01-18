using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Bot.Models;

namespace TicTacToe.Bot.Services
{
    public interface IBotService
    {
        void CalculateMove(GameState gameState);
    }
}
