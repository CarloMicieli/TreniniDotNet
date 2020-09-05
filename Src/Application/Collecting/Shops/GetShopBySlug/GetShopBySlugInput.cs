using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
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
