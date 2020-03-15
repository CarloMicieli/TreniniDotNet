using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class CatalogItems
    {
        private readonly ICatalogItem _acme_60458;
        private readonly IList<ICatalogItem> _all;

        internal CatalogItems()
        {
            _acme_60458 = Build_Acme_60458();
            _all = new List<ICatalogItem>()
            {
                _acme_60458
            };
        }

        public ICollection<ICatalogItem> All() => _all;

        public ICatalogItem Acme_60458() => _acme_60458;

        private static ICatalogItem Build_Acme_60458()
        {
            var rollingStocks = new List<RollingStock>()
            {
                new RollingStock(
                    CatalogSeedData.Railways.Fs(),
                    Category.ElectricLocomotive,
                    Era.VI,
                    Length.OfMillimeters(210),
                    "E 636",
                    "E 636 117")
            };

            return new CatalogItem(
                CatalogItemId.NewId(),
                CatalogSeedData.Brands.Acme(),
                new ItemNumber("60458"),
                Slug.Of("acme", "60458"),
                CatalogSeedData.Scales.ScaleH0(),
                rollingStocks,
                PowerMethod.DC,
                @"FS Locomotiva elettrica E 636 117 nella livrea storica blu orientale e grigio 
                perla con vomere giallo, logo e scritta Trenitalia, nella fase di fine esercizio",
                null,
                null);
        }
    }
}
