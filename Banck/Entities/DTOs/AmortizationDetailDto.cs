using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class AmortizationDetailDto
    {
        public int InstallmentNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal CapitalAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal RemainingBalance { get; set; }
    }
}
