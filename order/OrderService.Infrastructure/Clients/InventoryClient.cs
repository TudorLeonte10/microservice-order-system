using OrderService.Application.Abstractions;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace OrderService.Infrastructure.Clients
{
    public class InventoryClient : IInventoryClient
    {
        private readonly HttpClient _httpClient;

        public InventoryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDetailsDto?> GetProductByIdAsync(Guid productId)
        {
            var response = await _httpClient.GetAsync($"api/Product/{productId}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProductDetailsDto>();
        }

        public async Task<bool> ReserveAsync(IReadOnlyCollection<OrderItem> items)
        {
            foreach (var item in items)
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Product/{item.ProductId}/reserve", new { Quantity = item.Quantity });

                if(!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
