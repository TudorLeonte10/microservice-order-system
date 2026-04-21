using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Payments.Dtos
{
    public class CreatePaymentIntentResponse
    {
        public string PaymentIntentId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}
