using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public sealed class RailwayInfoDto
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles

        public Guid railway_id { set; get; }
        public string name { set; get; } = null!;
        public string slug { set; get; } = null!;
        public string? country { set; get; }

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
    }
}