using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exceptions
{
    public class NullDecoratorException : Exception
    {
        public NullDecoratorException(string message) : base(message)
        {

        }
    }
}
