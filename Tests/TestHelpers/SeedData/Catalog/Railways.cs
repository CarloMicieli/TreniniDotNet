using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NodaTime.TimeZones;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Countries;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Railways
    {
        private readonly Railway _fs;
        private readonly Railway _sbb;
        private readonly Railway _dieBahn;
        private readonly Railway _sncb;
        private readonly Railway _sncf;
        private readonly Railway _rhb;
        private readonly Railway _dr;

        private readonly IList<Railway> _all;

        public RailwaysBuilder New() => new RailwaysBuilder();

        internal Railways()
        {
            #region [ Init data ]
            _fs = New()
                .Id(new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313"))
                .Name("FS")
                .CompanyName("Ferrovie dello stato")
                .Country(Country.Of("IT"))
                .PeriodOfActivity(PeriodOfActivity.ActiveRailway(new DateTime(1905, 7, 1)))
                .Build();

            _sbb = New()
                .Id(new Guid("1c44e65f-bb53-4f14-a368-23daa5bfee05"))
                .Name("SBB")
                .CompanyName("Schweizerische Bundesbahnen")
                .Country(Country.Of("CH"))
                .Build();

            _dieBahn = New()
                .Id(new Guid("f12a3c5b-21f0-4d96-baf0-7bbf67e85e93"))
                .Name("DB")
                .CompanyName("Die Bahn AG")
                .Country(Country.Of("DE"))
                .Build();

            _sncb = New()
                .Id(new Guid("452ecf0f-0999-443d-a333-7afc23542d38"))
                .Name("SNCB")
                .CompanyName("Société Nationale des Chemins de fer belges")
                .Country(Country.Of("BE"))
                .Build();

            _sncf = New()
                .Id(new Guid("5cbccdd5-826d-4dc8-a0ff-d80572d43ac5"))
                .Name("SNCF")
                .CompanyName("Société Nationale des Chemins de fer Français")
                .Country(Country.Of("FR"))
                .Build();

            _rhb = New()
                .Id(new Guid("fa307786-00a9-4257-9274-76f7a0c06fab"))
                .Name("RhB")
                .CompanyName("Rhätische Bahn / Viafier retica")
                .Country(Country.Of("CH"))
                .Build();

            _dr = New()
                .Id(new Guid("93a911d8-0422-41b0-80a4-9f4650f1e8b4"))
                .Name("DR")
                .CompanyName("Deutsche Reichsbahn (East Germany)")
                .Country(Country.Of("DE"))
                .PeriodOfActivity(PeriodOfActivity.InactiveRailway(
                    new DateTime(1949, 1, 1),
                    new DateTime(1993, 12, 31)))
                .Build();
            #endregion

            _all = new List<Railway>()
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

        public IList<Railway> All() => _all;

        public Railway Fs() => _fs;

        public Railway Sbb() => _sbb;

        public Railway DieBahn() => _dieBahn;

        public Railway Sncb() => _sncb;

        public Railway Sncf() => _sncf;

        public Railway RhB() => _rhb;

        public Railway DR() => _dr;
    }

    public static class RailwaysRepositoryExtensions
    {
        public static async Task SeedDatabase(this IRailwaysRepository repo)
        {
            var railways = CatalogSeedData.Railways.All();
            foreach (var railway in railways)
            {
                await repo.AddAsync(railway);
            }
        }
    }
}
