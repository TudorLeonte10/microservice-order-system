using OrderService.Application.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Orders.Services
{
    public interface IOrderPaymentService
    {
        Task<OrderResponse> PayForOrder(Guid orderId);
    }
}
