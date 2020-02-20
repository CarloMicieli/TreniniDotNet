using System;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Infrastracture.Persistence.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ApplyConfiguration(new BrandsConfiguration());
            builder.ApplyConfiguration(new RailwaysConfiguration());
            builder.ApplyConfiguration(new ScalesConfiguration());
            builder.ApplyConfiguration(new CatalogItemConfiguration());
            builder.ApplyConfiguration(new RollingStockConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Railway> Railways { get; set; } = null!;
        public DbSet<Scale> Scales { get; set; } = null!;
        public DbSet<CatalogItem> CatalogItems { get; set; } = null!;
    }
}