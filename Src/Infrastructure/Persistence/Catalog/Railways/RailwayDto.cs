using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    internal sealed class RailwayDto
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE1006 // Naming Styles
        public Guid railway_id { set; get; }
        public string name { set; get; } = null!;
        public string? company_name { set; get; }
        public string slug { set; get; } = null!;
        public string? country { set; get; }
        public DateTime? operating_since { set; get; }
        public DateTime? operating_until { set; get; }
        public bool? active { set; get; }
        public DateTime? created_at { set; get; }
        public int? version { set; get; }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE1006 // Naming Styles
    }
}
