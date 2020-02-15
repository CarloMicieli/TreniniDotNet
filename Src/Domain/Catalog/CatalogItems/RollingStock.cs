using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    /// <summary>
    /// It represents a rolling stock.
    /// </summary>
    public abstract class RollingStock : IRollingStock
    {
        private readonly Railway _railway;
        private readonly Scale _scale;
        private readonly Category _category;
        private readonly Era _era;
        private readonly PowerMethod _powerMethod;
        private readonly Length _length;

        protected RollingStock(Railway railway, Scale scale, Category category, Era era, PowerMethod powerMethod, Length length)
        {
            _railway = railway;
            _scale = scale;
            _category = category;
            _era = era;
            _powerMethod = powerMethod;
            _length = length;
        }

        #region [ Properties ]
        public Railway Railway => _railway;

        public Scale Scale => _scale;

        public Category Category => _category;

        public Era Era => _era;

        public PowerMethod PowerMethod => _powerMethod;

        public Length Length => _length;
        #endregion
    }
}
