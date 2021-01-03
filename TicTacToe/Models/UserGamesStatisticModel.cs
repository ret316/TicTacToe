using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Models
{
    public class UserGamesStatisticModel
    {
        public Guid PlayerId { get; set; }
        public int GameCount { get; set; }
        public int WinCount { get; set; }
        public int LostCount { get; set; }
        public int DrawCount { get; set; }
    }
}
