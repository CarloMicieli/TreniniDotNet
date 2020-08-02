using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public sealed class AddItemToWishlistUseCase : AbstractUseCase<AddItemToWishlistInput, AddItemToWishlistOutput, IAddItemToWishlistOutputPort>
    {
        private readonly WishlistsService _wishlistService;
        private readonly WishlistItemsFactory _wishlistItemsFactory;
        private readonly IUnitOfWork _unitOfWork;

        public AddItemToWishlistUseCase(
            IUseCaseInputValidator<AddItemToWishlistInput> inputValidator,
            IAddItemToWishlistOutputPort output,
            WishlistsService wishlistService,
            WishlistItemsFactory wishlistItemsFactory,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
        {
            _wishlistService = wishlistService ?? throw new ArgumentNullException(nameof(wishlistService));
            _wishlistItemsFactory = wishlistItemsFactory ?? throw new ArgumentNullException(nameof(wishlistItemsFactory));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(AddItemToWishlistInput input)
        {
            var id = new WishlistId(input.Id);
            var owner = new Owner(input.Owner);

            var wishlist = await _wishlistService.GetByIdAsync(id);
            if (wishlist is null)
            {
                OutputPort.WishlistNotFound(id);
                return;
            }

            if (owner.CanEdit(wishlist) == false)
            {
                OutputPort.NotAuthorizedToEditThisWishlist(owner);
                return;
            }

            var catalogItemSlug = Slug.Of(input.CatalogItem);
            var catalogItem = await _wishlistService.GetCatalogItemAsync(catalogItemSlug);
            if (catalogItem is null)
            {
                OutputPort.CatalogItemNotFound(catalogItemSlug);
                return;
            }

            if (wishlist.Contains(catalogItem))
            {
                OutputPort.CatalogItemAlreadyPresent(id, catalogItem);
                return;
            }

            var price = input.Price?.ToPriceOrDefault();

            var priority = EnumHelpers.OptionalValueFor<Priority>(input.Priority) ?? Priority.Normal;

            var item = _wishlistItemsFactory.CreateWishlistItem(
                new CatalogItemRef(catalogItem),
                priority,
                input.AddedDate.ToLocalDate(),
                price,
                input.Notes);
            wishlist.AddItem(item);

            await _wishlistService.UpdateWishlistAsync(wishlist);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new AddItemToWishlistOutput(id, item.Id));
        }
    }
}
