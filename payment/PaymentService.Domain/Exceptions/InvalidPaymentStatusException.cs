using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.Exceptions
{
    public class InvalidPaymentStatusException : Exception
    {
        public InvalidPaymentStatusException(string message) : base(message) { }
    }
}
