using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.DL.Models
{
    /// <summary>
    /// Game model in data layer
    /// </summary>
    public class GameDL
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Game id
        /// </summary>
        public Guid GameId { get; set; }
        /// <summary>
        /// Player 1 id
        /// </summary>
        public Guid Player1Id { get; set; }
        /// <summary>
        /// Player 2 id
        /// </summary>
        public Guid? Player2Id { get; set; }
        /// <summary>
        /// Is game with bot
        /// </summary>
        public bool IsPlayer2Bot { get; set; }
        /// <summary>
        /// Is game ended, not used in creation
        /// </summary>
        public bool IsGameFinished { get; set; }
    }
}
