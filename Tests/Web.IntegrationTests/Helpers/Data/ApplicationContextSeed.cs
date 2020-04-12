using Microsoft.Extensions.DependencyInjection;
using System;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.IntegrationTests.Helpers.Data
{
    public class ApplicationContextSeed
    {
        public static void SeedCatalog(IServiceProvider scopedServices)
        {
            IBrandsRepository brands = scopedServices.GetRequiredService<IBrandsRepository>();
            brands.SeedDatabase();

            IRailwaysRepository railways = scopedServices.GetRequiredService<IRailwaysRepository>();
            railways.SeedDatabase();

            IScalesRepository scales = scopedServices.GetRequiredService<IScalesRepository>();
            scales.SeedDatabase();
        }
    }
}