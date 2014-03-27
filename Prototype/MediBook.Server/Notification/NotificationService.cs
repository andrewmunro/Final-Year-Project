using System;
using System.Linq;
using System.Reactive.Linq;

using MediBook.Server.Models;
using MediBook.Shared.Models;

namespace MediBook.Server.Notification
{
    public class NotificationService
    {
        private readonly DataContext db = new DataContext();

        public NotificationService()
        {
            db.Notifications.ToList().ForEach(this.ScheduleNotification);
        }

        public void AddNotification(AppointmentModel appointment, string senderID, string title, string body, DateTime dueTime)
        {
            var notification = new NotificationModel()
                                   {
                                       ID = new Guid(),
                                       Appointment = appointment,
                                       Title = title,
                                       Body = body,
                                       DueTime = dueTime
                                   };
            AddNotification(notification);
        }

        public void AddNotification(NotificationModel notification)
        {
            ScheduleNotification(db.Notifications.Add(notification));
        }

        private void ScheduleNotification(NotificationModel notification)
        {
            Observable.Timer(notification.DueTime).Subscribe(e => SendNotification(notification));   
        }

        private void SendNotification(NotificationModel notification)
        {
            var note = new PushNotification(notification);
            if (db.Notifications.Any(n => n.ID == notification.ID)) db.Notifications.Remove(notification);
        }
    }
}