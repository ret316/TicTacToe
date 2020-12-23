using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.DL.Config;

namespace TicTacToe.DL.Services.Implementation
{
    public class UserServiceDL : IUserServiceDL
    {
        private DataBaseContext _dataBaseContext;
        public UserServiceDL(DataBaseContext dataBaseContext)
        {
            this._dataBaseContext = dataBaseContext;
        }
    }
}
