using OrderService.Application.Orders.Dtos;
using System;

namespace OrderService.Application.Orders.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateOrder(CreateOrderRequest request);
        Task<OrderResponse?> GetOrderById(Guid id);
    }
}
