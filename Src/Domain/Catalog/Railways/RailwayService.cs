using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwayService
    {
        private readonly IRailwaysRepository _railwayRepository;
        private readonly IRailwaysFactory _railwaysFactory;

        public RailwayService(IRailwaysRepository railwayRepository, IRailwaysFactory railwaysFactory)
        {
            _railwayRepository = railwayRepository ??
                throw new ArgumentNullException(nameof(railwayRepository));
            _railwaysFactory = railwaysFactory ??
                throw new ArgumentNullException(nameof(railwaysFactory));
        }

        public Task<bool> RailwayAlreadyExists(Slug slug)
        {
            return _railwayRepository.ExistsAsync(slug);
        }

        public Task<IRailway?> GetBy(Slug slug)
        {
            return _railwayRepository.GetBySlugAsync(slug);
        }

        public Task<PaginatedResult<IRailway>> FindAllRailways(Page? page)
        {
            return _railwayRepository.GetRailwaysAsync(page ?? Page.Default);
        }

        public Task<RailwayId> CreateRailway(
            string name,
            Slug slug, string? companyName,
            Country country,
            PeriodOfActivity periodOfActivity,
            RailwayLength? railwayLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters)
        {
            var id = RailwayId.NewId();

            var newRailway = _railwaysFactory.NewRailway(id,
                name, slug,
                companyName,
                country,
                periodOfActivity,
                railwayLength,
                gauge,
                websiteUrl,
                headquarters);
            return _railwayRepository.AddAsync(newRailway);
        }
    }
}
