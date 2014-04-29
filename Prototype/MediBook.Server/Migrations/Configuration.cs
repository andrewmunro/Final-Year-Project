using System.Collections.Generic;

using MediBook.Shared.Enums;
using MediBook.Shared.Models;

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
             context.Locations.AddOrUpdate(
                 new LocationModel()
                 {
                     Name = "TestHospital",
                     ContactNumber = "01234-5678910",
                     Latititude = 53.8010068,
                     Longititude = -1.553798
                 });

             context.Doctors.AddOrUpdate(new DoctorModel() { UserName = "TestDoctor", FirstName = "Test", LastName = "Doctor", DoctorType = "Primary Care Doctor", ImageURL = "http://www.colourbox.com/preview/4315067-946968-portrait-american-doctor-on-hospital-ward.jpg" });

             context.SaveChanges();

/*             context.AppointmentTypes.AddOrUpdate(
                 new AppointmentTypeModel()
                 {
                     Type = "SampleAppointment",
                     AvailableDoctors = context.Doctors.ToList(),
                     CreatableByPatients = true,
                     Description =
                         "A sample appointment type. An appointment of this type lasts 30 minutes and can be performed by all doctors. It can be created by patients.",
                     TimeSlot = 30
                 });

             context.SaveChanges();*/

             context.Patients.AddOrUpdate(new PatientModel()
             {
                 FirstName = "Andrew",
                 LastName = "Munro",
                 UserName = "Test1",
                 GcmRegistrationId = ""
             });

             context.SaveChanges();
             

            var appointment = new AppointmentModel()
                                  {
                                      ID = Guid.NewGuid(),
                                      CreationTime = DateTime.Now,
                                      ScheduledTime = null,
                                      Doctor = context.Doctors.First(),
                                      Location = context.Locations.First(),
                                      Patient = context.Patients.Find("Test1"),
                                      Priority = PriorityGroup.P1,
                                      RequiredAppointmentSlots = 2,
                                      Status = AppointmentStatus.Unscheduled,
                                      Type = context.AppointmentTypes.Find("SampleAppointment")
                                  };

            context.Appointments.AddOrUpdate(appointment);

            context.SaveChanges();

            context.Notifications.AddOrUpdate(
                new NotificationModel()
                    {
                        ID = Guid.NewGuid(),
                        Appointment = appointment,
                        Title = "Test Notification!",
                        Body = "Test notification, lorel ipsum etc.",
                        DueTime = DateTime.Now.AddMinutes(5)
                    });

            context.SaveChanges();
        }
    }
}
