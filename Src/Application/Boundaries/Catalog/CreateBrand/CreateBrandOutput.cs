using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public sealed class CreateBrandOutput : IUseCaseOutput
    {
        public CreateBrandOutput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
