using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.Catalog;
using TreniniDotNet.Infrastructure.Persistence.Collecting;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;

namespace Infrastructure.UnitTests.Persistence.Database.Testing
{
    public abstract class EfRepositoryUnitTests<TRepository> : IDisposable
    {
        private Func<ApplicationDbContext, TRepository> RepositoryBuilder { get; }
        private DbContextOptions<ApplicationDbContext> ContextOptions { get; }

        private DbConnection Connection { get; }

        public EfRepositoryUnitTests(Func<ApplicationDbContext, TRepository> repositoryBuilder)
        {
            RepositoryBuilder = repositoryBuilder;

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlite(CreateInMemoryDatabase());
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            ContextOptions = builder.Options;

            Connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        public void Dispose() => Connection.Dispose();

        protected ApplicationDbContext NewDbContext() => new ApplicationDbContext(ContextOptions);

        protected async Task<TRepository> Repository(ApplicationDbContext dbContext, Create createOption = Create.WithEmptyDatabase)
        {
            if (createOption == Create.WithSeedData)
            {
                await SeedDatabase(NewDbContext());
            }
            else if (createOption == Create.WithEmptyDatabase)
            {
                await ResetDatabase(NewDbContext());
            }

            return RepositoryBuilder(dbContext);
        }

        protected static async Task ResetDatabase(ApplicationDbContext dbContext)
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
        }

        protected static async Task SeedDatabase(ApplicationDbContext dbContext)
        {
            await ResetDatabase(dbContext);

            var brands = new BrandsRepository(dbContext);
            await brands.SeedDatabase();

            var railways = new RailwaysRepository(dbContext);
            await railways.SeedDatabase();

            var scales = new ScalesRepository(dbContext);
            await scales.SeedDatabase();

            var catalogItems = new CatalogItemsRepository(dbContext);
            await catalogItems.SeedDatabase();

            var collections = new CollectionsRepository(dbContext);
            await collections.SeedDatabase();

            var shops = new ShopsRepository(dbContext);
            await shops.SeedDatabase();

            var wishlists = new WishlistsRepository(dbContext);
            await wishlists.SeedDatabase();

            await dbContext.SaveChangesAsync();
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            return connection;
        }
    }

    public enum Create
    {
        WithEmptyDatabase,
        WithSeedData,
        WithCurrentDatabase
    }
}
