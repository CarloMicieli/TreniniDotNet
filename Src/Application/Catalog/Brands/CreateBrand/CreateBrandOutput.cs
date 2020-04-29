using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
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
