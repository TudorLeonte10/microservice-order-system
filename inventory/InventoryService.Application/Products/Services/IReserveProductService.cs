using System;

namespace InventoryService.Application.Products.Services
{
    public interface IReserveProductService
    {
        Task ReserveProduct(Guid productId, int quantity);
    }
}
