using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class ScaleRef : AggregateRootRef<Scale, ScaleId>
    {
        public ScaleRef(ScaleId id, string slug, string name, decimal ratio) 
            : base(id, slug, $"{name} (1:{ratio})")
        {
        }

        public ScaleRef(Scale scale)
            : this(scale.Id, scale.Slug.ToString(), scale.Name, scale.Ratio.ToDecimal())
        {
        }

        public static ScaleRef? AsOptional(Scale? scale) =>
            (scale is null) ? null : new ScaleRef(scale);
    }
}