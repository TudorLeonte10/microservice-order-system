using OrderService.Application.Abstractions;
using OrderService.Application.Orders.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace OrderService.Infrastructure.Clients
{
    public class PaymentClient : IPaymentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PaymentClient> _logger;
        public PaymentClient(HttpClient httpClient, ILogger<PaymentClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<bool> ProcessPaymentAsync(OrderPaymentRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Payment/pay", request);
            if (response.IsSuccessStatusCode)
            { 
                return true;
            }
            else
            {
                _logger.LogError("Payment processing failed: {ReasonPhrase}", response.ReasonPhrase);
                return false;
            }
        }
    }
}
