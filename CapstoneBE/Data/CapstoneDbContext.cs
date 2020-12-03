using CapstoneBE.Models;
using CapstoneBE.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CapstoneBE.Data
{
    public class CapstoneDbContext : IdentityDbContext<CapstoneBEUser>
    {
        public virtual DbSet<MaintenanceOrder> MaintenanceOrder { get; set; }
        public virtual DbSet<MaintenanceWorker> MaintenanceWorker { get; set; }
        public virtual DbSet<PushNotification> PushNotification { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<LocationHistory> LocationHistory { get; set; }
        public virtual DbSet<Crack> Crack { get; set; }

        public CapstoneDbContext(DbContextOptions options) : base(options)
        {
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetDefaultValue();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            SetDefaultValue();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        //set default value for created and lastModified props
        private void SetDefaultValue()
        {
            IEnumerable<EntityEntry> entries = ChangeTracker.Entries()
                .Where(e => (e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified))
            || e.Entity is CapstoneBEUser);
            foreach (EntityEntry entry in entries)
            {
                DateTime curTime = DateTime.UtcNow;
                switch (entry.Entity)
                {
                    case BaseEntity baseEntity:
                        baseEntity.LastModified = curTime;
                        if (entry.State == EntityState.Added)
                            baseEntity.Created = curTime;
                        break;

                    case CapstoneBEUser userEntity:
                        userEntity.LastModified = curTime;
                        if (entry.State == EntityState.Added)
                            userEntity.Created = curTime;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Seeding roles to AspNetRoles table
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = APIConstants.Roles.AdminRole,
                    NormalizedName = APIConstants.Roles.AdminRole.ToUpper()
                },
                new IdentityRole
                {
                    Id = "3c5e154e-3b0e-446f-86af-483d54fd7210",
                    Name = APIConstants.Roles.ManagerRole,
                    NormalizedName = APIConstants.Roles.ManagerRole.ToUpper()
                },
                new IdentityRole
                {
                    Id = "2c3e174e-3b0e-446f-86af-483d56fd7210",
                    Name = APIConstants.Roles.StaffRole,
                    NormalizedName = APIConstants.Roles.StaffRole.ToUpper()
                });
        }
    }
}