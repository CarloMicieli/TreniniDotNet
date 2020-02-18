using AutoMapper;
using DomainScale = TreniniDotNet.Domain.Catalog.Scales.Scale;
using DbScale = TreniniDotNet.Infrastructure.Persistence.Catalog.Scales.Scale;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<DomainScale, DbScale>()
                .ConstructUsing(s => new DbScale {
                    Gauge = s.Gauge,
                    Name = s.Name,
                    Ratio = s.Ratio,
                    Notes = s.Notes,
                    ScaleId = s.ScaleId,
                    Slug = s.Slug,
                    TrackGauge = s.TrackGauge
                });
        }
    }
}