using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlist;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlist
{
    public sealed class EditWishlistHandler : UseCaseHandler<EditWishlistUseCase, EditWishlistRequest, EditWishlistInput>
    {
        public EditWishlistHandler(EditWishlistUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
