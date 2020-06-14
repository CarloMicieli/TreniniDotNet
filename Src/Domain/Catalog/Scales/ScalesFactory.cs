using System;
using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScalesFactory : IScalesFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public ScalesFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));
            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }

        public IScale CreateNewScale(
            string name,
            Ratio ratio,
            ScaleGauge gauge,
            string? description,
            IImmutableSet<ScaleStandard> standards,
            int? weight)
        {
            return new Scale(
                new ScaleId(_guidSource.NewGuid()),
                name,
                Slug.Of(name),
                ratio,
                gauge,
                description,
                standards,
                weight,
                _clock.GetCurrentInstant(),
                null,
                1);
        }

        public IScale NewScale(Guid id,
            string name, string slug,
            decimal ratio,
            decimal? gaugeMm, decimal? gaugeIn, string trackType,
            string? description,
            int? weight,
            DateTime created,
            DateTime? modified,
            int? version)
        {
            var scaleGauge = ScaleGauge.Of(gaugeMm, gaugeIn, trackType);

            return new Scale(
                new ScaleId(id),
                name, Slug.Of(slug),
                Ratio.Of(ratio),
                scaleGauge,
                description,
                ImmutableHashSet<ScaleStandard>.Empty,
                weight,
                created.ToUtc(),
                modified.ToUtcOrDefault(),
                version ?? 1);
        }

        public IScale UpdateScale(IScale scale,
            string? name,
            Ratio? ratio,
            ScaleGauge? gauge,
            string? description,
            IImmutableSet<ScaleStandard>? standards,
            int? weight)
        {
            Slug slug = (name is null) ? scale.Slug : Slug.Of(name);

            return new Scale(
                scale.Id,
                name ?? scale.Name,
                slug,
                ratio ?? scale.Ratio,
                gauge ?? scale.Gauge,
                description ?? scale.Description,
                standards ?? scale.Standards,
                weight ?? scale.Weight,
                scale.CreatedDate,
                _clock.GetCurrentInstant(),
                scale.Version + 1);
        }
    }
}