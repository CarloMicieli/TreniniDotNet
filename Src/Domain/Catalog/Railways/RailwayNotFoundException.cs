using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwayNotFoundException : Exception
    {
        public RailwayNotFoundException(Slug slug)
            : base($"Railway not found (slug: {slug}") { }

        public RailwayNotFoundException(ScaleId id)
            : base($"Railway not found (id: {id}") { }
    }
}
