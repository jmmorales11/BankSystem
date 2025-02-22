using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class UserResponseDto:ResponseDto
    {
        public List<User> Users { get; set; }
        public User User { get; set; }  
    }
}
