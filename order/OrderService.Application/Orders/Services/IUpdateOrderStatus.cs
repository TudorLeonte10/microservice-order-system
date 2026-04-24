using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Orders.Services
{
    public interface IUpdateOrderStatus
    {
        Task UpdateStatus(Guid id);
    }
}
