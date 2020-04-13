using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.CreateWishlist;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateWishlist
{
    public sealed class CreateWishlistHandler : UseCaseHandler<ICreateWishlistUseCase, CreateWishlistRequest, CreateWishlistInput>
    {
        public CreateWishlistHandler(ICreateWishlistUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
