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
            var minRadius = MinRadius.CreateOrDefault(input.MinRadius);

            var dccInterface = OptionalValueFor<DccInterface>(input.DccInterface) ?? DccInterface.None;
            var control = OptionalValueFor<Control>(input.Control) ?? Control.None;
            var couplers = OptionalValueFor<Couplers>(input.Couplers);

            if (Categories.IsLocomotive(category))
            {
                return factory.NewLocomotive(
                    railwayInfo,
                    category,
                    era,
                    length,
                    minRadius,
                    input.ClassName,
                    input.RoadNumber,
                    couplers,
                    input.Livery,
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
                    minRadius,
                    input.TypeName,
                    couplers,
                    input.Livery,
                    passengerCarType,
                    serviceLevel);
            }

            if (Categories.IsFreightCar(category))
            {
                return factory.NewFreightCar(
                    railwayInfo,
                    era,
                    length,
                    minRadius,
                    input.TypeName,
                    couplers,
                    input.Livery);
            }

            if (Categories.IsTrain(category))
            {
                return factory.NewTrain(
                    railwayInfo,
                    category,
                    era,
                    length,
                    minRadius,
                    input.ClassName,
                    input.RoadNumber,
                    couplers,
                    input.Livery,
                    dccInterface,
                    control);
            }

            throw new ArgumentOutOfRangeException(nameof(category));
        }
    }
}
