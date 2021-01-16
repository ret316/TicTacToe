using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.DataComponent.Models
{
    public class UserGamesStatistic
    {
        public Guid PlayerId { get; set; }
        public int GameCount { get; set; }
        public int WinCount { get; set; }
        public int LostCount { get; set; }
        public int DrawCount { get; set; }
    }
}
