using PaymentService.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Infrastructure.Clients
{
    public class OrderServiceClient : IOrderServiceClient
    {
        private readonly HttpClient _httpClient;
        public OrderServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task ResolveOrderStatus(Guid orderId)
        {
            await _httpClient.PostAsync($"api/orders/{orderId}/mark-as-paid", null);
        }
    }
}
