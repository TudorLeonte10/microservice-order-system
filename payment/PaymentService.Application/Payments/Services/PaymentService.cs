using Microsoft.Extensions.Logging;
using PaymentService.Application.Abstractions;
using PaymentService.Application.Payments.Dtos;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Payments.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IStripeClient _stripeClient;
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(
            IPaymentRepository paymentRepository,
            IStripeClient stripeClient,
            ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository;
            _stripeClient = stripeClient;
            _logger = logger;
        }
        public async Task<PaymentDto> ExecutePayment(PayRequest request)
        {
            _logger.LogInformation("Executing payment for OrderId={OrderId}, Amount={Amount}", request.OrderId, request.Amount);

            var response = await _stripeClient.CreatePaymentIntentAsync(request.Amount);
            _logger.LogInformation("PaymentIntent created: {PaymentIntentId}", response.PaymentIntentId);

            var payment = Payment.AddPayment(request.OrderId, request.Amount, response.PaymentIntentId);

            await _paymentRepository.AddPayment(payment);
            await _paymentRepository.SaveChangesAsync();
            _logger.LogInformation("Payment persisted: PaymentId={PaymentId}, PaymentIntentId={PaymentIntentId}", payment.Id, response.PaymentIntentId);

            await _stripeClient.ConfirmPaymentIntentAsync(response.PaymentIntentId);
            _logger.LogInformation("PaymentIntent confirmed: {PaymentIntentId}", response.PaymentIntentId);

            return new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Status = payment.Status,
                ClientSecret = response.ClientSecret
            };
        }
    }
}
