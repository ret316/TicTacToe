using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Models
{
    public enum ResultStatus
    {
        Draw,
        Lost,
        Won
    }
    public class GameResultModel
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public ResultStatus Result { get; set; }
    }
}
