using System.Collections.Generic;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemInput : IUseCaseInput
    {
        public CreateCatalogItemInput(
            string brandName,
            string itemNumber,
            string description,
            string? prototypeDescription,
            string? modelDescription,
            string powerMethod,
            string scale,
            string? deliveryDate,
            bool available,
            IList<RollingStockInput> rollingStocks)
        {
            Brand = brandName;
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

        public string Brand { get; }

        public string ItemNumber { get; }

        public string Description { get; }

        public string? PrototypeDescription { get; }

        public string? ModelDescription { get; }

        public string PowerMethod { get; }

        public string Scale { get; }

        public IList<RollingStockInput> RollingStocks { get; }

        public string? DeliveryDate { get; }

        public bool Available { get; }
    }
}
