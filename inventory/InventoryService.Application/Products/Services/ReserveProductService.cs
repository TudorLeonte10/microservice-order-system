using InventoryService.Application.Exceptions;
using InventoryService.Domain.Repositories;
using System;

namespace InventoryService.Application.Products.Services
{
    public class ReserveProductService : IReserveProductService
    {
        private readonly IProductRepository _repo;

        public ReserveProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task ReserveProduct(Guid productId, int quantity)
        {
            var product = await _repo.GetProductByIdAsync(productId);

            if (product == null)
            {
                throw new NotFoundException($"Product with ID {productId} not found.");
            }

            product.ReserveStock(quantity);
            await _repo.SaveChangesAsync();
        }
    }
}
