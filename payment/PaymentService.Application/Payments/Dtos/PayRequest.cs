using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Payments.Dtos
{
    public class PayRequest
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
