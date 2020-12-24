using System;

namespace TicTacToe.DL.Models
{
    public class UserDL
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}