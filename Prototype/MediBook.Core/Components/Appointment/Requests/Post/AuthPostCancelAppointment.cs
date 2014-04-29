using System;

using MediBook.Client.Core.Networking;

namespace MediBook.Client.Core.Components.Appointment.Requests.Post
{
    public class AuthPostCancelAppointment : AuthPostRequest
    {
        public AuthPostCancelAppointment(Guid appointmentId)
            : base("Appointment/CancelAppointment")
        {
            this.Request.AddParameter("AppointmentId", appointmentId);
        }
    }
}
