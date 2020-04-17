using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public sealed class EditWishlistItem : ValidatedUseCase<EditWishlistItemInput, IEditWishlistItemOutputPort>, IEditWishlistItemUseCase
    {
        private readonly WishlistService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public EditWishlistItem(IEditWishlistItemOutputPort output, WishlistService wishlistService, IUnitOfWork unitOfWork)
            : base(new EditWishlistItemInputValidator(), output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override Task Handle(EditWishlistItemInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
