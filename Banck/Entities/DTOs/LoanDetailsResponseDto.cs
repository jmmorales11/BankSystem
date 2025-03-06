using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class LoanDetailsResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public LoanDetailsDTO Data { get; set; }
    }
}
