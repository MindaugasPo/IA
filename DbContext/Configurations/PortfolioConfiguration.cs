using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Types.Entities;

namespace IADbContext.Configurations
{
    public class PortfolioConfiguration : BaseEntityConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
