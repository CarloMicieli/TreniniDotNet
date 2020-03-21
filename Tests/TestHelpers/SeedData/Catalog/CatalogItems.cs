using System.Collections.Generic;
using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class CatalogItems
    {
        private readonly static ICatalogItemsFactory itemsFactory =
            new CatalogItemsFactory(SystemClock.Instance, new GuidSource());
        private readonly static IRollingStocksFactory rsFactory =
            new RollingStocksFactory(SystemClock.Instance, new GuidSource());

        private readonly ICatalogItem _acme_60458;
        private readonly ICatalogItem _acme_60392;
        private readonly ICatalogItem _rivarossi_HR4298;
        private readonly IList<ICatalogItem> _all;

        internal CatalogItems()
        {
            _acme_60392 = Build_Acme_60392();
            _acme_60458 = Build_Acme_60458();
            _rivarossi_HR4298 = Build_Rivarossi_HR4298();

            _all = new List<ICatalogItem>()
            {
                _acme_60458,
                _acme_60392,
                _rivarossi_HR4298
            };
        }

        public ICollection<ICatalogItem> All() => _all;

        public ICatalogItem Acme_60458() => _acme_60458;
        public ICatalogItem Acme_60392() => _acme_60392;
        public ICatalogItem Rivarossi_HR4298() => _rivarossi_HR4298;

        private static ICatalogItem Build_Acme_60392()
        {
            var rs = rsFactory.NewLocomotive(
                CatalogSeedData.Railways.Fs(),
                Era.IV.ToString(),
                Category.ElectricLocomotive.ToString(),
                210M,
                "E 656",
                "E 656 291",
                DccInterface.Nem652.ToString(),
                Control.DccReady.ToString());

            IRollingStock rollingStock = rs.IfFail(() => throw new System.InvalidOperationException());

            return itemsFactory.NewCatalogItem(
                CatalogSeedData.Brands.Acme(),
                "60392",
                CatalogSeedData.Scales.ScaleH0(),
                PowerMethod.DC.ToString(),
                "2020/Q4",
                false,
                @"FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori",
                null, null,
                rollingStock).IfFail(() => throw new System.InvalidOperationException());
        }

        private static ICatalogItem Build_Acme_60458()
        {
            var rs = rsFactory.NewLocomotive(
                CatalogSeedData.Railways.Fs(),
                Era.IV.ToString(),
                Category.ElectricLocomotive.ToString(),
                210M,
                "E 636",
                "E 636 117",
                DccInterface.Nem652.ToString(),
                Control.DccReady.ToString());

            IRollingStock rollingStock = rs.IfFail(() => throw new System.InvalidOperationException());

            return itemsFactory.NewCatalogItem(
                CatalogSeedData.Brands.Acme(),
                "60458",
                CatalogSeedData.Scales.ScaleH0(),
                PowerMethod.DC.ToString(),
                "2020/Q4",
                false,
                @"FS Locomotiva elettrica E 636 117 nella livrea storica blu orientale e grigio 
                perla con vomere giallo, logo e scritta Trenitalia, nella fase di fine esercizio",
                null, null,
                rollingStock).IfFail(() => throw new System.InvalidOperationException());
        }

        private static ICatalogItem Build_Rivarossi_HR4298()
        {
            var rs = rsFactory.NewRollingStock(
                CatalogSeedData.Railways.Fs(),
                Era.IV.ToString(),
                Category.PassengerCar.ToString(),
                195M,
                "Corbellini");

            IRollingStock rollingStock = rs.IfFail(() => throw new System.InvalidOperationException());

            return itemsFactory.NewCatalogItem(
                CatalogSeedData.Brands.Rivarossi(),
                "HR4298",
                CatalogSeedData.Scales.ScaleH0(),
                PowerMethod.DC.ToString(),
                "2020/Q4",
                false,
                @"FS Locomotiva elettrica E 636 117 nella livrea storica blu orientale e grigio 
                perla con vomere giallo, logo e scritta Trenitalia, nella fase di fine esercizio",
                null, null,
                ImmutableList.Create<IRollingStock>(rollingStock, rollingStock)).IfFail(() => throw new System.InvalidOperationException());
        }
    }
}
