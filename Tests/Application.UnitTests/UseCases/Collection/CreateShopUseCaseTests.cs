using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.CreateShop;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class CreateShopUseCaseTests : UseCaseTestHelper<CreateShop, CreateShopOutput, CreateShopOutputPort>
    {
    }
}
