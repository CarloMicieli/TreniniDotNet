using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class EditWishlistItemUseCaseTests : UseCaseTestHelper<EditWishlistItem, EditWishlistItemOutput, EditWishlistItemOutputPort>
    {
    }
}
