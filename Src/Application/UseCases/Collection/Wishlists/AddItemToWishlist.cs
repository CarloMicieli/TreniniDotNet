using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public sealed class AddItemToWishlist : ValidatedUseCase<AddItemToWishlistInput, IAddItemToWishlistOutputPort>, IAddItemToWishlistUseCase
    {
        private readonly WishlistService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public AddItemToWishlist(IAddItemToWishlistOutputPort output, WishlistService wishlistService, IUnitOfWork unitOfWork)
            : base(new AddItemToWishlistInputValidator(), output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override Task Handle(AddItemToWishlistInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
