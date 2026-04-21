using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.OrderItems.Dtos
{
    public class CreateOrderItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
