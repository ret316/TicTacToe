using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.DL.Models
{
    public class GameHistoryDL
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid? PlayerId { get; set; }
        public bool IsBot { get; set; }
        //public int Move { get; set; }
        public int XAxis { get; set; }
        public int YAxis { get; set; }
        public DateTime MoveDate { get; set; }
    }
}
