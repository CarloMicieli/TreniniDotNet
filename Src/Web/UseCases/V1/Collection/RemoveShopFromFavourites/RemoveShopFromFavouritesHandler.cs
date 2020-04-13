using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.RemoveShopFromFavourites;

namespace TreniniDotNet.Web.UseCases.V1.Collection.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesHandler : UseCaseHandler<IRemoveShopFromFavouritesUseCase, RemoveShopFromFavouritesRequest, RemoveShopFromFavouritesInput>
    {
        public RemoveShopFromFavouritesHandler(IRemoveShopFromFavouritesUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
