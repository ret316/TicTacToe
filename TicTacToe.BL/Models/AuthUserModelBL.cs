﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.BL.Models
{
    public class AuthUserModelBL
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
