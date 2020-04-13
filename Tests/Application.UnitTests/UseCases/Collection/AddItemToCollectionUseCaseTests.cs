using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class AddItemToCollectionUseCaseTests : UseCaseTestHelper<AddItemToCollection, AddItemToCollectionOutput, AddItemToCollectionOutputPort>
    {
    }
}
