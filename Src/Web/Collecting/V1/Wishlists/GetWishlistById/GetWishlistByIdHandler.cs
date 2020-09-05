using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdHandler : UseCaseHandler<GetWishlistByIdUseCase, GetWishlistByIdRequest, GetWishlistByIdInput>
    {
        public GetWishlistByIdHandler(GetWishlistByIdUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
