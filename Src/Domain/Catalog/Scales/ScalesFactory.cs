using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public sealed class ScalesFactory : AggregateRootFactory<ScaleId, Scale>
    {
        public ScalesFactory(IClock clock, IGuidSource guidSource)
            : base(clock, guidSource)
        {
        }

        public Scale CreateScale(
            string name,
            Ratio ratio,
            ScaleGauge gauge,
            string? description,
            ISet<ScaleStandard> standards,
            int? weight)
        {
            return new Scale(
                NewId(id => new ScaleId(id)),
                name,
                Slug.Of(name),
                ratio,
                gauge,
                description,
                standards,
                weight,
                GetCurrentInstant(),
                null,
                1);
        }
    }
}
