using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class DeleteWishlistUseCaseTests : CollectionUseCaseTests<DeleteWishlist, DeleteWishlistOutput, DeleteWishlistOutputPort>
    {



        private DeleteWishlist NewDeleteWishlist(
            WishlistService collectionService,
            DeleteWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new DeleteWishlist(outputPort, collectionService, unitOfWork);
        }
    }
}
