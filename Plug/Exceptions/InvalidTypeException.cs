using System;

namespace Plug.Exceptions
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string message) : base(message) { }
    }
}
