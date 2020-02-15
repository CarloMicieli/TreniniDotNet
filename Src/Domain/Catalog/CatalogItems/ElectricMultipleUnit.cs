using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class ElectricMultipleUnit : RollingStock
    {
        public ElectricMultipleUnit(Railway railway, Scale scale, Era era, PowerMethod powerMethod, Length length)
            : base(railway, scale, Category.ElectricMultipleUnit, era, powerMethod, length)
        {
        }
    }
}
