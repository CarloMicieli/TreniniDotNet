using System;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.ViewModels
{
    public sealed class RollingStockView
    {
        private readonly RollingStock _rs;
        private readonly LengthOverBufferView? _lob;
        private readonly MinRadiusView? _minRadius;

        public RollingStockView(RollingStock rs)
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

        public Guid Id => _rs.Id;

        public RailwayInfoView Railway { get; }

        public string Category => _rs.Category.ToString();

        public string Epoch => _rs.Epoch.ToString();

        public LengthOverBufferView? LengthOverBuffer => _lob;

        public MinRadiusView? MinRadius => _minRadius;

        public string? ClassName => _rs switch
        {
            Locomotive { Prototype: var proto } => proto?.ClassName,
            _ => null
        };

        public string? RoadNumber => _rs switch
        {
            Locomotive { Prototype: var proto } => proto?.RoadNumber,
            _ => null
        };

        public string? Series => _rs switch
        {
            Locomotive { Prototype: var proto } => proto?.Series,
            _ => null
        };

        public string? TypeName => _rs switch
        {
            Train { TypeName: var typeName } => typeName,
            FreightCar { TypeName: var typeName } => typeName,
            PassengerCar { TypeName: var typeName } => typeName,
            _ => null
        };

        public string? Couplers => _rs.Couplers?.ToString();

        public string? Livery => _rs.Livery;

        public string? Depot => _rs switch
        {
            Locomotive { Depot: var depot } => depot,
            _ => null
        };

        public string? PassengerCarType => _rs switch
        {
            PassengerCar { PassengerCarType: var passengerCarType } => passengerCarType?.ToString(),
            _ => null
        };

        public string? ServiceLevel => _rs switch
        {
            PassengerCar { ServiceLevel: var level } => level?.ToString(),
            _ => null
        };

        public string? DccInterface => _rs switch
        {
            Locomotive { DccInterface: var dccInterface } => dccInterface.ToString(),
            Train { DccInterface: var dccInterface } => dccInterface.ToString(),
            _ => null
        };

        public string? Control => _rs switch
        {
            Locomotive { Control: var control } => control.ToString(),
            Train { Control: var control } => control.ToString(),
            _ => null
        };
    }

    public sealed class LengthOverBufferView
    {
        private LengthOverBuffer Length { get; }

        public LengthOverBufferView(LengthOverBuffer lengthOverBuffer)
        {
            Length = lengthOverBuffer ??
                throw new ArgumentNullException(nameof(lengthOverBuffer));
        }

        public decimal? Millimeters => decimal.Round(Length.Millimeters.Value, 1);

        public decimal? Inches => decimal.Round(Length.Inches.Value, 1);
    }

    public sealed class MinRadiusView
    {
        private MinRadius MinRadius { get; }

        public MinRadiusView(MinRadius minRadius)
        {
            MinRadius = minRadius;
        }

        public decimal? Millimeters => decimal.Round(MinRadius.Millimeters, 1);
    }
}
