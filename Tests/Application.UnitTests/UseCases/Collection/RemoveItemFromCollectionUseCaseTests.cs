using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class RemoveItemFromCollectionUseCaseTests : UseCaseTestHelper<RemoveItemFromCollection, RemoveItemFromCollectionOutput, RemoveItemFromCollectionOutputPort>
    {
    }
}
