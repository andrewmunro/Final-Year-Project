using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediBook.Shared.Models
{
    public class AppointmentTypeModel
    {
        [Key]
        [Display(Name = "Unique type of Appointment")]
        public string Type { get; set; }

        [Display(Name = "Average time in minutes that a single appointment slot will take")]
        public int TimeSlot { get; set; }

        [Display(Name = "Description about appointment")]
        public string Description { get; set; }

        [Display(Name = "List of doctors that can manage this type of appointment")]
        public virtual List<DoctorModel> AvailableDoctors { get; set; }

        [Display(Name = "Can patients setup this type of appointment?")]
        public bool CreatableByPatients { get; set; }
    }
}