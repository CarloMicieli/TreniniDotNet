using System.Collections.Generic;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandsList
{
    public sealed class GetBrandsListOutput : IUseCaseOutput
    {
        public GetBrandsListOutput(PaginatedResult<Brand> paginatedResult)
        {
            PaginatedResult = paginatedResult;
        }

        public PaginatedResult<Brand> PaginatedResult { get; }

        public IEnumerable<Brand> Result => PaginatedResult.Results;
    }
}
