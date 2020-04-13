using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.GetFavouriteShops;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetFavouriteShops
{
    public sealed class GetFavouriteShopsHandler : UseCaseHandler<IGetFavouriteShopsUseCase, GetFavouriteShopsRequest, GetFavouriteShopsInput>
    {
        public GetFavouriteShopsHandler(IGetFavouriteShopsUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
