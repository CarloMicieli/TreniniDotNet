﻿using System;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public sealed class RollingStockView
    {
        private readonly IRollingStock _rs;
        private readonly LengthOverBufferView? _lob;

        internal RollingStockView(IRollingStock rs)
        {
            _rs = rs ??
                throw new ArgumentNullException(nameof(rs));

            Railway = new RailwayInfoView(rs.Railway);

            if (!(rs.Length is null))
            {
                _lob = new LengthOverBufferView(rs.Length);
            }
        }

        public Guid Id => _rs.RollingStockId.ToGuid();

        public RailwayInfoView Railway { get; }

        public string Category => _rs.Category.ToString();

        public string Era => _rs.Era.ToString();

        public LengthOverBufferView? LengthOverBuffer => _lob;

        public string? ClassName => _rs.ClassName;

        public string? RoadNumber => _rs.RoadNumber;

        public string? TypeName => _rs.TypeName;

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
}
