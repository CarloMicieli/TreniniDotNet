using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockUseCase : AbstractUseCase<EditRollingStockInput, EditRollingStockOutput, IEditRollingStockOutputPort>
    {
        private readonly CatalogItemsService _catalogItemService;
        private readonly RollingStocksFactory _rollingStocksFactory;
        private readonly IUnitOfWork _unitOfWork;

        public EditRollingStockUseCase(
            IUseCaseInputValidator<EditRollingStockInput> inputValidator,
            IEditRollingStockOutputPort outputPort,
            RollingStocksFactory rollingStocksFactory,
            CatalogItemsService catalogItemService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
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
                .FirstOrDefault(it => it.Id == input.RollingStockId);
            if (rollingStock is null)
            {
                OutputPort.RollingStockWasNotFound(input.Slug, input.RollingStockId);
                return;
            }

            Railway? railway = null;
            if (!string.IsNullOrWhiteSpace(input.Values.Railway))
            {
                var railwaySlug = Slug.Of(input.Values.Railway);
                railway = await _catalogItemService.FindRailwayBySlug(railwaySlug);
                if (railway is null)
                {
                    OutputPort.RailwayWasNotFound(railwaySlug);
                    return;
                }
            }

            var length = LengthOverBuffer.CreateOrDefault(input.Values.LengthOverBuffer?.Inches, input.Values.LengthOverBuffer?.Millimeters);
            var minRadius = MinRadius.CreateOrDefault(input.Values.MinRadius);

            var couplers = EnumHelpers.OptionalValueFor<Couplers>(input.Values.Couplers);
            var epoch = !string.IsNullOrWhiteSpace(input.Values.Epoch) && Epoch.TryParse(input.Values.Epoch, out var e) ?
                e : (Epoch?)null;

            var category = EnumHelpers.OptionalValueFor<Category>(input.Values.Category);
            var dccInterface = EnumHelpers.OptionalValueFor<DccInterface>(input.Values.DccInterface);
            var control = EnumHelpers.OptionalValueFor<Control>(input.Values.Control);

            switch (rollingStock)
            {
                case Locomotive l:
                    var updatedLocomotive = l.With(
                        RailwayRef.AsOptional(railway),
                        category,
                        epoch,
                        length,
                        minRadius,
                        Prototype.OfLocomotive("", ""), //TODO
                        couplers,
                        input.Values.Livery,
                        input.Values.Depot,
                        dccInterface,
                        control);
                    catalogItem.UpdateRollingStock(updatedLocomotive);
                    break;
                case PassengerCar p:
                    var serviceLevel = input.Values.ServiceLevel.ToServiceLevelOpt();
                    var passengerCarType =
                        EnumHelpers.OptionalValueFor<PassengerCarType>(input.Values.PassengerCarType);
                    var passengerCar = p.With(
                        RailwayRef.AsOptional(railway),
                        epoch,
                        length,
                        minRadius,
                        couplers,
                        input.Values.TypeName,
                        input.Values.Livery,
                        passengerCarType,
                        serviceLevel);
                    catalogItem.UpdateRollingStock(passengerCar);
                    break;
                case FreightCar f:
                    var freightCar = f.With(
                        RailwayRef.AsOptional(railway),
                        epoch,
                        length,
                        minRadius,
                        couplers,
                        input.Values.TypeName,
                        input.Values.Livery);
                    catalogItem.UpdateRollingStock(freightCar);
                    break;
                case Train t:
                    var train = t.With(
                        RailwayRef.AsOptional(railway),
                        category,
                        epoch,
                        length,
                        minRadius,
                        couplers,
                        input.Values.TypeName,
                        input.Values.Livery,
                        dccInterface,
                        control);
                    catalogItem.UpdateRollingStock(train);
                    break;
            }

            await _catalogItemService.UpdateCatalogItemAsync(catalogItem);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditRollingStockOutput(input.Slug));
        }
    }
}
