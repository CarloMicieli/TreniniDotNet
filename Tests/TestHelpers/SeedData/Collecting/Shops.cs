using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.PhoneNumbers;

namespace TreniniDotNet.TestHelpers.SeedData.Collecting
{
    public sealed class Shops
    {
        public ShopsBuilder New() => new ShopsBuilder();

        public Shop ModellbahnshopLippe { get; }
        public Shop TecnomodelTreni { get; }

        internal Shops()
        {
            #region [ Init dataset ]

            ModellbahnshopLippe = NewModellbahnshopLippe();
            TecnomodelTreni = NewTecnomodelTreni();

            #endregion
        }

        public Shop NewModellbahnshopLippe() => New()
            .Id(new Guid("2b62520d-b559-4c38-bf5e-3de49bef327a"))
            .Name("Modellbahnshop Lippe")
            .WebsiteUrl(new Uri("https://www.modellbahnshop-lippe.com"))
            .MailAddress(new MailAddress("kundenservice@mail.modellbahnshop-lippe.com"))
            .Address(Address.With(line1: "Marie-Curie-Stra�e 1", postalCode: "32760", city: "Detmold",
                country: "DE"))
            .PhoneNumber(PhoneNumber.Of("+49 5231 9807 123"))
            .Build();

        public Shop NewTecnomodelTreni() => New()
            .Id(new Guid("7675cba2-023a-42ef-b7fd-a16cda375c58"))
            .Name("Tecnomodel")
            .WebsiteUrl(new Uri("https://www.tecnomodel-treni.it"))
            .Address(Address.With(line1: "Via Pian di Rota, 25 int. 1", postalCode: "57121", city: "Livorno",
                country: "IT"))
            .PhoneNumber(PhoneNumber.Of("+39 0586 407558"))
            .Build();

        public IEnumerable<Shop> All()
        {
            yield return ModellbahnshopLippe;
            yield return TecnomodelTreni;
        }
    }

    public static class ShopsRepositoryExtensions
    {
        public static async Task SeedDatabase(this IShopsRepository repo)
        {
            var shops = CollectingSeedData.Shops.All();
            foreach (var shop in shops)
            {
                var _ = await repo.AddAsync(shop);
            }

            var shopFavourites = CollectingSeedData.ShopsFavourites.All();
            foreach (var fav in shopFavourites)
            {
                await repo.AddShopToFavouritesAsync(fav.Owner, fav.ShopId);
            }
        }
    }
}
