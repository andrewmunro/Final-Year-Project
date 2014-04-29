using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using MediBook.Shared.Models;

namespace MediBook.Server.Models
{
    public class ScheduleAppointmentBinding
    {
        [Required]
        [Display(Name = "Appointment Id")]
        public Guid AppointmentId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Time of the Appointment")]
        public string Time { get; set; }
    }

    public class ConfirmSchedulingChoiceBinding
    {
        [Required]
        [Display(Name = "Appointment Id")]
        public Guid AppointmentId { get; set; }

        [Required]
        [Display(Name = "Chosen Possible time")]
        public String Time { get; set; }

        [Required]
        [Display(Name = "Conflicting Appointment Guids to cancel")]
        public List<Guid> AppointmentsToCancel { get; set; }
    }

    public class CancelAppointmentBinding
    {
        [Required]
        [Display(Name = "Appointment Id")]
        public Guid AppointmentId { get; set; }
    }
}