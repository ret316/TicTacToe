using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicTacToe.BusinessComponent.Enum;

namespace TicTacToe.BusinessComponent.Models
{
    /// <summary>
    /// Game results model in business layer
    /// </summary>
    public class GameResult
    {
        /// <summary>
        /// Result id
        /// </summary>
        [Required] public Guid Id { get; set; }
        /// <summary>
        /// Game id
        /// </summary>
        [Required] public Guid GameId { get; set; }
        /// <summary>
        /// Player id
        /// </summary>
        [Required] public Guid PlayerId { get; set; }
        /// <summary>
        /// Game score
        /// </summary>
        [Required] public ResultStatus Result { get; set; }
    }
}
