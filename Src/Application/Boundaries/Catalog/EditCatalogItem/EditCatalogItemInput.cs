using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem
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
            IList<RollingStockInput> rollingStocks)
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
            IList<RollingStockInput> rollingStocks)
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

        public IList<RollingStockInput> RollingStocks { get; }

        public string? DeliveryDate { get; }

        public bool? Available { get; }
    }
}
