using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.AddShopToFavourites;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class AddShopToFavouritesUseCaseTests : UseCaseTestHelper<AddShopToFavourites, AddShopToFavouritesOutput, AddShopToFavouritesOutputPort>
    {
    }
}
