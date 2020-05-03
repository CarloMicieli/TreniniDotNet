using System;
using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using static TreniniDotNet.Common.Enums.EnumHelpers;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public static class RollingStocksFactoryExtensions
    {
        public static IRollingStock FromInput(this IRollingStocksFactory factory, RollingStockInput input,
            IReadOnlyDictionary<Slug, IRailwayInfo> railways)
        {
            if (!railways.TryGetValue(Slug.Of(input.Railway), out var railwayInfo))
            {
                throw new ArgumentOutOfRangeException(nameof(railways),
                    $"Unable to find the '{input.Railway}' railway for this rolling stock");
            }

            return FromInput(factory, input, railwayInfo);
        }

        public static IRollingStock FromInput(this IRollingStocksFactory factory, RollingStockInput input, IRailwayInfo railwayInfo)
        {
            var category = RequiredValueFor<Category>(input.Category);

            var era = Epoch.Parse(input.Epoch);

            var length = LengthOverBuffer.CreateOrDefault(input.LengthOverBuffer?.Inches, input.LengthOverBuffer?.Millimeters);

            var dccInterface = OptionalValueFor<DccInterface>(input.DccInterface) ?? DccInterface.None;
            var control = OptionalValueFor<Control>(input.Control) ?? Control.None;

            if (Categories.IsLocomotive(category))
            {
                return factory.NewLocomotive(
                    railwayInfo,
                    category,
                    era,
                    length,
                    input.ClassName,
                    input.RoadNumber,
                    dccInterface,
                    control
                );
            }

            if (Categories.IsPassengerCar(category))
            {
                var passengerCarType = OptionalValueFor<PassengerCarType>(input.PassengerCarType);
                var serviceLevel = input.ServiceLevel.ToServiceLevelOpt();

                return factory.NewPassengerCar(
                    railwayInfo,
                    era,
                    length,
                    input.TypeName,
                    passengerCarType,
                    serviceLevel);
            }

            if (Categories.IsFreightCar(category))
            {
                return factory.NewFreightCar(
                    railwayInfo,
                    era,
                    length,
                    input.TypeName);
            }

            if (Categories.IsTrain(category))
            {
                return factory.NewTrain(
                    railwayInfo,
                    category,
                    era,
                    length,
                    input.ClassName,
                    input.RoadNumber,
                    dccInterface,
                    control);
            }

            throw new ArgumentOutOfRangeException(nameof(category));
        }
    }
}
