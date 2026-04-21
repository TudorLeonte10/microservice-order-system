using InventoryService.Application.Products.Dtos;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;

namespace InventoryService.Application.Products.Services
{
    public class CreateProductService : ICreateProductService
    {
        private readonly IProductRepository _repo;

        public CreateProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProductResponse> CreateProduct(CreateProductRequest request)
        {
            var product = new Product(request.Name, request.Price, request.StockQuantity);

            await _repo.AddProductAsync(product);
            await _repo.SaveChangesAsync();

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ReservedQuantity = product.ReservedQuantity
            };
        }
    }
}
