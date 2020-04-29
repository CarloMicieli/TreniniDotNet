using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;

namespace TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesUseCase : ValidatedUseCase<RemoveShopFromFavouritesInput, IRemoveShopFromFavouritesOutputPort>, IRemoveShopFromFavouritesUseCase
    {
        public RemoveShopFromFavouritesUseCase(IRemoveShopFromFavouritesOutputPort output)
            : base(new RemoveShopFromFavouritesInputValidator(), output)
        {
        }

        protected override Task Handle(RemoveShopFromFavouritesInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
