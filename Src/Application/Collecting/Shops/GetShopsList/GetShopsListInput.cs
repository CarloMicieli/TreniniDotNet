using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

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
