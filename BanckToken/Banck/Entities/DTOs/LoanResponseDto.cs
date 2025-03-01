using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class LoanResponseDto:ResponseDto
    {
        public List<Loan> Loans { get; set; }
        public Loan Loan { get; set; }
    }
}
