using System;

namespace TreniniDotNet.Infrastructure.Persistence.Seed.CsvRecords
{
    public sealed class RailwayRecord
    {
        public Guid RailwayId { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public string? Status { get; set; }
        public string? OperatingSince { get; set; }//TODO
        public string? OperatingUntil { get; set; } //TODO
        public int TrackGauge { get; set; }
        public int? Length { get; set; }
        public string? Website { get; set; }
    }
}
