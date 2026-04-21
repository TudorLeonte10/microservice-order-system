using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OrderService.Application.Abstractions
{
    public interface IInventoryClient
    {
        Task<ProductDetailsDto?> GetProductByIdAsync(Guid productId);
        Task<bool> ReserveAsync(IReadOnlyCollection<OrderItem> items);
    }
}
