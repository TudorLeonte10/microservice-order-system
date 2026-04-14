using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}
