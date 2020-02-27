using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public sealed class CreateBrandOutput : IUseCaseOutput
    {
        private readonly Slug _slug;

        public CreateBrandOutput(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}
