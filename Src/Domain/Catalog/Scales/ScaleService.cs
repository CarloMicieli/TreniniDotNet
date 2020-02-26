using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public sealed class ScaleService
    {
        private readonly IScalesRepository _scaleRepository;

        public ScaleService(IScalesRepository scaleRepository)
        {
            _scaleRepository = scaleRepository ??
                throw new ArgumentNullException(nameof(scaleRepository));
        }

        public async Task<ScaleId> CreateScale(string name, Slug slug, Ratio ratio, Gauge gauge, TrackGauge trackGauge, string? notes)
        {
            ScaleId scaleId = await _scaleRepository.Add(
                ScaleId.NewId(),
                slug,
                name, 
                ratio,
                gauge, 
                trackGauge,
                notes);
            return scaleId;
        }

        public Task<PaginatedResult<IScale>> FindAllScales(Page? page)
        {
            return _scaleRepository.GetScales(page ?? Page.Default);
        }

        public Task<List<IScale>> GetAll()
        {
            return _scaleRepository.GetAll();
        }

        public Task<IScale> GetBy(Slug slug)
        {
            return _scaleRepository.GetBy(slug);
        }

        public Task<bool> ScaleAlreadyExists(Slug slug)
        {
            return _scaleRepository.Exists(slug);
        }
    }
}
