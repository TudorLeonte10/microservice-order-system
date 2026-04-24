using PaymentService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Payments.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string ClientSecret { get; set; } = string.Empty;

    }
}
