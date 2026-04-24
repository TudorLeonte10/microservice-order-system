using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using PaymentService.Application.Abstractions;
using PaymentService.Application.Payments.Dtos;
using Stripe;

namespace PaymentService.Infrastructure.Clients
{
    public class StripeClient : Application.Abstractions.IStripeClient
    {
        public async Task<CreatePaymentIntentResponse> CreatePaymentIntentAsync(decimal amount, string currency = "ron")
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100),
                Currency = currency,
                PaymentMethod = "pm_card_visa",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                    AllowRedirects = "never"
                }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return new CreatePaymentIntentResponse
            {
                PaymentIntentId = paymentIntent.Id,
                ClientSecret = paymentIntent.ClientSecret
            };
        }

        public async Task ConfirmPaymentIntentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            await service.ConfirmAsync(paymentIntentId);
        }
    }
}
