using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.BL.Models
{
    public class GameBL
    {
        public Guid Id { get; set; }
        public Guid? GameId { get; set; }
        public Guid Player1Id { get; set; }
        public Guid? Player2Id { get; set; }
        public bool IsPlayer2Bot { get; set; }
        public bool IsGameFinished { get; set; }
    }
}
