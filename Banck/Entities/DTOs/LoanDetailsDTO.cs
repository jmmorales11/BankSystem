using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class LoanDetailsDTO
    {
        public string UserFullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal RequestedAmount { get; set; }
        public int PaymentTermMonths { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
