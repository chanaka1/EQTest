using EQTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQTest.Data
{
    public class EQTestContext : DbContext
    {

        public EQTestContext(DbContextOptions<EQTestContext> options)
            : base(options)
        { }

        public DbSet<Permit> Permits { get; set; }

        public DbSet<PermitDateGroup> PermitDateGroups { get; set; }

        public DbSet<Lot> Lots { get; set; }

        public DbSet<LotLocation> LotLocations { get; set; }

        public DbSet<PermitLot> PermitLots { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<PermitDateTimeGroup> PermitDateTimeGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermitLot>(entity =>
            {
                entity.HasKey(e => new { e.PermitId, e.LotId }).HasName("PK_PermitLot");
            });

            // modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.ProductId });
        }

    }
}
