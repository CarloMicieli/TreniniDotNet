using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlist
{
    public sealed class EditWishlistUseCase : AbstractUseCase<EditWishlistInput, EditWishlistOutput, IEditWishlistOutputPort>
    {
        private readonly WishlistsService _wishlistsService;
        private readonly IUnitOfWork _unitOfWork;

        public EditWishlistUseCase(
            IUseCaseInputValidator<EditWishlistInput> inputValidator,
            IEditWishlistOutputPort outputPort,
            WishlistsService wishlistsService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _wishlistsService = wishlistsService ?? throw new ArgumentNullException(nameof(wishlistsService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(EditWishlistInput input)
        {
            var wishlistId = new WishlistId(input.Id);

            var wishlist = await _wishlistsService.GetByIdAsync(wishlistId);
            if (wishlist is null)
            {
                OutputPort.WishlistNotFound(wishlistId);
                return;
            }

            var owner = new Owner(input.Owner);
            if (owner.CanEdit(wishlist) == false)
            {
                OutputPort.NotAuthorizedToEditThisWishlist(owner);
                return;
            }

            var budget = input.Budget?.ToBudgetOrDefault();
            var visibility = EnumHelpers.OptionalValueFor<Visibility>(input.Visibility);

            var modified = wishlist.With(
                input.ListName,
                visibility,
                budget);

            await _wishlistsService.UpdateWishlistAsync(modified);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditWishlistOutput());
        }
    }
}
