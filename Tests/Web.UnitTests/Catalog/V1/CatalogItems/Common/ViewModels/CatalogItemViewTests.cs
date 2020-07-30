using System.Linq;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;
using Xunit;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.ViewModels
{
    public class CatalogItemViewTests
    {
        [Fact]
        public void CatalogItemView_Should_RenderPassengerCarViews()
        {
            var passengerCar = CatalogSeedData.CatalogItems.Rivarossi_HR4298();

            var view = new CatalogItemView(passengerCar, new LinksView());

            view.RollingStocks.Should().HaveCount(1);

            var passengerCarView = view.RollingStocks.First();

            passengerCarView.Category.Should().Be(Category.PassengerCar.ToString());
            passengerCarView.TypeName.Should().Be("Corbellini");
            passengerCarView.Epoch.Should().Be("IV");
            passengerCarView.PassengerCarType.Should().Be("OpenCoach");
            passengerCarView.ServiceLevel.Should().Be("2cl");
            passengerCarView.LengthOverBuffer?.Millimeters.Should().Be(195M);
            passengerCarView.MinRadius?.Millimeters.Should().Be(360);
            passengerCarView.Couplers.Should().Be("Nem352");
            passengerCarView.Livery.Should().Be("grigio ardesia");
        }

        [Fact]
        public void CatalogItemView_Should_RenderLocomotiveViews()
        {
            var locomotive = CatalogSeedData.CatalogItems.Acme_60392();

            var view = new CatalogItemView(locomotive, new LinksView());

            view.RollingStocks.Should().HaveCount(1);

            var locomotiveView = view.RollingStocks.First();

            locomotiveView.Category.Should().Be(Category.ElectricLocomotive.ToString());
            locomotiveView.ClassName.Should().Be("E 656");
            locomotiveView.RoadNumber.Should().Be("E 656 291");
            locomotiveView.Epoch.Should().Be("IV");
            locomotiveView.LengthOverBuffer?.Millimeters.Should().Be(210M);
            locomotiveView.MinRadius?.Millimeters.Should().Be(360);
            locomotiveView.Couplers.Should().Be("Nem352");
            locomotiveView.Livery.Should().Be("bianco/rosso/blu");
            locomotiveView.Control.Should().Be("DccReady");
            locomotiveView.DccInterface.Should().Be("Nem652");
        }

        [Fact]
        public void CatalogItemView_Should_RenderCatalogItemViews()
        {
            var catItem = CatalogSeedData.CatalogItems.Acme_60392();
            var view = new CatalogItemView(catItem, new LinksView());

            view.Should().NotBeNull();

            view.Id.Should().Be(catItem.Id.ToGuid());
            view.ItemNumber.Should().Be(catItem.ItemNumber.ToString());

            view.Brand.Should().NotBeNull();
            view.Brand.Name.Should().Be(catItem.Brand.Name);

            view.Scale.Should().NotBeNull();
            view.Scale.Name.Should().Be(catItem.Scale.Name);

            view.RollingStocks.Should().NotBeNull();
            view.RollingStocks.Should().HaveCount(1);
        }
    }
}
