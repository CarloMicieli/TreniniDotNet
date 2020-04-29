using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
{
    public class GetScaleBySlugOutput : IUseCaseOutput
    {
        public GetScaleBySlugOutput(IScale scale)
        {
            Scale = scale;
        }

        public IScale Scale { get; }
    }
}