using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Types.Entities;

namespace IADbContext.Configurations
{
    public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(x => x.AssetId).IsRequired();
            builder.Property(x => x.PortfolioId).IsRequired();
            builder.Property(x => x.OpenPrice).IsRequired();
            builder.Property(x => x.OpenDateUtc).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Commission).IsRequired();
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
