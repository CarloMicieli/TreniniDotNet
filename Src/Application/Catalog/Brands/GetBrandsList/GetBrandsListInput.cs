using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandsList
{
    public sealed class GetBrandsListInput : IUseCaseInput
    {
        public Page Page { get; }

        public GetBrandsListInput(Page page)
        {
            Page = page;
        }
    }
}
