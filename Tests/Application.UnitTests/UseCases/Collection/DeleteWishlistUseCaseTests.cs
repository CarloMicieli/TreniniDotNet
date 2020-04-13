using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class DeleteWishlistUseCaseTests : UseCaseTestHelper<DeleteWishlist, DeleteWishlistOutput, DeleteWishlistOutputPort>
    {
    }
}
