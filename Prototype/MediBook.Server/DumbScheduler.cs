using System;
using System.Collections.Generic;

using MediBook.Server.Models;
using MediBook.Shared.Models;

namespace MediBook.Server
{
    public static class DumbScheduler
    {
        private static DataContext db = new DataContext();

        public static List<DateTime> GetSchedulingOptions(AppointmentModel appointment)
        {
            //TODO Implement
            var appointmentLength = appointment.RequiredAppointmentSlots * appointment.Type.TimeSlot;
            return new List<DateTime>();
        }
    }
}