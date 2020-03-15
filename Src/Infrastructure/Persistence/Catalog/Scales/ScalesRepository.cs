using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    public sealed class ScalesRepository : IScalesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IScalesFactory _scalesFactory;

        public ScalesRepository(ApplicationDbContext context, IScalesFactory scalesFactory)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _scalesFactory = scalesFactory ??
                throw new ArgumentNullException(nameof(scalesFactory));
        }

        public Task<ScaleId> Add(
            ScaleId scaleId, 
            Slug slug, 
            string name, 
            Ratio ratio,
            Gauge gauge,
            TrackGauge trackGauge, 
            string? notes)
        {
            _context.Scales.Add(new Scale
            {
                ScaleId = scaleId.ToGuid(),
                Name = name,
                Slug = slug.ToString(),
                Gauge = gauge.ToDecimal(MeasureUnit.Millimeters),
                Ratio = ratio.ToDecimal(),
                Notes = notes,
                TrackGauge = trackGauge.ToString(),
                Version = 1,
                CreatedAt = DateTime.Now
            });

            return Task.FromResult(scaleId);
        }

        public Task<IScale> GetBy(Slug slug)
        {
            return _context.Scales
                .Where(s => s.Slug == slug.ToString())
                .Select(scale => _scalesFactory.NewScale(
                    scale.ScaleId,
                    scale.Name,
                    scale.Slug,
                    scale.Ratio,
                    scale.Gauge,
                    scale.TrackGauge,
                    scale.Notes))
                .SingleOrDefaultAsync();
        }

        public Task<List<IScale>> GetAll()
        {
            return _context.Scales
                .Select(scale => _scalesFactory.NewScale(
                    scale.ScaleId,
                    scale.Name,
                    scale.Slug,
                    scale.Ratio,
                    scale.Gauge,
                    scale.TrackGauge,
                    scale.Notes))
                .ToListAsync();
        }

        public Task<bool> Exists(Slug slug)
        {
            return _context.Scales
                .AnyAsync(s => s.Slug == slug.ToString());
        }

        public async Task<PaginatedResult<IScale>> GetScales(Page page)
        {
            var results = await _context.Scales
                .OrderBy(s => s.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .Select(scale => _scalesFactory.NewScale(
                    scale.ScaleId,
                    scale.Name,
                    scale.Slug,
                    scale.Ratio,
                    scale.Gauge,
                    scale.TrackGauge,
                    scale.Notes))
                .ToListAsync();

            return new PaginatedResult<IScale>(page, results);
        }

        public Task<IScale?> GetByName(string name)
        {
            return _context.Scales
                .Where(s => s.Name == name)
                .Select(scale => _scalesFactory.NewScale(
                    scale.ScaleId,
                    scale.Name,
                    scale.Slug,
                    scale.Ratio,
                    scale.Gauge,
                    scale.TrackGauge,
                    scale.Notes))
                .SingleOrDefaultAsync();
        }
    }
}