using System;
using DataSeeding.Clients.Extensions;
using DataSeeding.DataLoader.Records.Catalog.CatalogItems;
using TreniniDotNet.Catalog;
using RollingStock = TreniniDotNet.Catalog.RollingStock;

namespace DataSeeding.Clients.Catalog.CatalogItems
{
    public static class CatalogItemRequests
    {
        public static CreateCatalogItemRequest RequestFromRecord(CatalogItem catalogItem)
        {
            var request = new CreateCatalogItemRequest
            {
                Brand = catalogItem.Brand,
                DeliveryDate = catalogItem.DeliveryDate.ToStringOrBlank(),
                Description = catalogItem.Description.ToStringOrBlank(),
                IsAvailable = catalogItem.Available ?? true,
                ItemNumber = catalogItem.ItemNumber.ToStringOrBlank(),
                ModelDescription = catalogItem.ModelDescription.ToStringOrBlank(),
                PowerMethod = ExtractPowerMethod(catalogItem.PowerMethod),
                PrototypeDescription = catalogItem.PrototypeDescription.ToStringOrBlank(),
                Scale = catalogItem.Scale.ToStringOrBlank()
            };

            foreach (var rs in catalogItem.RollingStocks)
            {
                request.RollingStocks.Add(new RollingStock
                {
                    Railway = rs.Railway.ToStringOrBlank(),
                    Category = ExtractCategory(rs.Category),
                    Epoch = rs.Epoch.ToStringOrBlank(),
                    Length = new RollingStockLength
                    {
                        Millimeters = rs.Length.Millimeters,
                        Inches =  rs.Length.Inches
                    },
                    DccInterface = ExtractDccInterface(rs.DccInterface),
                    Control = ExtractControl(rs.Control),
                    ClassName = rs.ClassName.ToStringOrBlank(),
                    RoadNumber = rs.RoadNumber.ToStringOrBlank(),
                    TypeName = rs.TypeName.ToStringOrBlank(),
                    Series = rs.Series.ToStringOrBlank(),
                    Depot = rs.Depot.ToStringOrBlank(),
                    Livery = rs.Livery.ToStringOrBlank(),
                    MinRadius = rs.MinRadius,
                    Couplers = ExtractCouplers(rs.Couplers),
                    ServiceLevel = rs.ServiceLevel.ToStringOrBlank(),
                    PassengerCarType = ExtractPassengerCarType(rs.PassengerCarType)
                });
            }

            return request;
        }

        private static Couplers ExtractCouplers(string s)
        {
            return Enum.TryParse<Couplers>(s, true, out var couplers) ? couplers : Couplers.NoCouplers;
        }

        private static PassengerCarType ExtractPassengerCarType(string s)
        {
            return Enum.TryParse<PassengerCarType>(s, true, out var passengerCarType) ? passengerCarType : PassengerCarType.NoType;
        }

        private static DccInterface ExtractDccInterface(string s)
        {
            return Enum.TryParse<DccInterface>(s, true, out var dccInterface) ? dccInterface : DccInterface.NoInterface;
        }

        private static Control ExtractControl(string s)
        {
            return Enum.TryParse<Control>(s, true, out var control) ? control : Control.NoControl;
        }

        private static Category ExtractCategory(string s)
        {
            return Enum.TryParse<Category>(s, true, out var cat) ? cat : Category.NoCategory;
        }

        private static PowerMethod ExtractPowerMethod(string s)
        {
            return Enum.TryParse<PowerMethod>(s, true, out var powerMethod) ? powerMethod : PowerMethod.NoPowerMethod;
        }
    }
}
