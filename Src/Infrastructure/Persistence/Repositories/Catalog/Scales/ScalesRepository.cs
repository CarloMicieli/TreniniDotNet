using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.SharedKernel.Lengths;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Scales
{
    public sealed class ScalesRepository : IScalesRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScalesRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PaginatedResult<Scale>> GetScalesAsync(Page page)
        {
            var results = await _unitOfWork.QueryAsync<ScaleDto>(
                GetAllScalesWithPaginationQuery,
                new { skip = page.Start, limit = page.Limit + 1 });

            return new PaginatedResult<Scale>(
                page,
                results.Select(it => ProjectToDomain(it)!).ToList());
        }

        public async Task<ScaleId> AddAsync(Scale scale)
        {
            var _ = await _unitOfWork.ExecuteAsync(InsertScaleCommand, ToScaleDto(scale));
            return scale.Id;
        }

        public async Task UpdateAsync(Scale scale)
        {
            var _ = await _unitOfWork.ExecuteAsync(UpdateScaleCommand, ToScaleDto(scale));
        }

        private ScaleDto ToScaleDto(Scale scale) => new ScaleDto
        {
            scale_id = scale.Id.ToGuid(),
            name = scale.Name,
            slug = scale.Slug.Value,
            ratio = scale.Ratio.ToDecimal(),
            gauge_mm = scale.Gauge.InMillimeters.Value,
            gauge_in = scale.Gauge.InInches.Value,
            track_type = scale.Gauge.TrackGauge.ToString(),
            description = scale.Description,
            standards = string.Join(", ", scale.Standards.Select(it => it.ToString())),
            weight = scale.Weight,
            created = scale.CreatedDate.ToDateTimeUtc(),
            last_modified = scale.ModifiedDate?.ToDateTimeUtc(),
            version = scale.Version
        };

        public Task DeleteAsync(ScaleId id)
        {
            return _unitOfWork.ExecuteScalarAsync<string>(
                DeleteScaleCommand,
                new { scale_id = id.ToGuid() });
        }

        public async Task<bool> ExistsAsync(Slug slug)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<string>(
                GetScaleExistsQuery,
                new { slug = slug.ToString() });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<Scale?> GetBySlugAsync(Slug slug)
        {
            var result = await _unitOfWork.QueryFirstOrDefaultAsync<ScaleDto>(
                GetScaleBySlugQuery,
                new { slug = slug.ToString() });

            return ProjectToDomain(result);
        }

        private static Scale? ProjectToDomain(ScaleDto? dto)
        {
            if (dto is null)
            {
                return null;
            }

            var scaleGauge = ScaleGauge.Of(
                dto.gauge_mm,
                MeasureUnit.Millimeters,
                EnumHelpers.RequiredValueFor<TrackGauge>(dto.track_type));

            var standards = (dto.standards is null)
                ? new HashSet<ScaleStandard>()
                : dto.standards.Split(",")
                    .Where(it => string.IsNullOrWhiteSpace(it) == false)
                    .Select(EnumHelpers.RequiredValueFor<ScaleStandard>)
                    .ToHashSet();

            return new Scale(
                new ScaleId(dto.scale_id),
                dto.name,
                Slug.Of(dto.name),
                Ratio.Of(dto.ratio),
                scaleGauge,
                dto.description,
                standards,
                dto.weight,
                dto.created.ToUtc(),
                dto.last_modified.ToUtcOrDefault(),
                dto.version ?? 1);
        }

        #region [ Query / Command text ]

        private const string InsertScaleCommand = @"INSERT INTO scales(
	            scale_id, name, slug, ratio, gauge_mm, gauge_in, track_type, description, 
                   standards, weight, created, last_modified, version)
            VALUES(
                @scale_id, @name, @slug, @ratio, @gauge_mm, @gauge_in, @track_type, @description,
                   @standards, @weight, @created, @last_modified, @version);";

        private const string UpdateScaleCommand = @"UPDATE scales SET 
                name = @name, 
                slug = @slug, 
                ratio = @ratio, 
                gauge_mm = @gauge_mm, 
                gauge_in = @gauge_in, 
                track_type = @track_type, 
                description = @description,
                standards = @standards,
                weight = @weight, 
                last_modified = @last_modified, 
                version = @version
            WHERE scale_id = @scale_id;";

        private const string GetScaleExistsQuery = @"SELECT slug FROM scales WHERE slug = @slug LIMIT 1;";

        private const string GetScaleBySlugQuery = @"SELECT * FROM scales WHERE slug = @slug LIMIT 1;";

        private const string GetAllScalesWithPaginationQuery = @"SELECT * FROM scales ORDER BY name LIMIT @limit OFFSET @skip;";

        private const string DeleteScaleCommand = @"DELETE FROM scales WHERE scale_id = @scale_id";

        #endregion
    }
}