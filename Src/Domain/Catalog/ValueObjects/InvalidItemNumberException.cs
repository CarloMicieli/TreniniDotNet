using System;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public sealed class InvalidItemNumberException : Exception
    {
        public InvalidItemNumberException() : base()
        {
        }

        public InvalidItemNumberException(string message) : base(message)
        {
        }

        public InvalidItemNumberException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
