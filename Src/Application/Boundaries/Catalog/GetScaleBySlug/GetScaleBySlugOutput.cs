using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug
{
    public class GetScaleBySlugOutput : IUseCaseOutput
    {
        private readonly IScale _scale;

        public GetScaleBySlugOutput(IScale scale)
        {
            _scale = scale;
        }

        public IScale Scale => _scale;
    }
}