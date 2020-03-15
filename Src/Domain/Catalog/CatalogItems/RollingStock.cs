using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class RollingStock : IRollingStock
    {
        private readonly IRailwayInfo _railway;
        private readonly Category _category;
        private readonly Era _era;
        private readonly Length _length;
        private readonly string? _className;
        private readonly string? _roadNumber;

        public RollingStock(IRailwayInfo railway, Category category, Era era, Length length, string? className, string? roadNumber)
        {
            _railway = railway;
            _category = category;
            _era = era;
            _length = length;
            _className = className;
            _roadNumber = roadNumber;
        }

        #region [ Properties ]
        public IRailwayInfo Railway => _railway;

        public Category Category => _category;

        public Era Era => _era;

        public Length Length => _length;

        public string? ClassName => _className;

        public string? RoadNumber => _roadNumber;
        #endregion
    }
}
