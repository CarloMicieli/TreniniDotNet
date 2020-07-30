using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Configuration;
using TreniniDotNet.Infrastructure.Persistence.Collecting.Configuration;
using TreniniDotNet.Infrastructure.Persistence.Images;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseNpgsql("Host=localhost;Database=TreniniDb;Username=tdbuser;Password=tdbpass");
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new RailwayConfiguration());
            modelBuilder.ApplyConfiguration(new ScaleConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogItemConfiguration());
            modelBuilder.ApplyConfiguration(new RollingStockConfiguration());
            modelBuilder.ApplyConfiguration(new LocomotiveConfiguration());
            modelBuilder.ApplyConfiguration(new FreightCarConfiguration());
            modelBuilder.ApplyConfiguration(new TrainConfiguration());
            modelBuilder.ApplyConfiguration(new PassengerCarConfiguration());
            modelBuilder.ApplyConfiguration(new CollectionConfiguration());
            modelBuilder.ApplyConfiguration(new CollectionItemConfiguration());
            modelBuilder.ApplyConfiguration(new ShopConfiguration());
            modelBuilder.ApplyConfiguration(new WishlistConfiguration());
            modelBuilder.ApplyConfiguration(new WishlistItemConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Brand> Brands { set; get; } = null!;
        public DbSet<Railway> Railways { set; get; } = null!;
        public DbSet<Scale> Scales { get; set; } = null!;
        public DbSet<CatalogItem> CatalogItems { get; set; } = null!;
        public DbSet<Collection> Collections { get; set; } = null!;
        public DbSet<Shop> Shops { get; set; } = null!;
        public DbSet<Wishlist> Wishlists { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
    }
}

