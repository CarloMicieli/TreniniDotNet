using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.AddShopToFavourites;

namespace TreniniDotNet.Web.UseCases.V1.Collection.AddShopToFavourites
{
    public sealed class AddShopToFavouritesHandler : UseCaseHandler<IAddShopToFavouritesUseCase, AddShopToFavouritesRequest, AddShopToFavouritesInput>
    {
        public AddShopToFavouritesHandler(IAddShopToFavouritesUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
