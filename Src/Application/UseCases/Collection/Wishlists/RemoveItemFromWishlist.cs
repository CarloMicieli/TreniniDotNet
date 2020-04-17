using FluentValidation;
using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public sealed class RemoveItemFromWishlist : ValidatedUseCase<RemoveItemFromWishlistInput, IRemoveItemFromWishlistOutputPort>, IRemoveItemFromWishlistUseCase
    {
        private readonly WishlistService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveItemFromWishlist(IRemoveItemFromWishlistOutputPort output, WishlistService wishlistService, IUnitOfWork unitOfWork)
            : base(new RemoveItemFromWishlistInputValidator(), output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override Task Handle(RemoveItemFromWishlistInput input)
        {
            throw new NotImplementedException();
        }
    }
}
