using InventoryService.Application.Exceptions;
using InventoryService.Application.Products.Dtos;
using InventoryService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.Products.Services
{
    public class GetProductByIdService : IGetProductByIdService
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> GetProductByIdAsync(Guid productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {productId} not found.");
            }

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
