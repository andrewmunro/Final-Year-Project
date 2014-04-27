using System;
using System.Linq;
using System.Reactive.Linq;

using MediBook.Server.Models;
using MediBook.Shared.Enums;
using MediBook.Shared.Models;
using MediBook.Shared.utils;

namespace MediBook.Server.Notification
{
    public class NotificationService
    {
        private readonly DataContext db = new DataContext();

        private static NotificationService instance;
        public static NotificationService Instance { get { return instance ?? new NotificationService(); } }

        public NotificationService()
        {
            instance = this;
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
            Observable.Timer(notification.DueTime).Subscribe(e => GetNotificationAndSend(notification.ID));   
        }

        private void GetNotificationAndSend(Guid notificationID)
        {
            var database = new DataContext();
            var notification = database.Notifications.Find(notificationID);

            this.SendNotification(notification.Appointment, notification.Title, notification.Body);

            database.Notifications.Remove(notification);
            database.SaveChanges();
        }

        private void SendNotification(AppointmentModel appointment, string title, string body)
        {
            var registrationId = appointment.Patient.GcmRegistrationId;
            if (registrationId == null) Console.WriteLine("Notification failed to send because there are no deviceIDs set");
            else new PushNotification(registrationId, title, body, appointment.ID.ToString());
        }

        public void AddNotification(AppointmentModel appointment, NotificationType scheduled)
        {
            switch (scheduled)
            {
                case NotificationType.Scheduled:
                    this.SendNotification(appointment, "Appointment Scheduled", "Your appointment has been successfully scheduled for " + appointment.ScheduledTime.Value.ToFormattedString());
                    break;
            }
        }
    }
}