using System;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandNotFoundException : Exception
    {
        public BrandNotFoundException()
        {
        }

        public BrandNotFoundException(string message)
            : base(message) { }
    }
}
