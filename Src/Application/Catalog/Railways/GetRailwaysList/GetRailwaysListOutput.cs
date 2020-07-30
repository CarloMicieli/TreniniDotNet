using System.Collections.Generic;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwaysList
{
    public sealed class GetRailwaysListOutput : IUseCaseOutput
    {
        public GetRailwaysListOutput(PaginatedResult<Railway> paginatedResult)
        {
            PaginatedResult = paginatedResult;
        }

        public PaginatedResult<Railway> PaginatedResult { get; }

        public IEnumerable<Railway> Result => PaginatedResult.Results;
    }
}
