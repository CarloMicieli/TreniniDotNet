using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.DeliveryDates;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class CatalogItems
    {
        private readonly CatalogItem _acme_60458;
        private readonly CatalogItem _acme_60392;
        private readonly CatalogItem _rivarossi_HR4298;
        private readonly CatalogItem _roco_62182;
        private readonly CatalogItem _Bemo_1252125;
        private readonly CatalogItem _Bemo_1254134;
        private readonly CatalogItem _Acme_999999;
        private readonly CatalogItem _Roco_71934;

        private readonly IList<CatalogItem> _all;

        internal CatalogItems()
        {
            _acme_60392 = Build_Acme_60392();
            _acme_60458 = Build_Acme_60458();
            _rivarossi_HR4298 = Build_Rivarossi_HR4298();
            _roco_62182 = Build_Roco_62182();
            _Bemo_1252125 = Build_Bemo_1252125();
            _Bemo_1254134 = Build_Bemo_1254134();
            _Acme_999999 = Build_Acme_999999();
            _Roco_71934 = Build_Roco_71934();

            _all = new List<CatalogItem>()
            {
                _acme_60458,
                _acme_60392,
                _rivarossi_HR4298,
                _roco_62182,
                _Bemo_1252125,
                _Bemo_1254134,
                _Acme_999999,
                _Roco_71934
            };
        }

        public CatalogItemsBuilder New() => new CatalogItemsBuilder();

        public IList<CatalogItem> All() => _all;

        public CatalogItem Acme_60458() => _acme_60458;
        public CatalogItem Acme_60392() => _acme_60392;
        public CatalogItem Rivarossi_HR4298() => _rivarossi_HR4298;
        public CatalogItem Roco_62182() => _roco_62182;
        public CatalogItem Bemo_1252125() => _Bemo_1252125;
        public CatalogItem Bemo_1254134() => _Bemo_1254134;
        public CatalogItem Acme_999999() => _Acme_999999;
        public CatalogItem Roco_71934() => _Roco_71934;

        private CatalogItem Build_Acme_60392()
        {
            return New()
                .Id(Guid.NewGuid())
                .Brand(CatalogSeedData.Brands.Acme())
                .Scale(CatalogSeedData.Scales.ScaleH0())
                .ItemNumber(new ItemNumber("60392"))
                .PowerMethod(PowerMethod.DC)
                .Description("FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori")
                .DeliveryDate(DeliveryDate.FourthQuarterOf(2020))
                .Available()
                .Locomotive(lb => lb
                    .Id(Guid.NewGuid())
                    .Railway(CatalogSeedData.Railways.Fs())
                    .Category(Category.ElectricLocomotive)
                    .Epoch(Epoch.IV)
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(210M))
                    .Prototype(Prototype.OfLocomotive("E 656", "E 656 291"))
                    .DccInterface(DccInterface.Nem652)
                    .Control(Control.DccReady)
                    .MinRadius(MinRadius.OfMillimeters(360M))
                    .Livery("bianco/rosso/blu")
                    .Couplers(Couplers.Nem352)
                )
                .Build();
        }

        private CatalogItem Build_Acme_60458()
        {
            return New()
                .Id(Guid.NewGuid())
                .Brand(CatalogSeedData.Brands.Acme())
                .Scale(CatalogSeedData.Scales.ScaleH0())
                .ItemNumber(new ItemNumber("60458"))
                .PowerMethod(PowerMethod.DC)
                .Description(@"FS Locomotiva elettrica E 636 117 nella livrea storica blu orientale e grigio 
                    perla con vomere giallo, logo e scritta Trenitalia, nella fase di fine esercizio")
                .DeliveryDate(DeliveryDate.FourthQuarterOf(2020))
                .Unavailable()
                .Locomotive(lb => lb
                    .Id(Guid.NewGuid())
                    .Railway(CatalogSeedData.Railways.Fs())
                    .Category(Category.ElectricLocomotive)
                    .Epoch(Epoch.IV)
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(210M))
                    .Prototype(Prototype.OfLocomotive("E 636", "E 636 117"))
                    .DccInterface(DccInterface.Nem652)
                    .Control(Control.DccReady)
                )
                .Build();
        }

        private CatalogItem Build_Rivarossi_HR4298()
        {
            return New()
                .Id(Guid.NewGuid())
                .Brand(CatalogSeedData.Brands.Rivarossi())
                .Scale(CatalogSeedData.Scales.ScaleH0())
                .ItemNumber(new ItemNumber("HR4298"))
                .PowerMethod(PowerMethod.DC)
                .Description("FS set 2 carrozze a due assi tipo ''Corbellini'' livrea grigio ardesia di 2 cl.")
                .DeliveryDate(DeliveryDate.FirstQuarterOf(2020))
                .Available()
                .PassengerCar(pb => pb
                    .Id(Guid.NewGuid())
                    .Railway(CatalogSeedData.Railways.Fs())
                    .Category(Category.PassengerCar)
                    .Epoch(Epoch.IV)
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(195M))
                    .TypeName("Corbellini")
                    .ServiceLevel(ServiceLevel.SecondClass)
                    .PassengerCarType(PassengerCarType.OpenCoach)
                    .Livery("grigio ardesia")
                    .Couplers(Couplers.Nem352)
                )
                .Build();
        }

        private CatalogItem Build_Roco_62182()
        {
            return New()
                .Id(Guid.NewGuid())
                .Brand(CatalogSeedData.Brands.Roco())
                .Scale(CatalogSeedData.Scales.ScaleH0())
                .ItemNumber(new ItemNumber("62182"))
                .PowerMethod(PowerMethod.DC)
                .Description("Steam locomotive BR 50.40 of the Deutsche Reichsbahn.")
                .DeliveryDate(DeliveryDate.FirstQuarterOf(2020))
                .Available()
                .Locomotive(lb => lb
                    .Id(Guid.NewGuid())
                    .Railway(CatalogSeedData.Railways.DR())
                    .Category(Category.SteamLocomotive)
                    .Epoch(Epoch.III)
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(254M))
                    .Prototype(Prototype.OfLocomotive("Br 50", "Br 50.40"))
                    .DccInterface(DccInterface.Next18)
                    .Control(Control.DccReady)
                )
                .Build();
        }

        private CatalogItem Build_Bemo_1252125()
        {
            return New()
                .Id(Guid.NewGuid())
                .Brand(CatalogSeedData.Brands.Bemo())
                .Scale(CatalogSeedData.Scales.ScaleH0m())
                .ItemNumber(new ItemNumber("1252125"))
                .PowerMethod(PowerMethod.DC)
                .Description(@"Reissue of the Ge 4/4 I 601-610 in the conversion version after the 
                    modernization of the prototype, now with revised chassis.")
                .DeliveryDate(DeliveryDate.FirstQuarterOf(2020))
                .Available()
                .Locomotive(lb => lb
                    .Id(Guid.NewGuid())
                    .Railway(CatalogSeedData.Railways.RhB())
                    .Category(Category.ElectricLocomotive)
                    .Epoch(Epoch.V)
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(139M))
                    .Prototype(Prototype.OfLocomotive("Ge 4/4 I", "Ge 4/4 I 605"))
                    .DccInterface(DccInterface.Mtc21)
                    .Control(Control.DccReady)
                )
                .Build();
        }

        private CatalogItem Build_Bemo_1254134()
        {
            return New()
                .Id(Guid.NewGuid())
                .Brand(CatalogSeedData.Brands.Bemo())
                .Scale(CatalogSeedData.Scales.ScaleH0m())
                .ItemNumber(new ItemNumber("1254134"))
                .PowerMethod(PowerMethod.DC)
                .Description("Electric Locomotive Ge6/6 II of the RhB")
                .DeliveryDate(DeliveryDate.FirstQuarterOf(2020))
                .Available()
                .Locomotive(lb => lb
                    .Id(Guid.NewGuid())
                    .Railway(CatalogSeedData.Railways.RhB())
                    .Category(Category.ElectricLocomotive)
                    .Epoch(Epoch.V)
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(166.7M))
                    .Prototype(Prototype.OfLocomotive("Ge 6/6 II", "Ge 6/6 II 704 Davos"))
                    .DccInterface(DccInterface.Nem651)
                    .Control(Control.DccReady)
                )
                .Build();
        }

        private CatalogItem Build_Roco_71934()
        {
            return New()
                .Id(Guid.NewGuid())
                .Brand(CatalogSeedData.Brands.Roco())
                .Scale(CatalogSeedData.Scales.ScaleH0())
                .ItemNumber(new ItemNumber("71934"))
                .PowerMethod(PowerMethod.DC)
                .Description("Set di sette elementi del convoglio Diesel DMU BR 601 delle DB nella livrea d'origine.")
                .DeliveryDate(DeliveryDate.SecondQuarterOf(2020))
                .Available()
                .Train(tb => tb
                    .Id(Guid.NewGuid())
                    .Railway(CatalogSeedData.Railways.DieBahn())
                    .Epoch(Epoch.IV)
                    .Category(Category.ElectricMultipleUnit)
                    .DccInterface(DccInterface.Nem652)
                    .Control(Control.DccReady)
                    .TypeName("DMU BR 601")
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(1510))
                    .Build())
                .Build();
        }

        private CatalogItem Build_Acme_999999()
        {
            return New()
                .Id(Guid.NewGuid())
                .Brand(CatalogSeedData.Brands.Acme())
                .Scale(CatalogSeedData.Scales.ScaleH0())
                .ItemNumber(new ItemNumber("999999"))
                .PowerMethod(PowerMethod.DC)
                .Description("Catalog item - empty")
                .DeliveryDate(DeliveryDate.FirstQuarterOf(2020))
                .Available()
                .Build();
        }
    }

    public static class CatalogItemsRepositoryExtensions
    {
        public static async Task SeedDatabase(this ICatalogItemsRepository repo)
        {
            var catalogItems = CatalogSeedData.CatalogItems.All();
            foreach (var item in catalogItems)
            {
                await repo.AddAsync(item);
            }
        }
    }
}
