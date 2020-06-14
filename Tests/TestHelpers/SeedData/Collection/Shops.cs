using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using NodaTime.Testing;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.PhoneNumbers;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.TestHelpers.SeedData.Collection
{
    public sealed class Shops
    {
        private static IShopsFactory factory = new ShopsFactory(
            FakeClock.FromUtc(1988, 11, 25),
            new GuidSource());

        private readonly IList<IShop> _all;

        private readonly IShop _modellbahnshopLippe;
        private readonly IShop _tecnomodel;

        public Shops()
        {
            _modellbahnshopLippe = factory.NewShop(
                "Modellbahnshop Lippe",
                new Uri("https://www.modellbahnshop-lippe.com"),
                new MailAddress("kundenservice@mail.modellbahnshop-lippe.com"),
                Address.With(line1: "Marie-Curie-Stra�e 1", postalCode: "32760", city: "Detmold", country: "DE"),
                PhoneNumber.Of("+49 5231 9807 123"));

            _tecnomodel = factory.NewShop(
                "Tecnomodel",
                new Uri("https://www.tecnomodel-treni.it"),
                null,
                Address.With(line1: "Via Pian di Rota, 25 int. 1", postalCode: "57121", city: "Livorno", country: "IT"),
                PhoneNumber.Of("+39 0586 407558"));

            _all = new List<IShop>()
            {
                _modellbahnshopLippe,
                _tecnomodel
            };
        }

        public IShop ModellbahnshopLippe() => _modellbahnshopLippe;

        public IShop TecnomodelTreni() => _tecnomodel;

        public IList<IShop> All() => _all;
    }

    public static class IShopsRepositoryExtensions
    {
        public static async Task SeedDatabase(this IShopsRepository repo)
        {
            var shops = CollectionSeedData.Shops.All();
            foreach (var shop in shops)
            {
                var _ = await repo.AddAsync(shop);
            }
        }
    }
}