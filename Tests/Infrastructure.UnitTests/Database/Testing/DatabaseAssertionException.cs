using System;

namespace TreniniDotNet.Infrastructure.Database.Testing
{
    public class DatabaseAssertionException : Exception
    {
        public DatabaseAssertionException(string because)
            : base(because)
        {
        }
    }
}
