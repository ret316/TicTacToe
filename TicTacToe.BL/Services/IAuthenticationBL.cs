using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Services
{
    public interface IAuthenticationBL
    {
        /// <summary>
        /// Method for authentication that generates user tokens
        /// </summary>
        /// <param name="email">Users email</param>
        /// <param name="password">Users password</param>
        /// <returns></returns>
        Task<AuthUserModelBL> Authenticate(string email, string password);
    }
}
