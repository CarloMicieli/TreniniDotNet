using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.GetBrandBySlug
{
    public class GetBrandBySlugInput : IUseCaseInput
    {
        private readonly Slug _slug;

        public GetBrandBySlugInput(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}
