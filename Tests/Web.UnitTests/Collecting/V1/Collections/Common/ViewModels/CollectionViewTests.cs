using System;
using System.Linq;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels
{
    public class CollectionViewTests
    {
        [Fact]
        public void CollectionView_FromCollection()
        {
            var view = new CollectionView(TestCollection());

            view.Id.Should().Be(new Guid("68b1d5a6-f35b-4ad7-9400-8cccddf3df1d"));
            view.Owner.Should().Be("George");
            view.Items.Should().HaveCount(1);

            var item = view.Items.First();
            item.ItemId.Should().Be(new Guid("739a7bba-a6b9-4e5a-8ece-22b145d91015"));
            item.Condition.Should().Be("New");
            item.Price.Amount.Should().Be(250M);
            item.Price.Currency.Should().Be("EUR");
            item.Notes.Should().Be("Some random notes");
            item.CatalogItem.Slug.Should().Be("acme-123456");
            item.CatalogItem.CatalogItemId.Should().Be(new Guid("d9af16b6-20bf-45ea-b44c-5ffcf6524018"));
        }


        private Collection TestCollection()
        {
            return CollectingSeedData.Collections.New()
                .Id(new Guid("68b1d5a6-f35b-4ad7-9400-8cccddf3df1d"))
                .Owner(new Owner("George"))
                .CreatedDate(Instant.FromUtc(2019, 11, 25, 9, 0))
                .Item(ib => ib
                    .ItemId(new Guid("739a7bba-a6b9-4e5a-8ece-22b145d91015"))
                    .Condition(Condition.New)
                    .Price(Price.Euro(250M))
                    .CatalogItem(CatalogSeedData.CatalogItems.New()
                        .Id(new Guid("d9af16b6-20bf-45ea-b44c-5ffcf6524018"))
                        .ItemNumber(new ItemNumber("123456"))
                        .Brand(CatalogSeedData.Brands.Acme())
                        .Build())
                    .AddedDate(new LocalDate(2019, 11, 25))
                    .Notes("Some random notes")
                    .Build())
                .Build();
        }
    }
}
