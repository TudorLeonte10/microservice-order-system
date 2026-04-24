using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentService.Application.Abstractions;
using PaymentService.Domain.Repositories;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Infrastructure.Webhooks
{
    public class StripeWebhookService : IStripeWebhookService
    {
        private readonly IPaymentRepository _repo;
        private readonly IConfiguration _configuration;
        private readonly IOrderServiceClient _orderServiceClient;
        private readonly ILogger<StripeWebhookService> _logger;
        public StripeWebhookService(
            IPaymentRepository repo,
            IConfiguration configuration,
            IOrderServiceClient orderServiceClient,
            ILogger<StripeWebhookService> logger)
        {
            _repo = repo;
            _configuration = configuration;
            _orderServiceClient = orderServiceClient;
            _logger = logger;
        }
        public async Task HandleWebhookAsync(string payload, string signature)
        {
            var eventus = EventUtility.ConstructEvent(payload, signature, _configuration["Stripe:WebhookSecret"], throwOnApiVersionMismatch: false);

            _logger.LogInformation("Stripe webhook received: {EventType} (EventId={EventId})", eventus.Type, eventus.Id);

            var paymentIntent = eventus.Data.Object as PaymentIntent;

            if (paymentIntent == null)
            {
                _logger.LogDebug("Webhook event {EventType} is not a PaymentIntent, ignoring", eventus.Type);
                return;
            }

            var payment = await _repo.GetPaymentByPaymentIntentIdAsync(paymentIntent.Id);

            if (payment == null)
            {
                _logger.LogWarning("Payment not found for PaymentIntentId={PaymentIntentId}", paymentIntent.Id);
                return;
            }

            if (eventus.Type == EventTypes.PaymentIntentSucceeded)
            {
                payment.Pay();
                await _repo.SaveChangesAsync();
                _logger.LogInformation("Payment marked as completed: PaymentId={PaymentId}, OrderId={OrderId}", payment.Id, payment.OrderId);

                await _orderServiceClient.ResolveOrderStatus(payment.OrderId);
                _logger.LogInformation("Order service notified: OrderId={OrderId}", payment.OrderId);
            }
            else if (eventus.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                payment.Fail();
                await _repo.SaveChangesAsync();
                _logger.LogInformation("Payment marked as failed: PaymentId={PaymentId}, OrderId={OrderId}", payment.Id, payment.OrderId);
            }
        }
    }
}
