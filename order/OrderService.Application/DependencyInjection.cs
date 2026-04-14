using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Orders.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, Orders.Services.OrderService>();
        }
    }
}
