using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsList;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class GetWishlistsListUseCaseTests : UseCaseTestHelper<GetWishlistsList, GetWishlistsListOutput, GetWishlistsListOutputPort>
    {
    }
}
