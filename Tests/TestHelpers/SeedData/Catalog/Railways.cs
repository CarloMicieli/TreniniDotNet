using System;
using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using static TreniniDotNet.TestHelpers.SeedData.Catalog.CatalogSeedData;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Railways
    {
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
            _fs = NewRailwayWith(
                id: new RailwayId(new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313")),
                name: "FS",
                companyName: "Ferrovie dello stato",
                country: Country.Of("IT"),
                periodOfActivity: PeriodOfActivity.ActiveRailway(new DateTime(1905, 7, 1)));

            _sbb = NewRailwayWith(
                id: new RailwayId(new Guid("1c44e65f-bb53-4f14-a368-23daa5bfee05")),
                name: "SBB",
                companyName: "Schweizerische Bundesbahnen",
                country: Country.Of("CH"));

            _dieBahn = NewRailwayWith(
                id: new RailwayId(new Guid("f12a3c5b-21f0-4d96-baf0-7bbf67e85e93")),
                name: "DB",
                companyName: "Die Bahn AG",
                country: Country.Of("DE"));

            _sncb = NewRailwayWith(
                id: new RailwayId(new Guid("452ecf0f-0999-443d-a333-7afc23542d38")),
                name: "SNCB",
                companyName: "Société Nationale des Chemins de fer belges",
                country: Country.Of("BE"));

            _sncf = NewRailwayWith(
                id: new RailwayId(new Guid("5cbccdd5-826d-4dc8-a0ff-d80572d43ac5")),
                name: "SNCF",
                companyName: "Société Nationale des Chemins de fer Français",
                country: Country.Of("FR"));

            _rhb = NewRailwayWith(
                id: new RailwayId(new Guid("fa307786-00a9-4257-9274-76f7a0c06fab")),
                name: "RhB",
                companyName: "Rhätische Bahn / Viafier retica",
                country: Country.Of("CH"));

            _dr = NewRailwayWith(
                id: new RailwayId(new Guid("93a911d8-0422-41b0-80a4-9f4650f1e8b4")),
                name: "DR",
                companyName: "Deutsche Reichsbahn (East Germany)",
                country: Country.Of("DE"),
                periodOfActivity: PeriodOfActivity.InactiveRailway(
                    new DateTime(1949, 1, 1),
                    new DateTime(1993, 12, 31)));
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

        public IList<IRailway> All() => _all;

        public IRailway Fs() => _fs;

        public IRailway Sbb() => _sbb;

        public IRailway DieBahn() => _dieBahn;

        private IRailway Sncb() => _sncb;

        public IRailway Sncf() => _sncf;

        public IRailway RhB() => _rhb;

        public IRailway DR() => _dr;
    }

    public static class RailwaysRepositoryExtensions
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
