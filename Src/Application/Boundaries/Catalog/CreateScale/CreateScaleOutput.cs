using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
{
    public class CreateScaleOutput : IUseCaseOutput
    {
        private readonly Slug _slug;

        public CreateScaleOutput(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}