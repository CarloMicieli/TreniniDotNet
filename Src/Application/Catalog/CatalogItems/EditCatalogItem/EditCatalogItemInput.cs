﻿using System.Collections.Generic;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem
{
    public sealed class EditCatalogItemInput : IUseCaseInput
    {
        public EditCatalogItemInput(
            Slug slug,
            string? brand,
            string? itemNumber,
            string? description,
            string? prototypeDescription,
            string? modelDescription,
            string? powerMethod,
            string? scale,
            string? deliveryDate,
            bool? available,
            IReadOnlyList<RollingStockInput> rollingStocks)
        {
            Slug = slug;
            Values = new ModifiedCatalogItemValues(
                brand,
                itemNumber,
                description,
                prototypeDescription,
                modelDescription,
                powerMethod,
                scale,
                deliveryDate,
                available,
                rollingStocks);
        }

        public Slug Slug { get; }
        public ModifiedCatalogItemValues Values { get; }
    }

    public sealed class ModifiedCatalogItemValues
    {
        public ModifiedCatalogItemValues(
            string? brand,
            string? itemNumber,
            string? description,
            string? prototypeDescription,
            string? modelDescription,
            string? powerMethod,
            string? scale,
            string? deliveryDate,
            bool? available,
            IReadOnlyList<RollingStockInput> rollingStocks)
        {
            Brand = brand;
            ItemNumber = itemNumber;
            Description = description;
            PrototypeDescription = prototypeDescription;
            ModelDescription = modelDescription;
            PowerMethod = powerMethod;
            Scale = scale;
            RollingStocks = rollingStocks;
            DeliveryDate = deliveryDate;
            Available = available;
        }

        public string? Brand { get; }

        public string? ItemNumber { get; }

        public string? Description { get; }

        public string? PrototypeDescription { get; }

        public string? ModelDescription { get; }

        public string? PowerMethod { get; }

        public string? Scale { get; }

        public IReadOnlyList<RollingStockInput> RollingStocks { get; }

        public string? DeliveryDate { get; }

        public bool? Available { get; }
    }
}
