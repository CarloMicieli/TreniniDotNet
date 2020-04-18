using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug
{
    public sealed class GetShopBySlugInput : IUseCaseInput
    {
        public GetShopBySlugInput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
