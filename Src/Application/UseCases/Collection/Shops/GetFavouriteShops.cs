using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetFavouriteShops;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public sealed class GetFavouriteShops : ValidatedUseCase<GetFavouriteShopsInput, IGetFavouriteShopsOutputPort>, IGetFavouriteShopsUseCase
    {
        public GetFavouriteShops(IGetFavouriteShopsOutputPort output)
            : base(new GetFavouriteShopsInputValidator(), output)
        {
        }

        protected override Task Handle(GetFavouriteShopsInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
