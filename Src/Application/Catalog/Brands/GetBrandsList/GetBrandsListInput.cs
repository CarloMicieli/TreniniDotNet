using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandsList
{
    public sealed class GetBrandsListInput : IUseCaseInput
    {
        public Page Page { get; }

        public GetBrandsListInput(Page page)
        {
            this.Page = page;
        }
    }
}
