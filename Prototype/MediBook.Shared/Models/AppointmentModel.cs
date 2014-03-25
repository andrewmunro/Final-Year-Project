using System;
using System.ComponentModel.DataAnnotations;

using MediBook.Server.Models.Enums;
using MediBook.Shared.Constants.Appointment;

namespace MediBook.Shared.Models
{
    public class AppointmentModel
    {
        [Key]
        public Guid ID { get; set; }

        public virtual AppointmentTypeModel TypeModel { get; set; }

        public virtual DoctorModel DoctorModel { get; set; }

        public virtual PatientModel Patient { get; set; }

        public virtual LocationModel Location { get; set; }

        public AppointmentStatus Status { get; set; }

        public int RequiredAppointmentSlots { get; set; }

        public DateTime CreationTime { get; set; }

        public PriorityGroup Priority { get; set; }
    }
}