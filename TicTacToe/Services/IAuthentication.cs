using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services
{
    public interface IAuthentication
    {
        /// <summary>
        /// Method for authentication that generates user tokens
        /// </summary>
        /// <param name="user">User authentication model</param>
        /// <returns>Authentication model</returns>
        Task<AuthUser> Authenticate(UserAuth user);
    }
}
