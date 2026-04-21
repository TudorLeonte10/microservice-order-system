using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Orders.Dtos
{
    public class OrderPaymentRequest
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
