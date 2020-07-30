using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels
{
    public class ShopViewTests
    {
        [Fact]
        public void ShopView_ShouldRenderShops()
        {
            var shop = CollectingSeedData.Shops.NewTecnomodelTreni();

            var view = new ShopView(shop);

            view.ShopId.Should().Be(shop.Id);
            view.Name.Should().Be(shop.Name);
            view.Slug.Should().Be(shop.Slug);
            view.EmailAddress.Should().Be(shop.EmailAddress?.ToString());
            view.PhoneNumber.Should().Be(shop.PhoneNumber?.ToString());
            view.WebsiteUrl.Should().Be(shop.WebsiteUrl?.ToString());

            view.Address?.City.Should().Be(shop.Address?.City);
            view.Address?.Country.Should().Be(shop.Address?.Country);
            view.Address?.Line1.Should().Be(shop.Address?.Line1);
            view.Address?.Line2.Should().Be(shop.Address?.Line2);
            view.Address?.PostalCode.Should().Be(shop.Address?.PostalCode);
        }
    }
}