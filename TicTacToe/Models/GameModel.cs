using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Models
{
    /// <summary>
    /// Game model in api
    /// </summary>
    public class GameModel
    {
        /// <summary>
        /// Game id, not used in game creation
        /// </summary>
        public Guid? GameId { get; set; }
        /// <summary>
        /// Player id
        /// </summary>
        public Guid Player1Id { get; set; }
        /// <summary>
        /// Player id. can be null if game with bot
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
