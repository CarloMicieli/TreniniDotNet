using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
{
    public sealed class DeleteWishlistUseCase : ValidatedUseCase<DeleteWishlistInput, IDeleteWishlistOutputPort>, IDeleteWishlistUseCase
    {
        private readonly WishlistService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWishlistUseCase(IDeleteWishlistOutputPort output, WishlistService wishlistService, IUnitOfWork unitOfWork)
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
            var owner = new Owner(input.Owner);

            var wishlistExists = await _wishlistService.ExistAsync(owner, id);
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
