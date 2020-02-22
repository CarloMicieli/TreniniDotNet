using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    public sealed class Railway 
    {
        public Guid RailwayId { set; get; }
        public string Slug { set; get; } = null!;
        public string Name { set; get; } = null!;
        public string? CompanyName { get; set; }
        public string? Country { set; get; }
        public string? Status { set; get; }
        public DateTime? OperatingUntil { set; get; }
        public DateTime? OperatingSince { set; get; }
        public int Version { set; get; } = 1;
        public DateTime CreationTime { set; get; }
    }
}
