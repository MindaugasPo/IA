using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Types;
using Types.Entities;

namespace IADbContext
{
    public class IAContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetPrice> AssetPrices { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        public IAContext(DbContextOptions<IAContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
