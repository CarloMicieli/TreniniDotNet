using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistHandler : UseCaseHandler<IRemoveItemFromWishlistUseCase, RemoveItemFromWishlistRequest, RemoveItemFromWishlistInput>
    {
        public RemoveItemFromWishlistHandler(IRemoveItemFromWishlistUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
