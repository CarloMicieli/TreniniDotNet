using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopsList
{
    public sealed class GetShopsListOutput : IUseCaseOutput
    {
        public GetShopsListOutput(PaginatedResult<Shop> shops)
        {
            Shops = shops;
        }

        public PaginatedResult<Shop> Shops { get; }
    }
}
