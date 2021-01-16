using System;

namespace TicTacToe.WebApi.Models
{
    /// <summary>
    /// User model in api layer
    /// </summary>
    public class User
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