using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    internal sealed class BrandDto
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE1006 // Naming Styles
        public Guid brand_id { set; get; }
        public string name { set; get; } = null!;
        public string slug { set; get; } = null!;
        public string? company_name { set; get; }
        public string? description { set; get; }
        public string? mail_address { set; get; }
        public string? website_url { set; get; }
        public string kind { set; get; } = null!;
        public DateTime? created_at { set; get; }
        public int? version { set; get; }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE1006 // Naming Styles
    }
}
