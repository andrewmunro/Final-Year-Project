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

        private static NotificationService instance;

        public static NotificationService Instance
        {
            get
            {
                if (instance == null) return instance = new NotificationService();
                return instance;
            }
        }

        public NotificationService()
        {
            db.Notifications.ToList().ForEach(this.ScheduleNotification);
        }

        public void AddNotification(Guid appointmentGUID, string title, string body, DateTime dueTime)
        {
            var notification = new NotificationModel()
                                   {
                                       ID = Guid.NewGuid(),
                                       Appointment = db.Appointments.Find(appointmentGUID),
                                       Title = title,
                                       Body = body,
                                       DueTime = dueTime
                                   };
            AddNotification(notification);
        }

        public void AddNotification(NotificationModel notification)
        {
            db.Notifications.Add(notification);
            db.SaveChanges();
            ScheduleNotification(notification);
        }

        private void ScheduleNotification(NotificationModel notification)
        {
            Observable.Timer(notification.DueTime).Subscribe(e => SendNotification(notification.ID));   
        }

        private void SendNotification(Guid notificationID)
        {
            var database = new DataContext();
            var notification = database.Notifications.Find(notificationID);
            if (notification.Appointment.Patient.GcmRegistrationId == null) Console.WriteLine("Notification failed to send because there are no deviceIDs set");
            else new PushNotification(notification);
            database.Notifications.Remove(notification);
            database.SaveChanges();
        }
    }
}