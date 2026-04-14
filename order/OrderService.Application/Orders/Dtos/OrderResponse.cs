using OrderService.Application.OrderItems.Dtos;

namespace OrderService.Application.Orders.Dtos
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new();
    }
}
