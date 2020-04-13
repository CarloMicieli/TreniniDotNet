using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class CreateCollectionUseCaseTests : UseCaseTestHelper<CreateCollection, CreateCollectionOutput, CreateCollectionOutputPort>
    {
    }
}
