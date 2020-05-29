﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.DeliveryDates;
using static TreniniDotNet.Common.Enums.EnumHelpers;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem
{
    public sealed class EditCatalogItemUseCase : ValidatedUseCase<EditCatalogItemInput, IEditCatalogItemOutputPort>, IEditCatalogItemUseCase
    {
        private readonly CatalogItemService _catalogItemService;
        private readonly IRollingStocksFactory _rollingStocksFactory;
        private readonly IUnitOfWork _unitOfWork;

        public EditCatalogItemUseCase(
            IEditCatalogItemOutputPort output,
            IRollingStocksFactory rollingStocksFactory,
            CatalogItemService catalogItemService,
            IUnitOfWork unitOfWork)
            : base(new EditCatalogItemInputValidator(), output)
        {
            _catalogItemService = catalogItemService ??
                throw new ArgumentNullException(nameof(catalogItemService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _rollingStocksFactory = rollingStocksFactory ??
                throw new ArgumentNullException(nameof(rollingStocksFactory));
        }

        protected override async Task Handle(EditCatalogItemInput input)
        {
            var item = await _catalogItemService.GetBySlugAsync(input.Slug);
            if (item is null)
            {
                OutputPort.CatalogItemNotFound(input.Slug);
                return;
            }

            IBrandInfo? brand = null;
            if (input.Values.Brand != null)
            {
                var brandSlug = Slug.Of(input.Values.Brand);
                brand = await _catalogItemService.FindBrandInfoBySlug(brandSlug);
                if (brand is null)
                {
                    OutputPort.BrandNotFound(brandSlug);
                    return;
                }
            }

            IScaleInfo? scale = null;
            if (input.Values.Scale != null)
            {
                var scaleSlug = Slug.Of(input.Values.Scale);
                scale = await _catalogItemService.FindScaleInfoBySlug(scaleSlug);
                if (scale is null)
                {
                    OutputPort.ScaleNotFound(scaleSlug);
                    return;
                }
            }

            IReadOnlyList<IRollingStock> rollingStocks = ImmutableList<IRollingStock>.Empty;
            if (input.Values.RollingStocks != null &&
                input.Values.RollingStocks.Count > 0)
            {
                IEnumerable<Slug> inputRailways = input.Values.RollingStocks
                    .Select(it => Slug.Of(it.Railway))
                    .Distinct();

                var (railways, railwaysNotFound) =
                    await _catalogItemService.FindRailwaysInfoBySlug(inputRailways);

                if (railwaysNotFound.Count > 0)
                {
                    OutputPort.RailwayNotFound(railwaysNotFound);
                    return;
                }

                rollingStocks = input.Values.RollingStocks
                    .Select(it => _rollingStocksFactory.FromInput(it, railways))
                    .ToImmutableList();
            }

            ItemNumber? itemNumber = null;
            if (input.Values.ItemNumber != null)
            {
                itemNumber = new ItemNumber(input.Values.ItemNumber);
            }

            var deliveryDate = input.Values.DeliveryDate.ToDeliveryDateOpt();
            var powerMethod = OptionalValueFor<PowerMethod>(input.Values.PowerMethod);

            await _catalogItemService.UpdateCatalogItem(
                item,
                brand,
                itemNumber,
                scale,
                powerMethod,
                rollingStocks,
                input.Values.Description,
                input.Values.PrototypeDescription,
                input.Values.ModelDescription,
                deliveryDate,
                input.Values.Available);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditCatalogItemOutput());
        }
    }
}