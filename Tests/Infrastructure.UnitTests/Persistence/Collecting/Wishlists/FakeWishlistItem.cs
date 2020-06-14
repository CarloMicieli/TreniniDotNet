using System;
using NodaMoney;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Wishlists
{
    public class FakeWishlistItem : IWishlistItem
    {
        public WishlistItemId Id { set; get; }

        public Priority Priority { set; get; }

        public LocalDate AddedDate { set; get; }

        public Money? Price { set; get; }

        public ICatalogRef CatalogItem { set; get; }

        public ICatalogItemDetails Details { set; get; }

        public string Notes { set; get; }

        public FakeWishlistItem()
        {
            Id = new WishlistItemId(new Guid("833e3a24-346b-409b-8db9-c7ba0bbd7a48"));
            Priority = Priority.High;
            AddedDate = new LocalDate(2019, 11, 25);
            Price = Money.Euro(250M);
            CatalogItem = CatalogRef.Of(Guid.NewGuid(), "acme-123456");
            Details = null;
            Notes = "My notes";
        }

        internal IWishlistItem With(Priority? Priority = null, Money? Price = null, string Notes = null)
        {
            return new FakeWishlistItem
            {
                Id = this.Id,
                Priority = Priority.HasValue ? Priority.Value : this.Priority,
                AddedDate = this.AddedDate,
                Price = Price.HasValue ? Price : this.Price,
                CatalogItem = this.CatalogItem,
                Details = null,
                Notes = (Notes is null) ? this.Notes : Notes
            };
        }
    }
}
