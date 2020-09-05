using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
{
    public sealed class GetScaleBySlugOutput : IUseCaseOutput
    {
        public GetScaleBySlugOutput(Scale scale)
        {
            Scale = scale;
        }

        public Scale Scale { get; }
    }
}
