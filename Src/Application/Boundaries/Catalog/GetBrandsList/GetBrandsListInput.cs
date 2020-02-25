using TreniniDotNet.Common.Interfaces;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList
{
    public sealed class GetBrandsListInput : IUseCaseInput
    {
        public Page? Page { get; }

        public GetBrandsListInput()
            : this(default)
        { }

        public GetBrandsListInput(Page? page)
        {
            this.Page = page;
        }
    }
}
