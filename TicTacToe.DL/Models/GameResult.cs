using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.DataComponent.Enum;

namespace TicTacToe.DataComponent.Models
{
    /// <summary>
    /// Game results model in data layer
    /// </summary>
    public class GameResult
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
