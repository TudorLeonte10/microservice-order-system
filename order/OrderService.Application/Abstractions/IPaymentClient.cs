using OrderService.Application.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Abstractions
{
    public interface IPaymentClient
    {
        Task<bool> ProcessPaymentAsync(OrderPaymentRequest request);
    }
}
