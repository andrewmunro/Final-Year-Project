using System.Data.Entity;
using System.Linq;

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
        public DbSet<NotificationModel> Notifications { get; set; }

        public DataContext()
        {
            /*
            Example LINQ statement to select rows of data 
            from the Patients table where the name is 'Andrew'
            */
            var patientsCalledAndrew = Patients.Where(patients => patients.FirstName == "Andrew");


            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppointmentModel>().Ignore(t => t.ScheduledEndTime);

            modelBuilder.Entity<PatientModel>().Map(x =>
            {
                x.MapInheritedProperties();
                x.ToTable("PatientModels");
            });

            modelBuilder.Entity<DoctorModel>().Map(x =>
            {
                x.MapInheritedProperties();
                x.ToTable("DoctorModels");
            });

            modelBuilder.Entity<AppointmentModel>().Property(p => p.ScheduledTime).HasColumnType("datetime2");
            modelBuilder.Entity<AppointmentModel>().Property(p => p.CreationTime).HasColumnType("datetime2");
            modelBuilder.Entity<NotificationModel>().Property(p => p.DueTime).HasColumnType("datetime2");

            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id).ToTable("AspNetRoles");

            modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId).ToTable("AspNetUserLogins");

            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId }).ToTable("AspNetUserRoles");

            modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");
        }
    }
}