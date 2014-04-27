using System.Collections.Generic;

using MediBook.Shared.Models;

namespace MediBook.Client.Core.Components.Notification
{
    public class NotificationComponent : ComponentBase
    {
        public List<NotificationModel> Notifications { get; set; }

        public NotificationComponent(AppCore core)
            : base(core)
        {
            Notifications = new List<NotificationModel>();
        }

        public void AddNotification(string s, string s1, string s2)
        {
        }
    }
}
