using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.CreateWishlist;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class CreateWishlistUseCaseTests : UseCaseTestHelper<CreateWishlist, CreateWishlistOutput, CreateWishlistOutputPort>
    {
    }
}
