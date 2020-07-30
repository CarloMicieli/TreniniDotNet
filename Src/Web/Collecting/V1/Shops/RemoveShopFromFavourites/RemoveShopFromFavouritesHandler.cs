using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesHandler : UseCaseHandler<RemoveShopFromFavouritesUseCase, RemoveShopFromFavouritesRequest, RemoveShopFromFavouritesInput>
    {
        public RemoveShopFromFavouritesHandler(RemoveShopFromFavouritesUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
