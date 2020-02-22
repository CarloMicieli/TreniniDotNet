using AutoMapper;
using TreniniDotNet.Domain.Catalog.ValueObjects;

using CatalogDomain = TreniniDotNet.Domain.Catalog;
using CatalogPersistence = TreniniDotNet.Infrastructure.Persistence.Catalog;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CatalogDomain.Brands.Brand, CatalogPersistence.Brands.Brand>()
                .ConstructUsing(b => new CatalogPersistence.Brands.Brand
                {
                    BrandId = b.BrandId.ToGuid(),
                    BrandKind = b.Kind.ToString(),
                    CompanyName = b.CompanyName,
                    Name = b.Name,
                    Slug = b.Slug.ToString()
                });

            CreateMap<CatalogDomain.Railways.Railway, CatalogPersistence.Railways.Railway>()
                .ConstructUsing(r => new CatalogPersistence.Railways.Railway
                {
                    RailwayId = r.RailwayId.ToGuid(),
                    Name = r.Name,
                    CompanyName = r.CompanyName
                });

            CreateMap<CatalogDomain.Scales.Scale, CatalogPersistence.Scales.Scale>()
                .ConstructUsing(s => new CatalogPersistence.Scales.Scale
                {
                    Gauge = s.Gauge.ToDecimal(MeasureUnit.Millimeters),
                    Name = s.Name,
                    Ratio = s.Ratio.ToDecimal(),
                    Notes = s.Notes,
                    ScaleId = s.ScaleId.ToGuid(),
                    Slug = s.Slug.ToString(),
                    TrackGauge = s.TrackGauge.ToString()
                });

        }
    }
}