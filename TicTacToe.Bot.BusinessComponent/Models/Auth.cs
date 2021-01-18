using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Bot.BusinessComponent.Models
{
    public class Auth
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
