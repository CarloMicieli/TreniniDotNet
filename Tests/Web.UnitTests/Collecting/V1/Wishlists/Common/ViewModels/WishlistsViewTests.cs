using System.Collections.Generic;
using FluentAssertions;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels
{
    public class WishlistsViewTests
    {
        [Fact]
        public void WishlistsView_ShouldRenderWishlists()
        {
            var list = new List<Wishlist>
            {
                CollectingSeedData.Wishlists.GeorgeFirstList()
            };

            var wishlistsView = new WishlistsView(new Owner("George"), VisibilityCriteria.All, list);

            wishlistsView.Owner.Should().Be("George");
            wishlistsView.Visibility.Should().Be("All");
            wishlistsView.Lists.Should().HaveCount(1);
        }
    }
}