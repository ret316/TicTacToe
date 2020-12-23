using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.DL.Services;

namespace TicTacToe.BL.Services.Implementation
{
    public class UserServiceBL : IUserServiceBL
    {
        private readonly IUserServiceDL _userServiceDL;
        public UserServiceBL(IUserServiceDL userServiceDL)
        {
            this._userServiceDL = userServiceDL;
        }
    }
}
