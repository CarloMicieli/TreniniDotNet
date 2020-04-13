using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistById;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistById
{
    public sealed class GetWishlistByIdHandler : UseCaseHandler<IGetWishlistByIdUseCase, GetWishlistByIdRequest, GetWishlistByIdInput>
    {
        public GetWishlistByIdHandler(IGetWishlistByIdUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
