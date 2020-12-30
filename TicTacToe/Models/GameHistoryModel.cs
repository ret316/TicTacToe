using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Models
{
    public class GameHistoryModel
    {
        public Guid GameId { get; set; }
        public Guid? PlayerId { get; set; }
        public bool IsBot { get; set; }
        public int XAxis { get; set; }
        public int YAxis { get; set; }
        public DateTime? MoveDate { get; set; }
    }
}
