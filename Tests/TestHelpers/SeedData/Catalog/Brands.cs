using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Addresses;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class Brands
    {
        public BrandsBuilder New() => new BrandsBuilder();

        public Brand Acme { get; }
        public Brand Bemo { get; }
        public Brand Brawa { get; }
        public Brand Fleischmann { get; }
        public Brand Maerklin { get; }
        public Brand Rivarossi { get; }
        public Brand Roco { get; }

        internal Brands()
        {
            #region [ Init dataset ]

            Acme = NewAcme();
            Bemo = NewBemo();
            Brawa = NewBrawa();
            Fleischmann = NewFleischmann();
            Maerklin = NewMaerklin();
            Rivarossi = NewRivarossi();
            Roco = NewRoco();

            #endregion
        }

        public Brand NewAcme() => New()
            .Id(new Guid("9ed9f089-2053-4a39-b669-a6d603080402"))
            .Name("ACME")
            .CompanyName("Associazione Costruzioni Modellistiche Esatte")
            .WebsiteUrl("http://www.acmetreni.com")
            .MailAddress("mail@acmetreni.com")
            .Industrial()
            .Build();

        public Brand NewRoco() => New()
            .Id(new Guid("4b7a619b-65cc-41f5-a003-450537c85dea"))
            .Name("Roco")
            .CompanyName("Modelleisenbahn GmbH")
            .WebsiteUrl("http://www.roco.cc")
            .MailAddress("webshop@roco.cc")
            .Industrial()
            .Build();

        public Brand NewRivarossi() => New()
            .Id(new Guid("1e16aee7-3102-4d03-b99f-1a59a8b07202"))
            .Name("Rivarossi")
            .CompanyName("Rivarossi")
            .WebsiteUrl("https://www.hornby.it/")
            .Industrial()
            .Build();

        public Brand NewBemo() => New()
            .Id(new Guid("ff9f5055-8ae7-4d58-a68f-0cee3adb6656"))
            .Name("BEMO")
            .CompanyName("BEMO Modelleisenbahnen GmbH u. Co KG")
            .WebsiteUrl("https://www.bemo-modellbahn.de/")
            .MailAddress("mail@bemo-modellbahn.de")
            .Industrial()
            .Build();

        public Brand NewBrawa() => New()
            .Id(new Guid("c37f8ac1-991b-422c-b273-5c02efe2087e"))
            .Name("Brawa")
            .CompanyName("BRAWA Artur Braun Modellspielwarenfabrik GmbH & Co. KG")
            .WebsiteUrl("https://www.brawa.de/")
            .Industrial()
            .Build();

        public Brand NewMaerklin() => New()
            .Id(new Guid("66fa5a39-7e47-471f-9f92-d2bb01258c31"))
            .Name("Märklin")
            .CompanyName("Gebr. Märklin & Cie. GmbH")
            .WebsiteUrl("https://www.maerklin.de")
            .Address(Address.With(
                line1: "Postfach 860",
                postalCode: "D-73008",
                city: "Göppingen",
                country: "DE"))
            .Industrial()
            .Build();

        public Brand NewFleischmann() => New()
            .Id(new Guid("2a916d99-953f-44d6-8115-7e72ca22b081"))
            .Name("Fleischmann")
            .CompanyName("Modelleisenbahn München GmbH")
            .WebsiteUrl("https://www.fleischmann.de")
            .Industrial()
            .Build();

        public IEnumerable<Brand> All()
        {
            yield return Acme;
            yield return Bemo;
            yield return Brawa;
            yield return Fleischmann;
            yield return Maerklin;
            yield return Rivarossi;
            yield return Roco;
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
