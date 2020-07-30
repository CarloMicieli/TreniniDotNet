using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwaysService : IDomainService
    {
        private readonly IRailwaysRepository _railwaysRepository;
        private readonly RailwaysFactory _railwaysFactory;

        public RailwaysService(IRailwaysRepository railwaysRepository, RailwaysFactory railwaysFactory)
        {
            _railwaysRepository = railwaysRepository ?? throw new ArgumentNullException(nameof(railwaysRepository));
            _railwaysFactory = railwaysFactory ?? throw new ArgumentNullException(nameof(railwaysFactory));
        }

        public Task<bool> RailwayAlreadyExists(Slug slug) =>
            _railwaysRepository.ExistsAsync(slug);

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
            var newRailway = _railwaysFactory.CreateRailway(
                name,
                companyName,
                country,
                periodOfActivity,
                railwayLength,
                gauge,
                websiteUrl,
                headquarters);
            return _railwaysRepository.AddAsync(newRailway);
        }

        public Task<Railway?> GetBySlugAsync(Slug slug) => _railwaysRepository.GetBySlugAsync(slug);

        public Task UpdateRailway(Railway railway) => _railwaysRepository.UpdateAsync(railway);

        public Task<PaginatedResult<Railway>> FindAllRailways(Page page) => _railwaysRepository.GetAllAsync(page);
    }
}
