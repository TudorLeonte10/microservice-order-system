using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Abstractions
{
    public interface IOrderServiceClient
    {
        Task ResolveOrderStatus(Guid orderId);
    }
}
