using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface IRollingStockFactory
    {
        IRollingStock CreateLocomotive(RoadName roadName, RoadNumber roadNumber, Railway railway, Scale scale, Category category, Era era, PowerMethod powerMethod, Length length);

        IRollingStock CreatePassengerCar(Railway railway, Scale scale, Era era, Length length);

        IRollingStock CreateFreightCar(Railway railway, Scale scale, Era era, Length length);

        IRollingStock CreateRailcar(Railway railway, Scale scale, Era era, PowerMethod powerMethod, Length length);

        IRollingStock CreateElectricMultipleUnit(Railway railway, Scale scale, Era era, PowerMethod powerMethod, Length length);
    }
}