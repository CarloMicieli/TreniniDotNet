using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.AddShopToFavourites;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public class AddShopToFavouritesUseCaseTests : UseCaseTestHelper<AddShopToFavourites, AddShopToFavouritesOutput, AddShopToFavouritesOutputPort>
    {
    }
}
