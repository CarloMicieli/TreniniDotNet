using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlistItem
{
    public sealed class EditWishlistItemHandler : UseCaseHandler<EditWishlistItemUseCase, EditWishlistItemRequest, EditWishlistItemInput>
    {
        public EditWishlistItemHandler(EditWishlistItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
