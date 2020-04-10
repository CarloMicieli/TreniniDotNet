using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;
using System.Collections.Immutable;

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
            _scalesFactory = scalesFactory;
        }

        public Task<ScaleId> CreateScale(
            string name, Slug slug,
            Ratio ratio,
            ScaleGauge gauge,
            string? description,
            ImmutableHashSet<ScaleStandard> standards,
            int? weight)
        {
            var newScale = _scalesFactory.NewScale(ScaleId.NewId(), name, slug, ratio, gauge, description, standards, weight);
            return _scaleRepository.Add(newScale);
        }

        public Task<PaginatedResult<IScale>> FindAllScales(Page? page)
        {
            return _scaleRepository.GetScales(page ?? Page.Default);
        }

        public Task<List<IScale>> GetAll()
        {
            return _scaleRepository.GetAll();
        }

        public Task<IScale?> GetBy(Slug slug)
        {
            return _scaleRepository.GetBySlug(slug);
        }

        public Task<bool> ScaleAlreadyExists(Slug slug)
        {
            return _scaleRepository.Exists(slug);
        }
    }
}
