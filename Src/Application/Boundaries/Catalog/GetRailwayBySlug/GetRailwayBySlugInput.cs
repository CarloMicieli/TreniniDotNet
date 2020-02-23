using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug
{
    public sealed class GetRailwayBySlugInput : IUseCaseInput
    {
        private readonly Slug _slug;

        public GetRailwayBySlugInput(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}
