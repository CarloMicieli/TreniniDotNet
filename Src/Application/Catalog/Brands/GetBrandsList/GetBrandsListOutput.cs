using System.Collections.Generic;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandsList
{
    public sealed class GetBrandsListOutput : IUseCaseOutput
    {
        public GetBrandsListOutput(PaginatedResult<IBrand> paginatedResult)
        {
            PaginatedResult = paginatedResult;
        }

        public PaginatedResult<IBrand> PaginatedResult { get; }

        public IEnumerable<IBrand> Result => PaginatedResult.Results;
    }
}
