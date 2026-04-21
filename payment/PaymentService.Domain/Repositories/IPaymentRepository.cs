using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task AddPayment(Payment payment);
        Task SaveChangesAsync();
        Task<Payment?> GetPaymentByIdAsync(Guid paymentId);
    }
}
