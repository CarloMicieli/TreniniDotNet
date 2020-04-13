using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class EditCollectionItemUseCaseTests : UseCaseTestHelper<EditCollectionItem, EditCollectionItemOutput, EditCollectionItemOutputPort>
    {
    }
}
