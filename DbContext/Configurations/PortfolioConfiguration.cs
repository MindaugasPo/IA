using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Types.Entities;

namespace IADbContext.Configurations
{
    public class PortfolioConfiguration : BaseEntityConfiguration<Portfolio>
    {
        public override void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
        }
    }
}
