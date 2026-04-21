using InventoryService.Application.Products.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IReserveProductService, ReserveProductService>();
            services.AddScoped<ICreateProductService, CreateProductService>();
            services.AddScoped<IGetProductByIdService, GetProductByIdService>();
        }
    }
}
