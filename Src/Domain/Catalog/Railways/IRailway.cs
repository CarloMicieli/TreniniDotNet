using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailway
    {
        public GuidId RailwayId { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public string? CompanyName { get; }

        public string? Country { get; }

        public RailwayStatus? Status { get; }

        public DateTime? OperatingUntil { get; }

        public DateTime? OperatingSince { get; }
    }
}
