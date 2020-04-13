using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetFavouriteShops;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class GetFavouriteShopsUseCaseTests : UseCaseTestHelper<GetFavouriteShops, GetFavouriteShopsOutput, GetFavouriteShopsOutputPort>
    {
    }
}
