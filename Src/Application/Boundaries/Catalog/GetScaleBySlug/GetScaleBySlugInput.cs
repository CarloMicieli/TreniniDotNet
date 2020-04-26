using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug
{
    public class GetScaleBySlugInput : IUseCaseInput
    {
        public GetScaleBySlugInput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}