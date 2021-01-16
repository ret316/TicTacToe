using System;
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.BusinessComponent.Models
{
    /// <summary>
    /// User model in business layer
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
        [Required] public string Email { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        [Required] public string Password { get; set; }
    }
}