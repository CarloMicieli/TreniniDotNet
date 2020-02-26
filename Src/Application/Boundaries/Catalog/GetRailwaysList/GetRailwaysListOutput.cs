using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList
{
    public sealed class GetRailwaysListOutput : IUseCaseOutput
    {
        private readonly PaginatedResult<IRailway> _paginatedResult;

        public GetRailwaysListOutput(PaginatedResult<IRailway> paginatedResult)
        {
            _paginatedResult = paginatedResult;
        }

        public IEnumerable<IRailway> Result => _paginatedResult.Results;

        public PaginatedResult<IRailway> PaginatedResult => _paginatedResult;
    }
}
