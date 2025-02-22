using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class VerifyCodeRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public bool Success { get; set; }
    }
}