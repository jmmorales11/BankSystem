using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class ResponseAmortizationDto:ResponseDto
    {
        public Amortization Amortization { get; set; }
        public List<AmortizationDetailDto> AmortizationDetails { get; set; }
    }
}
