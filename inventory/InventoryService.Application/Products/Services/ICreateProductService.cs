using InventoryService.Application.Products.Dtos;

namespace InventoryService.Application.Products.Services
{
    public interface ICreateProductService
    {
        Task<ProductResponse> CreateProduct(CreateProductRequest request);
    }
}
