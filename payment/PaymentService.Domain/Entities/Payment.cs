using PaymentService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; }

        public Payment(Guid orderId, decimal amount)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            Amount = amount;
            Status = PaymentStatus.Pending;
        }

        public void MarkAsCompleted()
        {
            Status = PaymentStatus.Completed;
        }

        public void MarkAsFailed()
        {
            Status = PaymentStatus.Failed;
        }

        public void MarkAsRefunded()
        {
            Status = PaymentStatus.Refunded;
        }
    }
}
