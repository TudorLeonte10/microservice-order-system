using PaymentService.Domain.Enums;
using PaymentService.Domain.Exceptions;
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
        public string PaymentIntentId { get; private set; }

        public Payment(Guid orderId, decimal amount, string paymentIntentId)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            Amount = amount;
            Status = PaymentStatus.Pending;
            PaymentIntentId = paymentIntentId;
        }

        public static Payment AddPayment(Guid orderId, decimal amount, string paymentIntentId)
        {
            return new Payment(orderId, amount, paymentIntentId);
        }

        public void Pay()
        {
            if(Status != PaymentStatus.Pending)
                throw new InvalidPaymentStatusException("Payment can only be processed if it is in pending status.");

            Status = PaymentStatus.Completed;
        }

        public void Fail()
        {
            if (Status != PaymentStatus.Pending)
                throw new InvalidPaymentStatusException("Payment can only be failed if it is in pending status.");
            Status = PaymentStatus.Failed;
        }
    }
}
