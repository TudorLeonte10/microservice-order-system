using OrderService.Application.OrderItems.Dtos;
using OrderService.Application.Orders.Dtos;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using System;
using System.Linq;

namespace OrderService.Application.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> CreateOrder(CreateOrderRequest request)
        {
            var order = new Order();

            foreach (var item in request.Items)
            {
                order.AddItem(item.ProductId, item.ProductName, item.Quantity, item.Price);
            }

            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task<OrderResponse?> GetOrderById(Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null) return null;

            return new OrderResponse
            {
                Id = order.Id,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemResponse
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
        }
    }
}
