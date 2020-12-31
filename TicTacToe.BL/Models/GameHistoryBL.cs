using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.BL.Models
{
    /// <summary>
    /// Game history model in business layer
    /// </summary>
    public class GameHistoryBL
    {
        /// <summary>
        /// Game id
        /// </summary>
        public Guid GameId { get; set; }
        /// <summary>
        /// Player id, if not bot move
        /// </summary>
        public Guid? PlayerId { get; set; }
        /// <summary>
        /// Is player bot
        /// </summary>
        public bool IsBot { get; set; }
        /// <summary>
        /// X axis
        /// </summary>
        public int XAxis { get; set; }
        /// <summary>
        /// Y axis
        /// </summary>
        public int YAxis { get; set; }
        /// <summary>
        /// Move date
        /// </summary>
        public DateTime MoveDate { get; set; }
    }
}
