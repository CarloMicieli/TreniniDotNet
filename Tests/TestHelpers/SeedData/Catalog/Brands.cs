using System;
using System.Collections.Generic;
using System.Net.Mail;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Brands
    {
        private readonly static IBrandsFactory brandFactory = new BrandsFactory();

        private readonly IList<IBrand> _all;
        private readonly IBrand _brawa;
        private readonly IBrand _acme;
        private readonly IBrand _bemo;
        private readonly IBrand _roco;
        private readonly IBrand _maerklin;
        private readonly IBrand _fleischmann;

        internal Brands()
        {
            #region [ Init data ]
            _acme = brandFactory.NewBrand(
                new BrandId(new Guid("9ed9f089-2053-4a39-b669-a6d603080402")),
                "ACME",
                Slug.Of("acme"),
                "Associazione Costruzioni Modellistiche Esatte",
                new Uri("http://www.acmetreni.com"),
                new MailAddress("mail@acmetreni.com"),
                BrandKind.Industrial);

            _roco = brandFactory.NewBrand(
                new BrandId(new Guid("4b7a619b-65cc-41f5-a003-450537c85dea")),
                "Roco",
                Slug.Of("roco"),
                "Modelleisenbahn GmbH",
                new Uri("http://www.roco.cc"),
                new MailAddress("webshop@roco.cc"),
                BrandKind.Industrial);

            _bemo = brandFactory.NewBrand(
                new BrandId(new Guid("ff9f5055-8ae7-4d58-a68f-0cee3adb6656")),
                "BEMO",
                Slug.Of("bemo"),
                "BEMO Modelleisenbahnen GmbH u. Co KG",
                new Uri("https://www.bemo-modellbahn.de/"),
                new MailAddress("mail@bemo-modellbahn.de"),
                BrandKind.Industrial);

            _brawa = brandFactory.NewBrand(
               new BrandId(new Guid("c37f8ac1-991b-422c-b273-5c02efe2087e")),
               "Brawa",
               Slug.Of("Brawa"),
               "BRAWA Artur Braun Modellspielwarenfabrik GmbH & Co. KG",
               new Uri("https://www.brawa.de/"),
               null,
               BrandKind.Industrial);

            _maerklin = brandFactory.NewBrand(
                new BrandId(new Guid("66fa5a39-7e47-471f-9f92-d2bb01258c31")),
                "Märklin",
                Slug.Of("Märklin"),
                "Gebr. Märklin & Cie. GmbH",
                new Uri("https://www.maerklin.de"),
                null,
                BrandKind.Industrial);

            _fleischmann = brandFactory.NewBrand(
                new BrandId(new Guid("2a916d99-953f-44d6-8115-7e72ca22b081")),
                "Fleischmann",
                Slug.Of("Fleischmann"),
                "Modelleisenbahn München GmbH",
                new Uri("https://www.fleischmann.de"),
                null,
                BrandKind.Industrial);
            #endregion

            _all = new List<IBrand>()
            {
                _acme,
                _roco,
                _bemo,
                _brawa,
                _fleischmann,
                _maerklin
            };
        }

        public IBrand Acme() => _acme;

        public IBrand Roco() => _roco;

        public IBrand Bemo() => _bemo;

        public IBrand Brawa() => _brawa;

        public IBrand Maerklin() => _maerklin;

        public IBrand Fleischmann() => _fleischmann;

        public ICollection<IBrand> All()
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
                repo.Add(brand);
            }
        }
    }
}
