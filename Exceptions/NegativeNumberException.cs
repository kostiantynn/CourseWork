using System;

namespace Exceptions
{
    public class NegativeNumberException : ArgumentException
    {
        public NegativeNumberException(string message) : base(message)
        {
        }
    }
}