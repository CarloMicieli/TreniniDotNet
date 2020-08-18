using System;
using System.Threading.Tasks;
using NodaTime;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem
{
    public sealed class EditWishlistItemUseCase : AbstractUseCase<EditWishlistItemInput, EditWishlistItemOutput, IEditWishlistItemOutputPort>
    {
        private readonly WishlistsService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public EditWishlistItemUseCase(
            IUseCaseInputValidator<EditWishlistItemInput> inputValidator,
            IEditWishlistItemOutputPort outputPort,
            WishlistsService wishlistService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(EditWishlistItemInput input)
        {
            var id = new WishlistId(input.Id);
            var itemId = new WishlistItemId(input.ItemId);
            var owner = new Owner(input.Owner);

            var wishlist = await _wishlistService.GetByIdAsync(id);
            if (wishlist is null)
            {
                OutputPort.WishlistItemNotFound(id, itemId);
                return;
            }

            if (owner.CanEdit(wishlist) == false)
            {
                OutputPort.NotAuthorizedToEditThisWishlist(owner);
                return;
            }

            var item = wishlist.FindItemById(itemId);
            if (item is null)
            {
                OutputPort.WishlistItemNotFound(id, itemId);
                return;
            }

            var price = input.Price?.ToPriceOrDefault();

            var priority = EnumHelpers.OptionalValueFor<Priority>(input.Priority);
            LocalDate? addedDate = input.AddedDate.ToLocalDateOrDefault();

            var modifiedItem = item.With(
                price: price,
                priority: priority,
                addedDate: addedDate,
                notes: input.Notes);

            wishlist.UpdateItem(modifiedItem);
            await _wishlistService.UpdateWishlistAsync(wishlist);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditWishlistItemOutput(id, itemId));
        }
    }
}
