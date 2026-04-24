using OrderService.Application.Abstractions;
using OrderService.Application.Exceptions;
using OrderService.Application.OrderItems.Dtos;
using OrderService.Application.Orders.Dtos;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Orders.Services
{
    public class ConfirmOrderService : IConfirmOrderService
    {
        private readonly IInventoryClient _inventoryClient;
        private readonly IOrderRepository _orderRepository;
        public ConfirmOrderService(IInventoryClient inventoryClient, IOrderRepository orderRepository)
        {
            _inventoryClient = inventoryClient;
            _orderRepository = orderRepository;
        }
        public async Task<OrderResponse> ConfirmOrder(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if(order == null)
            {
                throw new NotFoundException($"Order with id {orderId} not found.");
            }

            await _inventoryClient.ReserveAsync(order.Items);

            order.Confirm();
            await _orderRepository.SaveChangesAsync();

            return new OrderResponse
            {
                Id = order.Id,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemResponse
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    ProductName = i.ProductName,
                    Price = i.Price
                }).ToList()
            };
        }
    }
}
