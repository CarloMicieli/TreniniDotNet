using System;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    public sealed class Railway : IRailway
    {
        public RailwayId RailwayId { set; get; }

        public Slug Slug { set; get; }

        public string Name { set; get; } = null!;

        public string? CompanyName { get; set; }

        public string? Country { set; get; }

        public RailwayStatus? Status { set; get; }

        public DateTime? OperatingUntil { set; get; }

        public DateTime? OperatingSince { set; get; }
    }
}
