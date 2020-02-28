using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class RollingStock : IRollingStock
    {
        private readonly IRailway _railway;
        private readonly IScale _scale;
        private readonly Category _category;
        private readonly Era _era;
        private readonly PowerMethod _powerMethod;
        private readonly Length _length;

        public RollingStock(IRailway railway, IScale scale, Category category, Era era, PowerMethod powerMethod, Length length)
        {
            _railway = railway;
            _scale = scale;
            _category = category;
            _era = era;
            _powerMethod = powerMethod;
            _length = length;
        }

        #region [ Properties ]
        public IRailway Railway => _railway;

        public IScale Scale => _scale;

        public Category Category => _category;

        public Era Era => _era;

        public PowerMethod PowerMethod => _powerMethod;

        public Length Length => _length;
        #endregion
    }
}
