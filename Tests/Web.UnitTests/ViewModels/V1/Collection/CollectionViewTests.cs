using FluentAssertions;
using NodaMoney;
using NodaTime;
using System;
using System.Collections.Immutable;
using System.Linq;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.ValueObjects;
using Xunit;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public class CollectionViewTests
    {
        [Fact]
        public void CollectionView_FromCollection()
        {
            var view = new CollectionView(new TestCollection());

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
    }

    internal class TestCollection : ICollection
    {
        public CollectionId CollectionId => new CollectionId(new Guid("68b1d5a6-f35b-4ad7-9400-8cccddf3df1d"));

        public Owner Owner => new Owner("George");

        public IImmutableList<ICollectionItem> Items => ImmutableList.Create<ICollectionItem>(new TestCollectionItem());

        public Instant CreatedDate => Instant.FromUtc(2019, 11, 25, 9, 0);

        public Instant? ModifiedDate => null;

        public int Version => 1;
    }

    internal class TestCollectionItem : ICollectionItem
    {
        public CollectionItemId ItemId => new CollectionItemId(new Guid("739a7bba-a6b9-4e5a-8ece-22b145d91015"));

        public ICatalogRef CatalogItem => new TestCatalogRef();

        public ICatalogItemDetails Details => new TestCatalogItemDetails();

        public Condition Condition => Condition.New;

        public Money Price => Money.Euro(250);

        public IShopInfo PurchasedAt => null;

        public LocalDate AddedDate => new LocalDate(2019, 11, 25);

        public string Notes => "Some random notes";
    }

    internal class TestCatalogRef : ICatalogRef
    {
        public CatalogItemId CatalogItemId => new CatalogItemId(new Guid("d9af16b6-20bf-45ea-b44c-5ffcf6524018"));

        public Slug Slug => Slug.Of("acme-123456");
    }
}
