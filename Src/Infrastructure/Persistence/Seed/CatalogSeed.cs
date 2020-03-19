using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence.Seed.CsvRecords;

namespace TreniniDotNet.Infrastructure.Persistence.Seed
{
    public static class CatalogSeed
    {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
        public static async Task InitDatabase(IServiceProvider scopedServices, ILogger logger)
        {
            IBrandsRepository brands = scopedServices.GetRequiredService<IBrandsRepository>();
            IRailwaysRepository railways = scopedServices.GetRequiredService<IRailwaysRepository>();
            IScalesRepository scales = scopedServices.GetRequiredService<IScalesRepository>();

            await SeedBrands(brands, logger);
            await SeedRailways(railways, logger);
            await SeedScales(scales, logger);
        }

        private static async Task SeedBrands(IBrandsRepository brandsRepo, ILogger logger)
        {
            var brandsFactory = new BrandsFactory();
            var brandRecords = CsvLoader.LoadRecords<BrandRecord>(@"..\..\Resources\brands.csv");
            logger.LogInformation("{0} brand(s) found", brandRecords.Count());

            bool alreadyRun = await brandsRepo.Exists(Slug.Of(brandRecords.First().Slug));
            if (alreadyRun)
            {
                logger.LogInformation("Catalog seed already run for brands - skipped");
                return;
            }

            foreach (var br in brandRecords)
            {
                var brand = brandsFactory.NewBrand(
                    br.BrandId,
                    br.Name,
                    br.Slug,
                    br.CompanyName,
                    br.WebsiteUrl,
                    br.MailAddress == null ? null : br.MailAddress?.Replace("[AT]", "@", StringComparison.InvariantCultureIgnoreCase),
                    br.BrandKind); ;

                var brandId = await brandsRepo.Add(brand);
                logger.LogInformation("Inserted brand {0} (id = {1})", br.Name, brandId);
            }
        }

        private static async Task SeedScales(IScalesRepository scalesRepo, ILogger logger)
        {
            var scalesFactory = new ScalesFactory();
            var scalesRecords = CsvLoader.LoadRecords<ScaleRecord>(@"..\..\Resources\scales.csv");
            logger.LogInformation("{0} scale(s) found", scalesRecords.Count());

            bool alreadyRun = await scalesRepo.Exists(Slug.Of(scalesRecords.First().Slug));
            if (alreadyRun)
            {
                logger.LogInformation("Catalog seed already run for scales - skipped");
                return;
            }

            foreach (var sr in scalesRecords)
            {
                var scale = scalesFactory.NewScale(
                    sr.ScaleId,
                    sr.Name,
                    sr.Slug,
                    sr.Ratio,
                    sr.Gauge,
                    sr.TrackGauge,
                    null
                    );

                var scaleId = await scalesRepo.Add(scale!);
                logger.LogInformation("Inserted scale {0} (id = {1})", sr.Name, scaleId);
            }
        }

        private static async Task SeedRailways(IRailwaysRepository railwaysRepo, ILogger logger)
        {
            var railwaysFactory = new Domain.Catalog.Railways.RailwaysFactory();
            var railwayRecords = CsvLoader.LoadRecords<RailwayRecord>(@"..\..\Resources\railways.csv");
            logger.LogInformation("{0} railway(s) found", railwayRecords.Count());

            bool alreadyRun = await railwaysRepo.Exists(Slug.Of(railwayRecords.First().Slug));
            if (alreadyRun)
            {
                logger.LogInformation("Catalog seed already run for railways - skipped");
                return;
            }

            foreach (var r in railwayRecords)
            {
                var railway = railwaysFactory.NewRailway(
                    r.RailwayId,
                    r.Name,
                    r.Slug,
                    r.CompanyName,
                    r.Country,
                    null, //r.OperatingSince,
                    null, ///r.OperatingUntil,
                    true, //TODO: fixme
                    DateTime.UtcNow,
                    1);

                var railwayId = await railwaysRepo.Add(railway!);
                logger.LogInformation("Inserted railway {0} (id = {1})", r.Name, railwayId);
            }
        }
#pragma warning restore CA1303 // Do not pass literals as localized parameters
    }
}
