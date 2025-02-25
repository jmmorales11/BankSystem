using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
   public class AmortizationResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Amortization> AmortizationSchedule { get; set; }
    }
}
