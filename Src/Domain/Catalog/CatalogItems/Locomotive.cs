using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class Locomotive : RollingStock
    {
        private readonly RoadName _roadName;
        private readonly RoadNumber _roadNumber;
        private readonly string? _notes;

        public Locomotive(RoadName roadName, RoadNumber roadNumber, Railway railway, Scale scale, Category category, Era era, PowerMethod powerMethod, Length length, string? notes)
            : base(railway, scale, category, era, powerMethod, length)
        {
            _roadName = roadName;
            _roadNumber = roadNumber;
            _notes = notes;
        }
    }
}
