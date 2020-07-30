using System.Collections.Generic;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog.Scales.GetScalesList
{
    public sealed class GetScalesListOutput : IUseCaseOutput
    {
        public GetScalesListOutput(PaginatedResult<Scale> paginatedResult)
        {
            this.PaginatedResult = paginatedResult;
        }

        public PaginatedResult<Scale> PaginatedResult { get; }

        public IEnumerable<Scale> Result => PaginatedResult.Results;
    }
}
