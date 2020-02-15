using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScaleNotFoundException : Exception
    {
        public ScaleNotFoundException(Slug slug)
            : base($"Scale not found (slug: {slug}") {}

        public ScaleNotFoundException(ScaleId id)
            : base($"Scale not found (id: {id}") {}            
    }
}
