using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.Boundaries.Collection.GetShopsList
{
    public sealed class GetShopsListOutput : IUseCaseOutput
    {
        public GetShopsListOutput(PaginatedResult<IShop> shops)
        {
            Shops = shops;
        }

        public PaginatedResult<IShop> Shops { get; }
    }
}
