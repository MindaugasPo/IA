using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Types.Entities;

namespace IADbContext.Configurations
{
    public class AssetPriceConfiguration : BaseEntityConfiguration<AssetPrice>
    {
        public override void Configure(EntityTypeBuilder<AssetPrice> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.AssetId).IsRequired();
            builder.Property(x => x.OpenPrice).IsRequired().HasColumnType("decimal(19,4)");
            builder.Property(x => x.HighPrice).IsRequired().HasColumnType("decimal(19,4)");
            builder.Property(x => x.LowPrice).IsRequired().HasColumnType("decimal(19,4)");
            builder.Property(x => x.ClosePrice).IsRequired().HasColumnType("decimal(19,4)");
            builder.Property(x => x.Date).IsRequired();

            builder.HasOne(x => x.Asset)
                .WithMany(x => x.AssetPrices)
                .HasForeignKey(x => x.AssetId);
        }
    }
}
