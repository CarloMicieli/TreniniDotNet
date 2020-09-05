using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockOutput : IUseCaseOutput
    {
        public EditRollingStockOutput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
