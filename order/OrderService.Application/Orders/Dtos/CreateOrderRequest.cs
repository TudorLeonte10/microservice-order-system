using OrderService.Application.OrderItems.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Application.Orders.Dtos
{
    public class CreateOrderRequest
    {
        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item.")]
        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }
}
