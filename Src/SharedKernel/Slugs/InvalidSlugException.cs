using System;

namespace TreniniDotNet.SharedKernel.Slugs
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