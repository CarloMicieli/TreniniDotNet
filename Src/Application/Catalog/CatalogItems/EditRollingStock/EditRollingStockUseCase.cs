using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockUseCase : ValidatedUseCase<EditRollingStockInput, IEditRollingStockOutputPort>, IEditRollingStockUseCase
    {
        private readonly CatalogItemService _catalogItemService;
        private readonly IRollingStocksFactory _rollingStocksFactory;
        private readonly IUnitOfWork _unitOfWork;

        public EditRollingStockUseCase(IEditRollingStockOutputPort output,
            CatalogItemService catalogItemService,
            IRollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork)
            : base(new EditRollingStockInputValidator(), output)
        {
            _catalogItemService = catalogItemService ??
                throw new ArgumentNullException(nameof(catalogItemService));
            _rollingStocksFactory = rollingStocksFactory ??
                throw new ArgumentNullException(nameof(rollingStocksFactory));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(EditRollingStockInput input)
        {
            var catalogItem = await _catalogItemService.GetBySlugAsync(input.Slug);
            if (catalogItem is null)
            {
                OutputPort.CatalogItemWasNotFound(input.Slug);
                return;
            }

            var rollingStock = catalogItem.RollingStocks
                .FirstOrDefault(it => it.RollingStockId == input.RollingStockId);
            if (rollingStock is null)
            {
                OutputPort.RollingStockWasNotFound(input.Slug, input.RollingStockId);
                return;
            }

            IRailwayInfo? railwayInfo = null;
            if (!string.IsNullOrWhiteSpace(input.Values.Railway))
            {
                var railwaySlug = Slug.Of(input.Values.Railway);
                railwayInfo = await _catalogItemService.FindRailwayInfoBySlug(railwaySlug);
                if (railwayInfo is null)
                {
                    OutputPort.RailwayWasNotFound(railwaySlug);
                    return;
                }
            }

            var couplers = EnumHelpers.OptionalValueFor<Couplers>(input.Values.Couplers);
            var epoch = !string.IsNullOrWhiteSpace(input.Values.Epoch) && Epoch.TryParse(input.Values.Epoch, out var e) ?
                e : (Epoch?)null;

            var modifiedRollingStock = rollingStock.With(
                railwayInfo,
                EnumHelpers.OptionalValueFor<Category>(input.Values.Category),
                epoch,
                null,
                input.Values.ClassName,
                input.Values.RoadNumber,
                input.Values.TypeName,
                couplers,
                input.Values.Livery,
                EnumHelpers.OptionalValueFor<PassengerCarType>(input.Values.PassengerCarType),
                null,
                EnumHelpers.OptionalValueFor<DccInterface>(input.Values.DccInterface),
                EnumHelpers.OptionalValueFor<Control>(input.Values.Control));

            await _catalogItemService.UpdateRollingStockAsync(catalogItem, modifiedRollingStock);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditRollingStockOutput(input.Slug));
        }
    }
}
