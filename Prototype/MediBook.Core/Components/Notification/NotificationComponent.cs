using System;
using System.Collections.Generic;

using MediBook.Client.Core.Components.Notification.Models;
using MediBook.Shared.Models;

namespace MediBook.Client.Core.Components.Notification
{
    public class NotificationComponent : ComponentBase
    {
        public List<AndroidNotificationModel> Notifications { get; set; }

        public NotificationComponent(AppCore core)
            : base(core)
        {
            Notifications = new List<AndroidNotificationModel>();
        }

        public void AddNotification(string title, string body, string appointmentId)
        {
            Notifications.Insert(0, new AndroidNotificationModel(){Title = title, Body = body, AppointmentId = Guid.ParseExact(appointmentId, "D")});
        }
    }
}
