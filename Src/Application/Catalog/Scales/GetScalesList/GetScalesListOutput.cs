using System.Collections.Generic;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog.Scales.GetScalesList
{
    public sealed class GetScalesListOutput : IUseCaseOutput
    {
        public GetScalesListOutput(PaginatedResult<IScale> paginatedResult)
        {
            this.PaginatedResult = paginatedResult;
        }

        public PaginatedResult<IScale> PaginatedResult { get; }

        public IEnumerable<IScale> Result => PaginatedResult.Results;
    }
}
