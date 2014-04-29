using System;
using System.Data.Entity.Migrations;
using System.Linq;

using MediBook.Server.Models;
using MediBook.Shared.Enums;
using MediBook.Shared.Models;

namespace MediBook.Server.Migrations
{
    public static class ExampleData
    {
        public static void Seed(string patientId)
        {
            var context = new DataContext();

            var appointment = new AppointmentModel()
            {
                ID = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                ScheduledTime = null,
                Doctor = context.Doctors.Find("TestDoctor2"),
                Location = context.Locations.First(),
                Patient = context.Patients.Find(patientId),
                Priority = PriorityGroup.P1,
                RequiredAppointmentSlots = 2,
                Status = AppointmentStatus.Unscheduled,
                Type = context.AppointmentTypes.Find("Day Surgery")
            };

            context.Appointments.AddOrUpdate(appointment);

            var appointment2 = new AppointmentModel()
            {
                ID = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                ScheduledTime = null,
                Doctor = context.Doctors.First(),
                Location = context.Locations.First(),
                Patient = context.Patients.Find(patientId),
                Priority = PriorityGroup.P1,
                RequiredAppointmentSlots = 2,
                Status = AppointmentStatus.Unscheduled,
                Type = context.AppointmentTypes.Find("SampleAppointment")
            };

            context.Appointments.AddOrUpdate(appointment2);

            var appointment3 = new AppointmentModel()
            {
                ID = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                ScheduledTime = null,
                Doctor = context.Doctors.Find("TestDoctor2"),
                Location = context.Locations.First(),
                Patient = context.Patients.Find(patientId),
                Priority = PriorityGroup.P1,
                RequiredAppointmentSlots = 2,
                Status = AppointmentStatus.Unscheduled,
                Type = context.AppointmentTypes.Find("Day Surgery")
            };

            context.Appointments.AddOrUpdate(appointment3);

            context.SaveChanges();
        }
    }
}