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
        public CatalogItem Acme60458 { get; }
        public CatalogItem Acme60392 { get; }
        public CatalogItem RivarossiHr4298 { get; }
        public CatalogItem Roco62182 { get; }
        public CatalogItem Bemo1252125 { get; }
        public CatalogItem Bemo1254134 { get; }
        public CatalogItem Acme999999 { get; }
        public CatalogItem Roco71934 { get; }

        public CatalogItemsBuilder New() => new CatalogItemsBuilder();

        internal CatalogItems()
        {
            Acme60392 = NewAcme60392();
            Acme60458 = NewAcme60458();
            RivarossiHr4298 = NewRivarossiHR4298();
            Roco62182 = NewRoco62182();
            Bemo1252125 = NewBemo1252125();
            Bemo1254134 = NewBemo1254134();
            Acme999999 = NewAcme999999();
            Roco71934 = NewRoco71934();
        }

        public IEnumerable<CatalogItem> All()
        {
            yield return Acme60458;
            yield return Acme60392;
            yield return RivarossiHr4298;
            yield return Roco62182;
            yield return Bemo1252125;
            yield return Bemo1254134;
            yield return Acme999999;
            yield return Roco71934;
        }

        public List<CatalogItem> NewList() =>
            new List<CatalogItem>
            {
                NewAcme60458(),
                NewAcme60392(),
                NewRivarossiHR4298(),
                NewRoco62182(),
                NewBemo1252125(),
                NewBemo1254134(),
                NewAcme999999(),
                NewRoco71934()
           };

        public CatalogItem NewAcme60392()
        {
            return New()
                .Id(new Guid("bead1309-7daa-4eb4-8e50-05a0fd525f9b"))
                .Brand(CatalogSeedData.Brands.Acme)
                .Scale(CatalogSeedData.Scales.H0)
                .ItemNumber(new ItemNumber("60392"))
                .PowerMethod(PowerMethod.DC)
                .Description("FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori")
                .DeliveryDate(DeliveryDate.FourthQuarterOf(2020))
                .Available()
                .Locomotive(lb => lb
                    .Id(new Guid("0133a009-51e2-4cc0-a459-7e5c8b83a653"))
                    .Railway(CatalogSeedData.Railways.Fs)
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

        public CatalogItem NewAcme60458()
        {
            return New()
                .Id(new Guid("04f06fed-d972-4aac-ac31-000889642b06"))
                .Brand(CatalogSeedData.Brands.Acme)
                .Scale(CatalogSeedData.Scales.H0)
                .ItemNumber(new ItemNumber("60458"))
                .PowerMethod(PowerMethod.DC)
                .Description(@"FS Locomotiva elettrica E 636 117 nella livrea storica blu orientale e grigio 
                    perla con vomere giallo, logo e scritta Trenitalia, nella fase di fine esercizio")
                .DeliveryDate(DeliveryDate.FourthQuarterOf(2020))
                .Unavailable()
                .Locomotive(lb => lb
                    .Id(new Guid("740af440-b189-47c7-a071-a2c1d6641e66"))
                    .Railway(CatalogSeedData.Railways.Fs)
                    .Category(Category.ElectricLocomotive)
                    .Epoch(Epoch.IV)
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(210M))
                    .Prototype(Prototype.OfLocomotive("E 636", "E 636 117"))
                    .DccInterface(DccInterface.Nem652)
                    .Control(Control.DccReady)
                )
                .Build();
        }

        public CatalogItem NewRivarossiHR4298()
        {
            return New()
                .Id(new Guid("f65d97a6-6dd0-46c8-a8e5-833f0f0757c6"))
                .Brand(CatalogSeedData.Brands.Rivarossi)
                .Scale(CatalogSeedData.Scales.H0)
                .ItemNumber(new ItemNumber("HR4298"))
                .PowerMethod(PowerMethod.DC)
                .Description("FS set 2 carrozze a due assi tipo ''Corbellini'' livrea grigio ardesia di 2 cl.")
                .DeliveryDate(DeliveryDate.FirstQuarterOf(2020))
                .Available()
                .PassengerCar(pb => pb
                    .Id(new Guid("a7552253-0cce-4af5-aa0c-61d6ef3115ad"))
                    .Railway(CatalogSeedData.Railways.Fs)
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

        public CatalogItem NewRoco62182()
        {
            return New()
                .Id(new Guid("c8458578-3032-416d-9d9d-9b9035a1515d"))
                .Brand(CatalogSeedData.Brands.Roco)
                .Scale(CatalogSeedData.Scales.H0)
                .ItemNumber(new ItemNumber("62182"))
                .PowerMethod(PowerMethod.DC)
                .Description("Steam locomotive BR 50.40 of the Deutsche Reichsbahn.")
                .DeliveryDate(DeliveryDate.FirstQuarterOf(2020))
                .Available()
                .Locomotive(lb => lb
                    .Id(new Guid("e7a46ec3-17e6-4250-a772-b11870d94477"))
                    .Railway(CatalogSeedData.Railways.Dr)
                    .Category(Category.SteamLocomotive)
                    .Epoch(Epoch.III)
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(254M))
                    .Prototype(Prototype.OfLocomotive("Br 50", "Br 50.40"))
                    .DccInterface(DccInterface.Next18)
                    .Control(Control.DccReady)
                )
                .Build();
        }

        public CatalogItem NewBemo1252125()
        {
            return New()
                .Id(new Guid("c5b1520f-78a1-4b14-b106-bb4cf07be2d3"))
                .Brand(CatalogSeedData.Brands.Bemo)
                .Scale(CatalogSeedData.Scales.H0m)
                .ItemNumber(new ItemNumber("1252125"))
                .PowerMethod(PowerMethod.DC)
                .Description(@"Reissue of the Ge 4/4 I 601-610 in the conversion version after the 
                    modernization of the prototype, now with revised chassis.")
                .DeliveryDate(DeliveryDate.FirstQuarterOf(2020))
                .Available()
                .Locomotive(lb => lb
                    .Id(new Guid("920e7922-b837-48ba-8384-07d615a11329"))
                    .Railway(CatalogSeedData.Railways.RhB)
                    .Category(Category.ElectricLocomotive)
                    .Epoch(Epoch.V)
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(139M))
                    .Prototype(Prototype.OfLocomotive("Ge 4/4 I", "Ge 4/4 I 605"))
                    .DccInterface(DccInterface.Mtc21)
                    .Control(Control.DccReady)
                )
                .Build();
        }

        public CatalogItem NewBemo1254134()
        {
            return New()
                .Id(new Guid("0df689eb-cb5a-4813-9172-d156707c3651"))
                .Brand(CatalogSeedData.Brands.Bemo)
                .Scale(CatalogSeedData.Scales.H0m)
                .ItemNumber(new ItemNumber("1254134"))
                .PowerMethod(PowerMethod.DC)
                .Description("Electric Locomotive Ge6/6 II of the RhB")
                .DeliveryDate(DeliveryDate.FirstQuarterOf(2020))
                .Available()
                .Locomotive(lb => lb
                    .Id(new Guid("68f1bef3-a47b-4cf3-8c97-1dc605a264e6"))
                    .Railway(CatalogSeedData.Railways.RhB)
                    .Category(Category.ElectricLocomotive)
                    .Epoch(Epoch.V)
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(166.7M))
                    .Prototype(Prototype.OfLocomotive("Ge 6/6 II", "Ge 6/6 II 704 Davos"))
                    .DccInterface(DccInterface.Nem651)
                    .Control(Control.DccReady)
                )
                .Build();
        }

        public CatalogItem NewRoco71934()
        {
            return New()
                .Id(new Guid("b0da1567-157b-4f8d-9200-d79539720958"))
                .Brand(CatalogSeedData.Brands.Roco)
                .Scale(CatalogSeedData.Scales.H0)
                .ItemNumber(new ItemNumber("71934"))
                .PowerMethod(PowerMethod.DC)
                .Description("Set di sette elementi del convoglio Diesel DMU BR 601 delle DB nella livrea d'origine.")
                .DeliveryDate(DeliveryDate.SecondQuarterOf(2020))
                .Available()
                .Train(tb => tb
                    .Id(new Guid("39789fd9-721b-4699-8358-20bb331afa19"))
                    .Railway(CatalogSeedData.Railways.DieBahn)
                    .Epoch(Epoch.IV)
                    .Category(Category.ElectricMultipleUnit)
                    .DccInterface(DccInterface.Nem652)
                    .Control(Control.DccReady)
                    .TypeName("DMU BR 601")
                    .LengthOverBuffer(LengthOverBuffer.OfMillimeters(1510))
                    .Build())
                .Build();
        }

        public CatalogItem NewAcme999999()
        {
            return New()
                .Id(new Guid("6dd6d484-2bc5-4a5d-8719-3b473ab7e471"))
                .Brand(CatalogSeedData.Brands.Acme)
                .Scale(CatalogSeedData.Scales.H0)
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
