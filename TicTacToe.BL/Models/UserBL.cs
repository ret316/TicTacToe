using System;

namespace TicTacToe.BL.Models
{
    public class UserBL
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}