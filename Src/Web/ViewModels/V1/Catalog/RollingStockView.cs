using System;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public sealed class RollingStockView
    {
        private readonly IRollingStock _rs;

        internal RollingStockView(IRollingStock rs)
        {
            _rs = rs ??
                throw new ArgumentNullException(nameof(rs));

            Railway = new RailwayInfoView(rs.Railway);
        }

        public Guid Id => _rs.RollingStockId.ToGuid();

        public RailwayInfoView Railway { get; }

        public string Category => _rs.Category.ToString();

        public string Era => _rs.Era.ToString();

        public decimal? Length => _rs.Length?.Value;

        public string? ClassName => _rs.ClassName;

        public string? RoadNumber => _rs.RoadNumber;

        public string? TypeName => _rs.TypeName;

        public string? DccInterface => _rs.DccInterface.ToString();

        public string? Control => _rs.Control.ToString();
    }
}
