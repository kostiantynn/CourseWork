using System;

namespace Exceptions
{
    public class UnderflowException : OverflowException
    {
        public UnderflowException(string message) : base(message)
        {
        }
    }
}