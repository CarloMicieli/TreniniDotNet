using System;
using DataSeeding.DataLoader.Records.Catalog.CatalogItems;
using TreniniDotNet.Catalog;
using RollingStock = TreniniDotNet.Catalog.RollingStock;

namespace DataSeeding.Clients.Catalog.CatalogItems
{
    public static class CatalogItemRequests
    {
        public static CreateCatalogItemRequest From(CatalogItem catalogItem)
        {
            var request = new CreateCatalogItemRequest
            {
                Brand = catalogItem.Brand,
                DeliveryDate = catalogItem.DeliveryDate ?? "",
                Description = catalogItem.Description ?? "",
                IsAvailable = catalogItem.Available ?? true,
                ItemNumber = catalogItem.ItemNumber ?? "",
                ModelDescription = catalogItem.ModelDescription ?? "",
                PowerMethod = ExtractPowerMethod(catalogItem.PowerMethod),
                PrototypeDescription = catalogItem.PrototypeDescription ?? "",
                Scale = catalogItem.Scale ?? ""
            };

            foreach (var rs in catalogItem.RollingStocks)
            {
                request.RollingStocks.Add(new RollingStock
                {
                    Railway = rs.Railway ?? "",
                    Category = ExtractCategory(rs.Category),
                    ClassName = rs.ClassName ?? "",
                    RoadNumber = rs.RoadNumber ?? "",
                    TypeName = rs.TypeName ?? "",
                    Control = ExtractControl(rs.Control),
                    DccInterface = ExtractDccInterface(rs.DccInterface),
                    Epoch = rs.Era ?? "",
                    Length = new RollingStockLength
                    {
                        Millimeters = rs.Length.Millimeters ?? 0,
                        Inches =  rs.Length.Inches ?? 0
                    },
                    PassengerCarType = ExtractPassengerCarType(rs.PassengerCarType),
                    Couplers = ExtractCouplers(rs.Couplers),
                    Livery = rs.Livery ?? ""
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
