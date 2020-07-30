using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TreniniDotNet.Infrastructure.Persistence.Images.Configuration
{
    public class ImageConfiguration  : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("images");
            builder.HasKey(x => x.Filename);

            builder.Property(x => x.Filename)
                .HasColumnName("filename")
                .IsUnicode()
                .HasMaxLength(15);

            builder.Property(x => x.ContentType)
                .HasColumnName("content_type")
                .IsUnicode()
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(x => x.Content)
                .HasColumnName("content")
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasColumnName("is_deleted");

            builder.Property(x => x.CreatedDate)
                .HasColumnName("created");
        }
    }
}
