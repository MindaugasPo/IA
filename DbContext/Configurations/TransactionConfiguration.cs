using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Types.Entities;

namespace IADbContext.Configurations
{
    public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.AssetId).IsRequired();
            builder.Property(x => x.PortfolioId).IsRequired();
            builder.Property(x => x.OpenPrice).IsRequired().HasColumnType("decimal(19,4)");
            builder.Property(x => x.OpenDateUtc).IsRequired();
            builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(19,4)");
            builder.Property(x => x.Commission).IsRequired().HasColumnType("decimal(19,4)");
            builder.Property(x => x.TransactionType).IsRequired();
            builder.Property(x => x.Currency).IsRequired();

            builder.HasOne(x => x.Asset)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AssetId);

            builder.HasOne(x => x.Portfolio)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.PortfolioId);
        }
    }
}
