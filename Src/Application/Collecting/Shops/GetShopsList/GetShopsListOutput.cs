using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopsList
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
