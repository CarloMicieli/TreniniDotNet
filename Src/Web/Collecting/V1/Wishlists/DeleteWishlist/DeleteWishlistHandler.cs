using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.DeleteWishlist
{
    public sealed class DeleteWishlistHandler : UseCaseHandler<IDeleteWishlistUseCase, DeleteWishlistRequest, DeleteWishlistInput>
    {
        public DeleteWishlistHandler(IDeleteWishlistUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
