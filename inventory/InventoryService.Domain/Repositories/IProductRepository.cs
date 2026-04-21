using InventoryService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Domain.Repositories
{
    public interface IProductRepository
    {
        Task SaveChangesAsync();
        Task<Product?> GetProductByIdAsync(Guid id);
        Task AddProductAsync(Product product);
    }
}
