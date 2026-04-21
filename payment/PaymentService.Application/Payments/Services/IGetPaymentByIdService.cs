using PaymentService.Application.Payments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Payments.Services
{
    public interface IGetPaymentByIdService
    {
        Task<PaymentDto> GetPaymentByIdAsync(Guid paymentId);
    }
}
