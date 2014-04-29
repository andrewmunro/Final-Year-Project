using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using MediBook.Server.Models;
using MediBook.Shared.Models;
using MediBook.Shared.utils;

namespace MediBook.Server
{
    public static class ExampleScheduler
    {
        private static DataContext db = new DataContext();

        public static SchedulingResult GetSchedulingOptions(AppointmentModel appointment, DateTime requestedTime)
        {
            var result = new SchedulingResult();

            var time = requestedTime;
            var appointmentDuration = appointment.RequiredAppointmentSlots * appointment.Type.TimeSlot;

            //Stop after 3 times are added
            while (result.PossibleTimes.Count < 3)
            {
                var endTime = time.AddMinutes(appointmentDuration);

                var conflictingAppointments = GetConflictingAppointmentsAtTime(appointment.Type.Type, time, endTime);

                if (conflictingAppointments.Count == 0)
                {
                    result.PossibleTimes.Add(new PossibleTime() { Time = time.ToParsableString() });
                }
                else if (ConflictsLowerPriority(appointment, conflictingAppointments))
                {
                    result.PossibleTimes.Add(new PossibleTime(){ Time = time.ToParsableString(), AppointmentsToCancel = conflictingAppointments.Select(ca => ca.ID).ToList() });
                }

                //Stop after 1 time if original requested time is available
                if (time == requestedTime && result.PossibleTimes.Count == 1) break;

                //Increment by an hour and try again
                time = time.AddHours(1);
            }

            return result;
        }

        //Returns true if ALL conflicting appointments have a lower priority than the appointment
        private static bool ConflictsLowerPriority(AppointmentModel appointment, IEnumerable<AppointmentModel> conflictingAppointments)
        {
            //uses the greater opperator(>) instead of lower (<) because it converts the enum to int values when comparing
            return conflictingAppointments.All(conflictingAppointment => conflictingAppointment.Priority > appointment.Priority);
        }

        private static List<AppointmentModel> GetConflictingAppointmentsAtTime(string appointmentType, DateTime startTime, DateTime endTime)
        {
            //Get appointments of the same type and are currently scheduled
            var scheduledAppointments = db.Appointments.Where(ap => ap.Type.Type == appointmentType && ap.ScheduledTime != null).ToList();

            return scheduledAppointments.Where(ap =>
            (ap.ScheduledTime < startTime &&
             startTime < ap.ScheduledEndTime)
            ||
            (ap.ScheduledTime < endTime &&
             endTime <= ap.ScheduledEndTime)
            ||
            (startTime < ap.ScheduledTime &&
             ap.ScheduledTime < endTime)
            ||
            (startTime < ap.ScheduledEndTime &&
             ap.ScheduledEndTime <= endTime)
            ).ToList();
        }
    }

    public class SchedulingResult
    {
        public List<PossibleTime> PossibleTimes { get; set; }

        public SchedulingResult()
        {
            PossibleTimes = new List<PossibleTime>();
        }
    }
}