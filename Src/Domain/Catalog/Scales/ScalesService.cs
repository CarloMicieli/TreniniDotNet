using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public sealed class ScalesService : IDomainService
    {
        private readonly IScalesRepository _scalesRepository;
        private readonly ScalesFactory _scalesFactory;

        public ScalesService(IScalesRepository scalesRepository, ScalesFactory scalesFactory)
        {
            _scalesRepository = scalesRepository ?? throw new ArgumentNullException(nameof(scalesRepository));
            _scalesFactory = scalesFactory ?? throw new ArgumentNullException(nameof(scalesFactory));
        }

        public Task<bool> ScaleAlreadyExists(Slug slug) => _scalesRepository.ExistsAsync(slug);

        public Task<ScaleId> CreateScale(
            string name,
            Ratio ratio,
            ScaleGauge gauge,
            string? description,
            ISet<ScaleStandard> standards,
            int? weight)
        {
            var newScale = _scalesFactory.CreateScale(name, ratio, gauge, description, standards, weight);
            return _scalesRepository.AddAsync(newScale);
        }

        public Task<Scale?> GetBySlugAsync(Slug slug) => _scalesRepository.GetBySlugAsync(slug);

        public Task UpdateScaleAsync(Scale scale) => _scalesRepository.UpdateAsync(scale);

        public Task<PaginatedResult<Scale>> FindAllScales(Page page) => _scalesRepository.GetScalesAsync(page);
    }
}
