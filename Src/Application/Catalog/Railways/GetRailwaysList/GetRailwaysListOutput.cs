using System.Collections.Generic;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwaysList
{
    public sealed class GetRailwaysListOutput : IUseCaseOutput
    {
        public GetRailwaysListOutput(PaginatedResult<IRailway> paginatedResult)
        {
            PaginatedResult = paginatedResult;
        }

        public PaginatedResult<IRailway> PaginatedResult { get; }

        public IEnumerable<IRailway> Result => PaginatedResult.Results;
    }
}
