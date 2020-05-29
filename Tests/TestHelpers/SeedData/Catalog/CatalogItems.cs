using System.Collections.Generic;
using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using static TreniniDotNet.TestHelpers.SeedData.Catalog.CatalogSeedData;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class CatalogItems
    {
        private static readonly ICatalogItemsFactory factory =
            new CatalogItemsFactory(SystemClock.Instance, new GuidSource());

        private readonly ICatalogItem _acme_60458;
        private readonly ICatalogItem _acme_60392;
        private readonly ICatalogItem _rivarossi_HR4298;
        private readonly ICatalogItem _roco_62182;
        private readonly ICatalogItem _Bemo_1252125;
        private readonly ICatalogItem _Bemo_1254134;
        private readonly ICatalogItem _Acme_999999;

        private readonly IList<ICatalogItem> _all;

        internal CatalogItems()
        {
            _acme_60392 = Build_Acme_60392();
            _acme_60458 = Build_Acme_60458();
            _rivarossi_HR4298 = Build_Rivarossi_HR4298();
            _roco_62182 = Build_Roco_62182();
            _Bemo_1252125 = Build_Bemo_1252125();
            _Bemo_1254134 = Build_Bemo_1254134();
            _Acme_999999 = Build_Acme_999999();

            _all = new List<ICatalogItem>()
            {
                _acme_60458,
                _acme_60392,
                _rivarossi_HR4298,
                _roco_62182,
                _Bemo_1252125,
                _Bemo_1254134,
                _Acme_999999
            };
        }

        public IList<ICatalogItem> All() => _all;

        public ICatalogItem Acme_60458() => _acme_60458;
        public ICatalogItem Acme_60392() => _acme_60392;
        public ICatalogItem Rivarossi_HR4298() => _rivarossi_HR4298;
        public ICatalogItem Roco_62182() => _roco_62182;
        public ICatalogItem Bemo_1252125() => _Bemo_1252125;
        public ICatalogItem Bemo_1254134() => _Bemo_1254134;
        public ICatalogItem Acme_999999() => _Acme_999999;

        private static ICatalogItem Build_Acme_60392()
        {
            var rs = NewRollingStockWith(
                RollingStockId.NewId(),
                CatalogSeedData.Railways.Fs(),
                Category.ElectricLocomotive,
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(210M),
                "E 656",
                "E 656 291",
                dccInterface: DccInterface.Nem652,
                control: Control.DccReady,
                livery: "bianco/rosso/blu",
                couplers: Couplers.Nem352);

            return NewCatalogItemWith(
                CatalogItemId.NewId(),
                CatalogSeedData.Brands.Acme(),
                CatalogSeedData.Scales.ScaleH0(),
                new ItemNumber("60392"),
                PowerMethod.DC,
                @"FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori",
                rollingStocks: ImmutableList.Create<IRollingStock>(rs),
                deliveryDate: DeliveryDate.FourthQuarterOf(2020),
                available: false);
        }

        private static ICatalogItem Build_Acme_60458()
        {
            var rs = NewRollingStockWith(
                RollingStockId.NewId(),
                CatalogSeedData.Railways.Fs(),
                Category.ElectricLocomotive,
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(210M),
                "E 636",
                "E 636 117",
                dccInterface: DccInterface.Nem652,
                control: Control.DccReady);

            return NewCatalogItemWith(
                CatalogItemId.NewId(),
                CatalogSeedData.Brands.Acme(),
                CatalogSeedData.Scales.ScaleH0(),
                new ItemNumber("60458"),
                PowerMethod.DC,
                @"FS Locomotiva elettrica E 636 117 nella livrea storica blu orientale e grigio 
                perla con vomere giallo, logo e scritta Trenitalia, nella fase di fine esercizio",
                rollingStocks: ImmutableList.Create<IRollingStock>(rs),
                deliveryDate: DeliveryDate.FourthQuarterOf(2020),
                available: false);
        }

        private static ICatalogItem Build_Rivarossi_HR4298()
        {
            var rs = NewRollingStockWith(
                RollingStockId.NewId(),
                CatalogSeedData.Railways.Fs(),
                Category.PassengerCar,
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(195M),
                typeName: "Corbellini",
                livery: "grigio ardesia",
                couplers: Couplers.Nem352);

            return NewCatalogItemWith(
                CatalogItemId.NewId(),
                CatalogSeedData.Brands.Rivarossi(),
                CatalogSeedData.Scales.ScaleH0(),
                new ItemNumber("HR4298"),
                PowerMethod.DC,
                @"FS set 2 carrozze a due assi tipo ''Corbellini'' livrea grigio ardesia di 2 cl.",
                rollingStocks: ImmutableList.Create<IRollingStock>(rs, rs),
                deliveryDate: DeliveryDate.FirstQuarterOf(2020),
                available: true);
        }

        private static ICatalogItem Build_Roco_62182()
        {
            var rs = NewRollingStockWith(
                RollingStockId.NewId(),
                CatalogSeedData.Railways.DR(),
                Category.SteamLocomotive,
                Epoch.III,
                LengthOverBuffer.OfMillimeters(254M),
                "BR 50.40",
                dccInterface: DccInterface.Next18,
                control: Control.DccReady);

            return NewCatalogItemWith(
                CatalogItemId.NewId(),
                CatalogSeedData.Brands.Roco(),
                CatalogSeedData.Scales.ScaleH0(),
                new ItemNumber("62182"),
                PowerMethod.DC,
                @"Steam locomotive BR 50.40 of the Deutsche Reichsbahn.",
                rollingStocks: ImmutableList.Create(rs),
                deliveryDate: DeliveryDate.FirstQuarterOf(2020),
                available: true);
        }

        private static ICatalogItem Build_Bemo_1252125()
        {
            var rs = NewRollingStockWith(
                RollingStockId.NewId(),
                CatalogSeedData.Railways.RhB(),
                Category.ElectricLocomotive,
                Epoch.V,
                LengthOverBuffer.OfMillimeters(139M),
                "Ge 4/4 I",
                "Ge 4/4 I 605",
                dccInterface: DccInterface.Mtc21,
                control: Control.DccReady);

            return NewCatalogItemWith(
                CatalogItemId.NewId(),
                CatalogSeedData.Brands.Bemo(),
                CatalogSeedData.Scales.ScaleH0m(),
                new ItemNumber("1252125"),
                PowerMethod.DC,
                @"Reissue of the Ge 4/4 I 601-610 in the conversion version after the 
                modernization of the prototype, now with revised chassis.",
                rollingStocks: ImmutableList.Create(rs),
                deliveryDate: DeliveryDate.FirstQuarterOf(2020),
                available: true);
        }

        private static ICatalogItem Build_Bemo_1254134()
        {
            var rs = NewRollingStockWith(
                RollingStockId.NewId(),
                CatalogSeedData.Railways.RhB(),
                Category.ElectricLocomotive,
                Epoch.V,
                LengthOverBuffer.OfMillimeters(166.7M),
                "Ge 6/6 II",
                "Ge 6/6 II 704 Davos",
                dccInterface: DccInterface.Nem651,
                control: Control.DccReady);

            return NewCatalogItemWith(
                CatalogItemId.NewId(),
                CatalogSeedData.Brands.Bemo(),
                CatalogSeedData.Scales.ScaleH0m(),
                new ItemNumber("1254134"),
                PowerMethod.DC,
                @"Electric Locomotive Ge6/6 II of the RhB",
                rollingStocks: ImmutableList.Create(rs),
                deliveryDate: DeliveryDate.FirstQuarterOf(2020),
                available: true);
        }

        private static ICatalogItem Build_Acme_999999()
        {
            return NewCatalogItemWith(
                CatalogItemId.NewId(),
                CatalogSeedData.Brands.Acme(),
                CatalogSeedData.Scales.ScaleH0(),
                new ItemNumber("999999"),
                PowerMethod.DC,
                "Catalog item - empty",
                rollingStocks: ImmutableList<IRollingStock>.Empty,
                deliveryDate: DeliveryDate.FirstQuarterOf(2020),
                available: true);
        }
    }

    public static class CatalogItemsRepositoryExtensions
    {
        public static void SeedDatabase(this ICatalogItemRepository repo)
        {
            var catalogItems = CatalogSeedData.CatalogItems.All();
            foreach (var item in catalogItems)
            {
                repo.AddAsync(item);
            }
        }
    }
}
