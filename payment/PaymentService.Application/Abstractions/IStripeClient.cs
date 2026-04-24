using PaymentService.Application.Payments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Abstractions
{
    public interface IStripeClient
    {
        Task<CreatePaymentIntentResponse> CreatePaymentIntentAsync(decimal amount, string currency = "ron");
        Task ConfirmPaymentIntentAsync(string paymentIntentId);
    }
}
