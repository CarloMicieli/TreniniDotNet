using System.Collections.Generic;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetLatestCatalogItems
{
    public sealed class GetLatestCatalogItemsOutput : IUseCaseOutput
    {
        public GetLatestCatalogItemsOutput(PaginatedResult<ICatalogItem> paginatedResult)
        {
            this.PaginatedResult = paginatedResult;
        }

        public PaginatedResult<ICatalogItem> PaginatedResult { get; }

        public IEnumerable<ICatalogItem> Results => PaginatedResult.Results;
    }
}
