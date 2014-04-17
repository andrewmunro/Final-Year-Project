using MediBook.Server.Models.Enums;
using MediBook.Shared.Models;
using MediBook.Shared.Models.Enums;

namespace MediBook.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.DataContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Models.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Locations.Add(
                new LocationModel()
                {
                    Name = "TestHospital",
                    GoogleMapsUri =
                        "https://maps.google.co.uk/maps?q=Nuffield+Health+Leeds+Hospital&hl=en&ll=53.801867,-1.548042&spn=0.022532,0.066047&sll=53.801867,-1.548042&sspn=0.022532,0.066047&hq=Nuffield+Health+Leeds+Hospital&t=m&z=15"
                });

            context.SaveChanges();

            context.Doctors.Add(new DoctorModel() { UserName = "TestDoctor", FirstName = "Test", LastName = "Doctor" });

            context.SaveChanges();

            context.AppointmentTypes.Add(
                new AppointmentTypeModel()
                {
                    Type = "SampleAppointment",
                    AvailableDoctors = context.Doctors.ToList(),
                    CreatableByPatients = true,
                    Description =
                        "A sample appointment type. An appointment of this type lasts 30 minutes and can be performed by all doctors. It can be created by patients.",
                    TimeSlot = 30
                });

            context.SaveChanges();

            context.Patients.Add(new PatientModel()
            {
                FirstName = "Andrew",
                LastName = "Munro",
                UserName = "Test1"
            });

            context.SaveChanges();

            context.Appointments.Add(
                new AppointmentModel()
                {
                    ID = new Guid(),
                    CreationTime = DateTime.Now,
                    Doctor = context.Doctors.First(),
                    Location = context.Locations.First(),
                    Patient = context.Patients.Find("Test1"),
                    Priority = PriorityGroup.P1,
                    RequiredAppointmentSlots = 2,
                    Status = AppointmentStatus.Unscheduled,
                    Type = context.AppointmentTypes.Find("SampleAppointment")
                });

            context.SaveChanges();
        }
    }
}
