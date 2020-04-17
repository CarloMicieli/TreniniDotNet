using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner;

namespace TreniniDotNet.Application.UseCases.Collection.Collections
{
    public class GetCollectionByOwnerUseCaseTests : UseCaseTestHelper<GetCollectionByOwner, GetCollectionByOwnerOutput, GetCollectionByOwnerOutputPort>
    {
    }
}
