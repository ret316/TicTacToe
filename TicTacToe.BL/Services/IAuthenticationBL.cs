using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Services
{
    public interface IAuthenticationBL
    {
        Task<AuthUserModelBL> Authenticate(string email, string password);
    }
}
