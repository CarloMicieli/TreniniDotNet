using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class ScaleInfo : IScaleInfo
    {
        private readonly ScaleId scaleId;
        private readonly Slug slug;
        private readonly string name;
        private readonly Ratio ratio;

        public ScaleInfo(Guid scaleId, string slug, string name, decimal ratio)
            : this(new ScaleId(scaleId), Slug.Of(slug), name, Ratio.Of(ratio))
        {
        }

        public ScaleInfo(ScaleId scaleId, Slug slug, string name, Ratio ratio)
        {
            this.scaleId = scaleId;
            this.slug = slug;
            this.name = name;
            this.ratio = ratio;
        }

        public ScaleId ScaleId => scaleId;

        public Slug Slug => slug;

        public string Name => name;

        public Ratio Ratio => ratio;

        public IScaleInfo ToScaleInfo()
        {
            return this;
        }
    }
}
