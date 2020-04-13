using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetUserCollection;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class GetUserCollectionUseCaseTests : UseCaseTestHelper<GetUserCollection, GetUserCollectionOutput, GetUserCollectionOutputPort>
    {
    }
}
