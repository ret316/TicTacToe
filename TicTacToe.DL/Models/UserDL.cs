using System;

namespace TicTacToe.DL.Models
{
    /// <summary>
    /// User model in data layer
    /// </summary>
    public class UserDL
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
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
        public byte[] Password { get; set; }
        /// <summary>
        /// User passwords salt
        /// </summary>
        public byte[] PasswordSalt { get; set; }
    }
}