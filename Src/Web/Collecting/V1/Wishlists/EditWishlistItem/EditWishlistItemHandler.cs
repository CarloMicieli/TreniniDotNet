using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlistItem
{
    public sealed class EditWishlistItemHandler : UseCaseHandler<IEditWishlistItemUseCase, EditWishlistItemRequest, EditWishlistItemInput>
    {
        public EditWishlistItemHandler(IEditWishlistItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
