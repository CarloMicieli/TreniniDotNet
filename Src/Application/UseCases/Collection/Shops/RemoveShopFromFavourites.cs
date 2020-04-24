using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.RemoveShopFromFavourites;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public sealed class RemoveShopFromFavourites : ValidatedUseCase<RemoveShopFromFavouritesInput, IRemoveShopFromFavouritesOutputPort>, IRemoveShopFromFavouritesUseCase
    {
        public RemoveShopFromFavourites(IRemoveShopFromFavouritesOutputPort output)
            : base(new RemoveShopFromFavouritesInputValidator(), output)
        {
        }

        protected override Task Handle(RemoveShopFromFavouritesInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
