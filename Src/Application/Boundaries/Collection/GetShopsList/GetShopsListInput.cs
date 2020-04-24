using TreniniDotNet.Common.Interfaces;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.Boundaries.Collection.GetShopsList
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
