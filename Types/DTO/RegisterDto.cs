using System;
using System.Collections.Generic;
using System.Text;

namespace Types.DTO
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
