using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem;

namespace TreniniDotNet.Web.UseCases.V1.Collection.EditWishlistItem
{
    public sealed class EditWishlistItemHandler : UseCaseHandler<IEditWishlistItemUseCase, EditWishlistItemRequest, EditWishlistItemInput>
    {
        public EditWishlistItemHandler(IEditWishlistItemUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
