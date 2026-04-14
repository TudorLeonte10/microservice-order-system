using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task AddOrderAsync(Order order);
        Task SaveChangesAsync();
    }
}
