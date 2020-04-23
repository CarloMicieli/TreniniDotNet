using FluentAssertions;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Collection.Shared;
using Xunit;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class CatalogItemDetailsViewTests
    {
        [Fact]
        public void CatalogItemDetailsView_CreateFromCatalogItemDetails()
        {
            var view = new CatalogItemDetailsView(new TestCatalogItemDetails());

            view.ItemNumber.Should().Be("123456");
            view.Count.Should().Be(4);
            view.Description.Should().Be("Description goes here");
            view.Category.Should().Be(CollectionCategory.Locomotives.ToString());
            view.Brand.Value.Should().Be("ACME");
            view.Brand.Slug.Should().Be("acme");
            view.Scale.Value.Should().Be("H0 (1:87)");
            view.Scale.Slug.Should().Be("h0");
        }
    }

    internal class TestCatalogItemDetails : ICatalogItemDetails
    {
        public IBrandRef Brand => new TestBrandRef();

        public ItemNumber ItemNumber => new ItemNumber("123456");

        public CollectionCategory Category => CollectionCategory.Locomotives;

        public IScaleRef Scale => new TestScaleRef();

        public int RollingStocksCount => 4;

        public string Description => "Description goes here";
    }

    internal class TestBrandRef : IBrandRef
    {
        public string Name => "ACME";

        public Slug Slug => Slug.Of("ACME");
    }

    internal class TestScaleRef : IScaleRef
    {
        public string Name => "H0 (1:87)";

        public Slug Slug => Slug.Of("H0");
    }
}
