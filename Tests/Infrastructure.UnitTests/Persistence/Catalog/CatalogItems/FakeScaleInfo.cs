using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public class FakeScaleInfo : IScaleInfo
    {
        private readonly ScaleId scaleId = new ScaleId(Guid.NewGuid());

        public ScaleId ScaleId => scaleId;

        public Slug Slug => Slug.Of("H0");

        public string Name => "H0";

        public Ratio Ratio => Ratio.Of(87M);
    }
}
