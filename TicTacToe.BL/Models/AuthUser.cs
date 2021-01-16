using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.BusinessComponent.Models
{
    /// <summary>
    /// Model for user authentication in business layer
    /// </summary>
    public class AuthUser
    {
        /// <summary>
        /// User id
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
        /// User authentication token
        /// </summary>
        public string Token { get; set; }
    }
}
