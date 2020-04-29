using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;

namespace TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsUseCase : ValidatedUseCase<GetFavouriteShopsInput, IGetFavouriteShopsOutputPort>, IGetFavouriteShopsUseCase
    {
        public GetFavouriteShopsUseCase(IGetFavouriteShopsOutputPort output)
            : base(new GetFavouriteShopsInputValidator(), output)
        {
        }

        protected override Task Handle(GetFavouriteShopsInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
