using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

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
