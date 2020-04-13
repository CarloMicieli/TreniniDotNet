using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.RemoveShopFromFavourites;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class RemoveShopFromFavouritesUseCaseTests : UseCaseTestHelper<RemoveShopFromFavourites, RemoveShopFromFavouritesOutput, RemoveShopFromFavouritesOutputPort>
    {
    }
}
