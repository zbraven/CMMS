using CMMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.DataAccess.Context
{
    public class CMMSDbContext : DbContext
    {
        public CMMSDbContext()
        {
            
        }
        public CMMSDbContext(DbContextOptions<CMMSDbContext> options)
        : base(options)
        {

        }

        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MaintenanceTask> MaintenanceTasks { get; set; }
        public DbSet<TaskMaterial> TaskMaterials { get; set; }
    


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActivityLog>().ToTable("ActivityLogs");

        // EmployeeRole çoka çok ilişkisinin yapılandırılması
            modelBuilder.Entity<EmployeeRole>()
                .HasKey(er => new { er.EmployeeId, er.RoleId });

            modelBuilder.Entity<EmployeeRole>()
                .HasOne(er => er.Employee)
                .WithMany(e => e.EmployeeRoles)
                .HasForeignKey(er => er.EmployeeId);

            modelBuilder.Entity<EmployeeRole>()
                .HasOne(er => er.Role)
                .WithMany(r => r.EmployeeRoles)
                .HasForeignKey(er => er.RoleId);
        }

    }
}


