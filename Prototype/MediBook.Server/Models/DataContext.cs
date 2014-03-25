using System.Data.Entity;

using MediBook.Shared.Models;

using Microsoft.AspNet.Identity.EntityFramework;

namespace MediBook.Server.Models
{
    public class DataContext : DbContext
    {
        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<AppointmentTypeModel> AppointmentTypes { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<LocationModel> Locations { get; set; }

        public DataContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id).ToTable("AspNetRoles");

            modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId).ToTable("AspNetUserLogins");

            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId }).ToTable("AspNetUserRoles");

            modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");
        }
    }
}