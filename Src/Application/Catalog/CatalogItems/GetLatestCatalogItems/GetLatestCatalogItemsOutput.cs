using System.Collections.Generic;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetLatestCatalogItems
{
    public sealed class GetLatestCatalogItemsOutput : IUseCaseOutput
    {
        public GetLatestCatalogItemsOutput(PaginatedResult<CatalogItem> paginatedResult)
        {
            this.PaginatedResult = paginatedResult;
        }

        public PaginatedResult<CatalogItem> PaginatedResult { get; }

        public IEnumerable<CatalogItem> Results => PaginatedResult.Results;
    }
}
