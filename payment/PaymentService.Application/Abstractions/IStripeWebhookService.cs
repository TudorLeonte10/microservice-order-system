using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Abstractions
{
    public interface IStripeWebhookService
    {
        Task HandleWebhookAsync(string payload, string signature);
    }
}
