using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public sealed class DeleteWishlist : ValidatedUseCase<DeleteWishlistInput, IDeleteWishlistOutputPort>, IDeleteWishlistUseCase
    {
        private readonly WishlistService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWishlist(IDeleteWishlistOutputPort output, WishlistService wishlistService, IUnitOfWork unitOfWork)
            : base(new DeleteWishlistInputValidator(), output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(DeleteWishlistInput input)
        {
            var id = new WishlistId(input.Id);

            var wishlistExists = await _wishlistService.ExistAsync(id);
            if (wishlistExists == false)
            {
                OutputPort.WishlistNotFound(id);
                return;
            }

            await _wishlistService.DeleteAsync(id);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new DeleteWishlistOutput(id));
        }
    }
}
