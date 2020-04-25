using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditBrand
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
