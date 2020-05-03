using System;
using System.Collections.Generic;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using static TreniniDotNet.TestHelpers.SeedData.Catalog.CatalogSeedData;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Brands
    {
        private readonly IList<IBrand> _all;
        private readonly IBrand _brawa;
        private readonly IBrand _acme;
        private readonly IBrand _bemo;
        private readonly IBrand _roco;
        private readonly IBrand _maerklin;
        private readonly IBrand _fleischmann;
        private readonly IBrand _rivarossi;

        internal Brands()
        {
            #region [ Init data ]
            _acme = NewBrandWith(
                new BrandId(new Guid("9ed9f089-2053-4a39-b669-a6d603080402")),
                "ACME",
                "Associazione Costruzioni Modellistiche Esatte",
                websiteUrl: new Uri("http://www.acmetreni.com"),
                mailAddress: new MailAddress("mail@acmetreni.com"),
                brandKind: BrandKind.Industrial);

            _roco = NewBrandWith(
                new BrandId(new Guid("4b7a619b-65cc-41f5-a003-450537c85dea")),
                "Roco",
                "Modelleisenbahn GmbH",
                websiteUrl: new Uri("http://www.roco.cc"),
                mailAddress: new MailAddress("webshop@roco.cc"),
                brandKind:BrandKind.Industrial);

            _bemo = NewBrandWith(
                new BrandId(new Guid("ff9f5055-8ae7-4d58-a68f-0cee3adb6656")),
                "BEMO",
                "BEMO Modelleisenbahnen GmbH u. Co KG",
                websiteUrl: new Uri("https://www.bemo-modellbahn.de/"),
                mailAddress: new MailAddress("mail@bemo-modellbahn.de"),
                brandKind:BrandKind.Industrial);

            _brawa = NewBrandWith(
               new BrandId(new Guid("c37f8ac1-991b-422c-b273-5c02efe2087e")),
               "Brawa",
               "BRAWA Artur Braun Modellspielwarenfabrik GmbH & Co. KG",
               websiteUrl: new Uri("https://www.brawa.de/"),
               brandKind: BrandKind.Industrial);

            _rivarossi = NewBrandWith(
               new BrandId(new Guid("1e16aee7-3102-4d03-b99f-1a59a8b07202")),
               "Rivarossi",
               "Rivarossi",
               websiteUrl: new Uri("https://www.hornby.it/"),
               brandKind: BrandKind.Industrial);

            _maerklin = NewBrandWith(
                new BrandId(new Guid("66fa5a39-7e47-471f-9f92-d2bb01258c31")),
                "Märklin",
                "Gebr. Märklin & Cie. GmbH",
                websiteUrl: new Uri("https://www.maerklin.de"),
                brandKind: BrandKind.Industrial);

            _fleischmann = NewBrandWith(
                new BrandId(new Guid("2a916d99-953f-44d6-8115-7e72ca22b081")),
                "Fleischmann",
                "Modelleisenbahn München GmbH",
                websiteUrl: new Uri("https://www.fleischmann.de"),
                brandKind: BrandKind.Industrial);
            #endregion

            _all = new List<IBrand>()
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

        public IBrand Acme() => _acme;

        public IBrand Roco() => _roco;

        public IBrand Rivarossi() => _rivarossi;

        public IBrand Bemo() => _bemo;

        public IBrand Brawa() => _brawa;

        public IBrand Maerklin() => _maerklin;

        public IBrand Fleischmann() => _fleischmann;

        public IList<IBrand> All()
        {
            return _all;
        }
    }

    public static class IBrandsRepositoryExtensions
    {
        public static void SeedDatabase(this IBrandsRepository repo)
        {
            var brands = CatalogSeedData.Brands.All();
            foreach (var brand in brands)
            {
                repo.AddAsync(brand);
            }
        }
    }
}
