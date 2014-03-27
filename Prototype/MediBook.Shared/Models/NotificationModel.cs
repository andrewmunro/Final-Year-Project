using System;

namespace MediBook.Shared.Models
{
    public class NotificationModel
    {
        public Guid ID { get; set; }

        public virtual AppointmentModel Appointment { get; set; }

        public DateTime DueTime { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}
