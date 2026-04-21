using PaymentService.Application.Payments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Payments.Services
{
    public interface IPaymentService
    {
        Task<PaymentDto> ExecutePayment(PayRequest request);
    }
}
