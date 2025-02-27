using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }

}
