using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.GetScaleBySlug
{
    public class GetScaleBySlugInput : IUseCaseInput
    {
        private readonly Slug _slug;

        public GetScaleBySlugInput(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}