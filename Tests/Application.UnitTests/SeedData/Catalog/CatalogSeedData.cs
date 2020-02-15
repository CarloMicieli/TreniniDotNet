using System;
using System.Collections.Generic;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.SeedData.Catalog
{
    public static class CatalogSeedData
    {
        #region [ Brands ]

        private readonly static IBrandsFactory brandFactory = new DomainBrandFactory();

        private readonly static IBrand _acme = Acme();
        private readonly static IBrand _bemo = Bemo();
        private readonly static IBrand _roco = Roco();
        private readonly static IBrand _maerklin = Maerklin();

        public static ICollection<IBrand> Brands
        {
            get
            {
                return _brands;
            }
        }

        private static readonly ICollection<IBrand> _brands = new List<IBrand>()
        {
            _acme,
            _roco,
            _bemo,
            _maerklin
        };
        
        private static IBrand Acme()
        {
            return brandFactory.NewBrand(
                BrandId.NewId(),
                "ACME",
                Slug.Of("acme"),
                "Associazione Costruzioni Modellistiche Esatte",
                new Uri("http://www.acmetreni.com"),
                new MailAddress("mail@acmetreni.com"),
                BrandKind.Industrial);
        }

        private static IBrand Roco()
        {
            return brandFactory.NewBrand(
                BrandId.NewId(),
                "Roco",
                Slug.Of("roco"),
                "Modelleisenbahn GmbH",
                new Uri("http://www.roco.cc"),
                new MailAddress("webshop@roco.cc"),
                BrandKind.Industrial);
        }

        private static IBrand Bemo()
        {
            return brandFactory.NewBrand(
                BrandId.NewId(),
                "BEMO",
                Slug.Of("bemo"),
                "BEMO Modelleisenbahnen GmbH u. Co KG",
                new Uri("https://www.bemo-modellbahn.de/"),
                new MailAddress("mail@bemo-modellbahn.de"),
                BrandKind.Industrial);
        }

        private static IBrand Maerklin()
        {
            return brandFactory.NewBrand(
                BrandId.NewId(),
                "Märklin",
                Slug.Of("Märklin"),
                "Gebr. Märklin & Cie. GmbH",
                new Uri("https://www.maerklin.de"),
                null,
                BrandKind.Industrial);
        }
        #endregion
    }
}
