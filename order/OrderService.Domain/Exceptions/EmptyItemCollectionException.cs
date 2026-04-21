using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Exceptions
{
    public class EmptyItemCollectionException : Exception
    {
        public EmptyItemCollectionException(string message) : base(message)
        {
        }
    }
}
