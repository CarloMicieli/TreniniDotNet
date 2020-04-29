using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsHandler : UseCaseHandler<IGetFavouriteShopsUseCase, GetFavouriteShopsRequest, GetFavouriteShopsInput>
    {
        public GetFavouriteShopsHandler(IGetFavouriteShopsUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
