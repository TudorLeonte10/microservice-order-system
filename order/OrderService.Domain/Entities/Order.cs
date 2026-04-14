using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<OrderItem> _items = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> Items => _items;

        public Order()
        {
            Id = Guid.NewGuid();
            Status = OrderStatus.Placed;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddItem(Guid productId, string productName, int quantity, decimal price)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Increase(quantity);
            }
            else
            {
                var newItem = new OrderItem(productId, productName, quantity, price);
                _items.Add(newItem);
            }
        }

        public void MarkAsPaid()
        {
            if(Items == null || !Items.Any())
                throw new InvalidOperationException("Cannot mark an order as paid without any items.");

            if (Status != OrderStatus.Placed)
                throw new InvalidOperationException("Only placed orders can be marked as paid.");
            
            Status = OrderStatus.Paid;
        }

        public void MarkAsShipped()
        {
            if (Status != OrderStatus.Paid)
                throw new InvalidOperationException("Only paid orders can be marked as shipped.");
            Status = OrderStatus.Shipped;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Shipped)
                throw new InvalidOperationException("Cannot cancel an order that has already been shipped.");
            Status = OrderStatus.Cancelled;
        }
    }
}
