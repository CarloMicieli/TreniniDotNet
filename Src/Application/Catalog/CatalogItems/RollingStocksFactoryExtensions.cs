using System;
using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Slugs;
using static TreniniDotNet.Common.Enums.EnumHelpers;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public static class RollingStocksFactoryExtensions
    {
        public static RollingStock FromInput(this RollingStocksFactory factory,
            RollingStockInput input,
            IReadOnlyDictionary<Slug, Railway> railways)
        {
            if (!railways.TryGetValue(Slug.Of(input.Railway), out var railway))
            {
                throw new ArgumentOutOfRangeException(nameof(railways),
                    $"Unable to find the '{input.Railway}' railway for this rolling stock");
            }

            return FromInput(factory, input, railway);
        }

        public static RollingStock FromInput(this RollingStocksFactory factory, RollingStockInput input, Railway railway)
        {
            var category = RequiredValueFor<Category>(input.Category);

            var epoch = Epoch.Parse(input.Epoch);

            var length = LengthOverBuffer.CreateOrDefault(input.LengthOverBuffer?.Inches, input.LengthOverBuffer?.Millimeters);
            var minRadius = MinRadius.CreateOrDefault(input.MinRadius);

            var dccInterface = OptionalValueFor<DccInterface>(input.DccInterface) ?? DccInterface.None;
            var control = OptionalValueFor<Control>(input.Control) ?? Control.None;
            var couplers = OptionalValueFor<Couplers>(input.Couplers);

            if (Categories.IsLocomotive(category))
            {
                var prototype = (!string.IsNullOrEmpty(input.ClassName) && !string.IsNullOrEmpty(input.RoadNumber))
                    ? Prototype.OfLocomotive(input.ClassName, input.RoadNumber, input.Series)
                    : null;

                return factory.CreateLocomotive(
                    railway,
                    category,
                    epoch,
                    length,
                    minRadius,
                    prototype,
                    couplers,
                    input.Livery,
                    input.Depot,
                    dccInterface,
                    control
                );
            }

            if (Categories.IsPassengerCar(category))
            {
                var passengerCarType = OptionalValueFor<PassengerCarType>(input.PassengerCarType);
                var serviceLevel = input.ServiceLevel.ToServiceLevelOpt();

                return factory.CreatePassengerCar(
                    railway,
                    epoch,
                    length,
                    minRadius,
                    couplers,
                    input.TypeName,
                    input.Livery,
                    passengerCarType,
                    serviceLevel);
            }

            if (Categories.IsFreightCar(category))
            {
                return factory.CreateFreightCar(
                    railway,
                    epoch,
                    length,
                    minRadius,
                    couplers,
                    input.TypeName,
                    input.Livery);
            }

            if (Categories.IsTrain(category))
            {
                return factory.CreateTrain(
                    railway,
                    category,
                    epoch,
                    length,
                    minRadius,
                    couplers,
                    input.TypeName,
                    input.Livery,
                    dccInterface,
                    control);
            }

            throw new ArgumentOutOfRangeException(nameof(category));
        }
    }
}