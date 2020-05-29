using System;

namespace DataSeeding.DataLoader.Records.Catalog.Brands
{
    public sealed class Brand
    {
        public Guid BrandId { set; get; }
        public string Slug { set; get; }
        public string Name { set; get; }
        public string BrandLogo { set; get; }
        public string WebsiteUrl { set; get; }
        public string MailAddress { set; get; }
        public string CompanyName { set; get; }
        public string GroupName { set; get; }
        public string Description { set; get; }
        public string BrandKind { set; get; }
        public bool? Active { set; get; }
        public Address Address { set; get; }
        public int Version { set; get; }
        public DateTime? LastModified { set; get; }
    }
}