using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class UserDataResponseDto : ResponseDto
    {
        public User_Data UserData { get; set; }
    }

}
