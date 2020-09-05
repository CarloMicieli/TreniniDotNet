using System;
using NodaTime;
using NodaTime.TimeZones;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwaysFactory : AggregateRootFactory<RailwayId, Railway>
    {
        public RailwaysFactory(IClock clock, IGuidSource guidSource)
            : base(clock, guidSource)
        {
        }

        public Railway CreateRailway(
            string name,
            string? companyName,
            Country country,
            PeriodOfActivity periodOfActivity,
            RailwayLength? railwayLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters)
        {
            return new Railway(
                NewId(id => new RailwayId(id)),
                name,
                companyName,
                country,
                periodOfActivity,
                railwayLength,
                gauge,
                websiteUrl,
                headquarters,
                GetCurrentInstant(),
                null,
                1);
        }
    }
}
