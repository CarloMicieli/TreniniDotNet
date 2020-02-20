using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
{
    public class CreateScaleOutput : IUseCaseOutput
    {
        private readonly Slug _slug;

        public CreateScaleOutput(Scale s)
        {
            _slug = s.Slug;
        }

        public Slug Slug => _slug;
    }
}