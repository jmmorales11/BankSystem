﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Success { get; set; }
        
    }
}
