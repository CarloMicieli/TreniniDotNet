using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal sealed class RailwayDto
    {
        public Guid railway_id { set; get; }
        public string name { set; get; } = null!;
        public string? company_name { set; get; }
        public string slug { set; get; } = null!;
        public string? country { set; get; }
        public DateTime? operating_since { set; get; }
        public DateTime? operating_until { set; get; }
        public bool? active { set; get; }
        public decimal? gauge_mm { set; get; }
        public decimal? gauge_in { set; get; }
        public string? track_gauge { set; get; }
        public string? headquarters { set; get; }
        public decimal? total_length_mi { set; get; }
        public decimal? total_length_km { set; get; }
        public string? website_url { set; get; }
        public DateTime? last_modified { set; get; }
        public int? version { set; get; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}
