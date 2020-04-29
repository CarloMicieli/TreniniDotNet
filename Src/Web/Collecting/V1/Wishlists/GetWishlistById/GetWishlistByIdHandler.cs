using AutoMapper;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdHandler : UseCaseHandler<IGetWishlistByIdUseCase, GetWishlistByIdRequest, GetWishlistByIdInput>
    {
        public GetWishlistByIdHandler(IGetWishlistByIdUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
