using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;

namespace TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites
{
    public sealed class AddShopToFavouritesUseCase : ValidatedUseCase<AddShopToFavouritesInput, IAddShopToFavouritesOutputPort>, IAddShopToFavouritesUseCase
    {
        public AddShopToFavouritesUseCase(IAddShopToFavouritesOutputPort output)
            : base(new AddShopToFavouritesInputValidator(), output)
        {
        }

        protected override Task Handle(AddShopToFavouritesInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
