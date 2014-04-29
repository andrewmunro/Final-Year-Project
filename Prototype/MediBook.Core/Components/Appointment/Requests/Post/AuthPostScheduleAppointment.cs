using System;

using MediBook.Client.Core.Networking;
using MediBook.Shared.utils;

namespace MediBook.Client.Core.Components.Appointment.Requests.Post
{
    public class AuthPostScheduleAppointment : AuthPostRequest
    {
        public AuthPostScheduleAppointment(Guid appointmentId, DateTime time)
            : base("Appointment/ScheduleAppointment")
        {
            this.Request.AddParameter("AppointmentId", appointmentId);
            this.Request.AddParameter("Time", time.ToUniversalTime().ToParsableString());
        }
    }
}
