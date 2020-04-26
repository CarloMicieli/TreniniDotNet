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

        public Task<IRailway?> GetBySlugAsync(Slug slug)
        {
            return _railwayRepository.GetBySlugAsync(slug);
        }

        public Task<PaginatedResult<IRailway>> FindAllRailways(Page? page)
        {
            return _railwayRepository.GetRailwaysAsync(page ?? Page.Default);
        }

        public Task<RailwayId> CreateRailway(
            string name,
            string? companyName,
            Country country,
            PeriodOfActivity periodOfActivity,
            RailwayLength? railwayLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters)
        {
            var newRailway = _railwaysFactory.CreateNewRailway(
                name,
                companyName,
                country,
                periodOfActivity,
                railwayLength,
                gauge,
                websiteUrl,
                headquarters);
            return _railwayRepository.AddAsync(newRailway);
        }

        public Task UpdateRailway(
            IRailway railway,
            string? name,
            string? companyName,
            Country? country,
            PeriodOfActivity? periodOfActivity,
            RailwayLength? railwayLength,
            RailwayGauge? railwayGauge,
            Uri? websiteUrl,
            string? headquarters)
        {
            var updated = _railwaysFactory.UpdateRailway(railway,
                name,
                companyName,
                country,
                periodOfActivity,
                railwayLength,
                railwayGauge,
                websiteUrl,
                headquarters);

            return _railwayRepository.UpdateAsync(updated);
        }
    }
}
