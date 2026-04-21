using PaymentService.Application.Exceptions;
using PaymentService.Application.Payments.Dtos;
using PaymentService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Payments.Services
{
    public class GetPaymentByIdService : IGetPaymentByIdService
    {
        private readonly IPaymentRepository _paymentRepository;
        public GetPaymentByIdService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<PaymentDto> GetPaymentByIdAsync(Guid paymentId)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);

            if (payment == null)
            {
                throw new NotFoundException("Payment not found");
            }

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
