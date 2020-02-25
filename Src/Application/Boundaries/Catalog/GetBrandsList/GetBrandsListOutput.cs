using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList
{
    public sealed class GetBrandsListOutput : IUseCaseOutput
    {
        private readonly PaginatedResult<IBrand> _paginatedResult;

        public GetBrandsListOutput(PaginatedResult<IBrand> paginatedResult)
        {
            _paginatedResult = paginatedResult;
        }

        public IEnumerable<IBrand> Result => _paginatedResult.Results;

        public PaginatedResult<IBrand> PaginatedResult => _paginatedResult;
    }
}
