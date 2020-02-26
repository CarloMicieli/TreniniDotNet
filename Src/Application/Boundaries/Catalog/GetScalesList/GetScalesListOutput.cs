using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetScalesList
{
    public sealed class GetScalesListOutput : IUseCaseOutput
    {
        private readonly PaginatedResult<IScale> _paginatedResult;

        public GetScalesListOutput(PaginatedResult<IScale> paginatedResult)
        {
            this._paginatedResult = paginatedResult;
        }

        public IEnumerable<IScale> Result => PaginatedResult.Results;

        public PaginatedResult<IScale> PaginatedResult => _paginatedResult;
    }
}
