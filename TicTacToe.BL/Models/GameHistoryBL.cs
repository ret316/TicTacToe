using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicTacToe.BL.Services;

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
        [Required] public Guid GameId { get; set; }
        /// <summary>
        /// Player id, if not bot move
        /// </summary>
        public Guid? PlayerId { get; set; }
        /// <summary>
        /// Is player bot
        /// </summary>
        [Required] public bool IsBot { get; set; }
        /// <summary>
        /// X axis
        /// </summary>
        [Required] [Range(0, IFieldChecker.BOARD_SIZE)] public int XAxis { get; set; }
        /// <summary>
        /// Y axis
        /// </summary>
        [Required] [Range(0, IFieldChecker.BOARD_SIZE)] public int YAxis { get; set; }
        /// <summary>
        /// Move date
        /// </summary>
        public DateTime MoveDate { get; set; }
    }
}
