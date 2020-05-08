using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Types.Entities;

namespace IADbContext.Configurations
{
    public class AssetPriceConfiguration : BaseEntityConfiguration<AssetPrice>
    {
        public void Configure(EntityTypeBuilder<AssetPrice> builder)
        {
            builder.Property(x => x.AssetId).IsRequired();
            builder.Property(x => x.OpenPrice).IsRequired();
            builder.Property(x => x.HighPrice).IsRequired();
            builder.Property(x => x.LowPrice).IsRequired();
            builder.Property(x => x.ClosePrice).IsRequired();
            builder.Property(x => x.Date).IsRequired();

            builder.HasOne(x => x.Asset)
                .WithMany(x => x.AssetPrices)
                .HasForeignKey(x => x.AssetId);
        }
    }
}
