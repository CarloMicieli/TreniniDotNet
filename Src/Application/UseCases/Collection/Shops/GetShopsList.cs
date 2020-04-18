using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetShopsList;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public sealed class GetShopsList : ValidatedUseCase<GetShopsListInput, IGetShopsListOutputPort>, IGetShopsListUseCase
    {
        public GetShopsList(IGetShopsListOutputPort output)
            : base(new GetShopsListInputValidator(), output)
        {
        }

        protected override Task Handle(GetShopsListInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
