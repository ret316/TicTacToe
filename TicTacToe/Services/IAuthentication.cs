using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Services
{
    public interface IAuthentication
    {
        Task<AuthUserModel> Authenticate(UserAuthModel user);
    }
}
