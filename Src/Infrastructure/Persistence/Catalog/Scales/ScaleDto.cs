using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal sealed class ScaleDto
    {
        public Guid scale_id { set; get; }
        public string name { set; get; } = null!;
        public string slug { set; get; } = null!;
        public decimal ratio { set; get; } = default;
        public decimal gauge { set; get; } = default;
        public string track_type { set; get; } = null!;
        public string? notes { set; get; }
        public DateTime? created_at { set; get; }
        public int? version { set; get; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}
