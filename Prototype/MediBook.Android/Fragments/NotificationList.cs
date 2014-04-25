using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MediBook.Client.Android.Fragments
{
    public class NotificationList : ListFragment
    {
        private List<string> Notifications { get; set; }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            Notifications = new List<string>();
            
            this.RefreshItems();
        }

        public void AddNotification(string notification)
        {
            Notifications.Add(notification);
            this.RefreshItems();
        }

        private void RefreshItems()
        {
            this.ListAdapter = new ArrayAdapter<string>(Activity, global::Android.Resource.Layout.SimpleListItem1, Notifications.ToArray());
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.NotificationList, container, false);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            this.ShowNotification(position);
        }

        private void ShowNotification(int appointmentID)
        {

        }

    }
}