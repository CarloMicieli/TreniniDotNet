using System;

namespace TreniniDotNet.Common
{
    public sealed class InvalidSlugException : Exception
    {
        public InvalidSlugException(string message)
            : base(message)
        {
        }

        public InvalidSlugException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
