using System;

using MediBook.Client.Core.Database;
using MediBook.Shared.Constants.Appointment;

namespace MediBook.Client.Core.Components.Appointment.Models
{
    public class Appointment
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }

        public string DrName { get; set; }

        public string MiscInfo { get; set; }

        public AppointmentStatus Status { get; set; }

        public AppointmentType Type { get; set; }

        public DateTime TimeCreated { get; set; }

        public DateTime TimeScheduled { get; set; }

        public float Duration { get; set; } //In hours
    }
}
