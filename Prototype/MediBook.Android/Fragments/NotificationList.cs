using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using MediBook.Client.Android.Fragments.Adapters;
using MediBook.Client.Android.Screens;
using MediBook.Client.Core;
using MediBook.Client.Core.Components.Appointment;
using MediBook.Client.Core.Components.Notification;

namespace MediBook.Client.Android.Fragments
{
    public class NotificationList : ListFragment
    {
        public AppointmentComponent AppointmentComponent { get { return AppCore.Instance.GetComponent<AppointmentComponent>(); } }

        public NotificationComponent NotificationComponent { get { return AppCore.Instance.GetComponent<NotificationComponent>(); } }

        public override void OnStart()
        {
            base.OnStart();
            this.ListAdapter = new NotificationListAdapter(Activity, NotificationComponent.Notifications);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.AppointmentList, container, false);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var notification = NotificationComponent.Notifications[position];
            AppointmentComponent.ActiveAppointment = AppointmentComponent.Appointments.Find(ap => ap.ID == notification.AppointmentId);

            StartActivity(new Intent (Activity, typeof(AppointmentScreen)));
        }

    }
}