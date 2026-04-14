using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public int ReservedQuantity { get; private set; }

        public Product(string name, decimal price, int stockQuantity) 
        { 
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            ReservedQuantity = 0;
        }

        public bool HasSufficientStock(int quantity)
        {
            return StockQuantity - ReservedQuantity >= quantity;
        }

        public void ReserveStock(int quantity)
        {
            if (!HasSufficientStock(quantity))
                throw new InvalidOperationException("Insufficient stock to reserve.");
            ReservedQuantity += quantity;
        }
    }
}
