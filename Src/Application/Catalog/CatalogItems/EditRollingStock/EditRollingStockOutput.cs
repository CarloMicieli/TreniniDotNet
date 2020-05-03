using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

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
