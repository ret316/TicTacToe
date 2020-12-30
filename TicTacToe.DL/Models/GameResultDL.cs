using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.DL.Models
{
    public enum ResultStatus
    {
        Draw,
        Lost,
        Won
    }
    public class GameResultDL
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public ResultStatus Result { get; set; }
    }
}
