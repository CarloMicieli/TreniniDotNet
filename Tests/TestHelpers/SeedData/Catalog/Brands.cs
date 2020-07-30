using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Brands
    {
        private readonly List<Brand> _all;

        private readonly Brand _brawa;
        private readonly Brand _acme;
        private readonly Brand _bemo;
        private readonly Brand _roco;
        private readonly Brand _maerklin;
        private readonly Brand _fleischmann;
        private readonly Brand _rivarossi;

        public BrandsBuilder New() => new BrandsBuilder();

        internal Brands()
        {
            #region [ Init data ]

            _acme = New()
                .Id(new Guid("9ed9f089-2053-4a39-b669-a6d603080402"))
                .Name("ACME")
                .CompanyName("Associazione Costruzioni Modellistiche Esatte")
                .WebsiteUrl("http://www.acmetreni.com")
                .MailAddress("mail@acmetreni.com")
                .Industrial()
                .Build();

            _roco = New()
                .Id(new Guid("4b7a619b-65cc-41f5-a003-450537c85dea"))
                .Name("Roco")
                .CompanyName("Modelleisenbahn GmbH")
                .WebsiteUrl("http://www.roco.cc")
                .MailAddress("webshop@roco.cc")
                .Industrial()
                .Build();

            _bemo = New()
                .Id(new Guid("ff9f5055-8ae7-4d58-a68f-0cee3adb6656"))
                .Name("BEMO")
                .CompanyName("BEMO Modelleisenbahnen GmbH u. Co KG")
                .WebsiteUrl("https://www.bemo-modellbahn.de/")
                .MailAddress("mail@bemo-modellbahn.de")
                .Industrial()
                .Build();

            _brawa = New()
                .Id(new Guid("c37f8ac1-991b-422c-b273-5c02efe2087e"))
                .Name("Brawa")
                .CompanyName("BRAWA Artur Braun Modellspielwarenfabrik GmbH & Co. KG")
                .WebsiteUrl("https://www.brawa.de/")
                .Industrial()
                .Build();

            _rivarossi = New()
                .Id(new Guid("1e16aee7-3102-4d03-b99f-1a59a8b07202"))
                .Name("Rivarossi")
                .CompanyName("Rivarossi")
                .WebsiteUrl("https://www.hornby.it/")
                .Industrial()
                .Build();

            _maerklin = New()
                .Id(new Guid("66fa5a39-7e47-471f-9f92-d2bb01258c31"))
                .Name("Märklin")
                .CompanyName("Gebr. Märklin & Cie. GmbH")
                .WebsiteUrl("https://www.maerklin.de")
                .Industrial()
                .Build();

            _fleischmann = New()
                .Id(new Guid("2a916d99-953f-44d6-8115-7e72ca22b081"))
                .Name("Fleischmann")
                .CompanyName("Modelleisenbahn München GmbH")
                .WebsiteUrl("https://www.fleischmann.de")
                .Industrial()
                .Build();

            #endregion

            _all = new List<Brand>()
            {
                _acme,
                _roco,
                _bemo,
                _brawa,
                _fleischmann,
                _maerklin,
                _rivarossi
            };
        }

        public Brand Acme() => _acme;

        public Brand Roco() => _roco;

        public Brand Rivarossi() => _rivarossi;

        public Brand Bemo() => _bemo;

        public Brand Brawa() => _brawa;

        public Brand Maerklin() => _maerklin;

        public Brand Fleischmann() => _fleischmann;

        public List<Brand> All()
        {
            return _all;
        }
    }

    public static class BrandsRepositoryExtensions
    {
        public static async Task SeedDatabase(this IBrandsRepository repo)
        {
            var brands = CatalogSeedData.Brands.All();
            foreach (var brand in brands)
            {
                await repo.AddAsync(brand);
            }
        }
    }
}
