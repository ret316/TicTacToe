using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.DL.Models
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
    /// Game results model in data layer
    /// </summary>
    public class GameResultDL
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
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
