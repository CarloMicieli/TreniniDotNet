using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug
{
    public class GetScaleBySlugOutput : IUseCaseOutput
    {
        private readonly Scale? _scale;

        public GetScaleBySlugOutput(Scale scale)
        {
            _scale = scale;
        }

        public Scale? Scale => _scale;
    }
}