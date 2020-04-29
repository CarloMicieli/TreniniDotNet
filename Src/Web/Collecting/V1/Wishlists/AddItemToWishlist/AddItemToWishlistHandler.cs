using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.AddItemToWishlist
{
    public sealed class AddItemToWishlistHandler : UseCaseHandler<IAddItemToWishlistUseCase, AddItemToWishlistRequest, AddItemToWishlistInput>
    {
        public AddItemToWishlistHandler(IAddItemToWishlistUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
