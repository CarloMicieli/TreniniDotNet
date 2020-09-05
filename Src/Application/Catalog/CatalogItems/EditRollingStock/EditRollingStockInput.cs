using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockInput : IUseCaseInput
    {
        public EditRollingStockInput(Slug slug,
            RollingStockId rollingStockId,
            string? epoch, string? category,
            string? railway,
            string? className, string? roadNumber, string? typeName, string? series,
            string? passengerCarType, string? serviceLevel,
            string? couplers,
            string? livery,
            string? depot,
            LengthOverBufferInput? lengthOverBuffer,
            decimal? minRadius,
            string? control, string? dccInterface)
        {
            RollingStockId = rollingStockId;
            Slug = slug;
            Values = new RollingStockModifiedValues(
                epoch, category,
                railway,
                className, roadNumber, typeName, series,
                couplers,
                livery, depot,
                passengerCarType, serviceLevel,
                lengthOverBuffer,
                minRadius,
                control, dccInterface);
        }

        public RollingStockId RollingStockId { get; }
        public Slug Slug { get; }
        public RollingStockModifiedValues Values { get; }
    }
}
