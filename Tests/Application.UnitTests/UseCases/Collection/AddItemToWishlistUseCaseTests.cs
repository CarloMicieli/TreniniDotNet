using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class AddItemToWishlistUseCaseTests : UseCaseTestHelper<AddItemToWishlist, AddItemToWishlistOutput, AddItemToWishlistOutputPort>
    {
    }
}
