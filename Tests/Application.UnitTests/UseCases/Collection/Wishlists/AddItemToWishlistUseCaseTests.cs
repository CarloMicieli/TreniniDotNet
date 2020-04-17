using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class AddItemToWishlistUseCaseTests : CollectionUseCaseTests<AddItemToWishlist, AddItemToWishlistOutput, AddItemToWishlistOutputPort>
    {





        private AddItemToWishlist NewAddItemToWishlist(
            WishlistService collectionService,
            AddItemToWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new AddItemToWishlist(outputPort, collectionService, unitOfWork);
        }
    }
}
