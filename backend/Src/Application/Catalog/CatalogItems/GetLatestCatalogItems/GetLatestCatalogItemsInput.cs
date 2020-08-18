using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetLatestCatalogItems
{
    public sealed class GetLatestCatalogItemsInput : IUseCaseInput
    {
        public GetLatestCatalogItemsInput(Page page)
        {
            Page = page;
        }

        public Page Page { get; }
    }
}
