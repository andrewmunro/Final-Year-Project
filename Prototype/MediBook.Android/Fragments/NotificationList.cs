using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MediBook.Client.Android.Fragments
{
    public class NotificationList : ListFragment
    {
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            var testItems = new string[] { "Notification1", "Notification2", "Notification3", "Notification4", "Notification4", "Notification5" };
            this.ListAdapter = new ArrayAdapter<string>(Activity, global::Android.Resource.Layout.SimpleListItem1, testItems);
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