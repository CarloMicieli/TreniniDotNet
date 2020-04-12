using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NodaTime;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Infrastructure.Persistence.Seed.CsvRecords;

namespace TreniniDotNet.Infrastructure.Persistence.Seed
{
    public static class CatalogSeed
    {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
#pragma warning disable CA1031 // Do not catch general exception types
        public static async Task InitDatabase(IServiceProvider scopedServices, ILogger logger)
        {
            IBrandsRepository brands = scopedServices.GetRequiredService<IBrandsRepository>();
            IRailwaysRepository railways = scopedServices.GetRequiredService<IRailwaysRepository>();
            IScalesRepository scales = scopedServices.GetRequiredService<IScalesRepository>();

            IClock clock = SystemClock.Instance;
            IGuidSource guidSource = new GuidSource();

            var brandsFactory = new BrandsFactory(clock, guidSource);
            var railwaysFactory = new RailwaysFactory(clock, guidSource);
            var scalesFactory = new ScalesFactory(clock, guidSource);

            await SeedBrands(brands, logger, brandsFactory);
            await SeedRailways(railways, logger, railwaysFactory);
            await SeedScales(scales, logger, scalesFactory);
        }

        private static async Task SeedBrands(IBrandsRepository brandsRepo, ILogger logger, IBrandsFactory brandsFactory)
        {
            var brandRecords = CsvLoader.LoadRecords<BrandRecord>(@"..\..\Resources\brands.csv");
            logger.LogInformation("Found {0} brand(s) to seed", brandRecords.Count());

            var first = brandRecords
                .Where(it => string.IsNullOrEmpty(it.Slug) == false)
                .Select(it => Slug.Of(it.Slug))
                .First();

            bool alreadyRun = await brandsRepo.ExistsAsync(first);
            if (alreadyRun)
            {
                logger.LogInformation("Catalog seed already run for brands - skipped");
                return;
            }

            foreach (var brand in brandRecords)
            {
                try
                {
                    var slug = Slug.Of(brand.Slug);

                    var newBrand = brandsFactory.NewBrandWith(
                        brandId: new BrandId(brand.BrandId),
                        name: brand.Name,
                        slug: Slug.Of(brand.Slug),
                        companyName: brand.CompanyName,
                        website: !string.IsNullOrWhiteSpace(brand.WebsiteUrl) ? new Uri(brand.WebsiteUrl) : null,
                        mailAddress: brand.MailAddress == null ? null : new MailAddress(brand.MailAddress?.Replace("[AT]", "@", StringComparison.InvariantCultureIgnoreCase)),
                        kind: BrandKind.Industrial);

                    var brandId = await brandsRepo.AddAsync(newBrand);
                    logger.LogInformation("Inserted brand {0} (id = {1})", newBrand.Name, brandId);
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Brand {brand.BrandId}: {ex.Message}");
                }
            }
        }

        private static async Task SeedScales(IScalesRepository scalesRepo, ILogger logger, IScalesFactory scalesFactory)
        {
            var scalesRecords = CsvLoader.LoadRecords<ScaleRecord>(@"..\..\Resources\scales.csv");
            logger.LogInformation("Found {0} scale(s) to seed", scalesRecords.Count());

            var first = scalesRecords
                .Where(it => string.IsNullOrEmpty(it.Slug) == false)
                .Select(it => Slug.Of(it.Slug))
                .First();

            bool alreadyRun = await scalesRepo.ExistsAsync(first);
            if (alreadyRun)
            {
                logger.LogInformation("Catalog seed already run for scales - skipped");
                return;
            }

            foreach (var scale in scalesRecords)
            {
                try
                {
                    var slug = Slug.Of(scale.Slug);

                    var newScale = scalesFactory.NewScale(
                        scaleId: scale.ScaleId,
                        name: scale.Name,
                        slug: scale.Slug,
                        ratio: scale.Ratio,
                        gaugeMm: scale.Gauge,
                        gaugeIn: null,
                        trackType: scale.TrackGauge ?? TrackGauge.Standard.ToString(),
                        description: null,
                        weight: null,
                        created: DateTime.UtcNow,
                        lastModified: null,
                        version: 1);

                    var scaleId = await scalesRepo.AddAsync(newScale);
                    logger.LogInformation("Inserted scale {0} (id = {1})", scale.Name, scaleId);
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Scale {scale.ScaleId}: {ex.Message}");
                }
            }
        }

        private static async Task SeedRailways(IRailwaysRepository railwaysRepo, ILogger logger, IRailwaysFactory railwaysFactory)
        {
            var railwayRecords = CsvLoader.LoadRecords<RailwayRecord>(@"..\..\Resources\railways.csv");
            logger.LogInformation("Found {0} railway(s) to seed", railwayRecords.Count());

            var first = railwayRecords
                .Where(it => string.IsNullOrEmpty(it.Slug) == false)
                .Select(it => Slug.Of(it.Slug))
                .First();

            bool alreadyRun = await railwaysRepo.ExistsAsync(first);
            if (alreadyRun)
            {
                logger.LogInformation("Catalog seed already run for railways - skipped");
                return;
            }

            foreach (var railway in railwayRecords)
            {
                try
                {
                    var slug = Slug.Of(railway.Slug);

                    var newRailway = railwaysFactory.NewRailway(
                        railwayId: railway.RailwayId,
                        name: railway.Name,
                        slug: slug.ToString(),
                        companyName: railway.CompanyName,
                        countryCode: railway.Country,
                        operatingSince: null,
                        operatingUntil: null,
                        active: true, //railway.Status == "active",
                        gaugeMm: railway.TrackGauge,
                        gaugeIn: null,
                        trackGauge: null,
                        headquarters: null,
                        totalLengthMi: null,
                        totalLengthKm: railway.Length,
                        websiteUrl: railway.Website,
                        created: DateTime.UtcNow,
                        lastModified: null,
                        version: 1);

                    var railwayId = await railwaysRepo.AddAsync(newRailway);
                    logger.LogInformation("Inserted railway {0} (id = {1})", newRailway.Name, railwayId);
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Railway {railway.RailwayId}: {ex.Message}");
                }
            }
        }
#pragma warning restore CA1031 // Do not catch general exception types
#pragma warning restore CA1303 // Do not pass literals as localized parameters
    }
}
