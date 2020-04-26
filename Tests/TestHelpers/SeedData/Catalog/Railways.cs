using System;
using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Railways
    {
        private static readonly IRailwaysFactory factory = new RailwaysFactory(SystemClock.Instance, new GuidSource());

        private readonly IRailway _fs;
        private readonly IRailway _sbb;
        private readonly IRailway _dieBahn;
        private readonly IRailway _sncb;
        private readonly IRailway _sncf;
        private readonly IRailway _rhb;
        private readonly IRailway _dr;

        private readonly IList<IRailway> _all;

        internal Railways()
        {
            #region [ Init data ]
            _fs = NewWith(
                id: new RailwayId(new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313")),
                name: "FS",
                companyName: "Ferrovie dello stato",
                country: "IT",
                since: new DateTime(1905, 7, 1),
                until: null,
                status: RailwayStatus.Active);

            _sbb = NewWith(
                id: new RailwayId(new Guid("1c44e65f-bb53-4f14-a368-23daa5bfee05")),
                name: "SBB",
                companyName: "Schweizerische Bundesbahnen",
                country: "CH",
                since: null,
                until: null,
                status: RailwayStatus.Active);

            _dieBahn = NewWith(
                id: new RailwayId(new Guid("f12a3c5b-21f0-4d96-baf0-7bbf67e85e93")),
                name: "DB",
                companyName: "Die Bahn AG",
                country: "DE",
                since: null,
                until: null,
                status: RailwayStatus.Active);

            _sncb = NewWith(
                id: new RailwayId(new Guid("452ecf0f-0999-443d-a333-7afc23542d38")),
                name: "SNCB",
                companyName: "Société Nationale des Chemins de fer belges",
                country: "BE",
                since: null,
                until: null,
                status: RailwayStatus.Active);

            _sncf = NewWith(
                id: new RailwayId(new Guid("5cbccdd5-826d-4dc8-a0ff-d80572d43ac5")),
                name: "SNCF",
                companyName: "Société Nationale des Chemins de fer Français",
                country: "FR",
                since: null,
                until: null,
                status: RailwayStatus.Active);

            _rhb = NewWith(
                id: new RailwayId(new Guid("fa307786-00a9-4257-9274-76f7a0c06fab")),
                name: "RhB",
                companyName: "Rhätische Bahn / Viafier retica",
                country: "CH",
                since: null,
                until: null,
                status: RailwayStatus.Active);

            _dr = NewWith(
                id: new RailwayId(new Guid("93a911d8-0422-41b0-80a4-9f4650f1e8b4")),
                name: "DR",
                companyName: "Deutsche Reichsbahn (East Germany)",
                country: "DE",
                since: new DateTime(1949, 1, 1),
                until: new DateTime(1993, 12, 31),
                status: RailwayStatus.Inactive);
            #endregion

            _all = new List<IRailway>()
            {
                _fs,
                _sbb,
                _dieBahn,
                _sncb,
                _sncf,
                _rhb,
                _dr
            };
        }

        public static IRailway NewWith(
            RailwayId id,
            string name,
            string companyName = null,
            string country = "IT",
            DateTime? since = null,
            DateTime? until = null,
            RailwayStatus status = RailwayStatus.Active,
            RailwayLength railwayLength = null,
            RailwayGauge gauge = null,
            Uri website = null,
            string headquarters = null)
        {
            return factory.NewRailway(
                id.ToGuid(),
                name,
                Slug.Of(name).Value,
                companyName,
                country,
                since,
                until,
                status == RailwayStatus.Active,
                gauge?.Millimeters.Value,
                gauge?.Inches.Value,
                gauge?.TrackGauge.ToString(),
                headquarters,
                railwayLength?.Miles.Value,
                railwayLength?.Kilometers.Value,
                website?.ToString(),
                Instant.FromUtc(2019, 11, 25, 9, 0).ToDateTimeUtc(),
                null,
                1);
        }

        public IList<IRailway> All() => _all;

        public IRailway Fs() => _fs;

        public IRailway Sbb() => _sbb;

        public IRailway DieBahn() => _dieBahn;

        private IRailway Sncb() => _sncb;

        public IRailway Sncf() => _sncf;

        public IRailway RhB() => _rhb;

        public IRailway DR() => _dr;
    }

    public static class IRailwaysRepositoryExtensions
    {
        public static void SeedDatabase(this IRailwaysRepository repo)
        {
            var railways = CatalogSeedData.Railways.All();
            foreach (var railway in railways)
            {
                repo.AddAsync(railway);
            }
        }
    }
}
