using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class FreightCar : RollingStock
    {
        public FreightCar(Railway railway, Scale scale, Era era, Length length)
            : base(railway, scale, Category.FreightCar, era, PowerMethod.None, length)
        {
        }
    }
}
