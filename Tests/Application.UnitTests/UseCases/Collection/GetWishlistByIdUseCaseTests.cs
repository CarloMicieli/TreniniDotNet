using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistById;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class GetWishlistByIdUseCaseTests : UseCaseTestHelper<GetWishlistById, GetWishlistByIdOutput, GetWishlistByIdOutputPort>
    {
    }
}
