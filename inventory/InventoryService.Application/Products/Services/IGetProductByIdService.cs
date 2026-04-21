using InventoryService.Application.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.Products.Services
{
    public interface IGetProductByIdService
    {
        Task<ProductResponse> GetProductByIdAsync(Guid productId);
    }
}
