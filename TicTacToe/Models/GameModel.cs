using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Models
{
    public class GameModel
    {
        public Guid? GameId { get; set; }
        public Guid Player1Id { get; set; }
        public Guid? Player2Id { get; set; }
        public bool IsPlayer2Bot { get; set; }
        public bool IsGameFinished { get; set; }
    }
}
