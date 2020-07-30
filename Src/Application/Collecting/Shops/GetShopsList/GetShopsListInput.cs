using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopsList
{
    public sealed class GetShopsListInput : IUseCaseInput
    {
        public Page Page { get; }

        public GetShopsListInput(Page page)
        {
            this.Page = page;
        }
    }
}
