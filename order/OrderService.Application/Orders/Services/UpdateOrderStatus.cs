using OrderService.Application.Exceptions;
using OrderService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Orders.Services
{
    public class UpdateOrderStatus : IUpdateOrderStatus
    {
        private readonly IOrderRepository _repository;
        public UpdateOrderStatus(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task UpdateStatus(Guid id)
        {
            var order = await _repository.GetOrderByIdAsync(id);

            if (order == null)
            {
                throw new NotFoundException($"Order with id {id} not found.");
            }

            order.MarkAsPaid();
            await _repository.SaveChangesAsync();
        }
    }
}
