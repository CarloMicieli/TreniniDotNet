using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public sealed class EditBrandOutput : IUseCaseOutput
    {
        public EditBrandOutput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
