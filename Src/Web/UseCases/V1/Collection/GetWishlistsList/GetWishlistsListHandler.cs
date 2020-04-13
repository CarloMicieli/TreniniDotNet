using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsList;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistsList
{
    public sealed class GetWishlistsListHandler : UseCaseHandler<IGetWishlistsListUseCase, GetWishlistsListRequest, GetWishlistsListInput>
    {
        public GetWishlistsListHandler(IGetWishlistsListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
