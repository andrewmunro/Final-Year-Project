using System;

namespace MediBook.Client.Core.Components.Notification.Models
{
    public class AndroidNotificationModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
