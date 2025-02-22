using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class UserCreationResponse:ResponseDto
    {
        public User User { get; set; }
    }
}
