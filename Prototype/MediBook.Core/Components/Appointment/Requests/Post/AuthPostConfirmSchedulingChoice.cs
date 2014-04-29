using System;
using System.Collections.Generic;

using MediBook.Client.Core.Networking;
using MediBook.Shared.Models;

namespace MediBook.Client.Core.Components.Appointment.Requests.Post
{
    public class AuthPostConfirmSchedulingChoice : AuthPostRequest
    {
        public AuthPostConfirmSchedulingChoice(Guid appointmentId, PossibleTime time)
            : base("Appointment/ConfirmSchedulingChoice")
        {
            //For some reason passing the possible time object directly isn't serialised properly.
            //Send fields instead
            this.Request.AddParameter("AppointmentId", appointmentId);
            this.Request.AddParameter("Time", time.Time);
            this.Request.AddParameter("AppointmentsToCancel", time.AppointmentsToCancel);
        }
    }
}
