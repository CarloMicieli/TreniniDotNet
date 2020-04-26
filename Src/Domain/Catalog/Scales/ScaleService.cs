using System;
using System.Threading.Tasks;
using System.Collections.Immutable;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public sealed class ScaleService
    {
        private readonly IScalesRepository _scaleRepository;
        private readonly IScalesFactory _scalesFactory;

        public ScaleService(IScalesRepository scaleRepository, IScalesFactory scalesFactory)
        {
            _scaleRepository = scaleRepository ??
                throw new ArgumentNullException(nameof(scaleRepository));
            _scalesFactory = scalesFactory ??
                throw new ArgumentNullException(nameof(scalesFactory));
        }

        public Task<ScaleId> CreateScale(
            string name,
            Ratio ratio,
            ScaleGauge gauge,
            string? description,
            ImmutableHashSet<ScaleStandard> standards,
            int? weight)
        {
            var newScale = _scalesFactory.CreateNewScale(name, ratio, gauge, description, standards, weight);
            return _scaleRepository.AddAsync(newScale);
        }

        public Task<PaginatedResult<IScale>> FindAllScales(Page? page)
        {
            return _scaleRepository.GetScalesAsync(page ?? Page.Default);
        }

        public Task<IScale?> GetBySlugAsync(Slug slug)
        {
            return _scaleRepository.GetBySlugAsync(slug);
        }

        public Task UpdateScale(IScale scale,
            string? name,
            Ratio? ratio,
            ScaleGauge? gauge,
            string? description,
            ImmutableHashSet<ScaleStandard> standards,
            int? weight)
        {
            var updated = _scalesFactory.UpdateScale(
                scale,
                name,
                ratio,
                gauge,
                description,
                standards,
                weight);
            return _scaleRepository.UpdateAsync(updated);
        }

        public Task<bool> ScaleAlreadyExists(Slug slug)
        {
            return _scaleRepository.ExistsAsync(slug);
        }
    }
}
