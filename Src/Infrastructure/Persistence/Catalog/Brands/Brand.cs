using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public class Brand
    {
        public Guid BrandId { set; get; }
        public string Slug { set; get; } = null!;
        public string Name { set; get; } = null!;
        public string? WebsiteUrl { set; get; }
        public string? EmailAddress { set; get; }
        public string? CompanyName { set; get; }
        public string? BrandKind { set; get; }
        public bool? Active { set; get; }
        public int Version { set; get; } = 1;
        public DateTime CreationTime { set; get; }
    }
}
