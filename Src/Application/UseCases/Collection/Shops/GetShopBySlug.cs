using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public sealed class GetShopBySlug : ValidatedUseCase<GetShopBySlugInput, IGetShopBySlugOutputPort>, IGetShopBySlugUseCase
    {
        public GetShopBySlug(IGetShopBySlugOutputPort output)
            : base(new GetShopBySlugInputValidator(), output)
        {
        }

        protected override Task Handle(GetShopBySlugInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
