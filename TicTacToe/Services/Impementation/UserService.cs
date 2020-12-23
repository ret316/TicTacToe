using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.BL.Services;

namespace TicTacToe.WebApi.Services.Impementation
{
    public class UserService : IUserService
    {
        private readonly IUserServiceBL _userServiceBL;
        public UserService(IUserServiceBL userServiceBL)
        {
            this._userServiceBL = userServiceBL;
        }
    }
}
