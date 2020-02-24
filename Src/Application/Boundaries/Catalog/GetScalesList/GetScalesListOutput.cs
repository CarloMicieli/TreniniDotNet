using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetScalesList
{
    public sealed class GetScalesListOutput : IUseCaseOutput
    {
        private readonly IEnumerable<IScale> result;

        public GetScalesListOutput(IEnumerable<IScale> result)
        {
            this.result = result;
        }

        public IEnumerable<IScale> Result => result;
    }
}
