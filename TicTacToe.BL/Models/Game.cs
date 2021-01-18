using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicTacToe.BusinessComponent.Models
{
    /// <summary>
    /// Game model in business layer
    /// </summary>
    public class Game
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Game id
        /// </summary>
        public Guid? GameId { get; set; }
        /// <summary>
        /// Player 1 id
        /// </summary>
        [Required] public Guid Player1Id { get; set; }
        /// <summary>
        /// Player 2 id
        /// </summary>
        public Guid Player2Id { get; set; }
        ///// <summary>
        ///// Is game with bot
        ///// </summary>
        //[Required] public bool IsPlayer2Bot { get; set; }
        /// <summary>
        /// Is game ended, not used in creation
        /// </summary>
        [Required] public bool IsGameFinished { get; set; }
    }
}
