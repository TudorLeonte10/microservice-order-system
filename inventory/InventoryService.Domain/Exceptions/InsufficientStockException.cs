using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Domain.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(string message) : base(message)
        {
        }
    }
}
