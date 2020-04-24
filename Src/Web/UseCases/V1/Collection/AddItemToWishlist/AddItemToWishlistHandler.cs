using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddItemToWishlist
{
    public sealed class AddItemToWishlistHandler : UseCaseHandler<IAddItemToWishlistUseCase, AddItemToWishlistRequest, AddItemToWishlistInput>
    {
        public AddItemToWishlistHandler(IAddItemToWishlistUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
