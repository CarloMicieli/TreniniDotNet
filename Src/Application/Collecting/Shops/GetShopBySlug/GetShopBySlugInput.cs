using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

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
