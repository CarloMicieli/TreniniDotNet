using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public class GetBrandBySlugInput : IUseCaseInput
    {
        public GetBrandBySlugInput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
