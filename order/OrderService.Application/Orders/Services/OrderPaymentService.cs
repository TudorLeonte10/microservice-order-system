using OrderService.Application.Abstractions;
using OrderService.Application.Exceptions;
using OrderService.Application.OrderItems.Dtos;
using OrderService.Application.Orders.Dtos;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Orders.Services
{
    public class OrderPaymentService : IOrderPaymentService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentClient _paymentClient;
        public OrderPaymentService(IOrderRepository orderRepository, IPaymentClient paymentClient)
        {
            _orderRepository = orderRepository;
            _paymentClient = paymentClient;
        }
        public async Task<OrderResponse> PayForOrder(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException($"Order with ID {orderId} not found.");
            }

            var amount = order.Items.Sum(s => s.Quantity * s.Price);

            var paymentRequest = new OrderPaymentRequest
            {
                OrderId = order.Id,
                Amount = amount
            };

            var success = await _paymentClient.ProcessPaymentAsync(paymentRequest);

            if (!success)
                throw new PaymentException("Payment processing failed.");

            order.MarkAsPaid();
            await _orderRepository.SaveChangesAsync();

            return new OrderResponse
            {
                Id = order.Id,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemResponse
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
        }
    }
}
