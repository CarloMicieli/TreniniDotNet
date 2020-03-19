using System;

namespace TreniniDotNet.Infrastructure.Persistence.Seed.CsvRecords
{
    public sealed class BrandRecord
    {
        public Guid BrandId { set; get; }
        public string Slug { set; get; } = null!;
        public string Name { set; get; } = null!;
        public string? WebsiteUrl { set; get; }
        public string? MailAddress { set; get; }
        public string? CompanyName { set; get; }
        public string BrandKind { set; get; } = null!;
        public bool? Active { set; get; }
    }
}
