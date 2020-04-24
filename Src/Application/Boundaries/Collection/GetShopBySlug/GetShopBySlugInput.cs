using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug
{
    public sealed class GetShopBySlugInput : IUseCaseInput
    {
        public GetShopBySlugInput(string slug)
        {
            Slug = slug;
        }

        public string Slug { get; }
    }
}
