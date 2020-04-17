using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class RemoveItemFromWishlistUseCaseTests : UseCaseTestHelper<RemoveItemFromWishlist, RemoveItemFromWishlistOutput, RemoveItemFromWishlistOutputPort>
    {
    }
}
