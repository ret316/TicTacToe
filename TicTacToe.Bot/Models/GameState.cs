﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Bot.Models
{
    /// <summary>
    /// Class with game info
    /// </summary>
    public class GameState
    {
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
        public Guid Player2Id { get; set; }
        /// <summary>
        /// Is game ended, not used in creation
        /// </summary>
        public bool IsGameFinished { get; set; }
        /// <summary>
        /// Game board
        /// </summary>
        public char[,] Board { get; set; }
    }
}
