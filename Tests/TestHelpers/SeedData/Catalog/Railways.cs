using System;
using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Railways
    {
        private static readonly IRailwaysFactory factory = new RailwaysFactory();

        private readonly IRailway _fs;
        private readonly IRailway _sbb;
        private readonly IRailway _dieBahn;
        private readonly IRailway _sncb;
        private readonly IRailway _sncf;
        private readonly IRailway _rhb;

        private readonly IList<IRailway> _all;

        internal Railways()
        {
            #region [ Init data ]
            _fs = factory.NewRailway(
                new RailwayId(new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313")),
                "FS",
                Slug.Of("FS"),
                "Ferrovie dello stato",
                "IT",
                new DateTime(1905, 7, 1),
                null,
                RailwayStatus.Active);

            _sbb = factory.NewRailway(
                new RailwayId(new Guid("1c44e65f-bb53-4f14-a368-23daa5bfee05")),
                "SBB",
                Slug.Of("SBB"),
                "Schweizerische Bundesbahnen",
                "CH",
                null,
                null,
                RailwayStatus.Active);

            _dieBahn = factory.NewRailway(
                new RailwayId(new Guid("f12a3c5b-21f0-4d96-baf0-7bbf67e85e93")),
                "DB",
                Slug.Of("DB"),
                "Die Bahn AG",
                "DE",
                null,
                null,
                RailwayStatus.Active);

            _sncb = factory.NewRailway(
                new RailwayId(new Guid("452ecf0f-0999-443d-a333-7afc23542d38")),
                "SNCB",
                Slug.Of("SNCB"),
                "Société Nationale des Chemins de fer belges",
                "BE",
                null,
                null,
                RailwayStatus.Active);

            _sncf = factory.NewRailway(
                new RailwayId(new Guid("5cbccdd5-826d-4dc8-a0ff-d80572d43ac5")),
                "SNCF",
                Slug.Of("SNCF"),
                "Société Nationale des Chemins de fer Français",
                "FR",
                null,
                null,
                RailwayStatus.Active);

            _rhb = factory.NewRailway(
                new RailwayId(new Guid("fa307786-00a9-4257-9274-76f7a0c06fab")),
                "RhB",
                Slug.Of("RhB"),
                "Rhätische Bahn / Viafier retica",
                "CH",
                null,
                null,
                RailwayStatus.Active);
            #endregion

            _all = new List<IRailway>()
            {
                _fs,
                _sbb,
                _dieBahn,
                _sncb,
                _sncf,
                _rhb
            };
        }

        public ICollection<IRailway> All() => _all;

        public IRailway Fs() => _fs;

        public IRailway Sbb() => _sbb;

        public IRailway DieBahn() => _dieBahn;

        private IRailway Sncb() => _sncb;

        public IRailway Sncf() => _sncf;

        public IRailway RhB() => _rhb;
    }

    public static class IRailwaysRepositoryExtensions
    {
        public static void SeedDatabase(this IRailwaysRepository repo)
        {
            var railways = CatalogSeedData.Railways.All();
            foreach (var railway in railways)
            {
                repo.Add(railway);
            }
        }
    }
}
