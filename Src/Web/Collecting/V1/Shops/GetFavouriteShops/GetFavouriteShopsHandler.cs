using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsHandler : UseCaseHandler<GetFavouriteShopsUseCase, GetFavouriteShopsRequest, GetFavouriteShopsInput>
    {
        public GetFavouriteShopsHandler(GetFavouriteShopsUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
