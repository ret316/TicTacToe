using System;

namespace TicTacToe.BL.Models
{
    /// <summary>
    /// User model in business layer
    /// </summary>
    public class UserBL
    {
        /// <summary>
        /// User id. not used in user creation
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }
    }
}