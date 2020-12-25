using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Models
{
    public class UserAuthModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
