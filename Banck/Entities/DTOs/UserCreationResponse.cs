using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class UserCreationResponse
    {
        public User User { get; set; }
        public string Message { get; set; }

        public bool Success { get; set; }
    }
}
