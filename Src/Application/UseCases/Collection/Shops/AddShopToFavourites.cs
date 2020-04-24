using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.AddShopToFavourites;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public sealed class AddShopToFavourites : ValidatedUseCase<AddShopToFavouritesInput, IAddShopToFavouritesOutputPort>, IAddShopToFavouritesUseCase
    {
        public AddShopToFavourites(IAddShopToFavouritesOutputPort output)
            : base(new AddShopToFavouritesInputValidator(), output)
        {
        }

        protected override Task Handle(AddShopToFavouritesInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
