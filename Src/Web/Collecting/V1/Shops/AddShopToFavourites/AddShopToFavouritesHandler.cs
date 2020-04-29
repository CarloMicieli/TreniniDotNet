using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.AddShopToFavourites
{
    public sealed class AddShopToFavouritesHandler : UseCaseHandler<IAddShopToFavouritesUseCase, AddShopToFavouritesRequest, AddShopToFavouritesInput>
    {
        public AddShopToFavouritesHandler(IAddShopToFavouritesUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
