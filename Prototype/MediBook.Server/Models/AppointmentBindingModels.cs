using System;
using System.ComponentModel.DataAnnotations;

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
}