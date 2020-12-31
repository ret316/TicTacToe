using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Models
{
    /// <summary>
    /// Game results
    /// </summary>
    public enum ResultStatus
    {
        Draw,
        Lost,
        Won
    }

    /// <summary>
    /// Game results model in api
    /// </summary>
    public class GameResultModel
    {
        /// <summary>
        /// Game id
        /// </summary>
        public Guid GameId { get; set; }
        /// <summary>
        /// Player id
        /// </summary>
        public Guid PlayerId { get; set; }
        /// <summary>
        /// Game score
        /// </summary>
        public ResultStatus Result { get; set; }
    }
}
