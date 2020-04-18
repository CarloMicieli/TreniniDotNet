using FluentAssertions;
using NodaMoney;
using NodaTime;
using NodaTime.Testing;
using System;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using Xunit;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public class WishListTests
    {
        private WishlistsFactory Factory { get; }

        public WishListTests()
        {
            Factory = new WishlistsFactory(
                FakeClock.FromUtc(1988, 11, 25, 9, 0, 0),
                FakeGuidSource.NewSource(Guid.NewGuid()));
        }

        [Fact]
        public void WishlistItem_ShouldCheckEquality()
        {
            var id = Guid.NewGuid();

            var item1 = NewItemWith(id);
            var item2 = NewItemWith(id);

            item1.Equals(item2).Should().BeTrue();
        }

        private IWishlistItem NewItemWith(Guid Id) => Factory.NewWishlistItem(
                new WishlistItemId(Id),
                CatalogRef.Of(Guid.NewGuid(), "acme-123456"),
                null,
                Priority.High,
                new LocalDate(2020, 11, 25),
                Money.Euro(150),
                "My notes");
    }
}