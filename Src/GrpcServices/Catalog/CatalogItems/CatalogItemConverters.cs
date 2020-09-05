using TreniniDotNet.Catalog;
using DomainCategory = TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks.Category;
using DomainControl = TreniniDotNet.Domain.Catalog.CatalogItems.Control;
using DomainDccInterface = TreniniDotNet.Domain.Catalog.CatalogItems.DccInterface;
using DomainPassengerCarType = TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks.PassengerCarType;
using DomainPowerMode = TreniniDotNet.Domain.Catalog.CatalogItems.PowerMethod;

namespace TreniniDotNet.GrpcServices.Catalog.CatalogItems
{
    public static class CatalogItemConverters
    {
        public static string? ToPowerMethodName(this PowerMethod rpcPowerMethod)
        {
            return rpcPowerMethod switch
            {
                PowerMethod.Ac => DomainPowerMode.AC.ToString(),
                PowerMethod.Dc => DomainPowerMode.DC.ToString(),
                _ => null
            };
        }

        public static string? ToControlName(this Control rpcControl)
        {
            return rpcControl switch
            {
                Control.Dcc => DomainControl.Dcc.ToString(),
                Control.DccReady => DomainControl.DccReady.ToString(),
                Control.DccSound => DomainControl.DccSound.ToString(),
                Control.NoControl => DomainControl.None.ToString(),
                _ => null
            };
        }

        public static string? ToCategoryName(this Category rpcCategory)
        {
            return rpcCategory switch
            {
                Category.SteamLocomotive => DomainCategory.SteamLocomotive.ToString(),
                Category.DieselLocomotive => DomainCategory.DieselLocomotive.ToString(),
                Category.ElectricLocomotive => DomainCategory.ElectricLocomotive.ToString(),
                Category.Railcar => DomainCategory.Railcar.ToString(),
                Category.ElectricMultipleUnit => DomainCategory.ElectricMultipleUnit.ToString(),
                Category.FreightCar => DomainCategory.FreightCar.ToString(),
                Category.PassengerCar => DomainCategory.PassengerCar.ToString(),
                Category.TrainSet => DomainCategory.TrainSet.ToString(),
                Category.StarterSet => DomainCategory.StarterSet.ToString(),
                _ => null
            };
        }

        public static string? ToDccInterfaceName(this DccInterface rpcDccInterface)
        {
            return rpcDccInterface switch
            {
                DccInterface.NoInterface => DomainDccInterface.None.ToString(),
                DccInterface.Nem651 => DomainDccInterface.Nem651.ToString(),
                DccInterface.Nem652 => DomainDccInterface.Nem652.ToString(),
                DccInterface.Plux8 => DomainDccInterface.Plux8.ToString(),
                DccInterface.Plux16 => DomainDccInterface.Plux16.ToString(),
                DccInterface.Plux22 => DomainDccInterface.Plux22.ToString(),
                DccInterface.Next18 => DomainDccInterface.Next18.ToString(),
                DccInterface.Mtc21 => DomainDccInterface.Mtc21.ToString(),
                _ => null
            };
        }

        public static string? ToPassengerCarType(this PassengerCarType passengerCarType)
        {
            if (passengerCarType == PassengerCarType.NoType)
            {
                return null;
            }

            return passengerCarType.ToString();
        }

        public static string? ToCouplers(this Couplers couplers)
        {
            if (couplers == Couplers.NoCouplers)
            {
                return null;
            }

            return couplers.ToString();
        }
    }
}
