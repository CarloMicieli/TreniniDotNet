using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

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
