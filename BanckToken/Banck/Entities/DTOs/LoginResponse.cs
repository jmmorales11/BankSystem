using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class LoginResponse:ResponseDto
    {
        public string Token { get; set; }  
        public string Email { get; set; }   
        public string Role { get; set; }    

    }

}
