using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsList;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public sealed class GetWishlistsList : ValidatedUseCase<GetWishlistsListInput, IGetWishlistsListOutputPort>, IGetWishlistsListUseCase
    {
        private readonly WishlistService _wishlistService;

        public GetWishlistsList(IGetWishlistsListOutputPort output, WishlistService wishlistService)
            : base(new GetWishlistsListInputValidator(), output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
        }

        protected override Task Handle(GetWishlistsListInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
