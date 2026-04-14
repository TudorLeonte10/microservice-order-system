using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        private OrderItem() { }

        internal OrderItem(Guid productId, string productName, int quantity, decimal price)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }

        internal void Increase(int quantity)
        {
            Quantity += quantity;
        }
    }
}
