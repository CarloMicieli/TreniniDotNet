using System;
using System.Threading.Tasks;
using NodaMoney;
using NodaTime;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem
{
    public sealed class EditWishlistItemUseCase : ValidatedUseCase<EditWishlistItemInput, IEditWishlistItemOutputPort>, IEditWishlistItemUseCase
    {
        private readonly WishlistService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public EditWishlistItemUseCase(IEditWishlistItemOutputPort output, WishlistService wishlistService, IUnitOfWork unitOfWork)
            : base(new EditWishlistItemInputValidator(), output)
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

            var exists = await _wishlistService.ExistAsync(owner, id);
            if (exists == false)
            {
                OutputPort.WishlistItemNotFound(id, itemId);
                return;
            }

            var item = await _wishlistService.GetItemByIdAsync(id, itemId);
            if (item is null)
            {
                OutputPort.WishlistItemNotFound(id, itemId);
                return;
            }

            Money? price = input.Price.HasValue ?
                Money.Euro(input.Price.Value) : (Money?)null; //TODO: fix currency management

            Priority? priority = EnumHelpers.OptionalValueFor<Priority>(input.Priority);

            LocalDate? addedDate = input.AddedDate.ToLocalDateOrDefault();

            await _wishlistService.EditItemAsync(id,
                item,
                addedDate,
                price,
                priority,
                input.Notes);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditWishlistItemOutput(id, itemId));
        }
    }
}
