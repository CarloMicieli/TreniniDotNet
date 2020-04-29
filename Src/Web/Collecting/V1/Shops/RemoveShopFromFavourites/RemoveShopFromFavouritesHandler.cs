using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesHandler : UseCaseHandler<IRemoveShopFromFavouritesUseCase, RemoveShopFromFavouritesRequest, RemoveShopFromFavouritesInput>
    {
        public RemoveShopFromFavouritesHandler(IRemoveShopFromFavouritesUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
