using System;
using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Wishlists
{
    public class FakeWishlist : IWishlist
    {
        public WishlistId Id { set; get; }

        public Owner Owner { set; get; }

        public IImmutableList<IWishlistItem> Items { set; get; }

        public Slug Slug { set; get; }

        public string ListName { set; get; }

        public Visibility Visibility { set; get; }

        public Instant CreatedDate { set; get; }

        public Instant? ModifiedDate { set; get; }

        public int Version { set; get; }

        public FakeWishlist()
        {
            Id = new WishlistId(new Guid("51e05ca4-a7f9-4634-af2f-8e0fe3c0d55d"));
            Owner = new Owner("George");
            Items = ImmutableList<IWishlistItem>.Empty;
            Slug = Slug.Of("My first list");
            ListName = "My first list";
            Visibility = Visibility.Private;
            CreatedDate = Instant.FromUtc(2019, 11, 25, 9, 0);
            ModifiedDate = null;
            Version = 1;
        }

        public IWishlist With(ImmutableList<IWishlistItem> Items = null)
        {
            return new FakeWishlist
            {
                Id = this.Id,
                Owner = this.Owner,
                Items = (Items is null) ? this.Items : Items,
                Slug = this.Slug,
                ListName = this.ListName,
                Visibility = this.Visibility,
                CreatedDate = this.CreatedDate,
                ModifiedDate = this.ModifiedDate,
                Version = this.Version
            };
        }
    }
}
