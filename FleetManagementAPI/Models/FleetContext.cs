using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagementAPI.Models
{
    public class FleetContext : DbContext
    {
        public FleetContext(DbContextOptions<FleetContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Chassis> Chasseez { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Chassis)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => v.ChassisId);

            modelBuilder.Entity<Chassis>()
                .HasMany(c => c.Vehicles)
                .WithOne(v => v.Chassis);
        }
    }
}
