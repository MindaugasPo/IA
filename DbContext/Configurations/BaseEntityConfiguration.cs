using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Types.Entities;

namespace IADbContext.Configurations
{
    public abstract class BaseEntityConfiguration<T> 
        : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDateUtc).IsRequired();
        }
    }
}
