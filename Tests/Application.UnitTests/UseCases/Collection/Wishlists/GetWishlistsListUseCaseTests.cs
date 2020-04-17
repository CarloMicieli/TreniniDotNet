using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsList;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class GetWishlistsListUseCaseTests : CollectionUseCaseTests<GetWishlistsList, GetWishlistsListOutput, GetWishlistsListOutputPort>
    {



        private GetWishlistsList NewGetWishlistsList(
            WishlistService collectionService,
            GetWishlistsListOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetWishlistsList(outputPort, collectionService);
        }
    }
}
