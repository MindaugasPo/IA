using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Types.Entities;

namespace IADbContext.Configurations
{
    public class AssetConfiguration : BaseEntityConfiguration<Asset>
    {
        public override void Configure(EntityTypeBuilder<Asset> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Ticker).IsRequired().HasMaxLength(10);
            builder.Property(x => x.AssetType).IsRequired();
        }
    }
}
