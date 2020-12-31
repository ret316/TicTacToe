using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.BL.Models
{
    /// <summary>
    /// Model for user authentication in business layer
    /// </summary>
    public class AuthUserModelBL
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
    }
}
