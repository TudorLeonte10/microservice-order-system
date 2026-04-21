using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
