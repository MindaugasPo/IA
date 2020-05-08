using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Types.Entities;

namespace IADbContext.Configurations
{
    public class AssetConfiguration : BaseEntityConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.Ticker).HasMaxLength(10);
            builder.Property(x => x.AssetType).IsRequired();
        }
    }
}
