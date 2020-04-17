using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class RemoveItemFromWishlistUseCaseTests : CollectionUseCaseTests<RemoveItemFromWishlist, RemoveItemFromWishlistOutput, RemoveItemFromWishlistOutputPort>
    {

        private RemoveItemFromWishlist NewRemoveItemFromWishlist(
            WishlistService collectionService,
            RemoveItemFromWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new RemoveItemFromWishlist(outputPort, collectionService, unitOfWork);
        }
    }
}
