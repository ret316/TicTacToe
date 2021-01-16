using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BusinessComponent.Models;

namespace TicTacToe.BusinessComponent.Services
{
    public interface IAuthentication
    {
        /// <summary>
        /// Method for authentication that generates user tokens
        /// </summary>
        /// <param name="email">Users email</param>
        /// <param name="password">Users password</param>
        /// <returns></returns>
        Task<AuthUser> Authenticate(string email, string password);
    }
}
