using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistHandler : UseCaseHandler<ICreateWishlistUseCase, CreateWishlistRequest, CreateWishlistInput>
    {
        public CreateWishlistHandler(ICreateWishlistUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
