using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug
{
    public sealed class GetRailwayBySlugInput : IUseCaseInput
    {
        public GetRailwayBySlugInput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
