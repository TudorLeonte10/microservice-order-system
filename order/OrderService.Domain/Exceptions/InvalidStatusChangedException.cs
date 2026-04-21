using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Exceptions
{
    public class InvalidStatusChangedException : Exception
    {
        public InvalidStatusChangedException(string message) : base(message)
        {
        }
    }
}
