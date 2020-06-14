using System;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.ViewModels
{
    public sealed class RollingStockView
    {
        private readonly IRollingStock _rs;
        private readonly LengthOverBufferView? _lob;
        private readonly MinRadiusView? _minRadius;

        internal RollingStockView(IRollingStock rs)
        {
            _rs = rs ??
                throw new ArgumentNullException(nameof(rs));

            Railway = new RailwayInfoView(rs.Railway);

            if (!(rs.Length is null))
            {
                _lob = new LengthOverBufferView(rs.Length);
            }

            if (!(rs.MinRadius is null))
            {
                _minRadius = new MinRadiusView(rs.MinRadius);
            }
        }

        public Guid Id => _rs.Id.ToGuid();

        public RailwayInfoView Railway { get; }

        public string Category => _rs.Category.ToString();

        public string Epoch => _rs.Epoch.ToString();

        public LengthOverBufferView? LengthOverBuffer => _lob;

        public MinRadiusView? MinRadius => _minRadius;

        public string? ClassName => _rs.Prototype?.ClassName;

        public string? RoadNumber => _rs.Prototype?.RoadNumber;

        public string? TypeName => _rs.Prototype?.TypeName;

        public string? Series => _rs.Prototype?.Series;

        public string? Couplers => _rs.Couplers?.ToString();

        public string? Livery => _rs.Livery;

        public string? Depot => _rs.Depot;

        public string? ServiceLevel => _rs.ServiceLevel?.ToString();

        public string? DccInterface => _rs.DccInterface.ToString();

        public string? Control => _rs.Control.ToString();
    }

    public sealed class LengthOverBufferView
    {
        private readonly LengthOverBuffer _lob;

        public LengthOverBufferView(LengthOverBuffer lob)
        {
            _lob = lob ??
                throw new ArgumentNullException(nameof(lob));
        }

        public decimal? Millimeters => decimal.Round(_lob.Millimeters.Value, 1);

        public decimal? Inches => decimal.Round(_lob.Inches.Value, 1);
    }

    public sealed class MinRadiusView
    {
        private readonly MinRadius _minRadius;

        public MinRadiusView(MinRadius minRadius)
        {
            _minRadius = minRadius;
        }

        public decimal? Millimeters => decimal.Round(_minRadius.Millimeters, 1);
    }
}
