using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Models
{
    /// <summary>
    /// Game history model in api
    /// </summary>
    public class GameHistoryModel
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
        public DateTime? MoveDate { get; set; }
    }
}
