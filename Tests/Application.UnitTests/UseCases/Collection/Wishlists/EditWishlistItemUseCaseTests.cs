using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class EditWishlistItemUseCaseTests : CollectionUseCaseTests<EditWishlistItem, EditWishlistItemOutput, EditWishlistItemOutputPort>
    {




        private EditWishlistItem NewEditWishlistItem(
            WishlistService collectionService,
            EditWishlistItemOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new EditWishlistItem(outputPort, collectionService, unitOfWork);
        }
    }
}
