using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Configuration
{
    public sealed class ShopFavouritesConfiguration : IEntityTypeConfiguration<ShopFavourite>
    {
        public void Configure(EntityTypeBuilder<ShopFavourite> builder)
        {
            builder.ToTable("shop_favourites");
            builder.HasKey(x => new { x.Owner, x.ShopId });

            builder.Property(x => x.Owner)
                .HasColumnName("owner")
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode()
                .HasConversion(
                    o => o.ToString(),
                    s => new Owner(s));

            builder.HasOne(x => x.Shop);
            builder.Property(x => x.ShopId)
                .HasColumnName("shop_id");
        }
    }
}
