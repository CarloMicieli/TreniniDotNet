using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels
{
    public class WishlistViewModelTests
    {
        [Fact]
        public void WishlistViewModel_ShouldRenderWishlists()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();

            var view = new WishlistView(wishlist);

            view.Id.Should().Be(wishlist.Id);
            view.Owner.Should().Be("George");
            view.Visibility.Should().Be("Private");
            view.Slug.Should().Be("george-first-list");
            view.ListName.Should().Be("First list");

            view.Items.Should().HaveCount(1);
        }
    }
}