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
        public CatalogItemsBuilder New() => new CatalogItemsBuilder();

        public IEnumerable<CatalogItem> All()
        {
            yield return Acme_60392();
            yield return Acme_60458();
            yield return Acme_999999();
            yield return Rivarossi_HR4298();
            yield return Bemo_1252125();
            yield return Bemo_1254134();
            yield return Roco_62182();
            yield return Roco_71934();
        }

        public CatalogItem Acme_60392()
        {
            return New()
                .Id(new Guid("bead1309-7daa-4eb4-8e50-05a0fd525f9b"))
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

        public CatalogItem Acme_60458()
        {
            return New()
                .Id(new Guid("04f06fed-d972-4aac-ac31-000889642b06"))
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

        public CatalogItem Rivarossi_HR4298()
        {
            return New()
                .Id(new Guid("f65d97a6-6dd0-46c8-a8e5-833f0f0757c6"))
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

        public CatalogItem Roco_62182()
        {
            return New()
                .Id(new Guid("c8458578-3032-416d-9d9d-9b9035a1515d"))
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

        public CatalogItem Bemo_1252125()
        {
            return New()
                .Id(new Guid("c5b1520f-78a1-4b14-b106-bb4cf07be2d3"))
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

        public CatalogItem Bemo_1254134()
        {
            return New()
                .Id(new Guid("0df689eb-cb5a-4813-9172-d156707c3651"))
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

        public CatalogItem Roco_71934()
        {
            return New()
                .Id(new Guid("b0da1567-157b-4f8d-9200-d79539720958"))
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

        public CatalogItem Acme_999999()
        {
            return New()
                .Id(new Guid("6dd6d484-2bc5-4a5d-8719-3b473ab7e471"))
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
