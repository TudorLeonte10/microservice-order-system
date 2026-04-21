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
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<PaymentDto> ExecutePayment(PayRequest request)
        {
            var payment = Payment.AddPayment(request.OrderId, request.Amount);
            await _paymentRepository.AddPayment(payment);

            if (Random.Shared.Next(1, 11) <= 8)
            {
                payment.Pay();
            }
            else
            {
                payment.Fail();
            }

            await _paymentRepository.SaveChangesAsync();

            return new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Status = payment.Status
            };
        }
    }
}
